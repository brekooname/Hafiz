using QuranCore;
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

            QuranCore.Properties p = await GetEnglishNamesProperties("surah_english_names.txt");
            QuranCore.Properties arText = await GetArabicTextAsync("quran-uthmani.txt");
            QuranCore.Properties juz = await GetJuzAsync("list-juz.txt");

            QuranCore.Quran q = new QuranCore.Quran();
            QuranCore.Convert.AddProperties(p, ref q);
            QuranCore.Convert.AddProperties(arText, ref q);
            QuranCore.Convert.AddProperties(juz, ref q);
            QuranCore.Convert.WriteQuranXML(q, @"C:\Users\Abdullah Yousuf\Documents\Hafiz\Hafiz\custom-quran.xml");

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static async Task<QuranCore.Properties> GetEnglishNamesProperties(string path) {
            QuranCore.Properties properties = new QuranCore.Properties();

            string[] lines = await System.IO.File.ReadAllLinesAsync(path);

            for (int i=0; i<lines.Length; i++)
            {
                QuranCore.Property index = new QuranCore.Property();
                index.Name = "Index";
                index.Index = i+1;
                index.Type = PropertyType.Surah;
                index.Value = i+1;
                properties.List.Add(index);

                QuranCore.Property enName = new QuranCore.Property();
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
            QuranCore.Properties properties = new Properties();

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
                    QuranCore.Property indexProp = new Property();
                    indexProp.Type = PropertyType.Ayat;
                    indexProp.Index = surah;
                    indexProp.Name = "Index";
                    indexProp.AyatIndex = ayat;
                    indexProp.Value = ayat;
                    properties.List.Add(indexProp);

                    //Set the text property
                    QuranCore.Property p = new QuranCore.Property();
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
