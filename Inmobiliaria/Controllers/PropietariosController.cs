using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Controllers
{
    public class PropietariosController : Controller
    {
        Propietario p;
        private readonly DataContext contexto;
        public PropietariosController(DataContext contexto)
        {
             this.contexto = contexto;
        }
        // GET: Propietarios
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Propietarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpGet]
        // GET: Propietarios/Create
        [Route("Propietarios/Create", Name = "CreatePropietario")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PropietarioView propietarioView)
        {
            try
            {
                
                if (propietarioView.Dni!=0 && propietarioView.Nombre != null && propietarioView.Apellido != null && propietarioView.Domicilio != null && propietarioView.Telefono!= 0 && propietarioView.Email != null && propietarioView.Clave != null)
                {
                    propietarioView.Clave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: propietarioView.Clave,
                        salt: System.Text.Encoding.ASCII.GetBytes("SALADA"),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));
                    p = new Propietario {
                        Dni = propietarioView.Dni,
                        Nombre = propietarioView.Nombre,
                        Apellido = propietarioView.Apellido,
                        Email = propietarioView.Email,
                        Telefono= propietarioView.Telefono,
                        Clave = propietarioView.Clave,
                        Domicilio = propietarioView.Domicilio
                    };
                    Propietario propietarioVerificar = contexto.Propietario.FirstOrDefault(x => x.Dni == p.Dni || x.Email == p.Email);
                    if (propietarioVerificar != null)
                    {
                        ViewBag.registrado = "ya existe un Propietario con ese email o dni";
                        return View();
                    }
                    contexto.Propietario.Add(p);
                    contexto.SaveChanges();
                    int i = p.Id;
                    if (i!=0)
                    {
                        TempData["mensaje"]= "Gracias por registrarte";
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        ViewBag.registrado = "error al insertar";
                        return View(); 
                    }
                    
                }
                else
                {
                    ViewBag.registrado = "Complete todos campos";
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View();
            }
        }

        // GET: Propietarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Propietarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Propietarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}