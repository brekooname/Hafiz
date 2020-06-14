using System;


namespace QuranXML
{
    public abstract class SurahBase
    {
        [System.Xml.Serialization.XmlAttribute]
        public int Index { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public string EN_Name { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public string AR_Name { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public bool Bismillah { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public string Juz { get; set; }
    }
}
