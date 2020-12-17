using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class TipoArticuloDa : Base
    {
        public List<Entity.TipoArticulo> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetTiposArticulo/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetTiposArticuloResult_ regreso = JsonConvert.DeserializeObject<Entity.GetTiposArticuloResult_>(json);
            return regreso.GetTiposArticuloResult;
        }

        public List<Entity.TipoArticulo> GetCombo()
        {
            string json = methodGet("GetCmbTiposArticulo");
            Entity.GetCmbTiposArticuloResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbTiposArticuloResult_>(json);
            return regreso.GetCmbTiposArticuloResult;
        }

        public int DelTipoArticulo(int IdUser, int IdTipoArticulo)
        {
            Entity.DelTipoArticuloResult_ regreso = JsonConvert.DeserializeObject<Entity.DelTipoArticuloResult_>(methodPost("DelTipoArticulo/" + IdUser.ToString() + "/" + IdTipoArticulo.ToString()));
            return regreso.DelTipoArticuloResult;
        }

        public int InsTipoArticulo(int IdUser, string Codigo, string tipoArticulo, string M, string N, string comentarios)
        {
            Entity.TipoArticulo util = new Entity.TipoArticulo();
            util.codigoTipoArticulo = Codigo;
            util.tipoArticulo = tipoArticulo;
            util.M = M;
            util.N = N;
            util.comentarios = comentarios;

            Entity.InsTipoArticuloResult_ regreso = JsonConvert.DeserializeObject<Entity.InsTipoArticuloResult_>(methodPost("InsTipoArticulo/" + IdUser.ToString(), JsonConvert.SerializeObject(util)));
            return regreso.InsTipoArticuloResult;
        }

        public int UpdTipoArticulo(int IdUser, int TipoArticuloId, string Codigo, string tipoArticulo, string M, string N, string comentarios)
        {
            Entity.TipoArticulo util = new Entity.TipoArticulo();
            util.tipoArticuloID = TipoArticuloId;
            util.codigoTipoArticulo = Codigo;
            util.tipoArticulo = tipoArticulo;
            util.M = M;
            util.N = N;
            util.comentarios = comentarios;

            Entity.UpdTipoArticuloResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdTipoArticuloResult_>(methodPost("UpdTipoArticulo/" + IdUser.ToString(), JsonConvert.SerializeObject(util)));
            return regreso.UpdTipoArticuloResult;
        }

        public int ValTipoArticulo(int IdTipoArticulo, string Codigo, string Descripcion)
        {
            Entity.ValTipoArticuloResult_ regreso = JsonConvert.DeserializeObject<Entity.ValTipoArticuloResult_>(methodPost("ValTipoArticulo/" + IdTipoArticulo.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValTipoArticuloResult;
        }

        public int DelTipoArticuloSelected(int IdUser, string Valores)
        {
            Entity.DelTipoArticuloSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelTipoArticuloSelectedResult_>(methodPost("DelTipoArticuloSelected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelTipoArticuloSelectedResult;
        }

        public int DelTipoArticuloAll(int IdUser, bool Activo)
        {
            Entity.DelTipoArticuloAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelTipoArticuloAllResult_>(methodPost("DelTipoArticuloAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelTipoArticuloAllResult;
        }
    }
}