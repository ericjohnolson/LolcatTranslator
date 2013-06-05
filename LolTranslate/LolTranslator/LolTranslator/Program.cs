using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YamlDotNet;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;


namespace LolTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument original = XDocument.Load("preston_session.xml");
            Dictionary<string, string> lolcatDictionary = new Dictionary<string, string>();
            StreamReader sr = new StreamReader("tranzlator.yml");
            var yaml = new YamlStream();
            yaml.Load(sr);
            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            foreach (var entry in mapping.Children)
            {
                lolcatDictionary.Add(((YamlScalarNode)entry.Key).Value, ((YamlScalarNode)entry.Value).Value);
            }

            foreach (var item in original.Descendants("WRD"))
                if(lolcatDictionary.ContainsKey(item.Attribute("SRT").Value))
                    item.Attribute("SRT").Value = lolcatDictionary[item.Attribute("SRT").Value];

            original.Save("preston_lol.xml");
        }
    }
}
