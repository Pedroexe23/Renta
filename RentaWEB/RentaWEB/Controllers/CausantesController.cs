using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
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
        public ActionResult Inicio()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inicio(FormCollection formCollection)
        {
            Usuario usuario = new Usuario();
            String User = formCollection["nombre-txt"];
            String Contrasena = formCollection["contrasena-txt"];
            if (User.Equals(usuario.Username) && Contrasena.Equals(usuario.Password))
            {
                return RedirectToAction("Insertar");
            }
            return View();

        }
        [HttpGet]
        public ActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insertar(HttpPostedFileBase Files)
        {

            if (Files == null || Files.ContentLength == 0)
            {
                return Content("file not selected");
            }
            else
            {
                String fileName = Path.GetFileName(Files.FileName);

                String folderpath = Path.Combine(Server.MapPath("~/Views/Causantes/descargas"), fileName);
               

                Files.SaveAs(folderpath);
                ViewBag.Message = "Archivo Subido";

                //Crea un DataTable.
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[19]{
                new DataColumn("Num_Correlativo",typeof(int)),
                new DataColumn("Rut_Causante",typeof(string)),
                new DataColumn("Nombre_Causante",typeof(string)),
                new DataColumn("Cod_Tipo_Causante",typeof(int)),
                new DataColumn("Tipo_Causante",typeof(string)),
                new DataColumn("Rut_beneficiario",typeof(string)),
                new DataColumn("Nombre_beneficiario",typeof(string)),
                new DataColumn("Codigo_Tipo_beneficiario",typeof(int)),
                new DataColumn("Tipo_beneficiario",typeof(string)),
                new DataColumn("Codigo_Tipo_Beneficio",typeof(int)),
                new DataColumn("Tipo_Beneficio",typeof(string)),
                new DataColumn("Rut_Empleador",typeof(string)),
                new DataColumn("Nombre_Empleador",typeof(string)),
                new DataColumn("Fecha_Reconocimiento",typeof(DateTime)),
                new DataColumn("Tramo",typeof(int)),
                new DataColumn("Monto_Beneficiario",typeof(int)),
                new DataColumn("Codigo_Estado_Tupla",typeof(int)),
                new DataColumn("Glosa_Estado_Tupla",typeof(string)),
                 new DataColumn("Renta_Promedio",typeof(int)) });

                //Lee el contenido del archivo CSV.
                string csvData = System.IO.File.ReadAllText(folderpath);
                //Ejecuta un bucle sobre las filas.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;

                        //Ejecuta un bucle sobre las columnas.
                        foreach (string cell in row.Split('|'))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell;
                            i++;
                        }

                    }
                }
               


                    return View();
            }

        }
        public FileResult Descargar()
        {
            String fileName = Path.GetFileName("archivo.csv");

            String folderpath = Path.Combine(Server.MapPath("~/Views/Causantes/descargas"), fileName);
            ViewBag.Message = "Archivo Subido";

            return File(folderpath, "text/csv", "archivo.csv");
        }
    }
}
