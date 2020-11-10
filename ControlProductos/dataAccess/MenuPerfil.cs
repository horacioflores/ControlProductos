using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class MenuPerfilDa : Base
    {
        public List<Entity.MenuPerfil> GetMenuPerfil(int IdPerfile, int IdAplication, bool Selected)
        {
            string json = methodGet("GetMenuPerfil/" + IdPerfile.ToString() + "/" + IdAplication.ToString() + "/" + Selected.ToString());
            Entity.GetMenuPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.GetMenuPerfilResult_>(json);
            return regreso.GetMenuPerfilResult;
        }

        public int UpdMenuPerfil(int IdUser, int IdPerfile, int IdScreen)
        {
            Entity.MenuPerfil mnu_perf = new Entity.MenuPerfil();
            mnu_perf.PerfilId = IdPerfile;
            mnu_perf.MenuId = IdScreen;

            Entity.UpdMenuPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdMenuPerfilResult_>(methodPost("UpdMenuPerfil/" + IdUser.ToString(), JsonConvert.SerializeObject(mnu_perf)));
            return regreso.UpdMenuPerfilResult;
        }

        public int DelMenuPerfil(int IdUser, int PerfilId, int MenuId)
        {
            Entity.MenuPerfil mnu_perf = new Entity.MenuPerfil();
            mnu_perf.PerfilId = PerfilId;
            mnu_perf.MenuId = MenuId;

            Entity.DelMenuPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMenuPerfilResult_>(methodPost("DelMenuPerfil/" + IdUser.ToString(), JsonConvert.SerializeObject(mnu_perf)));
            return regreso.DelMenuPerfilResult;
        }

        //public int InsMenuPerfil(int IdUser, int AppId, int IdParent, string Description, string UrlImg, string Key, string url)
        //{
        //    var DAMenuPerfil = new DataAccess.MenuPerfil(ConnectionString);
        //    return DAMenuPerfil.InsMenuPerfil(IdUser, AppId, IdParent, Description, UrlImg, Key, url);
        //}

        public List<Entity.MenuPerfil> GetAccesoMenuPerfilAll()
        {
            string json = methodGet("GetAccesoMenuPerfilAll");
            Entity.GetAccesoMenuPerfilAllResult_ regreso = JsonConvert.DeserializeObject<Entity.GetAccesoMenuPerfilAllResult_>(json);
            return regreso.GetAccesoMenuPerfilAllResult;
        }

        public List<Entity.MenuPerfil> GetAccesoMenuPerfil(int PerfilId, int MenuId)
        {
            string json = methodGet("GetAccesoMenuPerfil/"+ PerfilId.ToString()+ "/" + MenuId.ToString());
            Entity.GetAccesoMenuPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.GetAccesoMenuPerfilResult_>(json);
            return regreso.GetAccesoMenuPerfilResult;
        }

        public int UpdMenuAccionPerfil(int IdUser, int PerfilId, int MenuId, string Acciones)
        {
            Entity.MenuPerfil mnu_perf = new Entity.MenuPerfil();
            mnu_perf.PerfilId = PerfilId;
            mnu_perf.MenuId = MenuId;

            Entity.UpdMenuAccionPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdMenuAccionPerfilResult_>(methodPost("UpdMenuAccionPerfil/" + IdUser.ToString() + "/" + Acciones, JsonConvert.SerializeObject(mnu_perf)));
            return regreso.UpdMenuAccionPerfilResult;
        }

        public int DelMenuPerfilSelected(int IdUser, string Valores, bool Activo)
        {
            Entity.DelMenuPerfilSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMenuPerfilSelectedResult_>(methodPost("DelMenuPerfilSelected/" + IdUser.ToString() + "/" + Valores + "/" + Activo.ToString()));
            return regreso.DelMenuPerfilSelectedResult;
        }

        public int DelMenuPerfilAll(int IdUser, int AplicacionId, int PerfilId, bool Activo)
        {
            Entity.DelMenuPerfilSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMenuPerfilSelectedResult_>(methodPost("DelMenuPerfilAll/" + IdUser.ToString() + "/"+ AplicacionId.ToString() + "/" + PerfilId.ToString() + "/" + Activo.ToString()));
            return regreso.DelMenuPerfilSelectedResult;
        }
    }
}