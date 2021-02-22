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
            /* se crea 3 variables una para guardar a los nuevos funcionarios otro
             * para activar funcionarios y otro para desactivar los funcionarios */ 
            int counts = 0;
            int count = 0;
            int c = 0;
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
            foreach (var items in db.Funcionario)
            {
                counts = 0;
                Funcionario funcionario1 = new Funcionario();
                funcionario1.Id_Funcionario = items.Id_Funcionario;
                funcionario1.Rut = items.Rut;
                /* se va a mostrar los datos almacenados en la lista de objecto Funcionario almacenados  */
                foreach (var ilem in funcionarios)
                {
                    
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
                    /* si id funcionario de la base de datos es igual a la lista y  el Rut de la Base de datos es igual al rut de la lista
                     * entonces counts se queda en cero y se edita el activo a 1 por comando SQL y descansa   */
                    if (funcionario1.Id_Funcionario == ilem.Id_Funcionario && funcionario1.Rut.Equals(ilem.Rut))
                    {
                        counts = counts+(counts*-1);
                        funcionario1.Activo = 1;
                        conexion.Close();
                        conexion.Open();
                        String Cadena = "update Funcionario set Activo =" + funcionario1.Activo + "where Id_Funcionario =" + funcionario1.Id_Funcionario + "";
                        SqlCommand command = new SqlCommand(Cadena, conexion);
                        int cant;
                        cant = command.ExecuteNonQuery();
                        conexion.Close();
                        c = c + 1;
                        break;

                    }
                    /* si no count se eleva a 1 y counts disminuye a -1 */
                    else
                    {
                        counts = 1 + counts;
                       count = 1 + count;
                    }
                    /* si count es mayor a cero entonces el activo del funcionario sera 0  por comando  SQL  */
                    if (count > 0)
                    {
                        c = c + 1;
                        funcionario1.Activo = 0;
                        conexion.Close();
                        conexion.Open();
                        String Cadena = "update Funcionario set Activo =" + funcionario1.Activo + "where Id_Funcionario =" + funcionario1.Id_Funcionario + "";
                        SqlCommand command = new SqlCommand(Cadena, conexion);
                        int cant;
                        cant = command.ExecuteNonQuery();
                        conexion.Close();

                    }
                    /* si counts es menor a cero entonces el objecto funcionario sera almacenado en la base de datos  */
                    if (counts > 2)
                    {
                        counts = 0;
                        db.Funcionario.Add(funcionario);
                        
                    }
                    
                }
                db.SaveChanges();

            }
            /*en caso que la base de datos esta vacia, la Variable c estara en 0 
             *  y la lista de objectos funcionarios se guardara en la Base de datos por DEFAULT */
            if (c == 0)
            {
                db.Funcionario.AddRange(funcionarios);
                db.SaveChanges();
            }


            return Redirect("../Causantes/Descargar");

        }



    }

 }
