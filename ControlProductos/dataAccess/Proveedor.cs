using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class ProveedorDa : Base
    {
        public Entity.Proveedor GetProveedor(int ProveedorId)
        {
            string json = methodGet("GetProveedor/" + ProveedorId.ToString());
            Entity.GetProveedorResult_ regreso = JsonConvert.DeserializeObject<Entity.GetProveedorResult_>(json);
            return regreso.GetProveedorResult;
        }

        //public List<Entity.ProveedorFabricante> GetFabricantes(int ProveedorId)
        //{
        //    string json = methodGet("getFabricantes/" + ProveedorId.ToString());
        //    Entity.getFabricantesResult_ regreso = JsonConvert.DeserializeObject<Entity.getFabricantesResult_>(json);
        //    return regreso.getFabricantesResult;
        //}

        public List<Entity.Proveedor> GetCatalog(string Codigo, string Nombre, bool Activo)
        {
            string json = methodGet("GetProveedores/" + Codigo + "/" + Nombre + "/" + Activo.ToString());
            Entity.GetProveedoresResult_ regreso = JsonConvert.DeserializeObject<Entity.GetProveedoresResult_>(json);
            return regreso.GetProveedoresResult;
        }

        public List<Entity.Proveedor> GetCombo()
        {
            string json = methodGet("GetCmbProveedores");
            Entity.GetCmbProveedoresResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbProveedoresResult_>(json);
            return regreso.GetCmbProveedoresResult;
        }

        public int InsProveedor(int UsuarioId, string key, string Nombre, string NombreCorto, string Direccion1, string Direccion2, string Ciudad, string Estado, string CP, string Pais, string Telefono, string Correo)
        {
            Entity.Proveedor prov = new Entity.Proveedor();
            prov.Codigo = key;
            prov.Nombre = Nombre;
            prov.NombreCorto = NombreCorto;
            prov.Direccion1 = Direccion1;
            prov.Direccion2 = Direccion2;
            prov.Ciudad = Ciudad;
            prov.Estado = Estado;
            prov.CP = CP;
            prov.Pais = Pais;
            prov.Telefono = Telefono;
            prov.Correo = Correo;

            Entity.InsProveedorResult_ regreso = JsonConvert.DeserializeObject<Entity.InsProveedorResult_>(methodPost("InsProveedor/" + UsuarioId.ToString(), JsonConvert.SerializeObject(prov)));
            return regreso.InsProveedorResult;
        }

        public int UpdProveedor(int UsuarioId, int ProveedorId, string key, string Nombre, string NombreCorto, string Direccion1, string Direccion2, string Ciudad, string Estado, string CP, string Pais, string Telefono, string Correo)
        {
            Entity.Proveedor prov = new Entity.Proveedor();
            prov.ProveedorId = ProveedorId;
            prov.Codigo = key;
            prov.Nombre = Nombre;
            prov.NombreCorto = NombreCorto;
            prov.Direccion1 = Direccion1;
            prov.Direccion2 = Direccion2;
            prov.Ciudad = Ciudad;
            prov.Estado = Estado;
            prov.CP = CP;
            prov.Pais = Pais;
            prov.Telefono = Telefono;
            prov.Correo = Correo;

            Entity.UpdProveedorResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdProveedorResult_>(methodPost("UpdProveedor/" + UsuarioId.ToString(), JsonConvert.SerializeObject(prov)));
            return regreso.UpdProveedorResult;
        }

        public int DelProveedor(int UsuarioId, int ProveedorId)
        {
            Entity.DelProveedorResult_ regreso = JsonConvert.DeserializeObject<Entity.DelProveedorResult_>(methodPost("DelProveedor/" + UsuarioId.ToString() + "/" + ProveedorId.ToString()));
            return regreso.DelProveedorResult;
        }

        public int ValProveedor(int ProveedorId, string key, string Nombre)
        {
            Entity.ValProveedorResult_ regreso = JsonConvert.DeserializeObject<Entity.ValProveedorResult_>(methodPost("ValProveedor/" + ProveedorId.ToString() + "/" + key + "/" + Nombre));
            return regreso.ValProveedorResult;
        }

        public int DelProveedorSelected(int IdUser, string Valores, bool Activo)
        {
            Entity.DelProveedorSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelProveedorSelectedResult_>(methodPost("DelProveedorSelected/" + IdUser.ToString() + "/" + Valores + "/" + Activo.ToString()));
            return regreso.DelProveedorSelectedResult;
        }

        public int DelProveedorAll(int IdUser, bool Activo)
        {
            Entity.DelProveedorAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelProveedorAllResult_>(methodPost("DelProveedorAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelProveedorAllResult;
        }
    }
}


