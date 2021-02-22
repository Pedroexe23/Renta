using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pruebas.Models.Tecnologia;
using Pruebas.Models.Tecnologia.DAO;

namespace Pruebas.Controllers.Tecnologia
{
    public class CausantesController : Controller
    {
        private Municipalidad db = new Municipalidad();

        // GET: Causantes
        public ActionResult Index()
        {
            var causantes = db.Causantes.Include(c => c.Asignacion_Familiar).Include(c => c.Funcionario);
            return View(causantes.ToList());
        }

        // GET: Causantes/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Causante causante = db.Causantes.Find(id);
            if (causante == null)
            {
                return HttpNotFound();
            }
            return View(causante);
        }

        // GET: Causantes/Create
        public ActionResult Create()
        {
            ViewBag.TRAMO = new SelectList(db.Asignacion_Familiar, "Tramo", "Requisito_De_Sistema");
            ViewBag.NUM_CORRELATIVO = new SelectList(db.Funcionarios, "Id_Funcionario", "Rut");
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
                db.Causantes.Add(causante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TRAMO = new SelectList(db.Asignacion_Familiar, "Tramo", "Requisito_De_Sistema", causante.TRAMO);
            ViewBag.NUM_CORRELATIVO = new SelectList(db.Funcionarios, "Id_Funcionario", "Rut", causante.NUM_CORRELATIVO);
            return View(causante);
        }

        // GET: Causantes/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Causante causante = db.Causantes.Find(id);
            if (causante == null)
            {
                return HttpNotFound();
            }
            ViewBag.TRAMO = new SelectList(db.Asignacion_Familiar, "Tramo", "Requisito_De_Sistema", causante.TRAMO);
            ViewBag.NUM_CORRELATIVO = new SelectList(db.Funcionarios, "Id_Funcionario", "Rut", causante.NUM_CORRELATIVO);
            return View(causante);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Insertar()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Insertar(HttpPostedFileBase Files)
        {
            CausantesDAO causanteDAO = new CausantesDAO();
            List<Causante> causa = new List<Causante>();
            DocumentoDAO documentoDAO = new DocumentoDAO();
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

                    StreamReader streamReader = new StreamReader(Files.InputStream);
                    List<string> ListaA = new List<string>();
                    List<string> ListaB = new List<string>();
                    List<string> ListaC = new List<string>();
                    int count = 0;
                    while (!streamReader.EndOfStream)
                    {

                        var line = streamReader.ReadLine();
                        if (line.Equals(""))
                        {
                            line = "null|null";
                            var values = line.Split('|');
                            ListaA.Add(values[0]);
                            ListaB.Add(values[1]);
                            count = count + 1;
                        }
                        else
                        {
                            var values = line.Split('|');
                            if (count <= 4 || count == 4)
                            {
                                ListaA.Add(values[0]);
                                ListaB.Add(values[1]);
                                count = count + 1;
                            }
                            else if (count == 5)
                            {
                                ListaC.Add(values[0]);
                                ListaC.Add(values[1]);
                                ListaC.Add(values[2]);
                                ListaC.Add(values[3]);
                                ListaC.Add(values[4]);
                                ListaC.Add(values[5]);
                                ListaC.Add(values[6]);
                                ListaC.Add(values[7]);
                                ListaC.Add(values[8]);
                                ListaC.Add(values[9]);
                                ListaC.Add(values[10]);
                                ListaC.Add(values[11]);
                                ListaC.Add(values[12]);
                                ListaC.Add(values[13]);
                                ListaC.Add(values[14]);
                                ListaC.Add(values[15]);
                                ListaC.Add(values[16]);
                                ListaC.Add(values[17]);
                                count = count + 1;
                            }
                            else
                            {

                                Causante causantes = new Causante();

                                string utf8_String = values[2];
                                byte[] bytes = Encoding.Default.GetBytes(utf8_String);

                                short Num_Correlativo = Convert.ToInt16(values[0]);
                                int Codigo_tipo_causante = Int32.Parse(values[3]);
                                int Codigo_tipo_beneficiario = Int32.Parse(values[7]);

                                int Codigo_tipo_beneficio = Int32.Parse(values[9]);

                                DateTime Fecha_Reconocimiento = DateTime.Parse(values[13]);
                                short Tramo = Convert.ToInt16(values[14]);

                                int Monto_Beneficio = Int32.Parse(values[15]);
                                int Codigo_estado_Tupla = Int32.Parse(values[16]);
                                int Promedio_Renta;
                                try
                                {
                                    Promedio_Renta = Int32.Parse(values[18]);
                                }
                                catch (Exception)
                                {
                                    Promedio_Renta = 0;

                                }




                                causantes.NUM_CORRELATIVO = Num_Correlativo;
                                causantes.RUT_CAUSANTE = values[1];
                                causantes.NOMBRE_CAUSANTE = Encoding.UTF8.GetString(bytes);
                                causantes.CODIGO_TIPO_CAUSANTE = Codigo_tipo_causante;
                                causantes.TIPO_CAUSANTE = values[4];
                                causantes.RUT_BENEFICIARIO = values[5];
                                causantes.NOMBRE_BENEFICIARIO = values[6];
                                causantes.CODIGO_TIPO_BENEFICIARIO = Codigo_tipo_beneficiario;
                                causantes.TIPO_BENEFICIARIO = values[8];
                                causantes.CODIGO_TIPO_BENEFICIO = Codigo_tipo_beneficio;
                                causantes.TIPO_BENEFICIO = values[10];
                                causantes.RUT_EMPLEADOR = values[11];
                                causantes.NOMBRE_EMPLEADOR = values[12];
                                causantes.FECHA_RECONOCIMIENTO = Fecha_Reconocimiento;
                                causantes.TRAMO = Tramo;
                                causantes.MONTO_BENEFICIO = Monto_Beneficio;
                                causantes.CODIGO_ESTADO_TUPLA = Codigo_estado_Tupla;
                                causantes.GLOSA_ESTADO_TUPLA = values[17];
                                causantes.PROMEDIO_RENTA = Promedio_Renta;
                                causanteDAO.Crear(causantes);
                               
                                count = count + 1;




                            }

                        }

                    }
                     byte[] files = null;
                      Stream stream = Files.InputStream;
                      using (MemoryStream MS= new MemoryStream())
                      {
                          stream.CopyTo(MS);
                          files = MS.ToArray();


                      }
                      String Fechas = DateTime.Now.Date.ToString("dd-MM-yyyy");
                      Documento doc = new Documento();
                      int id = 1;
                      doc.id_Documento = id;
                      doc.Nombre = Files.FileName.Trim();
                      doc.documento1 = files;
                      doc.NombreReal = fileName;
                      doc.Fecha = DateTime.Parse(Fechas);
                      documentoDAO.Crear(doc);
                    
                    ViewBag.Message = "Archivo Subiendo";
                    return Redirect("Proceso_de_guardado");
               


                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Archivo erroneo";
                    ViewBag.Message = ex;
                    return View();

                }


            }





        }

        [HttpGet]
        public ActionResult Proceso_de_guardado()
        {
            CausantesDAO causanteDAO = new CausantesDAO();
            List<Causante> causantesguardados = causanteDAO.GetCausantes();
   
            return View(causantesguardados.OrderBy(Y => Y.NUM_CORRELATIVO).ToList());


        }

        [HttpPost]
        public ActionResult Proceso_de_guardado(String id)
        {
            CausantesDAO causanteDAO = new CausantesDAO();
            List<Causante> causantesguardados = causanteDAO.GetCausantes();
            List<Causante> Repetidos = new List<Causante>();
            List<Causante> guardados = new List<Causante>();   
            foreach (var items in causantesguardados)
            {
                int count = 0;
                Causante c = new Causante();
                c.NUM_CORRELATIVO = items.NUM_CORRELATIVO;
                c.RUT_CAUSANTE = items.RUT_CAUSANTE;
                c.NOMBRE_CAUSANTE = items.NOMBRE_CAUSANTE;
                c.CODIGO_TIPO_CAUSANTE = items.CODIGO_TIPO_CAUSANTE;
                c.TIPO_CAUSANTE = items.TIPO_CAUSANTE;
                c.RUT_BENEFICIARIO = items.RUT_BENEFICIARIO;
                c.NOMBRE_BENEFICIARIO = items.NOMBRE_BENEFICIARIO;
                c.CODIGO_TIPO_BENEFICIARIO = items.CODIGO_TIPO_BENEFICIARIO;
                c.TIPO_BENEFICIARIO = items.TIPO_BENEFICIARIO;
                c.CODIGO_TIPO_BENEFICIO = items.CODIGO_TIPO_BENEFICIO;
                c.TIPO_BENEFICIO = items.TIPO_BENEFICIO;
                c.RUT_EMPLEADOR = items.RUT_EMPLEADOR;
                c.NOMBRE_EMPLEADOR = items.NOMBRE_EMPLEADOR;
                c.FECHA_RECONOCIMIENTO = items.FECHA_RECONOCIMIENTO;
                c.TRAMO = items.TRAMO;
                c.MONTO_BENEFICIO = items.MONTO_BENEFICIO;
                c.CODIGO_ESTADO_TUPLA = items.CODIGO_ESTADO_TUPLA;
                c.GLOSA_ESTADO_TUPLA = items.GLOSA_ESTADO_TUPLA;
                c.PROMEDIO_RENTA = items.PROMEDIO_RENTA;
              

                
                    foreach (var item in db.Causantes)
                    {
                    count = 0;
                    Causante ca = new Causante();
                    ca.NUM_CORRELATIVO = item.NUM_CORRELATIVO;
                    ca.RUT_CAUSANTE = item.RUT_CAUSANTE;
                    if (c.NUM_CORRELATIVO==ca.NUM_CORRELATIVO && c.RUT_CAUSANTE.Equals(ca.RUT_CAUSANTE))
                    {
                        Repetidos.Add(c);
                        count = count + 1;
                        break;
                    }
                

                     }
                    if (count ==0)
                    {
                    db.Causantes.Add(c);
                    db.SaveChanges();

                    }
                

            }
            
            return Redirect("../Funcionarios/Proceso");


        }



        public ActionResult Descargar()
        {

            return View(db.Causantes.OrderBy(Y => Y.NUM_CORRELATIVO).ToList());
        }


        [HttpPost]
        public void Descargar(String id)
        {

            var gv = new GridView();


            gv.DataSource = db.Causantes.OrderBy(x => x.NUM_CORRELATIVO).ToList();
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
