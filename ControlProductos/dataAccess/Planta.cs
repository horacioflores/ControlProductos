using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class PlantaDa : Base
    {

        public List<Entity.Planta> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetPlantas/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetPlantasResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPlantasResult_>(json);
            return regreso.GetPlantasResult;
        }

        public List<Entity.Planta> GetCombo()
        {
            string json = methodGet("GetCmbPlanta");
            Entity.GetCmbPlantaResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbPlantaResult_>(json);
            return regreso.GetCmbPlantaResult;
        }

        public int DelPlanta(int IdUser, int IdPlanta)
        {
            Entity.DelPlantaResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPlantaResult_>(methodPost("DelPlanta/" + IdUser.ToString() + "/" + IdPlanta.ToString()));
            return regreso.DelPlantaResult;
        }

        public int InsPlanta(int IdUser, string Codigo, string Descripcion, string Direccion)
        {
            Entity.Planta pnta = new Entity.Planta();
            pnta.Codigo = Codigo;
            pnta.Descripcion = Descripcion;
            pnta.Direccion = Direccion;

            Entity.InsPlantaResult_ regreso = JsonConvert.DeserializeObject<Entity.InsPlantaResult_>(methodPost("InsPlanta/" + IdUser.ToString(), JsonConvert.SerializeObject(pnta)));
            return regreso.InsPlantaResult;
        }

        public int UpdPlanta(int IdUser, int DeptoId, string Codigo, string Descripcion, string Direccion)
        {
            Entity.Planta pnta = new Entity.Planta();
            pnta.PlantaId = DeptoId;
            pnta.Codigo = Codigo;
            pnta.Descripcion = Descripcion;
            pnta.Direccion = Direccion;

            Entity.UpdPlantaResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdPlantaResult_>(methodPost("UpdPlanta/" + IdUser.ToString(), JsonConvert.SerializeObject(pnta)));
            return regreso.UpdPlantaResult;
        }

        public int ValPlanta(int IdPlanta, string Codigo, string Descripcion)
        {
            Entity.ValPlantaResult_ regreso = JsonConvert.DeserializeObject<Entity.ValPlantaResult_>(methodPost("ValPlanta/" + IdPlanta.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValPlantaResult;
        }

        public int DelPlantaSelected(int IdUser, string Valores, bool Activo)
        {
            Entity.DelPlantaSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPlantaSelectedResult_>(methodPost("DelPlantaSelected/" + IdUser.ToString() + "/" + Valores + "/" + Activo.ToString()));
            return regreso.DelPlantaSelectedResult;
        }

        public int DelPlantaAll(int IdUser, bool Activo)
        {
            Entity.DelPlantaAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPlantaAllResult_>(methodPost("DelPlantaAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelPlantaAllResult;
        }

    }
}