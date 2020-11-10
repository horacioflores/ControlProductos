using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlProductos.dataAccess;
using ControlProductos.Entity;
using System.Web.UI.HtmlControls;
using System.Globalization;


namespace ControlProductos
{
    public partial class wellcome : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Indicadores();
            if (LoginInfo.CurrentUsuario.CodigoProveedor == "NA")
            {
                //fillMejoresCostos();
                //fillMejoresEntregas();
            }
        }

        public void fillGrid()
        {
            string Fecha = DateTime.Today.Date.ToString("yyyy-MM-dd");
        }

        //public void Indicadores()
        //{
        //    string idUsuario = LoginInfo.CurrentUsuario.UsuarioId.ToString();

        //    HomeDa home = new HomeDa();
        //    indicadores ind = new indicadores();
        //    ind = home.GetIndicadores(idUsuario);

        //    lblTotalMissing.Text = ind.faltantes.ToString();
        //    lblTotalQuoted.Text = ind.cotizados.ToString();
        //    lbldaysPass.Text = ind.transcurridos.ToString();
        //    lblRemaingdays.Text = ind.restantes.ToString();
        //}

        //public void fillMejoresCostos()
        //{
        //    var BGraficas = new GraficasDa();
        //    List<ProveedorMejorCosto> oListMejoresCostos = BGraficas.GetMejoresCostos();
        //    string strScript = returnscrptCostos(oListMejoresCostos);

        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptCts", strScript, true);
        //}
        //public string returnscrptCostos(List<ProveedorMejorCosto> list)
        //{
        //    string regreso = "";
        //    regreso = "Highcharts.chart('betterCost', { chart: { type: 'pie'},title:{text: 'Better Costs'},xAxis:{type: 'category'},yAxis: [{title:{text: 'Total ($)'}}],";
        //    regreso = regreso + "legend:{enabled: false},plotOptions:{series:{borderWidth: 0,dataLabels:{enabled: false,format: '${point.y:.2f}'}}},";
        //    regreso = regreso + "tooltip:{headerFormat: '<span style=\"font - size:11px\">{point.name}</span><br>',pointFormat: '<span style=\"font - size:11px\">{point.name}: <b>${point.y:.2f}</b></span><br/>'},";
        //    regreso = regreso + "series: [{colorByPoint: true,data: [";
        //    int index = -1;
        //    string drilldown = "";
        //    foreach (ProveedorMejorCosto pmc in list)
        //    {
        //        index++;
        //        if (index > 0)
        //        {
        //            regreso = regreso + ",";
        //            drilldown = drilldown + ",";
        //        }
        //        regreso = regreso + "{name:'" + pmc.Nombre + "', y:" + pmc.costo_total.ToString() + ", drilldown:'" + pmc.Codigo + "'}";
        //        drilldown = drilldown + "{id:'" + pmc.Codigo + "',name:'" + pmc.Nombre + "',colorByPoint: true,data: [";
        //        int index2 = -1;
        //        foreach (DetalleMejorCosto dmc in pmc.detalle)
        //        {
        //            index2++;
        //            if (index2 > 0)
        //            {
        //                drilldown = drilldown + ",";
        //            }
        //            drilldown = drilldown + "{name:'" + dmc.folio + "',y:" + dmc.costo_total.ToString() + ", drilldown:'" + dmc.CotizacionID.ToString() + "'}";
        //        }
        //        drilldown = drilldown + "]}";
        //    }
        //    regreso = regreso + "]}],drilldown:{series:[" + drilldown + "]}});";
        //    return regreso;
        //}

        //public void fillMejoresEntregas()
        //{
        //    var BGraficas = new GraficasDa();
        //    List<ProveedorMejorEntrega> oListMejoresEntregas = BGraficas.GetMejoresEntregas();
        //    string strScript = returnscrptEntregas(oListMejoresEntregas);

        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptEnt", strScript, true);
        //}
        //public string returnscrptEntregas(List<ProveedorMejorEntrega> list)
        //{
        //    string regreso = "Highcharts.chart('bestDelivery', {chart:{type: 'column'},title:{text: 'Best deliveries'},";
        //    regreso = regreso + "xAxis:{categories:[";
        //    int index = -1;
        //    string seriesData = "";
        //    foreach (ProveedorMejorEntrega pme in list)
        //    {
        //        index++;
        //        if (index > 0)
        //        {
        //            regreso = regreso + ",";
        //            seriesData = seriesData + ",";
        //        }
        //        regreso = regreso + "'" + pme.Nombre + "'";
        //        seriesData = seriesData + pme.dias.ToString();
        //    }
        //    regreso = regreso + "],crosshair: true},yAxis:{min: 0,title:{text: 'Días'}},";
        //    regreso = regreso + "tooltip:{headerFormat: '<span style=\"font-size:10px\">{point.key}</span><table>',";
        //    regreso = regreso + "pointFormat: '<tr><td style=\"color: { series.color}; padding: 0\">{series.name}: </td>' + '<td style=\"padding: 0\"><b>{point.y:.0f} días</b></td></tr>',";
        //    regreso = regreso + "footerFormat: '</table>',shared: true,useHTML: true},plotOptions:{column:{pointPadding: 0.2,borderWidth: 0}},";
        //    regreso = regreso + "series: [{name:'Proveedores',data:[" + seriesData + "]}]});";

        //    return regreso;
        //}
    }
}