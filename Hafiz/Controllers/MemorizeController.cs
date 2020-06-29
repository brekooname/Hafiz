using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hafiz.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hafiz.Controllers
{
    public class MemorizeController : Controller
    {
        
        private Random RandomGen;

        public static QuranCore.ConcurrentQuran AlQuran;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult arrangeayat(int surah, int ayat)
        {
            return View();
        }

        public IActionResult ArrangeSurah(int surah)
        {
            if (surah < 1 || surah > 114)
            {
                return StatusCode(404);
            }
            QuranCore.ArrangeSurah model = AlQuran.Suwar[surah].ConvertToArrange();
            model.Scramble(RandomGen);
            return View(model);
        }

        public IActionResult ArrangeSurahRange(int surah, int begin, int end)
        {
            if (surah < 1 || surah > 114)
            {
                return StatusCode(404);
            }

            QuranCore.ArrangeSurah model = AlQuran.Suwar[surah].ConvertToArrange(begin, end);
            if (model == null) return StatusCode(404);
            model.Scramble(RandomGen);
            return View("ArrangeSurah", model);
        }

        public IActionResult Read(int surah)
        {
            if (surah < 1 || surah > 114)
            {
                return StatusCode(404);
            }
            QuranCore.ArrangeSurah model = AlQuran.Suwar[surah].ConvertToArrange();
            return View(model);
        }

        public IActionResult Quiz(int surah)
        {
            if (surah < 1 || surah > 114)
            {
                return StatusCode(404);
            }
            QuranCore.Quiz model = AlQuran.Suwar[surah].GenerateQuiz(RandomGen);

            return View(model);
        }

        public IActionResult FillBlanks(int surah)
        {
            if (surah < 1 || surah > 114)
            {
                return StatusCode(404);
            }
            QuranCore.Quiz model = AlQuran.Suwar[surah].GenerateFillInTheBlanksQuiz(ref RandomGen);
            return View(model);
        }

        public MemorizeController()
        {
            RandomGen = new Random();
        }

        public static void LoadQuran()
        {
            AlQuran = (QuranCore.ConcurrentQuran)QuranCore.Convert.FromCustomXML("custom-quran.xml", QuranCore.QuranType.Concurrent);
        }
    }
}