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
                List<Funcionario> funcionarios = new List<Funcionario>();
                foreach (var item in db.Causante)
                {
                    String nombres = " ", apellidos = " ", nombre1 = " ", nombre2 = " ", apellido1 = " ", apellido2 = " ", subapellido1 = " ", subapellido2 = " ", subapellido3 = " ";
                    String[] subs = item.NOMBRE_CAUSANTE.Split(' ');
                    short id_Funcionario = item.NUM_CORRELATIVO;
                    String Rut = item.RUT_CAUSANTE;
                    if (item.NOMBRE_CAUSANTE.Length == 35)
                    {
                        for (int i = 0; i == subs.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    apellido2 = subs[i];
                                    break;
                                case 1:
                                    subapellido3 = subs[i];
                                    break;
                                case 2:
                                    subapellido2 = subs[i];
                                    break;
                                case 3:
                                    subapellido1 = subs[i];
                                    break;
                                case 4:

                                    nombre2 = subs[i];
                                    break;
                                case 5:
                                    nombre1 = subs[i];
                                    break;

                                default:
                                    break;
                            }
                            apellido1 = subapellido1 + " " + subapellido2 + " " + subapellido3;
                            apellidos = apellido1 + " " + apellido2;
                            nombres = nombre1 + " " + nombre2;
                        }

                    }
                    else if (item.NOMBRE_CAUSANTE.Substring(0, 16).Equals("GONZALEZ ACEVEDO"))
                    {
                        for (int i = 0; i == subs.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    apellido2 = subs[i];
                                    break;
                                case 1:
                                    apellido1 = subs[i];
                                    break;
                                case 2:
                                    nombre1 = subs[i];
                                    break;


                                default:
                                    break;
                            }

                            apellidos = apellido1 + " " + apellido2;
                            nombres = nombre1;
                        }

                    }

                    else
                    {
                        for (int i = 0; i == subs.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    apellido2 = subs[i];
                                    break;
                                case 1:
                                    apellido1 = subs[i];
                                    break;
                                case 2:
                                    nombre2 = subs[i];
                                    break;
                                case 3:
                                    nombre1 = subs[i];
                                    break;



                                default:
                                    break;
                            }

                            apellidos = apellido1 + " " + apellido2;
                            nombres = nombre1 + " " + nombre2;
                        }
                    }
                    Funcionario funcionario = new Funcionario();
                    funcionario.Id_Funcionario = id_Funcionario;
                    funcionario.Rut = Rut;
                    funcionario.Nombres = nombres;
                    funcionario.Apellidos = apellidos;

                    db.Funcionario.Add(funcionario);
                    db.SaveChangesAsync();

                }
               


                ViewBag.Message = "Archivo Subido";
                    return Redirect("Descargar");

                
                
                    //ViewBag.Message = "Archivo erroneo";

                

                //return View();
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
            Response.AddHeader("content-disposition",String.Format( "attachment;filename=Causantes.xls", DateTime.Now));
            Response.ContentType = "application/excel";
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter sb = new StringWriter();
            HtmlTextWriter htmlTw = new HtmlTextWriter(sb);
            gv.RenderControl(htmlTw);
            Response.Write(sb.ToString());
            Response.End();
        }
}   }

