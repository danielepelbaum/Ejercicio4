using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ej_4.Models;

namespace ej_4.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Prestamo(bool tarjeta, bool prestamoBancario, bool prestamoInformal, string tipoEmpleo, string nombre, int edad, string trabaja, int ingresoMensual, string deudasSiNo, int montoSolicitado, bool acepta)
    {
        bool errores = false;
        bool aprobado = true;
        string mensaje = "";
        if (edad < 18){
            aprobado = false;
        }
        else if (trabaja == "No"){
            aprobado = false;
        }
        else if (ingresoMensual<250000){
            aprobado = false;
        }
        else if (deudasSiNo == "Si"){
            aprobado = false;
        }
        else if (ingresoMensual*5 < montoSolicitado){
            aprobado = false;
        }
        else if (acepta == false){
            aprobado = false;
        }
        ViewBag.nombre = nombre;
        if (aprobado){
            mensaje = "Su préstamo ha sido aprobado. Felicidades!";
        }
        else{
            mensaje = "Su préstamo ha sido denegado debido a la falta de condiciones. Por favor, vuelva en otro momento que pueda cumplirlas.";
        }
        if(tipoEmpleo == "4" && trabaja == "Si" || trabaja == "No" && tipoEmpleo != "4" || deudasSiNo == "Si" && tarjeta == false && prestamoBancario == false && prestamoInformal == false || deudasSiNo == "No" && (tarjeta == true || prestamoBancario == true || prestamoInformal == true)){
            mensaje = "Su préstamo ha sido denegado debido a contradicciones en el formulario.";
        }
        ViewBag.mensaje = mensaje;
        return View("resultado");
    }
}
