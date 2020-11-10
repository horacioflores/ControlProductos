using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class UMedidaDa : Base
    {
        public List<Entity.UMedida> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetUMedidas/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetUMedidasResult_ regreso = JsonConvert.DeserializeObject<Entity.GetUMedidasResult_>(json);
            return regreso.GetUMedidasResult;
        }
        public List<Entity.UMedida> GetCombo()
        {
            string json = methodGet("GetCmbUMedida");
            Entity.GetCmbUMedidaResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbUMedidaResult_>(json);
            return regreso.GetCmbUMedidaResult;
        }

        public int DelUMedida(int IdUser, int IdUMedida)
        {
            Entity.DelUmedidaResult_ regreso = JsonConvert.DeserializeObject<Entity.DelUmedidaResult_>(methodPost("DelUmedida/" + IdUser.ToString() + "/" + IdUMedida.ToString()));
            return regreso.DelUmedidaResult;
        }

        public int InsUMedida(int IdUser, string Codigo, string Nombre)
        {
            Entity.UMedida UMedida = new Entity.UMedida();
            UMedida.Codigo = Codigo;
            UMedida.Nombre = Nombre;

            Entity.InsUmedidaResult_ regreso = JsonConvert.DeserializeObject<Entity.InsUmedidaResult_>(methodPost("InsUmedida/" + IdUser.ToString(), JsonConvert.SerializeObject(UMedida)));
            return regreso.InsUmedidaResult;
        }

        public int UpdUMedida(int IdUser, int UMedidaId, string Codigo, string Nombre)
        {
            Entity.UMedida UMedida = new Entity.UMedida();
            UMedida.UMedidaID = UMedidaId;
            UMedida.Codigo = Codigo;
            UMedida.Nombre = Nombre;

            Entity.UpdUmedidaResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdUmedidaResult_>(methodPost("UpdUmedida/" + IdUser.ToString(), JsonConvert.SerializeObject(UMedida)));
            return regreso.UpdUmedidaResult;
        }

        public int ValUMedida(int IdUMedida, string Codigo, string Descripcion)
        {
            Entity.ValUmedidaResult_ regreso = JsonConvert.DeserializeObject<Entity.ValUmedidaResult_>(methodPost("ValUmedida/" + IdUMedida.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValUmedidaResult;
        }

        public int DelUMedidaSelected(int IdUser, string Valores)
        {
            Entity.DelUmedidaSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelUmedidaSelectedResult_>(methodPost("DelUmedidaSelected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelUmedidaSelectedResult;
        }

        public int DelUMedidaAll(int IdUser, bool Activo)
        {
            Entity.DelUmedidaAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelUmedidaAllResult_>(methodPost("DelUmedidaAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelUmedidaAllResult;
        }
    }
}