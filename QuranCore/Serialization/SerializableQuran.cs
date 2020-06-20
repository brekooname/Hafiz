using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace QuranCore
{
    [XmlRoot("Quran")]
    public class SerializableQuran
    {
        [XmlElement("Surah")]
        public SerializableSurah[] Suwar { get; set; }

        public SerializableQuran()
        {

        }

        public Quran ConvertToQuran(QuranType type)
        {
            Quran quran = null;
            if (type == QuranType.Concurrent)
            {
                quran = new ConcurrentQuran();
            } else
            {
                quran = new Quran();
            }

            foreach (SerializableSurah surah in Suwar)
            {
                quran.Suwar[surah.Index] = surah.ConvertToSurah(type);
            }
            return quran;
        }
    }
}
