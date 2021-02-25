using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetEmpleadoResult_
    {
        public List<Empleados> GetEmpleadoResult { get; set; }
    }
    public class GetEmpleadosResult_
    {
        public List<Empleados> GetEmpleadosResult { get; set; }
    }
    public class GetCmbEmpleadosResult_
    {
        public List<Empleados> GetCmbEmpleadosResult { get; set; }
    }

    public class GetEmpleadoWithPerfilResult_
    {
        public List<Empleados> GetEmpleadoWithPerfilResult { get; set; }
    }

    public class DelEmpleadosResult_
    {
        public int DelEmpleadosResult { get; set; }
    }
    public class InsEmpleadosResult_
    {
        public int InsEmpleadosResult { get; set; }
    }
    public class UpdEmpleadosResult_
    {
        public int UpdEmpleadosResult { get; set; }
    }
    public class ValEmpleadosResult_
    {
        public int ValEmpleadosResult { get; set; }
    }
    public class DelEmpleadosSelectedResult_
    {
        public int DelEmpleadosSelectedResult { get; set; }
    }
    public class DelEmpleadosAllResult_
    {
        public int DelEmpleadosAllResult { get; set; }
    }
    public class UpdEmpleadosXlSResult_
    {
        public int UpdEmpleadosXlSResult { get; set; }
    }

    public class Empleados
    {
        public int EmpleadoId { get; set; }
        public string Codigo { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Credencial { get; set; }
        public string NSS { get; set; }
        public string CodigoPlanta { get; set; }
        public string Planta { get; set; }
        public string CodigoDepto { get; set; }
        public string Departamento { get; set; }
        public string CodigoPosicion { get; set; }
        public string Posicion { get; set; }
        public string CodigoProveedor { get; set; }
        public string Proveedor { get; set; }
        public bool Activo { get; set; }

        public string CodigoYNombre { get; set; }
        public string CodigoPerfil { get; set; }
    }
}