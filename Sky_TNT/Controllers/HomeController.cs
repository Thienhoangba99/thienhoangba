using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using bai5_Anh.Models;
using PagedList;
namespace bai5_Anh.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index(string chao, int? page, string search)
        {
            var img = db.img.Include(i => i.Images_GroupImages);
            ViewBag.listimg = img.OrderByDescending(i => i.CountView).ToList();
            if (DateTime.Now.Hour < 5)
            {
                ViewBag.chao = "Good Night !";
            }
            else if (DateTime.Now.Hour < 11)
            {
                ViewBag.chao = "Good Morning !";
            }
            else if (DateTime.Now.Hour < 18)
            {
                ViewBag.chao = "Good Afternoon !";
            }
            else if (DateTime.Now.Hour < 24)
            {
                ViewBag.chao = "Good Evening !";
            }
            if(!string.IsNullOrEmpty(search))
            {
                if (search.Contains("@gmail.com"))
                {
                    img = img.Where(i => i.Email.Contains(search));
                }
                else
                {
                    img = img.Where(i => i.Images_GroupImages.NameGroup.Contains(search));
                }
            }
            return View(img.OrderBy(e => e.IdImages).ToPagedList(page ?? 1, 6 ));
        }

    }
}