using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Propiedades.Interfaces;
using Propiedades.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static Propiedades.Models.DetallesPropiedad;

namespace Propiedades.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;

        public HomeController(ILogger<HomeController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(int? pageNumber, string location, string tipo, string regiones, string sectores)
        {
            int? tipoProp = tipo == null ? null : int.Parse(tipo);
            int? Region = regiones == null ? -1 : int.Parse(regiones);
            int? Sector = sectores == null ? 0 : int.Parse(sectores);

            vmPropiedadResponse response = await _apiService.ObtenerTodasLasPropiedades(pageNumber, null, null, 10, null, tipoProp, Sector);
            
            if (pageNumber == 1 || pageNumber == null)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("TotalRecords", response.Response.TotalRecords.ToString());
                HttpContext.Session.SetString("Pages", response.Response.TotalPages.ToString());
            }

            HttpContext.Session.SetString("PageNumber", (pageNumber == null ? 1 : pageNumber).ToString());
            ViewBag.PageNumber = pageNumber == null ? 1 : pageNumber;
            ViewBag.Pages = Convert.ToInt32(HttpContext.Session.GetString("Pages"));

            var propiedades = await _apiService.GetTipoPropiedad();
            propiedades.Insert(0, new TipoPropiedades { IdPropertyType = -1, Type = "Seleccionar" });
            ViewBag.PropiedadTipos = new SelectList(propiedades, "IdPropertyType", "Type", tipoProp);

            var _regiones = await _apiService.GetRegiones();
            _regiones.Insert(0, new Regiones { idRegion = -1, name = "Seleccionar" });
            ViewBag.Regiones = new SelectList(_regiones, "idRegion", "name", Region);

            ViewBag.sectorSeleccionado = Sector;

            return View(response);
        }
        public async Task<IActionResult> Detalles(int? PropiedadId)
        {
            PropertyDetalle detalle = await _apiService.ObtenerDetalles(PropiedadId);
            if (detalle == null)
            {
                return RedirectToAction("Index");
            }
            detalle.Observations = Regex.Replace(detalle.Observations, @"<[^>]+>", string.Empty);
            ViewBag.Pages = Convert.ToInt32(HttpContext.Session.GetString("PageNumber"));
            return View(detalle);
        }
        public async Task<IActionResult> ObtenerSectoresPorRegion(int regionId)
        {
            var sectores = await _apiService.GetBoroughs(regionId);
            return Json(sectores);
        }
    }
}
