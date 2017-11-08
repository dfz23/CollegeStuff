using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace FrameworkCrud.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            ViewBag.Message = "Tienda Diego Jaramillo";
            var objeto = new ArticuloEntities();
            //Get the Products entities and add them to the ViewBag.
            ViewBag.Products = objeto.Articulo.ToList();
            return View();
        }

        
        public string Get()
        {
            var objeto = new ArticuloEntities();
            var articulos = objeto.Articulo.ToList();
            var json = JsonConvert.SerializeObject(articulos);
            return json;
        }

        [HttpPost]
        public ActionResult Update(Articulo Objetoupdate)
        {
            var objeto = new ArticuloEntities();
            var A = (from a in objeto.Articulo
                     where a.ReferenciaArticulo == Objetoupdate.ReferenciaArticulo
                     select a).FirstOrDefault();

            A.NombreArticulo = Objetoupdate.NombreArticulo;
            A.CantidadArticulo = Objetoupdate.CantidadArticulo;
            A.ValorArticulo = Objetoupdate.ValorArticulo;
            objeto.SaveChanges();

            return View("Index");
        }
        [HttpPost]
        public ActionResult Create(Articulo Objetoupdate)
        {
            var objeto = new ArticuloEntities();
            objeto.Articulo.Add(Objetoupdate);

            objeto.SaveChanges();

            return View("Index");
        }
    }
}