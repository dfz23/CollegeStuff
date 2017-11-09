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
        public ActionResult Index(int? id)
        {            
            var objeto = new ArticuloEntities();            
            
            return View(objeto.Articulo.ToList());
        }

        [HttpPost]
        public ActionResult Index()
        {
            var objeto = new ArticuloEntities();

            return View(objeto.Articulo.ToList());
        }


        public string Get()
        {
            var objeto = new ArticuloEntities();
            var articulos = objeto.Articulo.ToList();
            var json = JsonConvert.SerializeObject(articulos);
            return json;
        }

        [HttpPost]
        public JsonResult Update(Articulo Objetoupdate)
        {
            var objeto = new ArticuloEntities();
            var data = System.Web.HttpContext.Current.Request["models"];
            if (!string.IsNullOrEmpty(data))
            {
                var oDataNueva = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<List<Articulo>>(data);
                if (oDataNueva != null && oDataNueva.Count > 0)
                {

                    oDataNueva.All(x =>
                    {
                        var A = (from a in objeto.Articulo
                                 where a.ReferenciaArticulo == x.ReferenciaArticulo
                                 select a).FirstOrDefault();
                        A.NombreArticulo = x.NombreArticulo;
                        A.CantidadArticulo = x.CantidadArticulo;
                        A.ValorArticulo = x.ValorArticulo;
                        return true;
                    });

                }
            }
            
            objeto.SaveChanges();
            return this.Json(objeto.Articulo);
        }
        [HttpPost]
        public ActionResult Create()
        {
            var objeto = new ArticuloEntities();
            var data = System.Web.HttpContext.Current.Request["models"];
            if (!string.IsNullOrEmpty(data))
            {
                var oDataNueva = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<List<Articulo>>(data);
                if (oDataNueva != null && oDataNueva.Count > 0)
                {
                    objeto.Articulo.AddRange(oDataNueva);
                }
            }
            
            objeto.SaveChanges();
            return this.Json(objeto.Articulo);
        }
        [HttpPost]
        public ActionResult Destroy()
        {
            var objeto = new ArticuloEntities();
            var data = System.Web.HttpContext.Current.Request["models"];
            if (!string.IsNullOrEmpty(data))
            {
                var oDataNueva = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<List<Articulo>>(data);
                if (oDataNueva != null && oDataNueva.Count > 0)
                {

                    oDataNueva.All(x =>
                    {
                        var A = (from a in objeto.Articulo
                                 where a.ReferenciaArticulo == x.ReferenciaArticulo
                                 select a).FirstOrDefault();
                        objeto.Articulo.Remove(A);
                        return true;
                    });

                }
            }
            objeto.SaveChanges();
            return this.Json(objeto.Articulo);
        }
    }
}