using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Translator.Models;
using Translator.Models.Context;
using Translator.Models.Data;
using Translator.Models.VM;
using Translator.Telegram;
using X.PagedList;


namespace Translator.Controllers
{
    public class HomeController : Controller
    {
        GlossaryContext db;
        public HomeController(GlossaryContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            

            return View();
        }

        public IActionResult About(int? page)
        {

            TelegramChater chater = new TelegramChater(db);
            chater.RunChat();
            
            
            List<GlossaryVM> allvocabulary;
           
            var numberPage = page ?? 1;
            using (db)
            {
                allvocabulary = db.glossary.ToArray().Select(x => new GlossaryVM(x)).ToList();
               
            }
           
            ViewBag.OnePageOfProducts = allvocabulary.ToPagedList(numberPage, 4);
            return View(allvocabulary);
           
        }
        public ActionResult DelTranslate(int id)
        {
            using (db)
            {
                Glossary gl = db.glossary.Find(id);
                db.glossary.Remove(gl);
                db.SaveChanges();

            }
            TempData["DelTranslate"] = "Перевод удален";

            return RedirectToAction("About");
        }
        [HttpPost]
        public string ChangeTranslate(string newmytextcat, int id)
        {
            using (db)
            {
                Glossary gl = db.glossary.Find(id);
                
                gl.English = newmytextcat;
                
                
                db.SaveChanges();
            }
            return newmytextcat;
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
