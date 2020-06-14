using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEGOCIO
{
    public class ItinerarioCapaNegocio {


        DATOS.ItinerarioCapaDatos itinerario = new DATOS.ItinerarioCapaDatos();
        public IEnumerable<ENTIDAD.Itinerario> listadoItinerario() {
            return itinerario.listadoItinerario();
        }

        public IEnumerable<ENTIDAD.Itinerario> listadoItinerario2()
        {
            return itinerario.listadoItinerario2();
        }
        public IEnumerable<ENTIDAD.Itinerario> listarItinerarioById(Itinerario itinerarios)
        {
            return itinerario.listarItinerarioById(itinerarios);
        }
        
        public int modificarItinerario(Itinerario itinerarios)
        {
            return itinerario.modificarItinerario(itinerarios);
        }
        public int registrarItinerario(Itinerario itinerarios)
        {
            return itinerario.registrarItinerario(itinerarios);
        }
        public int eliminarItinerario(Itinerario itinerarios)
        {
            return itinerario.eliminarItinerario(itinerarios);
        }

    }
}
