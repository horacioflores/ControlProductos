using ControlProductos.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class loggedEmpleado
    {
        public Usuario CurrentUsuario { get; set; }
        public Perfil CurrentPerfil { get; set; }

        public loggedEmpleado()
        {
            CurrentPerfil = new Perfil();
            CurrentUsuario = new Usuario();
        }
    }
}