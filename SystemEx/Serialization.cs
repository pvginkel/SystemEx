using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace SystemEx
{
    public static class Serialization
    {
        public static string SerializeXml<T>(T obj)
        {
            return SerializeXml<T>(obj, null);
        }

        public static string SerializeXml<T>(T obj, bool indent)
        {
            return SerializeXml<T>(obj, null, indent);
        }

        public static string SerializeXml<T>(T obj, string defaultNamespace)
        {
            return SerializeXml<T>(obj, defaultNamespace, false);
        }

        public static string SerializeXml<T>(T obj, string defaultNamespace, bool indent)
        {
            var settings = new XmlWriterSettings();

            settings.OmitXmlDeclaration = true;

            if (indent)
            {
                settings.Indent = true;
                settings.IndentChars = "  ";
            }

            using (var writer = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(writer, settings))
            {
                GetSerializer<T>(defaultNamespace).Serialize(xmlWriter, obj);

                return writer.ToString();
            }
        }

        private static XmlSerializer GetSerializer<T>(string defaultNamespace)
        {
            if (String.IsNullOrEmpty(defaultNamespace))
            {
                return new XmlSerializer(typeof(T));
            }
            else
            {
                return new XmlSerializer(typeof(T), defaultNamespace);
            }
        }

        public static T DeserializeXml<T>(string xml)
        {
            return DeserializeXml<T>(xml, null);
        }

        public static T DeserializeXml<T>(string xml, string defaultNamespace)
        {
            using (var reader = new StringReader(xml))
            {
                return (T)GetSerializer<T>(defaultNamespace).Deserialize(reader);
            }
        }
    }
}
