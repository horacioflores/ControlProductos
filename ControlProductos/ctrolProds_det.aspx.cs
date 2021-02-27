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
using System.Data;
using DevExpress.Spreadsheet;
using System.IO;
using DevExpress.XtraPrinting;

namespace ControlProductos
{
    public partial class ctrolProds_det : BasePage
    {
        string UploadDirectory = "~/Upload/";

        public string SelectedFile
        {
            get { return (string)Session["SelectedFile"]; }
            set { Session["SelectedFile"] = value; }
        }

        public string SelectedFileName
        {
            get { return (string)Session["SelectedFileName"]; }
            set { Session["SelectedFileName"] = value; }
        }

        public List<archivos> archivos
        {
            get
            {
                return (List<archivos>)Session["Currentarchivos"];
            }
            set
            {
                Session["Currentarchivos"] = value;
            }
        }

        public List<_tipoArticulo> tiposArticulo
        {
            get
            {
                return (List<_tipoArticulo>)Session["CurrenttiposArticulo"];
            }
            set
            {
                Session["CurrenttiposArticulo"] = value;
            }
        }

        //public List<Mtto_Almn> mttos
        //{
        //    get
        //    {
        //        return (List<Mtto_Almn>)Session["CurrentMttos"];
        //    }
        //    set
        //    {
        //        Session["CurrentMttos"] = value;
        //    }
        //}

        //public List<Mtto_Almn> almnes
        //{
        //    get
        //    {
        //        return (List<Mtto_Almn>)Session["CurrentAlmnes"];
        //    }
        //    set
        //    {
        //        Session["CurrentAlmnes"] = value;
        //    }
        //}

        public List<Aprobacion> aprbnes
        {
            get
            {
                return (List<Aprobacion>)Session["Currentaprbnes"];
            }
            set
            {
                Session["Currentaprbnes"] = value;
            }
        }

        public List<TipoArticulo> tArticulos
        {
            get
            {
                return (List<TipoArticulo>)Session["CurrentTArticulo"];
            }
            set
            {
                Session["CurrentTArticulo"] = value;
            }
        }

        public List<MttoAlmn> tMttoAlmn
        {
            get
            {
                return (List<MttoAlmn>)Session["CurrentTMttoAlmn"];
            }
            set
            {
                Session["CurrentTMttoAlmn"] = value;
            }
        }

        public void ApplyLayoutTipoArticulo()
        {
            xgrdTipoArticuloMDL.DataSource = tArticulos;
            xgrdTipoArticuloMDL.DataBind();
        }

        //public void ApplyLayoutMtto()
        //{
        //    xgrdMttoMDL.DataSource = tMttoAlmn.FindAll(item => item.tipo == "M");
        //    xgrdMttoMDL.DataBind();
        //}

        //public void ApplyLayoutAlmnes()
        //{
        //    xgrdAlmacenMDL.DataSource = tMttoAlmn.FindAll(item => item.tipo == "A");
        //    xgrdAlmacenMDL.DataBind();
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ctrlProdsID"] == null)
            {
                Response.Redirect("ctrolProds.aspx");
            }
            else
            {
                init();
            }
        }

