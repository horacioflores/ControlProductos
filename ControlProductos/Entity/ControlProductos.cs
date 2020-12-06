using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetCtrlProdResult_
    {
        public List<ControlProductos> GetCtrlProdResult { get; set; }
    }

    public class GetCtrlProductoResult_
    {
        public List<ControlProductos> GetCtrlProductoResult { get; set; }
    }

    public class InsCtrlPResult_
    {
        public int InsCtrlPResult { get; set; }
    }

    public class UpdCtrlPResult_
    {
        public int UpdCtrlPResult { get; set; }
    }

    public class ControlProductos
    {
        public int ctrlProdsID { get; set; }
        public string noDocumento { get; set; }
        public string codigoSolicitante { get; set; }
        public string fechaSolicitud { get; set; }
        public string remplazaOtro { get; set; }
        public string cualArticulo { get; set; }
        public string CodigoMaquina { get; set; }
        public string CodigoSubcategoria1 { get; set; }
        public string CodigoSubcategoria2 { get; set; }
        public string CodigoSubcategoria3 { get; set; }
        public string CodigoUtilizado { get; set; }
        public string CodigoDepto { get; set; }
        public string descripcionUno { get; set; }
        public string descripcionDos { get; set; }
        public string descripcionLargo { get; set; }
        public string CodigoPlan { get; set; }
        public string comoAyudarStockCero { get; set; }
        public string funcionMaquina { get; set; }
        public decimal cantidadOrden { get; set; }
        public decimal stockMinimo { get; set; }
        public decimal stockMaximo { get; set; }
        public string CodigoMarca { get; set; }
        public string CodigoProveedor { get; set; }
        public bool esUnico { get; set; }
        public string fechaCotizacion { get; set; }
        public decimal precioUnitario { get; set; }
        public int diasEntrega { get; set; }
        public string Codigomoneda { get; set; }
        public decimal total { get; set; }
        public string fichaDatoSeguridad { get; set; }
        public string CodigoUM { get; set; }
        public string conteoCiclico { get; set; }
        public string almacenamientoExternoPosible { get; set; }
        public string codigoPlaneador { get; set; }
        public string codigoComprador { get; set; }
        public string fichaInventario { get; set; }
        public string multiplo { get; set; }
        public string hojaSeguridad { get; set; }
        public string codigoArticulo { get; set; }
        public string comentarios { get; set; }
        public string codigo_sts_Prods { get; set; }
        public string sts_Prods { get; set; }
        public string usuario { get; set; }
        public string ModFecha { get; set; }
        public List<_tipoArticulo> tiposArticulo { get; set; }
        public List<Mantenimiento> mantenimientos { get; set; }
        public List<Almacen> almacenes { get; set; }
        public List<Aprobacion> aprobaciones { get; set; }
    }

    public class _tipoArticulo
    {
        public int tipoArticuloID { get; set; }
        public string noDocumento { get; set; }
        public string codigoTipoArticulo { get; set; }
        public string tipoArticulo { get; set; }
        public string M { get; set; }
        public string N { get; set; }
        public string comentarios { get; set; }
    }

    public class Mantenimiento
    {
        public int MantenimientoID { get; set; }
        public string noDocumento { get; set; }
        public string codigoMantenimiento { get; set; }
        public string especificacion { get; set; }
        public string notas { get; set; }
        public string clasificacion { get; set; }
        public string responsable { get; set; }
    }

    public class Almacen
    {
        public int AlmacenID { get; set; }
        public string noDocumento { get; set; }
        public string codigoAlmacen { get; set; }
        public string especificacion { get; set; }
        public string notas { get; set; }
        public string clasificacion { get; set; }
        public string responsable { get; set; }
    }

    public class Aprobacion
    {
        public int AprobacionesID { get; set; }
        public string noDocumento { get; set; }
        public string codigoAprobaciones { get; set; }
        public string paso { get; set; }
        public string titulo { get; set; }
        public string codigoEmpleado { get; set; }
        public string fechaNotificacion { get; set; }
        public string fechaAccion { get; set; }
        public string accion { get; set; }
        public string comentario { get; set; }
    }


    public class Unico
    {
        public bool value { get; set; }
        public string text { get; set; }
    }
}