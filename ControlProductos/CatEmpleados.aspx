<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CatEmpleados.aspx.cs" Inherits="ControlProductos.CatEmpleados" %>
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
                swal("Information", "File uploaded successfully", "success");
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
    </style>
    <div id="Busqueda" runat="server">
        <dx:ASPxNavBar ID="ASPxNavBar2" runat="server" Theme="Metropolis" Width="100%" Font-Bold="False">
            <Groups>
                <dx:NavBarGroup Text="Employees\ Filters">
                    <ContentTemplate>
                        <div id="Parametros" runat="server" visible="true">
                            <table style="float: left; width: 40%"" class="OptionsTable BottomMargin">
                                <tr style="height:10px"></tr> 
                                <tr>
                                    <td  style="width:10%; text-align:left">
                                        <dx:ASPxLabel ID="lblPosicion" runat="server" Text="Position: "/>
                                    </td>
                                    <td  style="width:30%; text-align:left">
                                        <dx:ASPxComboBox  ID="cmbPosicion" runat="server" CssClass="labelGral" Width="200px" FocusedStyle-Border-BorderColor="#3399ff" Font-Names="Segoe UI"  IncrementalFilteringMode="Contains" FilterMinLength="0" 
                                            EnableCallbackMode="True" CallbackPageSize="20"  Theme="SoftOrange" PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr style="height:10px"></tr> 
                                <tr>
                                    <td  style="width:10%; text-align:left">
                                        <dx:ASPxLabel ID="lblProveedor" runat="server" Text="Supplier: "/>
                                    </td>
                                    <td  style="width:30%; text-align:left">
                                        <dx:ASPxComboBox  ID="cmbProveedor" runat="server" CssClass="labelGral" Width="200px" FocusedStyle-Border-BorderColor="#3399ff"
                                            Font-Names="Segoe UI"  IncrementalFilteringMode="Contains" FilterMinLength="0" 
                                            EnableCallbackMode="True" CallbackPageSize="20"  Theme="SoftOrange" >
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr style="height:10px"></tr> 
                                <tr>
                                    <td  style="width:10%; text-align:left">
                                            <dx:ASPxLabel ID="lblCodigo" runat="server" Text="Code: ">
                                    </dx:ASPxLabel>
                                    </td>
                                    <td  style="width:30%; text-align:left">
                                        <dx:ASPxTextBox runat="server" Theme="SoftOrange" Height="5px"  ID="xtxtCodigo" Enabled="True" Width="100%" FocusedStyle-Border-BorderColor="#3399ff" FocusedStyle-Border-BorderStyle="Double">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr style="height:5px"></tr>
                                <tr>
                                    <td  style="width:10%; text-align:left">
                                        <dx:ASPxLabel ID="lblName" runat="server" Text="Name: ">
                                    </dx:ASPxLabel>
                                    </td>
                                    <td  style="width:30%; text-align:left">
                                        <dx:ASPxTextBox runat="server" theme="SoftOrange" ID="xtxtNombreCompleto" Enabled="True" Width="100%" FocusedStyle-Border-BorderColor="#3399ff" FocusedStyle-Border-BorderStyle="Double">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr style="height:5px"></tr>
                                <tr>
                                    <td  style="width:10%; text-align:left">
                                        <dx:ASPxCheckBox ID="chkActive" runat="server" Theme="SoftOrange"
                                        Text="Active Records" Checked="true" TextAlign="Left"></dx:ASPxCheckBox>
                                    </td>
                                    <td  style="width:20%; text-align:right">
                                        <dx:ASPxButton ID="btnBuscar" runat="server" Text="Search" Theme="SoftOrange" AutoPostBack="false" >
                                            <ClientSideEvents Click="OnCallback"/>
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
                                                                        <img class="img" src="Assets/Images/New.png" alt="New" style="cursor:pointer" onclick="return grid.AddNewRow();" title="New"/>
                                                                        </div>
                                                                    </div>
                                                                </td> 
                                                                <td style="width:20%">
                                                                        <div class="div-container">
                                                                        <div class="div-img" >
                                                                        <img class="img" src="Assets/Images/trash_can.png" alt="Remove Selected" style="cursor:pointer" onclick="return DisableSelected();" title="Remove Selected"/>
                                                                        </div>
                                                                        </div>
                                                                </td>
                                                                <td style="width:20%" >
                                                                    <div class="div-container">
                                                                        <div class="div-img" >
                                                                            <img class="img" src="Assets/Images/delete_all.png" alt="Remove All" style="cursor:pointer" onclick="return DisableAll();" title="Remove All"/> 
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                                <td style="width:20%" >
                                                                    <div class="div-container">
                                                                        <div class="div-img" >                                                                                           
                                                                            <asp:ImageButton CssClass="img" ID="ImageButton1" runat="server" ImageUrl="Assets/Images/dowload_xls.png" AlternateText="Dowload xls" OnClick="ImageButton1_Click" title="Dowload xls"/>                                                                                       
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                                <td style="width:20%" >
                                                                    <div class="div-container">
                                                                        <div class="div-img" >
                                                                            <img class="img" ID="ImageButton2" runat="server" src="Assets/Images/upload_xls.png" alt="Upload xls" style="cursor:pointer" onclick="return UploadFileXLS();" title="Upload xls"/>                                                                                                                                                                                     
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxCallbackPanel ID="CallbackPanelDisable" ClientInstanceName="CallbackPanelDisable" runat="server" Width="200px" oncallback="CallbackPanelDisable_Callback"/>
                                                                    <dx:ASPxCallbackPanel ID="CallbackPanelDisableAll" ClientInstanceName="CallbackPanelDisableAll" runat="server" Width="200px" oncallback="CallbackPanelDisableAll_Callback"/>
                                                                    <dx:ASPxCallbackPanel ID="CallbackPanelDowload" ClientInstanceName="CallbackPanelDowload" runat="server" Width="200px" oncallback="CallbackPanelDowload_Callback"/>      
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
    <dx:ASPxPanel ID="PanelUploadFile" runat="server" Width="100%" ClientInstanceName="PanelUploadFile" ClientVisible="false" Theme="Metropolis">
        <PanelCollection>
            <dx:PanelContent>
                <div id="DivUploadFile" runat="server" visible="true" >
                    <table class="TablaFiltros" style="text-align:center; width:100%; margin-left:10px; margin-top:10px">
                        <tr>
                            <td style="text-align:left; " colspan="2" >
                                <asp:Label ID="lblUploadFile" runat="server" Text="Cargar Archivo" Font-Bold="True" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width:100%;text-align:left"></td>
                        </tr>   
                        <tr>
                            <td colspan="1" style="width:70%; text-align:right">
                                <asp:Label ID="Label19" runat="server" Text=" Tipo de archivos: xls, xlsx."></asp:Label>
                            </td>
                            <td colspan="1" style="width:30%"></td>
                        </tr>                   
                        <tr>
                            <td style="width:70%" colspan="1">
                                <dx:ASPxUploadControl ID="uplReceiptFileExcel" runat="server" 
                                    ClientInstanceName="uploaderReceiptExcel" CssClass="labelGral" Height="58px" NullText="Click aqui para buscar archivos..." 
                                    OnFileUploadComplete="uplReceipt_FileUploadCompleteExcel" ShowProgressPanel="True" Theme="Metropolis" Width="100%">
                                    <ValidationSettings AllowedFileExtensions=".xls, .xlsx" MaxFileSize="4194304"></ValidationSettings>
                                    <ClientSideEvents FilesUploadComplete="function(s, e) { UploaderReceipt_OnFilesUploadCompleteExcel(e); }" 
                                        FileUploadComplete="function(s, e) { UploaderReceipt_OnFileUploadCompleteExcel(e); }" 
                                        FileUploadStart="function(s, e) { UploaderReceipt_OnUploadStartExcel(); }" 
                                        TextChanged="function(s, e) { UpdateUploadReceiptButtonExcel(); }"/>
                                    <AdvancedModeSettings>
                                        <FileListItemStyle CssClass="pending dxucFileListItem"></FileListItemStyle>
                                    </AdvancedModeSettings>
                                    <ButtonStyle CssClass="labelGral" Font-Size="10pt"></ButtonStyle>
                                </dx:ASPxUploadControl>
                            </td>
                            <td style="vertical-align:top; width:30px; text-align:center" colspan="1">
                                <dx:ASPxButton ID="btnUploadReceiptExcel" runat="server" AutoPostBack="False" Theme="Metropolis" 
                                    ClientEnabled="False" ClientInstanceName="btnUploadReceiptExcel" Height="18px" Text="Upload File" Width="20%" >
                                    <ClientSideEvents Click="function(s, e) { uploaderReceiptExcel.Upload(); }"/>
                                </dx:ASPxButton>                   
                            </td>                            
                        </tr>
                    </table>
                </div>            
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
    <div id="Detalle" runat="server">
         <dx:ASPxGridView ID="xgrdEmpleado" runat="server" ClientInstanceName="grid" AutoGenerateColumns="False" Width="100%" Font-Names="Segoe UI"
            KeyFieldName="EmpleadoId" oncustomcallback="xgrdEmpleado_CustomCallback" onrowdeleting="xgrdEmpleado_RowDeleting" onrowinserting="xgrdEmpleado_RowInserting" 
            onrowupdating="xgrdEmpleado_RowUpdating" onhtmleditformcreated="xgrdEmpleado_HtmlEditFormCreated" OnHtmlDataCellPrepared="xgrdEmpleado_HtmlDataCellPrepared"
            Theme="Metropolis" 
             StylesPopup-Common-Content-Paddings-Padding="0" StylesPopup-Common-Content-Border-BorderStyle="None" StylesPopup-Common-Content-BorderWidth="0"
             StylesPopup-Common-PopupControl-BackColor="Transparent" StylesPopup-Common-PopupControl-Paddings-Padding="0" StylesPopup-Common-PopupControl-Border-BorderStyle="None" StylesPopup-Common-PopupControl-Border-BorderWidth="0">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="EmpleadoId" VisibleIndex="0" Visible="false" Caption="Identifier"/>
                <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="1" Width="5%"/>
                <dx:GridViewDataTextColumn FieldName="Codigo" VisibleIndex="2" Caption="Code" Width="5%"/>                     
                <dx:GridViewDataTextColumn FieldName="NombreCompleto" VisibleIndex="3" Caption="Name" Width="15%"/>
                <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="4" Caption="Email" Width="10%"/>
                <dx:GridViewDataTextColumn FieldName="Credencial" VisibleIndex="5" Caption="Credential" Width="5%"/>
                <dx:GridViewDataTextColumn FieldName="NSS" VisibleIndex="6" Caption="NSS" Width="5%"/>     
                <dx:GridViewDataTextColumn FieldName="CodigoPlanta" VisibleIndex="7" Caption="CodigoPlanta" Width="5%" visible="false"/>
                <dx:GridViewDataTextColumn FieldName="Planta" VisibleIndex="8" Caption="Plant" Width="10%"/>
                <dx:GridViewDataTextColumn FieldName="CodigoDepto" VisibleIndex="9" Caption="CodigoDepto" Width="5%" Visible="false"/>
                <dx:GridViewDataTextColumn FieldName="Departamento" VisibleIndex="10" Caption="Department" Width="10%"/>    
                <dx:GridViewDataTextColumn FieldName="CodigoPosicion" VisibleIndex="11" Caption="CodigoPosicion" Width="5%" Visible="false"/>
                <dx:GridViewDataTextColumn FieldName="Posicion" VisibleIndex="12" Caption="Position" Width="10%"/>
                <dx:GridViewDataTextColumn FieldName="CodigoProveedor" VisibleIndex="13" Caption="CodigoProveedor" Width="5%" Visible="false"/>
                <dx:GridViewDataTextColumn FieldName="Proveedor" VisibleIndex="14" Caption="Supplier" Width="10%"/>
            </Columns>
            <Styles>
                <AlternatingRow BackColor="#F2F2F2"/>
                <RowHotTrack BackColor="#CEECF5"/>     
                <Header BackColor="#F2F2F2"/>                                         
            </Styles>
            <SettingsBehavior EnableRowHotTrack="true"/>             
            <SettingsPager Mode="ShowPager" PageSize="13"/>
            <SettingsEditing Mode="PopupEditForm">                                            
                </SettingsEditing>
            <Settings ShowFilterRow="True"/>
            <SettingsBehavior ConfirmDelete="true"/>
                <SettingsPopup EditForm-CloseOnEscape="True" >
                        <EditForm Width="800" Modal="true" ShowHeader="false" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" AllowResize="true" ResizingMode="Live"/>
                </SettingsPopup>
            <SettingsText ConfirmDelete="Are you sure you want to enable/disable this record"/> 
            <ClientSideEvents EndCallback="OnEndCallback"  RowDblClick="function(s, e){                                                                                                        
                        s.StartEditRow(e.visibleIndex);  
            }"/>       
            <Templates>
                <EditForm>
                  <div class="container col-lg-12 col-md-11 col-sm-10 col-xs-5" style="background-color:#F2F2F2;padding:0px;margin:0px;">
                    <div class="form-group" style="width:100%;padding:0px;margin:0px;">
                        <div class="btn-group-sm" style="background-image:url('Images/GreenBar.png');vertical-align:middle; height:32px;">
                            <img src="Assets/Images/save.png" class="btn" alt="Guardar" style="cursor:pointer" onclick="return raiseValidation();"/>
                            <img src="Assets/Images/Close.png" class="btn pull-right" alt="Cerrar" style="cursor:pointer; margin-left:2px;" onclick="return grid.CancelEdit();"/>
                        </div>
                        <h4 class="modal-title" style="text-align:right;margin-top:10px;margin-bottom:10px;margin-right:5px;">Employee registration</h4>
                        <h6 style="font-size:medium;align-content:center;background-color:#E0E0E0;height:24px;margin:1px 1px 5px 1px;padding-top:3px;">Description</h6>
                        <div class="col-md-6">
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="ASPxLabel5" Text="Position" Font-Names="Segoe UI"/>
                                <div class="col-md-9">
                                    <input  type="hidden" id="hdnCodigoPosicion" value='<%# Eval("CodigoPosicion")%>' runat="server"/>
                                    <dx:ASPxComboBox  ID="cmbPosicionEdit" runat="server" CssClass="form-control input-sm Campos" FocusedStyle-Border-BorderColor="#3399ff" Font-Names="Segoe UI"  IncrementalFilteringMode="Contains" FilterMinLength="0" 
                                        EnableCallbackMode="True" CallbackPageSize="20" Theme="SoftOrange" OnDataBound="cmbPosicionEdit_DataBound" PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Selecciona una opción"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="lblProveedor" Text="Supplier" Font-Names="Segoe UI"/>
                                <div class="col-md-9">
                                    <input type="hidden" id="hdnCodigoProveedor" value='<%# Eval("CodigoProveedor")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbProveedorEdit" runat="server" FocusedStyle-Border-BorderColor="#3399ff"
                                    Font-Names="Segoe UI" IncrementalFilteringMode="Contains" FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" Theme="SoftOrange" OnDataBound="cmbProveedorEdit_DataBound" PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Selecciona una opción"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="ASPxLabel6" Text="Code" Font-Names="Segoe UI"/>
                                <div class="col-md-9">
                                    <dx:ASPxTextBox CssClass="form-control input-sm Campos" runat="server" ID="xtxtKeyEdit" theme="SoftOrange" MaxLength="50" Text='<%# Eval("Codigo")%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                            <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Selecciona solo caracteres validos"/>
                                                <RequiredField IsRequired="true" ErrorText="Field Required"/>
                                            </ValidationSettings>                                                            
                                        </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="lblDescription" Text="Name" Font-Names="Segoe UI"/>
                                <div class="col-md-9">
                                    <dx:ASPxTextBox CssClass="form-control input-sm Campos" runat="server" ID="xtxtNombreEdit" theme="SoftOrange" MaxLength="100" Text='<%# Eval("NombreCompleto")%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                        <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                            <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Selecciona solo caracteres validos"/>
                                            <RequiredField IsRequired="true" ErrorText="Field Required"/>
                                        </ValidationSettings>                                                            
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="lblEmail" Text="Email" Font-Names="Segoe UI"/>
                                <div class="col-md-9">
                                    <dx:ASPxTextBox CssClass="form-control input-sm Campos" runat="server" ID="xtxtEmailEdit" theme="SoftOrange" MaxLength="100" Text='<%# Eval("Email")%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                        <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true"></ValidationSettings>
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">            
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="lblPlanta" Text="Plant" Font-Names="Segoe UI"/>
                                <div class="col-md-9">
                                    <input type="hidden" id="hdnCodigoPlanta" value='<%# Eval("CodigoPlanta")%>' runat="server"/>
                                    <dx:ASPxComboBox  CssClass="form-control input-sm Campos" ID="cmbPlantaEdit" runat="server" FocusedStyle-Border-BorderColor="#3399ff" Font-Names="Segoe UI" IncrementalFilteringMode="Contains" FilterMinLength="0" 
                                    EnableCallbackMode="True" CallbackPageSize="20"  Theme="SoftOrange" OnDataBound="cmbPlantaEdit_DataBound" PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Selecciona una opción"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="lblDepto" Text="Department" Font-Names="Segoe UI"/>
                                <div class="col-md-9">
                                    <input type="hidden" id="hdnCodigoDepto" value='<%# Eval("CodigoDepto")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbDeptoEdit" runat="server" FocusedStyle-Border-BorderColor="#3399ff" Font-Names="Segoe UI" IncrementalFilteringMode="Contains" FilterMinLength="0" 
                                    EnableCallbackMode="True" CallbackPageSize="20"  Theme="SoftOrange" OnDataBound="cmbDeptoEdit_DataBound" PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Selecciona una opción"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="ASPxLabel1" Text="Credential" Font-Names="Segoe UI"/>
                                <div class="col-md-9">
                                    <dx:ASPxTextBox CssClass="form-control input-sm Campos" runat="server" ID="xtxtCredencialEdit" theme="SoftOrange" MaxLength="100" Text='<%# Eval("Credencial")%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                        <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                            <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Selecciona solo caracteres validos"/>
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="ASPxLabel4" Text="NSS" Font-Names="Segoe UI"/>
                                <div class="col-md-9">
                                    <dx:ASPxTextBox CssClass="form-control input-sm Campos" runat="server" ID="xtxtNSSEdit" theme="SoftOrange" MaxLength="100" Text='<%# Eval("NSS")%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                        <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                            <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Selecciona solo caracteres validos"/>
                                        </ValidationSettings>                                                            
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                  </div>
                </EditForm>
            </Templates>                   
        </dx:ASPxGridView>
    </div>
</asp:Content>
