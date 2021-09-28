using BookListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Book Book { get; set; }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int?id)
        {
            Book = new Book();
            if(id==null)
            {
                //create
                return View(Book);
            }
            //a ce niveau vous pouvez utiliser la methode Find() en lui passant juste le parametre id par contre si vous utiliser la Fonction FirstOrDefault()
            //comme Methode vous allez devoir utiliser u=>u.Id==id
            Book = _db.Book.FirstOrDefault(u => u.Id == id);
                return View(Book);
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if(ModelState.IsValid)
            {
                if(Book.Id==0)
                {
                    //Create
                    _db.Book.Add(Book);
                }
                else
                {
                    //Update
                    _db.Book.Update(Book);
                }
                // pui en dernière Position nous allons =>poussez<= les données dans la Base de données
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            //dans le cas ou le Model n'est pas Valide 9a me Gardera sur la même Page avec Les même données Invalid
            return View(Book);
        }

        #region Api Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.Book.ToList() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //recuperer un livre de la base de données en function de l'identifiant
            var BookFromDb = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);

            //Chaque fois que vous effectuer un acces a la database pour une Modification il est capital d'effectuer un Test

            if (BookFromDb == null)
            {
                return Json(new { success = false, Message = "Impossible de supprimer un livre qui n'existe pas dans la base de données" });
            }
            _db.Book.Remove(BookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "suppression effectuer avec succès" });
        }
        #endregion
    }
}
