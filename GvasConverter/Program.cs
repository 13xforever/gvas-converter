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
                Console.WriteLine("Usage:");
                Console.WriteLine("  gvas-converter path_to_save_file|path_to_json");
                return;
            }

            var ext = Path.GetExtension(args[0]).ToLower();
            if (ext == ".json")
            {
                Console.WriteLine("Not implemented atm");
            }
            else
            {
                Console.WriteLine("Parsing UE4 save file structure...");
                Gvas save;
                using (var stream = File.Open(args[0], FileMode.Open, FileAccess.Read, FileShare.Read))
                    save = UESerializer.Read(stream);

                Console.WriteLine("Converting to json...");
                var json = JsonConvert.SerializeObject(save, new JsonSerializerSettings{Formatting = Formatting.Indented});

                Console.WriteLine("Saving json...");
                using (var stream = File.Open(args[0] + ".json", FileMode.Create, FileAccess.Write, FileShare.Read))
                using (var writer = new StreamWriter(stream, new UTF8Encoding(false)))
                    writer.Write(json);
            }
            Console.WriteLine("Done.");
            Console.ReadKey(true);
        }
    }
}