        private void init()
        {
            //var BJDE = new JDEda();
            var BJDE = new JDETwoda();
            cmbSubcuenta.TextField = "descripcion";
            cmbSubcuenta.ValueField = "codigo";
            cmbSubcuenta.DataSource = BJDE.GetCombo();
            cmbSubcuenta.DataBind();

            cmbUM.TextField = "descripcion";
            cmbUM.ValueField = "codigo";
            cmbUM.DataSource = BJDE.GetCombo();
            cmbUM.DataBind();

            cmbOQ.TextField = "descripcion";
            cmbOQ.ValueField = "codigo";
            cmbOQ.DataSource = BJDE.GetCombo();
            cmbOQ.DataBind();

            cmbGlClass.TextField = "descripcion";
            cmbGlClass.ValueField = "codigo";
            cmbGlClass.DataSource = BJDE.GetCombo();
            cmbGlClass.DataBind();

            cmbPaisOrigen.TextField = "descripcion";
            cmbPaisOrigen.ValueField = "codigo";
            cmbPaisOrigen.DataSource = BJDE.GetCombo();
            cmbPaisOrigen.DataBind();

            cmbMtdoCosteInv.TextField = "descripcion";
            cmbMtdoCosteInv.ValueField = "codigo";
            cmbMtdoCosteInv.DataSource = BJDE.GetCombo();
            cmbMtdoCosteInv.DataBind();

            cmbMtdoCostePursh.TextField = "descripcion";
            cmbMtdoCostePursh.ValueField = "codigo";
            cmbMtdoCostePursh.DataSource = BJDE.GetCombo();
            cmbMtdoCostePursh.DataBind();

            cmbPursh1.TextField = "descripcion";
            cmbPursh1.ValueField = "codigo";
            cmbPursh1.DataSource = BJDE.GetCombo();
            cmbPursh1.DataBind();

            cmbPursh2.TextField = "descripcion";
            cmbPursh2.ValueField = "codigo";
            cmbPursh2.DataSource = BJDE.GetCombo();
            cmbPursh2.DataBind();

            cmbbranch.TextField = "descripcion";
            cmbbranch.ValueField = "codigo";
            cmbbranch.DataSource = BJDE.GetCombo();
            cmbbranch.DataBind();

            cmbUbicacionPrim.TextField = "descripcion";
            cmbUbicacionPrim.ValueField = "codigo";
            cmbUbicacionPrim.DataSource = BJDE.GetCombo();
            cmbUbicacionPrim.DataBind();

            cmbUbicacionSec.TextField = "descripcion";
            cmbUbicacionSec.ValueField = "codigo";
            cmbUbicacionSec.DataSource = BJDE.GetCombo();
            cmbUbicacionSec.DataBind();

            var BActFijo = new ActFijoda();
            cmbActFijo.TextField = "descripcion";
            cmbActFijo.ValueField = "codigo";
            cmbActFijo.DataSource = BActFijo.GetCombo();
            cmbActFijo.DataBind();

            var BTipoEmqt = new tipoEmpaqueda();
            cmbTipoEmaque.TextField = "descripcion";
            cmbTipoEmaque.ValueField = "codigo";
            cmbTipoEmaque.DataSource = BActFijo.GetCombo();
            cmbTipoEmaque.DataBind();

            var BFamilia = new familiada();
            cmbfamilia.TextField = "descripcion";
            cmbfamilia.ValueField = "codigo";
            cmbfamilia.DataSource = BFamilia.GetCombo();
            cmbfamilia.DataBind();

            var BtipoDoc = new tipoDocumentoda();
            cmbTipoDoc.TextField = "descripcion";
            cmbTipoDoc.ValueField = "codigo";
            cmbTipoDoc.DataSource = BtipoDoc.GetTiposDocumentos();
            cmbTipoDoc.DataBind();

            var BCtrlProd = new ControlProductosda();
            cmbProducto.TextField = "codigoYNombre";
            cmbProducto.ValueField = "codigoArticulo";
            cmbProducto.DataSource = BCtrlProd.GetCombo();
            cmbProducto.DataBind();

            cmbCualArticulo.TextField = "codigoYNombre";
            cmbCualArticulo.ValueField = "codigoArticulo";
            cmbCualArticulo.DataSource = BCtrlProd.GetCombo();
            cmbCualArticulo.DataBind();

            var BMaquina = new MaquinaDa();
            cmbMaquina.TextField = "Nombre";
            cmbMaquina.ValueField = "Codigo";
            cmbMaquina.DataSource = BMaquina.GetCombo();
            cmbMaquina.DataBind();

            var BSubcategoria1 = new SubCategoria1Da();
            cmbSubCat1.TextField = "Nombre";
            cmbSubCat1.ValueField = "Codigo";
            cmbSubCat1.DataSource = BSubcategoria1.GetCombo();
            cmbSubCat1.DataBind();

            var BSubcategoria2 = new SubCategoria2Da();
            cmbSubCat2.TextField = "Nombre";
            cmbSubCat2.ValueField = "Codigo";
            cmbSubCat2.DataSource = BSubcategoria2.GetCombo();
            cmbSubCat2.DataBind();

            var BSubcategoria3 = new SubCategoria3Da();
            cmbSubCat3.TextField = "Nombre";
            cmbSubCat3.ValueField = "Codigo";
            cmbSubCat3.DataSource = BSubcategoria3.GetCombo();
            cmbSubCat3.DataBind();

            //var BUtilizado = new UtilizadoDa();
            //cmbUtilizado.TextField = "Nombre";
            //cmbUtilizado.ValueField = "Codigo";
            //cmbUtilizado.DataSource = BUtilizado.GetCombo();
            //cmbUtilizado.DataBind();

            var BDepartamento = new DepartamentoDa();
            cmbDepa.TextField = "Descripcion";
            cmbDepa.ValueField = "Codigo";
            cmbDepa.DataSource = BDepartamento.GetCombo();
            cmbDepa.DataBind();

            var BPlan = new PlanDa();
            cmbPlan.TextField = "Nombre";
            cmbPlan.ValueField = "Codigo";
            cmbPlan.DataSource = BPlan.GetCombo();
            cmbPlan.DataBind();

            var BMarca = new MarcaDa();
            cmbMarca.TextField = "Nombre";
            cmbMarca.ValueField = "Codigo";
            cmbMarca.DataSource = BMarca.GetCombo();
            cmbMarca.DataBind();

            var BProveedor = new ProveedorDa();
            cmbProveedor.TextField = "Nombre";
            cmbProveedor.ValueField = "Codigo";
            cmbProveedor.DataSource = BProveedor.GetCombo();
            cmbProveedor.DataBind();

            cmbProveedorComp.TextField = "Nombre";
            cmbProveedorComp.ValueField = "Codigo";
            cmbProveedorComp.DataSource = BProveedor.GetCombo();
            cmbProveedorComp.DataBind();
            

            //List<Unico> lUnico = new List<Unico>();
            //Unico item = new Unico();
            //item.value = true;
            //item.text = "Si";
            //Unico item2 = new Unico();
            //item2.value = false;
            //item2.text = "No";
            //lUnico.Add(item);
            //lUnico.Add(item2);

            //cmbUnico.TextField = "text";
            //cmbUnico.ValueField = "value";
            //cmbUnico.DataSource = lUnico;
            //cmbUnico.DataBind();

            var BMoneda = new MonedaDa();
            cmbMoneda.TextField = "Nombre";
            cmbMoneda.ValueField = "Codigo";
            cmbMoneda.DataSource = BMoneda.GetCombo();
            cmbMoneda.DataBind();

            cmbMonedaMtoo.TextField = "Nombre";
            cmbMonedaMtoo.ValueField = "Codigo";
            cmbMonedaMtoo.DataSource = BMoneda.GetCombo();
            cmbMonedaMtoo.DataBind();

            cmbMonedaComprador.TextField = "Nombre";
            cmbMonedaComprador.ValueField = "Codigo";
            cmbMonedaComprador.DataSource = BMoneda.GetCombo();
            cmbMonedaComprador.DataBind();

            var BUM = new UMedidaDa();
            cmbCodigoUM.TextField = "Nombre";
            cmbCodigoUM.ValueField = "Codigo";
            cmbCodigoUM.DataSource = BUM.GetCombo();
            cmbCodigoUM.DataBind();

            cmbUmEmpaque.TextField = "Nombre";
            cmbUmEmpaque.ValueField = "Codigo";
            cmbUmEmpaque.DataSource = BUM.GetCombo();
            cmbUmEmpaque.DataBind();

            cmbUMAlmacen.TextField = "Nombre";
            cmbUMAlmacen.ValueField = "Codigo";
            cmbUMAlmacen.DataSource = BUM.GetCombo();
            cmbUMAlmacen.DataBind();

            var BPlaneador = new PlaneadorDa();
            cmbPlaneador.TextField = "Nombre";
            cmbPlaneador.ValueField = "Codigo";
            cmbPlaneador.DataSource = BPlaneador.GetCombo();
            cmbPlaneador.DataBind();

            var BComprador = new CompradorDa();
            cmbComprador.TextField = "Nombre";
            cmbComprador.ValueField = "Codigo";
            cmbComprador.DataSource = BComprador.GetCombo();
            cmbComprador.DataBind();

            var BConteo = new ConteoDa();
            cmbConteoCiclico.TextField = "Nombre";
            cmbConteoCiclico.ValueField = "Codigo";
            cmbConteoCiclico.DataSource = BConteo.GetCombo();
            cmbConteoCiclico.DataBind();

            var BDias = new diasda();
            cmbDias.TextField = "descripcion";
            cmbDias.ValueField = "codigo";
            cmbDias.DataSource = BDias.GetCombo();
            cmbDias.DataBind();

            var tArtc = new TipoArticuloDa();
            var tMttoD = new MttoAlmnDa();
            List<TipoArticulo> lTipoArt = tArtc.GetCatalog("", "", true);
            List<MttoAlmn> lmtto = tMttoD.GetCatalog("", "", true);
            tArticulos = lTipoArt;
            tMttoAlmn = lmtto;
            ApplyLayoutTipoArticulo();
            //ApplyLayoutMtto();
            //ApplyLayoutAlmnes();

            if (!IsPostBack)
            {
                if (cmbSubcuenta.Items.Count > 0)
                {
                    ListEditItem li = cmbSubcuenta.Items[0];
                    li.Selected = true;
                }

                if (cmbUM.Items.Count > 0)
                {
                    ListEditItem li = cmbUM.Items[0];
                    li.Selected = true;
                }

                if (cmbGlClass.Items.Count > 0)
                {
                    ListEditItem li = cmbGlClass.Items[0];
                    li.Selected = true;
                }

                if (cmbPaisOrigen.Items.Count > 0)
                {
                    ListEditItem li = cmbPaisOrigen.Items[0];
                    li.Selected = true;
                }

                if (cmbMtdoCosteInv.Items.Count > 0)
                {
                    ListEditItem li = cmbMtdoCosteInv.Items[0];
                    li.Selected = true;
                }

                if (cmbMtdoCostePursh.Items.Count > 0)
                {
                    ListEditItem li = cmbMtdoCostePursh.Items[0];
                    li.Selected = true;
                }

                if (cmbbranch.Items.Count > 0)
                {
                    ListEditItem li = cmbbranch.Items[0];
                    li.Selected = true;
                }

                if (cmbPursh1.Items.Count > 0)
                {
                    ListEditItem li = cmbPursh1.Items[0];
                    li.Selected = true;
                }

                if (cmbPursh2.Items.Count > 0)
                {
                    ListEditItem li = cmbPursh2.Items[0];
                    li.Selected = true;
                }

                if (cmbUbicacionPrim.Items.Count > 0)
                {
                    ListEditItem li = cmbUbicacionPrim.Items[0];
                    li.Selected = true;
                }

                if (cmbUbicacionSec.Items.Count > 0)
                {
                    ListEditItem li = cmbUbicacionSec.Items[0];
                    li.Selected = true;
                }

                if (cmbfamilia.Items.Count > 0)
                {
                    ListEditItem li = cmbfamilia.Items[0];
                    li.Selected = true;
                }

                if (cmbOQ.Items.Count > 0)
                {
                    ListEditItem li = cmbOQ.Items[0];
                    li.Selected = true;
                }

                if (cmbActFijo.Items.Count > 0)
                {
                    ListEditItem li = cmbActFijo.Items[0];
                    li.Selected = true;
                }

                if (cmbUmEmpaque.Items.Count > 0)
                {
                    ListEditItem li = cmbUmEmpaque.Items[0];
                    li.Selected = true;
                }

                if (cmbTipoEmaque.Items.Count > 0)
                {
                    ListEditItem li = cmbTipoEmaque.Items[0];
                    li.Selected = true;
                }

                if (cmbMaquina.Items.Count > 0)
                {
                    ListEditItem li = cmbMaquina.Items[0];
                    li.Selected = true;
                }
                if (cmbSubCat1.Items.Count > 0)
                {
                    ListEditItem li = cmbSubCat1.Items[0];
                    li.Selected = true;
                }
                if (cmbSubCat2.Items.Count > 0)
                {
                    ListEditItem li = cmbSubCat2.Items[0];
                    li.Selected = true;
                }
                if (cmbSubCat3.Items.Count > 0)
                {
                    ListEditItem li = cmbSubCat3.Items[0];
                    li.Selected = true;
                }
                //if (cmbUtilizado.Items.Count > 0)
                //{
                //    ListEditItem li = cmbUtilizado.Items[0];
                //    li.Selected = true;
                //}
                if (cmbDepa.Items.Count > 0)
                {
                    ListEditItem li = cmbDepa.Items[0];
                    li.Selected = true;
                }
                if (cmbPlan.Items.Count > 0)
                {
                    ListEditItem li = cmbPlan.Items[0];
                    li.Selected = true;
                }
                if (cmbMarca.Items.Count > 0)
                {
                    ListEditItem li = cmbMarca.Items[0];
                    li.Selected = true;
                }
                if (cmbProveedor.Items.Count > 0)
                {
                    ListEditItem li = cmbProveedor.Items[0];
                    li.Selected = true;
                }
                if (cmbProveedorComp.Items.Count > 0)
                {
                    ListEditItem li = cmbProveedorComp.Items[0];
                    li.Selected = true;
                }
                //if (cmbUnico.Items.Count > 0)
                //{
                //    ListEditItem li = cmbUnico.Items[0];
                //    li.Selected = true;
                //}
                if (cmbMoneda.Items.Count > 0)
                {
                    ListEditItem li = cmbMoneda.Items[0];
                    li.Selected = true;
                }

                if (cmbMonedaMtoo.Items.Count > 0)
                {
                    ListEditItem li = cmbMonedaMtoo.Items[0];
                    li.Selected = true;
                }


                if (cmbCodigoUM.Items.Count > 0)
                {
                    ListEditItem li = cmbCodigoUM.Items[0];
                    li.Selected = true;
                }
                if (cmbUMAlmacen.Items.Count > 0)
                {
                    ListEditItem li = cmbUMAlmacen.Items[0];
                    li.Selected = true;
                }
                if (cmbPlaneador.Items.Count > 0)
                {
                    ListEditItem li = cmbPlaneador.Items[0];
                    li.Selected = true;
                }
                if (cmbComprador.Items.Count > 0)
                {
                    ListEditItem li = cmbComprador.Items[0];
                    li.Selected = true;
                }
                if (cmbConteoCiclico.Items.Count > 0)
                {
                    ListEditItem li = cmbConteoCiclico.Items[0];
                    li.Selected = true;
                }

                if (cmbDias.Items.Count > 0)
                {
                    ListEditItem li = cmbDias.Items[0];
                    li.Selected = true;
                }

                refresData();
                //productosToRel = new List<cotizacion_prodRelProv>();
            }
            else
            {
                xgrdTipoArticulo.DataSource = tiposArticulo;
                xgrdTipoArticulo.DataBind();
                xgrdArchivos.DataSource = archivos;
                xgrdArchivos.DataBind();
                //xgrdMtto.DataSource = mttos;
                //xgrdMtto.DataBind();
                //xgrdAlmacen.DataSource = almnes;
                //xgrdAlmacen.DataBind();
                xgrdAprobaciones.DataSource = aprbnes;
                xgrdAprobaciones.DataBind();
                ApplyLayoutTipoArticulo();
                //ApplyLayoutMtto();
                //ApplyLayoutAlmnes();
            }
        }

