using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class CompradorDa : Base
    {
        public List<Entity.Comprador> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetCompradores/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetCompradoresResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCompradoresResult_>(json);
            return regreso.GetCompradoresResult;
        }

        public List<Entity.Comprador> GetCombo()
        {
            string json = methodGet("GetCmbCompradores");
            Entity.GetCmbCompradoresResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbCompradoresResult_>(json);
            return regreso.GetCmbCompradoresResult;
        }

        public int DelComprador(int IdUser, int IdComprador)
        {
            Entity.DelCompradorResult_ regreso = JsonConvert.DeserializeObject<Entity.DelCompradorResult_>(methodPost("DelComprador/" + IdUser.ToString() + "/" + IdComprador.ToString()));
            return regreso.DelCompradorResult;
        }

        public int InsComprador(int IdUser, string Codigo, string Nombre, string Telefono, string Email)
        {
            Entity.Comprador cmpdor = new Entity.Comprador();
            cmpdor.Codigo = Codigo;
            cmpdor.Nombre = Nombre;
            cmpdor.Telefono = Telefono;
            cmpdor.Email = Email;

            Entity.InsCompradorResult_ regreso = JsonConvert.DeserializeObject<Entity.InsCompradorResult_>(methodPost("InsComprador/" + IdUser.ToString(), JsonConvert.SerializeObject(cmpdor)));
            return regreso.InsCompradorResult;
        }

        public int UpdComprador(int IdUser, int CompradorId, string Codigo, string Nombre, string Telefono, string Email)
        {
            Entity.Comprador cmpdor = new Entity.Comprador();
            cmpdor.CompradorID = CompradorId;
            cmpdor.Codigo = Codigo;
            cmpdor.Nombre = Nombre;
            cmpdor.Telefono = Telefono;
            cmpdor.Email = Email;

            Entity.UpdCompradorResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdCompradorResult_>(methodPost("UpdComprador/" + IdUser.ToString(), JsonConvert.SerializeObject(cmpdor)));
            return regreso.UpdCompradorResult;
        }

        public int ValComprador(int IdComprador, string Codigo, string Descripcion)
        {
            Entity.ValCompradorResult_ regreso = JsonConvert.DeserializeObject<Entity.ValCompradorResult_>(methodPost("ValComprador/" + IdComprador.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValCompradorResult;
        }

        public int DelCompradorSelected(int IdUser, string Valores)
        {
            Entity.DelCompradorSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelCompradorSelectedResult_>(methodPost("DelCompradorSelected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelCompradorSelectedResult;
        }

        public int DelCompradorAll(int IdUser, bool Activo)
        {
            Entity.DelCompradorAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelCompradorAllResult_>(methodPost("DelCompradorAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelCompradorAllResult;
        }
    }
}