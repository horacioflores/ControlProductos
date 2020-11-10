<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ControlProductos.login" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>
<html>
    <head runat="server">
        <meta charset="utf-8" />
        <title>Control de Productos</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
        <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
        <meta content="Coderthemes" name="author" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <link rel="shortcut icon" href="Assets/images/favicon_1.ico">
        <link href="Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css">
        <%--<link href="Assets/css/core.css" rel="stylesheet" type="text/css">--%>
        <link href="Assets/css/icons.css" rel="stylesheet" type="text/css">
        <link href="Assets/css/components.css" rel="stylesheet" type="text/css">
        <link href="Assets/css/pages.css" rel="stylesheet" type="text/css">
        <link href="Assets/css/menu.css" rel="stylesheet" type="text/css">
        <link href="Assets/css/responsive.css" rel="stylesheet" type="text/css">
        <script src="Assets/js/modernizr.min.js"></script>   
        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->       
        <link href="Assets/plugins/sweetalert/dist/sweetalert.css" rel="stylesheet" type="text/css">
         <!-- jQuery  -->
        <script src="Assets/plugins/sweetalert/dist/sweetalert.min.js"></script>
        <script src="Assets/pages/jquery.sweet-alert.init.js"></script>
        <script src="Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
        <script type="text/jscript">
            function raiseValidation() {
                if (ASPxClientEdit.ValidateEditorsInContainer(null)) {
                }                    
            }
      

            function cbRecoveryCompleteCB(s, e) {
                if (e.result == "1") {
                    document.getElementById("pcRecovery_xtxtUserName_I").value = "";
                    pcRecovery.Hide();
                }

                LoadingPanel.Hide();

                if (s.cpAlertMessage != '') {
                    if (s.cpAlertMessage == 'Success') {                        
                        swal("Information!", "Current password was sent to the user's email", "success");
                    } else if (s.cpAlertMessage == 'NoUser') {
                        swal("Information!", "User was not found","warning");
                    } else if (s.cpAlertMessage == 'Error') {
                        swal("warning!", "An error occurred from the Database","error" );
                    }
                    //alert(s.cpAlertMessage);                    
                }
            }

            function valUsername(s, e) {
                var ev = window.event;
                var keyPressed = String.fromCharCode(ev.keyCode);
                var number = /^[A-Za-z\.\_\-\d]+$/;

                var valid = number.test(keyPressed)

                if (!valid) {
                    ev.cancelBubble = true;
                    ev.returnValue = false;
                    return false;
                }
            }

            function OnSuccess(response) {
                alert(response.d);                
            }

        </script>
    </head>
    <body style="background-image:url('<%= ResolveClientUrl(ConfigurationManager.AppSettings["LoginCompleto"])%>')">
        <div>
        <div class="wrapper-page">
            <div class="panel panel-color panel-pages">
                <%--<div class="panel-heading bg-img">
                    <div class="bg-overlay"></div>
                    <h3 class="text-center m-t-10 text-white"> </h3>                                        
                </div>--%>
                <div class="panel-body">
                <form class="form-horizontal m-t-20" runat="server">
                   
                    <div class="form-group text-left"> 
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <dx:ASPxLabel Font-Bold="true" Font-Size="Medium" runat="server" ID="ASPxLabel6" Text="User:" width="100px" Font-Names="Segoe UI"></dx:ASPxLabel>
                                </td>
                                <td>
                                      <dx:ASPxTextBox runat="server" ID="xtxtUsuario" Width="100%" MaxLength="30" Font-Names="Segoe UI" Height="22px" HelpText="User" Theme="Moderno">
                                        <HelpTextSettings DisplayMode="Popup" HorizontalAlign="Center">
                                        </HelpTextSettings>
                                        <ValidationSettings SetFocusOnError="true" Display="Dynamic" CausesValidation="true">
                                            <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                            <RequiredField IsRequired="true" ErrorText="Field Required" />
                                        </ValidationSettings>                                
                                     </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height:5px;"></td>
                                <td style="height:5px;"></td>
                            </tr>
                            <tr>
                                <td>
                                     <dx:ASPxLabel Font-Bold="true" Font-Size="Medium" runat="server" ID="ASPxLabel5" Text="Password:" width="100px" Font-Names="Segoe UI"></dx:ASPxLabel>
                                </td> 
                                <td>
                                    <dx:ASPxTextBox runat="server" ID="xtxtPassword" Width="100%" MaxLength="30" Font-Names="Segoe UI" Height="22px" HelpText="Password" Password="True" Theme="Moderno" >
                                         <HelpTextSettings DisplayMode="Popup" HorizontalAlign="Center">
                                         </HelpTextSettings>
                                        <ValidationSettings SetFocusOnError="true" Display="Dynamic" CausesValidation="true">                                    
                                            <RequiredField IsRequired="true" ErrorText="Field Required" />
                                        </ValidationSettings>                                
                                    </dx:ASPxTextBox>         
                                </td>        
                            </tr>

                        </table>             
                    </div>
                    <div class="form-group text-center m-t-40">                 
                           
                         <asp:ImageButton ID="LoginImageButton" runat="server" AlternateText="Log In" 
                             CommandName="Login" CssClass="loginbutton" ImageUrl="Assets/Images/Entrar.png" OnClick="LoginImageButton_Click" />                                                    
                    </div>
                    <div>      
                            <dx:ASPxCallbackPanel ID="CallbackPanelDisable" ClientInstanceName="CallbackPanelDisable" runat="server" Width="200px" oncallback="CallbackPanelDisable_Callback"></dx:ASPxCallbackPanel> 
                    </div>
                    <div class="form-group m-t-30">
                        <div class="col-sm-7 col-sm-offset-3">                            
                             <asp:LinkButton ID="hlkRecoveryPW" runat="server" Text="Recover password" 
									         OnClientClick="pcRecovery.Show(); return false;"></asp:LinkButton>
                        </div>
                    </div>
                    <div>
                         <dx:ASPxPopupControl ID="pcRecovery" runat="server" CloseAction="CloseButton" Modal="True" ShowHeader="False"
                            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcRecovery"
                            HeaderText="Recovery Password" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="350px">
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                    <div style="margin:1px 1px 1px 1px; background-color:#E0E0E0" >
                                        <table style="width:100%; height:150px;">
                                            <tr style="background-repeat:repeat-x; background-image:url('Assets/Images/GreenBar.png'); vertical-align:middle">
                                                <td  style="height:22px; text-align:left">
                                                    <img src="Assets/Images/Send2.png" alt="" style="cursor:pointer" onclick="cbRecovery.PerformCallback(); LoadingPanel.Show();" />
                                                </td>
                                                <td  style="height:22px; text-align:right">
                                                    <img src="Assets/Images/Close.png" alt="" style="cursor:pointer" onclick="return pcRecovery.Hide();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table style="width:100%; padding-left:10px; padding-right:10px; padding-bottom:20px">
                                                        <tr style="height:40px">
                                                            <td style="text-align:left" colspan="2" >
                                                                <dx:ASPxLabel runat="server" ID="lblRecoveryTitle" Text="Recover password" Font-Size="13pt" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                            </td>
                                                        </tr>                                                       
                                                        <tr><td style="height:20px"></td></tr>
                                                        <tr style="align-content:center">  
                                                            <td style="text-align:left">
                                                                <dx:ASPxLabel runat="server" ID="ASPxLabel4" Text="User:" width="100px" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                            </td>                                                         
                                                            <td style="text-align:right"">
                                                                <dx:ASPxTextBox runat="server" ID="xtxtUserName" Width="150px" MaxLength="100" Text='<%# Eval("UserName")%>' Font-Names="Segoe UI">
                                                                    <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                                        <RequiredField IsRequired="true" ErrorText="Field Required" />
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents KeyPress="valUsername" />
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr><td style="height:10px"></td></tr>
                                                    </table> 
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                            <ContentStyle>
                                <Paddings Padding="0" />
                            </ContentStyle>
                        </dx:ASPxPopupControl>
                        <dx:ASPxCallback runat="server" ID="cbRecovery" ClientInstanceName="cbRecovery" oncallback="cbRecovery_Callback">
                          <ClientSideEvents CallbackComplete="cbRecoveryCompleteCB" />
                        </dx:ASPxCallback>
                        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True"></dx:ASPxLoadingPanel>
                    </div>
                </form>
                </div>
            </div>
        </div>
        </div>
    	<script>
            var resizefunc = [];
        </script>
        <!-- Main  -->
        <script src="Assets/js/jquery.min.js"></script>
        <script src="Assets/js/bootstrap.min.js"></script>
        <script src="Assets/js/detect.js"></script>
        <script src="Assets/js/fastclick.js"></script>
        <script src="Assets/js/jquery.slimscroll.js"></script>
        <script src="Assets/js/jquery.blockUI.js"></script>
        <script src="Assets/js/waves.js"></script>
        <script src="Assets/js/wow.min.js"></script>
        <script src="Assets/js/jquery.nicescroll.js"></script>
        <script src="Assets/js/jquery.scrollTo.min.js"></script>
        <script src="Assets/js/jquery.app.js"></script>
    </body>
</html>