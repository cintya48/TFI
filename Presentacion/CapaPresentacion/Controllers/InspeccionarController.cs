using Datos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Negocio.Repositorio;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class InspeccionarController : Controller
    {
        private IRepoDefecto repoDefecto;
        private ServicioTurno servicioTurno;
        private Servicio_Color servicio_Color;
        private Servicio_Modelo servicio_Modelo;
        private Servicio_OP servicio_OP;

        public InspeccionarController()
        {
            if (repoDefecto == null)
            {
                repoDefecto = new RepoDefecto();
            }
            if( servicio_Modelo== null)
            {
                servicio_Modelo= new Servicio_Modelo();
            }
            if (servicioTurno == null)
            {
                servicioTurno = new ServicioTurno();
            }
            if (servicio_Color == null)
            {
                servicio_Color = new Servicio_Color();
            }
            if (servicio_OP == null)
            {
                servicio_OP = new Servicio_OP();
            }
        }

        // GET: Inspeccion
        public ActionResult Index()
        {
            var usuario = Session["Usuario"] as ModeloUsuario;

            var modeloOP = servicio_OP.BuscarOPPorJornada(usuario.Legajo);

            ViewBag.NumeroOP = modeloOP.Numero_OP;
            ViewBag.ModeloOP = servicio_Modelo.BuscarModelo(modeloOP.sku_modelo.Value);
            ViewBag.ColorOP = servicio_Color.BuscarColor(modeloOP.codigo_color.Value);
           
            var defectos = repoDefecto.GetDefectos();
            ViewData.Model = defectos;

            // Obtener la hora actual como un objeto DateTime
            DateTime ahora = DateTime.Now;

            // Convertir la hora actual a un TimeSpan
            TimeSpan hrActual = ahora.TimeOfDay;

            // Obtener el turno actual
            var turnoActual = servicioTurno.GetTurno(hrActual);

            // Obtener la hora de inicio y fin del turno actual
            var horainicio = turnoActual.Hr_Inicio;
            var horaFin = turnoActual.HR_Fin;

            // Convertir la hora de inicio y fin a objetos DateTime
            var inicio = DateTime.Today + horainicio;
            var fin = DateTime.Today + horaFin;

            // Crear una lista de horas para el turno actual
            var horasTurno = new List<string>();

            // Iterar sobre las horas del turno y agregar cada hora a la lista
            for (DateTime hora = inicio; hora < fin.AddHours(1); hora = hora.AddHours(1))
            {
                var horaFormateada = hora.ToString("HH:mm");
                horasTurno.Add(horaFormateada);
            }



            //for (DateTime hora = inicio; hora <= fin; hora = hora.AddHours(1))
            //{
            //    var horaFormateada = hora.ToString("HH:mm");
            //    horasTurno.Add(horaFormateada);
            //}

            ViewBag.Horas = horasTurno;


            return View();
        }

        public ActionResult MyView(int currentValue)
        {

            ViewBag.CurrentValue = currentValue;
            return View();
        }

       

        [HttpPost]
        public ActionResult GuardarHallazgo(string TipoDefecto, int IdDefecto, string Pie, int Valor)
        {

            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult RegistrarInspeccion(int TotalPP, int TotalDO,int TotalDR)
        {

            return Json(new { success = true });
        }


    }
}
