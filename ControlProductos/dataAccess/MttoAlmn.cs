using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class MttoAlmnDa: Base
    {
        public List<Entity.MttoAlmn> GetCatalog(string Codigo, string tipo, bool Activo)
        {
            string json = methodGet("GetMttoAlmn/" + Codigo + "/" + tipo + "/" + Activo.ToString());
            Entity.GetMttoAlmnResult_ regreso = JsonConvert.DeserializeObject<Entity.GetMttoAlmnResult_>(json);
            return regreso.GetMttoAlmnResult;
        }

        public List<Entity.MttoAlmn> GetCombo()
        {
            string json = methodGet("GetCmbMttoAlmn");
            Entity.GetCmbMttoAlmnResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbMttoAlmnResult_>(json);
            return regreso.GetCmbMttoAlmnResult;
        }

        public int DelMttoAlmn(int IdUser, int IdDelMttoAlmn)
        {
            Entity.DelMttoAlmnResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMttoAlmnResult_>(methodPost("DelMttoAlmn/" + IdUser.ToString() + "/" + IdDelMttoAlmn.ToString()));
            return regreso.DelMttoAlmnResult;
        }

        public int InsMttoAlmn(int IdUser, string Codigo, string especificacion, string notas, string clasificacion, string responsable, string tipo)
        {
            Entity.MttoAlmn util = new Entity.MttoAlmn();
            util.codigoMttoAlmn = Codigo;
            util.especificacion = especificacion;
            util.notas = notas;
            util.clasificacion = clasificacion;
            util.responsable = responsable;
            util.tipo = tipo;

            Entity.InsMttoAlmnResult_ regreso = JsonConvert.DeserializeObject<Entity.InsMttoAlmnResult_>(methodPost("InsMttoAlmn/" + IdUser.ToString(), JsonConvert.SerializeObject(util)));
            return regreso.InsMttoAlmnResult;
        }

        public int UpdMttoAlmn(int IdUser, int MttoAlmnID, string Codigo, string especificacion, string notas, string clasificacion, string responsable, string tipo)
        {
            Entity.MttoAlmn util = new Entity.MttoAlmn();
            util.MttoAlmnID = MttoAlmnID;
            util.codigoMttoAlmn = Codigo;
            util.especificacion = especificacion;
            util.notas = notas;
            util.clasificacion = clasificacion;
            util.responsable = responsable;
            util.tipo = tipo;

            Entity.UpdTipoArticuloResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdTipoArticuloResult_>(methodPost("UpdMttoAlmn/" + IdUser.ToString(), JsonConvert.SerializeObject(util)));
            return regreso.UpdTipoArticuloResult;
        }

        public int ValMttoAlmn(int MttoAlmnID, string Codigo, string tipo)
        {
            Entity.ValMttoAlmnResult_ regreso = JsonConvert.DeserializeObject<Entity.ValMttoAlmnResult_>(methodPost("ValMttoAlmn/" + MttoAlmnID.ToString() + "/" + Codigo + "/" + tipo));
            return regreso.ValMttoAlmnResult;
        }

        public int DelMttoAlmnSelected(int IdUser, string Valores)
        {
            Entity.DelMttoAlmnSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMttoAlmnSelectedResult_>(methodPost("DelMttoAlmnSelected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelMttoAlmnSelectedResult;
        }

        public int DelMttoAlmnAll(int IdUser, bool Activo)
        {
            Entity.DelMttoAlmnAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMttoAlmnAllResult_>(methodPost("DelMttoAlmnAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelMttoAlmnAllResult;
        }
    }
}