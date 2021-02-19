using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pruebas.Models.Tecnologia;
using Pruebas.Models.Tecnologia.DAO;

namespace Pruebas.Controllers.Tecnologia
{
    public class FuncionariosController : Controller
    {
        static CausantesDAO causanteDAO = new CausantesDAO();
        private Municipalidad db = new Municipalidad();
        private SqlConnection conexion = new SqlConnection("data source=TECNO-PRACTI;initial catalog=Municipalidad;integrated security=True;");
        // GET: Funcionarios
        public ActionResult Index()
        {
            var funcionarios = db.Funcionarios.Include(f => f.Causante);
            return View(funcionarios.ToList());
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            ViewBag.Id_Funcionario = new SelectList(db.Causantes, "NUM_CORRELATIVO", "RUT_CAUSANTE");
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
                db.Funcionarios.Add(funcionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Funcionario = new SelectList(db.Causantes, "NUM_CORRELATIVO", "RUT_CAUSANTE", funcionario.Id_Funcionario);
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Funcionario = new SelectList(db.Causantes, "NUM_CORRELATIVO", "RUT_CAUSANTE", funcionario.Id_Funcionario);
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
            ViewBag.Id_Funcionario = new SelectList(db.Causantes, "NUM_CORRELATIVO", "RUT_CAUSANTE", funcionario.Id_Funcionario);
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
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
            Funcionario funcionario = db.Funcionarios.Find(id);
            db.Funcionarios.Remove(funcionario);
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
            List<Causante> listaCausante =causanteDAO.GetCausantes() ;
            List<Funcionario> funcionarios = new List<Funcionario>();
            foreach (var items in db.Funcionarios)
            {
                int counts = 0;
                int count = 0;
                Funcionario funcionario1 = new Funcionario();
                funcionario1.Id_Funcionario = items.Id_Funcionario;

                foreach (var item in listaCausante)
                {

                    String nombres = " ", apellidos = " ", nombre1 = " ", nombre2 = " ", apellido1 = " ", apellido2 = " ", subapellido1 = " ", subapellido2 = " ", subapellido3 = " ";
                    String[] subs = item.NOMBRE_CAUSANTE.Split(' ');
                    short id_Funcionario = (short)item.NUM_CORRELATIVO;
                    String Rut = item.RUT_CAUSANTE;
                    int Renta = (int)item.PROMEDIO_RENTA;
                    short Activo = 1;


                    if (item.NOMBRE_CAUSANTE.Substring(0, 16).Equals("CARTER DE LA PAZ"))
                    {

                        apellido1 = subapellido3 + " " + subapellido2 + " " + subapellido1;
                        apellido2 = subs[0];
                        apellidos = apellido1 + " " + apellido2;
                        nombre1 = subs[5];
                        nombre2 = subs[4];
                        nombres = nombre1 + " " + nombre2;
                        subapellido1 = subs[3];
                        subapellido2 = subs[2];
                        subapellido3 = subs[1];

                    }
                    else if (item.NOMBRE_CAUSANTE.Substring(0, 16).Equals("GONZALEZ ACEVEDO"))
                    {
                        apellido1 = subs[1];
                        apellido2 = subs[0];
                        apellidos = apellido1 + " " + apellido2;
                        nombre1 = subs[2];
                        nombres = nombre1;

                    }

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
                    if (items.Id_Funcionario == id_Funcionario && items.Rut.Equals(Rut))
                    {
                        counts = 1;
                        funcionario1.Activo = 1;
                        conexion.Close();
                        conexion.Open();
                        String Cadena = "update Funcionario set Activo =" + funcionario1.Activo + "where Id_Funcionario =" + funcionario1.Id_Funcionario + "";
                        SqlCommand command = new SqlCommand(Cadena, conexion);
                        int cant;
                        cant = command.ExecuteNonQuery();
                        conexion.Close();
                        break;

                    }
                    else
                    {

                        count = 1 + count;
                    }
                    if (count > 0)
                    {

                        funcionario1.Activo = 0;
                        conexion.Close();
                        conexion.Open();
                        String Cadena = "update Funcionario set Activo =" + funcionario1.Activo + "where Id_Funcionario =" + funcionario1.Id_Funcionario + "";
                        SqlCommand command = new SqlCommand(Cadena, conexion);
                        int cant;
                        cant = command.ExecuteNonQuery();
                        conexion.Close();

                    }

                    if (counts == 0)
                    {
                        db.Funcionarios.Add(funcionario);
                        db.SaveChanges();
                    }

                }
               
            }
            // db.Funcionarios.AddRange(funcionarios);
            // db.SaveChanges();

            return Redirect("../Causantes/Descargar");

        }

    }
}
