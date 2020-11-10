using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;


namespace ControlProductos.dataAccess
{
    public class AplicacionDa : Base
    {
        public List<Entity.Aplicacion> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetAplicaciones/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetAplicacionesResult_ regreso = JsonConvert.DeserializeObject<Entity.GetAplicacionesResult_>(json);
            return regreso.GetAplicacionesResult;
        }

        public List<Entity.Aplicacion> GetCombo()
        {
            string json = methodGet("GetCmbAplicacion");
            Entity.GetCmbAplicacionResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbAplicacionResult_>(json);
            return regreso.GetCmbAplicacionResult;
        }

        public int DelAplicacion(int IdUser, int IdAplicacion)
        {
            Entity.Aplicacion app = new Entity.Aplicacion();
            app.AppId = IdAplicacion;

            Entity.DelAplicacionResult_ regreso = JsonConvert.DeserializeObject<Entity.DelAplicacionResult_>(methodPost("DelAplicacion/" + IdUser, JsonConvert.SerializeObject(app)));
            return regreso.DelAplicacionResult;
        }

        public int InsAplicacion(int IdUser, string Codigo, string Descripcion, string URL)
        {
            Entity.Aplicacion app = new Entity.Aplicacion();
            app.Codigo = Codigo;
            app.Descripcion = Descripcion;
            app.URL = URL;

            Entity.InsAplicacionResult_ regreso = JsonConvert.DeserializeObject<Entity.InsAplicacionResult_>(methodPost("InsAplicacion/" + IdUser, JsonConvert.SerializeObject(app)));
            return regreso.InsAplicacionResult;
        }

        public int UpdAplicacion(int IdUser, int appId, string Codigo, string Descripcion, string URL)
        {
            Entity.Aplicacion app = new Entity.Aplicacion();
            app.AppId = appId;
            app.Codigo = Codigo;
            app.Descripcion = Descripcion;
            app.URL = URL;

            Entity.UpdAplicacionResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdAplicacionResult_>(methodPost("UpdAplicacion/" + IdUser, JsonConvert.SerializeObject(app)));
            return regreso.UpdAplicacionResult;
        }

        public int ValAplicacion(int IdAplicacion, string Codigo, string Descripcion)
        {
            Entity.Aplicacion app = new Entity.Aplicacion();
            app.AppId = IdAplicacion;
            app.Codigo = Codigo;
            app.Descripcion = Descripcion;

            return Convert.ToInt32(methodPost("ValAplicacion", JsonConvert.SerializeObject(app)));
        }

        public int DelAplicacionSelected(int IdUser, string Valores, bool Activo)
        {
            Entity.DelAplicacionSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelAplicacionSelectedResult_>(methodPost("DelAplicacionSelected/"+IdUser + "/" + Valores + "/" + Activo.ToString()));
            return regreso.DelAplicacionSelectedResult;
        }

        public int DelAplicacionAll(int IdUser, bool Activo)
        {
            Entity.DelAplicacionAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelAplicacionAllResult_>(methodPost("DelAplicacionAll/" + IdUser  + "/" + Activo.ToString()));
            return regreso.DelAplicacionAllResult;
        }
    }
}