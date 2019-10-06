using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Controllers
{
    public class InmueblesController : Controller
    {

        private readonly DataContext contexto;
        public InmueblesController(DataContext contexto)
        {
            this.contexto = contexto;
        }

        public ActionResult ListaInmueble()
        {
            var inmueble = contexto.Inmueble.Include(c => c.Propietario);
            return View(inmueble.ToList());
        }



        // GET: Inmuebles
        public ActionResult Index()
        {
            return View();
        }

        // GET: Inmuebles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inmuebles/Create
        [Authorize(Policy = "Administrador")]
        public ActionResult Create(int id)
        {
            Propietario pro = contexto.Propietario.First(x => x.Id == id);
            ViewBag.PropietarioId = pro.Id;
            ViewBag.NombrePropietario = pro.Nombre + " " + pro.Apellido;
            return View();
        }
        [Authorize(Policy = "Administrador")]
        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            try
            {
                ViewBag.PropietarioId = inmueble.PropietarioId;
                Propietario pro = contexto.Propietario.First(x => x.Id == inmueble.PropietarioId);
                ViewBag.NombrePropietario = pro.Nombre + " " + pro.Apellido;
                Inmueble inm = new Inmueble
                {
                    Direccion = inmueble.Direccion,
                    Uso = inmueble.Uso,
                    Tipo = inmueble.Tipo,
                    Cantidad_Habitantes = inmueble.Cantidad_Habitantes,
                    Precio = inmueble.Precio,
                    PropietarioId = inmueble.PropietarioId,
                    Estado = inmueble.Estado
                };


                if(inmueble.Precio==0||inmueble.Direccion== null || inmueble.Cantidad_Habitantes == 0)
                {
                    ViewBag.error = "complete todos los campos";
                    return View();
                }

                contexto.Inmueble.Add(inm);
                contexto.SaveChanges();


                return RedirectToAction("ListaInmueble");
            }
            catch(Exception ex)
            {
                ViewBag.error = ex;
                return View();
            }
        }
        [Authorize(Policy = "Administrador")]
        // GET: Inmuebles/Edit/5
        public ActionResult Edit(int id)
        {
            Inmueble inm = contexto.Inmueble.First(x => x.Id == id);
            Propietario pro = contexto.Propietario.First(p => p.Id == inm.PropietarioId);

            ViewBag.PropietarioId = pro.Id;
            ViewBag.NombrePropietario = pro.Nombre + " " + pro.Apellido;
            return View(inm);
        }
        [Authorize(Policy = "Administrador")]
        // POST: Inmuebles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inmueble inmueble)
        {
            try
            {

                if (inmueble.Precio == 0 || inmueble.Direccion == null || inmueble.Cantidad_Habitantes == 0)
                {
                   
                    Propietario pro = contexto.Propietario.First(p => p.Id == inmueble.PropietarioId);

                    ViewBag.PropietarioId = pro.Id;
                    ViewBag.NombrePropietario = pro.Nombre + " " + pro.Apellido;
                    ViewBag.error = "complete todos los campos";
                    return View(inmueble);
                }
                contexto.Inmueble.Update(inmueble);
                contexto.SaveChanges();
                return RedirectToAction("ListaInmueble");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
                return View();
            }
        }
        [Authorize(Policy = "Administrador")]

        // GET: Inmuebles/Delete/5
        public ActionResult Delete(int id)
        {
            Inmueble inm = contexto.Inmueble.First(x => x.Id == id);
            Propietario pro = contexto.Propietario.First(p => p.Id == inm.PropietarioId);

          
            ViewBag.NombrePropietario = pro.Nombre + " " + pro.Apellido;
            return View(inm);
        }

        [Authorize(Policy = "Administrador")]
        // POST: Inmuebles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Inmueble inmueble)
        {
            try
            {
                contexto.Inmueble.Remove(inmueble);
                contexto.SaveChanges();
                return RedirectToAction("ListaInmueble");
            }
            catch(Exception ex)
            {
                ViewBag.error = ex;
                return View();
            }
        }
    }
}