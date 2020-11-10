<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CatPassword.aspx.cs" Inherits="ControlProductos.CatPassword" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/jscript">
         function CloseView() {
             document.location.href = "Wellcome.aspx";
         }
         function OnEndCallback(s, e) {             
             swal("Information!", "The Password was successfully updated!", "success");
             document.location.href = "Wellcome.aspx";
             //if (s.cpAlertMessage != '') {                 
             //    if (s.cpAlertMessage == 'Update') {
             //        swal("Information!", "The Password was successfully updated!", "success");                
             //    } else if (s.cpAlertMessage == 'Error') {
             //        swal("warning!", "An error occurred from the Database");
             //    }                 
             //}
         }

         function ChangePassword() {
             var OldPass = xtxtOldPassword.GetValue();
             var NewPass = xtxtNewPassword.GetValue();
             var Confirm = xtxtConfirm.GetValue();
             
             if (OldPass == "" || OldPass == null) {
                 swal("warning", "You need to enter the current password");
                 return;
             }
             else if( NewPass == "" || Confirm == "" || NewPass == null || Confirm == null){
                 swal("warning", "Enter the new password and confirm it");
                 return;
             }
             if( NewPass != Confirm){
                 swal("warning", "You must confirm the password correctly");
                 return;
             }

             if (OldPass != "" || OldPass != null && NewPass != ""  || NewPass != null && Confirm != "" || COnfirm != null) {
                CallbackChangePassword.PerformCallback();
             }
                
        }
    </script> 
    <div id="Busqueda" runat="server">
                    <dx:ASPxNavBar ID="ASPxNavBar2" runat="server" Theme="Metropolis" Width="100%" 
                        Font-Bold="False">
                        <Groups>
                            <dx:NavBarGroup Text="Change Password">
                                    <ContentTemplate>
                                         <div id="Parametros" runat="server" visible="true">                                            
                                            <table style="float: left; width: 40%"" class="OptionsTable BottomMargin">
                                              <tr style="height:10px"></tr>
                                              <tr>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" ID="lblCurrentPW" Text="Old Password" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox runat="server" ID="xtxtOldPassword" ClientInstanceName="xtxtOldPassword" Width="200px" MaxLength="15" Text="" Font-Names="Segoe UI" Password="true">
                                                                <ValidationSettings>
                                                                <RequiredField IsRequired="true" ErrorText="Field Required" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr style="height:10px"></tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" ID="lblNewPW" Text="New Password" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox runat="server" ID="xtxtNewPassword" ClientInstanceName="xtxtNewPassword" Width="200px" MaxLength="15" Text="" Font-Names="Segoe UI" Password="true">
                                                            <ValidationSettings>
                                                                <RequiredField IsRequired="true" ErrorText="Field Required" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr style="height:10px"></tr>
                                                <tr>
                                                    <td> 
                                                        <dx:ASPxLabel runat="server" ID="lblConfirm" Text="Confirm" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox runat="server" ID="xtxtConfirm" ClientInstanceName="xtxtConfirm" Width="200px" MaxLength="15" Text="" Font-Names="Segoe UI" Password="true">
                                                            <ValidationSettings>
                                                                <RequiredField IsRequired="true" ErrorText="Field Required" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
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
                                                                                <td>
                                                                                    <div class="div-container">
                                                                                      <div class="div-img" > 
                                                                                          <img class="img" src="Assets/Images/Save.png" alt="Save" style="cursor:pointer" onclick="return ChangePassword();" title="Save"/>
                                                                                       </div>
                                                                                    </div>
                                                                                </td>  
                                                                                <td>
                                                                                    <div class="div-container">
                                                                                       <div class="div-img" style="padding-left:15px" > 
                                                                                           <img class="img" src="Assets/Images/Close.png" alt="Close" style="cursor:pointer" onclick="return CloseView();" title="Close" />
                                                                                       </div>
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                 <dx:ASPxCallbackPanel ID="CallbackChangePassword" ClientInstanceName="CallbackChangePassword" runat="server" Width="200px" 
                                                                                     oncallback="CallbackChangePassword_Callback" ClientSideEvents-EndCallback="OnEndCallback" >
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
</asp:Content>

