using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace QuranCore
{
    public class Convert
    {
        //Convert from XML file to normal quran
        public static Quran FromOriginalXML(string xmlpath, Properties toSet)
        {
            Quran XMLQuran;
            //Derserialize
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Quran));
            using (System.IO.FileStream stream = new System.IO.FileStream(xmlpath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                XMLQuran = (Quran)serializer.Deserialize(stream);
            }
            //Now return the XMLQuran
            return XMLQuran;
        }

        public static Quran FromCustomXML(string xmlpath, QuranType type)
        {
            SerializableQuran serializable;
            //Derserialize
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SerializableQuran));
            using (System.IO.FileStream stream = new System.IO.FileStream(xmlpath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                serializable = (SerializableQuran)serializer.Deserialize(stream);
            }
            return serializable.ConvertToQuran(type);
        }

        public static void WriteQuranXML(Quran quran, string path)
        {
            SerializableQuran serializable = quran.ConvertToSerializable();
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SerializableQuran));
            using (System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                serializer.Serialize(stream, serializable);
            }
        }


        public static void WritePropertiesXML(Properties properties, string path)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Properties));
            using (System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                serializer.Serialize(stream, properties);
            }
        }

        public static Properties GetPropertiesFromXML(string xmlpath)
        {
            Properties properties;
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Properties));
            using (System.IO.FileStream stream = new System.IO.FileStream(xmlpath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                properties = (Properties)serializer.Deserialize(stream);
            }
            return properties;
        }

        
        public static void AddProperties(Properties toSet, ref Quran quran)
        {
            foreach (Property p in toSet.List)
            {
                object toChange = null;
                if (p.Type == PropertyType.Surah)
                {
                    if (quran.Suwar.ContainsKey(p.Index))
                    {
                        toChange = quran.Suwar[p.Index];
                    } else
                    {

                        Surah s = null;
                        if (quran.Type == QuranType.Concurrent)
                        {
                            s = new ConcurrentSurah();
                        }
                        else
                        {
                            s = new Surah();
                        }
                        quran.Suwar[p.Index] = s;
                        toChange = s;
                    }
                    

                }
                else if (p.Type == PropertyType.Ayat)
                {
                    if (quran.Suwar.ContainsKey(p.Index))
                    {
                        Surah s = quran.Suwar[p.Index];
                        if (s.Ayaat.ContainsKey(p.AyatIndex))
                        {
                            toChange = s.Ayaat[p.AyatIndex];
                        } else
                        {
                            Ayat a = new Ayat();
                            s.Ayaat[p.AyatIndex] = a;
                            toChange = a;
                        }
                    }
                    else
                    {
                        Surah s = null;
                        if (quran.Type == QuranType.Concurrent)
                        {
                            s = new ConcurrentSurah();
                        }
                        else
                        {
                            s = new Surah();
                        }
                        Ayat a = new Ayat();
                        s.Ayaat[p.AyatIndex] = a; 
                        quran.Suwar[p.Index] = s;
                        toChange = a;
                    }
                }
                else if (p.Type == PropertyType.Quran)
                {
                    toChange = quran;
                }
                if (toChange != null)
                {
                    PropertyInfo info = toChange.GetType().GetProperty(p.Name, BindingFlags.Public | BindingFlags.Instance);
                    if (null != info && info.CanWrite)
                    {
                        info.SetValue(toChange, p.Value);
                    }
                }

            }

        }
        
    }
}
