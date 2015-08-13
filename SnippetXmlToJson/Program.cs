using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace SnippetXmlToJson
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() != 1)
            {
                Console.WriteLine("set xml snippet file path as first parameter.");
                return;
            }
            var path = args[0];

            var xmlToJson = new SnippetXmlToJson(path);
            var json = xmlToJson.Convert();
            
            new FileWriter(path).WriteJson(json);
        }
    }

    public class FileWriter
    {
        public string Path { get; }
        const string JsonSuffix = ".json";

        public FileWriter(string path)
        {
            if (path == null) throw new ArgumentNullException($"parameter{nameof(path)} is null.");
            Path = path;
        }

        public void WriteJson(string json)
        {
            File.WriteAllText(Path + JsonSuffix, json);
        }
    }

    public class SnippetXmlToJson
    {
        readonly XDocument _xml;
        readonly XNamespace _nameSpace;

        public SnippetXmlToJson(string path)
        {
            if (path == null) throw new ArgumentNullException($"parameter{nameof(path)} is null.");
            if (!File.Exists(path)) throw new ArgumentException($"parameter{nameof(path)} is invalid.");

            _xml = XDocument.Load(path);
            _nameSpace = _xml.Root.GetDefaultNamespace();
        }

        public string Convert()
        {
            var header = _xml.Descendants(_nameSpace + "Header").First();

            var snippets = _xml.Descendants(_nameSpace + "Snippet");
            var objectsForJson = snippets.Select(snippet =>
            {
                var variables = snippet.Element(_nameSpace + "Declarations")
                    .Elements()
                    .Select(x => new {
                        Id = x.Element(_nameSpace + "ID").Value,
                        Label = x.Element(_nameSpace + "Default").Value,
                    });

                var rawBody = snippet.Element(_nameSpace + "Code")
                    .Value;

                var replacedBody = variables.Aggregate(rawBody, (acc, x) =>
                    acc.Replace($"${x.Id}$", $"${{{x.Id}:{x.Label}}}"));
                return new Dictionary<string, object>
                {
                    {
                        header.Element(_nameSpace + "Title").Value,
                        new
                        {
                            prefix = header.Element(_nameSpace + "Shortcut").Value,
                            body = replacedBody,
                            description = header.Element(_nameSpace + "Description").Value
                        }
                    }
                };
            });

            // including one Snippet element
            return JsonConvert.SerializeObject(objectsForJson.Single());
        }
    }
}
