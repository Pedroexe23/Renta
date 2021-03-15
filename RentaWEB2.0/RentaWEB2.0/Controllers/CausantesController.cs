using RentaWEB2._0.Models;
using RentaWEB2._0.Models.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace RentaWEB2._0.Controllers
{
    public class CausantesController : Controller
    {
        private Municipalidad db = new Municipalidad();
        private SqlConnection conexion = new SqlConnection("data source=TECNO-PRACTI;initial catalog=Municipalidad;integrated security=True;");
       

        // GET: Causantes
        public ActionResult Index()
        {
            return View();
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
            /* se extrae los datos del el nombre de usuario y contraseña */
            Usuario usuario = new Usuario();
            String User = formCollection["nombre-txt"];
            String Contrasena = formCollection["contrasena-txt"];
            /* Si el nombre de la variable de la clase es igual a la variable User 
             * y la variable Constrasena es igual a Password entonces se retorna a la vista y controlador get del index   */
            if (User.Equals(usuario.Username) && Contrasena.Equals(usuario.Password))
            {
                return RedirectToAction("Index");
            }
            return View();

        }
        /*solo se va  mostrar la vista de insertar */
        [HttpGet]
        public ActionResult Insertar()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Insertar(HttpPostedFileBase Files)
        {
            /* se crea dos clase de Dao una para el Causante y otro para el Documento  esas clases son para almacenamiento temporal
             * en caso que en la parte de proceso de guardado se Cancele se tiene que volver al proceso de insertar otra vez, ademas
             * el documento y todo el almacenamiento que estaban en la Clase DAO se borrara automaticamente
             * */

            CausantesDAO causanteDAO = new CausantesDAO();
            DocumentoDAO documentoDAO = new DocumentoDAO();
            causanteDAO.EliminarCausantes();
            documentoDAO.EliminarDocumento();

            List<Causante> causa = new List<Causante>();
            /* en caso de que no hay nada en el documento se enviara un mensaje de Archivo Vacio*/
            if (Files == null || Files.ContentLength == 0)
            {
                return ViewBag.Message = "Archivo Vacio";
            }
            else
            {
                try
                {
                    /* toma el nombre y el contenido del documento , lo envia y lo guarda en una carpeta llamada "descargas"  */
                    String fileName = Path.GetFileName(Files.FileName);

                    String folderpath = Path.Combine(Server.MapPath("~/Views/Causantes/descargas"), fileName);


                    Files.SaveAs(folderpath);
                    /* toma el contenido del la tabla lo dividi en tres listas las  Listas A y B son para contenido que no se guarda 
                     * use una variable Llamada Count si el count estan en menor o igual a 4 el contenido
                     * estara en la lista A y B en caso de si una de las filas esta vacia se agregan el Valor Null,
                     * se guardan en las listas A y B si Count es igual a 5 los datos se guardara en la lista C 
                     * que son para la Columnas del causante  ej: Num.Correlativo , Rut Causante, etc.
                     */
                    StreamReader streamReader = new StreamReader(Files.InputStream, System.Text.Encoding.UTF8);
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
                                /* ya si Count esta mayor a  5  se guardara los datos de los causantes  que al inicio estaran en String al inicio 
                                 * y algunos datos del causante se conviete en variables Int o Short ej: NUM_CORRELATIVO, CODIGO_TIPO_CAUSANTE
                                 * CODIGO_TIPO_BENEFICIARIO, CODIGO_TIPO_BENEFICIO, TRAMO, MONTO_BENEFICIO, CODIGO_ESTADO_TUPLA y PROMEDIO_RENTA 
                                 * ademas que la fechas son guardardas en la varible DATE y todo los datos de la clase Causante se guardan una Lista Temporal 
                                 * Llamada CausanteDAO 
                                 */
                                Causante causantes = new Causante();

                                short Num_Correlativo = Convert.ToInt16(values[0]);
                                int Codigo_tipo_causante = Int32.Parse(values[3]);
                                int Codigo_tipo_beneficiario = Int32.Parse(values[7]);

                                int Codigo_tipo_beneficio = Int32.Parse(values[9]);

                                DateTime Fecha_Reconocimiento = DateTime.Parse(values[13].Replace("/", "-"));
                                short Tramo = Convert.ToInt16(values[14]);

                                int Monto_Beneficio = Int32.Parse(values[15]);
                                int Codigo_estado_Tupla = Int32.Parse(values[16]);
                                int Promedio_Renta;
                                /* se enviara un try en caso que el promedio renta sea Null se cambiara un 0
                                 * si no es el caso entonces se enviara el numero que esta en el archivo  */
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
                                causantes.NOMBRE_CAUSANTE = values[2];
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

                    /* si el nombre del documento es igual a nombre del documento que hemos subido entonces se vas a extraer 
                    *el nombre, el tamaño, la extension y la fecha del el dia que se subio yse guardara en el archivo temporal de documentoDAO
                     *para mas informacion de la extension dirijase al metodo GetFileTypeByExtension */
                   String result = string.Empty;
                    String Fechas = DateTime.Now.Date.ToString("yyyy/MM/dd");

                    foreach (string strfile in Directory.GetFiles(Server.MapPath("~/Views/Causantes/descargas")))
                    {
                        FileInfo fi = new FileInfo(strfile);
                        if (fi.Name.Equals(fileName))
                        {
                            Documento documento = new Documento();
                            documento.Archivo = fi.Name;
                            documento.Tamaño = fi.Length;
                            documento.tipo = GetFileTypeByExtension(fi.Extension);
                            documento.Fecha = DateTime.Parse(Fechas);
                            documentoDAO.Creardocumento(documento);
                        }


                    }
                    /* despues sera enviado al controlador de Proceso_de_guardado  */

                    ViewBag.Message = "Archivo Subiendo";
                    return Redirect("Proceso_de_guardado");


                }
                /* en caso qyue el doucmento son sea  igual al formato entonces se enviara 
                 * un mensaje de error diciedo Archivo Erroneo  */
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
            /* en la  parte get se mostrara si los datos en la lista temporal de CausanteDAO en order por su numero Correlativo  */
            CausantesDAO causanteDAO = new CausantesDAO();
            List<Causante> causantesguardados = causanteDAO.GetCausantes();

            return View(causantesguardados.OrderBy(Y => Y.NUM_CORRELATIVO).ToList());


        }

        [HttpPost]
        public ActionResult Proceso_de_guardado(String Guardar)
        {
           /* si aceptan con todos los datos comienza el proceso de guardado que se guardara los datos de la clase Causante  y  Documento 
            * a la  base de datos ademas que crean 3 listas una en la clase Documento que se almacena en la lista tempora de DocumentoDAO y las dos en la clase Causante una para guardar los repetidos 
            * y otro que esta guardado en la lista temporal de CausanteDAO */
            int docs = 0;
       
            DocumentoDAO documentoDAO = new DocumentoDAO();
            List<Documento> documentos = documentoDAO.GetDocumentos();
            CausantesDAO causanteDAO = new CausantesDAO();
            List<Causante> causantesguardados = causanteDAO.GetCausantes();
            List<Causante> Repetidos = new List<Causante>();

            /* primero comienza en los datos guardados en la lista temporal de CausanteDAO se mostrara de uno por uno */
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


                /* aqui se  mostrara los datos que estan en la Base de Datos  solo se mostrara los datos de NUM_CORRELATIVO y Rut del Causante */
                foreach (var item in db.Causante)
                {
                   
                    count = 0;
                    Causante ca = new Causante();
                    ca.NUM_CORRELATIVO = item.NUM_CORRELATIVO;
                    ca.RUT_CAUSANTE = item.RUT_CAUSANTE;
                    /* en caso que si el numero NUM_CORRELATIVO de la lista es igual al NUM_CORRELATIVO de la base de datos y el RUT del CAUSANTE de la lista temporal
                     * es igual RUT del CAUSANTE de la base datos entonces se guarda los datos de la lista temporal en otra lista llamada repetidos */
                    if (c.NUM_CORRELATIVO == ca.NUM_CORRELATIVO && c.RUT_CAUSANTE.Equals(ca.RUT_CAUSANTE))
                    {
                        Repetidos.Add(c);
                        count = count + 1;
                        break;
                    }


                }
                /* si se ha mostrado todos los datos de la base dato y no ha parecido ninguna se guardara como un nuevo causante incluso si 
                 * la base de datos esta vacia se los datos del causante se guardara por defecto */
                if (count == 0)
                {
                    
                    db.Causante.Add(c);
                    db.SaveChanges();

                }


            }

            /* este proceso se mostrara el la lista temporal de DocumetoDAO 
             * */
            
           
            foreach (var item in documentos)
            {
                Documento documento = new Documento();
                documento.Archivo = item.Archivo;
                documento.Tamaño = item.Tamaño;
                documento.tipo = item.tipo;
                documento.Fecha = item.Fecha;
                String fechadoc = item.Fecha.Date.ToString();
                fechadoc = fechadoc.Substring(0, 10);
                String[] Fechacompletadoc = fechadoc.Split('/');
                fechadoc = Fechacompletadoc[2] + "/" + Fechacompletadoc[1] + "/" + Fechacompletadoc[0];

                /* se  muestra los datos de el documeto o los documentos que esta en la base de datos */

                foreach (var items in db.Documento)
                {
                    docs = 0;
                    Documento Documentos = new Documento();
                    Documentos.Id_documento = items.Id_documento;
                    Documentos.Archivo = items.Archivo;
                    Documentos.Tamaño = items.Tamaño;
                    Documentos.tipo = items.tipo;
                    Documentos.Fecha = items.Fecha;
                    /* si el nombre nombre del el archivo de la lista temporal es igual al nombre de la base de datos
                     * y tipo de la lista temporal es igual al tipo de la base de datos entonces se actualiza la fecha de el documento de la base de datos    */
                    if (Documentos.Archivo.Equals(documento.Archivo) && Documentos.tipo.Equals(documento.tipo))
                    {
                        docs = docs + 1;
                        conexion.Close();
                        conexion.Open();
                        String Cadena = " update Documento set Fecha="+"'"+documento.Fecha+"'where Id_documento="+ Documentos.Id_documento + "";
                        SqlCommand command = new SqlCommand(Cadena, conexion);
                        int cant;
                        cant = command.ExecuteNonQuery();
                        conexion.Close();
                        break;
                    }
                 
                    

                }
                /* si se ha mostrado todos los datos de la base datos y no ha parecido ninguna se guardara como un nuevo Documento incluso si 
                 * la base de datos esta vacia se los datos del Documento se guardara por defecto */
                if (docs == 0)
                {

                    db.Documento.Add(documento);
                    db.SaveChanges();

                }

            }

            /* en el caso de la lista repetido se van actualiza los datos repetidos que estan la base de datos */

            foreach (var item in Repetidos)
            {
                Causante c = new Causante();
                c.NUM_CORRELATIVO = item.NUM_CORRELATIVO;
                c.RUT_CAUSANTE = item.RUT_CAUSANTE;
                c.NOMBRE_CAUSANTE = item.NOMBRE_CAUSANTE;
                c.CODIGO_TIPO_CAUSANTE = item.CODIGO_TIPO_CAUSANTE;
                c.TIPO_CAUSANTE = item.TIPO_CAUSANTE;
                c.RUT_BENEFICIARIO = item.RUT_BENEFICIARIO;
                c.NOMBRE_BENEFICIARIO = item.NOMBRE_BENEFICIARIO;
                c.CODIGO_TIPO_BENEFICIARIO = item.CODIGO_TIPO_BENEFICIARIO;
                c.TIPO_BENEFICIARIO = item.TIPO_BENEFICIARIO;
                c.CODIGO_TIPO_BENEFICIO = item.CODIGO_TIPO_BENEFICIO;
                c.TIPO_BENEFICIO = item.TIPO_BENEFICIO;
                c.RUT_EMPLEADOR = item.RUT_EMPLEADOR;
                c.NOMBRE_EMPLEADOR = item.NOMBRE_EMPLEADOR;
                c.FECHA_RECONOCIMIENTO = item.FECHA_RECONOCIMIENTO.Date;
                c.TRAMO = item.TRAMO;
                c.MONTO_BENEFICIO = item.MONTO_BENEFICIO;
                c.CODIGO_ESTADO_TUPLA = item.CODIGO_ESTADO_TUPLA;
                c.GLOSA_ESTADO_TUPLA = item.GLOSA_ESTADO_TUPLA;
                c.PROMEDIO_RENTA = item.PROMEDIO_RENTA;
                String fecha = c.FECHA_RECONOCIMIENTO.Date.ToString();
                fecha = fecha.Substring(0, 10);
                String[] Fechacompleta = fecha.Split('/');
                fecha = Fechacompleta[2] + "/" + Fechacompleta[1] + "/" + Fechacompleta[0];
                conexion.Close();
                conexion.Open();
                String Cadena = "update Causante set RUT_CAUSANTE=" + "'" + c.RUT_CAUSANTE + "',NOMBRE_CAUSANTE=" + "'" + c.NOMBRE_CAUSANTE + "', CODIGO_TIPO_CAUSANTE=" + c.CODIGO_TIPO_CAUSANTE + ", TIPO_CAUSANTE=" + "'" + c.TIPO_CAUSANTE + "',RUT_BENEFICIARIO=" + "'" + c.RUT_BENEFICIARIO + "', NOMBRE_BENEFICIARIO=" + "'" + c.NOMBRE_BENEFICIARIO + "',CODIGO_TIPO_BENEFICIARIO=" + c.CODIGO_TIPO_BENEFICIARIO + ", TIPO_BENEFICIARIO=" + "'" + c.TIPO_BENEFICIARIO + "', CODIGO_TIPO_BENEFICIO=" + c.CODIGO_TIPO_BENEFICIO + ", TIPO_BENEFICIO=" + "'" + c.TIPO_BENEFICIO + "', RUT_EMPLEADOR=" + "'" + c.RUT_EMPLEADOR + "', FECHA_RECONOCIMIENTO=" + "'" + fecha + "', TRAMO=" + c.TRAMO + ",MONTO_BENEFICIO=" + c.MONTO_BENEFICIO + ",CODIGO_ESTADO_TUPLA=" + c.CODIGO_ESTADO_TUPLA + ", GLOSA_ESTADO_TUPLA= " + "'" + c.GLOSA_ESTADO_TUPLA + "', PROMEDIO_RENTA= " + c.PROMEDIO_RENTA + " where NUM_CORRELATIVO=" + c.NUM_CORRELATIVO + "";
                SqlCommand command = new SqlCommand(Cadena, conexion);
                int cant;
                cant = command.ExecuteNonQuery();
                conexion.Close();
            }

            /* se elimina el documentos que estan en la lista del archivo temporal */
            documentoDAO.EliminarDocumento();

            /* Direcciona a  proceso  */
            return Redirect("../Funcionarios/Proceso");
            
            


        }


        public ActionResult Descargar()
        {
            /* se va crear una lista de la clase Actividad que es una fusion de Causante y funcionario */
            List<Actividad> activos = new List<Actividad>();
            /* se va mostrar los datos que estan los causantes en la base de datos */
            foreach (var items in db.Causante)
            {
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
                /* se va mostrar los datos que estan los funcionario en la base de datos */
                foreach (var item in db.Funcionario)
                {
                    Funcionario F = new Funcionario();
                    F.Id_Funcionario = item.Id_Funcionario;
                    F.Rut = item.Rut;
                    F.Activo = item.Activo;
                    /* si el NUM_CORRELATIVO del Causante es igual a la Id_Funcionario y el Rut del Causante es igual
                     * al Rut del funcionario entonces se guardara los datos del causante y el activo del funcionario   */
                    if (c.NUM_CORRELATIVO == F.Id_Funcionario && c.RUT_CAUSANTE.Equals(F.Rut))
                    {
                        Actividad A = new Actividad();
                        A.NUM_CORRELATIVO = c.NUM_CORRELATIVO;
                        A.RUT_CAUSANTE = c.RUT_CAUSANTE;
                        A.NOMBRE_CAUSANTE = c.NOMBRE_CAUSANTE;
                        A.CODIGO_TIPO_CAUSANTE = c.CODIGO_TIPO_CAUSANTE;
                        A.TIPO_CAUSANTE = c.TIPO_CAUSANTE;
                        A.RUT_BENEFICIARIO = c.RUT_BENEFICIARIO;
                        A.NOMBRE_BENEFICIARIO = c.NOMBRE_BENEFICIARIO;
                        A.CODIGO_TIPO_BENEFICIARIO = c.CODIGO_TIPO_BENEFICIARIO;
                        A.TIPO_BENEFICIARIO = c.TIPO_BENEFICIARIO;
                        A.CODIGO_TIPO_BENEFICIO = c.CODIGO_TIPO_BENEFICIO;
                        A.TIPO_BENEFICIO = c.TIPO_BENEFICIO;
                        A.RUT_EMPLEADOR = c.RUT_EMPLEADOR;
                        A.NOMBRE_EMPLEADOR = c.NOMBRE_EMPLEADOR;
                        A.FECHA_RECONOCIMIENTO = c.FECHA_RECONOCIMIENTO;
                        A.TRAMO = c.TRAMO;
                        A.MONTO_BENEFICIO = c.MONTO_BENEFICIO;
                        A.CODIGO_ESTADO_TUPLA = c.CODIGO_ESTADO_TUPLA;
                        A.GLOSA_ESTADO_TUPLA = c.GLOSA_ESTADO_TUPLA;
                        A.PROMEDIO_RENTA = c.PROMEDIO_RENTA;
                        A.ACTIVO = F.Activo;
                        activos.Add(A);
                    }
                }
            }



            /* se va mostrar la lista de Actividad  en orden por su NUM_CORRELATIVO   */
            return View(activos.OrderBy(a => a.NUM_CORRELATIVO).ToList());



        }

         /*cuando presione el boton de descargar en la vista de Descarga lo que va
          * a hacer es una visualizacion de los datos de la tabla forma ordenada
          * y los datos estaran entregado en un archivo excel y sera guardado en la carpeta Descargas */
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

        public ActionResult Actualizar()
        {
            /* se va crear una lista de la clase Actividad que es una fusion de Causante y funcionario */

            List<Actividad> activos = new List<Actividad>();
            /* se va mostrar los datos que estan los causantes en la base de datos */
            foreach (var items in db.Causante)
            {
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
                /* se va mostrar los datos que estan los funcionario en la base de datos */
                foreach (var item in db.Funcionario)
                {
                    Funcionario F = new Funcionario();
                    F.Id_Funcionario = item.Id_Funcionario;
                    F.Rut = item.Rut;
                    F.Activo = item.Activo;
                    /* si el NUM_CORRELATIVO del Causante es igual a la Id_Funcionario y el Rut del Causante es igual
                    * al Rut del funcionario entonces se guardara los datos del causante y el activo del funcionario   */
                    if (c.NUM_CORRELATIVO == F.Id_Funcionario && c.RUT_CAUSANTE.Equals(F.Rut))
                    {
                        Actividad A = new Actividad();
                        A.NUM_CORRELATIVO = c.NUM_CORRELATIVO;
                        A.RUT_CAUSANTE = c.RUT_CAUSANTE;
                        A.NOMBRE_CAUSANTE = c.NOMBRE_CAUSANTE;
                        A.CODIGO_TIPO_CAUSANTE = c.CODIGO_TIPO_CAUSANTE;
                        A.TIPO_CAUSANTE = c.TIPO_CAUSANTE;
                        A.RUT_BENEFICIARIO = c.RUT_BENEFICIARIO;
                        A.NOMBRE_BENEFICIARIO = c.NOMBRE_BENEFICIARIO;
                        A.CODIGO_TIPO_BENEFICIARIO = c.CODIGO_TIPO_BENEFICIARIO;
                        A.TIPO_BENEFICIARIO = c.TIPO_BENEFICIARIO;
                        A.CODIGO_TIPO_BENEFICIO = c.CODIGO_TIPO_BENEFICIO;
                        A.TIPO_BENEFICIO = c.TIPO_BENEFICIO;
                        A.RUT_EMPLEADOR = c.RUT_EMPLEADOR;
                        A.NOMBRE_EMPLEADOR = c.NOMBRE_EMPLEADOR;
                        A.FECHA_RECONOCIMIENTO = c.FECHA_RECONOCIMIENTO;
                        A.TRAMO = c.TRAMO;
                        A.MONTO_BENEFICIO = c.MONTO_BENEFICIO;
                        A.CODIGO_ESTADO_TUPLA = c.CODIGO_ESTADO_TUPLA;
                        A.GLOSA_ESTADO_TUPLA = c.GLOSA_ESTADO_TUPLA;
                        A.PROMEDIO_RENTA = c.PROMEDIO_RENTA;
                        A.ACTIVO = F.Activo;
                        activos.Add(A);
                    }
                }
            }



            /* se va mostrar la lista de Actividad  en orden por su NUM_CORRELATIVO   */
            return View(activos.OrderBy(a => a.NUM_CORRELATIVO).ToList());




           
        }

        [HttpPost]
        public ActionResult Actualizar( string Actualizar,  Causante causante)
        {
            /* es es un proceso de actualizar los datos que estan en los tramos con el promedio de renta  */

            List<Causante> causantes = new List<Causante>();
            int tramo = 0;
            int monto = 0;
            int Renta = 0;
            int[] tramos = new int[9];
            int[] montos = new int[9];


            /* se va  mostrar los datos del causante  */
            foreach (var item in db.Causante)
            {
                causante.NUM_CORRELATIVO = item.NUM_CORRELATIVO;
                causante.RUT_CAUSANTE = item.RUT_CAUSANTE;
                causante.NOMBRE_CAUSANTE = item.NOMBRE_CAUSANTE;
                causante.CODIGO_TIPO_CAUSANTE = item.CODIGO_TIPO_CAUSANTE;
                causante.TIPO_CAUSANTE = item.TIPO_CAUSANTE;
                causante.RUT_BENEFICIARIO = item.RUT_BENEFICIARIO;
                causante.NOMBRE_BENEFICIARIO = item.NOMBRE_BENEFICIARIO;
                causante.CODIGO_TIPO_BENEFICIARIO = item.CODIGO_TIPO_BENEFICIARIO;
                causante.TIPO_BENEFICIARIO = item.TIPO_BENEFICIARIO;
                causante.CODIGO_TIPO_BENEFICIO = item.CODIGO_TIPO_BENEFICIO;
                causante.TIPO_BENEFICIO = item.TIPO_BENEFICIO;
                causante.RUT_EMPLEADOR = item.RUT_EMPLEADOR;
                causante.NOMBRE_EMPLEADOR = item.NOMBRE_EMPLEADOR;
                causante.FECHA_RECONOCIMIENTO = item.FECHA_RECONOCIMIENTO;
                causante.TRAMO = item.TRAMO;
                causante.CODIGO_ESTADO_TUPLA = item.CODIGO_ESTADO_TUPLA;
                causante.GLOSA_ESTADO_TUPLA = item.GLOSA_ESTADO_TUPLA;
                Renta = item.PROMEDIO_RENTA;
                causante.RUT_CAUSANTE = item.RUT_CAUSANTE;
                tramo = item.TRAMO;
                monto = item.MONTO_BENEFICIO;
                /* si la renta es mayor o igual 342246 entonces el tramo va a  ser 1 y el monto va a ser 13401   */
                if (Renta <= 342246)
                {
                    foreach (var list in db.Asignacion_Familiar)
                    {
                        if (list.Tramo == 1)
                        {
                            tramo = (int)list.Tramo;
                            monto = (int)list.Monto;
                        }

                    }

                }
                /* sino si la renta es mayor a 342246 y meno o igual a 500033 entonces el tramo va a  ser 2 y el monto va a ser 8224   */
                else if (Renta > 342246 && Renta <= 500033)
                {
                    foreach (var list in db.Asignacion_Familiar)
                    {
                        if (list.Tramo == 2)
                        {
                            tramo = (int)list.Tramo;
                            monto = (int)list.Monto;
                        }
                    }

                }
                /* sino si la renta es mayor a 500033 y meno o igual a 779882  entonces el tramo va a  ser 3 y el monto va a ser 2599   */
                else if (Renta > 500033 && Renta <= 779882)
                {
                    foreach (var list in db.Asignacion_Familiar)
                    {
                        if (list.Tramo == 3)
                        {
                            tramo = (int)list.Tramo;
                            monto = (int)list.Monto;
                        }
                    }
                }
                /* sino  entonces el tramo va a  ser 4 y el monto va a ser 0   */
                else
                {
                    foreach (var list in db.Asignacion_Familiar)
                    {
                        if (list.Tramo == 4)
                        {
                            tramo = (int)list.Tramo;
                            monto = (int)list.Monto;
                        }
                    }

                }

                /* se va a crear una conexion y que va a editar el monto y el tramo cuando 
                 * el numero correlativo es igual al numero correlativo del Causante  */
                conexion.Close();
                conexion.Open();
                String Cadena = "update Causante set MONTO_BENEFICIO = " + monto + ", TRAMO=" + tramo + " where NUM_CORRELATIVO =" + causante.NUM_CORRELATIVO + "";


                SqlCommand command = new SqlCommand(Cadena, conexion);
                int cant;
                cant = command.ExecuteNonQuery();
                conexion.Close();
            }
            /* Redirecciona al index */
            return Redirect("Index");
        }

        public ActionResult Historial()
        {
            /* se va mostrar una lista con los datos de los documentos en orden que es el ultimo que se subio  */
            return View(db.Documento.OrderByDescending( d => d.Fecha).ToList());
        }
        /*el proceso GetFileTypeByExtension es un subproceso de para saber que tipo de 
         * documento tomara los caracteres del documento y sera indetificado por un switch qu al final va a retonar a un nombre al documento  */
        private string GetFileTypeByExtension(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".xlsx":
                case ".xls":
                case ".csv": 
                    return "Microsoft Excel Document";
             
                default:
                    return "Unknown";
            }
        }
        /* cuando el presione el boton de descargar que esta el historial
         * va a ser un subproceso que va descargar segun su nombre del archivo y su contenido del archivo  */
        public FileResult Download( String CsvName)
        {
            string fileName = Path.GetFileName(CsvName);
            string fullPath = Path.Combine(Server.MapPath("~/Views/Causantes/descargas/"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);


        }





    }
}

