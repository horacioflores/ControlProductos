using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetPerfilAppsResult_
    {
        public List<Perfil_Apps> GetPerfilAppsResult { get; set; }
    }

    public class Perfil_Apps
    {
        public int RegistroId { get; set; }
        public int PerfilId { get; set; }
        public string Perfil { get; set; }
        public bool EsAdministrador { get; set; }
        public bool RealizaEncuestas { get; set; }
        public int AppId { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}