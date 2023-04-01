using Negocio.Interfaces;
using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class Servicio_JL
    {
        private readonly IRepoJorndaLaboral _repoJornadaLaboral;

        public Servicio_JL()
        {


        }

        public Servicio_JL(IRepoJorndaLaboral repoJornadaLaboral)
        {
            _repoJornadaLaboral = repoJornadaLaboral;
        }

        public void ActualizarFechaFinJornada(int idJornada)
        {
            var jornada = _repoJornadaLaboral.BuscarJornada(idJornada);
            if (jornada != null)
            {
                jornada.FechaFin = DateTime.Now;
                _repoJornadaLaboral.ActualizarJornada(jornada);
            }
        }




    }
}
