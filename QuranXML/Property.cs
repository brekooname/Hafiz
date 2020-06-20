using System;
using System.Collections.Generic;
using System.Text;

namespace QuranCore
{
    public class Property
    {
        [System.Xml.Serialization.XmlAttribute]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public int Index { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public object Value { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public PropertyType Type { get; set; } 

        [System.Xml.Serialization.XmlAttribute]
        public int AyatIndex { get; set; }
    }

    [System.Xml.Serialization.XmlRoot]
    public class Properties
    {
        [System.Xml.Serialization.XmlElement]
        public List<Property> List { get; set; }

        public Properties()
        {
            List = new List<Property>();
        }
    }

    public enum PropertyType : byte
    {
        Surah,
        Ayat,
        Quran
    }
}
