using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cecchetti.Mattia._5H.SecondaWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;


namespace Cecchetti.Mattia._5H.SecondaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Prenota(Prenotazione p)
        {
            var db=new DBContext();
            db.Prenotazioni.Add(p);
            db.SaveChanges();
            return View("Elenco" , db);
        }
        public IActionResult Cancella( int id)
        {
            var db = new DBContext();
            Prenotazione prenotazione=db.Prenotazioni.Find(id);
            if(prenotazione!=null)
            {
            db.Remove(prenotazione);
            db.SaveChanges();
            return View("Elenco", db);
            }
            else
            {
                //return View("Cancella",db);
                return NotFound();
            }
            
        }

        [HttpGet]
         public IActionResult Modifica(int Id)
        {
            var db = new DBContext();
            Prenotazione prenotazione=db.Prenotazioni.Find(Id);
            if(prenotazione!=null)
            {
                return View("Modifica",prenotazione);
            }
            else
            {
                return NotFound();                
            }
            
        }

        [HttpPost]
        public IActionResult Modifica(int id,Prenotazione nuovo)
        {
            var db = new DBContext();
            var vecchio=db.Prenotazioni.Find(id);
            if(vecchio!=null)
            {
                vecchio.Nome=nuovo.Nome;
                vecchio.Email=nuovo.Email;
                db.Prenotazioni.Update(vecchio);
                db.SaveChanges();
                //return View("Grazie",db);
            }
            //return NotFound();
            return View("Elenco",db);
        }

        public IActionResult CancellaTutto()
        {   
            var db=new DBContext();
            db.RemoveRange(db.Prenotazioni);
            
            //Prenotazione prenotazione = db.Prenotazioni.Find(id);
            //db.Remove(prenotazione);
            db.SaveChanges();
            return View("Elenco",db);
        }  
        
        [HttpPost]
        public IActionResult Upload(CreatePost post)
        {
            MemoryStream stream=new MemoryStream();
            post.MyCSV.CopyTo(stream);
            stream.Seek(0,0);

            StreamReader fin=new StreamReader(stream);
            
            if(!fin.EndOfStream)
            {
                /*PrenotazioneContext db=new PrenotazioneContext();
                string riga=fin.ReadLine();*/
                var db=new DBContext(); //oppure PrenotazioneContext db=new PrenotazioneContext(); 
                string riga = fin.ReadLine();
                while(!fin.EndOfStream)
                {
                    riga=fin.ReadLine();
                    string[] colonne=riga.Split(";");
                    Prenotazione p=new Prenotazione{Nome=colonne[0],Email=colonne[1],DataPrenotazione=Convert.ToDateTime(colonne[2])};
                    db.Prenotazioni.Add(p);
                }   

                db.SaveChanges();
                return View("Elenco", db);    
                
            }
            return View();
        }
        
        [Authorize]
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Prenota()
        {
            return View();
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Elenco()
        {
            var db = new DBContext();
            return View(db.Prenotazioni);
        }
        public IActionResult Upload()
        {
            return View();
        }

    
    }
}
