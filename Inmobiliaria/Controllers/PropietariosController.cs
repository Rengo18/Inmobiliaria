using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        
        [Authorize(Policy = "Administrador")]
        // GET: Propietarios
        public ActionResult Index()
        {
          
            return View();
        }
        [Authorize(Policy = "Administrador")]
        public ActionResult ListaPropietarios()
        {
            IEnumerable<Propietario> pro = contexto.Propietario.ToList();
            return View(pro);
        }



        [HttpGet]
        // GET: Propietarios/Create
        [Authorize(Policy = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Policy = "Administrador")]
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
                        
                        return RedirectToAction("ListaPropietarios", "Propietarios");
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
            Propietario propietario = contexto.Propietario.First(x => x.Id == id);
            return View(propietario);
        }

        // POST: Propietarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( PropietarioView propietario)
        {
            try
            {
                if (propietario.Dni != 0 && propietario.Nombre != null && propietario.Apellido != null && propietario.Domicilio != null && propietario.Telefono != 0 && propietario.Email != null && propietario.Clave != null)
                {
                    propietario.Clave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                      password: propietario.Clave,
                      salt: System.Text.Encoding.ASCII.GetBytes("SALADA"),
                      prf: KeyDerivationPrf.HMACSHA1,
                      iterationCount: 1000,
                      numBytesRequested: 256 / 8));
                    p = new Propietario
                    {   Id = propietario.Id,
                        Dni = propietario.Dni,
                        Nombre = propietario.Nombre,
                        Apellido = propietario.Apellido,
                        Email = propietario.Email,
                        Telefono = propietario.Telefono,
                        Clave = propietario.Clave,
                        Domicilio = propietario.Domicilio
                    };
                    contexto.Propietario.Update(p);
                    contexto.SaveChanges();
                    TempData["Editado"] = "Usuario Modificado";
                    return RedirectToAction("ListaPropietarios");
                }else
                {
                    ViewBag.Error = "ingrese Todos los datos";
                    return View();
                }
               
               
            }
            catch(Exception ex)
            {
                ViewBag.exception = ex;
                return View();
            }
        }

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int id)
        {
            Propietario propietario=contexto.Propietario.First(x => x.Id == id);
            return View(propietario);
        }

        // POST: Propietarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PropietarioView propietario)
        {
            try
            {
                Propietario p = contexto.Propietario.First(x => x.Id == propietario.Id);
                
                contexto.Propietario.Remove(p);
                contexto.SaveChanges();
                return RedirectToAction("ListaPropietarios");
            }
            catch(Exception ex)
            {
                ViewBag.Mensaje = ex;
                return View();
            }
        }


        public ActionResult BuscarPropietario()
        {
            return View();
        }
    }
}