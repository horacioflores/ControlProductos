using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class PlaneadorDa : Base
    {
        public List<Entity.Planeador> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetPlaneadores/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetPlaneadoresResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPlaneadoresResult_>(json);
            return regreso.GetPlaneadoresResult;
        }

        public List<Entity.Planeador> GetCombo()
        {
            string json = methodGet("GetCmbPlaneadores");
            Entity.GetCmbPlaneadoresResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbPlaneadoresResult_>(json);
            return regreso.GetCmbPlaneadoresResult;
        }

        public int DelPlaneador(int IdUser, int IdPlaneador)
        {
            Entity.DelPlaneadorResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPlaneadorResult_>(methodPost("DelPlaneador/" + IdUser.ToString() + "/" + IdPlaneador.ToString()));
            return regreso.DelPlaneadorResult;
        }

        public int InsPlaneador(int IdUser, string Codigo, string Nombre)
        {
            Entity.Planeador maq = new Entity.Planeador();
            maq.Codigo = Codigo;
            maq.Nombre = Nombre;

            Entity.InsPlaneadorResult_ regreso = JsonConvert.DeserializeObject<Entity.InsPlaneadorResult_>(methodPost("InsPlaneador/" + IdUser.ToString(), JsonConvert.SerializeObject(maq)));
            return regreso.InsPlaneadorResult;
        }

        public int UpdPlaneador(int IdUser, int PlaneadorId, string Codigo, string Nombre)
        {
            Entity.Planeador maq = new Entity.Planeador();
            maq.PlaneadorID = PlaneadorId;
            maq.Codigo = Codigo;
            maq.Nombre = Nombre;

            Entity.UpdPlaneadorResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdPlaneadorResult_>(methodPost("UpdPlaneador/" + IdUser.ToString(), JsonConvert.SerializeObject(maq)));
            return regreso.UpdPlaneadorResult;
        }

        public int ValPlaneador(int IdPlaneador, string Codigo, string Descripcion)
        {
            Entity.ValPlaneadorResult_ regreso = JsonConvert.DeserializeObject<Entity.ValPlaneadorResult_>(methodPost("ValPlaneador/" + IdPlaneador.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValPlaneadorResult;
        }

        public int DelPlaneadorSelected(int IdUser, string Valores)
        {
            Entity.DelPlaneadorSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPlaneadorSelectedResult_>(methodPost("DelPlaneadorSelected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelPlaneadorSelectedResult;
        }

        public int DelPlaneadorAll(int IdUser, bool Activo)
        {
            Entity.DelPlaneadorAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPlaneadorAllResult_>(methodPost("DelPlaneadorAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelPlaneadorAllResult;
        }
    }
}