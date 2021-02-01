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
using LINQtoCSV;
using RentaWEB.Models;

namespace RentaWEB.Controllers
{
    public class CausantesController : Controller
    {
        private MunicipalModel db = new MunicipalModel();

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

                
                CsvFileDescription csvFileDescription = new CsvFileDescription
                    {
                    SeparatorChar = '|' ,
                    FirstLineHasColumnNames = true,
                    IgnoreUnknownColumns =true

                };
                    CsvContext csvContext = new CsvContext();
                    StreamReader streamReader = new StreamReader(Files.InputStream);
                /*List<Causante> list = (List < Causante >)csvContext.Read<Causante>(streamReader, csvFileDescription);
                    list=list[0].*/
                IEnumerable<Causante> list = csvContext.Read<Causante>(streamReader, csvFileDescription);
                db.Causante.AddRange(list);
                    db.SaveChanges();




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
