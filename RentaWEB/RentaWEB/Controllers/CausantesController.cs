using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentaWEB.Models;

namespace RentaWEB.Controllers
{
    public class CausantesController : Controller
    {
        private CausanteModel db = new CausanteModel();

        // GET: Causantes
        public ActionResult Index()
        {
            return View(db.Causante.ToList());
        }

        // GET: Causantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Causante causante = db.Causante.Find(id);
            if (causante == null)
            {
                return HttpNotFound();
            }
            return View(causante);
        }

        // GET: Causantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Causantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Num_Correlativo,Rut_Causante,Nombre_Causante,Cod_Tipo_Causante,Tipo_Causante,Rut_beneficiario,Nombre_beneficiario,Codigo_Tipo_beneficiario,Tipo_beneficiario,Codigo_Tipo_Beneficio,Tipo_Beneficio,Rut_Empleador,Nombre_Empleador,Fecha_Reconocimiento,Tramo,Monto_Beneficiario,Codigo_Estado_Tupla,Glosa_Estado_Tupla,Renta_Promedio")] Causante causante)
        {
            if (ModelState.IsValid)
            {
                db.Causante.Add(causante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(causante);
        }

        // GET: Causantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Causante causante = db.Causante.Find(id);
            if (causante == null)
            {
                return HttpNotFound();
            }
            return View(causante);
        }

        // POST: Causantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Num_Correlativo,Rut_Causante,Nombre_Causante,Cod_Tipo_Causante,Tipo_Causante,Rut_beneficiario,Nombre_beneficiario,Codigo_Tipo_beneficiario,Tipo_beneficiario,Codigo_Tipo_Beneficio,Tipo_Beneficio,Rut_Empleador,Nombre_Empleador,Fecha_Reconocimiento,Tramo,Monto_Beneficiario,Codigo_Estado_Tupla,Glosa_Estado_Tupla,Renta_Promedio")] Causante causante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(causante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(causante);
        }

        // GET: Causantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Causante causante = db.Causante.Find(id);
            if (causante == null)
            {
                return HttpNotFound();
            }
            return View(causante);
        }

        // POST: Causantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Causante causante = db.Causante.Find(id);
            db.Causante.Remove(causante);
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
