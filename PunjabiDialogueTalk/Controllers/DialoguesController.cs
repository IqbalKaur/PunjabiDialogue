using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PunjabiDialogueTalk.Models;

namespace PunjabiDialogueTalk.Controllers
{
    public class DialoguesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dialogues
        public async Task<ActionResult> Index()
        {
            var dialogues = db.Dialogues.Include(d => d.User);
            return View(await dialogues.ToListAsync());
        }

        // GET: Dialogues/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dialogue dialogue = await db.Dialogues.FindAsync(id);
            if (dialogue == null)
            {
                return HttpNotFound();
            }
            return View(dialogue);
        }

        // GET: Dialogues/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown");
            return View();
        }

        // POST: Dialogues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Content,CreatedAt,UserId")] Dialogue dialogue)
        {
            if (ModelState.IsValid)
            {
                db.Dialogues.Add(dialogue);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown", dialogue.UserId);
            return View(dialogue);
        }

        // GET: Dialogues/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dialogue dialogue = await db.Dialogues.FindAsync(id);
            if (dialogue == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown", dialogue.UserId);
            return View(dialogue);
        }

        // POST: Dialogues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Content,CreatedAt,UserId")] Dialogue dialogue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dialogue).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown", dialogue.UserId);
            return View(dialogue);
        }

        // GET: Dialogues/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dialogue dialogue = await db.Dialogues.FindAsync(id);
            if (dialogue == null)
            {
                return HttpNotFound();
            }
            return View(dialogue);
        }

        // POST: Dialogues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Dialogue dialogue = await db.Dialogues.FindAsync(id);
            db.Dialogues.Remove(dialogue);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