        public void refresData()
        {
            var tArtc = new TipoArticuloDa();
            List<TipoArticulo> lTipoArt = tArtc.GetCatalog("", "", true);

            limpiarModalAprnes();
            int ctrlProdsID = Convert.ToInt32(Session["ctrlProdsID"]);
            var BctrlProd = new ControlProductosda();
            List<Entity.ControlProductos> oList = BctrlProd.GetCtrlProducto(ctrlProdsID.ToString());
            if (ctrlProdsID > 0)
            {
                foreach (Entity.ControlProductos ctrlP in oList)
                {
                    if (ctrlProdsID == ctrlP.ctrlProdsID)
                    {
                        lblcodigoSts.Text = ctrlP.codigo_sts_Prods;
                        lblSigPerf.Text = ctrlP.sigPerfil;
                        switch (ctrlP.sts_Prods)
                        {
                            case "Abierto":
                                btnEnviarSolicitante.Visible = true;
                                ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-edit' style='padding-right:5px;'></i>" + ctrlP.sts_Prods + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Pendiente por el autor para terminar la captura</span>";
                                break;
                            case "En Aprobación por ":
                                string perfil = "";
                                btnSave.Visible = false;

                                if (ctrlP.sigPerfil != LoginInfo.CurrentPerfil.Codigo)
                                {
                                    btnEnviarSolicitante.Visible = false;
                                }
                                else
                                {
                                    btnEnviarSolicitante.Visible = true;
                                }

                                switch (ctrlP.sigPerfil)
                                {
                                    case "0002":
                                        perfil = "Comprador";
                                        break;
                                    case "0003":
                                        perfil = "PLANNER";
                                        break;
                                    case "0004":
                                        perfil = "ALMACÉN";
                                        break;
                                    case "0005":
                                        perfil = "GTE MANTENIMIENTO";
                                        break;
                                    case "0006":
                                        perfil = "GTE COMPRAS";
                                        if (ctrlP.operacion == "BAJA")
                                        {
                                            btnEnviarDM.Visible = true;
                                        }

                                        break;
                                    case "0007":
                                        perfil = "DIRECCION";
                                        break;
                                }

                                ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-eye-open' style='padding-right:5px;'></i>" + ctrlP.sts_Prods + perfil + "</span>";
                                break;
                            case "Pendiente por Data Management":
                                btnSave.Visible = false;

                                if (ctrlP.sigPerfil != LoginInfo.CurrentPerfil.Codigo)
                                {
                                    btnEnviarSolicitante.Visible = false;
                                }
                                ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-eye-open' style='padding-right:5px;'></i>" + ctrlP.sts_Prods + "</span>";
                                break;
                            case "Aprobado":
                                btnSave.Visible = false;
                                btnEnviarSolicitante.Visible = false;
                                ltlSts.Text = "<span id='spanStatus' class='alert btn-success docEstatus' ><i class='glyphicon glyphicon-ok' style='padding-right:5px;'></i>" + ctrlP.sts_Prods + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Aprobado por " + ctrlP.usuario + " el " + ctrlP.ModFecha + "</span>";
                                break;
                        }

                        if (ctrlP.sigPerfil == LoginInfo.CurrentPerfil.Codigo)
                        {
                            List<Aprobacion> lprob = ctrlP.aprobaciones.FindAll(a => a.codigoPerfil == LoginInfo.CurrentPerfil.Codigo);

                            EmpleadosDa empDa = new EmpleadosDa();
                            List<Empleados> emps = empDa.GetCatalog("", "", "", "", true);
                            string codigoEmpAprb = "";

                            foreach (Empleados item in emps)
                            {
                                if (item.EmpleadoId == LoginInfo.CurrentUsuario.EmpleadoId)
                                {
                                    codigoEmpAprb = item.Codigo;
                                }
                            }

                            if (lprob.Count > 0)
                            {
                                if (codigoEmpAprb != lprob[0].codigoEmpleado)
                                {
                                    btnSave.Visible = false;
                                    btnEnviarSolicitante.Visible = false;
                                    btnEnviarDM.Visible = false;
                                }
                            }
                            
                        }

                        lblnDoc.Text = ctrlP.noDocumento;
                        lblDocSolicitante.Text = ctrlP.usuario;
                        lblDocFechaSol.Text = ctrlP.fechaSolicitud;
                        rbAlta.Enabled = false;
                        rbModificacion.Enabled = false;
                        rbBaja.Enabled = false;
                        cmbProducto.Enabled = false;
                        switch (ctrlP.operacion)
                        {
                            case "ALTA":
                                rbAlta.Checked = true;
                                lblProd.Visible = false;
                                cmbProducto.Visible = false;
                                dvReemplazaOtro.Visible = true;
                                break;
                            case "MODIFICACIÓN":
                                rbModificacion.Checked = true;
                                lblProd.Visible = true;
                                cmbProducto.Visible = true;
                                dvReemplazaOtro.Visible = false;
                                break;
                            case "BAJA":
                                rbBaja.Checked = true;
                                lblProd.Visible = true;
                                cmbProducto.Visible = true;
                                dvReemplazaOtro.Visible = false;
                                break;
                        }
                        ListEditItem oItProd = cmbProducto.Items.FindByValue(ctrlP.producto);
                        if (oItProd != null)
                        {
                            oItProd.Selected = true;
                        }

                        rbSi.Enabled = false;
                        rbNo.Enabled = false;
                        cmbCualArticulo.Enabled = false;
                        switch (ctrlP.remplazaOtro)
                        {
                            case "Si":
                                rbSi.Checked = true;
                                lblcual.Visible = true;
                                cmbCualArticulo.Visible = true;
                                break;
                            case "No":
                                rbNo.Checked = true;
                                lblcual.Visible = false;
                                cmbCualArticulo.Visible = false;
                                break;
                        }

                        ListEditItem oItcual = cmbCualArticulo.Items.FindByValue(ctrlP.cualArticulo);
                        if (oItcual != null)
                        {
                            oItcual.Selected = true;
                        }
                        txtdescripcion.Text = ctrlP.descripcion;
                        txtModelo.Text = ctrlP.modelo;

                        ListEditItem oItsubc = cmbSubcuenta.Items.FindByValue(ctrlP.subcuenta);
                        if (oItsubc != null)
                        {
                            oItsubc.Selected = true;
                        }
                        ListEditItem oItcodActFijo = cmbActFijo.Items.FindByValue(ctrlP.codActFijo);
                        if (oItcodActFijo != null)
                        {
                            oItcodActFijo.Selected = true;
                        }


                        archivos = new List<archivos>();
                        archivos = ctrlP.archivos;
                        xgrdArchivos.DataSource = archivos;
                        xgrdArchivos.DataBind();

                        ListEditItem oItMaquina = cmbMaquina.Items.FindByValue(ctrlP.CodigoMaquina);
                        if (oItMaquina != null)
                        {
                            oItMaquina.Selected = true;
                        }

                        ListEditItem oItmSubCat1 = cmbSubCat1.Items.FindByValue(ctrlP.CodigoSubcategoria1);
                        if (oItmSubCat1 != null)
                        {
                            oItmSubCat1.Selected = true;
                        }

                        ListEditItem oItmSubCat2 = cmbSubCat2.Items.FindByValue(ctrlP.CodigoSubcategoria2);
                        if (oItmSubCat2 != null)
                        {
                            oItmSubCat2.Selected = true;
                        }

                        ListEditItem oItmSubCat3 = cmbSubCat3.Items.FindByValue(ctrlP.CodigoSubcategoria3);
                        if (oItmSubCat3 != null)
                        {
                            oItmSubCat3.Selected = true;
                        }

                        //ListEditItem oItmUtilizado = cmbUtilizado.Items.FindByValue(ctrlP.CodigoUtilizado);
                        //if (oItmUtilizado != null)
                        //{
                        //    oItmUtilizado.Selected = true;
                        //}

                        ListEditItem oItmDepto = cmbDepa.Items.FindByValue(ctrlP.CodigoDepto);
                        if (oItmDepto != null)
                        {
                            oItmDepto.Selected = true;
                        }
                        tiposArticulo = new List<_tipoArticulo>();
                        tiposArticulo = ctrlP.tiposArticulo;
                        xgrdTipoArticulo.DataSource = ctrlP.tiposArticulo;
                        xgrdTipoArticulo.DataBind();

                        int M = 0;
                        foreach (_tipoArticulo art in ctrlP.tiposArticulo)
                        {
                            if (art.valor == "M")
                            {
                                M++;
                            }
                        }
                        if (M > 5)
                        {
                            lblstock.Text = "M";
                            lblstock2.Text = "M: Con Stock";
                        }
                        else
                        {
                            lblstock.Text = "N";
                            lblstock2.Text = "N: Con Stock";
                        }

                        txtDescripcion1.Text = ctrlP.descripcionUno;
                        txtDescripcion2.Text = ctrlP.descripcionDos;
                        txtDescripcionLarga.Text = ctrlP.descripcionDos;

                        ListEditItem oItmPlan = cmbPlan.Items.FindByValue(ctrlP.CodigoPlan);
                        if (oItmPlan != null)
                        {
                            oItmPlan.Selected = true;
                        }

                        txtcomoAyudarStockCero.Text = ctrlP.comoAyudarStockCero;
                        txtfuncionMaquina.Text = ctrlP.funcionMaquina;
                        txtCantidad.Text = ctrlP.cantidadOrden.ToString("0.##");
                        txtStockMin.Text = ctrlP.stockMinimo.ToString("0.##");
                        txtStockMax.Text = ctrlP.stockMaximo.ToString("0.##");

                        ListEditItem oItmMarca = cmbMarca.Items.FindByValue(ctrlP.CodigoMarca);
                        if (oItmMarca != null)
                        {
                            oItmMarca.Selected = true;
                        }

                        ListEditItem oItmProveedor = cmbProveedor.Items.FindByValue(ctrlP.CodigoProveedor);
                        if (oItmProveedor != null)
                        {
                            oItmProveedor.Selected = true;
                        }


                        //ListEditItem oItmUnico= cmbUnico.Items.FindByValue(ctrlP.esUnico.ToString());
                        //if (oItmUnico != null)
                        //{
                        //    oItmUnico.Selected = true;
                        //}
                        //DateTime fechaCot = new DateTime(Convert.ToInt32(ctrlP.fechaCotizacion.Substring(6, 4)), Convert.ToInt32(ctrlP.fechaCotizacion.Substring(3, 2)), Convert.ToInt32(ctrlP.fechaCotizacion.Substring(0, 2)));
                        //xDateFechaCot.Date = fechaCot;
                        txtPrecioU.Text = ctrlP.precioUnitario.ToString("0.##");
                        txtDiasEntrega.Text = ctrlP.diasEntrega.ToString();

                        ListEditItem oItmMoneda = cmbMoneda.Items.FindByValue(ctrlP.Codigomoneda);
                        if (oItmMoneda != null)
                        {
                            oItmMoneda.Selected = true;
                        }
                        ListEditItem oItmMonedaMtto = cmbMonedaMtoo.Items.FindByValue(ctrlP.monedaMtto);
                        if (oItmMonedaMtto != null)
                        {
                            oItmMonedaMtto.Selected = true;
                        }
                        //lblTotal.Text = ctrlP.total.ToString("0.##");

                        //mttos = new List<Mtto_Almn>();
                        //mttos = ctrlP.mantenimientos;
                        //xgrdMtto.DataSource = ctrlP.mantenimientos;
                        //xgrdMtto.DataBind();

                        //almnes = new List<Mtto_Almn>();
                        //almnes = ctrlP.almacenes;
                        //xgrdAlmacen.DataSource = ctrlP.almacenes;
                        //xgrdAlmacen.DataBind();

                        //if(ctrlP.fichaDatoSeguridad == "Si")
                        //{
                        //    rbFichaSi.Checked = true;
                        //    rbFichaNo.Checked = false;
                        //    dvHojaSeg1.Visible = true;
                        //    dvHojaSeg2.Visible = true;
                        //}
                        //else
                        //{
                        //    rbFichaSi.Checked = false;
                        //    rbFichaNo.Checked = true;
                        //    dvHojaSeg1.Visible = false;
                        //    dvHojaSeg2.Visible = false;
                        //}

                        ListEditItem oItmUM = cmbCodigoUM.Items.FindByValue(ctrlP.CodigoUM);
                        if (oItmUM != null)
                        {
                            oItmUM.Selected = true;
                        }

                        //txtConteoCiclico.Text = ctrlP.conteoCiclico;
                        ListEditItem oItmCC = cmbConteoCiclico.Items.FindByValue(ctrlP.conteoCiclico);
                        if (oItmCC != null)
                        {
                            oItmCC.Selected = true;
                        }

                        //if (ctrlP.almacenamientoExternoPosible == "Si")
                        //{
                        //    rbAlmExtSi.Checked = true;
                        //}
                        //else
                        //{
                        //    rbAlmExtNo.Checked = false;
                        //}

                        ListEditItem oItmPlaneador = cmbPlaneador.Items.FindByValue(ctrlP.codigoPlaneador);
                        if (oItmPlaneador != null)
                        {
                            oItmPlaneador.Selected = true;
                        }

                        ListEditItem oItmComprador = cmbComprador.Items.FindByValue(ctrlP.codigoComprador);
                        if (oItmComprador != null)
                        {
                            oItmComprador.Selected = true;
                        }

                        //if(ctrlP.fechaInventario != "")
                        //{
                        //    DateTime fInv = new DateTime(Convert.ToInt32(ctrlP.fechaInventario.Substring(6,4)), Convert.ToInt32(ctrlP.fechaInventario.Substring(3, 2)), Convert.ToInt32(ctrlP.fechaInventario.Substring(0, 2)));
                        //    xDateFechaInv.Date = fInv;
                        //}

                        txtMultiplo.Text = ctrlP.multiplo;
                        //txtFile.Text = ctrlP.hojaSeguridad;
                        txtCodigoArticulo.Text = ctrlP.codigoArticulo;

                        aprbnes = new List<Aprobacion>();
                        aprbnes = ctrlP.aprobaciones;

                        foreach (Aprobacion item in aprbnes)
                        {
                            if (item.codigoEmpleado == "")
                            {
                                EmpleadosDa eDa = new EmpleadosDa();
                                List<Empleados> lemp = eDa.GetCatalog("", "", "", "", true);
                                foreach (Empleados item2 in lemp)
                                {
                                    lemp = lemp.FindAll(e => e.CodigoPerfil == item.codigoPerfil);
                                    if (lemp.Count > 0)
                                    {
                                        item.codigoEmpleado = lemp[0].Codigo;
                                    }
                                }
                            }
                        }

                        xgrdAprobaciones.DataSource = ctrlP.aprobaciones;
                        xgrdAprobaciones.DataBind();

                        txtConsEstimado.Text = ctrlP.consEstimado.ToString();
                        ListEditItem oItmUM2 = cmbUM.Items.FindByValue(ctrlP.unidadMedida);
                        if (oItmUM2 != null)
                        {
                            oItmUM2.Selected = true;
                        }
                        txtCantMinima.Text = ctrlP.cantMinima.ToString();
                        DateTime fechaReq = new DateTime(Convert.ToInt32(ctrlP.FechaRequerida.Substring(6, 4)), Convert.ToInt32(ctrlP.FechaRequerida.Substring(3, 2)), Convert.ToInt32(ctrlP.FechaRequerida.Substring(0, 2)));
                        xDateFechaReq.Date = fechaReq;

                        ListEditItem oItmNOQ = cmbOQ.Items.FindByValue(ctrlP.numOQ);
                        if (oItmNOQ != null)
                        {
                            oItmNOQ.Selected = true;
                        }
                        txtPrecio.Text = ctrlP.precio.ToString();
                        txtContrato.Text = ctrlP.numOrden;
                        switch (ctrlP.reparar)
                        {
                            case "Si":
                                rbReparaSi.Checked = true;
                                break;
                            case "No":
                                rbReparaNo.Checked = true;
                                break;
                        }

                        ListEditItem oItmglclass = cmbGlClass.Items.FindByValue(ctrlP.GlClass);
                        if (oItmglclass != null)
                        {
                            oItmglclass.Selected = true;
                        }
                        txtTextBusq.Text = ctrlP.textBusq;
                        ListEditItem oItmProveedorComp = cmbProveedorComp.Items.FindByValue(ctrlP.CodigoProveedor_comp);
                        if (oItmProveedorComp != null)
                        {
                            oItmProveedorComp.Selected = true;
                        }
                        ListEditItem oItmPais = cmbPaisOrigen.Items.FindByValue(ctrlP.PaisOrigen);
                        if (oItmPais != null)
                        {
                            oItmPais.Selected = true;
                        }
                        ListEditItem oItmMtdoInv = cmbMtdoCosteInv.Items.FindByValue(ctrlP.MTDOCoste_Inv);
                        if (oItmMtdoInv != null)
                        {
                            oItmMtdoInv.Selected = true;
                        }
                        ListEditItem oItmMtdoPursh = cmbPaisOrigen.Items.FindByValue(ctrlP.MTDOCoste_Pursh);
                        if (oItmMtdoPursh != null)
                        {
                            oItmMtdoPursh.Selected = true;
                        }

                        ListEditItem oItmTipoEmq = cmbTipoEmaque.Items.FindByValue(ctrlP.codigo_tipoEmpaque);
                        if (oItmTipoEmq != null)
                        {
                            oItmTipoEmq.Selected = true;
                        }
                        txtPiezaEmp.Text = ctrlP.piezasEmpaque.ToString();
                        ListEditItem oItmUmTempq = cmbUmEmpaque.Items.FindByValue(ctrlP.UMEmpaque);
                        if (oItmUmTempq != null)
                        {
                            oItmUmTempq.Selected = true;
                        }
                        txtAlto.Text = ctrlP.piezasEmpaque.ToString();
                        txtAncho.Text = ctrlP.piezasEmpaque.ToString();
                        txtLargo.Text = ctrlP.piezasEmpaque.ToString();

                        ListEditItem oItmpursh1 = cmbPursh1.Items.FindByValue(ctrlP.pursh1);
                        if (oItmpursh1 != null)
                        {
                            oItmpursh1.Selected = true;
                        }
                        ListEditItem oItmpursh2 = cmbPursh1.Items.FindByValue(ctrlP.pursh2);
                        if (oItmpursh2 != null)
                        {
                            oItmpursh2.Selected = true;
                        }
                        ListEditItem oItmfamilia = cmbfamilia.Items.FindByValue(ctrlP.codigoFamilia);
                        if (oItmfamilia != null)
                        {
                            oItmfamilia.Selected = true;
                        }
                        ListEditItem oItmbranch = cmbbranch.Items.FindByValue(ctrlP.branch);
                        if (oItmbranch != null)
                        {
                            oItmbranch.Selected = true;
                        }
                        ListEditItem oItmdias = cmbDias.Items.FindByValue(ctrlP.diasStok);
                        if (oItmdias != null)
                        {
                            oItmdias.Selected = true;
                        }
                        ListEditItem oItmUbiPrim = cmbUbicacionPrim.Items.FindByValue(ctrlP.ubicacionPrim);
                        if (oItmUbiPrim != null)
                        {
                            oItmUbiPrim.Selected = true;
                        }
                        ListEditItem oItmUbiSec = cmbUbicacionSec.Items.FindByValue(ctrlP.ubicacionSec);
                        if (oItmUbiSec != null)
                        {
                            oItmUbiSec.Selected = true;
                        }
                        ListEditItem oItmumALm = cmbUMAlmacen.Items.FindByValue(ctrlP.umAlmacen);
                        if (oItmumALm != null)
                        {
                            oItmumALm.Selected = true;
                        }
                        txtAltoAlm.Text = ctrlP.alto_alm.ToString();
                        txtAnchoAlm.Text = ctrlP.ancho_alm.ToString();
                        txtLargoAlm.Text = ctrlP.largo_alm.ToString();

                        break;
                    }
                }
            }
            else
            {
                DateTime date = new DateTime();
                date = DateTime.Now;
                lblcodigoSts.Text = "0001";
                ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-edit' style='padding-right:5px;'></i>Abierto</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Pendiente por el autor para terminar la captura</span>";
                lblnDoc.Text = BctrlProd.newDoc();
                lblDocSolicitante.Text = LoginInfo.CurrentUsuario.NombreCompleto;
                lblDocFechaSol.Text = date.ToString("dd/MM/yyyy HH:mm");
                rbAlta.Checked = true;
                dvReemplazaOtro.Visible = true;
                lblProd.Visible = false;
                cmbProducto.Visible = false;
                rbNo.Checked = true;
                lblcual.Visible = false;
                cmbCualArticulo.Visible = false;
                txtdescripcion.Text = "";
                txtModelo.Text = "";
                archivos = new List<archivos>();
                xgrdArchivos.DataSource = archivos;
                xgrdArchivos.DataBind();
                xDateFechaReq.Date = date;
                btnEnviarSolicitante.Visible = false;

                tiposArticulo = new List<_tipoArticulo>();
                //mttos = new List<Mtto_Almn>();
                //almnes = new List<Mtto_Almn>();


                foreach (TipoArticulo TipoArt in lTipoArt)
                {
                    _tipoArticulo n = new _tipoArticulo();
                    n.ctrlPTipoArticuloID = 0;
                    n.noDocumento = "";
                    n.codigoTipoArticulo = TipoArt.codigoTipoArticulo;
                    n.tipoArticulo = TipoArt.tipoArticulo;
                    n.M = TipoArt.M;
                    n.N = TipoArt.N;
                    n.comentarios = TipoArt.comentarios;
                    n.valor = "M";
                    tiposArticulo.Add(n);
                }
                xgrdTipoArticulo.DataSource = tiposArticulo;
                xgrdTipoArticulo.DataBind();

                var tMtto = new MttoAlmnDa();
                List<MttoAlmn> ltMtto = tMtto.GetCatalog("", "", true);
                foreach (MttoAlmn mtto in ltMtto)
                {
                    Mtto_Almn n = new Mtto_Almn();
                    n.ctrlPMantenimientoID = 0;
                    n.noDocumento = "";
                    n.codigoMttoAlmn = mtto.codigoMttoAlmn;
                    n.especificacion = mtto.especificacion;
                    n.notas = mtto.notas;
                    n.clasificacion = mtto.clasificacion;
                    n.responsable = mtto.responsable;
                    n.tipo = mtto.tipo;
                    //if(mtto.tipo == "M")
                    //{
                    //    mttos.Add(n);
                    //}
                    //else
                    //{
                    //    almnes.Add(n);
                    //}
                }
                //xgrdMtto.DataSource = mttos;
                //xgrdMtto.DataBind();
                //xgrdAlmacen.DataSource = almnes;
                //xgrdAlmacen.DataBind();


                txtDescripcion1.Text = "";
                txtDescripcion2.Text = "";
                txtDescripcionLarga.Text = "";

                txtcomoAyudarStockCero.Text = "";
                txtfuncionMaquina.Text = "";
                txtCantidad.Text = "0";
                txtStockMin.Text = "0";
                txtStockMax.Text = "0";

                txtPrecioU.Text = "0";
                txtDiasEntrega.Text = "0";

                //lblTotal.Text = "0";

                //rbFichaSi.Checked = true;

                ListEditItem oItmCC = cmbConteoCiclico.Items[0];
                if (oItmCC != null)
                {
                    oItmCC.Selected = true;
                }
                //rbAlmExtSi.Checked = true;


                txtMultiplo.Text = "";
                //txtFile.Text = "";
                txtCodigoArticulo.Text = "";

                aprbnes = new List<Aprobacion>();
                PerfilDa perda = new PerfilDa();
                List<Perfil> lperf = perda.GetCatalog("", true);

                foreach (Perfil perf in lperf)
                {
                    EmpleadosDa eDa = new EmpleadosDa();
                    List<Empleados> lemp = eDa.GetCatalog("", "", "", "", true);
                    string empleadoCod = "";
                    if (perf.Codigo == "0001" && LoginInfo.CurrentPerfil.Codigo == "0001")
                    {
                        for (int i = 0; i < lemp.Count; i++)
                        {
                            if (lemp[i].EmpleadoId == LoginInfo.CurrentUsuario.EmpleadoId)
                            {
                                empleadoCod = lemp[i].Codigo;
                                break;
                            }
                        }
                    }
                    else
                    {
                        lemp = lemp.FindAll(e => e.CodigoPerfil == perf.Codigo);
                        empleadoCod = lemp[0].Codigo;
                    }

                    Aprobacion aprb = new Aprobacion();
                    aprb.AprobacionesID = 0;
                    aprb.noDocumento = lblnDoc.Text;
                    aprb.codigoPerfil = perf.Codigo;
                    aprb.codigoEmpleado = empleadoCod;
                    aprb.paso = "";
                    aprb.titulo = "";
                    aprb.usuario = "";
                    aprb.puesto = "";
                    aprb.fechaNotificacion = "";
                    aprb.fechaAccion = "";
                    aprb.accion = "";
                    aprb.comentario = "";
                    aprbnes.Add(aprb);
                }
                xgrdAprobaciones.DataSource = aprbnes;
                xgrdAprobaciones.DataBind();

                rbReparaNo.Checked = true;

                //productos = new List<cotizacion_prod>();
                //proveedores = new List<cotizacion_proveedor>();
                //comentarios = new List<Entity.cotizacion_comentarios>();
                //archivos = new List<cotizacion_archivos>();
            }
        }

