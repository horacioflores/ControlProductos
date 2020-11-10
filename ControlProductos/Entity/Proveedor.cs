using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetProveedorResult_
    {
        public Proveedor GetProveedorResult { get; set; }
    }
    public class GetProveedoresResult_
    {
        public List<Proveedor> GetProveedoresResult { get; set; }
    }
    public class GetCmbProveedoresResult_
    {
        public List<Proveedor> GetCmbProveedoresResult { get; set; }
    }

    //public class getFabricantesResult_
    //{
    //    public List<ProveedorFabricante> getFabricantesResult { get; set; }
    //}

    public class InsProveedorResult_
    {
        public int InsProveedorResult { get; set; }
    }
    public class UpdProveedorResult_
    {
        public int UpdProveedorResult { get; set; }
    }
    public class DelProveedorResult_
    {
        public int DelProveedorResult { get; set; }
    }
    public class ValProveedorResult_
    {
        public int ValProveedorResult { get; set; }
    }
    public class DelProveedorSelectedResult_
    {
        public int DelProveedorSelectedResult { get; set; }
    }
    public class DelProveedorAllResult_
    {
        public int DelProveedorAllResult { get; set; }
    }


    public class Proveedor
	{
	    public int ProveedorId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string Direccion1 { get; set; }
        public string Direccion2 { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CP { get; set; }
        public string Pais { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
        public string CodigoYNombre { get; set; }

        //public List<ProveedorFabricante> fabricantes { get; set; }
    }

    //public class ProveedorFabricante
    //{
    //    public int ProvFabricantesId { get; set; }   
    //    public int ProveedorId { get; set; }
    //    public int FabricanteId { get; set; }
    //    public string CodigoProveedor { get; set; }
    //    public string CodigoFabricante { get; set; }
    //    public string Proveedor { get; set; }
    //    public string Fabricante { get; set; }
    //}
}

