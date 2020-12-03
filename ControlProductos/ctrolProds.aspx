<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ctrolProds.aspx.cs" Inherits="ControlProductos.ctrolProds" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/jscript">
          function OnEndCallback(s, e) {              
            if (s.cpAlertMessage != '') {
                if (s.cpAlertMessage == 'Update') {
                    swal("Information", "The registry was updated successfully", "success");
                } else if (s.cpAlertMessage == 'Insert') {
                    swal("Information", "Record added successfully!", "success");
                } else if (s.cpAlertMessage == 'Delete') {
                    swal("Information", "Registration was successfully enabled / disabled!", "success");
                } else if (s.cpAlertMessage == 'Error') {
                    swal("Warning", "An error has occurred from the database");
                }
                grid.PerformCallback('Search');
            }
        }

        function raiseValidation() {
            if (ASPxClientEdit.ValidateEditorsInContainer(null))
                grid.UpdateEdit();            
        }

        function OnCallback() {
            grid.PerformCallback('Search');
        }

        function GetSelectedFieldValuesCallback(values) {
            var Valores = "";
            for (var i = 0; i < values.length; i++) {
                if (Valores == "") {
                    Valores = Valores + values[i];
                } else {
                    Valores = Valores + ", " + values[i];
                }
            }
        }

        function DisableSelected() {
            var Valores = "";
            $(".chk").each(function () {
                if (this.checked) {
                    if (Valores == "") {
                        Valores = this.id.substr(3);
                    } else {
                        Valores += "," + this.id.substr(3);
                    }
                }
            });

            if (Valores == "") {

                swal({
                    title: "Information",
                    text: "No records selected!",
                    type: "info"
                });
                return;
            }

            swal({
                title: "Are you sure?",
                text: "Are you sure you want to enable / disable all the selected records?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: false,
                closeOnCancel: false
            },
              function (isConfirm) {
                  if (isConfirm) {
                      CallbackPanelDisable.PerformCallback(Valores);
                      grid.PerformCallback('Search');
                      swal("Deleted", "Records were successfully enabled / disabled", "success");
                  } else {
                      swal("Cancelled", "You canceled the operation", "error");
                  }
              });
        }

        function DisableAll() {

            swal({
                title: "Are you sure?",
                text: "Are you sure you want to enable / disable all registers?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: false,
                closeOnCancel: false
            },
            function (isConfirm) {
                if (isConfirm) {
                    CallbackPanelDisableAll.PerformCallback();
                    grid.PerformCallback('Search');
                    swal("Deleted", "All logs were successfully enabled/disabled", "success");
                } else {
                    swal("Cancelled", "You canceled the operation", "error");
                }
            });
        }

        function DowloadFileXLS() {
            CallbackPanelDowload.PerformCallback();
        }

        function UploadFileXLS() {
            PanelUploadFile.SetVisible(true);
        }

        //Carga Archivo Excel de Lista Material con Precios.
        function UploaderReceipt_OnUploadStartExcel() {
            btnUploadReceiptExcel.SetEnabled(false);
        }

        function UploaderReceipt_OnFileUploadCompleteExcel(args) {
            if (args.isValid) {
                grid.PerformCallback('Search');
                swal("Satisfactoriamente!", "Archivo cargado satisfactoriamente", "success");
                PanelUploadFile.SetVisible(false);
            }
        }

        function UploaderReceipt_OnFilesUploadCompleteExcel(args) {
            UpdateUploadReceiptButtonExcel();
        }

        function UpdateUploadReceiptButtonExcel() {
            btnUploadReceiptExcel.SetEnabled(uploaderReceiptExcel.GetText(0) != "");
        }

        function OnEndReceiptCallbackExcel(s, e) {
            if (s.cpAlertMessage != '') {
                alert(s.cpAlertMessage)
                grid.PerformCallback('Search');
            }
        }

        function OnAppsEndCallback(s, e) {
            if (s.cpAlertMessage != '') {
                if (s.cpAlertMessage == 'SelectOne') {
                    swal("Information", "Select an application to add it to the list", "info");
                } else if (s.cpAlertMessage == 'Exist') {
                    swal("Information", "The selected application is already added to the list", "info");
                }
                //alert(s.cpAlertMessage);
            }
        }
    </script>
    <style>
        .Campos {
            width:250px;
            background-color:white;
            margin:2px;
            padding:1px;
            border-color:darkgray;
            font-size:small;
        }
        .gblue{
            color:#337ab7;
        }
        .dot {
          height: 15px;
          width: 15px;
          border-radius: 50%;
          display: inline-block;
        }
    </style>
    <div id="Busqueda" runat="server">
        <dx:ASPxNavBar ID="ASPxNavBar2" runat="server" Theme="Metropolis" Width="100%" 
            Font-Bold="False">
            <Groups>
                <dx:NavBarGroup Text="Control Products\ Filters">
                        <ContentTemplate>
                                <div id="Parametros" runat="server" visible="true">
                                            
                                <table style="float: left; width: 40%"" class="OptionsTable BottomMargin">
                                    <tr style="height:10px"></tr>
                                    <tr>
                                        <td  style="width:10%; text-align:left">
                                                <dx:ASPxLabel ID="lblFolio" runat="server" Text="Quote number: ">
                                        </dx:ASPxLabel>
                                        </td>
                                        <td  style="width:30%; text-align:left">
                                            <dx:ASPxTextBox runat="server" Theme="SoftOrange" Height="5px"  ID="xtxtFolio" Enabled="True" Width="100%" FocusedStyle-Border-BorderColor="#3399ff" FocusedStyle-Border-BorderStyle="Double">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:10px"></tr>
                                    <tr>
                                        <td  style="width:10%; text-align:left">
                                             <dx:ASPxLabel ID="ASPxLblFechaIni" runat="server" Text="Initial Date: "></dx:ASPxLabel>
                                        </td>
                                        <td  style="width:30%; text-align:left">
                                            <dx:ASPxDateEdit ID="xDateFechaIni" runat="server" Width="200px"  Theme="SoftOrange"
                                                DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd">                                                             
                                                <TimeSectionProperties>
                                                    <TimeEditProperties EditFormatString="hh:mm tt" />
                                                </TimeSectionProperties>
                                            </dx:ASPxDateEdit> 
                                        </td>
                                    </tr>
                                    <tr style="height:10px"></tr>
                                    <tr>
                                        <td  style="width:10%; text-align:left">
                                             <dx:ASPxLabel ID="ASPxLblFechaFin" runat="server" Text="End Date: "></dx:ASPxLabel>
                                        </td>
                                        <td  style="width:30%; text-align:left">
                                            <dx:ASPxDateEdit ID="xDateFechaFin" runat="server" Width="200px"  Theme="SoftOrange"
                                                DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd">                                                             
                                                <TimeSectionProperties>
                                                    <TimeEditProperties EditFormatString="hh:mm tt" />
                                                </TimeSectionProperties>
                                            </dx:ASPxDateEdit> 
                                        </td>
                                    </tr>
                                    <tr style="height:10px"></tr>
                                    <tr>
                                        <td  style="width:20%; text-align:right">
                                            <dx:ASPxButton ID="btnBuscar" runat="server" Text="Search" Theme="SoftOrange" AutoPostBack="false" >
                                                <ClientSideEvents Click="OnCallback" />
                                            </dx:ASPxButton>
                                        </td>                                                   
                                      </tr>
                                </table>
                                <table style="float: right; width: 20%" class="OptionsTable BottomMargin">
                                    <tr>
                                        <td>
                                                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" HeaderText="+ Options" Theme="Metropolis" >
                                                <PanelCollection>
                                                <dx:PanelContent>
                                                    <div>
                                                        <table>
                                                            <tr>
                                                                <td style="width:100%">
                                                                    <div class="div-container">
                                                                        <div class="div-img" >
                                                                        <asp:ImageButton runat="server" id="imgNew" class="img"   ImageUrl="Assets/Images/New.png" OnClick="imgNew_Click" alt="New" style="cursor:pointer" title="New" />
                                                                        </div>
                                                                    </div>
                                                                </td> 
                                                                    <td style="width:20%">
                                                                        <div class="div-container">
                                                                        <div class="div-img" >
                                                                        <img class="img" src="Assets/Images/trash_can.png" alt="Remove Selected" style="cursor:pointer" onclick="return DisableSelected();" title="Remove Selected" />
                                                                        </div>
                                                                        </div>
                                                                </td>
                                                                <td style="width:20%" >
                                                                    <div class="div-container">
                                                                        <div class="div-img" >
                                                                            <img class="img" src="Assets/Images/delete_all.png" alt="Remove All" style="cursor:pointer" onclick="return DisableAll();" title="Remove All" /> 
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                                    <td>
                                                                    <dx:ASPxCallbackPanel ID="CallbackPanelDisable" ClientInstanceName="CallbackPanelDisable" runat="server" Width="200px" oncallback="CallbackPanelDisable_Callback">
                                                                    </dx:ASPxCallbackPanel>
                                                                    <dx:ASPxCallbackPanel ID="CallbackPanelDisableAll" ClientInstanceName="CallbackPanelDisableAll" runat="server" Width="200px" oncallback="CallbackPanelDisableAll_Callback">
                                                                    </dx:ASPxCallbackPanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxRoundPanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>                                
                </dx:NavBarGroup>                            
            </Groups>
        </dx:ASPxNavBar>
    </div>
    <div id="Detalle" runat="server">

        <dx:ASPxGridView ID="xgrdProds" runat="server" AutoGenerateColumns="False" 
            Width="100%" Font-Names="Segoe UI"
            KeyFieldName="ctrlProdsID" 
            OnHtmlDataCellPrepared="xgrdProds_HtmlDataCellPrepared"
            OnDetailRowExpandedChanged="xgrdProds_DetailRowExpandedChanged"
            ClientInstanceName="grid"
            Theme="Metropolis">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="ctrlProdsID" Visible="false" Caption="Identifier" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="1" Width="1%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="noDocumento" VisibleIndex="3" Caption="Document Number">
                </dx:GridViewDataTextColumn>     
                <dx:GridViewDataTextColumn FieldName="codigoSolicitante" VisibleIndex="4" Caption="Applicant">
                </dx:GridViewDataTextColumn>    
                <dx:GridViewDataTextColumn FieldName="fechaSolicitud" VisibleIndex="6" Caption="Request Date">
                </dx:GridViewDataTextColumn>                                   
                <dx:GridViewDataColumn FieldName="ctrlProdsID" VisibleIndex="10"  Caption="Actions" CellStyle-HorizontalAlign="Left">
                      <DataItemTemplate>
                          <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/Assets/images/layout_edit.png"  OnClick="imgEditar_Click" CommandArgument='<%# Eval("ctrlProdsID") %>' />
                      </DataItemTemplate>
                </dx:GridViewDataColumn>
            </Columns>
            <SettingsBehavior ConfirmDelete="true"/>
            <SettingsPager Mode="ShowPager" PageSize="13"/>
            <Settings ShowFilterRow="True" />
            <SettingsDetail ShowDetailRow="false" />
            <SettingsText ConfirmDelete="Are you sure you want to enable/disable this Solicitud"/> 
            <SettingsEditing Mode="EditForm"></SettingsEditing>
            <Styles>
                <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                <Header BackColor="#F2F2F2"></Header>                                              
            </Styles>
            <SettingsBehavior EnableRowHotTrack="true" />                 
