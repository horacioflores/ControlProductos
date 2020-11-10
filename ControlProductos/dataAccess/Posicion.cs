using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;


namespace ControlProductos.dataAccess
{
    public class PosicionDa :Base
    {
        public List<Entity.Posicion> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetPosiciones/" + Codigo+ "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetPosicionesResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPosicionesResult_>(json);
            return regreso.GetPosicionesResult;
        }

        public List<Entity.Posicion> GetCombo(bool IncluyeEmpleados)
        {
            string json = methodGet("GetCmbPosiciones/" + IncluyeEmpleados.ToString());
            Entity.GetCmbPosicionesResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbPosicionesResult_>(json);
            return regreso.GetCmbPosicionesResult;
        }

        public int DelPosicion(int IdUser, int IdPosicion)
        {
            Entity.DelPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPerfilResult_>(methodPost("DelPosicion/" + IdUser.ToString() + "/" + IdPosicion.ToString()));
            return regreso.DelPerfilResult;
        }

        public int InsPosicion(int IdUser, string Codigo, string Descripcion, int Nivel)
        {
            Entity.Posicion pos = new Entity.Posicion();
            pos.Codigo = Codigo;
            pos.Descripcion = Descripcion;
            pos.Nivel = Nivel;

            Entity.InsPosicionResult_ regreso = JsonConvert.DeserializeObject<Entity.InsPosicionResult_>(methodPost("InsPosicion/" + IdUser.ToString(), JsonConvert.SerializeObject(pos)));
            return regreso.InsPosicionResult;
        }

        public int UpdPosicion(int IdUser, int PosicionId, string Codigo, string Descripcion, int Nivel)
        {
            Entity.Posicion pos = new Entity.Posicion();
            pos.PosicionId = PosicionId;
            pos.Codigo = Codigo;
            pos.Descripcion = Descripcion;
            pos.Nivel = Nivel;

            Entity.UpdPosicionResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdPosicionResult_>(methodPost("UpdPosicion/" + IdUser.ToString(), JsonConvert.SerializeObject(pos)));
            return regreso.UpdPosicionResult;
        }

        public int ValPosicion(int IdPosicion, string Codigo, string Descripcion)
        {
            Entity.DelPerfilResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPerfilResult_>(methodPost("ValPosicion/" + IdPosicion.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.DelPerfilResult;
        }

        public int DelPosicionSelected(int IdUser, string Valores, bool Activo)
        {
            Entity.DelPosicionSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPosicionSelectedResult_>(methodPost("DelPosicionSelected/" + IdUser.ToString() + "/" + Valores + "/" + Activo.ToString()));
            return regreso.DelPosicionSelectedResult;
        }

        public int DelPosicionAll(int IdUser, bool Activo)
        {
            Entity.DelPosicionAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPosicionAllResult_>(methodPost("DelPosicionAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelPosicionAllResult;
        }
    }
}