﻿using System;
using System.Collections.Generic;
using System.Text;
using ENTIDAD;

namespace NEGOCIO
{
    public class ClienteCapaNegocio
    {
        DATOS.ClienteCapaDatos capaDatos = new DATOS.ClienteCapaDatos();
        public IEnumerable<ENTIDAD.Cliente> listadoClientes(Cliente cliente)
        {
            return capaDatos.listadoClientes(cliente);
        }

        public int registrarCliente(Cliente cliente)
        {
            return capaDatos.registrarCliente(cliente);
        }
    }
}
