using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class ConteoDa : Base
    {
        public List<Entity.Conteo> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetConteos/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetConteosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetConteosResult_>(json);
            return regreso.GetConteosResult;
        }

        public List<Entity.Conteo> GetCombo()
        {
            string json = methodGet("GetCmbConteos");
            Entity.GetCmbConteosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbConteosResult_>(json);
            return regreso.GetCmbConteosResult;
        }

        public int DelConteo(int IdUser, int IdConteo)
        {
            Entity.DelConteoResult_ regreso = JsonConvert.DeserializeObject<Entity.DelConteoResult_>(methodPost("DelConteo/" + IdUser.ToString() + "/" + IdConteo.ToString()));
            return regreso.DelConteoResult;
        }

        public int InsConteo(int IdUser, string Codigo, string Nombre)
        {
            Entity.Conteo util = new Entity.Conteo();
            util.Codigo = Codigo;
            util.Nombre = Nombre;

            Entity.InsConteoResult_ regreso = JsonConvert.DeserializeObject<Entity.InsConteoResult_>(methodPost("InsConteo/" + IdUser.ToString(), JsonConvert.SerializeObject(util)));
            return regreso.InsConteoResult;
        }

        public int UpdConteo(int IdUser, int ConteoId, string Codigo, string Nombre)
        {
            Entity.Conteo util = new Entity.Conteo();
            util.ConteoID = ConteoId;
            util.Codigo = Codigo;
            util.Nombre = Nombre;

            Entity.UpdConteoResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdConteoResult_>(methodPost("UpdConteo/" + IdUser.ToString(), JsonConvert.SerializeObject(util)));
            return regreso.UpdConteoResult;
        }

        public int ValConteo(int IdConteo, string Codigo, string Descripcion)
        {
            Entity.ValConteoResult_ regreso = JsonConvert.DeserializeObject<Entity.ValConteoResult_>(methodPost("ValConteo/" + IdConteo.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValConteoResult;
        }

        public int DelConteoSelected(int IdUser, string Valores)
        {
            Entity.DelConteoSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelConteoSelectedResult_>(methodPost("DelConteoSelected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelConteoSelectedResult;
        }

        public int DelConteoAll(int IdUser, bool Activo)
        {
            Entity.DelConteoAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelConteoAllResult_>(methodPost("DelConteoAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelConteoAllResult;
        }
    }
}