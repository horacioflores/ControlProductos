<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CatProveedor.aspx.cs" Inherits="ControlProductos.CatProveedor" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
        .auto-style2 {
            height: 23px;
        }
    </style>
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

        function OnFabricanteEndCallback(s, e) {
            if (s.cpAlertMessage != '') {
                if (s.cpAlertMessage == 'SelectOne') {
                    swal("Information!", "Select a marker to add to the list!", "info");
                } else if (s.cpAlertMessage == 'Exist') {
                    swal("Information!", "The marker is already added to the list", "info");
                }
                //alert(s.cpAlertMessage);
            }
        }

        function DisableFabricantesSelected() {
            var Valores = "";
            $(".chkFab").each(function () {
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

            xgrdFabricantes.PerformCallback(Valores);
        }

    </script> 
    <style>
        .Campos {
            height: 25px;
            background-color:white;
            margin:1px;
            padding:2px;
            border: 1px solid #a9a9a9;
            font-size:small;
            border-radius: 3px;
        }

        /* Style the tab */
        .tab {
          overflow: hidden;
          border: 1px solid #ccc;
          background-color: #f1f1f1;
        }

        /* Style the buttons inside the tab */
        .tab button {
          background-color: inherit;
          float: left;
          border: none;
          outline: none;
          cursor: pointer;
          padding: 14px 16px;
          transition: 0.3s;
          font-size: 17px;
        }

        /* Change background color of buttons on hover */
        .tab button:hover {
          background-color: #ddd;
        }

        /* Create an active/current tablink class */
        .tab button.active {
          background-color: #ccc;
        }

        /* Style the tab content */
        .tabcontent {
          display: none;
          padding: 6px 12px;
          border: 1px solid #ccc;
          border-top: none;
          justify-content: center;
          align-items: center;
        }
    </style>
    <div id="Busqueda" runat="server">
        <dx:ASPxNavBar ID="ASPxNavBar2" runat="server" Theme="Metropolis" Width="100%" 
            Font-Bold="False">
            <Groups>
                <dx:NavBarGroup Text="Supplier\ Filters">
                        <ContentTemplate>
                                <div id="Parametros" runat="server" visible="true">
                                            
                                <table style="float: left; width: 40%"" class="OptionsTable BottomMargin">
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
                                                    <dx:ASPxLabel ID="lblDescripcion" runat="server" Text="Description: ">
                                            </dx:ASPxLabel>
                                            </td>
                                            <td  style="width:30%; text-align:left">
                                                <dx:ASPxTextBox runat="server" theme="SoftOrange" ID="xtxtDescripcion" Enabled="True" Width="100%" FocusedStyle-Border-BorderColor="#3399ff" FocusedStyle-Border-BorderStyle="Double">
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
        <dx:ASPxGridView ID="xgrdProveedor" runat="server" ClientInstanceName="grid" Width="100%" AutoGenerateColumns="False" Font-Names="Segoe UI" Theme="Metropolis"
            KeyFieldName="ProveedorId" OnCustomCallback="xgrdProveedor_CustomCallback" OnRowValidating="xgrdProveedor_RowValidating" 
            OnRowDeleting="xgrdProveedor_RowDeleting" OnRowInserting="xgrdProveedor_RowInserting" OnRowUpdating="xgrdProveedor_RowUpdating"
            OnHtmlDataCellPrepared="xgrdProveedor_HtmlDataCellPrepared" OnHtmlEditFormCreated="xgrdProveedor_HtmlEditFormCreated" OnInitNewRow="xgrdProveedor_InitNewRow"
            OnStartRowEditing="xgrdProveedor_StartRowEditing"
            >
            <Columns>
                <dx:GridViewDataTextColumn FieldName="ProveedorId" Visible="false" Caption="Identifier" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="1" Width="5%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Codigo" VisibleIndex="2" Caption="Code" Width="5%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Nombre" VisibleIndex="3" Caption="Name" Width="25%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NombreCorto" VisibleIndex="4" Caption="Short Name" Width="5%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Correo" VisibleIndex="5" Caption="Email" Width="10%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Direccion1" VisibleIndex="6" Caption="Direction" Width="15%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Direccion2" VisibleIndex="7" Caption="Direcction" Width="15%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Ciudad" VisibleIndex="8" Caption="City" Width="5%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Estado" VisibleIndex="9" Caption="State" Width="5%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CP" VisibleIndex="10" Caption="Postal Code" Width="5%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Pais" VisibleIndex="11" Caption="Country" Width="5%">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior ConfirmDelete="true"/>
            <SettingsPager Mode="ShowPager" PageSize="100"/>
            <SettingsEditing Mode="PopupEditForm">                                            
            </SettingsEditing>
            <SettingsPopup >
                    <EditForm Width="600"  Modal="true" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" ShowHeader="false"/>
            </SettingsPopup>
            <Settings ShowFilterRow="True" />                                        
            <SettingsText ConfirmDelete="Are you sure you want to enable/disable this record"/>  
            <Styles>
                <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                <RowHotTrack BackColor="#CEECF5"></RowHotTrack>  
                <Header BackColor="#F2F2F2"></Header>                                           
            </Styles>
            <SettingsBehavior EnableRowHotTrack="true" />             
            <ClientSideEvents EndCallback="OnEndCallback"  RowDblClick="function(s, e){ s.StartEditRow(e.visibleIndex); }"/>      
            <Templates>
                <EditForm>
                    <div style="margin:1px 1px 1px 1px; background-color:#F2F2F2" >
                        <table style="width:100%; height:200px;">
                            <tr style="background-repeat:repeat-x; background-image:url('Assets/Images/GreenBar.png'); vertical-align:middle">
                                <td  style="height:22px; text-align:left">
                                    <img src="Assets/Images/save.png" alt="Guardar" title="Guardar" style="cursor:pointer" onclick="return raiseValidation();" />
                                </td>
                                <td  style="height:22px; text-align:right">
                                    <img src="Assets/Images/Close.png" alt="Cerrar" title="Cerrar" style="cursor:pointer" onclick="return grid.CancelEdit();" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height:40px; padding-right:10px; text-align:right" colspan="2">
                                    <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="Supplier registration" Font-Size="13pt" Font-Names="Segoe UI"></dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align:top" colspan="2">
                                    <tr style="width:100%; padding-left:10px; padding-right:10px; padding-bottom:20px">                                               
<%--                                        <tr>
                                            <td colspan="2" style="background-color:#E0E0E0; height:20px" >
                                                <dx:ASPxLabel runat="server" ID="ASPxLabel2" Text="Description" Font-Size="12pt" Font-Names="Segoe UI"></dx:ASPxLabel>
                                            </td>
                                        </tr>--%>
                                        <td colspan="2">
                                            <div class="tab" style="background-color:#12659D;color:white;">
                                                <button id="btnBasic" class="tablinks active"  onclick="openCity(event, 'basic'); return false;">&nbsp Basic Information</button>
                                                <%--<button id="btnMarkers" class="tablinks"  onclick="openCity(event, 'markers'); return false;">&nbsp Markers</button>--%>
                                            </div>
                                            <div id="basic" class="tabcontent" style="display:flex">
                                                <table style="width:100%">
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-bottom:5px; padding-top:5px">
                                                            <dx:ASPxLabel runat="server" ID="lblKey" Text="Code" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox runat="server" ID="xtxtProveedorId" Text='<%# Eval("ProveedorId")%>' Visible="false"></dx:ASPxTextBox>
                                                            <dx:ASPxTextBox runat="server" ID="xtxtKeyEdit" theme="SoftOrange" Width="50px" MaxLength="50" Text='<%# Eval("Codigo")%>' Font-Names="Segoe UI"
                                                                FocusedStyle-Border-BorderColor="#3399ff">
                                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                                    <RequiredField IsRequired="true" ErrorText="Field Required" />
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr> 
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-bottom:5px; padding-top:5px">
                                                            <dx:ASPxLabel runat="server" ID="lblNombre" Text="Name" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox runat="server" ID="xtxtNombreEdit" theme="SoftOrange" Width="400px" MaxLength="100" Text='<%# Eval("Nombre")%>' Font-Names="Segoe UI"
                                                                FocusedStyle-Border-BorderColor="#3399ff">
                                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                                    <RequiredField IsRequired="true" ErrorText="Field Required" />
                                                                </ValidationSettings>                                                            
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-bottom:5px; padding-top:5px">
                                                            <dx:ASPxLabel runat="server" ID="lblAbbrev" Text="Short Name" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox runat="server" ID="xtxtAbbrevEdit" theme="SoftOrange" Width="200px" MaxLength="100" Text='<%# Eval("NombreCorto")%>' Font-Names="Segoe UI"
                                                                FocusedStyle-Border-BorderColor="#3399ff">
                                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                                </ValidationSettings>                                                            
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-bottom:5px; padding-top:5px">
                                                            <dx:ASPxLabel runat="server" ID="lblDireccion1" Text="Direction 1" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox runat="server" ID="xtxtDireccion1Edit" theme="SoftOrange" Width="400px" MaxLength="100" Text='<%# Eval("Direccion1")%>' Font-Names="Segoe UI"
                                                                FocusedStyle-Border-BorderColor="#3399ff">
                                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                                </ValidationSettings>                                                            
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-bottom:5px; padding-top:5px">
                                                            <dx:ASPxLabel runat="server" ID="lblDireccion2" Text="Direction 2" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox runat="server" ID="xtxtDireccion2Edit" theme="SoftOrange" Width="400px" MaxLength="100" Text='<%# Eval("Direccion2")%>' Font-Names="Segoe UI"
                                                                FocusedStyle-Border-BorderColor="#3399ff">
                                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                                </ValidationSettings>                                                            
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-bottom:5px; padding-top:5px">
                                                            <dx:ASPxLabel runat="server" ID="lblCiudad" Text="City" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox runat="server" ID="xtxtCiudadEdit" theme="SoftOrange" Width="200px" MaxLength="100" Text='<%# Eval("Ciudad")%>' Font-Names="Segoe UI"
                                                                FocusedStyle-Border-BorderColor="#3399ff">
                                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                                </ValidationSettings>                                                            
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-bottom:5px; padding-top:5px">
                                                            <dx:ASPxLabel runat="server" ID="lblEstado" Text="State" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox runat="server" ID="xtxtEstadoEdit" theme="SoftOrange" Width="200px" MaxLength="100" Text='<%# Eval("Estado")%>' Font-Names="Segoe UI"
                                                                FocusedStyle-Border-BorderColor="#3399ff">
                                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                                </ValidationSettings>                                                            
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-bottom:5px; padding-top:5px">
                                                            <dx:ASPxLabel runat="server" ID="lblPais" Text="Country" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td class="auto-style1">
                                                            <dx:ASPxTextBox runat="server" ID="xtxtPaisEdit" theme="SoftOrange" Width="200px" MaxLength="100" Text='<%# Eval("Pais")%>' Font-Names="Segoe UI"
                                                                FocusedStyle-Border-BorderColor="#3399ff">
                                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                                </ValidationSettings>                                                            
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-bottom:5px; padding-top:5px">
                                                            <dx:ASPxLabel runat="server" ID="lblCP" Text="Description" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td class="auto-style2">
                                                            <dx:ASPxTextBox runat="server" ID="xtxtCPEdit" theme="SoftOrange" Width="200px" MaxLength="100" Text='<%# Eval("CP")%>' Font-Names="Segoe UI"
                                                                FocusedStyle-Border-BorderColor="#3399ff">
                                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                                </ValidationSettings>                                                            
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-bottom:5px; padding-top:5px">
                                                            <dx:ASPxLabel runat="server" ID="lblTelefono" Text="Tel" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox runat="server" ID="xtxtTelefonoEdit" theme="SoftOrange" Width="200px" MaxLength="100" Text='<%# Eval("Telefono")%>' Font-Names="Segoe UI"
                                                                FocusedStyle-Border-BorderColor="#3399ff">
                                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                                </ValidationSettings>                                                            
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-bottom:5px; padding-top:5px">
                                                            <dx:ASPxLabel runat="server" ID="lblCorreo" Text="Email" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox runat="server" ID="xtxtCorreoEdit" theme="SoftOrange" Width="200px" MaxLength="100" Text='<%# Eval("Correo")%>' Font-Names="Segoe UI"
                                                                FocusedStyle-Border-BorderColor="#3399ff">
                                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                    <RegularExpression ValidationExpression="[\.\-\_A-Za-z@\.\-\d\;]+" ErrorText="Introduzca sólo caracteres válidos" />
                                                                </ValidationSettings>
                                                                <%--<ClientSideEvents KeyPress="valCorreo" />   --%>                                                       
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div id="markers" class="tabcontent" style="display:none">
<%--                                                <table style="width:100%">
                                                    <tr>
                                                        <td>
                                                            <div class="row form-group">
                                                                <dx:ASPxLabel runat="server" Cssclass="text-form col-md-2" ID="ASPxlblmarker" Text="Marker" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                                <div class="col-md-4">
                                                                    <dx:ASPxComboBox  ID="cmbfabricanteEdit" runat="server" Width="100%" CssClass="form-control input-sm Campos" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px"
                                                                    Font-Names="Segoe UI" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" FilterMinLength="0" 
                                                                    EnableCallbackMode="True" CallbackPageSize="10"  Theme="SoftOrange" AnimationType="Fade"
                                                                    OnDataBound="cmbfabricanteEdit_DataBound">                                             
                                                                        <ClientSideEvents />
                                                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                                                    </dx:ASPxComboBox>
                                                                </div>
                                                                <div class="col-md-2">
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <img class="img" src="Assets/Images/New.png" alt="Add" title="Add" style="cursor:pointer" onclick="return xgrdFabricantes.PerformCallback('Save');"/>
                                                                    <img class="img" src="Assets/Images/trash_can.png" alt="Remove Selected" style="cursor:pointer" onclick="return DisableFabricantesSelected();"/>
                                                                    <img class="img" src="Assets/Images/delete_all.png" alt="Remove Selected" style="cursor:pointer" onclick="return xgrdFabricantes.PerformCallback('Delete');"/>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr> 
                                                    <tr>
                                                        <td>
                                                            <div class="row" style="width:100%">
                                                            <dx:ASPxGridView ID="xgrdFabricantes" runat="server" AutoGenerateColumns="true" 
                                                                Width="100%" Font-Names="Segoe UI"
                                                                KeyFieldName="ProvFabricantesId" oncustomcallback="xgrdFabricantes_CustomCallback"   
                                                                OnHtmlDataCellPrepared="xgrdFabricantes_HtmlDataCellPrepared"                                                          
                                                                ClientInstanceName="xgrdFabricantes" Theme="Metropolis">                                                         
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="0" Width="15px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Fabricante" Caption="Fabricante" VisibleIndex="1" Width="100%">
                                                                    </dx:GridViewDataTextColumn>                                                                                                                  
                                                                </Columns>
                                                                <Styles>
                                                                    <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                                                                    <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                                                                    <Header BackColor="#F2F2F2"></Header>  
                                                                </Styles>                                                                                                                        
                                                                <SettingsPager Mode="ShowPager" PageSize="20"/>
                                                                <ClientSideEvents EndCallback="OnFabricanteEndCallback" />
                                                            </dx:ASPxGridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>--%>
                                            </div>
                                        </td>
                                    </tr> 
                                </td>
                            </tr>
                        </table>
                     </div>
                </EditForm>
            </Templates>                   
        </dx:ASPxGridView>
    </div>
    <script>
        function openCity(evt, opc) {
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(opc).style.display = "flex";
            evt.currentTarget.className += " active";
        }
    </script>
</asp:Content>