        public void limpiarModalAprnes()
        {
            //txtCodigoAprobacionAdd.Text = "";
            //txtPasoAdd.Text = "";
            //txtTituloAdd.Text = "";
            //txtAccionAdd.Text = "";
            //txtComentarioAdd.Text = "";

            //var BPosicion = new PosicionDa();
            //cmbPuestoAdd.TextField = "Descripcion";
            //cmbPuestoAdd.ValueField = "Codigo";
            //List<Posicion> posiciones = BPosicion.GetCombo(true);
            //cmbPuestoAdd.DataSource = posiciones;
            //cmbPuestoAdd.DataBind();

            //if (posiciones.Count > 0)
            //{
            //    ListEditItem iPuesto = cmbPuestoAdd.Items.FindByValue(posiciones[0].Codigo);
            //    if (iPuesto != null)
            //    {
            //        iPuesto.Selected = true;
            //    }
            //    var BEmpleado = new EmpleadosDa();
            //    cmbEmpleadoAdd.TextField = "NombreCompleto";
            //    cmbEmpleadoAdd.ValueField = "EmpleadoId";
            //    cmbEmpleadoAdd.DataSource = BEmpleado.GetCombo(posiciones[0].Codigo);
            //    cmbEmpleadoAdd.DataBind();

            //    if (cmbEmpleadoAdd.Items.Count > 0)
            //    {
            //        ListEditItem iEmp = cmbEmpleadoAdd.Items.FindByValue(cmbEmpleadoAdd.Items[0].Value);
            //        if (iEmp != null)
            //        {
            //            iEmp.Selected = true;
            //        }
            //    }
            //}

            //DateTime fechaActual = DateTime.Now;
            //xDateFechaReq.Date = fechaActual;
            //xDateFechaNotAdd.Date = fechaActual;
            //xDateFechaAccionAdd.Date = fechaActual;
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ctrolProds.aspx");
        }

