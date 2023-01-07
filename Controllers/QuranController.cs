using AsthaIslamicService.CacheManager;
using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Controllers
{
    public class QuranController : Controller
    {
        private readonly ISurahService surahService;
        private readonly IQuranCacheManager quranCacheManager;

        public QuranController(ISurahService surahService, IQuranCacheManager quranCacheManager)
        {
            this.surahService = surahService;
            this.quranCacheManager = quranCacheManager;
        }

        public async Task<IActionResult> Index()
        {
            //List<Sura> aList = await _suraService.GetAll();
            //ViewBag.SuraList = aList;
            return View();
        }


        public async Task<IActionResult> DetailsOperation(int id, string op, string play)
        {

            List<SurahModel_Quran> aList = await quranCacheManager.GetCachedSura();

            if (op == "next")
            {
                if (id == aList.Count)
                {
                    return RedirectToAction("Details", new { id = id.ToString(), name = aList[aList.Count - 1].SuraName, num = aList[aList.Count - 1].AyatNo, play = play });
                }
                else
                {
                    return RedirectToAction("Details", new { id = (id + 1).ToString(), name = aList[id].SuraName, num = aList[id].AyatNo, play = play });
                }
            }
            if (op == "prev")
            {
                if (id == 1)
                {
                    return RedirectToAction("Details", new { id = id.ToString(), name = aList[0].SuraName, num = aList[0].AyatNo, play = play });
                }
                else
                {


                    return RedirectToAction("Details", new { id = (id - 1).ToString(), name = aList[(id - 2)].SuraName, num = aList[(id - 2)].AyatNo, play = play });
                }

            }
            return View();
        }
        public async Task<IActionResult> Details(string id, string name, string num, string play)
        {
            //List<Sura> aList = new List<Sura>();
            //Sura sura = new Sura(); 
            //sura.SuraNo = id;
            //sura.SuraName = name; 
            //sura.AyatNo = num;
            //ViewBag.suraDetail = sura;
            //ViewBag.title = name;
            //aList = await _suraService.GetById(id);
            //ViewBag.SuraList = aList;
            //ViewBag.play = play;
            ViewBag.id = id;
            ViewBag.name = name;
            ViewBag.num = num;
            ViewBag.play = play;
            return View();
        }

        public async Task<PartialViewResult> GetSuraList()
        {
            List<SurahModel_Quran> aList = await quranCacheManager.GetCachedSura();
            ViewBag.SuraList = aList;
            return PartialView("_quranPartial");
        }

        public IActionResult Test()
        {
            return View("index");
        }

        public async Task<PartialViewResult> SuraDetailData(string id, string name, string num, string play)
        {
            List<SurahModel_Quran> aList = new List<SurahModel_Quran>();
            SurahModel_Quran sura = new SurahModel_Quran();
            sura.SuraNo = id;
            sura.SuraName = name;
            sura.AyatNo = num;
            ViewBag.suraDetail = sura;
            ViewBag.title = name;
            aList = await surahService.GetById(id);
            ViewBag.SuraList = aList;
            ViewBag.play = play;
            return PartialView("_suraDetailPartialView");
        }
        //public async Task<PartialViewResult> GetSuraPartialView()
        //{
        //    var data = await quranCacheManager.GetCachedSura();
        //    return PartialView("surahPartialView", data);
        //}


        //public async Task<IActionResult> Detail(string id)
        //{

        //    var data = await quranService.GetSurah(id);
        //    return View(data);
        //}

        //public async Task<IActionResult> Next(string id)
        //{
        //    var idSplit = id.Split('-');
        //    if (!string.IsNullOrEmpty(idSplit[0]))
        //    {
        //        int iid = Convert.ToInt32(idSplit[0]);
        //        if (iid >= 114)
        //        {
        //            iid = 114;
        //            return RedirectToAction("Detail", new { id = iid.ToString() + "-bn" });
        //        }

        //        else
        //        {
        //            iid++;
        //            return RedirectToAction("Detail", new { id = iid.ToString() + "-bn" });
        //        }
        //    }


        //    return RedirectToAction("index");
        //}

        //public async Task<IActionResult> Previous(string id)
        //{
        //    var idSplit = id.Split('-');
        //    if (!string.IsNullOrEmpty(idSplit[0]))
        //    {
        //        int iid = Convert.ToInt32(idSplit[0]);
        //        if (iid <= 1)
        //        {
        //            iid = 1;
        //            return RedirectToAction("Detail", new { id = iid.ToString() + "-bn" });
        //        }

        //        else
        //        {
        //            iid = iid - 1;
        //            return RedirectToAction("Detail", new { id = iid.ToString() + "-bn" });
        //        }
        //    }


        //    return RedirectToAction("index");
        //}
    }
}
