using System;
using System.Collections.Generic;
using System.Text;

namespace QuranXML
{
    public class Quiz
    {
        public int Index { get; set; }

        public string EN_Name { get; set; }

        public QuizQuestion[] Questions { get; set; }

        public Quiz()
        {

        }

        public Quiz(int index, string en_name)
        {
            Index = index;
            EN_Name = en_name;
        }
    }
}
