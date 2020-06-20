using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace QuranCore
{
    
    public class Quran
    {
        
        public IDictionary<int, Surah> Suwar { get; set; }

        
        public QuranType Type { get; set; }

        public Quran()
        {
            Type = QuranType.Normal;
            Suwar = new Dictionary<int, Surah>();
        }

        public SerializableQuran ConvertToSerializable()
        {
            SerializableQuran quran = new SerializableQuran();
            quran.Suwar = new SerializableSurah[Suwar.Count];

            IOrderedEnumerable<Surah> suwar = Suwar.Values.OrderBy(x => x.Index);
            int i = 0;
            foreach (Surah surah in suwar)
            {
                quran.Suwar[i] = surah.ConvertToSerializable();
                i++;
            }
            return quran;
        }
    }

    public class ConcurrentQuran : Quran
    {
        public ConcurrentQuran()
        {
            Suwar = new System.Collections.Concurrent.ConcurrentDictionary<int, Surah>();
            Type = QuranType.Concurrent;
        }
    }

    
    public enum QuranType
    {
        Normal,
        Concurrent
    }
}
