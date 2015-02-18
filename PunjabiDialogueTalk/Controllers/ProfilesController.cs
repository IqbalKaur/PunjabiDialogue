using System.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Drawing.Imaging;
using PunjabiDialogueTalk.Models;

namespace PunjabiDialogueTalk.Controllers
{
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profiles
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Profiles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HomeTown,BirthDate,DisplayName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Avatar,DisplayName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HomeTown,BirthDate,DisplayName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Profiles/EditMyProfile
        public ActionResult EditMyProfile()
        {
            var id = User.Identity.GetUserId();
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        /* Save and Resize the image */
        public string saveResizeImage(HttpPostedFileBase image)
        {
            var imgpath = System.IO.Path.GetFileName(image.FileName);

            if (imgpath != null)
            {
                Guid uid = Guid.NewGuid(); //get a unique identifier variable
                string FilePathStr = Server.MapPath("~/") + "Avatar";

                if (!(Directory.Exists(FilePathStr)))
                {
                    Directory.CreateDirectory(FilePathStr);
                }

                string SaveLocation = "~/Avatar/" + imgpath;
                string fileExtention = image.ContentType;
                int fileLenght = image.ContentLength;
                if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                {
                    if (fileLenght <= 1048576)
                    {
                        System.Drawing.Bitmap bmpUploadedImage = new System.Drawing.Bitmap(image.InputStream);

                        System.Drawing.Image objImage = Helpers.Common.ScaleImage(bmpUploadedImage, 81);
                        objImage.Save(Server.MapPath(SaveLocation), ImageFormat.Jpeg);
                    }
                }
            }
            return imgpath;
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMyProfile([Bind(Include = "Id,HomeTown,BirthDate,DisplayName,Email,UserName")] User user, HttpPostedFileBase image)
        {
            
            if (ModelState.IsValid)
            {
                var userfromDb = db.Users.Where(u => u.Id == user.Id).First();
                userfromDb.HomeTown = user.HomeTown;
                userfromDb.BirthDate = user.BirthDate;
                userfromDb.DisplayName = user.DisplayName;
                userfromDb.Email = user.Email;
                userfromDb.UserName = user.UserName;
                if (image != null)
                {
                    var filename = saveResizeImage(image);
                    userfromDb.Avatar = filename;
                }               
                db.SaveChanges();
                return RedirectToAction("Index");            
            }
            return View(user);       
        }
            

        // GET: Profiles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
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
