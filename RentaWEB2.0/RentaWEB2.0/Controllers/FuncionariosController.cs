using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentaWEB2._0.Models;

namespace RentaWEB2._0.Controllers
{
    public class FuncionariosController : Controller
    {
        private Municipalidad db = new Municipalidad();

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
            foreach (var item in db.Causante)
            {
                
                String nombres=" ", apellidos=" ",nombre1=" ",nombre2=" ",apellido1=" ",apellido2=" ",subapellido1=" ", subapellido2=" ", subapellido3=" ";
                String [] subs = item.NOMBRE_CAUSANTE.Split(' ');
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
                        apellido1 = subapellido1+" "+ subapellido2+" "+subapellido3;
                        apellidos = apellido1 +" "+ apellido2;
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
                        nombres = nombre1+" "+ nombre2;
                    }
                }
                
                Funcionario funcionario = new Funcionario();
                funcionario.Id_Funcionario = id_Funcionario;
                funcionario.Rut = Rut;
                funcionario.Nombres = nombres;
                funcionario.Apellidos = apellidos;
               
                db.Funcionario.Add();
                db.SaveChanges();



            }

            return Redirect("../Causantes/Descargar");
        }

    }
}
