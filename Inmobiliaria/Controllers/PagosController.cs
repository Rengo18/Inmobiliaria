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
  
    public class PagosController : Controller
    {
        private readonly DataContext contexto;
        public PagosController(DataContext contexto)
        {
            this.contexto = contexto;
        }

        [Authorize(Policy = "Administrador")]
        // GET: Pagos
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Policy = "Administrador")]
       
        public ActionResult ListaPagos()
        {
            var pago=contexto.Pago.Include(x => x.Contrato.Inquilino).Include(x => x.Contrato.Inmueble);
            return View(pago);
        }

        [Authorize(Policy = "Administrador")]

        public ActionResult ListaPagosPorContrato(int id)
        {
            Contrato contrato = contexto.Contrato.Include(x => x.Inquilino).First(x=>x.Id==id);

            @ViewBag.Inquilino = contrato.Inquilino.Nombre + " " + contrato.Inquilino.Apellido;

            var pago = contexto.Pago.Include(x => x.Contrato.Inquilino).Include(x=> x.Contrato.Inmueble).Where(x=>x.ContratoId ==id);
           

            return View(pago);
        }


    
        // GET: Pagos/Create
        public ActionResult Create(int id)
        {
            Contrato contrato = contexto.Contrato.Include(x => x.Inquilino).First(x => x.Id == id);
            ViewBag.contratoId = contrato.Id;
            ViewBag.InquilinoNombre = contrato.Inquilino.Nombre + " " + contrato.Inquilino.Apellido;
            return View();
        }

        // POST: Pagos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago pago)
        {
            try
            {
                Contrato contrato = contexto.Contrato.Include(x => x.Inquilino).First(x => x.Id == pago.ContratoId);
                ViewBag.contratoId = contrato.Id;
                ViewBag.InquilinoNombre = contrato.Inquilino.Nombre + " " + contrato.Inquilino.Apellido;


                if (pago.FechaPago==null|| pago.Importe == 0)
                {
                    ViewBag.error = "ingrese todos los datos";
                    return View();
                }
                List<Pago> pagos = contexto.Pago.Where(x => x.ContratoId == pago.ContratoId).ToList();
                int nroPagos = pagos.Count();

                Pago pagocreado = new Pago
                {
                    ContratoId = pago.ContratoId,
                    NroPago = nroPagos + 1,
                    Importe = pago.Importe,
                    FechaPago = pago.FechaPago,
                    Estado = pago.Estado
                };
                contexto.Pago.Add(pagocreado);
                contexto.SaveChanges();

                return RedirectToAction("ListaPagos");
            }
            catch(Exception ex)
            {
                ViewBag.error = ex;
                return View();
            }
        }
        [Authorize(Policy = "Administrador")]
        // GET: Pagos/Edit/5
        public ActionResult Edit(int id)
        {
            Pago pago = contexto.Pago.Include(x => x.Contrato.Inquilino).First(x => x.Id == id);
            ViewBag.contratoId = pago.ContratoId;
            ViewBag.nroPago = pago.NroPago;
            ViewBag.InquilinoNombre = pago.Contrato.Inquilino.Nombre + " " + pago.Contrato.Inquilino.Apellido;
            ViewBag.FechaPago = pago.FechaPago.ToString("yyyy-MM-dd");         
            return View(pago);
        }
        [Authorize(Policy = "Administrador")]
        // POST: Pagos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pago pago)
        {
            try
            {
            
                if (pago.FechaPago == null || pago.Importe == 0)
                {
                    Pago pa = contexto.Pago.Include(x => x.Contrato.Inquilino).First(x => x.Id == pago.Id);
                    ViewBag.contratoId = pago.ContratoId;
                    ViewBag.InquilinoNombre = pa.Contrato.Inquilino.Nombre + " " + pa.Contrato.Inquilino.Apellido;
                    ViewBag.FechaPago = pago.FechaPago.ToString("yyyy-MM-dd");
                    ViewBag.error = "ingrese todos los datos";
                    return View();
                }

                contexto.Pago.Update(pago);
                contexto.SaveChanges();

                return RedirectToAction("ListaPagos");
            }
            catch(Exception ex)
            {
                ViewBag.error = ex;
                return View();
            }
        }

       
    }
}