using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuranXML
{
    public class ArrangeSurah : SurahBase
    {
        public Ayat[] ScrambledAyaat { get; set; }

        public Ayat[] Ayaat { get; set; }

        public void Scramble(Random random)
        {
            ScrambledAyaat = Ayaat.OrderBy(x => random.Next()).ToArray();
        }

        public ArrangeSurah(int index, string en_name, string ar_name, bool bismillah, string juz)
        {
            Index = index;
            EN_Name = en_name;
            AR_Name = ar_name;
            Bismillah = bismillah;
            Juz = juz;
        }
    }
}
