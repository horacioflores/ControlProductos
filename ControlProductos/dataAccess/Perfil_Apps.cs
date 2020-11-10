using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class Perfil_AppsDa : Base
    {
        public Entity.Perfil_Apps GetPerfilApps(int PerfilId, bool Activo)
        {
            string json = methodGet("GetPerfilApps/" + PerfilId + "/"  + Activo.ToString());
            Entity.GetPerfilAppsResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPerfilAppsResult_>(json);
            Entity.Perfil_Apps reg = new Entity.Perfil_Apps();
            if(regreso.GetPerfilAppsResult.Count > 0)
            {
                reg = regreso.GetPerfilAppsResult[0];
            }

            return reg;
        }

        public List<Entity.Perfil_Apps> GetListPerfilApps(int PerfilId, bool Activo)
        {
            string json = methodGet("GetPerfilApps/" + PerfilId + "/" + Activo.ToString());
            Entity.GetPerfilAppsResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPerfilAppsResult_>(json);
            return regreso.GetPerfilAppsResult;
        }

    }
}