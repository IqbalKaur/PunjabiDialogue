using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;


using PunjabiDialogueTalk.Models;


namespace PunjabiDialogueTalk.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        // GET: Home/Index
        public ActionResult Index()
        {
            ViewBag.dialogues = db.Dialogues.ToList();
            return View();
        }

        // POST: Home/Index
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Id,Content")] Dialogue dialogue)
        {
            if (ModelState.IsValid)
            {
                dialogue.CreatedAt = DateTime.UtcNow;
                // http://stackoverflow.com/questions/19936433/asp-net-mvc-5-identity-application-user-as-foreign-key
                dialogue.User = db.Users.Find(User.Identity.GetUserId());
                db.Dialogues.Add(dialogue);
                await db.SaveChangesAsync();
                TempData["Success"] = "Your dialogue has been posted";
                return RedirectToAction("Index");
            }
            ViewData["ErrorUploading"] = "Oops! There is some problem. Try agin.";
            return View(dialogue);
        }

        // GET: Home/Create
        public ActionResult CreateComment()
        {
            ViewBag.comments = db.Comments.ToList();
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "Id,Content,UserId,DialogueId")] Comment comment, Dialogue dialogue)
        {
            if (ModelState.IsValid)
            {
                comment.CreatedAt = DateTime.UtcNow;
                comment.User = db.Users.Find(User.Identity.GetUserId());
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown", comment.UserId);
            return View(comment);
        }
    }
}