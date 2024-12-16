using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Xml;

namespace ClientDownloader
{
    class Program
    {
        static readonly HttpClient webClient = new();

        static readonly List<string> CDNs = new()
            {
                "dist.blizzard.com",
                "dist.blizzard.com.edgesuite.net",
                "blzddist1-a.akamaihd.net",
                "blzddist2-a.akamaihd.net",
                "blzddist3-a.akamaihd.net",
                "level3.blizzard.com",
            };

        private static bool TryDownloadFile(string path, string outFile)
        {
            foreach (var cdn in CDNs)
            {
                var catalogUrl = $"http://{cdn}/repair/" + path;
                try
                {
                    File.WriteAllBytes(outFile, webClient.GetByteArrayAsync(catalogUrl).Result);
                    return true;
                }
                catch (Exception e)
                {
                }
            }

            return false;
        }

        static void Main(string[] args)
        {
            var targetDirectory = "E:\\WotLKForUpgrade\\FromRepair";
            var cacheDir = "\\\\martin-nas\\Raid2024\\WoW\\Old CDN\\repair\\";
            var tryCDNs = false;

            var repairConfig = "http://dist.blizzard.com.edgesuite.net/repair/wow/repairconfig_eu_B533860ECA708FA85339E7E7885C91A8.xml";

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(repairConfig);

            var versions = xmlDocument.GetElementsByTagName("version");

            foreach (XmlElement version in versions)
            {
                var catalogHash = version.GetAttribute("catalog");
                var gameVersion = version.GetAttribute("gameversion");
                var subPath = version.GetAttribute("subpath");

                Console.WriteLine($"Process build: {gameVersion} ...");

                var cachePath = Path.Combine(cacheDir, subPath, catalogHash[0].ToString(), catalogHash[1].ToString(), catalogHash);

                if (!File.Exists(cachePath))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Repair catalog {catalogHash} for {gameVersion} not found in cache, trying CDN..");
                    Console.ResetColor();

                    var success = false;
                    var path = $"{subPath}/{catalogHash[0]}/{catalogHash[1]}/{catalogHash}";

                    if (tryCDNs)
                        success = TryDownloadFile(path, cachePath);

                    if (!success)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Failed to locate repair catalog {gameVersion} from all CDNs. Skipping build.");
                        Console.ResetColor();
                        continue;
                    }
                }

                var buildOutputDirectory = Path.Combine(targetDirectory, gameVersion);
                if (!Directory.Exists(buildOutputDirectory))
                    Directory.CreateDirectory(buildOutputDirectory);

                var repairCatalog = new RepairCatalog(cachePath);
                foreach (var os in repairCatalog.Entries)
                {
                    foreach (var entry in os.Value)
                    {
                        var repairListPath = Path.Combine(cacheDir, subPath, entry.Value[0].ToString(), entry.Value[1].ToString(), entry.Value);

                        if (!File.Exists(repairListPath))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Repair list {entry.Value} for {gameVersion} {os.Key} {entry.Value} not found in cache, trying CDN..");
                            Console.ResetColor();

                            var success = false;
                            var path = $"{subPath}/{entry.Value[0]}/{entry.Value[1]}/{entry.Value}";

                            if (tryCDNs)
                                success = TryDownloadFile(path, repairListPath);

                            if (!success)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Failed to locate repair list for {gameVersion} {os.Key} {entry.Value} from all CDNs. Skipping build.");
                                Console.ResetColor();
                                continue;
                            }
                        }

                        var repairList = new RepairList(repairListPath);
                        foreach (var fileEntry in repairList.Entries)
                        {
                            var fileName = fileEntry.Key;
                            var fileHash = fileEntry.Value.fileHash;
                            var fileFlags = fileEntry.Value.flags;

                            var repairedFilePath = Path.Combine(cacheDir, subPath, fileHash[0].ToString(), fileHash[1].ToString(), fileHash);

                            if (!File.Exists(repairedFilePath))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Repaired file {repairedFilePath} for {gameVersion} {os.Key} {entry.Value} not found in cache, trying CDN..");
                                Console.ResetColor();

                                var success = false;
                                var path = $"{subPath}/{fileHash[0]}/{fileHash[1]}/{fileHash}";

                                if (tryCDNs)
                                    success = TryDownloadFile(path, repairedFilePath);

                                if (!success)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Failed to locate repaired file {fileName} ({fileEntry.Value}) for {gameVersion} {os.Key} {entry.Value} from all CDNs. Skipping build.");
                                    Console.ResetColor();
                                    continue;
                                }
                            }

