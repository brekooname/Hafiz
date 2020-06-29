using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuranCore
{
    public class QuizQuestion
    {
        public string Text { get; set; }

        public string[] Choices { get; set; }

        public int CorrectChoice { get; set; }

        public int TextAyat { get; set; }

        public QuizQuestion()
        {

        }
    }
}
