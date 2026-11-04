using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WEB_HW_05.Models;
using WEB_HW_05.Models.ViewModels;

namespace WEB_HW_05.Controllers
{
    public class HomeController : Controller
    {
        List<Author> authorList = new List<Author>()
        {
            new Author { Id = 1, Name = "Nimra Ahmed",Country = "Pakistan"  },
            new Author { Id = 2, Name = "Umerah Ahmed",Country = "Pakistan" }
        };
        static List<Book> bookList = new List<Book>()
        { 
            new Book {Id=1,Title="Janat k Patte",Price=1500,AuthorId=2},
            new Book {Id=2,Title="Peer e Kamil",Price=1200,AuthorId=1},
            new Book {Id=3,Title="Abye Hayaat",Price=1800,AuthorId=1}
        };

        public IActionResult Index()
        {
            HomeViewModel model= new HomeViewModel();
            model.Authors = authorList;
            model.Books = new List<BookCardViewModel>();
            foreach(Book b in bookList)
            {
                string authorName = "";
                foreach(Author a in authorList)
                {
                    if(a.Id==b.AuthorId)
                    {
                        authorName = a.Name;
                    }
                }
                BookCardViewModel book = new BookCardViewModel();
                book.Id = b.Id;
                book.Title = b.Title;
                book.Price = b.Price;
                book.AuthorName = authorName;
                model.Books.Add(book);
            }
            return View(model);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            if (title == null || title.Trim() == "")
            {
                return RedirectToAction("Index");
            }
            int newId = 1;
            if (bookList.Count > 0)
            {
                newId = bookList[bookList.Count - 1].Id + 1;
            }
            Book b = new Book();
            b.Id = newId;
            b.Title = title;
            b.Price = price;
            b.AuthorId = authorId;
            bookList.Add(b);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            if (name == null || name.Trim() == "")
            {
                return RedirectToAction("Index");
            }
            int newId = 1;
            if (authorList.Count > 0)
            {
                newId = authorList[authorList.Count - 1].Id + 1;
            }
            Author a = new Author();
            a.Id = newId;
            a.Name = name;
            a.Country = country;
            authorList.Add(a);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Id == id)
                {
                    bookList.RemoveAt(i);
                    break;
                }
            }

            return RedirectToAction("Index");
        }
    }
}
