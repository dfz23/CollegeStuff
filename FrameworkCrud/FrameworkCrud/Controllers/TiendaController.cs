using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace FrameworkCrud.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Get()
        {
            var objeto = new ArticuloEntities();
            var articulos = objeto.Articulo.ToList().OrderBy(p => p.ReferenciaArticulo);
            return Json(articulos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(Articulo Objetoupdate)
        {
            List<Articulo> oDataNueva = null;
            var objeto = new ArticuloEntities();
            var data = System.Web.HttpContext.Current.Request["models"];
            if (!string.IsNullOrEmpty(data))
            {
                oDataNueva = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<List<Articulo>>(data);
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
            return Json(oDataNueva, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            List<Articulo> oDataNueva = null;
            var objeto = new ArticuloEntities();
            var data = System.Web.HttpContext.Current.Request["models"];
            if (!string.IsNullOrEmpty(data))
            {
                oDataNueva = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<List<Articulo>>(data);
                if (oDataNueva != null && oDataNueva.Count > 0)
                {
                    objeto.Articulo.AddRange(oDataNueva);
                }
            }

            objeto.SaveChanges();
            return Json(oDataNueva, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Destroy()
        {
            List<Articulo> oDataNueva = null;
            var objeto = new ArticuloEntities();
            var data = System.Web.HttpContext.Current.Request["models"];
            if (!string.IsNullOrEmpty(data))
            {
                oDataNueva = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<List<Articulo>>(data);
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
            return Json(oDataNueva, JsonRequestBehavior.AllowGet);
        }
    }
}