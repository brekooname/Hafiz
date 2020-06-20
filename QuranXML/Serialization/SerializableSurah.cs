using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuranCore
{
    public class SerializableSurah : SurahBase
    {
        [System.Xml.Serialization.XmlElement("Ayat")]
        public Ayat[] Ayaat { get; set; }

        public SerializableSurah()
        {

        }

        public SerializableSurah(int index, string en_name, string ar_name, bool bismillah, string juz)
        {
            Index = index;
            EN_Name = en_name;
            AR_Name = ar_name;
            Bismillah = bismillah;
            Juz = juz;
        }
        
        public Surah ConvertToSurah(QuranType type)
        {
            Surah s = new Surah(Index, EN_Name, AR_Name, Bismillah, Juz);
            if (type == QuranType.Concurrent)
            {
                s.Ayaat = new ConcurrentDictionary<int, Ayat>();
            } else
            {
                s.Ayaat = new Dictionary<int, Ayat>();
            }

            foreach (Ayat ayat in Ayaat)
            {
                s.Ayaat[ayat.Index] = ayat;
            }

            return s;
        }
    }
}
