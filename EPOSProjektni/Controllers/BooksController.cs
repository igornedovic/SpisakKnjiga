﻿using EPOSProjektni.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EPOSProjektni.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public Book Book { get; set; }
        public BooksController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment; // interfejs za dobijanje lokacije wwwroot foldera
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Update(int? id)
        {
            Book = new Book();
            if (id == null)
            {
                //dodavanje knjige koja nije na spisku
                return View(Book);
            }
            //azuriranje postojece knjige
            Book = _db.Books.FirstOrDefault(u => u.Id == id);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update()
        {
            if (ModelState.IsValid)
            {
                // cuvanje slike u wwwroot/image (wwwroot sadrzi staticke fajlove)
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(Book.FajlSlike.FileName);
                string extension = Path.GetExtension(Book.FajlSlike.FileName);
                Book.Slika = fileName = fileName + extension; // u polje slika sacuvati naziv fajla slike i ekstenziju
                string path = Path.Combine(wwwRootPath + "/image/", fileName);
                using(var fileStream = new FileStream(path, FileMode.Create))
                {
                    Book.FajlSlike.CopyTo(fileStream); 
                }

                // upisivanje podataka u bazu

                if (Book.Id == 0)
                {
                    // dodavanje ako ne postoji u bazi
                    _db.Books.Add(Book);
                }
                else
                {
                    // azuriranje ukoliko vec postoji
                    _db.Books.Update(Book);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Book);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() }); // pristupom Books/getall dobijamo listu svih zapisa o knjigama
                                                                       // iz baze u vidu JSON formata
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            // brisanje zapisa iz baze

            var bookFromDb = await _db.Books.FirstOrDefaultAsync(u => u.Id == id); // pronalazi knjigu sa id-em koji se poklapa 
                                                                                   // sa id-em trazene knjige za brisanje

            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Greška pri brisanju" }); // JSON sa porukom neuspeha
            }
            _db.Books.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Uspešno ste obrisali zapis" }); // JSON sa porukom uspeha
        }
        #endregion
    }
}
