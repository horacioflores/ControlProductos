<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CatPerfiles.aspx.cs" Inherits="ControlProductos.CatPerfiles" %>
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
                    swal("Información!", "Seleccione una aplicación para agregarlo a la lista!", "info");
                } else if (s.cpAlertMessage == 'Exist') {
                    swal("Información!", "La aplicación ya se encuentra agregado en la lista", "info");
                }
                //alert(s.cpAlertMessage);
            }
        }
    </script>            
    <div id="Busqueda" runat="server">
    <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" Width="100%" Font-Bold="false" Theme="Metropolis" >
        <Groups>
            <dx:NavBarGroup Text="Profiles\ Filters">
                <ContentTemplate>
                    <div id="parametros" runat="server" visible="true">
                        <table style="float: left; width: 40%; margin-left:5px" class="OptionsTable BottomMargin">
                            <tr style="height:5px"></tr>               
                            <tr>
                                <td  style="width:10%; text-align:left">
                                        <dx:ASPxLabel ID="lblCodigo" runat="server" Text="Code: ">
                                </dx:ASPxLabel>
                                </td>
                                <td style="width:30%; text-align:left">
                                    <dx:ASPxTextBox runat="server" theme="SoftOrange" ID="xtxtCodigo" Enabled="True" Width="100%"  FocusedStyle-Border-BorderColor="#3399ff" FocusedStyle-Border-BorderStyle="Double">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr style="height:5px"></tr>
                            <tr>
                                <td style="width:10%; text-align:left">
                                    <dx:ASPxCheckBox ID="chkActive" runat="server" Theme="SoftOrange" ClientInstanceName="chkActive" 
                                    Text="Active Records" Checked="true" TextAlign="Left"></dx:ASPxCheckBox>
                                </td>
                                <td style="width:20%; text-align:right">                                                         
                                    <dx:ASPxButton ID="btnBuscar" runat="server" Text="Search" Theme="SoftOrange" ClientInstanceName="btnBuscar" AutoPostBack="false">
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
                                                            <td style="width:20%" >
                                                                <div class="div-container">
                                                                    <div class="div-img" >                                                                                           
                                                                        <asp:ImageButton CssClass="img" ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" ImageUrl="Assets/Images/dowload_xls.png" AlternateText="Dowload xls" title="Dowload xls" />                                                                                       
                                                                    </div>
                                                                </div>
                                                            </td>
                                                                <td style="width:20%" >
                                                                <div class="div-container">
                                                                    <div class="div-img" >
                                                                        <img class="img" src="Assets/Images/upload_xls.png" alt="Upload xls" style="cursor:pointer" onclick="return UploadFileXLS();" title="Upload xls" />                                                                                                                                                                                     
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxCallbackPanel ID="CallbackPanelDisable" ClientInstanceName="CallbackPanelDisable" runat="server" Width="200px" oncallback="CallbackPanelDisable_Callback">
                                                                </dx:ASPxCallbackPanel>
                                                                <dx:ASPxCallbackPanel ID="CallbackPanelDisableAll" ClientInstanceName="CallbackPanelDisableAll" runat="server" Width="200px" oncallback="CallbackPanelDisableAll_Callback">
                                                                </dx:ASPxCallbackPanel>
                                                                <dx:ASPxCallbackPanel ID="CallbackPanelDowload" ClientInstanceName="CallbackPanelDowload" runat="server" Width="200px" oncallback="CallbackPanelDowload_Callback">                                                                                         
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
                            <td colspan="2" style="width:100%;text-align:left">
                            </td>
                        </tr>   
                        <tr>
                            <td colspan="1" style="width:70%; text-align:right">
                                <asp:Label ID="Label19" runat="server" 
                                    Text=" Type of File: xls, xlsx."></asp:Label>
                            </td>
                            <td colspan="1" style="width:30%">

                            </td>
                        </tr>                   
                        <tr>
                            <td style="width:70%" colspan="1">
                                <dx:ASPxUploadControl ID="uplReceiptFileExcel" runat="server" 
                                    ClientInstanceName="uploaderReceiptExcel" CssClass="labelGral" Height="58px" NullText="Click aqui para buscar archivos..." 
                                    OnFileUploadComplete="uplReceipt_FileUploadCompleteExcel" ShowProgressPanel="True" Theme="Metropolis" Width="100%">
                                    <ValidationSettings AllowedFileExtensions=".xls, .xlsx" 
                                        MaxFileSize="4194304">
                                    </ValidationSettings>
                                    <ClientSideEvents FilesUploadComplete="function(s, e) { UploaderReceipt_OnFilesUploadCompleteExcel(e); }" 
                                        FileUploadComplete="function(s, e) { UploaderReceipt_OnFileUploadCompleteExcel(e); }" 
                                        FileUploadStart="function(s, e) { UploaderReceipt_OnUploadStartExcel(); }" 
                                        TextChanged="function(s, e) { UpdateUploadReceiptButtonExcel(); }" />
                                    <AdvancedModeSettings>
                                        <FileListItemStyle CssClass="pending dxucFileListItem">
                                        </FileListItemStyle>
                                    </AdvancedModeSettings>
                                    <ButtonStyle CssClass="labelGral" Font-Size="10pt">
                                    </ButtonStyle>
                                </dx:ASPxUploadControl>
                            </td>
                            <td style="vertical-align:top; width:30px; text-align:center" colspan="1">
                                <dx:ASPxButton ID="btnUploadReceiptExcel" runat="server" AutoPostBack="False" Theme="Metropolis" 
                                    ClientEnabled="False" ClientInstanceName="btnUploadReceiptExcel" Height="18px" 
                                    Text="Upload File" Width="20%" >
                                    <ClientSideEvents Click="function(s, e) { uploaderReceiptExcel.Upload(); }" />
                                </dx:ASPxButton>                   
                            </td>                            
                        </tr>
                       
                    </table>
            </div>            
        </dx:PanelContent>
    </PanelCollection>

