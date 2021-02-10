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
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data.SqlClient;


namespace RentaWEB2._0.Controllers
{
    public class CausantesController : Controller
    {
        private Municipalidad db = new Municipalidad();
        private SqlConnection conexion =  new SqlConnection("data source=TECNO-PRACTI;initial catalog=Municipalidad;integrated security=True;");

        // GET: Causantes
        public ActionResult Index()
        {

            return View(db.Causante.ToList());
        }
        [HttpPost]
        public ActionResult Index(string Actualizar, Causante causante)
        {

            List<Causante> causantes = new List<Causante>();
            int tramo = 0;
                short monto = 0;
            foreach (var item in db.Causante)
            {
                causante.NUM_CORRELATIVO= item.NUM_CORRELATIVO;
                causante.RUT_CAUSANTE= item.RUT_CAUSANTE;
                causante.NOMBRE_CAUSANTE= item.NOMBRE_CAUSANTE;
                causante.CODIGO_TIPO_CAUSANTE= item.CODIGO_TIPO_CAUSANTE;
                causante.TIPO_CAUSANTE= item.TIPO_CAUSANTE;
                causante.RUT_BENEFICIARIO= item.RUT_BENEFICIARIO;
                causante.NOMBRE_BENEFICIARIO= item.NOMBRE_BENEFICIARIO;
                causante.CODIGO_TIPO_BENEFICIARIO= item.CODIGO_TIPO_BENEFICIARIO;
                causante.TIPO_BENEFICIARIO= item.TIPO_BENEFICIARIO;
                causante.CODIGO_TIPO_BENEFICIO= item.CODIGO_TIPO_BENEFICIO;
                causante.TIPO_BENEFICIO= item.TIPO_BENEFICIO;
                causante.RUT_EMPLEADOR= item.RUT_EMPLEADOR;
                causante.NOMBRE_EMPLEADOR= item.NOMBRE_EMPLEADOR;
                causante.FECHA_RECONOCIMIENTO= item.FECHA_RECONOCIMIENTO;
                causante.TRAMO = item.TRAMO;
                causante.CODIGO_ESTADO_TUPLA= item.CODIGO_ESTADO_TUPLA;
                causante.GLOSA_ESTADO_TUPLA= item.GLOSA_ESTADO_TUPLA;
                causante.PROMEDIO_RENTA= item.PROMEDIO_RENTA;
                
                causante.RUT_CAUSANTE = item.RUT_CAUSANTE;
                tramo = item.TRAMO;
                foreach (var asig in db.Asignacion_Familiar)
                {
                    if (tramo==asig.Tramo)
                    {
                        causante.MONTO_BENEFICIO = (short)asig.Monto;

                    }
                    

                }
               
                conexion.Close();
                conexion.Open();
                String Cadena = "update Causante set MONTO_BENEFICIO = "+ causante.MONTO_BENEFICIO +" where NUM_CORRELATIVO ="+causante.NUM_CORRELATIVO+"";
                
                   
                SqlCommand command = new SqlCommand(Cadena, conexion);
                int cant;
                cant=command.ExecuteNonQuery();
                conexion.Close();
            }

            return Redirect("Index");
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
                return RedirectToAction("Index");
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
                try
                {
                    String fileName = Path.GetFileName(Files.FileName);

                    String folderpath = Path.Combine(Server.MapPath("~/Views/Causantes/descargas"), fileName);


                    Files.SaveAs(folderpath);




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
                    return Redirect("../Funcionarios/Proceso");
                }
                catch (Exception)
                {
                    ViewBag.Message = "Archivo erroneo";
                    return View();

                }





            }





        }

        public ActionResult Descargar()
        {

            return View(db.Causante.ToList());
        }


        [HttpPost]
        public void Descargar(String id)
        {

            var gv = new GridView();
           
            gv.DataSource = db.Causante.OrderBy(x => x.NUM_CORRELATIVO).ToList();
            gv.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Causantes.xls", DateTime.Now));
            Response.ContentType = "application/excel";
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter sb = new StringWriter();
            HtmlTextWriter htmlTw = new HtmlTextWriter(sb);
            gv.RenderControl(htmlTw);
           
            Response.Write(sb.ToString());
            Response.End();
        }
    }
}

