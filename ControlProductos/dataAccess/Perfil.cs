using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class PerfilDa : Base
    {
        public Entity.Perfil GetPerfil(int PerfilId)
        {
            string json = methodGet("GetPerfil/" + PerfilId.ToString());
            Entity.GetPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPerfilResult_>(json);
            return regreso.GetPerfilResult;
        }

        public List<Entity.Perfil> GetCatalog(string Codigo, bool Activo)
        {
            string json = methodGet("GetPerfiles/" + Codigo+ "/" + Activo.ToString());
            Entity.GetPerfilesResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPerfilesResult_>(json);
            return regreso.GetPerfilesResult;
        }

        public System.Data.DataSet GetCatalogPerfiles(string Codigo, bool Activo)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Entity.Perfil> lPerfil = GetCatalog(Codigo, Activo);
            dt = ADataTable(lPerfil);
            ds.Tables.Add(dt);
            return ds;
        }

        public List<Entity.Perfil> GetCombo()
        {
            string json = methodGet("GetCmbPerfil");
            Entity.GetCmbPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbPerfilResult_>(json);
            return regreso.GetCmbPerfilResult;
        }

        public int InsPerfil(int IdUser, string Codigo, string Description, bool EsAdministrador, bool RealizaEncuestas, List<Entity.Perfil_Apps> perfil_apps)
        {
            Entity.Perfil perf = new Entity.Perfil();
            perf.Codigo = Codigo;
            perf.Descripcion = Description;
            perf.EsAdministrador = EsAdministrador;
            perf.RealizaEncuestas = RealizaEncuestas;

            Entity.Perfil_Perfil_Apps perf_perfApps = new Entity.Perfil_Perfil_Apps();
            perf_perfApps.perfil = perf;
            perf_perfApps.perfil_apps = perfil_apps;

            Entity.InsPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.InsPerfilResult_>(methodPost("InsPerfil/" + IdUser, JsonConvert.SerializeObject(perf_perfApps)));
            return regreso.InsPerfilResult;
        }

        public int UpdPerfil(int UsuarioId, int PerfilId, string Codigo, string Descripcion, bool EsAdministrador, bool RealizaEncuestas, List<Entity.Perfil_Apps> perfil_apps)
        {
            Entity.Perfil perf = new Entity.Perfil();
            perf.PerfilId = PerfilId;
            perf.Codigo = Codigo;
            perf.Descripcion = Descripcion;
            perf.EsAdministrador = EsAdministrador;
            perf.RealizaEncuestas = RealizaEncuestas;

            Entity.Perfil_Perfil_Apps perf_perfApps = new Entity.Perfil_Perfil_Apps();
            perf_perfApps.perfil = perf;
            perf_perfApps.perfil_apps = perfil_apps;

            Entity.UpdPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdPerfilResult_>(methodPost("UpdPerfil/" + UsuarioId, JsonConvert.SerializeObject(perf_perfApps)));
            return regreso.UpdPerfilResult;
        }

        public int DelPerfil(int IdUser, int PerfilId, bool Activo)
        {
            Entity.Perfil perf = new Entity.Perfil();
            perf.PerfilId = PerfilId;
            perf.Activo = Activo;

            Entity.DelPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPerfilResult_>(methodPost("DelPerfil/" + IdUser + "/" + PerfilId, JsonConvert.SerializeObject(perf)));
            return regreso.DelPerfilResult;

        }

        public int DelPerfilSelected(int IdUser, string Valores, bool Activo)
        {
            Entity.DelPerfilSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPerfilSelectedResult_>(methodPost("DelPerfilSelected/" + IdUser + "/" + Valores + "/" + Activo.ToString()));
            return regreso.DelPerfilSelectedResult;
        }

        public int DelPerfilAll(int IdUser, bool Activo)
        {
            Entity.DelPerfilAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPerfilAllResult_>(methodPost("DelPerfilAll/" + IdUser + "/" + Activo.ToString()));
            return regreso.DelPerfilAllResult;
        }

        public int ValPerfil(int PerfilId, string Codigo, string Descripcion)
        {
            Entity.ValPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.ValPerfilResult_>(methodGet("ValPerfil/" + PerfilId + "/" + Codigo + "/" + Descripcion));
            return regreso.ValPerfilResult;
        }

        public int UpdPerfilXlS(List<Entity.Perfiles> lPerf, int UsuarioId)
        {
            Entity.UpdPerfilXlSResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdPerfilXlSResult_>(methodPost("UpdPerfilXlS/" + UsuarioId, JsonConvert.SerializeObject(lPerf)));
            return regreso.UpdPerfilXlSResult;
        }

    }
}