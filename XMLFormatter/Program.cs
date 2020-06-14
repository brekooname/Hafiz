using QuranXML;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XMLFormatter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting!");

            QuranXML.Properties p = await GetEnglishNamesProperties("surah_english_names.txt");
            QuranXML.Properties arText = await GetArabicTextAsync("quran-uthmani.txt");
            QuranXML.Properties juz = await GetJuzAsync("list-juz.txt");

            QuranXML.Quran q = new QuranXML.Quran();
            QuranXML.Convert.AddProperties(p, ref q);
            QuranXML.Convert.AddProperties(arText, ref q);
            QuranXML.Convert.AddProperties(juz, ref q);
            QuranXML.Convert.WriteQuranXML(q, @"C:\Users\Abdullah Yousuf\Documents\Hafiz\Hafiz\custom-quran.xml");

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static async Task<QuranXML.Properties> GetEnglishNamesProperties(string path) {
            QuranXML.Properties properties = new QuranXML.Properties();

            string[] lines = await System.IO.File.ReadAllLinesAsync(path);

            for (int i=0; i<lines.Length; i++)
            {
                QuranXML.Property index = new QuranXML.Property();
                index.Name = "Index";
                index.Index = i+1;
                index.Type = PropertyType.Surah;
                index.Value = i+1;
                properties.List.Add(index);

                QuranXML.Property enName = new QuranXML.Property();
                enName.Name = "EN_Name";
                enName.Index = i+1;
                enName.Type = PropertyType.Surah;
                enName.Value = lines[i];
                properties.List.Add(enName);
                
            }

            
            return properties;
        }

        static async Task<Properties> GetArabicTextAsync(string path)
        {
            QuranXML.Properties properties = new Properties();

            string[] lines = await System.IO.File.ReadAllLinesAsync(path);

            foreach (string line in lines)
            {
                if (line.Contains('|'))
                {
                    //Split line into components
                    string[] components = line.Split('|');

                    int surah = Int32.Parse(components[0]);

                    int ayat = Int32.Parse(components[1]);

                    //Set index property
                    QuranXML.Property indexProp = new Property();
                    indexProp.Type = PropertyType.Ayat;
                    indexProp.Index = surah;
                    indexProp.Name = "Index";
                    indexProp.AyatIndex = ayat;
                    indexProp.Value = ayat;
                    properties.List.Add(indexProp);

                    //Set the text property
                    QuranXML.Property p = new QuranXML.Property();
                    p.Type = PropertyType.Ayat;
                    p.Index = surah;
                    p.Name = "Text";
                    p.AyatIndex = ayat;
                    p.Value = components[2];
                    properties.List.Add(p);

                    

                } else
                {
                    //Add line to previous property
                    Property p = properties.List[properties.List.Count - 1];
                    p.Value = String.Concat((string)p.Value, line);
                }
            }
            return properties;
        }

        static async Task<Properties> GetJuzAsync(string path)
        {
            string[] lines = await System.IO.File.ReadAllLinesAsync(path);

            Properties properties = new Properties();
            for (int i=0; i<lines.Length; i++)
            {
                Property p = new Property();
                p.Name = "Juz";
                p.Index = i + 1;
                p.Type = PropertyType.Surah;
                p.Value = lines[i];
                properties.List.Add(p);
            }

            return properties;
        }

    }
}