</dx:ASPxPanel>
<div id="Detalle" runat="server">        
      <dx:ASPxGridView ID="xgrdPerfiles" runat="server" ClientInstanceName="grid" AutoGenerateColumns="False" 
        Width="100%" Font-Names="Segoe UI" Theme="Metropolis" KeyFieldName="PerfilId" oncustomcallback="xgrdPerfiles_CustomCallback"                                         
        onrowdeleting="xgrdPerfiles_RowDeleting" onrowinserting="xgrdPerfiles_RowInserting" onrowupdating="xgrdPerfiles_RowUpdating"
        onrowvalidating="xgrdPerfiles_RowValidating" oninitnewrow="xgrdPerfiles_InitNewRow" onhtmleditformcreated = "xgrdPerfiles_HtmlEditFormCreated"
        onstartrowediting="xgrdPerfiles_StartRowEditing" OnHtmlDataCellPrepared="xgrdPerfiles_HtmlDataCellPrepared">   
        <SettingsBehavior EnableRowHotTrack="true" />             
        <ClientSideEvents EndCallback="OnEndCallback"  RowDblClick="function(s, e){                                                                                                        
                    s.StartEditRow(e.visibleIndex);  
        }"/>       
        <Columns>                                            
            <dx:GridViewDataTextColumn FieldName="PerfilId" Visible="false" Caption="Identifier">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="0" Width="5%">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Codigo" VisibleIndex="1" Caption="Code" Width="30%">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Descripcion" VisibleIndex="2" Caption="Description" Width="65%">
            </dx:GridViewDataTextColumn>       
            <dx:GridViewDataCheckColumn FieldName="EsAdministrador" VisibleIndex="3" Caption="Is Admin" Width="10%">
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="RealizaEncuestas" VisibleIndex="3" Caption="Approve quotes" Width="10%">
            </dx:GridViewDataCheckColumn>
        </Columns>
        <SettingsPager Mode="ShowPager" PageSize="13"/>
        <SettingsEditing Mode="PopupEditForm">                                            
        </SettingsEditing>
        <Settings ShowFilterRow="True" />
        <SettingsBehavior ConfirmDelete="true"/>
        <SettingsPopup >
                <EditForm Width="600"  Modal="true" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" ShowHeader="false"/>
        </SettingsPopup>
        <SettingsText ConfirmDelete="Are you sure you want to enable/disable this perfil"/> 
        <Styles>
            <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
            <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
            <Header BackColor="#F2F2F2"></Header>  
        </Styles>
        <Templates>
            <EditForm>
                <div style="margin:1px 1px 1px 1px; background-color:#F2F2F2" >
                        <table style="width:100%; height:200px;">
                        <tr style="background-repeat:repeat-x; background-image:url('Assets/Images/GreenBar.png'); vertical-align:middle">
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
                                            <dx:ASPxLabel runat="server" ID="ASPxLabel3" Text="Profiles Register" Font-Size="13pt" Font-Names="Segoe UI"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="background-color:#E0E0E0; height:20px" >
                                            <dx:ASPxLabel runat="server" ID="ASPxLabel2" Text="Descrition" Font-Size="12pt" Font-Names="Segoe UI"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <dx:ASPxLabel runat="server" ID="lblFolio" Text="Folio Profile" Font-Names="Segoe UI"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox runat="server" ID="xtxtPerfilEdit" theme="SoftOrange" Width="200px" 
                                                MaxLength="10" Text='<%# Eval("PerfilId")%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:10px">
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <dx:ASPxLabel runat="server" ID="lblKey" Text="Code" Font-Names="Segoe UI"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox runat="server" ID="xtxtKeyEdit" theme="SoftOrange" Width="200px" MaxLength="10" Text='<%# Eval("Codigo")%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                    <RequiredField IsRequired="true" ErrorText="Field Required" />
                                                </ValidationSettings>                                                
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:10px">
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <dx:ASPxLabel runat="server" ID="lblDescription" Text="Description" Font-Names="Segoe UI"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox runat="server" ID="xtxtDescriptionEdit" theme="SoftOrange" Width="200px" MaxLength="40" Text='<%# Eval("Descripcion")%>' Font-Names="Segoe UI" FocusedStyle-Border-BorderColor="#3399ff">
                                                <ValidationSettings SetFocusOnError="true" Display="Static" CausesValidation="true">
                                                    <RegularExpression ValidationExpression="[A-Za-záéíóúñÑ,;:\.\/\_\-\s\d\(\)]+" ErrorText="Please enter just valid characters" />
                                                    <RequiredField IsRequired="true" ErrorText="Field Required" />
                                                </ValidationSettings>                                                
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>         
                                    <tr style="height:10px">
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <dx:ASPxLabel runat="server" ID="lblEsAdministrador" Text="Is Admin" Font-Names="Segoe UI"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxCheckBox ID="chkEsAdministradorEdit" runat="server" Theme="SoftOrange" ClientInstanceName="chkEsAdministrador" Text="Si" Checked='<%# getValueBool(Eval("EsAdministrador")) %>'  TextAlign="Left"></dx:ASPxCheckBox>
                                        </td>
                                    </tr>         
                                    <tr style="height:10px">
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <dx:ASPxLabel runat="server" ID="lblRealizaEncuestas" Text="Approve quotes" Font-Names="Segoe UI"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxCheckBox ID="chkRealizaEncuestasEdit" runat="server" Theme="SoftOrange" ClientInstanceName="chkRealizaEncuestas" Text="Si" Checked='<%# getValueBool(Eval("RealizaEncuestas")) %>'  TextAlign="Left"></dx:ASPxCheckBox>
                                        </td>
                                    </tr>         
                                    <tr style="height:10px">
                                    </tr>
                                    <tr>
                                          <td style="padding-left: 10px">
                                            <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="Application" Font-Names="Segoe UI"></dx:ASPxLabel>
                                        </td>
                                        <td>           
                                              <table>
                                               <tr>
                                                   <td style="vertical-align:central">
                                                        <dx:ASPxComboBox  ID="cmbApplicationEdit" runat="server" CssClass="labelGral" Width="200px" FocusedStyle-Border-BorderColor="#3399ff"
                                                        Font-Names="Segoe UI" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" FilterMinLength="0" 
                                                        EnableCallbackMode="True" CallbackPageSize="10"  Theme="SoftOrange" AnimationType="Fade" 
                                                        OnDataBound="cmbApplicationEdit_DataBound">                                             
                                                            <ClientSideEvents />
                                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                                        </dx:ASPxComboBox>
                                                   </td>
                                                   <td style="padding-left: 20px">
                                                       <dx:ASPxLabel runat="server" ID="ASPxLabel4" Text=" + Options" Font-Names="Segoe UI"></dx:ASPxLabel>
                                                   </td>
                                                    <td style="padding-left: 10px;vertical-align:central">
                                                        <div class="div-container">
                                                            <div class="div-img" >
                                                            <img class="img" src="Assets/Images/New.png" alt="Add" title="Add" style="cursor:pointer" onclick="return xgrdApps.PerformCallback('Save');"/>                                                
                                                            </div>
                                                        </div>                                           
                                                    </td> 
                                                    <td style="vertical-align:central">
                                                         <div class="div-container">
                                                            <div class="div-img" >
                                                            <img class="img" src="Assets/Images/trash_can.png" alt="Remove All" style="cursor:pointer" onclick="return xgrdApps.PerformCallback('Delete');" title="Remove All" />
                                                            </div>
                                                        </div>
                                                    </td>
                                               </tr>
                                           </table>
                                        </td>     
                                    </tr>    
                                    <tr style="height:10px">                                   
                                    <tr style="vertical-align:top" id="trAccess">                                                    
                                        <td style="padding-left: 10px; padding-right:10px" colspan="2">
                                            <dx:ASPxGridView ID="xgrdApps" runat="server" AutoGenerateColumns="true" 
                                                Width="100%" Font-Names="Segoe UI"
                                                KeyFieldName="AppId" oncustomcallback="xgrdApps_CustomCallback"                                                            
                                                ClientInstanceName="xgrdApps" Theme="Metropolis">                                                         
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="AppId" Caption="Folio" VisibleIndex="0" Width="30%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Nombre" Caption="Application" VisibleIndex="1" Width="70%">
                                                    </dx:GridViewDataTextColumn>                                                                                                                  
                                                </Columns>
                                                <Styles>
                                                    <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                                                    <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                                                    <Header BackColor="#F2F2F2"></Header>  
                                                </Styles>                                                                                                                        
                                                <SettingsPager Mode="ShowPager" PageSize="20"/>
                                                <ClientSideEvents EndCallback="OnAppsEndCallback" />
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>           
                                    <tr style="height:10px">                                                                                                               
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