<%--            <Templates>
                <DetailRow>
                    <dx:ASPxGridView ID="xgrdCotizacion_2" runat="server" AutoGenerateColumns="False" 
                        Width="100%" Font-Names="Segoe UI"
                        KeyFieldName="cotizacionProdID" 
                        OnDetailRowExpandedChanged="xgrdCotizacion2_DetailRowExpandedChanged"
                        ClientInstanceName="grid2"
                        Theme="Metropolis">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="cotizacionProdID" Visible="false" Caption="Identifier" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="parteCodigo" VisibleIndex="3" Caption="Prod. Code" Width="7%">
                            </dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn FieldName="parte" VisibleIndex="4" Caption="Product Name" Width="25%">
                            </dx:GridViewDataTextColumn>    
                            <dx:GridViewDataTextColumn FieldName="um" VisibleIndex="5" Caption="UM" Width="25%">
                            </dx:GridViewDataTextColumn>  
                            <dx:GridViewDataTextColumn FieldName="done" VisibleIndex="6" Caption="Quote Done" Width="15%">
                            </dx:GridViewDataTextColumn>  
                        </Columns>
                        <SettingsBehavior ConfirmDelete="true"/>
                        <SettingsPager Mode="ShowPager" PageSize="13"/>
                        <Settings ShowFilterRow="True" />
                        <SettingsText ConfirmDelete="Are you sure you want to enable/disable this Solicitud"/> 
                        <SettingsDetail ShowDetailRow="true" />
                        <Styles>
                            <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                            <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                            <Header BackColor="#F2F2F2"></Header>                                              
                        </Styles>
                        <SettingsBehavior EnableRowHotTrack="true" />               
                        <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="xgrdCotizacion_3" runat="server" AutoGenerateColumns="False" 
                                    Width="100%" Font-Names="Segoe UI"
                                    OnHtmlDataCellPrepared="xgrdCotizacion_3_HtmlDataCellPrepared"
                                    KeyFieldName="cotizacionID" 
                                    ClientInstanceName="grid2"
                                    Theme="Metropolis">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="cotizacionID" Visible="false" Caption="Identifier" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ProveedorCodigo" VisibleIndex="1" Caption="Supp. Code" Width="7%">
                                        </dx:GridViewDataTextColumn>     
                                        <dx:GridViewDataTextColumn FieldName="proveedor" VisibleIndex="2" Caption="Supplier Name" Width="25%">
                                        </dx:GridViewDataTextColumn>    
                                        <dx:GridViewDataTextColumn FieldName="precio_unitario" VisibleIndex="3" Caption="Unit Price" Width="15%">
                                        </dx:GridViewDataTextColumn>  
                                        <dx:GridViewDataTextColumn FieldName="entrega" VisibleIndex="4" Caption="Delivery Time" Width="15%">
                                        </dx:GridViewDataTextColumn>  
                                        <dx:GridViewDataTextColumn FieldName="fechaCotizacion" VisibleIndex="5" Caption="Quotation Date" Width="10%">
                                        </dx:GridViewDataTextColumn>  
                                        <dx:GridViewDataTextColumn FieldName="sstatus" VisibleIndex="6" Caption="Status" Width="15%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="sstatus" VisibleIndex="7" >
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="Assigned" Caption="Assigned" VisibleIndex="8" >
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior ConfirmDelete="true"/>
                                    <SettingsPager Mode="ShowPager" PageSize="13"/>
                                    <Settings ShowFilterRow="True" />
                                    <SettingsText ConfirmDelete="Are you sure you want to enable/disable this Solicitud"/> 
                                    <Styles>
                                        <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                                        <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                                        <Header BackColor="#F2F2F2"></Header>                                              
                                    </Styles>
                                    <SettingsBehavior EnableRowHotTrack="true" />                               
                                </dx:ASPxGridView>                     
                            </DetailRow>
                        </Templates>                
                    </dx:ASPxGridView> 
                </DetailRow>
            </Templates>                --%>
        </dx:ASPxGridView>
    </div>
</asp:Content>
