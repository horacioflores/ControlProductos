<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ctrolProds_MisPendientes.aspx.cs" Inherits="ControlProductos.ctrolProds_MisPendientes" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>
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

        function redirect(value) {
            document.getElementById("MainContent_hdnRedirect").value = value;
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
                                             <dx:ASPxLabel ID="ASPxLblFechaIni" runat="server" Text="Initial Date: "/>
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
                                             <dx:ASPxLabel ID="ASPxLblFechaFin" runat="server" Text="End Date: "/>
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
                                        <td style="width:20%; text-align:right">
                                            <dx:ASPxButton ID="btnBuscar" runat="server" Text="Search" Theme="SoftOrange" AutoPostBack="false" >
                                                <ClientSideEvents Click="OnCallback" />
                                            </dx:ASPxButton>
                                        </td>                                                   
                                      </tr>
                                </table>
                                <table style="float: right; width: 20%" class="OptionsTable BottomMargin">
                                    <tr>
                                        <td>
                                            <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" HeaderText="+ Options" Theme="Metropolis" Visible="false" >
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
                                                                    <dx:ASPxCallbackPanel ID="CallbackPanelDisable" ClientInstanceName="CallbackPanelDisable" runat="server" Width="200px" oncallback="CallbackPanelDisable_Callback"/>
                                                                    <dx:ASPxCallbackPanel ID="CallbackPanelDisableAll" ClientInstanceName="CallbackPanelDisableAll" runat="server" Width="200px" oncallback="CallbackPanelDisableAll_Callback"/>
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
        <input type="hidden" runat="server" id="hdnRedirect" />
        <dx:ASPxGridView ID="xgrdProds" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Segoe UI" KeyFieldName="ctrlProdsID" 
            OnHtmlDataCellPrepared="xgrdProds_HtmlDataCellPrepared" OnDetailRowExpandedChanged="xgrdProds_DetailRowExpandedChanged"
            ClientInstanceName="grid" Theme="Metropolis">
            <SettingsBehavior ConfirmDelete="true" ColumnResizeMode="Control" EnableRowHotTrack="true" AllowEllipsisInText="True"/>
            <Settings HorizontalScrollBarMode="Auto" ShowFilterRow="True" ShowHeaderFilterButton="true" GridLines="Horizontal" 
                ShowFilterBar="Visible" ShowFilterRowMenu="true" ShowFooter="true" VerticalScrollBarMode="Hidden" ShowGroupPanel="true"/>
		    <SettingsPager Mode="ShowPager" PageSize="14" AllButton-Visible="true" Position="Bottom" ShowEmptyDataRows="true">
		        <AllButton Visible="true"/>
                <FirstPageButton Visible="true"/>
                <LastPageButton Visible="true"/>
                <PageSizeItemSettings ShowAllItem="true" Visible="true"/>
            </SettingsPager>
            <SettingsText ConfirmDelete="Are you sure you want to enable/disable this Solicitud"/> 
            <Columns>
                <dx:GridViewDataTextColumn FieldName="ctrlProdsID" Visible="false" Caption="Identifier" VisibleIndex="0"/>
                <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="1" Width="1%"/>
                <dx:GridViewDataColumn FieldName="ctrlProdsID" VisibleIndex="2"  Caption="" CellStyle-HorizontalAlign="Left" Width="30px">
                      <DataItemTemplate>
                          <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/Assets/images/layout_edit.png" OnClientClick='<%# string.Format("redirect(\"{0}\"); return true;", Eval("ctrlProdsID")) %>'  OnClick="imgEditar_Click" CommandArgument='<%# Eval("ctrlProdsID") %>' />
                      </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="noDocumento" VisibleIndex="3" Caption="Doc. Number" Width="50px" CellStyle-ForeColor="#FF0000" />
                <dx:GridViewDataTextColumn FieldName="codigoArticulo" VisibleIndex="4" Caption="Part Number" CellStyle-ForeColor="#0066FF"/>
                <dx:GridViewDataTextColumn FieldName="descripcion" VisibleIndex="5" Caption="Description" Width="200px"/>
                <dx:GridViewDataTextColumn FieldName="Marca" VisibleIndex="6" Caption="Marca"/>
                <dx:GridViewDataTextColumn FieldName="modelo" VisibleIndex="7" Caption="Model"/>
                <dx:GridViewDataTextColumn FieldName="usuario" VisibleIndex="8" Caption="User"/>
                <dx:GridViewDataTextColumn FieldName="fechaSolicitud" VisibleIndex="9" Caption="Request Date"/>
                <dx:GridViewDataTextColumn FieldName="codigoSolicitante" VisibleIndex="10" Caption="Applicant"/>
                <dx:GridViewDataTextColumn FieldName="Departamento" VisibleIndex="11" Caption="Depto"/>
                <dx:GridViewDataTextColumn FieldName="Comprador" VisibleIndex="12" Caption="Buyer"/>
                <dx:GridViewDataTextColumn FieldName="totalAnual" VisibleIndex="13" Caption="total annual"/>
                <dx:GridViewDataTextColumn FieldName="unidadMedida" VisibleIndex="14" Caption="UM"/>
                <dx:GridViewDataTextColumn FieldName="Subcategoria1" VisibleIndex="15" Caption="Subcategory 1"/>
                <dx:GridViewDataTextColumn FieldName="GlClass" VisibleIndex="16" Caption="GL Class"/>
                <dx:GridViewDataTextColumn FieldName="operacion" VisibleIndex="17" Caption="Request Type"/>
                <dx:GridViewDataTextColumn FieldName="sts_Prods" VisibleIndex="18" Caption="Status" CellStyle-ForeColor="#0066FF"/>
            </Columns>
            <GroupSummary>
                <dx:ASPxSummaryItem SummaryType="Count" />
            </GroupSummary>
            <Styles>
                <AlternatingRow BackColor="#F4F4F4"/>
                <RowHotTrack BackColor="#CEECF5"/>
                <Header BackColor="#F2F2F2" HorizontalAlign="Center" Font-Bold="true" CssClass="text-center"/>
                <GroupRow Font-Italic="true" ForeColor="#0066FF"/>
            </Styles>
        </dx:ASPxGridView>
    </div>
</asp:Content>
