using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class MarcaDa : Base
    {
        public List<Entity.Marca> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetMarcas/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetMarcasResult_ regreso = JsonConvert.DeserializeObject<Entity.GetMarcasResult_>(json);
            return regreso.GetMarcasResult;
        }

        public List<Entity.Marca> GetCombo()
        {
            string json = methodGet("GetCmbMarcas");
            Entity.GetCmbMarcasResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbMarcasResult_>(json);
            return regreso.GetCmbMarcasResult;
        }

        public int DelMarca(int IdUser, int IdMarca)
        {
            Entity.DelMarcaResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMarcaResult_>(methodPost("DelMarca/" + IdUser.ToString() + "/" + IdMarca.ToString()));
            return regreso.DelMarcaResult;
        }

        public int InsMarca(int IdUser, string Codigo, string Nombre)
        {
            Entity.Marca maq = new Entity.Marca();
            maq.Codigo = Codigo;
            maq.Nombre = Nombre;

            Entity.InsMarcaResult_ regreso = JsonConvert.DeserializeObject<Entity.InsMarcaResult_>(methodPost("InsMarca/" + IdUser.ToString(), JsonConvert.SerializeObject(maq)));
            return regreso.InsMarcaResult;
        }

        public int UpdMarca(int IdUser, int MarcaId, string Codigo, string Nombre)
        {
            Entity.Marca maq = new Entity.Marca();
            maq.MarcaID = MarcaId;
            maq.Codigo = Codigo;
            maq.Nombre = Nombre;

            Entity.UpdMarcaResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdMarcaResult_>(methodPost("UpdMarca/" + IdUser.ToString(), JsonConvert.SerializeObject(maq)));
            return regreso.UpdMarcaResult;
        }

        public int ValMarca(int IdMarca, string Codigo, string Descripcion)
        {
            Entity.ValMarcaResult_ regreso = JsonConvert.DeserializeObject<Entity.ValMarcaResult_>(methodPost("ValMarca/" + IdMarca.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValMarcaResult;
        }

        public int DelMarcaSelected(int IdUser, string Valores)
        {
            Entity.DelMarcaSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMarcaSelectedResult_>(methodPost("DelMarcaSelected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelMarcaSelectedResult;
        }

        public int DelMarcaAll(int IdUser, bool Activo)
        {
            Entity.DelMarcaAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMarcaAllResult_>(methodPost("DelMarcaAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelMarcaAllResult;
        }
    }
}