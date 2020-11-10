<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CatPhoto.aspx.cs" Inherits="ControlProductos.CatPhoto" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/jscript">       
    function CloseView(){
        document.location.href = "Wellcome.aspx";
    }
    function CIuplGraphicsFile_OnUploadStart() {
        btnUploadGraphicsFile.SetEnabled(false);
    }

      function CIuplGraphicsFile_OnFileUploadComplete(s, e) {
            if (e.isValid) {
                swal("Information", "The record was successfully updated", "success");
                cbSaveVars.PerformCallback("~/Assets/images/users/" + e.callbackData);

                if (document.getElementById("MainContent_ASPxNavBar2_GCTC0_imgPreview_0")) {
                    document.getElementById("MainContent_ASPxNavBar2_GCTC0_imgPreview_0").src = "Assets/images/users/" + e.callbackData;
                }
            }
            else {
                swal("Warning", "An error has occurred from the database");
            }
        }

        function CIuplGraphicsFile_OnFilesUploadComplete(args) {
            UpdateUploadGraphicsFileButton();
        }

        function UpdateUploadGraphicsFileButton() {
            btnUploadGraphicsFile.SetEnabled(CIuplGraphicsFile.GetText(0) != "");
        }     
</script> 
<div id="Busqueda" runat="server">
                    <dx:ASPxNavBar ID="ASPxNavBar2" runat="server" Theme="Metropolis" Width="100%" 
                        Font-Bold="False">
                        <Groups>
                            <dx:NavBarGroup Text="Change Photo">
                                    <ContentTemplate>
                                         <div id="Parametros" runat="server" visible="true">                                            
                                            <table style="float: left; width: 40%"" class="OptionsTable BottomMargin">                                                
                                                    <tr style="height:10px"><td></td></tr>
                                                    <tr>
                                                        <td style="width:20%">
                                                            <dx:ASPxLabel runat="server" ID="lblvcGraphicsFile" Text="Graphics File" 
                                                                Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td style="width:60%">
                                                            <table style="width:100%">
                                                                <tr>
                                                                    <td style="width:75%">
                                                                        <dx:ASPxUploadControl ID="uplGraphicsFile" runat="server" 
                                                                            ClientInstanceName="CIuplGraphicsFile" ShowProgressPanel="True"
                                                                            NullText="Click here to browse files..." Size="35" 
                                                                            OnFileUploadComplete="CIuplGraphicsFile_FileUploadComplete" CssClass="labelGral" 
                                                                            Width="100%">
                                                                            <ClientSideEvents 
                                                                                FileUploadComplete="function(s, e) { CIuplGraphicsFile_OnFileUploadComplete(s,e); }"
                                                                                FilesUploadComplete="function(s, e) { CIuplGraphicsFile_OnFilesUploadComplete(e); }"
                                                                                FileUploadStart="function(s, e) { CIuplGraphicsFile_OnUploadStart(); }"
                                                                                TextChanged="function(s, e) { UpdateUploadGraphicsFileButton(); }"></ClientSideEvents>
                                                                            <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.jpe,.gif,.bmp,.png, .pdf">
                                                                            </ValidationSettings>
                                                                            <ButtonStyle CssClass="labelGral" Font-Size="10pt"></ButtonStyle>
                                                                        </dx:ASPxUploadControl>
                                                                    </td>
                                                                    <td style="text-align:center; width:25%">
                                                                        <dx:ASPxButton ID="btnUploadGraphicsFile" runat="server" AutoPostBack="False" 
                                                                            Text="Upload" ClientInstanceName="btnUploadGraphicsFile"
                                                                            Width="100px" ClientEnabled="False">
                                                                            <ClientSideEvents Click="function(s, e) { CIuplGraphicsFile.Upload(); }" />                                                                                    
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="note">
                                                                        <dx:ASPxLabel ID="lblAllowebMimeTypeGraphicsFile" runat="server" 
                                                                            Text="Allowed image types: JPG, GIF, BMP, PNG" CssClass="labelGral"
                                                                            Font-Size="8pt">
                                                                        </dx:ASPxLabel>
                                                                        <br />
                                                                        <dx:ASPxLabel ID="lblMaxFileSizeGraphicsFile" runat="server" 
                                                                            Text="Maximum file size: 4Mb" Font-Size="8pt" CssClass="labelGral">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:10px"><td></td></tr>
                                                    <tr>
                                                        <td style="width:20%">
                                                            <dx:ASPxLabel runat="server" ID="ASPxLabel7" Text="Preliminar" 
                                                                Font-Names="Segoe UI"></dx:ASPxLabel>
                                                        </td>
                                                        <td style="width:80%">
                                                            <dx:ASPxImage ID="imgPreview" runat="server" Width="150px" Height="150px" ImageUrl="" ClientInstanceName="imgPreview">
                                                            </dx:ASPxImage>
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
                                                                                         <img class="img" src="Assets/Images/Close.png" alt="Close" title="Close" style="cursor:pointer" onclick="return CloseView();" />
                                                                                      </div>
                                                                                    </div>
                                                                                </td>  
                                                                                <td>                                                                                   
                                                                                    <dx:ASPxCallback runat="server" ID="cbSaveVars" ClientInstanceName="cbSaveVars" 
                                                                                        oncallback="cbRecovery_Callback">
                                                                                    </dx:ASPxCallback>                                                                               
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

