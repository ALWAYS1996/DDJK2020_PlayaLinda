using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEGOCIO
{
    public class PromocionCapaNegocio
    {
        DATOS.PromocionCapaDatos promocionCapaDatos = new DATOS.PromocionCapaDatos();

        public IEnumerable<ENTIDAD.Promocion> listadoPromociones()
        { return promocionCapaDatos.listadoPromociones(); }

        public int createPromo(Promocion promocion)
        {
            return promocionCapaDatos.createPromo(promocion);
        }

        public int deletePromo(int codigoPromocion)
        {
            return promocionCapaDatos.deletePromo(codigoPromocion);
        }

        public Promocion obtenerPromoById(int codigoPromocion)
        {
           return promocionCapaDatos.obtenerPromoById(codigoPromocion);
        }

        public int updatePromo(Promocion promocion)
        {
            return promocionCapaDatos.updatePromo(promocion);
        }
    }

}

