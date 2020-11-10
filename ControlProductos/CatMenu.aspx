<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CatMenu.aspx.cs" Inherits="ControlProductos.CatMenu" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script  type="text/jscript">

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
                text: "Are you sure you want to enable/disable all records selected?",
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
                      swal("Deleted", "All the Records were successfully Enabled / Disabled", "success");
                  } else {
                      swal("Cancelled", "Your Cancel", "error");
                  }
              });
        }

        function DisableAll() {

            swal({
                title: "Are you sure?",
                text: "Are you sure you want to enable/disable all the records?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes!",
                cancelButtonText: "No!",
                closeOnConfirm: false,
                closeOnCancel: false
            },
            function (isConfirm) {
                if (isConfirm) {
                    CallbackPanelDisableAll.PerformCallback();
                    grid.PerformCallback('Search');
                    swal("Deleted", "All the Records were successfully Enabled / Disabled", "success");
                } else {
                    swal("Cancelled", "Your Cancel!", "error");
                }
            });
        }
</script>   
<div id="Busqueda" runat="server">
    <dx:ASPxNavBar ID="ASPxNavBar2" runat="server" Theme="Metropolis" Width="100%" Font-Bold="False">
                        <Groups>
                            <dx:NavBarGroup Text="Access Menu\ Filters">
                                    <ContentTemplate>
                                         <div id="Parametros" runat="server" visible="true">
                                            
                                            <table style="float: left; width: 40%"" class="OptionsTable BottomMargin">
                                               <tr>
                                                     <td  style="width:10%; text-align:left">
                                                             <dx:ASPxLabel ID="lblPerfil" runat="server" Text="Profile: ">
                                                        </dx:ASPxLabel>
                                                     </td>
                                                     <td  style="width:30%; text-align:left">
                                                           <dx:ASPxComboBox  ID="cmbPerfile" runat="server" CssClass="labelGral" Width="100%"  FocusedStyle-Border-BorderColor="#3399ff"
                                                                Font-Names="Segoe UI" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                                                FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="10" AnimationType="Fade"
                                                                ClientInstanceName="cmbPerfile"
                                                                Theme="SoftOrange">                  
                                                            </dx:ASPxComboBox>
                                                     </td>
                                                </tr>
                                                <tr style="height:5px"></tr>
                                                <tr>
                                                     <td  style="width:10%; text-align:left">
                                                             <dx:ASPxLabel ID="lblApps" runat="server" Text="Application: ">
                                                        </dx:ASPxLabel>
                                                     </td>
                                                     <td  style="width:30%; text-align:left">
                                                           <dx:ASPxComboBox  ID="cmbApplication" runat="server" CssClass="labelGral" Width="100%"  FocusedStyle-Border-BorderColor="#3399ff"
                                                                Font-Names="Segoe UI" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                                                FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="10" AnimationType="Fade"
                                                                ClientInstanceName="cmbApplication"
                                                                Theme="SoftOrange">                  
                                                            </dx:ASPxComboBox>
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
    <dx:ASPxGridView ID="xgrdSA" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Segoe UI"
                    KeyFieldName="PerfilId;MenuId" oncustomcallback="xgrdSA_CustomCallback" onrowdeleting="xgrdSA_RowDeleting"
                    onrowinserting="xgrdSA_RowInserting" onrowupdating="xgrdSA_RowUpdating" onhtmleditformcreated="xgrdSA_HtmlEditFormCreated"
                    OnHtmlDataCellPrepared="xgrdSA_HtmlDataCellPrepared" ClientInstanceName="grid" Theme="Metropolis" onstartrowediting="xgrdSA_StartRowEditing">
                    <Columns>
                        <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="0" Width="10px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PerfilId"  Caption="PerfilId" Width="10%" VisibleIndex="1" Visible="false">
                        </dx:GridViewDataTextColumn>                       
                        <dx:GridViewDataTextColumn FieldName="Codigo" Caption="Code" VisibleIndex="2"  Width="10%" Visible="true">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MenuPadre"  Caption="Menu Padre" VisibleIndex="3" Width="10%" Visible="true">
                        </dx:GridViewDataTextColumn>        
                        <dx:GridViewDataTextColumn FieldName="MenuId"  Caption="Code Menu" VisibleIndex="4" Width="10%" Visible="true">
                        </dx:GridViewDataTextColumn>                        
                        <dx:GridViewDataTextColumn FieldName="Nombre" Caption="Name"  VisibleIndex="5" Width="35%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="URL" Caption="URL" VisibleIndex="6"  Width="35%">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior ConfirmDelete="true"/>
                    <SettingsPager Mode="ShowPager" PageSize="13"/>                    
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
                    <ClientSideEvents EndCallback="OnEndCallback"  RowDblClick="function(s, e){                                                                                                        
                                s.StartEditRow(e.visibleIndex);  
                    }"/>    
                    <Templates>
                        <EditForm>
                            <div style="margin:1px 1px 1px 1px; background-color:#F2F2F2" >
                                    <table style="width:100%; height:200px;">
                                   <tr style="background-repeat:repeat-x; background-image:url('Images/GreenBar.png'); vertical-align:middle">
                                        <td  style="height:22px; text-align:left">
                                            <img src="Assets/Images/save.png" alt="" style="cursor:pointer" onclick="return raiseValidation();" />
                                        </td>
                                        <td  style="height:22px; text-align:right">
                                            <img src="Assets/Images/Close.png" alt="" style="cursor:pointer" onclick="return grid.CancelEdit();" />
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td style="vertical-align:top" colspan="2">
                                            <table style="width:100%; padding-left:10px; padding-right:10px; padding-bottom:20px">
                                                <tr style="height:40px">
                                                    <td style="height:40px; padding-right:10px; text-align:right" colspan="2">
                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel3" Text="Access Menu" Font-Size="13pt" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                </tr>                                               
                                                <tr>
                                                    <td colspan="2" style="height:20px; padding-left: 10px;">
                                                        <dx:ASPxLabel runat="server" ID="lblPerfilId" Text='<%# Eval("PerfilId")%>' Font-Size="12pt" Font-Names="Segoe UI" Visible="false"></dx:ASPxLabel> 
                                                         <dx:ASPxLabel runat="server" ID="lblMenuId" Text='<%# Eval("MenuId")%>' Font-Size="12pt" Font-Names="Segoe UI" Visible="false"></dx:ASPxLabel>                                                       
                                                    </td>
                                                </tr>                                                  
                                                <tr>
                                                    <td colspan="2" style="background-color:#E0E0E0; height:20px; padding-left: 10px;" >
                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="Profile:   " Font-Size="12pt" Font-Names="Segoe UI" Font-Bold="true"></dx:ASPxLabel>                                                                                                    
                                                        <dx:ASPxLabel runat="server" ID="lblCodigo" Text='<%# Eval("Codigo")%>' Font-Size="12pt" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                </tr>        
                                                 <tr>
                                                    <td colspan="2" style="background-color:#E0E0E0; height:20px; padding-left: 10px;" >                                                              
                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel2" Text="Menu:     " Font-Size="12pt" Font-Names="Segoe UI" Font-Bold="true"></dx:ASPxLabel>
                                                         <dx:ASPxLabel runat="server" ID="lblNombre" Text='<%# Eval("Nombre")%>' Font-Size="12pt" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                </tr>            
                                               <tr style="height:10px"></tr>                               
                                                 <tr>
                                                    <td colspan="2" style="height:20px; padding-left: 10px;">
                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel4" Text="Options" Font-Size="12pt" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                               <tr style="height:10px"></tr>
                                                <tr>                                                   
                                                    <td colspan="2" style="height:20px; padding-left: 10px; width:100%">
                                                      <asp:CheckBoxList runat="server" ID="chkListArea" RepeatColumns="4" RepeatLayout="Table" 
                                                            Font-Names="Segoe UI" Width="100%" Height="30px" OnDataBound="chkListArea_DataBound" BorderStyle="None" BorderColor="#cccccc">
                                                        </asp:CheckBoxList>
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