        protected string Save()
        {
            string remplazaOtro = (rbSi.Checked) ? "Si" : "No";
            string repara = (rbReparaSi.Checked) ? "Si" : "No";
            string operacion;
            if (rbAlta.Checked)
            {
                operacion = "ALTA";
            }
            else if (rbModificacion.Checked)
            {
                operacion = "MODIFICACIÓN";
            }
            else
            {
                operacion = "BAJA";
            }

            int ctrlProdsID = Convert.ToInt32(Session["ctrlProdsID"]);
            Entity.ControlProductos ctrlProd = new Entity.ControlProductos();

            ctrlProd.ctrlProdsID = ctrlProdsID;
            ctrlProd.noDocumento = lblnDoc.Text;
            ctrlProd.codigoSolicitante = LoginInfo.CurrentUsuario.Codigo;
            ctrlProd.fechaSolicitud = lblDocFechaSol.Text.Substring(6, 4) + lblDocFechaSol.Text.Substring(3, 2) + lblDocFechaSol.Text.Substring(0, 2) + " " + lblDocFechaSol.Text.Substring(11, 5);
            ctrlProd.remplazaOtro = remplazaOtro;
            ctrlProd.cualArticulo = (remplazaOtro == "Si")?cmbCualArticulo.SelectedItem.Value.ToString():"";
            ctrlProd.CodigoMaquina = cmbMaquina.SelectedItem.Value.ToString();
            ctrlProd.CodigoSubcategoria1 = cmbSubCat1.SelectedItem.Value.ToString();
            ctrlProd.CodigoSubcategoria2 = cmbSubCat2.SelectedItem.Value.ToString();
            ctrlProd.CodigoSubcategoria3 = cmbSubCat2.SelectedItem.Value.ToString();
            //ctrlProd.CodigoUtilizado = cmbUtilizado.SelectedItem.Value.ToString();
            ctrlProd.CodigoDepto = cmbDepa.SelectedItem.Value.ToString();
            ctrlProd.descripcionUno = txtDescripcion1.Text;
            ctrlProd.descripcionDos = txtDescripcion2.Text;
            ctrlProd.descripcionLargo = txtDescripcionLarga.Text;
            ctrlProd.CodigoPlan = cmbPlan.SelectedItem.Value.ToString();
            ctrlProd.comoAyudarStockCero = txtcomoAyudarStockCero.Text;
            ctrlProd.funcionMaquina = txtfuncionMaquina.Text;
            ctrlProd.cantidadOrden = (txtCantidad.Text == "")?0:Convert.ToDecimal(txtCantidad.Text);
            ctrlProd.stockMinimo = (txtStockMin.Text == "") ? 0 : Convert.ToDecimal(txtStockMin.Text);
            ctrlProd.stockMaximo = (txtStockMax.Text == "") ? 0 : Convert.ToDecimal(txtStockMax.Text);
            ctrlProd.CodigoMarca = cmbMarca.SelectedItem.Value.ToString();
            ctrlProd.CodigoProveedor = cmbProveedor.SelectedItem.Value.ToString();
            //ctrlProd.esUnico = Convert.ToBoolean(cmbUnico.SelectedItem.Value);
            //ctrlProd.fechaCotizacion = (xDateFechaCot.Value.ToString() == "")?DateTime.Now.ToString("yyyyMMdd"): xDateFechaCot.Date.ToString("yyyyMMdd");
            ctrlProd.precioUnitario = (txtPrecioU.Text == "") ? 0 : Convert.ToDecimal(txtPrecioU.Text);
            ctrlProd.diasEntrega = (txtDiasEntrega.Text == "") ? 0 : Convert.ToInt32(txtDiasEntrega.Text);
            ctrlProd.Codigomoneda = cmbMoneda.SelectedItem.Value.ToString();
            //ctrlProd.total = (lblTotal.Text == "") ? 0 : Convert.ToDecimal(lblTotal.Text);
            //ctrlProd.fichaDatoSeguridad = (rbFichaSi.Checked)?"Si":"No";
            ctrlProd.CodigoUM = cmbCodigoUM.SelectedItem.Value.ToString();
            ctrlProd.conteoCiclico = cmbConteoCiclico.SelectedItem.Value.ToString();
            //ctrlProd.almacenamientoExternoPosible = (rbAlmExtSi.Checked) ? "Si" : "No";
            ctrlProd.codigoPlaneador = cmbPlaneador.SelectedItem.Value.ToString();
            ctrlProd.codigoComprador = cmbComprador.SelectedItem.Value.ToString();
            //ctrlProd.fechaInventario = (xDateFechaInv.Value == null) ? null : xDateFechaInv.Date.ToString("yyyyMMdd");
            ctrlProd.multiplo = txtMultiplo.Text;
            //ctrlProd.hojaSeguridad = txtFile.Text;
            ctrlProd.codigoArticulo = txtCodigoArticulo.Text;
            ctrlProd.comentarios = txtComentarios.Text;
            ctrlProd.codigo_sts_Prods = lblcodigoSts.Text;
            ctrlProd.operacion = operacion;
            if (operacion != "ALTA")
            {
                ctrlProd.producto = cmbProducto.SelectedItem.Value.ToString();
            }
            else
            {
                ctrlProd.producto = "";
            }
            ctrlProd.descripcion = txtdescripcion.Text;
            ctrlProd.modelo = txtModelo.Text;
            ctrlProd.subcuenta = cmbSubcuenta.SelectedItem.Value.ToString();
            ctrlProd.codActFijo = cmbActFijo.SelectedItem.Value.ToString();
            ctrlProd.consEstimado = (txtConsEstimado.Text == "") ? 0 : Convert.ToDecimal(txtConsEstimado.Text);
            ctrlProd.UMEmpaque = cmbUM.SelectedItem.Value.ToString();
            ctrlProd.cantMinima = (txtCantMinima.Text == "") ? 0 : Convert.ToDecimal(txtCantMinima.Text);
            ctrlProd.FechaRequerida = (xDateFechaReq.Value == null) ? null : xDateFechaReq.Date.ToString("yyyyMMdd");
            ctrlProd.numOQ = cmbOQ.SelectedItem.Value.ToString();
            ctrlProd.precio = (txtPrecio.Text == "") ? 0 : Convert.ToDecimal(txtPrecio.Text);
            ctrlProd.numOrden = txtContrato.Text;
            ctrlProd.GlClass = cmbGlClass.SelectedItem.Value.ToString();
            ctrlProd.textBusq = txtTextBusq.Text;
            ctrlProd.CodigoProveedor_comp = cmbProveedorComp.SelectedItem.Value.ToString();
            ctrlProd.PaisOrigen = cmbPaisOrigen.SelectedItem.Value.ToString();
            ctrlProd.MTDOCoste_Inv = cmbMtdoCosteInv.SelectedItem.Value.ToString();
            ctrlProd.MTDOCoste_Pursh = cmbMtdoCostePursh.SelectedItem.Value.ToString();
            ctrlProd.codigo_tipoEmpaque = cmbTipoEmaque.SelectedItem.Value.ToString();
            ctrlProd.piezasEmpaque = (txtPiezaEmp.Text == "") ? 0 : Convert.ToInt32(txtPiezaEmp.Text);
            ctrlProd.UMEmpaque = cmbUmEmpaque.SelectedItem.Value.ToString();
            ctrlProd.alto = (txtAlto.Text == "") ? 0 : Convert.ToInt32(txtAlto.Text);
            ctrlProd.ancho = (txtAncho.Text == "") ? 0 : Convert.ToInt32(txtAncho.Text);
            ctrlProd.largo = (txtLargo.Text == "") ? 0 : Convert.ToInt32(txtLargo.Text);
            ctrlProd.pursh1 = cmbPursh1.SelectedItem.Value.ToString();
            ctrlProd.pursh2 = cmbPursh2.SelectedItem.Value.ToString();
            ctrlProd.codigoFamilia = cmbfamilia.SelectedItem.Value.ToString();
            ctrlProd.branch = cmbbranch.SelectedItem.Value.ToString();
            ctrlProd.diasStok = cmbDias.SelectedItem.Value.ToString();
            ctrlProd.ubicacionPrim = cmbUbicacionPrim.SelectedItem.Value.ToString();
            ctrlProd.ubicacionSec = cmbUbicacionSec.SelectedItem.Value.ToString();
            ctrlProd.umAlmacen = cmbUMAlmacen.SelectedItem.Value.ToString();
            ctrlProd.alto_alm = (txtAltoAlm.Text == "") ? 0 : Convert.ToInt32(txtAltoAlm.Text);
            ctrlProd.ancho_alm = (txtAnchoAlm.Text == "") ? 0 : Convert.ToInt32(txtAnchoAlm.Text);
            ctrlProd.largo_alm = (txtLargoAlm.Text == "") ? 0 : Convert.ToInt32(txtLargoAlm.Text);

            foreach (_tipoArticulo item in tiposArticulo)
            {
                item.noDocumento = lblnDoc.Text;
            }
            //foreach (Mtto_Almn item in mttos)
            //{
            //    item.noDocumento = lblnDoc.Text;
            //}
            //foreach (Mtto_Almn item in almnes)
            //{
            //    item.noDocumento = lblnDoc.Text;
            //}
            ctrlProd.archivos = archivos;
            ctrlProd.tiposArticulo = tiposArticulo;
            ctrlProd.reparar = repara;
            ctrlProd.monedaMtto = cmbMonedaMtoo.SelectedItem.Value.ToString();
            //ctrlProd.mantenimientos = mttos;
            //ctrlProd.almacenes = almnes;

            List<Aprobacion> aprbInsert = new List<Aprobacion>();

            foreach (Aprobacion prb in aprbnes)
            {
                DateTime fecha = new DateTime();
                fecha = DateTime.Now;
                Aprobacion nuevo = new Aprobacion();
                nuevo = prb;
                nuevo.fechaNotificacion = fecha.ToString().Substring(6, 4) + fecha.ToString().Substring(3, 2) + fecha.ToString().Substring(0, 2);
                nuevo.fechaAccion = fecha.ToString().Substring(6, 4) + fecha.ToString().Substring(3, 2) + fecha.ToString().Substring(0, 2);
                aprbInsert.Add(nuevo);
            }

            ctrlProd.aprobaciones = aprbInsert;

            var BctrlProd = new ControlProductosda();
            //string strScript;
            if (ctrlProd.ctrlProdsID == 0)
            {
                var regreso = BctrlProd.InsCtrlP(ctrlProd, LoginInfo.CurrentUsuario.UsuarioId.ToString());
                if (regreso > 0)
                {
                    Session["ctrlProdsID"] = regreso;
                    refresData();
                    return "successSave";
                   // strScript = "swal('Information', 'The product has been success registered!', 'success');";
                }
                else
                {
                    return "errorSave";
                    //strScript = "swal('Information', 'There was an error registering the product!', 'error');";
                }
            }
            else
            {
                var regreso = BctrlProd.UpdCtrlP(ctrlProd, LoginInfo.CurrentUsuario.UsuarioId.ToString());
                if (regreso > 0)
                {
                    refresData();
                    return "successUpdate";
                    //strScript = "swal('Information', 'The product has been success updated!', 'success');";
                }
                else
                {
                    return "errorUpdate";
                    //strScript = "swal('Information', 'There was an error updating the product!', 'error');";
                }
            }
        }

