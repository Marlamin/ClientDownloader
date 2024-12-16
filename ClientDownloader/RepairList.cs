using System;
using System.Collections.Generic;
using System.IO;

namespace ClientDownloader
{
    public class RepairList
    {
        public Dictionary<string, (FileFlags flags, string fileHash)> Entries = new();

        public RepairList(string file)
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
                    if (magic != 0x54534C52) // "RLST"
                        throw new InvalidDataException("Invalid repair list file");

                    var unk1 = reader.ReadUInt16();
                    if (unk1 != 2 && unk1 != 3)
                        throw new InvalidDataException("Invalid unk1 value: " + unk1);

                    var unk2 = reader.ReadByte();
                    if(unk2 != 0x18)
                        throw new InvalidDataException("Invalid unk2 value: " + unk2);

                    var recordSize = reader.ReadByte();
                    if (recordSize != 28 && recordSize != 40)
                        throw new InvalidDataException("Invalid recordSize value: " + unk2);

                    //Console.WriteLine("unk1 " + unk1);
                    //Console.WriteLine("unk2 " + unk2);

                    var numFiles = reader.ReadUInt32();

                    var unk4 = reader.ReadUInt32();
                    if (unk4 != 1)
                        throw new InvalidDataException("Invalid unk4 value: " + unk4);

                    var fileUnk0 = reader.ReadUInt32();
                    var fileUnk1 = reader.ReadUInt32();
                    if (fileUnk1 != 0)
                        throw new InvalidDataException("Invalid fileUnk1 value: " + fileUnk1);

                    //Console.WriteLine("fileunk0 " + fileUnk0);

                    for (var i = 0; i < numFiles; i++)
                    {
                        var fileMD5 = reader.ReadBytes(16);
                        var fileNameOffset = reader.ReadUInt32();
                        var prevPos = fs.Position;
                        fs.Position = fileNameOffset;
                        var fileName = reader.ReadCString();
                        //Console.WriteLine(fileName + " @ " + fileNameOffset);
                        fs.Position = prevPos;

                        var flags = (FileFlags)reader.ReadUInt16();
                        //Console.WriteLine("flags "+ flags.ToString());

                        Entries.Add(fileName, (flags, Convert.ToHexString(fileMD5)));

                        var fileUnk2_2 = reader.ReadUInt16(); // maybe also flags or gets moved up by something before it (when recordSize 40?)
                        if(fileUnk2_2 != 0 && fileUnk2_2 != 1 && fileUnk2_2 != 2 && fileUnk2_2 != 16 && fileUnk2_2 != 64 && fileUnk2_2 != 128 && fileUnk2_2 != 8192)
                            throw new InvalidDataException("Invalid fileUnk2_2 value: " + fileUnk2_2);

                        var fileUnk3 = reader.ReadUInt32(); // always 0?
                        if (fileUnk3 != 0)
                            throw new InvalidDataException("Invalid fileUnk3 value: " + fileUnk3);

                        if(recordSize == 40)
                        {
                            var fileUnk4 = reader.ReadUInt32();
                            var fileUnk5 = reader.ReadUInt32();
                            var fileUnk6 = reader.ReadUInt32();

                            //Console.WriteLine("fileunk4 " + fileUnk4);
                            //Console.WriteLine("fileunk5 " + fileUnk5);
                            //Console.WriteLine("fileunk6 " + fileUnk6);
                        }
                    }
                }
            }
        }

        [Flags]
        public enum FileFlags : uint
        {
            RepackMPQ = 0x1,
            Unk2 = 0x2,
            Unk3 = 0x4,
            Unk4 = 0x8,
            Unk5 = 0x10,
            Unk6 = 0x20,
            Unk7 = 0x40,
            Common = 0x80,
            enUS = 0x100,
            enGB = 0x200,
            esES = 0x400,
            esMX = 0x800,
            deDE = 0x1000,
            frFR = 0x2000,
            koKR = 0x4000,
            Unk16 = 0x8000,
            Unk17 = 0x10000,
            Unk18 = 0x20000,
            Unk19 = 0x40000,
            Unk20 = 0x80000,
            Unk21 = 0x100000,
            Unk22 = 0x200000,
            Unk23 = 0x400000,
            Unk24 = 0x800000,
            Unk25 = 0x1000000,
            Unk26 = 0x2000000,
            Unk27 = 0x4000000,
            Unk28 = 0x8000000,
            Unk29 = 0x10000000,
            Unk30 = 0x20000000,
            Unk31 = 0x40000000,
            Unk32 = 0x80000000
        }
    }
}
