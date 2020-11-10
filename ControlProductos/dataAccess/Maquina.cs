using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class MaquinaDa : Base
    {
        public List<Entity.Maquina> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetMaquinas/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetMaquinasResult_ regreso = JsonConvert.DeserializeObject<Entity.GetMaquinasResult_>(json);
            return regreso.GetMaquinasResult;
        }

        public List<Entity.Maquina> GetCombo()
        {
            string json = methodGet("GetCmbMaquinas");
            Entity.GetCmbMaquinasResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbMaquinasResult_>(json);
            return regreso.GetCmbMaquinasResult;
        }

        public int DelMaquina(int IdUser, int IdMaquina)
        {
            Entity.DelMaquinaResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMaquinaResult_>(methodPost("DelMaquina/" + IdUser.ToString() + "/" + IdMaquina.ToString()));
            return regreso.DelMaquinaResult;
        }

        public int InsMaquina(int IdUser, string Codigo, string Nombre)
        {
            Entity.Maquina maq = new Entity.Maquina();
            maq.Codigo = Codigo;
            maq.Nombre = Nombre;

            Entity.InsMaquinaResult_ regreso = JsonConvert.DeserializeObject<Entity.InsMaquinaResult_>(methodPost("InsMaquina/" + IdUser.ToString(), JsonConvert.SerializeObject(maq)));
            return regreso.InsMaquinaResult;
        }

        public int UpdMaquina(int IdUser, int MaquinaId, string Codigo, string Nombre)
        {
            Entity.Maquina maq = new Entity.Maquina();
            maq.MaquinaID = MaquinaId;
            maq.Codigo = Codigo;
            maq.Nombre = Nombre;

            Entity.UpdMaquinaResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdMaquinaResult_>(methodPost("UpdMaquina/" + IdUser.ToString(), JsonConvert.SerializeObject(maq)));
            return regreso.UpdMaquinaResult;
        }

        public int ValMaquina(int IdMaquina, string Codigo, string Descripcion)
        {
            Entity.ValMaquinaResult_ regreso = JsonConvert.DeserializeObject<Entity.ValMaquinaResult_>(methodPost("ValMaquina/" + IdMaquina.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValMaquinaResult;
        }

        public int DelMaquinaSelected(int IdUser, string Valores)
        {
            Entity.DelMaquinaSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMaquinaSelectedResult_>(methodPost("DelMaquinaSelected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelMaquinaSelectedResult;
        }

        public int DelMaquinaAll(int IdUser, bool Activo)
        {
            Entity.DelMaquinaAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMaquinaAllResult_>(methodPost("DelMaquinaAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelMaquinaAllResult;
        }
    }
}