using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LINQtoCSV;
using RentaWEB2._0.Models;
using ClosedXML.Excel;
using System.Text;

namespace RentaWEB2._0.Controllers
{
    public class CausantesController : Controller
    {
        private Municipalidad db = new Municipalidad();

        // GET: Causantes
        public ActionResult Index()
        {
            return View(db.Causante.ToList());
        }

        // GET: Causantes/Details/5
        public ActionResult Details(byte? id)
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
        public ActionResult Create([Bind(Include = "NUM_CORRELATIVO,RUT_CAUSANTE,NOMBRE_CAUSANTE,CODIGO_TIPO_CAUSANTE,TIPO_CAUSANTE,RUT_BENEFICIARIO,NOMBRE_BENEFICIARIO,CODIGO_TIPO_BENEFICIARIO,TIPO_BENEFICIARIO,CODIGO_TIPO_BENEFICIO,TIPO_BENEFICIO,RUT_EMPLEADOR,NOMBRE_EMPLEADOR,FECHA_RECONOCIMIENTO,TRAMO,MONTO_BENEFICIO,CODIGO_ESTADO_TUPLA,GLOSA_ESTADO_TUPLA,PROMEDIO_RENTA")] Causante causante)
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
        public ActionResult Edit(byte? id)
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
        public ActionResult Edit([Bind(Include = "NUM_CORRELATIVO,RUT_CAUSANTE,NOMBRE_CAUSANTE,CODIGO_TIPO_CAUSANTE,TIPO_CAUSANTE,RUT_BENEFICIARIO,NOMBRE_BENEFICIARIO,CODIGO_TIPO_BENEFICIARIO,TIPO_BENEFICIARIO,CODIGO_TIPO_BENEFICIO,TIPO_BENEFICIO,RUT_EMPLEADOR,NOMBRE_EMPLEADOR,FECHA_RECONOCIMIENTO,TRAMO,MONTO_BENEFICIO,CODIGO_ESTADO_TUPLA,GLOSA_ESTADO_TUPLA,PROMEDIO_RENTA")] Causante causante)
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
        public ActionResult Delete(byte? id)
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
        public ActionResult DeleteConfirmed(byte id)
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



                try
                {
                    CsvFileDescription csvFileDescription = new CsvFileDescription
                    {
                        SeparatorChar = '|',
                        FirstLineHasColumnNames = true,


                    };
                    CsvContext csvContext = new CsvContext();
                    StreamReader streamReader = new StreamReader(Files.InputStream);
                    IEnumerable<Causante> list = csvContext.Read<Causante>(streamReader, csvFileDescription);
                    db.Causante.AddRange(list);
                    db.SaveChanges();
                    ViewBag.Message = "Archivo Subido";
                    return Redirect("Descargar");

                }
                catch (Exception)
                {
                    ViewBag.Message = "Archivo erroneo";

                }

                return View();
            }





        }

        public ActionResult Descargar()
        {
            return View(db.Causante.ToList());
        }


        [HttpPost]
        public void Descargar(String id)
        {/*
       
            List<Causante>list = db.Causante.ToList();

                CsvFileDescription csvFileDescription = new CsvFileDescription
                {
                    SeparatorChar = ',',
                    FirstLineHasColumnNames = true,
                    EnforceCsvColumnAttribute = true
                };
            CsvContext csvContext = new CsvContext();
            byte[] file = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(memoryStream))
                {
                    csvContext.Write<Causante>(list, streamWriter, csvFileDescription);
                    streamWriter.Flush();
                     
                    
                }
                
            }
            return File(file, "text/csv", "Causante.csv");




           */

            StringWriter sb =  new StringWriter();
            string qry = ("Select * from dbo.Causante");
            IEnumerable<Causante> query = db.Database.SqlQuery<Causante>(qry);
            var list = db.Causante.OrderBy(x => x.NUM_CORRELATIVO).ToList();
            sb.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}",
                "NUM_CORRELATIVO", "RUT_CAUSANTE", "NOMBRE_CAUSANTE", "CODIGO_TIPO_CAUSANTE", "TIPO_CAUSANTE", "RUT_BENEFICIARIO",
                "NOMBRE_BENEFICIARIO", "CODIGO_TIPO_BENEFICIARIO", "TIPO_BENEFICIARIO", "CODIGO_TIPO_BENEFICIO", "TIPO_BENEFICIO",
                "RUT_EMPLEADOR", "NOMBRE_EMPLEADOR", "FECHA_RECONOCIMIENTO", "TRAMO", "MONTO_BENEFICIO", "CODIGO_ESTADO_TUPLA", 
                "GLOSA_ESTADO_TUPLA", "PROMEDIO_RENTA", Environment.NewLine).Split());
             
            foreach (var item in list)
            {
                sb.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}",
                    item.NUM_CORRELATIVO, item.RUT_CAUSANTE, item.NOMBRE_CAUSANTE, item.CODIGO_TIPO_CAUSANTE,
                    item.TIPO_CAUSANTE, item.RUT_BENEFICIARIO, item.NOMBRE_BENEFICIARIO, item.CODIGO_TIPO_BENEFICIARIO,
                    item.TIPO_BENEFICIARIO, item.CODIGO_TIPO_BENEFICIO, item.TIPO_BENEFICIO, item.RUT_EMPLEADOR, item.NOMBRE_EMPLEADOR,
                    item.FECHA_RECONOCIMIENTO, item.TRAMO, item.MONTO_BENEFICIO, item.CODIGO_ESTADO_TUPLA, item.GLOSA_ESTADO_TUPLA, item.PROMEDIO_RENTA, Environment.NewLine).Split()); 

            }
            var response = System.Web.HttpContext.Current.Response;
            response.BufferOutput = true;
            response.Clear();
            response.ClearHeaders();
            response.ContentEncoding = Encoding.Unicode;
            response.AddHeader("content-disposition", "attachment;filename=Causantes.CSV ");
            response.ContentType = "text/csv";
            response.Write(sb.ToString());
            response.End();

        }
    }
}
