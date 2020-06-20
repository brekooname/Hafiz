﻿using System;

namespace QuranCore
{
    
    public class Ayat
    {
        [System.Xml.Serialization.XmlAttribute]
        public int Index { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public string Text { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public bool Repeated { get; set; }

        public Ayat()
        {

        }

        public Ayat(int index, string text)
        {
            Index = index;
            Text = text;
        }

        public Ayat Clone()
        {
            return new Ayat(Index, Text);
        }
        
    }
}
