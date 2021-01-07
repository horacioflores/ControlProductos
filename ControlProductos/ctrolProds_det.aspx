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
        <script src="Assets/pages/jquery.sweet-alert.init.js"></script> <!--prueba github-->
    <script type="text/jscript">

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

       function OnTipoArticuloEndCallback(s, e) {
            if (s.cpAlertMessage != '') {
                if (s.cpAlertMessage == 'SelectOne') {
                    swal("Information", "Type and code cannot be empty!", "info");
                } else if (s.cpAlertMessage == 'Exist') {
                    swal({
                        title: "Information",
                        text: "Type and code already exist",
                        type: "info",
                        timer: 2000,
                        showConfirmButton: false
                    });
                }
            }
            s.cpAlertMessage = "";
       }

       function AddedTiposArticulos() {
           var Valores = "";
           $(".chkArtMDL").each(function () {
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
           xgrdTipoArticuloMDL.PerformCallback(Valores);
       }

       function OnTipoArticuloMDLEndCallback(s, e) {
           if (s.cpAlertMessage != '') {
               if (s.cpAlertMessage == 'Add') {
                   xgrdTipoArticulo.PerformCallback('Save');
               }
           }
           s.cpAlertMessage = "";
       }

       function DisableSelectedTipoArt() {

           var Valores = "";
           $(".chkArt").each(function () {
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

           xgrdTipoArticulo.PerformCallback(Valores);
       }

       function OnMttoEndCallback(s, e) {
           if (s.cpAlertMessage != '') {
               if (s.cpAlertMessage == 'SelectOne') {
                   swal("Information", "Specification and code are empty!", "info");
               } else if (s.cpAlertMessage == 'Exist') {
                   swal({
                       title: "Information",
                       text: "Specification and code already exist",
                       type: "info",
                       timer: 2000,
                       showConfirmButton: false
                   });
               }
           }
           s.cpAlertMessage = "";
       }

       function SelMtto(obj) {
           valores = "SelMtto;" + obj.id.toString() + ";" + obj.checked;
           xgrdMtto.PerformCallback(valores);
       }

       function SelAlmn(obj) {
           valores = "SelAlmn;" + obj.id.toString() + ";" + obj.checked;
           xgrdAlmacen.PerformCallback(valores);
       }

       function DisableSelectedMtto() {

           var Valores = "";
           $(".chkMtto").each(function () {
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

           xgrdMtto.PerformCallback(Valores);
       }

       function AddedMttos() {
           var Valores = "";
           $(".chkMttoMDL").each(function () {
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
           xgrdMttoMDL.PerformCallback(Valores);
       }

       function OnMttoxgrdMttoMDLEndCallback(s, e) {
           if (s.cpAlertMessage != '') {
               if (s.cpAlertMessage == 'Add') {
                   xgrdMtto.PerformCallback('Save');
               }
           }
           s.cpAlertMessage = "";
       }

       function OnAlmacenEndCallback(s, e) {
           document.getElementById("MainContent_txtCodigoAlmacenAdd").value = "";
           document.getElementById("MainContent_txtEspecificacionAlMAdd").value = "";
           document.getElementById("MainContent_txtResponsableALMAdd").value = "";
           document.getElementById("MainContent_txtCasificacionALMAdd").value = "";
           document.getElementById("MainContent_txtNotasALMAdd").value = "";
           if (s.cpAlertMessage != '') {
               if (s.cpAlertMessage == 'SelectOne') {
                   swal("Information", "Specification and code are empty!", "info");
               } else if (s.cpAlertMessage == 'Exist') {
                   swal({
                       title: "Information",
                       text: "Specification and code already exist",
                       type: "info",
                       timer: 2000,
                       showConfirmButton: false
                   });
               }
           }
           s.cpAlertMessage = "";
       }

       function AddedAlmnes() {
           var Valores = "";
           $(".chkAlmnMDL").each(function () {
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
           xgrdAlmacenMDL.PerformCallback(Valores);
       }

       function OnAlmacenMDLEndCallback(s, e) {
           if (s.cpAlertMessage != '') {
               if (s.cpAlertMessage == 'Add') {
                   xgrdAlmacen.PerformCallback('Save');
               }
           }
           s.cpAlertMessage = "";
       }

       function DisableSelectedAlmacen() {

           var Valores = "";
           $(".chkAlmn").each(function () {
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

           xgrdAlmacen.PerformCallback(Valores);
       }

       function OnAprobacionesEndCallback(s, e) {
           CallbackPanel.PerformCallback("Limpiar");
           if (s.cpAlertMessage != '') {
               if (s.cpAlertMessage == 'SelectOne') {
                   swal("Information", "The title is empty!", "info");
               } else if (s.cpAlertMessage == 'Exist') {
                   swal({
                       title: "Information",
                       text: "Specification and code already exist",
                       type: "info",
                       timer: 2000,
                       showConfirmButton: false
                   });
               }
           }
           s.cpAlertMessage = "";
       }

       function DisableSelectedAprobacion() {

           var Valores = "";
           $(".chkAprb").each(function () {
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

           xgrdAprobaciones.PerformCallback(Valores);
       }

       function recalcular() {
           cantidad = parseFloat(document.getElementById("MainContent_txtCantidad").value);
           precio = parseFloat(document.getElementById("MainContent_txtPrecioU").value);
           total = cantidad * precio;
           document.getElementById("MainContent_lblTotal").innerHTML = (Math.round(total*100)/100).toString();
       }

       function OnPanel1EndCallback(s, e) {
            if (s.cpAlertMessage != '') {
                if (s.cpAlertMessage == 'errror') {
                    swal("Information", "There was an error", "info");
                }
            }
       }


        $(document).ready(function () {
            // Muestra las teclas rápidas
            $("span.badge").hide(); //Se puso aquí para que no cambara de lugar los botones
            $("body").keydown(function (e) {
                if (e.which == 17) {
                    $("span.badge").toggle();
                }
                if (e.altKey) {
                    if (e.altKey && ((e.which == 49) || (e.which == 97))) { //1
                        alert('boton 1');
                    }
                    if (e.altKey && ((e.which == 50) || (e.which == 98))) {//2
                        alert('boton 2');
                    }
                    if (e.altKey && ((e.which == 51) || (e.which == 99))) {//3
                        alert('boton 3');
                    }
                    if (e.altKey && ((e.which == 52) || (e.which == 100))) {//4
                        alert('boton 4');
                    }
                    if (e.altKey && ((e.which == 53) || (e.which == 101))) {//5
                        alert('boton 5');
                    }
                    if (e.altKey && ((e.which == 54) || (e.which == 102))) {//6
                        alert('boton 6');
                    }
                    if (e.altKey && ((e.which == 55) || (e.which == 103))) {//7
                        $(".clickable").click();
                    }
                    if (e.altKey && ((e.which == 56) || (e.which == 104))) {//8
                        $(".clickable").click();
                    }
                    if (e.altKey && ((e.which == 57) || (e.which == 105))) {//9
                        $("#BtnInicio").get(0).click();
                    }
                    if (e.altKey && ((e.which == 48) || (e.which == 96))) {//0
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

        function openModal(opcion) {
            switch (opcion) {
                case 'TipoArticulo':
                        $('#mdlTipoArticulo').modal('show');
                        break;
                case 'Mantenimiento':
                    $('#mdlMtto').modal('show');
                    break;
                case 'Almacen':
                    $('#mdlAlmacen').modal('show');
                    break;
                case 'Aprobaciones':
                    $('#mdlAprobaciones').modal('show');
                    break;
            }
        }

        function ASPxCallbackPanel2_EndCallback(s, e) {
            if (s.cpAlertMessage != '') {
                if (s.cpAlertMessage == 'InputNoDoc') {
                    swal("Information", "Add the document number", "info");
                }
                if (s.cpAlertMessage == 'SelectMachine') {
                    swal("Information", "Select a machine", "info");
                }
                if (s.cpAlertMessage == 'SelectSubcategory1') {
                    swal("Information", "Select a subcategory 1", "info");
                }
                if (s.cpAlertMessage == 'SelectSubcategory2') {
                    swal("Information", "Select a subcategory 3", "info");
                }
                if (s.cpAlertMessage == 'SelectSubcategory3') {
                    swal("Information", "Select a subcategory 3", "info");
                }
                if (s.cpAlertMessage == 'SelectUsed') {
                    swal("Information", "Select a used", "info");
                }
                if (s.cpAlertMessage == 'SelectDepartment') {
                    swal("Information", "Select a department", "info");
                }
                if (s.cpAlertMessage == 'SelectPlan') {
                    swal("Information", "Select a plan", "info");
                }
                if (s.cpAlertMessage == 'SelectBrand') {
                    swal("Information", "Select a brand", "info");
                }
                if (s.cpAlertMessage == 'SelectProvider') {
                    swal("Information", "Select a provider", "info");
                }
                if (s.cpAlertMessage == 'SelectUnique') {
                    swal("Information", "Select Unique", "info");
                }
                if (s.cpAlertMessage == 'SelectQuoteDate') {
                    swal("Information", "Select Quote Date", "info");
                }
                if (s.cpAlertMessage == 'SelectCurrency') {
                    swal("Information", "Select Currency", "info");
                }
                if (s.cpAlertMessage == 'SelectUM') {
                    swal("Information", "Select UM", "info");
                }
                if (s.cpAlertMessage == 'SelectGlider') {
                    swal("Information", "Select Glider", "info");
                }
                if (s.cpAlertMessage == 'SelectBuyer') {
                    swal("Information", "Select Buyer", "info");
                }
                if (s.cpAlertMessage == 'SelectFile') {
                    swal("Information", "Please upload the security file", "info");
                }

                if (s.cpAlertMessage == 'successSave') {
                    swal("Information", "The product has been success registered!", "success");
                }
                if (s.cpAlertMessage == 'successUpdate') {
                    swal("Information", "The product has been success updated!", "success");
                }

                if (s.cpAlertMessage == 'errorSave') {
                    swal("Information", "There was an error registering the product!", "error");
                }
                if (s.cpAlertMessage == 'errorUpdate') {
                    swal("Information", "There was an error updating the product!", "error");
                }
            }
            s.cpAlertMessage = "";
        }

        function SelMN() {
            var Valores = "";
            $(".rbMN").each(function () {
                if (this.checked) {
                    if (Valores == "") {
                        Valores = this.name.substr(5)+":"+this.value;
                    } else {
                        Valores += "," + this.name.substr(5) + ":" + this.value;
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

            ASPxCallbackPanel2.PerformCallback(Valores);
        }

        function bs_input_file() {
            $(".input-file").before(
                function () {
                    if (!$(this).prev().hasClass('input-ghost')) {
                        var element = $("<input type='file' class='input-ghost' style='visibility:hidden; height:0'>");
                        element.attr("name", $(this).attr("name"));
                        element.change(function () {
                            element.next(element).find('input').val((element.val()).split('\\').pop());
                        });
                        $(this).find("button.btn-choose").click(function () {
                            element.click();
                        });
                        $(this).find("button.btn-reset").click(function () {
                            element.val(null);
                            $(this).parents(".input-file").find('input').val('');
                        });
                        $(this).find('input').css("cursor", "pointer");
                        $(this).find('input').mousedown(function () {
                            $(this).parents('.input-file').prev().click();
                            return false;
                        });
                        return element;
                    }
                }
            );
        }
        $(function () {
            bs_input_file();
        });

        function CIuplGraphicsFile_OnFileUploadComplete(s, e) {
            if (e.isValid) {
                console.log(e.callbackData);

                MainContent_ASPxCallbackPanel2_txtFile.value = e.callbackData;

                //cbSaveVars.PerformCallback(e.callbackData);

                //var params = "SaveWithFile";
                //xgrdPartes.PerformCallback(params);

                //HabilitaCambio();

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
        h3 {
            line-height: 15px;
           }

    </style>
    <div class="CeroPM" id="Inicio">
    <div class="container-fluid CeroPM">
      <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel2" Width="100%"
        ClientInstanceName="ASPxCallbackPanel2" RenderMode="Table" 
        OnCallback="ASPxCallbackPanel2_Callback">
          <ClientSideEvents EndCallback="ASPxCallbackPanel2_EndCallback"/>
          <PanelCollection>
              <dx:PanelContent>
        <div id="divBotones" class="col-xs-12 btn-group CeroPM" data-spy="affix" data-offset-top="100" style="color:white;width:100%;z-index:1 !important;background-color:#EFEFEF;">
            <img ID="btnSave" class="btn" Style="margin: 0px; padding: 0px;" onclick="ASPxCallbackPanel2.PerformCallback('Save');" src="Assets/images/BtnGuardar.png" /><%--OnClick="btnSave_Click"--%>
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
                        <h2 class="HeaderSpanNombreFormulario">SOLICITUD DE PROCESO MRO</h2>
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
                                <asp:TextBox ID="lblnDoc" runat="server" style="background-color: transparent !important;" Enabled="false"></asp:TextBox>
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
                                <label class="text-form col-sm-2">Operación</label>
                                <div class="col-sm-3">
                                    <asp:RadioButton ID="rbAlta" runat="server" GroupName="operacion" Text="&nbsp;Alta" onchange="ASPxCallbackPanel2.PerformCallback('rbAlta');" />&nbsp;
                                    <asp:RadioButton ID="rbModificacion" runat="server" GroupName="operacion" Text="&nbsp;Modificación" onchange="ASPxCallbackPanel2.PerformCallback('rbModificacion');" />&nbsp;
                                    <asp:RadioButton ID="rbBaja" runat="server" GroupName="operacion" Text="&nbsp;Baja" onchange="ASPxCallbackPanel2.PerformCallback('rbBaja');" />
                                </div>
                                <label id="lblProd" runat="server" class="text-form col-sm-2">Selecciona Producto</label>
                                <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbProducto" runat="server" IncrementalFilteringMode="Contains" 
                                    FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                    PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                    <ValidationSettings>
                                        <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                    </ValidationSettings>
                                    <ClientSideEvents/>
                                    <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                </dx:ASPxComboBox>
                            </div>
                            <div id="dvReemplazaOtro" runat="server" class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Reemplaza a otro</label>
                                <div class="col-sm-2">
                                    <asp:RadioButton ID="rbSi" runat="server" GroupName="Reemplaza" Text="&nbsp;Si" onchange="ASPxCallbackPanel2.PerformCallback('rbSi');"/>&nbsp;
                                    <asp:RadioButton ID="rbNo" runat="server" GroupName="Reemplaza" Text="&nbsp;No"  onchange="ASPxCallbackPanel2.PerformCallback('rbNo');"/>&nbsp;
                                    <%--<asp:TextBox ID="remplazaOtro" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>--%>
                                </div>
                                <label id="lblcual" runat="server" class="text-form col-sm-2">Cuál artículo</label>
                                <div class="col-sm-6">
                                    <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbCualArticulo" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                    <%--<asp:TextBox ID="cualArticulo" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>--%>
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

<%--                            <img src="Assets/Images/New.png" alt="add without file" title="add" style="cursor: pointer;" onclick="openModal('TipoArticulo');" />
                            <img class="img" src="Assets/Images/trash_can.png" alt="Remove Selected" style="cursor: pointer" onclick="return DisableSelectedTipoArt();" />
                            <img class="img" src="Assets/Images/delete_all.png" alt="Remove Selected" style="cursor: pointer" onclick="return xgrdTipoArticulo.PerformCallback('Delete');" />--%>
                            <!--***************************TIPOS ARTICULO*********************************************************-->
                            <dx:ASPxGridView ID="xgrdTipoArticulo" runat="server" AutoGenerateColumns="true"
                                Width="100%" Font-Names="Segoe UI"
                                OnCustomCallback="xgrdTipoArticulo_CustomCallback"
                                OnHtmlDataCellPrepared="xgrdTipoArticulo_HtmlDataCellPrepared"
                                ClientInstanceName="xgrdTipoArticulo" Theme="Metropolis">
                                <Columns>
                                    <dx:GridViewDataTextColumn Name="CheckID" Visible="false" VisibleIndex="0" Width="10px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="tipoArticulo" Caption="Tipo de Artículo" VisibleIndex="1" Width="35%">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Name="M" Caption="M" VisibleIndex="2" Width="10%">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Name="N" Caption="N" VisibleIndex="3" Width="10%">
                                        <DataItemTemplate>
                                            <table style="width:100%">
                                                <tr>
                                                    <td>
                                                        <%# Eval("N") %> 
                                                    </td>
                                                    <td style="width:10%">
                                                        <input type="radio" name='rbMN<%# Eval("ctrlPTipoArticuloID") %>'  />
                                                    </td>
                                                </tr>
                                            </table>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="comentarios" Caption="Comentarios / Justificaciones" VisibleIndex="4" Width="35%">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles>
                                    <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                                    <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                                    <Header BackColor="#F2F2F2"></Header>
                                </Styles>
                                <SettingsPager Mode="ShowPager" PageSize="20" />
                                <Settings ShowFilterRow="True" />
                                <ClientSideEvents EndCallback="OnTipoArticuloEndCallback" />
                            </dx:ASPxGridView>

<%--                            <div class="row form-group CeroPM">
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
                                            </tr>
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
                            </div>--%>

                            <div id="mdlTipoArticulo" class="modal fade" role="dialog">
                                <div class="modal-dialog" runat="server">
                                    <div class="modal-content" runat="server">
                                        <div class="modal-header" runat="server">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4>Tipo Articulo</h4>
                                        </div>
                                        <div class="modal-body" runat="server">
                                            <dx:ASPxGridView ID="xgrdTipoArticuloMDL" runat="server" AutoGenerateColumns="true"
                                                Width="100%" Font-Names="Segoe UI"
                                                OnCustomCallback="xgrdTipoArticuloMDL_CustomCallback"
                                                OnHtmlDataCellPrepared="xgrdTipoArticuloMDL_HtmlDataCellPrepared"
                                                ClientInstanceName="xgrdTipoArticuloMDL" Theme="Metropolis">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="0" Width="10px">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="tipoArticulo" Caption="Tipo de Artículo" VisibleIndex="1" Width="35%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="M" Caption="M" VisibleIndex="2" Width="10%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="N" Caption="N" VisibleIndex="3" Width="10%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="comentarios" Caption="Comentarios / Justificaciones" VisibleIndex="4" Width="35%">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Styles>
                                                    <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                                                    <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                                                    <Header BackColor="#F2F2F2"></Header>
                                                </Styles>
                                                <SettingsPager Mode="ShowPager" PageSize="20" />
                                                <Settings ShowFilterRow="True" />
                                                <ClientSideEvents EndCallback="OnTipoArticuloMDLEndCallback" />
                                            </dx:ASPxGridView>
                                            <br />
                                            <div>
                                                <button type="button" class="btn btn-primary" onclick="AddedTiposArticulos();" data-dismiss="modal">Add</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!--**************************************************************************************************-->
                            <br/>
                            <div class="alert alert-success center-block" style="text-align:center;padding:5px; width:50%;margin:auto;">
                                <strong>Importante!</strong> Artículo con referencia <a href="#" class="alert-link"><asp:label id="lblstock" runat="server" Text="M"></asp:label></a>: Con Stock.
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
                                    <asp:TextBox ID="txtCantidad" onChange="recalcular()"  TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
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
                                    <asp:TextBox ID="txtPrecioU" onChange="recalcular()" TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
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
<%--                            <img src="Assets/Images/New.png" alt="add without file" title="add" style="cursor: pointer;" onclick="openModal('Mantenimiento');" />
                            <img class="img" src="Assets/Images/trash_can.png" alt="Remove Selected" style="cursor: pointer" onclick="return DisableSelectedMtto();" />
                            <img class="img" src="Assets/Images/delete_all.png" alt="Remove Selected" style="cursor: pointer" onclick="return xgrdMtto.PerformCallback('Delete');" />--%>
                            <dx:ASPxGridView ID="xgrdMtto" runat="server" AutoGenerateColumns="true"
                                Width="100%" Font-Names="Segoe UI"
                                OnCustomCallback="xgrdMtto_CustomCallback"
                                OnHtmlDataCellPrepared="xgrdMtto_HtmlDataCellPrepared"
                                ClientInstanceName="xgrdMtto" Theme="Metropolis">
                                <Columns>
                                    <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="0" Width="10px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="especificacion" Caption="Especificación" VisibleIndex="1" Width="23%">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="notas" Caption="Notas" VisibleIndex="2" Width="22%">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="clasificacion" Caption="Clasificación" VisibleIndex="3" Width="22%">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="responsable" Caption="Responsable" VisibleIndex="4" Width="23%">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles>
                                    <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                                    <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                                    <Header BackColor="#F2F2F2"></Header>
                                </Styles>
                                <SettingsPager Mode="ShowPager" PageSize="20" />
                                <Settings ShowFilterRow="True" />
                                <ClientSideEvents EndCallback="OnMttoEndCallback" />
                            </dx:ASPxGridView>
<%--                        <div class="panel-body">
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
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <br />
                        </div>--%>

                            <div id="mdlMtto" class="modal fade" role="dialog">
                                <div class="modal-dialog" runat="server">
                                    <div class="modal-content" runat="server">
                                        <div class="modal-header" runat="server">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4>Mantenimiento</h4>
                                        </div>
                                        <div class="modal-body" runat="server">
                                            <dx:ASPxGridView ID="xgrdMttoMDL" runat="server" AutoGenerateColumns="true"
                                                Width="100%" Font-Names="Segoe UI"
                                                OnCustomCallback="xgrdMttoMDL_CustomCallback"
                                                OnHtmlDataCellPrepared="xgrdMttoMDL_HtmlDataCellPrepared"
                                                ClientInstanceName="xgrdMttoMDL" Theme="Metropolis">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="0" Width="10px">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="especificacion" Caption="Especificación" VisibleIndex="1" Width="23%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="notas" Caption="Notas" VisibleIndex="2" Width="22%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="clasificacion" Caption="Clasificación" VisibleIndex="3" Width="22%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="responsable" Caption="Responsable" VisibleIndex="4" Width="23%">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Styles>
                                                    <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                                                    <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                                                    <Header BackColor="#F2F2F2"></Header>
                                                </Styles>
                                                <SettingsPager Mode="ShowPager" PageSize="20" />
                                                <Settings ShowFilterRow="True" />
                                                <ClientSideEvents EndCallback="OnMttoxgrdMttoMDLEndCallback" />
                                            </dx:ASPxGridView>
                                            <br />
                                            <div>
                                                <button type="button" class="btn btn-primary" onclick="AddedMttos();" data-dismiss="modal">Add</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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
<%--                            <img src="Assets/Images/New.png" alt="add without file" title="add" style="cursor: pointer;" onclick="openModal('Almacen');" />
                            <img class="img" src="Assets/Images/trash_can.png" alt="Remove Selected" style="cursor: pointer" onclick="return DisableSelectedAlmacen();" />
                            <img class="img" src="Assets/Images/delete_all.png" alt="Remove Selected" style="cursor: pointer" onclick="return xgrdAlmacen.PerformCallback('Delete');" />--%>
                            <dx:ASPxGridView ID="xgrdAlmacen" runat="server" AutoGenerateColumns="true"
                                Width="100%" Font-Names="Segoe UI"
                                OnCustomCallback="xgrdAlmacen_CustomCallback"
                                OnHtmlDataCellPrepared="xgrdAlmacen_HtmlDataCellPrepared"
                                ClientInstanceName="xgrdAlmacen" Theme="Metropolis">
                                <Columns>
                                    <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="0" Width="10px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="especificacion" Caption="Especificación" VisibleIndex="1" Width="23%">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="notas" Caption="Notas" VisibleIndex="2" Width="22%">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="clasificacion" Caption="Clasificación" VisibleIndex="3" Width="22%">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="responsable" Caption="Responsable" VisibleIndex="4" Width="23%">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles>
                                    <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                                    <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                                    <Header BackColor="#F2F2F2"></Header>
                                </Styles>
                                <SettingsPager Mode="ShowPager" PageSize="20" />
                                <Settings ShowFilterRow="True" />
                                <ClientSideEvents EndCallback="OnAlmacenEndCallback" />
                            </dx:ASPxGridView>
<%--                        <div class="panel-body">
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
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <br />
                        </div>--%>
                            <div id="mdlAlmacen" class="modal fade" role="dialog">
                                <div class="modal-dialog" runat="server">
                                    <div class="modal-content" runat="server">
                                        <div class="modal-header" runat="server">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4>Almacén</h4>
                                        </div>
                                        <div class="modal-body" runat="server">
                                            <dx:ASPxGridView ID="xgrdAlmacenMDL" runat="server" AutoGenerateColumns="true"
                                                Width="100%" Font-Names="Segoe UI"
                                                OnCustomCallback="xgrdAlmacenMDL_CustomCallback"
                                                OnHtmlDataCellPrepared="xgrdAlmacenMDL_HtmlDataCellPrepared"
                                                ClientInstanceName="xgrdAlmacenMDL" Theme="Metropolis">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="0" Width="10px">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="especificacion" Caption="Especificación" VisibleIndex="1" Width="23%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="notas" Caption="Notas" VisibleIndex="2" Width="22%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="clasificacion" Caption="Clasificación" VisibleIndex="3" Width="22%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="responsable" Caption="Responsable" VisibleIndex="4" Width="23%">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Styles>
                                                    <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                                                    <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                                                    <Header BackColor="#F2F2F2"></Header>
                                                </Styles>
                                                <SettingsPager Mode="ShowPager" PageSize="20" />
                                                <Settings ShowFilterRow="True" />
                                                <ClientSideEvents EndCallback="OnAlmacenMDLEndCallback" />
                                            </dx:ASPxGridView>
                                            <br />
                                            <div>
                                                <button type="button" class="btn btn-primary" onclick="AddedAlmnes();" data-dismiss="modal">Add</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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
                                    <asp:RadioButton ID="rbFichaSi" runat="server" GroupName="FichaSeguridad" Text="&nbsp;Si" onchange="ASPxCallbackPanel2.PerformCallback('rbFichaSegSi');"/>&nbsp;
                                    <asp:RadioButton ID="rbFichaNo" runat="server" GroupName="FichaSeguridad" Text="&nbsp;No" onchange="ASPxCallbackPanel2.PerformCallback('rbFichaSegNo');"/>&nbsp;
                                    <%--<asp:TextBox ID="txtFichaSeguridad" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>--%>
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
                                    <%--<asp:TextBox ID="txtConteoCiclico" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>--%>
                                    <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbConteoCiclico" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-3">Alacenamiento externo posbible</label>
                                <div class="col-sm-3">
                                    <asp:RadioButton ID="rbAlmExtSi" runat="server" GroupName="AlmaExt" Text="&nbsp;Si" />&nbsp;
                                    <asp:RadioButton ID="rbAlmExtNo" runat="server" GroupName="AlmaExt" Text="&nbsp;No"/>&nbsp;
                                    <%--<asp:TextBox ID="txtAlmacenamientoExt" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>--%>
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
                                <label class="text-form col-sm-3">Fecha de inventario</label>
                                <div class="col-sm-3">
                                    <dx:ASPxDateEdit ID="xDateFechaInv" runat="server" Width="200px"  CssClass="form-control input-sm Campos"
                                        DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd">                                                             
                                        <TimeSectionProperties>
                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                        </TimeSectionProperties>
                                    </dx:ASPxDateEdit> 
                                </div>
                                <label class="text-form col-sm-3">Múltiplo</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtMultiplo" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div id="dvHojaSeg1" runat="server" class="row form-group CeroPM">
                               <label class="text-form col-sm-3">Hoja de seguridad</label>
                               <div class="col-sm-3">
                                    <dx:ASPxUploadControl ID="uplGraphicsFile" runat="server" Theme="SoftOrange" 
                                        ClientInstanceName="CIuplGraphicsFile" ShowProgressPanel="True"
                                        NullText="Click here to browse files..." Size="35"
                                        OnFileUploadComplete="CIuplGraphicsFile_FileUploadComplete" CssClass="labelGral"
                                        Width="100%">
                                        <ClientSideEvents 
                                            FileUploadComplete="function(s, e) { CIuplGraphicsFile_OnFileUploadComplete(s,e); }"
                                                FilesUploadComplete="function(s, e) { CIuplGraphicsFile_OnFilesUploadComplete(e); }"
                                            FileUploadStart="function(s, e) { CIuplGraphicsFile_OnUploadStart(); }"
                                            TextChanged="function(s, e) { UpdateUploadGraphicsFileButton(); }">                                                                            
                                        </ClientSideEvents>
                                        <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".pdf, .doc, .png, .jpg, .xlsx">                                                                            
                                        </ValidationSettings>
                                        <ButtonStyle CssClass="labelGral" Font-Size="10pt"></ButtonStyle>
                                    </dx:ASPxUploadControl>
                                </div>
                                <div class="col-sm-3">
                                    <dx:ASPxButton ID="btnUploadGraphicsFile" runat="server" AutoPostBack="False" Theme="SoftOrange" 
                                        Text="Upload File" ClientInstanceName="btnUploadGraphicsFile"
                                        Width="100px" ClientEnabled="False">
                                        <ClientSideEvents Click="function(s, e) { CIuplGraphicsFile.Upload(); }" />
                                    </dx:ASPxButton>
                               </div>
                            </div>
                            <div id="dvHojaSeg2" runat="server" class="row form-group CeroPM">
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtFile" Width="100%" Enabled="false" runat="server"></asp:TextBox>
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
                                <label class="text-form col-sm-1"><span class="label label-success pull-right" style="padding:6px;"><asp:label id="lblstock2" runat="server" Text="M"></asp:label></span></label>
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
                    <img src="Assets/Images/New.png" alt="add without file" title="add" style="cursor: pointer;" onclick="openModal('Aprobaciones');" />
                    <img class="img" src="Assets/Images/trash_can.png" alt="Remove Selected" style="cursor: pointer" onclick="return DisableSelectedAprobacion();" />
                    <img class="img" src="Assets/Images/delete_all.png" alt="Remove Selected" style="cursor: pointer" onclick="return xgrdAprobaciones.PerformCallback('Delete');" />
                    <dx:ASPxGridView ID="xgrdAprobaciones" runat="server" AutoGenerateColumns="true"
                        Width="100%" Font-Names="Segoe UI"
                        OnCustomCallback="xgrdAprobaciones_CustomCallback"
                        OnHtmlDataCellPrepared="xgrdAprobaciones_HtmlDataCellPrepared"
                        ClientInstanceName="xgrdAprobaciones" Theme="Metropolis">
                        <Columns>
                            <dx:GridViewDataTextColumn Name="CheckID" VisibleIndex="0" Width="10px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="paso" Caption="Paso" VisibleIndex="1" Width="12%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="titulo" Caption="Título" VisibleIndex="2" Width="12%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="usuario" Caption="Usuario" VisibleIndex="3" Width="11%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="puesto" Caption="Puesto" VisibleIndex="4" Width="11%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="fechaNotificacion" Caption="Fecha de notificación" VisibleIndex="5" Width="11%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="fechaAccion" Caption="Fecha de acción" VisibleIndex="6" Width="11%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="accion" Caption="Acción" VisibleIndex="7" Width="11%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="comentario" Caption="Comentario" VisibleIndex="8" Width="11%">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                            <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                            <Header BackColor="#F2F2F2"></Header>
                        </Styles>
                        <SettingsPager Mode="ShowPager" PageSize="20" />
                        <Settings ShowFilterRow="True" />
                        <ClientSideEvents EndCallback="OnAprobacionesEndCallback" />
                    </dx:ASPxGridView>
<%--                    <table id="table"
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
                            </tr>
                        </tbody>
                    </table>--%>

                            <div id="mdlAprobaciones" class="modal fade" role="dialog">
                                <div class="modal-dialog" runat="server">
                                    <div class="modal-content" runat="server">
                                        <div class="modal-header" runat="server">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4>Aprobación</h4>
                                        </div>
                                        <div class="modal-body" runat="server">
                                            <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel1" Width="100%"
                                                ClientInstanceName="CallbackPanel" RenderMode="Table"
                                                OnCallback="ASPxCallbackPanel1_Callback">
                                                <ClientSideEvents EndCallback="OnPanel1EndCallback" />
                                                <PanelCollection>
                                                    <dx:PanelContent ID="PanelContent3" runat="server">
                                                        <div class="row form-group CeroPM">
                                                            <label class="text-form col-sm-2">Codigo</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtCodigoAprobacionAdd" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row form-group CeroPM">
                                                            <label class="text-form col-sm-2">Paso</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtPasoAdd" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                                            </div>
                                                            <label class="text-form col-sm-2">Título</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtTituloAdd" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row form-group CeroPM">
                                                            <label class="text-form col-sm-2">Puesto</label>
                                                            <div class="col-sm-4">
                                                                <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbPuestoAdd" runat="server" IncrementalFilteringMode="Contains" 
                                                                    FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" 
                                                                    PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                                                    <ValidationSettings>
                                                                        <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) { 
                                                                                CallbackPanel.PerformCallback('filterPuesto,'+s.lastSuccessValue.toString());}" />
                                                                    <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <label class="text-form col-sm-2">Empleado</label>
                                                            <div class="col-sm-4">
                                                                <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbEmpleadoAdd" runat="server" IncrementalFilteringMode="Contains" 
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
                                                            <label class="text-form col-sm-2">Fecha Notificación</label>
                                                            <div class="col-sm-4">
                                                                <dx:ASPxDateEdit ID="xDateFechaNotAdd" runat="server" CssClass="form-control input-sm Campos"
                                                                    DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd">                                                             
                                                                    <TimeSectionProperties>
                                                                        <TimeEditProperties EditFormatString="hh:mm tt" />
                                                                    </TimeSectionProperties>
                                                                </dx:ASPxDateEdit>                                                     
                                                            </div>
                                                            <label class="text-form col-sm-2">Fecha Acción</label>
                                                            <div class="col-sm-4">
                                                                <dx:ASPxDateEdit ID="xDateFechaAccionAdd" runat="server" CssClass="form-control input-sm Campos"
                                                                    DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd">                                                             
                                                                    <TimeSectionProperties>
                                                                        <TimeEditProperties EditFormatString="hh:mm tt" />
                                                                    </TimeSectionProperties>
                                                                </dx:ASPxDateEdit>
                                                            </div>
                                                        </div>
                                                        <div class="row form-group CeroPM">
                                                            <label class="text-form col-sm-2">Acción</label>
                                                            <div class="col-sm-4">
                                                                 <asp:TextBox ID="txtAccionAdd" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>                                                 
                                                            </div>
                                                            <label class="text-form col-sm-2">Comentario</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtComentarioAdd" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>  
                                                            </div>
                                                        </div>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxCallbackPanel>
                                            <br />
                                            <div>
                                                <button type="button" class="btn btn-primary" onclick="xgrdAprobaciones.PerformCallback('Save');" data-dismiss="modal">Add</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                </div>
                <div id="menu1" class="tab-pane fade">
                    <h3>Comentarios Adicionales</h3>
                    <p><asp:TextBox ID="txtComentarios" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox></p>
                </div>
            </div>
        </div>
              </dx:PanelContent>
          </PanelCollection>
      </dx:ASPxCallbackPanel>
    </div>
    </div>
</asp:Content>
