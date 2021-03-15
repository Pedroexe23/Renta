using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentaWEB2._0.Models;
using RentaWEB2._0.Models.Dao;

namespace RentaWEB2._0.Controllers
{
    public class FuncionariosController : Controller
    {
        private Municipalidad db = new Municipalidad();
        static CausantesDAO causanteDAO = new CausantesDAO();
        private SqlConnection conexion = new SqlConnection("data source=TECNO-PRACTI;initial catalog=Municipalidad;integrated security=True;");
        // GET: Funcionarios
        public ActionResult Index()
        {
            return View(db.Funcionario.ToList());
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionario.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Funcionario,Activo,Rut,Apellidos,Nombres,Sexo,EstadoCivil,Fec_nacimiento,Direccion,RentaPromedio")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                db.Funcionario.Add(funcionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionario.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Funcionario,Activo,Rut,Apellidos,Nombres,Sexo,EstadoCivil,Fec_nacimiento,Direccion,RentaPromedio")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionario.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Funcionario funcionario = db.Funcionario.Find(id);
            db.Funcionario.Remove(funcionario);
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

       
        public ActionResult Proceso()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Proceso(String id)
        {
           
            // se crea dos Listas distintas una que esta guardado en el CausanteDAO y otro una lista con la clase funcionario 
            List<Causante> listaCausante = causanteDAO.GetCausantes();
            List<Funcionario> funcionarios = new List<Funcionario>();
            List<Funcionario> repetidos = new List<Funcionario>();
            List<Funcionario> norepetidos = new List<Funcionario>();
            List<Funcionario> nuevo = new List<Funcionario>();
            /* se crea 3 variables una para guardar a los nuevos funcionarios otro
             * para activar funcionarios y otro para desactivar los funcionarios */
            int on = 1;
            int c = 0;
            int b;
            foreach (var item in listaCausante)
            {
                /* se crea las variables  strings para guardar los nombres y apellidos,  el nombre 1 es el nombre principal,
                 * el  nombre2 es el nombre secundarios, el apellido1 es  el apellido paterno y el apellido 2 es para el apellido materno
                 * el arreglo subs es para separar el nombre completo por espacio y las variables que seran almacenadas en el objectos Funcionario son el Num.Correlativo
                 * como una Id para el funcionario el nombre como dije en lo anterior para sera separados para el nombre y apellido el Promedio_Renta en la RentaPromedio el activo
                 * como numero preterminado sera 1 */
                String nombres = " ", apellidos = " ", nombre1 = " ", nombre2 = " ", apellido1 = " ", apellido2 = " ", subapellido1 = " ", subapellido2 = " ", subapellido3 = " ";
                String[] subs = item.NOMBRE_CAUSANTE.Split(' ');
                short id_Funcionario = (short)item.NUM_CORRELATIVO;
                String Rut = item.RUT_CAUSANTE;
                int Renta = (int)item.PROMEDIO_RENTA;
                short Activo = 1;

                /* si los apellidos son "CARTER DE LA PAZ" se divide los apellidos en 4.
                 * 3 de ellos son subsapellidos que son: DE LA PAZ que al final son un apellido unido con el segundo apellido : CARTER que al final son el resultado es:
                 * "DE LA PAZ CARTER"*/
                if (item.NOMBRE_CAUSANTE.Substring(0, 16).Equals("CARTER DE LA PAZ"))
                {
                    subapellido1 = subs[3];
                    subapellido2 = subs[2];
                    subapellido3 = subs[1];
                    apellido1 = subapellido3 + " " + subapellido2 + " " + subapellido1;
                    apellido2 = subs[0];
                    apellidos = apellido1 + " " + apellido2;
                    nombre1 = subs[5];
                    nombre2 = subs[4];
                    nombres = nombre1 + " " + nombre2;


                }
                /* si no es el Caso y los apellidos son "GONZALEZ ACEVEDO" y terminan en un solo nombre se  inviterte los apellidos y separa el nombre  */
                else if (item.NOMBRE_CAUSANTE.Substring(0, 16).Equals("GONZALEZ ACEVEDO"))
                {
                    apellido1 = subs[1];
                    apellido2 = subs[0];
                    apellidos = apellido1 + " " + apellido2;

                    nombre1 = subs[2];
                    nombres = nombre1;


                }
                /* si no es ninguno de los casos por defecto de dividira en 4  2 nombre y 2 apellidos*/
                else
                {
                    apellido1 = subs[1];
                    apellido2 = subs[0];
                    apellidos = apellido1 + " " + apellido2;
                    nombre1 = subs[3];
                    nombre2 = subs[2];
                    nombres = nombre1 + " " + nombre2;

                }
                /* los datos de los causantes como Rut Nombre promedio_Renta estaran
                 * en los funcionarios como Rut, nombre, apelldios y Renta_promedio y los deams datos en 0 o nulos */
                Funcionario funcionario = new Funcionario();
                funcionario.Id_Funcionario = id_Funcionario;
                funcionario.Rut = Rut;
                funcionario.Nombres = nombres;
                funcionario.Apellidos = apellidos;
                funcionario.RentaPromedio = Renta;
                funcionario.Activo = Activo;
                funcionario.Sexo = 0;
                funcionario.EstadoCivil = 0;
                funcionario.Fec_nacimiento = null;
                funcionario.Direccion = "Null";
                /* Los objectos seran almacenados en una lista de objectos de Funcionarios  */
                funcionarios.Add(funcionario);

            }
            /* se va a mostrar los datos almacenados en la base de datos en el objecto Funcionario y counts se va a 0 y el objecto almacenado  */

            foreach (var ilem in funcionarios)
            {
                b = 0;
                Funcionario funcionario = new Funcionario();
                funcionario.Id_Funcionario = ilem.Id_Funcionario;
                funcionario.Rut = ilem.Rut;
                funcionario.Nombres = ilem.Nombres;
                funcionario.Apellidos = ilem.Apellidos;
                funcionario.RentaPromedio = ilem.RentaPromedio;
                funcionario.Activo = ilem.Activo;
                funcionario.Sexo = ilem.Sexo;
                funcionario.EstadoCivil = ilem.EstadoCivil;
                funcionario.Fec_nacimiento = ilem.Fec_nacimiento;
                funcionario.Direccion = ilem.Direccion;
                /* se va a leer los datos del objecto Funcionario solamente su id y su rut */
                foreach (var items in db.Funcionario)
                {
                      
                    Funcionario funcionario1 = new Funcionario();
                    funcionario1.Id_Funcionario = items.Id_Funcionario;
                    funcionario1.Rut = items.Rut;
                    /*si la id del funcionario de la lista es igual a la id del funcionario de la base de datos y
                     * el rut del funcionario de la lista es igual al rut del funcionario enttonces los datos de la lista
                     * se guardara a otra lista llamada repetidos con los mismo parametros del objectos Funcionario    */
                    if (funcionario1.Id_Funcionario == ilem.Id_Funcionario && funcionario1.Rut.Equals(ilem.Rut))
                    {
                        b = 0;
                        c = c + 1;
                        repetidos.Add(funcionario);
                    }
                    /*si no se guardara en otra lista con el nombre de los norepetidos con los datos del objeto funcionario de la base de datos 
                     * y la variable b y c se va a acumular    */
                    else
                    {
                        on= 0;
                        c = c + 1;
                        b = b + 1;
                        norepetidos.Add(funcionario1);
                    }
                    /*si la variable b es igual al conteo del foreach de la base de datos del funcionario entonces este dato es un nuevo funcionario 
                     * y se guarda en la  lista de nuevo */
                    if (b==db.Funcionario.Count())
                    {
                        
                        nuevo.Add(funcionario);
                    }

                    

                }
            }
            /*Se crea  un foreach y se va a leer  los datos de la lista del norepetido y  va modificar del activo va a ser 0
             * como si fuera una nueva hoja de activos mayormente a todos los funcionarios van a ser 0 al inicio  */
            foreach (var item in norepetidos)
            {
                Funcionario funcionario = new Funcionario();
                funcionario.Id_Funcionario = item.Id_Funcionario;
                conexion.Close();
                conexion.Open();
                String Cadena = "update Funcionario set Activo =" + 0 + "where Id_Funcionario =" + funcionario.Id_Funcionario + "";
                SqlCommand command = new SqlCommand(Cadena, conexion);
                int cant;
                cant = command.ExecuteNonQuery();
                conexion.Close();
            }
            /* se crea  un foreach para leer los datos de la  lista de los repetidos  va a cambiar su activo a  1 en la base de datos 
             * entonces solo va a pasar a los funcionarios que estan repetidos  */
            foreach (var item in repetidos)
            {
                Funcionario funcionario = new Funcionario();
                funcionario.Id_Funcionario = item.Id_Funcionario;
                conexion.Close();
                conexion.Open();
                String Cadena = "update Funcionario set Activo =" + 1 + "where Id_Funcionario =" + funcionario.Id_Funcionario + "";
                SqlCommand command = new SqlCommand(Cadena, conexion);
                int cant;
                cant = command.ExecuteNonQuery();
                conexion.Close();
            }
            /* se crea un foreach para leer a los nuevos  y la variable on va a ser  igual a 1  */
            foreach (var item in nuevo)
            {
                on = 1;
            }


            /*si la varible on es igual a 1  entonces  inicia la siguiente secuencia*/
            if (on==1)
            {
                /*en caso que la base de datos esta vacia, la Variable c estara en 0 
                     *  y la lista de objectos funcionarios se guardara en la Base de datos por DEFAULT */
                if (c == 0)
                {
                db.Funcionario.AddRange(funcionarios);

                }
                /* sino entonces los datos de  la lista nuevo se añade a la base de datos  */
                else
                {
                db.Funcionario.AddRange(nuevo);
                }
                /*se elimina  los datos del archivo temporal con su lista  y  se guarda  los datos de la base de datos ademas
                 * se redireciona al proceso de descargar */
                causanteDAO.EliminarCausantes();
                db.SaveChanges();
                return Redirect("../Causantes/Descargar");
            }
            /*si on esta en 0  entonces se elimina  los datos del archivo temporal con su lista y se redireciona al proceso de descargar */
            else
            {
                causanteDAO.EliminarCausantes();
                return Redirect("../Causantes/Descargar");
            }
           
        }      

    }

 }
