<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CatUsuarios.aspx.cs" Inherits="ControlProductos.CatUsuarios" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/jscript">
        function OnEndCallback(s, e) {
            if (s.cpAlertMessage != '') {
                //alert(s.cpAlertMessage);
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

        function OnCallback() {
             grid.PerformCallback('Search');
        }

        function raiseValidation() {
            if (ASPxClientEdit.ValidateEditorsInContainer(null))
                grid.UpdateEdit();
        }
                
        function OnCmbAppEditChanged(cmbAppEdit) {
            cmbPerfileEdit.PerformCallback(cmbAppEdit.GetValue().toString());
        }

        function OnCmbPerfileEditChanged(cmbPerfileEdit) {
            cbCombos.PerformCallback("IdPerfile " + cmbPerfileEdit.GetSelectedItem().value);
        }

        function OnAccessEndCallback(s, e) {
            if (s.cpAlertMessage != '') {
                alert(s.cpAlertMessage);
            }
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
                <dx:NavBarGroup Text="Users\ Filters ">
                    <ContentTemplate>
                        <div id="Parametros" runat="server" visible="true">                                            
                            <table style="float: left; width: 40%; margin-left:5px" class="OptionsTable BottomMargin">
                                <tr style="height:10px"></tr> 
                                <tr>
                                    <td  style="width:10%; text-align:left">
                                        <dx:ASPxLabel ID="lblPosicion" runat="server" Text="Position: "></dx:ASPxLabel>
                                    </td>
                                    <td  style="width:30%; text-align:left">
                                        <dx:ASPxComboBox  ID="cmbPosicion" runat="server" CssClass="labelGral" Width="200px" FocusedStyle-Border-BorderColor="#3399ff"
                                            Font-Names="Segoe UI" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" FilterMinLength="0" 
                                            EnableCallbackMode="True" CallbackPageSize="10"  Theme="SoftOrange" AnimationType="Fade">
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr style="height:10px"></tr> 
                                <tr>
                                    <td  style="width:10%; text-align:left">
                                        <dx:ASPxLabel ID="lblProveedor" runat="server" Text="Supplier: "></dx:ASPxLabel>
                                    </td>
                                    <td  style="width:30%; text-align:left">
                                        <dx:ASPxComboBox  ID="cmbProveedor" runat="server" CssClass="labelGral" Width="200px" FocusedStyle-Border-BorderColor="#3399ff"
                                            Font-Names="Segoe UI" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" FilterMinLength="0" 
                                            EnableCallbackMode="True" CallbackPageSize="10"  Theme="SoftOrange" AnimationType="Fade">
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr style="height:10px"></tr> 
                                <tr>
                                    <td  style="width:10%; text-align:left">
                                        <dx:ASPxLabel ID="lblUsuario" runat="server" Text="User: "></dx:ASPxLabel>
                                    </td>
                                    <td  style="width:30%; text-align:left">
                                        <dx:ASPxTextBox runat="server" ID="xtxtUsuario" theme="SoftOrange" Enabled="True" Width="100%" FocusedStyle-Border-BorderColor="#3399ff" FocusedStyle-Border-BorderStyle="Double">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr style="height:10px"></tr> 
                                <tr>
                                    <td  style="width:10%; text-align:left">
                                        <dx:ASPxLabel ID="lblNombre" runat="server" Text="Name: "></dx:ASPxLabel>
                                    </td>
                                    <td  style="width:30%; text-align:left">
                                        <dx:ASPxTextBox runat="server" theme="SoftOrange" ID="xtxtNombreCompleto" Enabled="True" Width="100%" FocusedStyle-Border-BorderColor="#3399ff" FocusedStyle-Border-BorderStyle="Double">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr style="height:10px"></tr>
                                <tr>
                                    <td  style="width:10%; text-align:left">
                                        <dx:ASPxCheckBox ID="chkActive" runat="server" Theme="SoftOrange"
                                        Text="Active Records" Checked="true" TextAlign="Left"></dx:ASPxCheckBox>
                                    </td>
                                    <td  style="width:20%; text-align:right">                                                      
                                        <dx:ASPxButton ID="btnBuscar" runat="server" Text="Search" Theme="SoftOrange" AutoPostBack="false" >                                                             
                                            <ClientSideEvents Click="OnCallback" />
                                        </dx:ASPxButton>
                                    </td>                                                   
                                </tr>
                                <tr style="height:10px"></tr>
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
                                                                        <img class="img" src="Assets/Images/New.png" alt="New" style="cursor:pointer" onclick="return grid.AddNewRow();" title="New" />
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
        <dx:ASPxGridView ID="xgrdUser" runat="server" ClientInstanceName="grid" AutoGenerateColumns="true"  Width="100%" Font-Names="Segoe UI" Theme="Metropolis" KeyFieldName="UsuarioId"
            oncustomcallback="xgrdUser_CustomCallback" onrowdeleting="xgrdUser_RowDeleting"  onrowinserting="xgrdUser_RowInserting"  onrowupdating="xgrdUser_RowUpdating" onhtmleditformcreated="xgrdUser_HtmlEditFormCreated" 
            OnHtmlDataCellPrepared="xgrdUser_HtmlDataCellPrepared" StylesPopup-Common-Content-Paddings-Padding="0" StylesPopup-Common-Content-Border-BorderStyle="None" StylesPopup-Common-Content-BorderWidth="0"
             StylesPopup-Common-PopupControl-BackColor="Transparent" StylesPopup-Common-PopupControl-Paddings-Padding="0" StylesPopup-Common-PopupControl-Border-BorderStyle="None" StylesPopup-Common-PopupControl-Border-BorderWidth="0">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="UsuarioId" Visible="false" Caption="Identifier"/>
                <dx:GridViewDataTextColumn FieldName="EmpleadoId" Visible="false" Caption="Identifier"/>  
                <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="1" Width="5%"/>
                <dx:GridViewDataTextColumn FieldName="Username" VisibleIndex="3" Caption="Username" Width="10%"/>                         
                <dx:GridViewDataTextColumn FieldName="CodigoPerfil" VisibleIndex="4" Caption="CodigoPerfil" Width="5%" Visible="false"/>  
                <dx:GridViewDataTextColumn FieldName="Perfil" VisibleIndex="5" Caption="Profile" Width="10%"/>                                            
                <dx:GridViewDataTextColumn FieldName="Codigo" VisibleIndex="6" Caption="Employee" Width="5%"/>  
                <dx:GridViewDataTextColumn FieldName="NombreCompleto" VisibleIndex="7" Caption="Name" Width="30%"/>
                <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="8" Caption="Email" Width="20%"/>        
                <dx:GridViewDataTextColumn FieldName="CodigoPosicion" VisibleIndex="9" Caption="CodePosition" Width="5%"/>  
                <dx:GridViewDataTextColumn FieldName="Posicion" VisibleIndex="10" Caption="Position" Width="10%"/>
            </Columns>
            <SettingsBehavior ConfirmDelete="true"/>
            <SettingsPager Mode="ShowPager" PageSize="13"/>            
            <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
            <SettingsPopup>
                <EditForm Width="450" Modal="true" ShowHeader="false" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" AllowResize="true" ResizingMode="Live"/>
            </SettingsPopup>
            <Settings ShowFilterRow="True" />
            <SettingsText ConfirmDelete="Are you sure you want to enable/disable this record"/> 
            <Styles>
                <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                <RowHotTrack BackColor="#CEECF5"></RowHotTrack>  
                <Header BackColor="#F2F2F2"></Header>                       
            </Styles>
            <SettingsBehavior EnableRowHotTrack="true" />             
            <ClientSideEvents EndCallback="OnEndCallback"  RowDblClick="function(s, e){                                                                                                        
                        s.StartEditRow(e.visibleIndex);  
            }"/>     
            <Templates>
                <EditForm>
                  <div class="container col-xs-12" style="background-color:#F2F2F2;padding:0px;margin:0px;">
                    <div class="form-group" style="width:100%;padding:0px;margin:0px;">
                        <div class="btn-group-sm" style="background-image:url('Images/GreenBar.png');vertical-align:middle; height:32px;">
                            <img src="Assets/Images/save.png" class="btn" alt="Guardar" style="cursor:pointer" onclick="return raiseValidation();"/>
                            <img src="Assets/Images/Close.png" class="btn pull-right" alt="Cerrar" style="cursor:pointer; margin-left:2px;" onclick="return grid.CancelEdit();"/>
                        </div>
                        <h4 class="modal-title" style="text-align:right;margin-top:10px;margin-bottom:10px;margin-right:5px;">User Registration</h4>
                        <h6 style="font-size:medium;align-content:center;background-color:#E0E0E0;height:24px;margin:1px 1px 5px 1px;padding-top:3px;">Description</h6>
                        <div class="col-xs-12">
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="lblUserName" Text="User" Font-Names="Segoe UI"></dx:ASPxLabel>
                                <div class="col-md-9">
                                    <dx:ASPxTextBox CssClass="form-control input-sm Campos" runat="server" ID="xtxtUserName" theme="SoftOrange" MaxLength="100" Text='<%# Eval("Username")%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                        <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                            <RequiredField IsRequired="true" ErrorText="Field Required" />
                                        </ValidationSettings>                                        
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="row" id="divPassword" runat="server">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="lblPassword" Text="Password" Font-Names="Segoe UI"></dx:ASPxLabel>
                                <div class="col-md-9">
                                    <dx:ASPxTextBox CssClass="form-control input-sm Campos" runat="server" ID="xtxtPassword" theme="SoftOrange" MaxLength="100" Text="" Font-Names="Segoe UI" Password="true" FocusedStyle-Border-BorderColor="#3399ff">
                                        <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                            <RequiredField IsRequired="true" ErrorText="Field Required" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="row" id="divConfirm" runat="server">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="lblConfirm" Text="Confirm" Font-Names="Segoe UI"></dx:ASPxLabel>
                                <div class="col-md-9">
                                    <dx:ASPxTextBox CssClass="form-control input-sm Campos" runat="server" ID="xtxtConfirm" theme="SoftOrange" MaxLength="100" Text="" Font-Names="Segoe UI" Password="true" FocusedStyle-Border-BorderColor="#3399ff">
                                        <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                            <RequiredField IsRequired="true" ErrorText="Campos requerido" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="lblEmpleado" Text="Employee" Font-Names="Segoe UI"></dx:ASPxLabel>
                                <div class="col-md-9">
                                    <input type="hidden" id="hdnEmpleadoId" value='<%# Eval("EmpleadoId")%>' runat="server" />
                                    <dx:ASPxComboBox  CssClass="form-control input-sm Campos" ID="cmbEmpleadoEdit" runat="server" FocusedStyle-Border-BorderColor="#3399ff"
                                    Font-Names="Segoe UI" IncrementalFilteringMode="Contains" FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="10" Theme="SoftOrange" 
                                    OnDataBound="cmbEmpleadoEdit_DataBound" PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Selecciona una opción"/>
                                        </ValidationSettings>
                                        <ClientSideEvents />
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <dx:ASPxLabel CssClass="control-label col-md-3" runat="server" ID="ASPxLabel2" Text="Profile" Font-Names="Segoe UI"></dx:ASPxLabel>
                                <div class="col-md-9">
                                    <input type="hidden" id="hdnCodigoPerfil" value='<%# Eval("CodigoPerfil")%>' runat="server" />
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbCodigoPerfilEdit" runat="server" FocusedStyle-Border-BorderColor="#3399ff"
                                        Font-Names="Segoe UI" IncrementalFilteringMode="Contains" FilterMinLength="0" 
                                        EnableCallbackMode="True" CallbackPageSize="10" Theme="SoftOrange" OnDataBound="cmbCodigoPerfilEdit_DataBound"
                                         PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                            <ValidationSettings>
                                                <RequiredField  IsRequired="true" ErrorText="Selecciona una opción"/>
                                            </ValidationSettings>
                                            <ClientSideEvents />
                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
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
