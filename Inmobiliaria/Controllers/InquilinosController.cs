using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Controllers
{
    public class InquilinosController : Controller
    {
        Inquilino inq;
        private readonly DataContext contexto;
        
        public InquilinosController(DataContext contexto)
        {
            this.contexto = contexto;
        }
        [Authorize(Policy = "Administrador")]
        // GET: Inquilinos
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = "Administrador")]
        // GET: Inquilinos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize(Policy = "Administrador")]
        // GET: Inquilinos/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Policy = "Administrador")]
        // POST: Inquilinos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino inquilino)
        {
            try
            {

                if (inquilino.Dni != 0 && inquilino.Nombre != null && inquilino.Apellido != null && inquilino.Domicilio!= null && inquilino.Telefono != 0 && inquilino.Email != null && inquilino.Lugar_Trabajo != null)
                {
                   
                    inq = new Inquilino
                    {
                        Dni = inquilino.Dni,
                        Nombre = inquilino.Nombre,
                        Apellido = inquilino.Apellido,
                        Email = inquilino.Email,
                        Telefono = inquilino.Telefono,
                       Lugar_Trabajo = inquilino.Lugar_Trabajo,
                        Domicilio = inquilino.Domicilio
                    };
                   Inquilino InquilinoVerificar = contexto.Inquilino.FirstOrDefault(x => x.Dni == inq.Dni);
                    if (InquilinoVerificar != null)
                    {
                        ViewBag.registrado = "ya existe un Propietario con ese email o dni";
                        return View();
                    }
                    contexto.Inquilino.Add(inq);
                    contexto.SaveChanges();
                    int i = inq.Id;
                    if (i != 0)
                    {

                        return RedirectToAction("ListaInquilinos");
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
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View();
            }
        }



        [Authorize(Policy = "Administrador")]
        // GET: Inquilinos/Edit/5
        public ActionResult Edit(int id)
        {
            Inquilino inquilino= contexto.Inquilino.First(x => x.Id == id);
            return View(inquilino);
            
        }
        [Authorize(Policy = "Administrador")]
        // POST: Inquilinos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inquilino inquilino)
        {
            try
            {
                if (inquilino.Dni != 0 && inquilino.Nombre != null && inquilino.Apellido != null && inquilino.Domicilio != null && inquilino.Telefono != 0 && inquilino.Email != null && inquilino.Lugar_Trabajo != null)
                {

                    inq = new Inquilino
                    {   Id = inquilino.Id,
                        Dni = inquilino.Dni,
                        Nombre = inquilino.Nombre,
                        Apellido = inquilino.Apellido,
                        Email = inquilino.Email,
                        Telefono = inquilino.Telefono,
                        Lugar_Trabajo = inquilino.Lugar_Trabajo,
                        Domicilio = inquilino.Domicilio
                    };
                    contexto.Inquilino.Update(inq);
                    contexto.SaveChanges();
                    return RedirectToAction("ListaInquilinos");
                }
                else
                {
                    ViewBag.Error = "ingrese Todos los datos";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex;
                return View();
            }
        }


        [Authorize(Policy = "Administrador")]
        // GET: Inquilinos/Delete/5
        public ActionResult Delete(int id)
        {
           Inquilino inquilino = contexto.Inquilino.First(x => x.Id == id);
            return View(inquilino);
        }



        [Authorize(Policy = "Administrador")]
        // POST: Inquilinos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Inquilino inquilino)
        {
            try
            {
                Inquilino inquili = contexto.Inquilino.First(x => x.Id == inquilino.Id);

                contexto.Inquilino.Remove(inquili);
                contexto.SaveChanges();
                return RedirectToAction("ListaInquilinos");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex;
                return View();
            }
        }

        [Authorize(Policy = "Administrador")]
        public ActionResult ListaInquilinos()
        {
            IEnumerable<Inquilino> inq = contexto.Inquilino.ToList();
            return View(inq);
        }

    }
}