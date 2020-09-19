using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Push.Models;

namespace Push.Controllers
{
    public class EncabezadosController : Controller
    {
        private PushContext db = new PushContext();

        // GET: Encabezados
        public ActionResult Index()
        {
            return View(db.Encabezado.ToList());
        }

        // GET: Encabezados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encabezado encabezado = db.Encabezado.Find(id);
            if (encabezado == null)
            {
                return HttpNotFound();
            }
            return View(encabezado);
        }

        // GET: Encabezados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Encabezados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EncabezadoId,TipoRegistro,TipoArchivo,Identificacion,Periodo")] Encabezado encabezado)
        {
            if (ModelState.IsValid)
            {
                
                db.Encabezado.Add(encabezado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(encabezado);
        }

        // GET: Encabezados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encabezado encabezado = db.Encabezado.Find(id);
            if (encabezado == null)
            {
                return HttpNotFound();
            }
            return View(encabezado);
        }

        // POST: Encabezados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EncabezadoId,TipoRegistro,TipoArchivo,Identificacion,Periodo")] Encabezado encabezado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encabezado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(encabezado);
        }

        // GET: Encabezados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encabezado encabezado = db.Encabezado.Find(id);
            if (encabezado == null)
            {
                return HttpNotFound();
            }
            return View(encabezado);
        }

        // POST: Encabezados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Encabezado encabezado = db.Encabezado.Find(id);
            db.Encabezado.Remove(encabezado);
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