                            if (os.Key == "Mac")
                                continue;

                            if (entry.Key != "common" && entry.Key != "enGB" && entry.Key != "enUS")
                                continue;

                            // TODO: Support MPQ repacking
                            if (fileFlags.HasFlag(RepairList.FileFlags.RepackMPQ))
                                continue;

                            var targetPath = Path.Combine(buildOutputDirectory, fileName);
                            if (File.Exists(targetPath))
                                continue;

                            var sourceFileMD5 = Convert.ToHexString(MD5.HashData(File.OpenRead(repairedFilePath)));
                            if (sourceFileMD5 != fileHash)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Repaired file {fileName} ({fileHash}) for {gameVersion} {os.Key} {entry.Value} is corrupted. Skipping.");
                                Console.ResetColor();
                                continue;
                            }

                            var targetDir = Path.GetDirectoryName(targetPath);
                            if (!Directory.Exists(targetDir))
                                Directory.CreateDirectory(targetDir);

                            File.Copy(repairedFilePath, targetPath, true);
                        }
                    }
                }

                // Create MFIL
                var mfilPath = Path.Combine(buildOutputDirectory, "WoW.mfil");
                if (!File.Exists(mfilPath))
                {
                    if (MFILMap.Entries.TryGetValue(gameVersion, out var mfilInfo))
                    {
                        Console.WriteLine("Creating new WoW.mfil...");
                        using (StreamWriter sw = File.CreateText(mfilPath))
                        {
                            sw.WriteLine("version=2");
                            sw.WriteLine("server=akamai");
                            sw.WriteLine("	location=" + mfilInfo.folder);
                            sw.WriteLine("manifest_partial=" + mfilInfo.file);
                        }
                        File.SetAttributes(mfilPath, FileAttributes.ReadOnly);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Failed to locate MFIL info for {gameVersion}. Skipping MFIL creation.");
                        Console.ResetColor();
                    }
                }
            }
                /*
            Dictionary<string, string> x64files = new Dictionary<string, string>()
            {
                { "5ACD2205377352083D2D98B89F48B602", "Wow-64.exe" },
                { "5CA22973EDF3D10F9C69297A1EB28058", "Battle.net-64.dll" },
                { "37EC741FCBDEEBD01F90D9877D872EA1", "MovieProxy.exe" }
            };

            string mfilHash = "F8E7D7BA6CDE053B1A9F85BD36980A72";


            List<string> x64FilesToDownload = new List<string>();

            foreach (var x64file in x64files)
            {
                string md5Hash = "";
                // check if file is valid
                if (File.Exists(x64file.Value))
                {
                    using (var stream = File.OpenRead(x64file.Value))
                    {
                        md5Hash = Convert.ToHexString(MD5.HashData(stream));
                    }

                    if (x64file.Key.Equals(md5Hash))
                    {
                        Console.WriteLine($"{x64file.Value} already exists. Skip.");
                        Console.WriteLine();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Existing {x64file.Value} is corrupted!");
                        Console.WriteLine($"Renamed corrupted file {x64file.Value} to {Path.GetFileNameWithoutExtension(x64file.Value)}_BACKUP{Path.GetExtension(x64file.Value)}");
                        Console.WriteLine();
                        File.Move(x64file.Value, Path.GetFileNameWithoutExtension(x64file.Value) + "_BACKUP" + Path.GetExtension(x64file.Value));
                    }
                }

                x64FilesToDownload.Add(x64file.Value);
            }

            if (x64FilesToDownload.Count > 0)
            {
                Console.WriteLine($"Downloading: WoWLive-64-Win-15595.zip ...");
                using (MemoryStream x64Zip = new MemoryStream(webClient.GetByteArrayAsync("http://eu.media.battle.net.edgesuite.net/downloads/wow-installers/live/WoWLive-64-Win-15595.zip").Result))
                {
                    using (ZipArchive archive = new ZipArchive(x64Zip, ZipArchiveMode.Read))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {

                            if (x64FilesToDownload.Contains(entry.FullName))
                            {
                                Console.WriteLine($"Extracting: {entry.FullName} ...");
                                entry.ExtractToFile(entry.FullName);
                            }
                        }
                    }
                }
            }
            */
        }
    }
}
