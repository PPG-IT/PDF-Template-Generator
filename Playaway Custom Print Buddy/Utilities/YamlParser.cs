using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace Playaway_Custom_Print_Buddy.Utilities
{
    public class YamlParser
    {
        public static Dictionary<string, object> ParseFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new Dictionary<string, object>();

            try
            {
                var deserializer = new DeserializerBuilder().Build();
                var yamlContent = File.ReadAllText(filePath);
                return deserializer.Deserialize<Dictionary<string, object>>(yamlContent) ?? new Dictionary<string, object>();
            }
            catch (Exception)
            {
                return new Dictionary<string, object>();
            }
        }

        public static void WriteFile(string filePath, Dictionary<string, object> data)
        {
            try
            {
                var serializer = new SerializerBuilder().Build();
                var yaml = serializer.Serialize(data);
                File.WriteAllText(filePath, yaml);
            }
            catch (Exception)
            {
                // Ignore write errors
            }
        }

        public static T ParseFile<T>(string filePath) where T : new()
        {
            if (!File.Exists(filePath))
                return new T();

            try
            {
                var deserializer = new DeserializerBuilder().Build();
                var yamlContent = File.ReadAllText(filePath);
                return deserializer.Deserialize<T>(yamlContent);
            }
            catch (Exception)
            {
                return new T();
            }
        }
    }
}