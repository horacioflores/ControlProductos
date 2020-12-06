<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ctrolProds_det.aspx.cs" Inherits="ControlProductos.ctrolProds_det" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server" Visible="false">
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!--Formateo de tablas-->
    <link href="https://unpkg.com/bootstrap-table@1.18.0/dist/bootstrap-table.min.css" rel="stylesheet">
    <script src="https://unpkg.com/bootstrap-table@1.18.0/dist/bootstrap-table.min.js"></script>
    <!--Tabla resize, borrar si no se necesita-->
    <script src="https://unpkg.com/jquery-resizable-columns@0.2.3/dist/jquery.resizableColumns.min.js"></script>
    <link href="https://unpkg.com/jquery-resizable-columns@0.2.3/dist/jquery.resizableColumns.css" rel="stylesheet">
    <script src="https://unpkg.com/bootstrap-table@1.18.0/dist/extensions/resizable/bootstrap-table-resizable.min.js"></script>
        <!-- jQuery  -->
        <script src="Assets/plugins/sweetalert/dist/sweetalert.min.js"></script>
        <script src="Assets/pages/jquery.sweet-alert.init.js"></script>
    <script type="text/jscript">
        function OnEndCallback(s, e) {
            
            if (s.cpAlertMessage != '') {
                if (s.cpAlertMessage == 'Update') {
                    swal("Information", "The record was successfully updated!", "success");
                } else if (s.cpAlertMessage == 'Insert') {
                    swal("Information", "Registration was successfully registered!", "success");
                } else if (s.cpAlertMessage == 'Delete') {
                    swal("Information", "Registration was successfully enabled / disabled!", "success");
                } else if (s.cpAlertMessage == 'Error') {
                    swal("Information", "An error has occurred from the database");
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

        function UploadFile() {
            var x = document.getElementById("myDIV");

            var codigoParte = cmbParteEdit.GetText();
            var cantidad = ASPxspnCant.GetText();            

            if (codigoParte == "" || cantidad == "") {             

                 swal({
                     title: "Information!",
                     text: "Select a product and enter the quantity",
                     type: "info",
                     timer: 2000,
                     showConfirmButton: false
                 });

                 if (codigoParte == "") {
                     window.setTimeout("cmbParteEdit.Focus()", 100);
                 } else {
                     window.setTimeout("ASPxspnCant.Focus()", 100);
                 }
                 
                 return;               
            }

            HabilitaCambio();

        }

        function HabilitaCambio() {
            var btn = document.getElementById("BtnUploadFile");
            var srcdesc = btn.src;
            var expresionRegular = "/";
            var listaNombres = srcdesc.split(expresionRegular);
            var namebtn = listaNombres[6].toString();

            if (namebtn == "BtnUploadFile.png") {
                btn.setAttribute('src', 'Assets/Images/BtnUploadFileCancel.png');             
            } else {
                btn.setAttribute('src', 'Assets/Images/BtnUploadFile.png');               
            }

            var x = document.getElementById("myDIV");
            var y = document.getElementById("divBotonesPart");
            if (x.style.display === "none") {
                x.style.display = "block";
                y.style.display = "none";
            } else {
                x.style.display = "none";
                y.style.display = "block";
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

            xgrdPartes.PerformCallback(Valores);
        }

            function DisableProvsSelected() {
                var Valores = "";
                $(".chkProv").each(function () {
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

                xgrdProvedores.PerformCallback(Valores);
            }

            function DisableAll() {

                swal({
                    title: "Information",
                    text: "Are you sure you want to enable / disable all registers?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Si!",
                    cancelButtonText: "No!",
                    closeOnConfirm: false,
                    closeOnCancel: false
                },
                function (isConfirm) {
                    if (isConfirm) {
                        CallbackPanelDisableAll.PerformCallback();
                        grid.PerformCallback('Search');
                        swal("Delete!", "All registrations were enabled / disabled successfully", "success");
                    } else {
                        swal("Cancel", "You canceled the operation!", "error");
                    }
                });
            }

            function OnPartesEndCallback(s, e) {
                if (s.cpAlertMessage != '') {
                    if (s.cpAlertMessage == 'SelectOne') {
                        swal("Information", "Select a part to add to the list!", "info");
                    }else if (s.cpAlertMessage == 'SelectCant') {
                            swal("Information", "Enter the Quantity!", "info");
                    } else if (s.cpAlertMessage == 'Exist') {
                        swal({
                            title: "Information",
                            text: "The part is already added to the list",
                            type: "info",
                            timer: 2000,
                            showConfirmButton: false
                        });                    
                    }               
                }
                s.cpAlertMessage = "";
            }

            function UpdPart(inx) {
                var cantNew = cantPartEdit.lastChangedValue;
                var params = "Updated," + inx.toString() + "," + cantNew.toString();
                xgrdPartes.PerformCallback(params);
                xgrdPartes.CancelEdit();
            }

            function OnProveedoresEndCallback(s, e) {
                if (s.cpAlertMessage != '') {
                    if (s.cpAlertMessage == 'SelectOne') {
                        swal("Information", "Select a provider to add to the list!", "info");
                    }
                    //alert(s.cpAlertMessage);
                }
            }

            function OnProductsToRelEndCallback(s, e) {
                if (s.cpAlertMessage != '') {
                    if (s.cpAlertMessage == 'SelectOne') {
                        swal("Information", "Select a provider to add to the list!", "info");
                    }
                    else if (s.cpAlertMessage == "Add") {
                        var proveedor = ASPxCmbProv.lastSuccessText;
                        var spnProveedor = document.getElementById("spnProveedor");
                        spnProveedor.innerText = proveedor;
                        $('#mdlProveedores').modal('show');
                    }
                    else if (s.cpAlertMessage == 'NoProducts') {
                        swal("Information", "The request has no products!", "info");
                    }
                    //alert(s.cpAlertMessage);
                }
            }


            function relacionShip() {
                var Valores = "Save";
                $(".chkRel").each(function () {
                    if (this.checked) {
                        if (Valores == "") {
                            Valores = this.id.substr(3);
                        } else {
                            Valores += "," + this.id.substr(3);
                        }
                    }
                });

                xgrdProvedores.PerformCallback(Valores);
            }

            function sendMessage() {
                var codProv = ASPcmbFilterProv.lastSuccessValue;
                var inp = document.getElementById("inpMessage");
                var valor = codProv + "," + inp.value;
                inp.value = "";
                CallbackPanel.PerformCallback(valor);
            }

            function OnPanel1EndCallback(s, e) {
                if (s.cpAlertMessage != '') {
                    if (s.cpAlertMessage == 'notFolio') {
                        swal("Information", "Comments cannot be added to an unstored request", "info");
                    }
                    else if (s.cpAlertMessage == 'errror') {
                        swal("Information", "There was an error", "info");
                    }
                }
            }

            function CIuplGraphicsFile_OnFileUploadComplete(s, e) {
                if (e.isValid) {
                    //swal("Information", "The record was successfully updated", "success");

                    var codigoParte = cmbParteEdit.GetSelectedItem().value;
                    var cantidad = ASPxspnCant.GetValue();
                    cbSaveVars.PerformCallback(codigoParte + ',' + + ',' + e.callbackData);              
             
                    var params = "SaveWithFile";
                    xgrdPartes.PerformCallback(params);

                    HabilitaCambio();

                    //cmbParteEdit.setAttribute('text', '');
                    
                    //ASPxspnCant.value = "";
                }
                else {
                    swal("Warning", "An error has occurred from the database");
                }
            }

            function CIuplGraphicsFile_OnFilesUploadComplete(args) {
                UpdateUploadGraphicsFileButton();
            }

            function CIuplGraphicsFile_OnUploadStart() {
                btnUploadGraphicsFile.SetEnabled(false);
            }            

            function UpdateUploadGraphicsFileButton() {
                btnUploadGraphicsFile.SetEnabled(CIuplGraphicsFile.GetText(0) != "");
            }

            function ValidarValorParte(s, e) {
                
                var codigoParte = cmbParteEdit.GetText();
                var cant = ASPxspnCant.GetText();
                if (codigoParte == "" || cant == "")
                {
                    var x = document.getElementById("myDIV");
                    var y = document.getElementById("divBotonesPart");
                    if (x.style.display === "block") {
                        x.style.display = "none";
                        y.style.display = "block";
                    }

                    var btn = document.getElementById("BtnUploadFile");
                    var srcdesc = btn.src;
                    var expresionRegular = "/";
                    var listaNombres = srcdesc.split(expresionRegular);
                    var namebtn = listaNombres[6].toString();

                    //if (namebtn == "BtnUploadFile.png") {
                    //    btn.setAttribute('src', 'Assets/Images/BtnUploadFileCancel.png');
                    //} else {
                    //    btn.setAttribute('src', 'Assets/Images/BtnUploadFile.png');
                    //}

                    if (namebtn == "BtnUploadFileCancel.png") {
                        btn.setAttribute('src', 'Assets/Images/BtnUploadFile.png');                    
                    }
                }
            }

            function ValidarValorCant() {
                var cant = ASPxspnCant.GetText();
                if (cant == "") {
                    var x = document.getElementById("myDIV");
                    var y = document.getElementById("divBotonesPart");
                    if (x.style.display === "block") {
                        x.style.display = "none";
                        y.style.display = "block";
                    }

                    var btn = document.getElementById("BtnUploadFile");
                    var srcdesc = btn.src;
                    var expresionRegular = "/";
                    var listaNombres = srcdesc.split(expresionRegular);
                    var namebtn = listaNombres[6].toString();

                    if (namebtn == "BtnUploadFile.png") {
                        btn.setAttribute('src', 'Assets/Images/BtnUploadFileCancel.png');
                    } else {
                        btn.setAttribute('src', 'Assets/Images/BtnUploadFile.png');
                    }
                }              

            }

            function cbSaveVars_Complete(s,e){
                if (s.cpAlertMessage != '') {
                    if (s.cpAlertMessage == 'Exist') {
                        swal({
                            title: "Information",
                            text: "The part is already added to the list",
                            type: "info",
                            timer: 2000,
                            showConfirmButton: false
                        });
                    }
                }
            }

            function Cancelar(){
                $('#myModal').modal('show'); // abrir
                //$('#myModal').modal('hide'); // cerrar
            }

            function Rechazar() {
                $('#myModal').modal('show'); // abrir
                //$('#myModal').modal('hide'); // cerrar
            }


        //CloseModal
            function SendModalCancelar() {
                btnCancelar_Click();
            }


            function SendModalRechazar() {
                btnRechazar_Click();
            }

            function ShowModal(value) {
                if (value == "SendSupplier") {
                    document.getElementById('titleModal').innerHTML = "Send to Suppliers";
                } else if(value == "Cancel") {
                    document.getElementById('titleModal').innerHTML = "Cancel Quote";
                }else if(value == "Rejected"){
                    document.getElementById('titleModal').innerHTML = "Rejected Quote";
                }
                
                
                document.getElementById('DescriptionModal').value = '';
                $('#myModal').modal('show');
            }

            
            function EnviarProveedor() {

                var description = document.getElementById('DescriptionModal').value;
                var title = document.getElementById('titleModal').innerHTML;
                if (description != "") {
                    cbEnviarModal.PerformCallback(description + "," + title);
                    //window.location.href = "cotizaciones.aspx";
                } else {
                    swal('Información', 'Add Reason', 'info')
                    return false;
                }
            }

            function selectedAllparts(selected) {
                $(".chkRel").each(function () {
                    this.checked = selected;
                });
            }

            $(document).ready(function () {
                // Muestra las teclas rápidas
                $("span.badge").hide(); //Se puso aquí para que no cambara de lugar los botones
                $("body").keydown(function (e) {
                    if (e.which == 17) {
                        $("span.badge").toggle();
                    }
                    if (e.altKey) {
                        if (e.altKey && e.which == 49) { //1
                            alert('boton 1');
                        }
                        if (e.altKey && e.which == 50) {//2
                            alert('boton 2');
                        }
                        if (e.altKey && e.which == 51) {//3
                            alert('boton 3');
                        }
                        if (e.altKey && e.which == 52) {//4
                            alert('boton 4');
                        }
                        if (e.altKey && e.which == 53) {//5
                            alert('boton 5');
                        }
                        if (e.altKey && e.which == 54) {//6
                            alert('boton 6');
                        }
                        if (e.altKey && e.which == 55) {//7
                            alert('boton 7');
                            $(".clickable").click();
                        }
                        if (e.altKey && e.which == 56) {//8
                            $(".clickable").click();
                        }
                        if (e.altKey && e.which == 57) {//9
                            $("#BtnInicio").get(0).click();
                        }
                        if (e.altKey && e.which == 48) {//0
                            $("#BtnFinal").get(0).click();
                        }
                    };
                });
                // Funcionalidad de los paneles colapsables
                $(document).on('click', '.panel-heading span.clickable', function (e) {
                    var $this = $(this);
                    if (!$this.hasClass('panel-collapsed')) {
                        $this.parents('.panel').find('.panel-body').slideUp();
                        $this.addClass('panel-collapsed');
                        $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
                    } else {
                        $this.parents('.panel').find('.panel-body').slideDown();
                        $this.removeClass('panel-collapsed');
                        $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
                    }
                });
                // Colapsa y expande todos a la vez
                $("#btnColapsable").click(function () {
                    $(".clickable").click();
                });
            });
            //Rota los iconos de inicio y fin, va fuera del ready
            function rotate(imgName) {
                var elm = document.getElementById(imgName);
                var className = elm.className;
                if (className.indexOf('BtnRotar') === -1) {
                    elm.className = elm.className + ' BtnRotar';
                } else {
                    elm.className = elm.className.replace(' BtnRotar', '');
                };
            };
            //Quita los checked de default de las tablas de bootstrap
            function quitaChecked(value) {
                return {
                    checked: false
                }
                return value
            };
    </script>
    <style>
        html {
            scroll-behavior: smooth;
        }

        body {
            background-color: #FBFBFB;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 12px;
        }

        .jumbotron {
            background-color: #FBFBFB;
        }

        .CeroPM {
            padding: 0px;
            margin: 0px;
        }

        .BGFormularioTitulo {
            margin-top: 5px;
            padding-bottom: 18px;
            background-image: url('./Assets/images/IconoFormulario.png');
            background-repeat: no-repeat;
            background-position: left top;
        }

        .HeaderSpanNombreFormulario {
            text-align: left;
            color: #355D87;
            margin: 0px;
            margin-left: 35px;
        }

        .RowsControl {
            margin: 0px;
            padding: 0px;
            border: 0px;
            border-bottom: 1px;
            border-bottom-color: #BCE8F1;
            border-bottom-style: dashed;
        }

        .clickable {
            cursor: pointer;
        }

        .panel {
            margin-bottom: 15px;
        }

        .panel-heading {
            height: 25px;
            font-weight: 400;
            padding: 5px 15px;
        }

            .panel-heading span {
                margin-top: -20px;
                font-size: 15px;
            }

        .panel-title {
            font-size: 14px;
            font-weight: normal;
        }

        .text-form {
            color: #585858;
            font-weight: normal;
            padding: 5px;
            vertical-align: central;
        }

        .Campos {
            height: 25px;
            background-color: white;
            margin: 1px;
            padding: 2px;
            border: 1px dashed #D9D9D9;
            border-radius: 3px;
            color: black;
        }

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
            color: #fff;
            background-color: #337AB7;
        }

        .BtnGpoIniFin {
            background-color: white;
            vertical-align: central;
            text-align: center;
            height: 81px;
            width: 35px;
            top: 50vh;
            right: 0px;
            position: fixed;
            z-index: 1;
            border-radius: 4px 0 0 4px;
            box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);
        }

        .BtnIniFin {
            display: block;
            height: 40px;
            font-size: x-large;
            padding-top: 5px;
        }

        .BtnRotar {
            animation-name: rotarImagen;
            animation-duration: 1s;
            animation-iteration-count: infinite;
        }

        @keyframes rotarImagen {
            100% {
                color: forestgreen;
                left: 0px;
                top: 0px;
                transform: rotate(360deg);
            }
        }

        #spanStatus {
            width: 190px;
            transition-property: width;
            transition-duration: 1s;
        }

            #spanStatus:hover {
                width: 390px;
            }

        .docEstatus {
            position: absolute;
            left: 60px;
            font-weight: bold;
            padding: 1px 10px;
            margin: 0px;
        }

        .fixed-table-toolbar {
            width: 100%;
        }

        .bootstrap-table bootstrap3 {
            width: 100%;
        }
        .badge{
            font-size:9px;
            font-weight:normal;
            transform: translate(5px, -8px);
            padding:2px 2px;
        }
        .hidden{
            display:none;
        }
    </style>
    <div class="CeroPM" id="Inicio">
    <div class="container-fluid CeroPM">
        <div id="divBotones" class="col-xs-12 btn-group CeroPM" data-spy="affix" data-offset-top="100" style="color:white;width:100%;z-index:1 !important;background-color:#EFEFEF;">
            <asp:ImageButton ID="btnSave" class="btn" Style="margin: 0px; padding: 0px;" runat="server" OnClick="btnSave_Click" ImageUrl="~/Assets/images/BtnGuardar.png"  />
            <asp:ImageButton ID="btnRegresar" class="btn" Style="margin: 0px; padding: 0px;" runat="server" ImageUrl="~/Assets/images/BtnSalir.png" OnClick="btnRegresar_Click" />
            <div class="BtnGpoIniFin">
                <a class="BtnIniFin" id="BtnInicio" href="#Inicio" title="Inicio" onmouseover="rotate('imgaRotarI');" onmouseout="rotate('imgaRotarI');"><i id="imgaRotarI" class="glyphicon glyphicon glyphicon-circle-arrow-up"></i><span class="badge badge-xs badge-danger">Alt+9</span></a>
                <a class="BtnIniFin" id="BtnFinal" href="#pieForma" title="Historial" onmouseover="rotate('imgaRotarF');" onmouseout="rotate('imgaRotarF');"><i id="imgaRotarF" class="glyphicon glyphicon glyphicon-circle-arrow-down"></i><span class="badge badge-xs badge-danger">Alt+0</span></a>
            </div>
        </div>
        <div class="jumbotron CeroPM" style="padding: 5px;">
            <div class="row CeroPM">
                <div class="col-xs-12">
                    <div class="col-xs-12 col-sm-8 form-group BGFormularioTitulo">
                        <h2 class="HeaderSpanNombreFormulario">ALTA DE ARTÍCULO</h2>
                        <asp:Label ID="lblcodigoSts" runat="server" CssClass="hidden"></asp:Label>
                        <asp:Literal ID="ltlSts" runat="server"></asp:Literal>
<%--                        <span id="spanStatus" class="alert btn-info docEstatus" hidden><i class="glyphicon glyphicon-edit" style="padding-right:5px;"></i>Abierto</span><span style="position: absolute;left: 250px;color:#FBFBFB;padding:2px 0px;" hidden>:Pendiente por el autor para terminar la captura</span>
                        <span id="spanStatus" class="alert btn-info docEstatus" hidden><i class="glyphicon glyphicon-eye-open" style="padding-right:5px;"></i>Revisado</span><span style="position: absolute;left: 250px;color:#FBFBFB;padding:2px 0px;" hidden>:Revisado para su proceso</span>
                        <span id="spanStatus" class="alert btn-info docEstatus" hidden><i class="glyphicon glyphicon-lock" style="padding-right:5px;"></i>Cerrado</span><span style="position: absolute;left: 250px;color:#FBFBFB;padding:2px 0px;" hidden>:Cerrado por Alejandro Gonalez el 12/10/20 2:35PM</span>
                        <span id="spanStatus" class="alert btn-warning docEstatus" hidden><i class="glyphicon glyphicon-ban-circle" style="padding-right:5px;"></i>Cancelado</span><span style="position: absolute;left: 250px;color:#FBFBFB;padding:2px 0px;" hidden>:Por Alejandro Gonzalez el 12/10/20 10:02AM</span>
                        <span id="spanStatus" class="alert btn-success docEstatus" hidden><i class="glyphicon glyphicon-ok" style="padding-right:5px;"></i>Aprobado</span><span style="position: absolute;left: 250px;color:#FBFBFB;padding:2px 0px;" hidden>:Aprobado el 24/10/20 08:20AM</span>
                        <span id="spanStatus" class="alert btn-primary docEstatus" hidden><i class="glyphicon glyphicon-dashboard" style="padding-right:5px;text-align:center;"></i>Pendiente por aprobación</span><span style="position: absolute;left: 250px;color:#FBFBFB;padding:2px 0px;" hidden>:Pendiente por el aprobador</span>
                        <span id="spanStatus" class="alert btn-danger docEstatus"><i class="glyphicon glyphicon-remove" style="padding-right:5px;"></i>Rechazado</span><span style="position: absolute;left: 250px;color:#FBFBFB;padding:2px 0px;">:Rechazado por Joel Hernandez</span>--%>
                    </div>
                    <div class="col-xs-12 col-sm-4 CeroPM" style="padding-top:3px;">
                        <div class="row form-group RowsControl">
                            <label class="text-form col-xs-4" style="padding: 1px;margin: 0px;">No. Documento</label>
                            <div class="col-xs-4 " style="color:red; font-weight:800;">
                                <asp:TextBox ID="lblnDoc" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group RowsControl">
                            <label class="text-form col-xs-4" style="padding: 1px;margin: 0px;">Solicitante</label>
                            <div class="col-xs-8" style="color:limegreen;">
                                <asp:Label ID="lblDocSolicitante" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row form-group RowsControl">
                            <label class="text-form col-xs-4" style="padding: 1px;margin: 0px;">Fecha de solicitud</label>
                            <div class="col-xs-8" style="color:limegreen;">
                                <asp:Label ID="lblDocFechaSol" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <form class="container-fluid">
            <section id="sSolicitante" class="row CeroPM">
                <div class="col-xs-12">
                    <div class="panel panel-info" id="infoSolicitante">
                        <div class="panel-heading">
                            <h3 class="panel-title">Información del Solicitante</h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i></span>
                            <span class="pull-right" id="btnColapsable" style="padding-right: 10px;cursor: pointer;"><i class="glyphicon glyphicon glyphicon-sort"></i><span class="badge badge-xs badge-info" style="font-size:9px;">Alt+8</span></span>
                        </div>
                        <div class="panel-body">
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Reemplaza a otro</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="remplazaOtro" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-2">Cuál artículo</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="cualArticulo" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <fieldset style="padding:10px;border: 1px solid #B5D2EA;border-radius:5px;">
                                <legend style="font-size:12px;font-weight:600;border:none;width: auto;padding:2px;color:#8CBADF;margin-bottom:0px;">Máquina asociada</legend>
                                <div class="row form-group CeroPM">
                                    <label class="text-form col-sm-1">Máquina</label>
                                    <div class="col-sm-11">
                                        <input type="hidden" id="hdnCodigoMaquina" value='<%# Eval("CodigoMaquina")%>' runat="server"/>
                                        <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbMaquina" runat="server" IncrementalFilteringMode="Contains" 
                                            FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                            PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                            <ValidationSettings>
                                                <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                            </ValidationSettings>
                                            <ClientSideEvents/>
                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                        </dx:ASPxComboBox>
                                    </div>
                                </div>
                                <div class="row form-group CeroPM">
                                    <label class="text-form col-sm-1">Subcategoría 1</label>
                                    <div class="col-sm-3">
                                        <input type="hidden" id="hdnCodigoSubCat1" value='<%# Eval("CodigoSubcategoria1")%>' runat="server"/>
                                        <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbSubCat1" runat="server" IncrementalFilteringMode="Contains" 
                                            FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                            PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                            <ValidationSettings>
                                                <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                            </ValidationSettings>
                                            <ClientSideEvents/>
                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                        </dx:ASPxComboBox>
                                    </div>
                                    <label class="text-form col-sm-1">Subcategoría 2</label>
                                    <div class="col-sm-3">
                                        <input type="hidden" id="hdnCodigoSubCat2" value='<%# Eval("CodigoSubcategoria2")%>' runat="server"/>
                                        <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbSubCat2" runat="server" IncrementalFilteringMode="Contains" 
                                            FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                            PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                            <ValidationSettings>
                                                <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                            </ValidationSettings>
                                            <ClientSideEvents/>
                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                        </dx:ASPxComboBox>
                                    </div>
                                    <label class="text-form col-sm-1">Subcategoría 3</label>
                                    <div class="col-sm-3">
                                        <input type="hidden" id="hdnCodigoSubCat3" value='<%# Eval("CodigoSubcategoria3")%>' runat="server"/>
                                        <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbSubCat3" runat="server" IncrementalFilteringMode="Contains" 
                                            FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                            PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                            <ValidationSettings>
                                                <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                            </ValidationSettings>
                                            <ClientSideEvents/>
                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                        </dx:ASPxComboBox>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row form-group CeroPM" style="padding-top:10px;">
                                <label class="text-form col-sm-2">Utilizado en </label>
                                <div class="col-sm-4">
                                        <input type="hidden" id="hdnCodigoUtilizado" value='<%# Eval("CodigoUtilizado")%>' runat="server"/>
                                        <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbUtilizado" runat="server" IncrementalFilteringMode="Contains" 
                                            FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                            PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                            <ValidationSettings>
                                                <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                            </ValidationSettings>
                                            <ClientSideEvents/>
                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                        </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Departamento</label>
                                <div class="col-sm-4">
                                        <input type="hidden" id="hdnCodigoDepto" value='<%# Eval("CodigoDepto")%>' runat="server"/>
                                        <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbDepa" runat="server" IncrementalFilteringMode="Contains" 
                                            FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                            PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                            <ValidationSettings>
                                                <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                            </ValidationSettings>
                                            <ClientSideEvents/>
                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                        </dx:ASPxComboBox>
                                </div>
                            </div>


                            <!--***************************TIPOS ARTICULO*********************************************************-->
                            <div class="row form-group CeroPM">
                                <div class="col-xs-12">
                                    <table id="table1"
                                           data-toggle="table"
                                           data-sortable="true"
                                           data-sort-name="resTipo"
                                           data-sort-order="asc"
                                           data-show-toggle="true"
                                           data-show-columns="true"
                                           data-show-fullscreen="true"
                                           data-buttons-class="primary"
                                           data-search="true"
                                           data-show-search-clear-button="true"
                                           data-resizable="true"
                                           data-show-footer="true"
                                           class="CeroPM">
                                        <thead style="background-color:#d9edf7;text-align:center;">
                                            <tr>
                                                <th data-field="resTipo" data-sortable="true" data-width="50" data-width-unit="%">Tipo de Artículo</th>
                                                <th data-field="resM" data-sortable="true" data-width="10" data-width-unit="%">M</th>
                                                <th data-field="resN" data-sortable="true" data-width="10" data-width-unit="%">N</th>
                                                <th data-field="resComs" data-sortable="true" data-width="30" data-width-unit="%">Comentarios / Justificaciones</th>   
                                            </tr>
                                        </thead>
                                        <tbody style="background-color:white;">
                                            <asp:Repeater id="tbRepetTipoArticulo" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("tipo") %></td>
                                                        <td><%# Eval("M") %></td>
                                                        <td><%# Eval("N") %></td>
                                                        <td><%# Eval("commentarios") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
<%--                                            <tr>
                                                <td>Impacto en la productividad a causa de la escasez de una refacción</td>
                                                <td>Fuerte</td>
                                                <td>Medio/Bajo</td>
                                                <td>Justificar</td>
                                            </tr>
                                            <tr>
                                                <td>Tiempo para suministrar o la disponibilidad de un proveedor local</td>
                                                <td>>2 días</td>
                                                <td><2 días</td>
                                                <td>Solución en la hoja de preparación</td>
                                            </tr>--%>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td>TOTALES</td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                            <!--**************************************************************************************************-->
                            <br/>
                            <div class="alert alert-success center-block" style="text-align:center;padding:5px; width:50%;margin:auto;">
                                <strong>Importante!</strong> Artículo con referencia <a href="#" class="alert-link">M</a>: Con Stock.
                            </div>
                            <hr style="border-color: #B5D2EA;"/>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Descripción 1</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDescripcion1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-2">Descripción 2</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDescripcion2" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Descripción Larga</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDescripcionLarga" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <hr style="border-color: #B5D2EA;"/>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">No. plan </label>
                                <div class="col-sm-2">
                                    <input type="hidden" id="hfnCodigoPlan" value='<%# Eval("CodigoPlan")%>' runat="server"/>
                                    <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbPlan" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Cómo ayudar si el stock es 0?</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtcomoAyudarStockCero" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Función de la máquina</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtfuncionMaquina" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <hr style="border-color: #B5D2EA;" />
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-1">Cantidad Orden</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtCantidad" OnTextChanged="recalculaTotal" AutoPostBack="true" TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-1">Stock Mínimo</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtStockMin" TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-1">Stock Máximo</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtStockMax" TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-1">Marca</label>
                                <div class="col-sm-3">
                                    <input type="hidden" id="hdnCodigoMarca" value='<%# Eval("CodigoMarca")%>' runat="server"/>
                                    <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbMarca" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-1">Proveedor</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnCodigoProveedor" value='<%# Eval("CodigoProveedor")%>' runat="server"/>
                                    <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbProveedor" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-1">¿Es único?</label>
                                <div class="col-sm-2">
                                    <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbUnico" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-1">Fecha Cotización</label>
                                <div class="col-sm-2">
                                    <dx:ASPxDateEdit ID="xDateFechaCot" runat="server" Width="200px"  CssClass="form-control input-sm Campos"
                                        DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd">                                                             
                                        <TimeSectionProperties>
                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                        </TimeSectionProperties>
                                    </dx:ASPxDateEdit> 
                                </div>
                                <label class="text-form col-sm-1">Precio Unitario</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtPrecioU" OnTextChanged="recalculaTotal" AutoPostBack="true"  TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-1">Días de Entrega</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtDiasEntrega" TextMode="Number" step="1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-1">Moneda</label>
                                <div class="col-sm-2">
                                    <input type="hidden" id="hdnCodigomoneda" value='<%# Eval("Codigomoneda")%>' runat="server"/>
                                    <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbMoneda" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <h3>
                                    <label class="text-form col-sm-offset-4 col-sm-2" style="text-align:right">Total</label>
                                    <label class="text-form col-sm-2" style="color:green;;width:15px">$ </label>
                                    <asp:Label ID="lblTotal" runat="server" CssClass="text-form col-sm-2" style="color:green;"></asp:Label>
                                </h3>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section id="sMantenimiento" class="row CeroPM">
                <div class="col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Información de Mantenimiento</h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i></span>
                        </div>
                        <div class="panel-body">
                            <div class="row form-group CeroPM">
                                <div class="col-xs-12">
                                    <table id="table1"
                                           data-toggle="table"
                                           data-sortable="true"
                                           data-sort-name="resId"
                                           data-sort-order="asc"
                                           data-show-toggle="true"
                                           data-show-columns="true"
                                           data-show-fullscreen="true"
                                           data-buttons-class="primary"
                                           data-search="true"
                                           data-show-search-clear-button="true"
                                           data-resizable="true"
                                           data-click-to-select="true"
                                           class="CeroPM">
                                        <thead style="background-color:#d9edf7;text-align:center;">
                                            <tr>
                                                <th data-field="tmttoId" data-checkbox="true" data-formatter="quitaChecked"></th>
                                                <th data-field="tmttoEsp" data-sortable="true" data-width="45" data-width-unit="%">Especificación</th>
                                                <th data-field="tmttoNot" data-sortable="true" data-width="25" data-width-unit="%">Notas</th>
                                                <th data-field="tmttoClas" data-sortable="true" data-width="10" data-width-unit="%">Clasificación</th>
                                                <th data-field="tmttoResp" data-sortable="true" data-width="20" data-width-unit="%">Responsable</th>
                                            </tr>
                                        </thead>
                                        <tbody style="background-color:white;">
                                            <asp:Repeater id="tbRepetMtto" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("especificacion") %></td>
                                                        <td><%# Eval("notas") %></td>
                                                        <td><%# Eval("clasificacion") %></td>
                                                        <td><%# Eval("responsable") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
<%--                                            <tr>
                                                <td>1</td>
                                                <td>Estratégico  (60)</td>
                                                <td>A: No se que</td>
                                                <td>S500</td>
                                                <td>RESPONSABLE DEL MTTO</td>
                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>Reparable  (29)</td>
                                                <td></td>
                                                <td>S100</td>
                                                <td>SOLICITANTE</td>
                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>Reposición bajo demanda   (20)</td>
                                                <td></td>
                                                <td>S100</td>
                                                <td>RESP. DE ALMACÉN</td>
                                            </tr>--%>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </section>
            <section id="sAlmacen" class="row CeroPM">
                <div class="col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Información de Almacén</h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i></span>
                        </div>
                        <div class="panel-body">
                            <div class="row form-group CeroPM">
                                <div class="col-xs-12">
                                    <table id="table1"
                                           data-toggle="table"
                                           data-sortable="true"
                                           data-sort-name="talmId"
                                           data-sort-order="asc"
                                           data-show-toggle="true"
                                           data-show-columns="true"
                                           data-show-fullscreen="true"
                                           data-buttons-class="primary"
                                           data-search="true"
                                           data-show-search-clear-button="true"
                                           data-resizable="true"
                                           data-click-to-select="true"
                                           class="CeroPM">
                                        <thead style="background-color:#d9edf7;text-align:center;">
                                            <tr>
                                                <th data-field="talmId" data-checkbox="true" data-formatter="quitaChecked"></th>
                                                <th data-field="talmEsp" data-sortable="true" data-width="45" data-width-unit="%">Especificación</th>
                                                <th data-field="talmNotas" data-sortable="true" data-width="25" data-width-unit="%">Notas</th>
                                                <th data-field="talmClas" data-sortable="true" data-width="10" data-width-unit="%">Clasificación</th>
                                                <th data-field="talmResp" data-sortable="true" data-width="20" data-width-unit="%">Responsable</th>
                                            </tr>
                                        </thead>
                                        <tbody style="background-color:white;">
                                            <asp:Repeater id="tbRepetAlmacen" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("especificacion") %></td>
                                                        <td><%# Eval("notas") %></td>
                                                        <td><%# Eval("clasificacion") %></td>
                                                        <td><%# Eval("responsable") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
<%--                                            <tr>
                                                <td>1</td>
                                                <td>Refractario (35)</td>
                                                <td>A: No se que</td>
                                                <td>S500</td>
                                                <td>RESPONSABLE DEL MTTO</td>
                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>Obsoleto</td>
                                                <td>U : Posible consumo</td>
                                                <td>S100</td>
                                                <td>RESP. DE ALMACÉN</td>
                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>Consignación</td>
                                                <td></td>
                                                <td>S700</td>
                                                <td>RESP. DE ALMACÉN</td>
                                            </tr>--%>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </section>
            <section id="sComprador" class="row CeroPM">
                <div class="col-xs-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Información del Comprador</h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i></span>
                        </div>
                        <div class="panel-body">
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-3">Ficha de datos de seguridad</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtFichaSeguridad" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-3">Unidad de Medida</label>
                                <div class="col-sm-3">
                                    <input type="hidden" id="hdnCodigoUM" value='<%# Eval("CodigoUM")%>' runat="server"/>
                                    <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbCodigoUM" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-3">Conteo Cíclico</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtConteoCiclico" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-3">Alacenamiento externo posbible</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtAlmacenamientoExt" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-3">Planeador</label>
                                <div class="col-sm-3">
                                    <input type="hidden" id="hdnPlaneador" value='<%# Eval("codigoPlaneador")%>' runat="server"/>
                                    <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbPlaneador" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-3">Comprador</label>
                                <div class="col-sm-3">
                                    <input type="hidden" id="hdnComprador" value='<%# Eval("codigoComprador")%>' runat="server"/>
                                    <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbComprador" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-3">Ficha de inventario</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtFichaInv" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-3">Múltiplo</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtMultiplo" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                               <label class="text-form col-sm-3">Hoja de seguridad</label>
                               <div class="col-sm-3">
                                   <asp:TextBox ID="txtHojaSeguridad" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                               </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section id="sDataManagement" class="row CeroPM">
                <div class="col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Información del Data Management</h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i></span>
                        </div>
                        <div class="panel-body">
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Código de artículo</label>
                                <label class="text-form col-sm-1"><span class="label label-success pull-right" style="padding:6px;">M</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtCodigoArticulo" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </form>
        <div id="pieForma">
            <div class="col-xs-12" style="background-color:dimgray;height:25px;font-weight:600;color:white;text-align:center;font-size:16px;">
                Historal del documento
            </div>
            <ul class="nav nav-tabs" style="padding-left:5px;">
                <li class="active"><a data-toggle="tab" href="#home">Aprobaciones</a></li>
                <li><a data-toggle="tab" href="#menu1">Comentarios adicionales</a></li>
            </ul>
            <div class="tab-content" style="padding:15px;">
                <div id="home" class="tab-pane fade in active">
                    <table id="table"
                           data-toggle="table"
                           data-sort-class="table-active"
                           data-sortable="true"
                           data-sort-name="id"
                           data-sort-order="asc"
                           data-show-toggle="true"
                           data-show-columns="true"
                           data-show-fullscreen="true"
                           data-buttons-class="primary"
                           data-search="true"
                           data-show-search-clear-button="true"
                           data-resizable="true"
                           class="CeroPM">
                        <thead style="background-color:#d9edf7;text-align:center;">
                            <tr>
                                <th data-field="id" data-sortable="true">Paso</th>
                                <th data-field="titulo" data-sortable="true">Título </th>
                                <th data-field="usuario" data-sortable="true">Usuario</th>
                                <th data-field="name" data-sortable="true">Puesto</th>
                                <th data-field="fechainicio" data-sortable="true">Fecha de notificación</th>
                                <th data-field="fechafin" data-sortable="true">Fecha de acción</th>
                                <th data-field="accion" data-sortable="true">Acción</th>
                                <th data-field="comentario" data-sortable="true">Comentario</th>
                            </tr>
                        </thead>
                        <tbody style="background-color:white;">
                            <asp:Repeater id="tbRepetAprobaciones" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("paso") %></td>
                                        <td><%# Eval("titulo") %></td>
                                        <td><%# Eval("codigoEmpleado") %></td>
                                        <td><%# Eval("codigoEmpleado") %></td>
                                        <td><%# Eval("fechaNotificacion") %></td>
                                        <td><%# Eval("fechaAccion") %></td>
                                        <td><%# Eval("accion") %></td>
                                        <td><%# Eval("comentario") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
<%--                            <tr>
                                <td>1</td>
                                <td>Autor</td>
                                <td>Alejandro Gonzalez</td>
                                <td>Supervisor</td>
                                <td>02/11/2020 08:45AM</td>
                                <td>02/11/2020 09:00AM</td>
                                <td>Envío</td>
                                <td>Todo listo para tirar</td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>Jefe Directo</td>
                                <td>Juan Perez</td>
                                <td>Sub gerente</td>
                                <td>02/11/2020 09:00AM</td>
                                <td>02/11/2020 09:15AM</td>
                                <td>Aprobó</td>
                                <td>Facturen lo más pronto posible</td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>Transportista</td>
                                <td>Julio Guerrero</td>
                                <td>Proveedor</td>
                                <td>02/11/2020 09:15AM</td>
                                <td>03/11/2020 4:30PM</td>
                                <td>Cargó</td>
                                <td>Ninguno</td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>Entrega de manifiesto</td>
                                <td>Juan Carlos Perez</td>
                                <td>Especialista</td>
                                <td>03/11/2020 04:30pM</td>
                                <td>10/11/2020 1:30PM</td>
                                <td>Recibió</td>
                                <td>Comenta el transportista que hubo problemas con la documentación, por lo tanto esta es una descripción nmuy larga apra ver co</td>
                            </tr>--%>
                        </tbody>
                    </table>
                </div>
                <div id="menu1" class="tab-pane fade">
                    <h3>Comentarios Adicionales</h3>
                    <p><asp:TextBox ID="txtComentarios" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox></p>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
