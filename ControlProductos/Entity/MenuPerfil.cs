using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetMenuPerfilResult_
    {
        public List<MenuPerfil> GetMenuPerfilResult { get; set; }
    }
    public class GetAccesoMenuPerfilAllResult_
    {
        public List<MenuPerfil> GetAccesoMenuPerfilAllResult { get; set; }
    }
    public class GetAccesoMenuPerfilResult_
    {
        public List<MenuPerfil> GetAccesoMenuPerfilResult { get; set; }
    }


    public class UpdMenuPerfilResult_
    {
        public int UpdMenuPerfilResult { get; set; }
    }
    public class UpdMenuAccionPerfilResult_
    {
        public int UpdMenuAccionPerfilResult { get; set; }
    }
    public class DelMenuPerfilResult_
    {
        public int DelMenuPerfilResult { get; set; }
    }
    public class DelMenuPerfilSelectedResult_
    {
        public int DelMenuPerfilSelectedResult { get; set; }
    }

    public class MenuPerfil
    {
        public int PerfilId { get; set; }
        public int MenuId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string URL { get; set; }
        public int Activo { get; set; }
        public int AccionId { get; set; }
        public int MenuPadre { get; private set; }
    }
}