using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.models
{
    public class Empleado
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public int id_perfil { get; set; }
        public string foto { get; set; }
        public bool activo { get; set; }
        public string username { get; set; }
        public string contrasena { get; set; }
        public string fecha_alta { get; set; }
        public int id_empleadoAdd { get; set; }
        public string fecha_modifico { get; set; }
        public int id_empleadoMod { get; set; }
        public string perfil { get; set; }
    }
}