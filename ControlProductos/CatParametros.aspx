<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CatParametros.aspx.cs" Inherits="ControlProductos.CatParametros" %>
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
         function raiseValidation() {
             var container = grid.GetMainElement();
             if (ASPxClientEdit.ValidateEditorsInContainer(container, null, true)) {
                 grid.UpdateEdit();
             }
         }

         function OnCallback() {
             grid.PerformCallback('Search');
         }
     </script>
    <div id="Busqueda" runat="server">
        <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" Theme="Metropolis" Width="100%" Font-Bold="False" CssClass="nav-smartit">
            <Groups>
                <dx:NavBarGroup Text="Parameters\ Filters">
                        <ContentTemplate>
                                <div id="Parametros" runat="server" visible="true">
                                    <div class="row" style="padding-top:5px">
                                        <div class="col-md-3 col-sm-3 col-xs-12">
                                            <dx:ASPxLabel ID="lblDescripcionBusqueda" runat="server" Text="Description: " CssClass="text-nav"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-4 col-sm-4 col-xs-12">
                                            <dx:ASPxTextBox runat="server" ID="xtxtDescripcion" theme="SoftOrange" Enabled="True" Width="100%" FocusedStyle-Border-BorderColor="#3399ff" FocusedStyle-Border-BorderStyle="Double" ></dx:ASPxTextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:5px">
                                        <div class="col-md-3 col-sm-3 col-xs-12">
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Active Records: " CssClass="text-nav"></dx:ASPxLabel>
                                            <dx:ASPxCheckBox ID="chkActivo" runat="server" Theme="SoftOrange" Checked="true" TextAlign="Left"></dx:ASPxCheckBox>
                                        </div>
                                        <div class="col-md-4 col-sm-4 col-xs-12">
                                            <dx:ASPxButton ID="btnBuscar" runat="server" Text="Search" Theme="SoftOrange" AutoPostBack="false">
                                                <ClientSideEvents Click="OnCallback" />
                                            </dx:ASPxButton>
                                        </div>
                                    </div>
                            </div>
                        </ContentTemplate>
                </dx:NavBarGroup>
            </Groups>
        </dx:ASPxNavBar>
    </div>
    <div id="Detalle" runat="server">
        <dx:ASPxGridView ID="xgrdParametro" runat="server" ClientInstanceName="grid" AutoGenerateColumns="False" 
                    Width="100%" Font-Names="Segoe UI" KeyFieldName="ID" Theme="Metropolis" oncustomcallback="xgrdParametro_CustomCallback"
                    onrowupdating="xgrdParametro_RowUpdating" onstartrowediting="xgrdParametro_StartRowEditing">
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ShowClearFilterButton="True"/>
                        <dx:GridViewDataTextColumn FieldName="ID" Visible="false" Caption="Identifier"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Descripcion" VisibleIndex="1" Caption="Description" Width="20%">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Valor" VisibleIndex="2" Caption="Value" Width="50%">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Padre" VisibleIndex="3" Caption="Padre" Width="30%">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Encriptado" VisibleIndex="4" Caption="Encrypted" Width="5%"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Editable" VisibleIndex="5" Caption="Editable" Width="5%"></dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="true" EnableRowHotTrack="true"/>
                    <SettingsPager Mode="ShowPager" PageSize="50"/>                    
                    <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                    <SettingsPopup >
                            <EditForm Width="600"  Modal="true" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" ShowHeader="false"/>
                    </SettingsPopup>
                    <Settings ShowFilterRow="True" />
                    <SettingsText ConfirmDelete="Are you sure you want to enable/disable this record"/> 
                    <Styles>
                        <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                        <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                        <Header BackColor="#F2F2F2"></Header>                       
                        <Cell Wrap="False"></Cell>
                    </Styles>
                    <ClientSideEvents EndCallback="OnEndCallback"  RowDblClick="function(s, e){                                                                                                        
                                s.StartEditRow(e.visibleIndex);  
                    }"/>
                    <Templates>
                        <EditForm>
                            <div style="margin:1px 1px 1px 1px; background-color:#F2F2F2" >
                                   <table style="width:100%; height:200px;">
                                   <tr style="background-color:grey; vertical-align:middle">
                                        <td  style="height:22px; text-align:left">
                                            <img src="<%= ResolveClientUrl("~/Assets/Images/save.png")%>" alt="" style="cursor:pointer" onclick="return raiseValidation();" />
                                        </td>
                                        <td  style="height:22px; text-align:right">
                                            <img src="<%= ResolveClientUrl("~/Assets/Images/Close.png")%>" alt="" style="cursor:pointer" onclick="return grid.CancelEdit();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align:top" colspan="2">
                                            <table style="width:100%; padding-left:10px; padding-right:10px; padding-bottom:20px">
                                                <tr style="height:40px">
                                                    <td colspan="2" style="height:40px; padding-right:10px; text-align:right">
                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel3" Text="Parameters" Font-Size="13pt" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="background-color:#E0E0E0; height:20px" >
                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel2" Text="Description" Font-Size="12pt" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr style="height:10px"></tr>
                                                <tr>
                                                    <td style="padding-left: 10px">
                                                        <dx:ASPxLabel runat="server" ID="lblParent" Text="Padre" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox runat="server" ID="xtxtParent" Width="300px" theme="SoftOrange" ReadOnly="true" Text='<%# Eval("Padre")%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr style="height:10px"></tr>
                                                <tr>
                                                    <td style="padding-left: 10px">
                                                        <dx:ASPxLabel runat="server" ID="lblDescripcion" Text="Name" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox runat="server" ID="xtxtDescripcionEdit" theme="SoftOrange" Width="300px" MaxLength="255" ReadOnly="true" Text='<%# Eval("Descripcion")%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr style="height:10px"></tr>
                                                <tr>
                                                    <td >
                                                        <dx:ASPxLabel runat="server" ID="lblEncrypted" Text="Encrypted" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chkEncrypted" runat="server" CssClass="labelGral" Width="100%" Font-Bold="true" ForeColor="White" Text=""  
                                                            Checked='<%# ValidaBoleano(Eval("Encriptado")) %>' Enabled="False">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr style="height:10px"></tr>
                                                <tr>
                                                    <td style="padding-left: 10px">
                                                        <dx:ASPxLabel runat="server" ID="lblValue" Text="Value" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox runat="server" ID="xtxtValue" Width="300px" theme="SoftOrange" MaxLength="1000" Text='<%# ValidaParametroEncriptado(Eval("Encriptado"), Eval("valor"))%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                                            <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                <RequiredField IsRequired="true" ErrorText="Campo Requerido" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                        </EditForm>
                    </Templates>
                </dx:ASPxGridView>
    </div>
</asp:Content>
