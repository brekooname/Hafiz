using System;
using System.Collections.Generic;

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

        [System.Xml.Serialization.XmlAttribute]
        public string TextWODiacritics { get; set; }

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

        public void MakeFillInTheBlankQestion(ref QuizQuestion quizQuestion, ref Random random)
        {
            quizQuestion.TextAyat = Index;
            quizQuestion.Choices = new string[1];

            int pos = TextWODiacritics.IndexOf(' ');
            if (pos < 0)
            {
                quizQuestion.Text = "______";
                quizQuestion.Choices[0] = (string)TextWODiacritics.Clone();
                return;
            }

            List<int> spacesIndices = new List<int>();
            if (pos > 0)
            {
                spacesIndices.Add(-1);
            }
            //spacesIndices.Add(pos);

            for (; pos > -1; pos = TextWODiacritics.IndexOf(' ', pos + 1))
            {
                if (pos < TextWODiacritics.Length - 2)
                {
                    spacesIndices.Add(pos);
                }
                
            }

            //spacesIndices.RemoveAt(spacesIndices.Count - 1);

            //Find a random space
            int index = random.Next(0, spacesIndices.Count);
            int endpoint = index + 1;

            if (endpoint >= spacesIndices.Count)
            {
                endpoint = TextWODiacritics.Length;
            } else
            {
                endpoint = spacesIndices[endpoint];
            }
            index = spacesIndices[index] + 1;

            quizQuestion.Choices[0] = TextWODiacritics.Substring(index, endpoint - index);
            char[] letters = TextWODiacritics.ToCharArray();
            char[] finalLetters = new char[3 + letters.Length];
            finalLetters[0] = '\u200F';
            for (int j=0; j<index; j++)
            {
                finalLetters[j + 1] = letters[j];
            }
            finalLetters[index+1] = '\u200F';
            for (int j = index; j < endpoint; j++)
            {
                finalLetters[j + 2] = '_';
            }
            finalLetters[endpoint + 2] = '\u200F';
            for (int j = endpoint; j < letters.Length; j++)
            {
                finalLetters[j + 3] = letters[j];
            }
            quizQuestion.Text = new string(finalLetters);
            

        }
    }
}
