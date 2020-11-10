using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class EmpleadosDa : Base
    {
        public List<Entity.Empleados> GetEmpleado(string Codigo)
        {
            string json = methodGet("GetEmpleado/" + Codigo);
            Entity.GetEmpleadoResult_ regreso = JsonConvert.DeserializeObject<Entity.GetEmpleadoResult_>(json);
            return regreso.GetEmpleadoResult;
        }

        public List<Entity.Empleados> GetCatalog(string CodigoPosicion, string CodigoProveedor, string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetEmpleados/" + CodigoPosicion + "/" + CodigoProveedor + "/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetEmpleadosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetEmpleadosResult_>(json);
            return regreso.GetEmpleadosResult;
        }

        public List<Entity.Empleados> GetCombo(string CodigoPosicion)
        {
            string json = methodGet("GetCmbEmpleados/" + CodigoPosicion);
            Entity.GetCmbEmpleadosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbEmpleadosResult_>(json);
            return regreso.GetCmbEmpleadosResult;
        }

        public System.Data.DataSet GetCatalogEmployees(string CodigoPosicion, string CodigoProveedor, string Codigo, string Descripcion, bool Activo)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Entity.Empleados> empleados = GetCatalog(CodigoPosicion, CodigoProveedor, Codigo, Descripcion, Activo);
            dt = ADataTable<Entity.Empleados>(empleados);
            if(dt.Rows.Count > 0)
            {
                ds.Tables.Add(dt);
            }
            return ds;
        }

        public int DelEmpleados(int IdUser, int IdEmpleados)
        {
            Entity.Empleados emp = new Entity.Empleados();
            emp.EmpleadoId = IdEmpleados;

            Entity.DelEmpleadosResult_ regreso = JsonConvert.DeserializeObject<Entity.DelEmpleadosResult_>(methodPost("DelEmpleados/" + IdUser.ToString(), JsonConvert.SerializeObject(emp)));
            return regreso.DelEmpleadosResult;
        }

        public int InsEmpleados(int UsuarioId, string Codigo, string NombreCompleto, string EMail, string Credencial, string NSS, string CodigoPlanta, string CodigoDepto, string CodigoPosicion, string CodigoProveedor)
        {
            Entity.Empleados emp = new Entity.Empleados();
            emp.Codigo = Codigo;
            emp.NombreCompleto = NombreCompleto;
            emp.Email = EMail;
            emp.Credencial = Credencial;
            emp.NSS = NSS;
            emp.CodigoPlanta = CodigoPlanta;
            emp.CodigoDepto = CodigoDepto;
            emp.CodigoPosicion = CodigoPosicion;
            emp.CodigoProveedor = CodigoProveedor;

            Entity.InsEmpleadosResult_ regreso = JsonConvert.DeserializeObject<Entity.InsEmpleadosResult_>(methodPost("InsEmpleados/" + UsuarioId.ToString(), JsonConvert.SerializeObject(emp)));
            return regreso.InsEmpleadosResult;
        }

        public int UpdEmpleados(int UsuarioId, int EmpleadoId, string Codigo, string NombreCompleto, string EMail, string Credencial, string NSS, string CodigoPlanta, string CodigoDepto, string CodigoPosicion, string CodigoProveedor)
        {
            Entity.Empleados emp = new Entity.Empleados();
            emp.EmpleadoId = EmpleadoId;
            emp.Codigo = Codigo;
            emp.NombreCompleto = NombreCompleto;
            emp.Email = EMail;
            emp.Credencial = Credencial;
            emp.NSS = NSS;
            emp.CodigoPlanta = CodigoPlanta;
            emp.CodigoDepto = CodigoDepto;
            emp.CodigoPosicion = CodigoPosicion;
            emp.CodigoProveedor = CodigoProveedor;

            Entity.UpdEmpleadosResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdEmpleadosResult_>(methodPost("UpdEmpleados/" + UsuarioId.ToString(), JsonConvert.SerializeObject(emp)));
            return regreso.UpdEmpleadosResult;
        }

        public int ValEmpleados(int IdEmpleados, string Codigo, string Descripcion)
        {
            Entity.Empleados emp = new Entity.Empleados();
            emp.EmpleadoId = IdEmpleados;
            emp.Codigo = Codigo;
            emp.NombreCompleto = Descripcion;

            Entity.ValEmpleadosResult_ regreso = JsonConvert.DeserializeObject<Entity.ValEmpleadosResult_>(methodPost("ValEmpleados", JsonConvert.SerializeObject(emp)));
            return regreso.ValEmpleadosResult;
        }

        public int DelEmpleadosSelected(int IdUser, string Valores, bool Activo)
        {
            Entity.DelEmpleadosSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelEmpleadosSelectedResult_>(methodPost("DelEmpleadosSelected/"+ IdUser .ToString()+ "/"+ Valores + "/" + Activo.ToString()));
            return regreso.DelEmpleadosSelectedResult;
        }

        public int DelEmpleadosAll(int IdUser, string CodigoPosicion, string CodigoProveedor, bool Activo)
        {
            Entity.DelEmpleadosAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelEmpleadosAllResult_>(methodPost("DelEmpleadosAll/" + IdUser.ToString() + "/" + CodigoPosicion + "/" + CodigoProveedor + "/" + Activo.ToString()));
            return regreso.DelEmpleadosAllResult;
        }

        public int UpdEmpleadosXlS(string xmlListaEmpleados, int UsuarioId)
        {
            Entity.UpdEmpleadosXlSResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdEmpleadosXlSResult_>(methodPost("UpdEmpleadosXlS/" + xmlListaEmpleados + "/" + UsuarioId.ToString()));
            return regreso.UpdEmpleadosXlSResult;
        }
    }
}