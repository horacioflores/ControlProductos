using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class UtilizadoDa : Base
    {
        public List<Entity.Utilizado> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetUtilizados/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetUtilizadosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetUtilizadosResult_>(json);
            return regreso.GetUtilizadosResult;
        }

        public List<Entity.Utilizado> GetCombo()
        {
            string json = methodGet("GetCmbUtilizados");
            Entity.GetCmbUtilizadosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbUtilizadosResult_>(json);
            return regreso.GetCmbUtilizadosResult;
        }

        public int DelUtilizado(int IdUser, int IdUtilizado)
        {
            Entity.DelUtilizadoResult_ regreso = JsonConvert.DeserializeObject<Entity.DelUtilizadoResult_>(methodPost("DelUtilizado/" + IdUser.ToString() + "/" + IdUtilizado.ToString()));
            return regreso.DelUtilizadoResult;
        }

        public int InsUtilizado(int IdUser, string Codigo, string Nombre)
        {
            Entity.Utilizado util = new Entity.Utilizado();
            util.Codigo = Codigo;
            util.Nombre = Nombre;

            Entity.InsUtilizadoResult_ regreso = JsonConvert.DeserializeObject<Entity.InsUtilizadoResult_>(methodPost("InsUtilizado/" + IdUser.ToString(), JsonConvert.SerializeObject(util)));
            return regreso.InsUtilizadoResult;
        }

        public int UpdUtilizado(int IdUser, int UtilizadoId, string Codigo, string Nombre)
        {
            Entity.Utilizado util = new Entity.Utilizado();
            util.UtilizadoID = UtilizadoId;
            util.Codigo = Codigo;
            util.Nombre = Nombre;

            Entity.UpdUtilizadoResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdUtilizadoResult_>(methodPost("UpdUtilizado/" + IdUser.ToString(), JsonConvert.SerializeObject(util)));
            return regreso.UpdUtilizadoResult;
        }

        public int ValUtilizado(int IdUtilizado, string Codigo, string Descripcion)
        {
            Entity.ValUtilizadoResult_ regreso = JsonConvert.DeserializeObject<Entity.ValUtilizadoResult_>(methodPost("ValUtilizado/" + IdUtilizado.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValUtilizadoResult;
        }

        public int DelUtilizadoSelected(int IdUser, string Valores)
        {
            Entity.DelUtilizadoSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelUtilizadoSelectedResult_>(methodPost("DelUtilizadoSelected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelUtilizadoSelectedResult;
        }

        public int DelUtilizadoAll(int IdUser, bool Activo)
        {
            Entity.DelUtilizadoAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelUtilizadoAllResult_>(methodPost("DelUtilizadoAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelUtilizadoAllResult;
        }
    }
}