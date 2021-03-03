using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetCtrlProd_MisSolicitudesResult_
    {
        public List<ControlProductos> GetCtrlProd_MisSolicitudesResult { get; set; }
    }
    public class GetCtrlProd_MisPendientesResult_
    {
        public List<ControlProductos> GetCtrlProd_MisPendientesResult { get; set; }
    }
    public class GetCtrlProdResult_
    {
        public List<ControlProductos> GetCtrlProdResult { get; set; }
    }

    public class GetCtrlProdComboResult_
    {
        public List<ControlProductos> GetCtrlProdComboResult { get; set; }
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


    public class newDocResult_
    {
        public string newDocResult { get; set; }
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
        //public string CodigoUtilizado { get; set; }
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
        //public bool esUnico { get; set; }
        //public string fechaCotizacion { get; set; }
        public decimal precioUnitario { get; set; }
        public int diasEntrega { get; set; }
        public string Codigomoneda { get; set; }
        //public decimal total { get; set; }
        //public string fichaDatoSeguridad { get; set; }
        public string CodigoUM { get; set; }
        public string conteoCiclico { get; set; }
        //public string almacenamientoExternoPosible { get; set; }
        public string codigoPlaneador { get; set; }
        public string codigoComprador { get; set; }
        //public string fechaInventario { get; set; }
        public string multiplo { get; set; }
        //public string hojaSeguridad { get; set; }
        public string codigoArticulo { get; set; }
        public string comentarios { get; set; }
        public string codigo_sts_Prods { get; set; }
        public string sts_Prods { get; set; }
        public string usuario { get; set; }
        public string ModFecha { get; set; }
        public string operacion { get; set; }
        public int version { get; set; }
        public bool vigente { get; set; }
        public string producto { get; set; }
        public string codigoYNombre { get; set; }

        
        public string descripcion { get; set; }
        public string modelo { get; set; }
        public string codActFijo { get; set; }
        public string subcuenta { get; set; }
        public decimal consEstimado { get; set; }
        public string unidadMedida { get; set; }
        public decimal cantMinima { get; set; }
        public string FechaRequerida { get; set; }
        public string numOQ { get; set; }
        public decimal precio { get; set; }
        public string numOrden { get; set; }
        public string reparar { get; set; }

        public string GlClass { get; set; }
        public string textBusq { get; set; }
        public string CodigoProveedor_comp { get; set; }
        public string PaisOrigen { get; set; }
        public string MTDOCoste_Inv { get; set; }
        public string MTDOCoste_Pursh { get; set; }

        public string codigo_tipoEmpaque { get; set; }
        public int piezasEmpaque { get; set; }
        public string UMEmpaque { get; set; }
        public int alto { get; set; }
        public int ancho { get; set; }
        public int largo { get; set; }
        public string pursh1 { get; set; }
        public string pursh2 { get; set; }
        public string codigoFamilia { get; set; }
        public string branch { get; set; }
        public string diasStok { get; set; }

        public string ubicacionPrim { get; set; }
        public string ubicacionSec { get; set; }
        public string umAlmacen { get; set; }
        public int alto_alm { get; set; }
        public int ancho_alm { get; set; }
        public int largo_alm { get; set; }
        public string monedaMtto { get; set; }
        public string sigPerfil { get; set; }
                
        public string Marca { get; set; }
        public string Comprador { get; set; }
        public string Subcategoria1 { get; set; }        
        public string Departamento { get; set; }        
        public decimal totalAnual { get; set; }
        public string monedaComprador { get; set; }
        public decimal montoMensual { get; set; }

        public List<archivos> archivos { get; set; }

        public List<_tipoArticulo> tiposArticulo { get; set; }
        public List<Mtto_Almn> mantenimientos { get; set; }
        public List<Mtto_Almn> almacenes { get; set; }
        public List<Aprobacion> aprobaciones { get; set; }
    }

    public class archivos
    {
        public int ctrlPArchivosID { get; set; }
        public string noDocumento { get; set; }
        public string codigoTipoDocumento { get; set; }
        public string descripcion { get; set; }
        public string archivo { get; set; }
        public string TipoDocumento { get; set; }
    }

    public class _tipoArticulo
    {
        public int ctrlPTipoArticuloID { get; set; }
        public string noDocumento { get; set; }
        public string codigoTipoArticulo { get; set; }
        public string tipoArticulo { get; set; }
        public string M { get; set; }
        public string N { get; set; }
        public string comentarios { get; set; }
        public string valor { get; set; }
    }

    public class Mtto_Almn
    {
        public int ctrlPMantenimientoID { get; set; }
        public string noDocumento { get; set; }
        public string codigoMttoAlmn { get; set; }
        public string especificacion { get; set; }
        public string notas { get; set; }
        public string clasificacion { get; set; }
        public string responsable { get; set; }
        public string tipo { get; set; }
        public bool selected { get; set; }
    }

    public class Aprobacion
    {
        public int AprobacionesID { get; set; }
        public string noDocumento { get; set; }
        public string codigoPerfil { get; set; }
        public string codigoEmpleado { get; set; }
        public string paso { get; set; }
        public string titulo { get; set; }
        public string usuario { get; set; }
        public string puesto { get; set; }
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