        protected void xgrdTipoArticulo_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            xgrdTipoArticulo.JSProperties["cpAlertMessage"] = string.Empty;
            var pars = e.Parameters;

            if (pars.Contains("saveComment"))
            {
                string[] datos = pars.Split(',');
                foreach (var item in tiposArticulo)
                {
                    if(item.codigoTipoArticulo == datos[1])
                    {
                        item.comentarios = datos[2];
                    }
                }
                xgrdTipoArticulo.DataSource = tiposArticulo;
                xgrdTipoArticulo.DataBind();
            }
            else if(pars == "Delete")
            {
                xgrdTipoArticulo.DataSource = null;
                tiposArticulo.Clear();
            }
            else
            {
                if (pars != "Save")
                {
                    var Valores = e.Parameters;
                    string[] data = Valores.Split(',');

                    foreach (string d in data)
                    {
                        string valor = d.Replace("chk", "");
                        string[] datos = valor.Split('-');
                        string idtipoarticulo = datos[0];
                        string folio = datos[1];
                        tiposArticulo = tiposArticulo.FindAll(p => p.codigoTipoArticulo != folio);
                    }
                }

                xgrdTipoArticulo.DataSource = tiposArticulo;
            }

            xgrdTipoArticulo.DataBind();
        }

        protected void xgrdTipoArticulo_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("ctrlPTipoArticuloID").ToString() + "-" + e.GetValue("codigoTipoArticulo").ToString();

                e.Cell.Text = string.Format("<input type='checkbox' class='chkArt' id='chk{0}'>", id);
            }
            if (e.DataColumn.Name == "M")
            {
                var id = e.GetValue("ctrlPTipoArticuloID").ToString() + "-" + e.GetValue("codigoTipoArticulo").ToString();
                var value = e.GetValue("M");
                string valor = e.GetValue("valor").ToString();
                if (valor == "M")
                {
                    e.Cell.Text = string.Format("<table style='width:100%'><tr><td>" + value + "</td><td style='width:10%'><input type='radio' class='rbMN' name='rbMN{0}' value='M' checked='checked' onclick='SelMN()'/></td></tr></table>", id);
                }
                else
                {
                    e.Cell.Text = string.Format("<table style='width:100%'><tr><td>" + value + "</td><td style='width:10%'><input type='radio' class='rbMN' name='rbMN{0}' value='M' onclick='SelMN()'/></td></tr></table>", id);
                }
            }
            if (e.DataColumn.Name == "N")
            {
                var id = e.GetValue("ctrlPTipoArticuloID").ToString() + "-" + e.GetValue("codigoTipoArticulo").ToString();
                var value = e.GetValue("N");
                string valor = e.GetValue("valor").ToString();
                if (valor == "N")
                {
                    e.Cell.Text = string.Format("<table style='width:100%'><tr><td>" + value + "</td><td style='width:10%'><input type='radio' class='rbMN' name='rbMN{0}' value='N' checked='checked' onclick='SelMN()' /></td></tr></table>", id);
                }
                else
                {
                    e.Cell.Text = string.Format("<table style='width:100%'><tr><td>" + value + "</td><td style='width:10%'><input type='radio' class='rbMN' name='rbMN{0}' value='N' onclick='SelMN()' /></td></tr></table>", id);
                }
            }
        }

        //protected void xgrdMtto_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    xgrdMtto.JSProperties["cpAlertMessage"] = string.Empty;
        //    var pars = e.Parameters;

        //    if (pars == "Delete")
        //    {
        //        xgrdMtto.DataSource = null;
        //        mttos.Clear();
        //    }
        //    if (pars.Contains("SelMtto"))
        //    {
        //        var Valores = e.Parameters;
        //        string[] data = Valores.Split(';');
        //        string idChk = data[1];
        //        string[] ids = idChk.Split('-');
        //        string id = ids[0];
        //        bool select = Convert.ToBoolean(data[2]);
        //        foreach (Mtto_Almn item in mttos)
        //        {
        //            if (item.ctrlPMantenimientoID == Convert.ToInt32(id.Substring(3)))
        //            {
        //                item.selected = select;
        //                break;
        //            }
        //        }

        //        xgrdMtto.DataSource = mttos;
        //        xgrdMtto.DataBind();
        //    }
        //    else
        //    {
        //        if (pars != "Save")
        //        {
        //            var Valores = e.Parameters;
        //            string[] data = Valores.Split(',');

        //            foreach (string d in data)
        //            {
        //                string valor = d.Replace("chk", "");
        //                string[] datos = valor.Split('-');
        //                string idmtto = datos[0];
        //                string folio = datos[1];
        //                mttos = mttos.FindAll(p => p.codigoMttoAlmn != folio);
        //            }
        //        }

        //        xgrdMtto.DataSource = mttos;
        //    }

        //    xgrdMtto.DataBind();
        //}

        //protected void xgrdMtto_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        //{
        //    if (e.DataColumn.Name == "CheckID")
        //    {
        //        var id = e.GetValue("ctrlPMantenimientoID").ToString() + "-" + e.GetValue("codigoMttoAlmn").ToString();
        //        bool selected = Convert.ToBoolean(e.GetValue("selected"));
        //        if (selected)
        //        {
        //            e.Cell.Text = string.Format("<input type='checkbox' class='chkMtto' id='chk{0}' onchange='SelMtto(this);' checked='checked'>", id);
        //        }
        //        else
        //        {
        //            e.Cell.Text = string.Format("<input type='checkbox' class='chkMtto' id='chk{0}' onchange='SelMtto(this);'>", id);
        //        }
        //    }
        //}

        //protected void xgrdAlmacen_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    xgrdAlmacen.JSProperties["cpAlertMessage"] = string.Empty;
        //    var pars = e.Parameters;

        //    if (pars == "Delete")
        //    {
        //        xgrdAlmacen.DataSource = null;
        //        almnes.Clear();
        //    }
        //    if (pars.Contains("SelAlmn"))
        //    {
        //        var Valores = e.Parameters;
        //        string[] data = Valores.Split(';');
        //        string idChk = data[1];
        //        string[] ids = idChk.Split('-');
        //        string id = ids[0];
        //        bool select = Convert.ToBoolean(data[2]);
        //        foreach (Mtto_Almn item in almnes)
        //        {
        //            if (item.ctrlPMantenimientoID == Convert.ToInt32(id.Substring(3)))
        //            {
        //                item.selected = select;
        //                break;
        //            }
        //        }

        //        xgrdAlmacen.DataSource = almnes;
        //        xgrdAlmacen.DataBind();
        //    }
        //    else
        //    {
        //        if (pars != "Save")
        //        {
        //            var Valores = e.Parameters;
        //            string[] data = Valores.Split(',');

        //            foreach (string d in data)
        //            {
        //                string valor = d.Replace("chk", "");
        //                string[] datos = valor.Split('-');
        //                string idmtto = datos[0];
        //                string folio = datos[1];
        //                almnes = almnes.FindAll(p => p.codigoMttoAlmn != folio);
        //            }
        //        }

        //        xgrdAlmacen.DataSource = almnes;
        //    }

        //    xgrdAlmacen.DataBind();
        //}

        //protected void xgrdAlmacen_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        //{
        //    if (e.DataColumn.Name == "CheckID")
        //    {
        //        var id = e.GetValue("ctrlPMantenimientoID").ToString() + "-" + e.GetValue("codigoMttoAlmn").ToString();
        //        bool selected = Convert.ToBoolean(e.GetValue("selected"));
        //        if (selected)
        //        {
        //            e.Cell.Text = string.Format("<input type='checkbox' class='chkAlmn' id='chk{0}' onchange='SelAlmn(this);' checked='checked'>", id);
        //        }
        //        else
        //        {
        //            e.Cell.Text = string.Format("<input type='checkbox' class='chkAlmn' id='chk{0}' onchange='SelAlmn(this);'>", id);
        //        }
        //    }
        //}


        protected void xgrdArchivos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            xgrdArchivos.JSProperties["cpAlertMessage"] = string.Empty;
            var pars = e.Parameters;

            string CodtDoc = cmbTipoDoc.SelectedItem.Value.ToString();
            string tDoc = cmbTipoDoc.SelectedItem.Text.ToString();

            if (pars == "Add")
            {
                if (txtdescripcionArchivo.Text == "")
                {
                    xgrdArchivos.JSProperties["cpAlertMessage"] = "NotDescripcion";
                    return;
                }
                if (txtFileArchivo.Text == "")
                {
                    xgrdArchivos.JSProperties["cpAlertMessage"] = "UploadFile";
                    return;
                }

                archivos a = new archivos();
                a.ctrlPArchivosID = 0;
                a.noDocumento = lblnDoc.Text;
                a.codigoTipoDocumento = CodtDoc;
                a.descripcion = txtdescripcionArchivo.Text;
                a.archivo = txtFileArchivo.Text;
                a.TipoDocumento = tDoc;


                List<archivos> exists = archivos.FindAll(ex => ex.archivo == a.archivo);
                if(exists.Count > 0)
                {
                    xgrdArchivos.JSProperties["cpAlertMessage"] = "Exist";
                    return;
                }

                archivos.Add(a);
                xgrdArchivos.DataSource = archivos;
                xgrdArchivos.DataBind();

                txtdescripcionArchivo.Text = "";
                txtFileArchivo.Text = "";
                xgrdArchivos.JSProperties["cpAlertMessage"] = "SussAdd";
            }
            else if (pars == "Delete")
            {
                archivos.Clear();
                xgrdArchivos.DataSource = archivos;
                xgrdArchivos.DataBind();

                xgrdArchivos.JSProperties["cpAlertMessage"] = "succDeleteAll";
            }
            else
            {
                var Valores = e.Parameters;
                string[] data = Valores.Split(',');
                foreach(string d in data)
                {
                    string[] datos = d.Split('-');
                    string codigoTipoDocumento = datos[1];
                    string archivo = datos[2];
                    archivos = archivos.FindAll(p => p.archivo != archivo);
                    xgrdArchivos.DataSource = archivos;
                    xgrdArchivos.DataBind();

                    xgrdArchivos.JSProperties["cpAlertMessage"] = "succDeleteSel";
                }
            }
        }

        protected void xgrdArchivos_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "ctrlPArchivosID")
            {
                var id = e.GetValue("ctrlPArchivosID").ToString() + "-" + e.GetValue("codigoTipoDocumento").ToString() + "-" + e.GetValue("archivo").ToString();

                e.Cell.Text = string.Format("<input type='checkbox' class='chkArchivo' id='chk{0}'>", id);
            }
        }

        protected void xgrdAprobaciones_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            xgrdAprobaciones.JSProperties["cpAlertMessage"] = string.Empty;
            var pars = e.Parameters;
            var Valores = e.Parameters;
            string[] data = Valores.Split(',');

            foreach (Aprobacion item in aprbnes)
            {
                if(Convert.ToInt32(item.codigoPerfil) == Convert.ToInt32(data[0]))
                {
                    item.codigoEmpleado = data[1];
                    break;
                }
            }
            xgrdAprobaciones.DataSource = aprbnes;
            xgrdAprobaciones.DataBind();
        }

        protected void xgrdAprobaciones_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName == "codigoPerfil")
            {
                PerfilDa pda = new PerfilDa();
                List<Perfil> perf = pda.GetCombo();
                List<Perfil> perf2 = perf.FindAll(p => p.Codigo == e.GetValue("codigoPerfil").ToString());
                e.Cell.Text = perf2[0].CodigoYNombre;
            }
            if (e.DataColumn.FieldName == "codigoEmpleado")
            {
                var id = e.GetValue("AprobacionesID").ToString() + "-" + e.GetValue("noDocumento").ToString() + "-" + e.GetValue("codigoPerfil").ToString();

                string disabled = "";

                if(lblcodigoSts.Text != "0001")
                {
                    disabled = "disabled='disabled'";
                }

                EmpleadosDa eDa = new EmpleadosDa();
                List<Empleados> lemp = eDa.GetEmpleadoWithPerfil();
                lemp = lemp.FindAll(u => u.CodigoPerfil == e.GetValue("codigoPerfil").ToString());
                string sel = "<select  class='selEmpleados' id='sel{0}' style='width: 230px'  onchange='javascript: selAprob("+ e.GetValue("codigoPerfil").ToString() + ",this.value); ' "+ disabled + ">";
                int iteracion = 0;
                foreach (Empleados emp in lemp)
                {
                    string selstr = "";
                    if(e.GetValue("codigoEmpleado").ToString() == emp.Codigo)
                    {
                        selstr = "selected='selected'";
                    }
                    else
                    {
                        if (iteracion == 0)
                        {
                            selstr = "selected='selected'";
                        }
                    }
                    sel += "<option value='"+ emp.Codigo+ "' "+ selstr + ">" + emp.NombreCompleto+"</option>";
                    iteracion++;
                }
                sel += "</select>";
                e.Cell.Text = string.Format(sel, id);
            }
        }

        protected void ASPxCallbackPanel1_Callback(object sender, CallbackEventArgsBase e)
        {
            var param = e.Parameter;
            var pS = param.Split(',');
            string CodigodPuestoSelected = "";

            if (pS.Length > 0)
            {
                if (pS[0] == "filterPuesto")
                {
                    CodigodPuestoSelected = pS[1];
                    var BEmpleado = new EmpleadosDa();
                    //cmbEmpleadoAdd.TextField = "NombreCompleto";
                    //cmbEmpleadoAdd.ValueField = "EmpleadoId";
                    //cmbEmpleadoAdd.DataSource = BEmpleado.GetCombo(CodigodPuestoSelected);
                    //cmbEmpleadoAdd.DataBind();
                }
                if (pS[0] == "Limpiar")
                {
                    limpiarModalAprnes();
                }
            }
            else
            {
                //ASPxCallbackPanel1.JSProperties["cpAlertMessage"] = "errror";
            }
        }

        protected void xgrdTipoArticuloMDL_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            xgrdTipoArticuloMDL.JSProperties["cpAlertMessage"] = string.Empty;
            var pars = e.Parameters;

            var Valores = e.Parameters;
            string[] data = Valores.Split(',');

            tiposArticulo.Clear();
            foreach (string d in data)
            {
                string valor = d.Replace("chk", "");
                string[] datos = valor.Split('-');
                string codigo = datos[0];
                List<TipoArticulo> t = tArticulos.FindAll(p => p.codigoTipoArticulo == codigo);
                foreach (TipoArticulo item in t)
                {
                    List<_tipoArticulo> t2 = tiposArticulo.FindAll(p => p.codigoTipoArticulo == item.codigoTipoArticulo);
                    if (t2.Count == 0)
                    {
                        _tipoArticulo n = new _tipoArticulo();
                        n.ctrlPTipoArticuloID = 0;
                        n.noDocumento = "";
                        n.codigoTipoArticulo = item.codigoTipoArticulo;
                        n.tipoArticulo = item.tipoArticulo;
                        n.M = item.M;
                        n.N = item.N;
                        n.comentarios = item.comentarios;
                        tiposArticulo.Add(n);
                    }
                }
            }

            xgrdTipoArticuloMDL.JSProperties["cpAlertMessage"] = "Add";
        }

        protected void xgrdTipoArticuloMDL_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("codigoTipoArticulo").ToString();
                bool rel = false;
                List<_tipoArticulo> tipos = tiposArticulo.FindAll(item => item.codigoTipoArticulo == id);
                if(tipos.Count > 0)
                {
                    rel = true;
                }
                if (rel)
                {
                    e.Cell.Text = string.Format("<input type='checkbox' class='chkArtMDL' id='chk{0}' checked>", id);
                }
                else
                {
                    e.Cell.Text = string.Format("<input type='checkbox' class='chkArtMDL' id='chk{0}'>", id);
                }
            }
        }

        //protected void xgrdMttoMDL_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    xgrdMttoMDL.JSProperties["cpAlertMessage"] = string.Empty;
        //    var pars = e.Parameters;

        //    var Valores = e.Parameters;
        //    string[] data = Valores.Split(',');

        //    mttos.Clear();
        //    foreach (string d in data)
        //    {
        //        string valor = d.Replace("chk", "");
        //        string[] datos = valor.Split('-');
        //        string codigo = datos[0];
        //        string tipo = datos[1];
        //        if (tipo == "M")
        //        {
        //            List<MttoAlmn> t = tMttoAlmn.FindAll(p => p.codigoMttoAlmn == codigo && p.tipo == tipo);
        //            foreach (MttoAlmn item in t)
        //            {
        //                List<Mtto_Almn> t2 = mttos.FindAll(p => p.codigoMttoAlmn == item.codigoMttoAlmn);
        //                if (t2.Count == 0)
        //                {
        //                    Mtto_Almn n = new Mtto_Almn();
        //                    n.ctrlPMantenimientoID = 0;
        //                    n.noDocumento = "";
        //                    n.codigoMttoAlmn = item.codigoMttoAlmn;
        //                    n.especificacion = item.especificacion;
        //                    n.notas = item.notas;
        //                    n.clasificacion = item.clasificacion;
        //                    n.responsable = item.responsable;
        //                    n.tipo = item.tipo;
        //                    mttos.Add(n);
        //                }
        //            }
        //        }
        //    }

        //    xgrdMttoMDL.JSProperties["cpAlertMessage"] = "Add";
        //}

        //protected void xgrdMttoMDL_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        //{
        //    if (e.DataColumn.Name == "CheckID")
        //    {
        //        var id = e.GetValue("codigoMttoAlmn").ToString() + "-" + e.GetValue("tipo").ToString();
        //        bool rel = false;
        //        List<Mtto_Almn> _mttos = mttos.FindAll(item => item.codigoMttoAlmn == e.GetValue("codigoMttoAlmn").ToString());
        //        if (_mttos.Count > 0)
        //        {
        //            rel = true;
        //        }
        //        if (rel)
        //        {
        //            e.Cell.Text = string.Format("<input type='checkbox' class='chkMttoMDL' id='chk{0}' checked>", id);
        //        }
        //        else
        //        {
        //            e.Cell.Text = string.Format("<input type='checkbox' class='chkMttoMDL' id='chk{0}'>", id);
        //        }
        //    }
        //}

        //protected void xgrdAlmacenMDL_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    xgrdAlmacenMDL.JSProperties["cpAlertMessage"] = string.Empty;
        //    var pars = e.Parameters;

        //    var Valores = e.Parameters;
        //    string[] data = Valores.Split(',');

        //    almnes.Clear();
        //    foreach (string d in data)
        //    {
        //        string valor = d.Replace("chk", "");
        //        string[] datos = valor.Split('-');
        //        string codigo = datos[0];
        //        string tipo = datos[1];
        //        if (tipo == "A")
        //        {
        //            List<MttoAlmn> t = tMttoAlmn.FindAll(p => p.codigoMttoAlmn == codigo && p.tipo == tipo);
        //            foreach (MttoAlmn item in t)
        //            {
        //                List<Mtto_Almn> t2 = almnes.FindAll(p => p.codigoMttoAlmn == item.codigoMttoAlmn);
        //                if (t2.Count == 0)
        //                {
        //                    Mtto_Almn n = new Mtto_Almn();
        //                    n.ctrlPMantenimientoID = 0;
        //                    n.noDocumento = "";
        //                    n.codigoMttoAlmn = item.codigoMttoAlmn;
        //                    n.especificacion = item.especificacion;
        //                    n.notas = item.notas;
        //                    n.clasificacion = item.clasificacion;
        //                    n.responsable = item.responsable;
        //                    n.tipo = item.tipo;
        //                    almnes.Add(n);
        //                }
        //            }
        //        }
        //    }

        //    xgrdAlmacenMDL.JSProperties["cpAlertMessage"] = "Add";
        //}

        //protected void xgrdAlmacenMDL_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        //{
        //    if (e.DataColumn.Name == "CheckID")
        //    {
        //        var id = e.GetValue("codigoMttoAlmn").ToString() + "-" + e.GetValue("tipo").ToString();
        //        bool rel = false;
        //        List<Mtto_Almn> _almnes = almnes.FindAll(item => item.codigoMttoAlmn == e.GetValue("codigoMttoAlmn").ToString());
        //        if (_almnes.Count > 0)
        //        {
        //            rel = true;
        //        }
        //        if (rel)
        //        {
        //            e.Cell.Text = string.Format("<input type='checkbox' class='chkAlmnMDL' id='chk{0}' checked>", id);
        //        }
        //        else
        //        {
        //            e.Cell.Text = string.Format("<input type='checkbox' class='chkAlmnMDL' id='chk{0}'>", id);
        //        }
        //    }
        //}

        protected void ASPxCallbackPanel2_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = string.Empty;
            var param = e.Parameter;
            var pS = param.Split(',');

            if (pS.Length > 0)
            {
                if (pS[0] == "Save")
                {
                    if (lblnDoc.Text == "")
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "InputNoDoc";
                        return;
                    }
                    if (cmbMaquina.SelectedItem == null)
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectMachine";
                        return;
                    }
                    if (cmbSubCat1.SelectedItem == null)
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectSubcategory1";
                        return;
                    }
                    if (cmbSubCat2.SelectedItem == null)
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectSubcategory2";
                        return;
                    }
                    if (cmbSubCat3.SelectedItem == null)
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectSubcategory3";
                        return;
                    }
                    //if (cmbUtilizado.SelectedItem == null)
                    //{
                    //    ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectUsed";
                    //    return;
                    //}
                    if (cmbDepa.SelectedItem == null)
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectDepartment";
                        return;
                    }
                    if (cmbPlan.SelectedItem == null)
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectPlan";
                        return;
                    }
                    if (cmbMarca.SelectedItem == null)  
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectBrand";
                        return;
                    }
                    if (cmbProveedor.SelectedItem == null)
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectProvider";
                        return;
                    }
                    //if (cmbUnico.SelectedItem == null)
                    //{
                    //    ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectUnique";
                    //    return;
                    //}
                    //if (xDateFechaCot.Date == new DateTime(1, 1, 1))
                    //{
                    //    ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectQuoteDate";
                    //    return;
                    //}
                    if (cmbMoneda.SelectedItem == null)
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectCurrency";
                        return;
                    }
                    if (cmbCodigoUM.SelectedItem == null)
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectUM";
                        return;
                    }
                    if (cmbPlaneador.SelectedItem == null)
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectGlider";
                        return;
                    }
                    if (cmbComprador.SelectedItem == null)
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectBuyer";
                        return;
                    }
                    if(txtCodigoArticulo.Text == "")
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectCodArt";
                        return;
                    }
                    //if (rbFichaSi.Checked)
                    //{
                    //    if(txtFile.Text == "")
                    //    {
                    //        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "SelectFile";
                    //        return;
                    //    }
                    //}
                    string savestr = Save();
                    ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = savestr;
                    return;
                }
                else if (pS[0] == "Enviar")
                {
                    int regreso = suguienteEstatus("false");
                    if(regreso > 0)
                    {
                        refresData();
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "EnvSucces";
                    }
                    else
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "EnvFaild";
                    }
                }
                else if (pS[0] == "EnviarDM")
                {
                    int regreso = suguienteEstatus("true");
                    if (regreso > 0)
                    {
                        refresData();
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "EnvSucces";
                    }
                    else
                    {
                        ASPxCallbackPanel2.JSProperties["cpAlertMessage"] = "EnvFaild";
                    }
                }
                else if (pS[0] == "rbAlta" || pS[0] == "rbModificacion" || pS[0] == "rbBaja")
                {
                    switch (pS[0])
                    {
                        case "rbAlta":
                            lblProd.Visible = false;
                            cmbProducto.Visible = false;
                            dvReemplazaOtro.Visible = true;
                            break;
                        default:
                            lblProd.Visible = true;
                            cmbProducto.Visible = true;
                            dvReemplazaOtro.Visible = false;
                            break;
                    }

                    if (rbSi.Checked)
                    {
                        lblcual.Visible = true;
                        cmbCualArticulo.Visible = true;
                    }
                    else
                    {
                        lblcual.Visible = false;
                        cmbCualArticulo.Visible = false;
                    }
                }
                else if (pS[0] == "rbSi" || pS[0] == "rbNo")
                {
                    switch (pS[0])
                    {
                        case "rbSi":
                            lblcual.Visible = true;
                            cmbCualArticulo.Visible = true;
                            break;
                        default:
                            lblcual.Visible = false;
                            cmbCualArticulo.Visible = false;
                            break;
                    }

                    if (rbAlta.Checked)
                    {
                        lblProd.Visible = false;
                        cmbProducto.Visible = false;
                    }
                    else
                    {
                        lblProd.Visible = true;
                        cmbProducto.Visible = true;
                    }
                }
                //else if (pS[0] == "rbFichaSegSi" || pS[0] == "rbFichaSegNo")
                //{
                //    switch (pS[0])
                //    {
                //        case "rbFichaSegSi":
                //            dvHojaSeg1.Visible = true;
                //            dvHojaSeg2.Visible = true;
                //            break;
                //        default:
                //            dvHojaSeg1.Visible = false;
                //            dvHojaSeg2.Visible = false;
                //            break;
                //    }
                //}
                else
                {
                    if (rbAlta.Checked)
                    {
                        lblProd.Visible = false;
                        cmbProducto.Visible = false;
                    }
                    else
                    {
                        lblProd.Visible = true;
                        cmbProducto.Visible = true;
                    }
                    if (rbSi.Checked)
                    {
                        lblcual.Visible = true;
                        cmbCualArticulo.Visible = true;
                    }
                    else
                    {
                        lblcual.Visible = false;
                        cmbCualArticulo.Visible = false;
                    }
                    string[] values = pS;
                    foreach (string valor in values)
                    {
                        string[] identificadores = valor.Split('-');
                        string[] datos = identificadores[1].Split(':');
                        string codigoTipoArticulo = datos[0];
                        string value = datos[1];
                        foreach (_tipoArticulo item in tiposArticulo)
                        {
                            if (item.codigoTipoArticulo == codigoTipoArticulo)
                            {
                                item.valor = value;
                            }
                        }

                        int M = 0;
                        foreach (_tipoArticulo art in tiposArticulo)
                        {
                            if (art.valor == "M")
                            {
                                M++;
                            }
                        }
                        if (M > 5)
                        {
                            lblstock.Text = "M";
                            lblstock2.Text = "M: Con Stock";
                        }
                        else
                        {
                            lblstock.Text = "N";
                            lblstock2.Text = "N: Con Stock";
                        }
                        xgrdTipoArticulo.DataSource = tiposArticulo;
                        xgrdTipoArticulo.DataBind();
                    }
                }
            }
        }

        private int suguienteEstatus(string DM)
        {
            int ctrlProdsID = Convert.ToInt32(Session["ctrlProdsID"]);
            string codPerf = LoginInfo.CurrentPerfil.Codigo;
            ControlProductosda cpda = new ControlProductosda();
            return cpda.changeSts(ctrlProdsID.ToString(), LoginInfo.CurrentUsuario.UsuarioId.ToString(), lblSigPerf.Text, DM);
        }

        protected void CIuplGFileArchivo_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.IsValid)
            {
                e.CallbackData = SavePostedGraphicsFile(e.UploadedFile);
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
                e.ErrorText = "Unexpected error";
            }
        }

        protected void CIuplGraphicsFile_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.IsValid)
            {
                e.CallbackData = SavePostedGraphicsFile(e.UploadedFile);
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
                e.ErrorText = "Unexpected error";
            }
        }

        private bool ExistsFile(string sFile)
        {
            bool bExists = false;

            bExists = System.IO.File.Exists(sFile);

            return bExists;
        }

        string SavePostedGraphicsFile(UploadedFile uploadedFile)
        {
            if (!uploadedFile.IsValid)
                return string.Empty;

            //string dateshort = DateTime.Now.ToLongTimeString().Replace(".", "").Replace("-", "").Replace(":", "").Replace(" ", "");

            //int ctrlProdsID = Convert.ToInt32(Session["ctrlProdsID"]);

            //var Names = uploadedFile.FileName.Split('.');

            //string codigoArchivo = ctrlProdsID + "_" + dateshort.ToUpper() + "." + Names[1];

            string sPath = Path.Combine(Server.MapPath(UploadDirectory), uploadedFile.FileName);

            if (ExistsFile(sPath))
                File.Delete(sPath);

            uploadedFile.SaveAs(sPath);

            SelectedFile = uploadedFile.FileName;
            SelectedFileName = uploadedFile.FileName;


            return SelectedFileName;
        }

        protected void perfil_Init(object sender, EventArgs e)
        {

        }

        //protected void rbOperacion_CheckedChanged(object sender, EventArgs e)
        //{
        //    RadioButton rb = (RadioButton)sender;
        //    if (rb.Checked)
        //    {
        //        switch (rb.ID)
        //        {
        //            case "rbAlta":
        //                lblProd.Visible = false;
        //                cmbProducto.Visible = false;
        //                break;
        //            default:
        //                lblProd.Visible = true;
        //                cmbProducto.Visible = true;
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        switch (rb.ID)
        //        {
        //            case "rbAlta":
        //                lblProd.Visible = true;
        //                cmbProducto.Visible = true;
        //                break;
        //            default:
        //                lblProd.Visible = false;
        //                cmbProducto.Visible = false;
        //                break;
        //        }
        //    }
        //}
    }
}