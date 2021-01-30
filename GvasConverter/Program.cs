using System;
using System.IO;
using System.Text;
using GvasFormat;
using GvasFormat.Serialization;
using Newtonsoft.Json;

namespace GvasConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine();
                Console.WriteLine("Usage:");
                Console.WriteLine();
                Console.WriteLine("  gvas-converter [path_to_save_file]");
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Parsing UE4 save file structure...");
            Gvas save;

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Error: File not found");
                return;
            }

            using (var stream = File.Open(args[0], FileMode.Open, FileAccess.Read, FileShare.Read))
                    save = UESerializer.Read(stream);

            Console.WriteLine("Converting to JSON...");
            var json = JsonConvert.SerializeObject(save, new JsonSerializerSettings{Formatting = Formatting.Indented});

            Console.WriteLine("Saving JSON...");
            using (var stream = File.Open(args[0] + ".json", FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream, new UTF8Encoding(false)))
                writer.Write(json);

            Console.WriteLine("Done");
            Console.ReadKey(true);
        }
    }
}
