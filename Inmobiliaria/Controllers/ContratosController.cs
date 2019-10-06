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
    public class ContratosController : Controller
    {


        private readonly DataContext contexto;
        public ContratosController(DataContext contexto)
        {
            this.contexto = contexto;
        }

        [Authorize(Policy = "Administrador")]
        // GET: Contratos
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = "Administrador")]
        public ActionResult ListaContratos()
        {

            var Contratos = contexto.Contrato
                .Include(x => x.Inquilino)
                .Include(x => x.Inmueble.Propietario);
            return View(Contratos.ToList());
        }

        [Authorize(Policy = "Administrador")]
        // GET: Contratos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize(Policy = "Administrador")]
        // GET: Contratos/Create
        public ActionResult Create(int id)
        {
            Inmueble inmueble = contexto.Inmueble.Include(x=> x.Propietario).First(i=> i.Id==id);
            
            ViewBag.InmuebleId = inmueble.Id;
            ViewBag.NombreInmueble = inmueble.Direccion;
            //ViewBag.NombrePropietario = inmueble.Propietario.Nombre + " " + inmueble.Propietario.Apellido;
            return View();
        }
        [Authorize(Policy = "Administrador")]
        // POST: Contratos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato contrato)
        {
            try
            {
                Inmueble inmueble = contexto.Inmueble.First(i => i.Id == contrato.InmuebleId);

                ViewBag.InmuebleId = inmueble.Id;
                ViewBag.NombreInmueble = inmueble.Direccion;

                if (contrato.InquilinoId == 0 ||contrato.FechaInicio==null || contrato.FechaCierre==null||contrato.Monto == 0)
                {
                    ViewBag.error = "cargue todo los datos";
                    return View();
                }
                Inquilino inquilino = contexto.Inquilino.FirstOrDefault(x => x.Dni == contrato.InquilinoId);
                if (inquilino == null)
                {
                    ViewBag.error = "Inquilino no registrado";
                    return View();
                }
                Contrato con = new Contrato
                {
                    FechaInicio = contrato.FechaInicio,
                    FechaCierre = contrato.FechaCierre,
                    Monto = contrato.Monto,
                    InmuebleId = contrato.InmuebleId,
                    InquilinoId = inquilino.Id,
                };
               

                contexto.Contrato.Add(con);
                contexto.SaveChanges();
                
                return RedirectToAction("ListaContratos");
            }
            catch(Exception ex)
            {
                ViewBag.error = ex;
                return View();
            }
        }
        [Authorize(Policy = "Administrador")]
        // GET: Contratos/Edit/5
        public ActionResult Edit(int id)
        {
            Contrato contrato = contexto.Contrato.First(x => x.Id == id);
            Inmueble inmueble = contexto.Inmueble.Include(x => x.Propietario).First(p => p.Id == contrato.InmuebleId);
            ViewBag.NombreInmueble = inmueble.Direccion;
            ViewBag.PropietarioNombre = inmueble.Propietario.Nombre + " " + inmueble.Propietario.Apellido;
            ViewBag.InmuebleId = inmueble.Id;
          
            ViewBag.FechaInicio = contrato.FechaInicio.ToString("yyyy-MM-dd");
            ViewBag.FechaCierre = contrato.FechaCierre.ToString("yyyy-MM-dd");
            Inquilino inquilino = contexto.Inquilino.First(x => x.Id == contrato.InquilinoId);
            ViewBag.InquilinoNombre = inquilino.Nombre + "" + inquilino.Apellido;
            ViewBag.InquilinoId = inquilino.Id;
            return View(contrato);
        }


        [Authorize(Policy = "Administrador")]
        // POST: Contratos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contrato contrato)
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
        [Authorize(Policy = "Administrador")]
        // GET: Contratos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        [Authorize(Policy = "Administrador")]
        // POST: Contratos/Delete/5
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