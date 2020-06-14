using System;
using System.Collections.Generic;
using System.Text;

namespace QuranXML
{
    public class XMLAyat : Ayat
    {
        [System.Xml.Serialization.XmlAttribute]
        public string bismillah { get; set; }
    }
}
