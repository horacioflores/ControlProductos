using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetPerfilResult_
    {
        public Perfil GetPerfilResult { get; set; }
    }
    public class GetPerfilesResult_
    {
        public List<Perfil> GetPerfilesResult { get; set; }
    }
    public class GetCmbPerfilResult_
    {
        public List<Perfil> GetCmbPerfilResult { get; set; }
    }


    public class InsPerfilResult_
    {
        public int InsPerfilResult { get; set; }
    }
    public class UpdPerfilResult_
    {
        public int UpdPerfilResult { get; set; }
    }
    public class DelPerfilResult_
    {
        public int DelPerfilResult { get; set; }
    }
    public class DelPerfilSelectedResult_
    {
        public int DelPerfilSelectedResult { get; set; }
    }
    public class DelPerfilAllResult_
    {
        public int DelPerfilAllResult { get; set; }
    }
    public class ValPerfilResult_
    {
        public int ValPerfilResult { get; set; }
    }
    public class UpdPerfilXlSResult_
    {
        public int UpdPerfilXlSResult { get; set; }
    }


    public class Perfil_Perfil_Apps
    {
        public Perfil perfil { get; set; }
        public List<Perfil_Apps> perfil_apps { get; set; }
    }
    public class Perfil
    {
        public int PerfilId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool EsAdministrador { get; set; }
        public bool RealizaEncuestas { get; set; }
        public bool Activo { get; set; }

        public string CodigoYNombre { get; set; }
    }
    public class Perfiles
    {
        public int PerfilId { get; set; }
        public string Codigo { get; set; }
        public string DESCRIPCION { get; set; }
        public bool EsAdministrador { get; set; }
        public bool RealizaEncuestas { get; set; }
        public int ACTIVO { get; set; }
    }
}