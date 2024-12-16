using System.Collections.Generic;
using System.IO;

namespace ClientDownloader
{
    public class RepairCatalog
    {
        public Dictionary<string, string> TextPairs = new();
        public Dictionary<string, Dictionary<string, string>> Entries = new();

        public RepairCatalog(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("File not found", file);
            }

            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fs))
                {
                    var magic = reader.ReadUInt32();
                    if (magic != 0x54414352) // "RCAT"
                    {
                        throw new InvalidDataException("Invalid repair catalog file");
                    }

                    var unk1 = reader.ReadUInt16();
                    if (unk1 != 1)
                        throw new InvalidDataException("Invalid unk1 value: " + unk1);

                    var unk2 = reader.ReadUInt16();
                    if (unk2 != 1)
                        throw new InvalidDataException("Invalid unk2 value: " + unk2);

                    var unk3 = reader.ReadUInt16();
                    if (unk3 != 0)
                        throw new InvalidDataException("Invalid unk3 value: " + unk3);

                    var unk4 = reader.ReadUInt16();
                    if (unk4 != 12)
                        throw new InvalidDataException("Invalid unk4 value: " + unk4);

                    var numTextKeys = reader.ReadUInt32();
                    if (numTextKeys != 2)
                        throw new InvalidDataException("Invalid numTextKeys value: " + numTextKeys);

                    long prevPos = 0;

                    for (var i = 0; i < numTextKeys; i++)
                    {
                        var descKeyOffset = reader.ReadUInt32();
                        var descValueOffset = reader.ReadUInt32();
                        prevPos = fs.Position;

                        fs.Seek(descKeyOffset, SeekOrigin.Begin);
                        var descKey = reader.ReadCString();
                        fs.Seek(descValueOffset, SeekOrigin.Begin);
                        var descValue = reader.ReadCString();

                        TextPairs.Add(descKey, descValue);

                        fs.Seek(prevPos, SeekOrigin.Begin);
                    }

                    prevPos = fs.Position;

                    fs.Seek(prevPos, SeekOrigin.Begin);

                    var osCount = reader.ReadUInt32();
                    if (osCount != 2)
                        throw new InvalidDataException("Invalid osCount value: " + osCount);

                    for (var i = 0; i < osCount; i++)
                    {
                        var osOffset = reader.ReadUInt32();
                        prevPos = fs.Position;
                        fs.Seek(osOffset, SeekOrigin.Begin);
                        var osValue = reader.ReadCString();

                        fs.Seek(prevPos, SeekOrigin.Begin);

                        Entries.Add(osValue, new Dictionary<string, string>());

                        var osLocales = reader.ReadUInt32(); // numLocales?
                        if (osLocales != 8 && osLocales != 11 & osLocales != 13)
                            throw new InvalidDataException("Invalid osLocales value: " + osLocales);

                        var osLocaleOffsetKeys = new uint[osLocales];
                        var osLocaleOffsetValues = new uint[osLocales];
                        for (var j = 0; j < osLocales; j++)
                        {
                            osLocaleOffsetKeys[j] = reader.ReadUInt32();
                            osLocaleOffsetValues[j] = reader.ReadUInt32();
                        }

                        prevPos = fs.Position;

                        for (var j = 0; j < osLocales; j++)
                        {
                            fs.Seek(osLocaleOffsetKeys[j], SeekOrigin.Begin);
                            var localeKey = reader.ReadCString();
                            fs.Seek(osLocaleOffsetValues[j], SeekOrigin.Begin);
                            var localeValue = reader.ReadCString();

                            Entries[osValue].Add(localeKey, localeValue);
                        }

                        fs.Seek(prevPos, SeekOrigin.Begin);
                    }
                }
            }
        }
    }
}
