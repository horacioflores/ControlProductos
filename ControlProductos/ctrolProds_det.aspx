<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ctrolProds_det.aspx.cs" Inherits="ControlProductos.ctrolProds_det" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server" Visible="false">
<%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">--%>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<%--<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>--%>
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

       function OnArchivosEndCallback(s, e) {
           if (s.cpAlertMessage != '') {
               if (s.cpAlertMessage == 'UploadFile') {
                   swal("Information", "Please upload the file", "info");
               } else if (s.cpAlertMessage == 'NotDescripcion') {
                   swal("Information", "Description missing", "info");
               }else if (s.cpAlertMessage == 'Exist') {
                   swal({
                       title: "Information",
                       text: "File already exist",
                       type: "info",
                       timer: 2000,
                       showConfirmButton: false
                   });
               } else if (s.cpAlertMessage == 'SussAdd') {
                   document.getElementById("MainContent_ASPxCallbackPanel2_txtdescripcionArchivo").value = "";
                   document.getElementById("MainContent_ASPxCallbackPanel2_txtFileArchivo").value = "";
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

       function selAprob(perfil, empleado) {
           var valores = perfil + "," + empleado;
           xgrdAprobaciones.PerformCallback(valores);
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
           cantidad = parseFloat(document.getElementById("MainContent_ASPxCallbackPanel2_txtConsEstimado").value);
           precio = parseFloat(document.getElementById("MainContent_ASPxCallbackPanel2_txtPrecioU").value);
           document.getElementById("MainContent_ASPxCallbackPanel2_txtCantidad").value = cantidad * precio;

           recalcularReorden();
       }

       function recalcularReorden() {
           consumo = parseFloat(document.getElementById("MainContent_ASPxCallbackPanel2_txtConsEstimado").value);
           te = parseFloat(document.getElementById("MainContent_ASPxCallbackPanel2_txtDiasEntrega").value);
           stock = parseFloat(document.getElementById("MainContent_ASPxCallbackPanel2_cmbDias").value);
           
           part1 = (consumo / 365) * te;
           part2 = (consumo / 365) * te;
           document.getElementById("MainContent_ASPxCallbackPanel2_txtStockMin").value = part1 + part2;
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
                        $('.btnSave').click();
                    }
                    if (e.altKey && ((e.which == 50) || (e.which == 98))) {//2
                        $('.btnEnviarSolicitante').click();
                    }
                    if (e.altKey && ((e.which == 51) || (e.which == 99))) {//3
                        $('.btnAsignAutor').click();
                    }
                    if (e.altKey && ((e.which == 52) || (e.which == 100))) {//4
                        $('.btnRechazar').click();
                    }
                    if (e.altKey && ((e.which == 53) || (e.which == 101))) {//5
                        $('.btnEnviarDM').click();
                    }
                    if (e.altKey && ((e.which == 54) || (e.which == 102))) {//6
                        $('.btnRegresar').click();
                    }
                    if (e.altKey && ((e.which == 55) || (e.which == 103))) {//7
                        // Pendiente
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

        function openModal(opcion) {
            switch (opcion) {
                case 'comentarios':
                    $('#mdlComentarios').modal('show');
                    break;
                case 'archivos':
                    $('#mdlArchivo').modal('show');
                    break;
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
                if (s.cpAlertMessage == 'changeUser') {
                    swal("Information", "You must change the author user of the approvers", "info");
                }
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
                if (s.cpAlertMessage == 'Selectdescripcion') {
                    swal("Information", "Description is missing", "info");
                }
                if (s.cpAlertMessage == 'SelectModelo') {
                    swal("Information", "Modelo is missing", "info");
                }
                if (s.cpAlertMessage == 'SelectFiles') {
                    swal("Information", "You must attach at least one file", "info");
                }
                if (s.cpAlertMessage == 'SelectFuncionMaquina') {
                    swal("Information", "Machine function is missing", "info");
                }
                if (s.cpAlertMessage == 'SelectAccionContencion') {
                    swal("Information", "Containment action missing on Inventory 0", "info");
                }
                if (s.cpAlertMessage == 'SelectContrato') {
                    swal("Information", "Contract number is missing", "info");
                }
                if (s.cpAlertMessage == 'Selectdescripcion1') {
                    swal("Information", "Description 1 is missing", "info");
                }
                if (s.cpAlertMessage == 'Selectdescripcion2') {
                    swal("Information", "Description 2 is missing", "info");
                }
                if (s.cpAlertMessage == 'Selectdescripcionlarga') {
                    swal("Information", "Long description is missing", "info");
                }
                if (s.cpAlertMessage == 'SelectGLClass') {
                    swal("Information", "GL Class is missing", "info");
                }
                if (s.cpAlertMessage == 'SelectGLClass') {
                    swal("Information", "GL Class is missing", "info");
                }
                if (s.cpAlertMessage == 'SelectTextBusq') {
                    swal("Information", "Search text is missing", "info");
                }
                if (s.cpAlertMessage == 'SelectPursh1') {
                    swal("Information", "Pursh 1 is missing", "info");
                }
                if (s.cpAlertMessage == 'SelectPursh2') {
                    swal("Information", "Pursh 2 is missing", "info");
                }
                if (s.cpAlertMessage == 'SelectPlaneador') {
                    swal("Information", "Glider missing", "info");
                }
                if (s.cpAlertMessage == 'SelectUbicacionPrim') {
                    swal("Information", "Primary location is missing", "info");
                }
                if (s.cpAlertMessage == 'SelectUbicacionSec') {
                    swal("Information", "Secondary location is missing", "info");
                }
                if (s.cpAlertMessage == 'SelectCodArt') {
                    swal("Information", "Part number missing", "info");
                }
                if (s.cpAlertMessage == 'exists') {
                    swal("Information", "The part number already exists", "info");
                }
                if (s.cpAlertMessage == 'exists2') {
                    swal("Information", "Another application with this part number is in the approval process", "info");
                }

                if (s.cpAlertMessage == 'successSave') {
                    swal("Information", "The product has been success registered!", "success");
                }
                if (s.cpAlertMessage == 'successUpdate') {
                    swal("Information", "The product has been success updated!", "success");
                }

                if (s.cpAlertMessage == 'EnvSucces') {
                    swal("Information", "The request has been sent successfully!", "success");
                }
                if (s.cpAlertMessage == 'EnvFaild') {
                    swal("Information", "There was an error submitting the request!", "success");
                }
                if (s.cpAlertMessage == 'errorAprbnes') {
                    swal("Information", "Error: Approver list is not missing", "error");
                }

                if (s.cpAlertMessage == 'errorSave') {
                    swal("Information", "There was an error registering the product!", "error");
                }
                if (s.cpAlertMessage == 'errorUpdate') {
                    swal("Information", "There was an error updating the product!", "error");
                }

                if (s.cpAlertMessage == 'RechSucces') {
                    swal("Information", "The request has been rejected successfully!", "success");
                }
                if (s.cpAlertMessage == 'EnvFaild') {
                    swal("Information", "There was an error rejecting the request!", "success");
                }
            }
            s.cpAlertMessage = "";
        }

        function PrecargaProducto(valor) {
            var operacion = "";
            if (document.getElementById("MainContent_ASPxCallbackPanel2_rbModificacion").checked) {
                operacion = "MODIFICACIÓN";
            }
            else {
                operacion = "BAJA";
            }
            var Valores = "changeProd," + valor + "," + operacion;
            ASPxCallbackPanel2.PerformCallback(Valores);
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

        function CIuplGFileArchivo_OnFilesUploadComplete(args) {
            UpdateUploadGraphicsFileButton();
        }

        function CIuplGraphicsFile_OnUploadStart() {
            btnUploadGraphicsFile.SetEnabled(false);
        }

        function UpdateUploadGraphicsFileButton() {
            btnUploadGraphicsFile.SetEnabled(CIuplGraphicsFile.GetText(0) != "");
        }

        function CIuplGFileArchivo_OnFileUploadComplete(s, e) {
            if (e.isValid) {
                console.log(e.callbackData);

                MainContent_ASPxCallbackPanel2_txtFileArchivo.value = e.callbackData;

            }
            else {
                swal("Warning", "An error has occurred from the database");
            }
        }

        function CIuplGFileArchivo_OnFilesUploadComplete(args) {
            UpdateUploadGFileArchivoButton();
        }

        function CIuplGFileArchivo_OnUploadStart() {
            btnUploadGFileArchivo.SetEnabled(false);
        }

        function UpdateUploadGFileArchivoButton() {
            btnUploadGFileArchivo.SetEnabled(CIuplGFileArchivo.GetText(0) != "");
        }

        function AddedArchivos() {
            xgrdArchivos.PerformCallback('Add');
        }

        function DisableSelectedArchivo() {

            var Valores = "";
            $(".chkArchivo").each(function () {
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

            xgrdArchivos.PerformCallback(Valores);

        }
        function saveComment(tipoArticulo, commentario) {
            console.log(tipoArticulo);
            console.log(commentario);
            var Valores = "saveComment," + tipoArticulo + "," + commentario;
            xgrdTipoArticulo.PerformCallback(Valores);
        }

        function selEnviar(tipo) {
            if (tipo == 'Enviar') {
                document.getElementById("MainContent_ASPxCallbackPanel2_txttipo").value = "Enviar";
                document.getElementById("MainContent_ASPxCallbackPanel2_txtAprobar").value = "1";
            }
            if (tipo == 'EnviarDM') {
                document.getElementById("MainContent_ASPxCallbackPanel2_txttipo").value = "EnviarDM";
                document.getElementById("MainContent_ASPxCallbackPanel2_txtAprobar").value = "1";
            }
            if (tipo == 'Rechazar') {
                document.getElementById("MainContent_ASPxCallbackPanel2_txttipo").value = "Rechazar";
                document.getElementById("MainContent_ASPxCallbackPanel2_txtAprobar").value = "0";
            }
            openModal('comentarios');
        }

        function enviar() {
            var Valores = "";
            var aprobacion = document.getElementById("MainContent_ASPxCallbackPanel2_txtAprobar").value;
            var tipo = document.getElementById("MainContent_ASPxCallbackPanel2_txttipo").value;
            var comentarios = document.getElementById("MainContent_ASPxCallbackPanel2_comentariosEnviar").value;

            if (comentarios == '') {
                swal("Information", "Comments are missing", "info");
            } else {

                Valores = tipo + "," + aprobacion + "," + comentarios;
                ASPxCallbackPanel2.PerformCallback(Valores);
            }
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

    /*Ajusta campos de acuerdo a la resolución*/
    @media only screen and (max-width: 450px) {.Campos {width:300px;}}
    @media(min-width:451px){.Campos {width:410px;}}
    @media(min-width:650px){.Campos {width:550px;}}
    @media(min-width:768px){
        .Campos {width:200px;}
        .Campos6Cols {width:150px;}
    }
    @media(min-width:1001px){
        .Campos {width:280px;}
        .Campos6Cols {width:200px;}
    }
    @media(min-width:1350px){
        .Campos {width:400px;}
        .Campos6Cols {width:290px;}
    }
    @media(min-width:1700px){
        .Campos {width:510px;}
        .Campos6Cols {width:370px;}
    }

    .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
        color: #31708f !important;
        background-color: #d9edf7;
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

    .form-control:focus {
        border-color: #66afe9;
        outline: 0;
        -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6);
        box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6);
    }

    .dxpnlControl {
        font: 12px 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif !important;
    }
    </style>
    <div class="CeroPM" id="Inicio">
    <div class="container-fluid CeroPM">
      <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel2" Width="100%" ClientInstanceName="ASPxCallbackPanel2" RenderMode="Table" 
        OnCallback="ASPxCallbackPanel2_Callback">
        <ClientSideEvents EndCallback="ASPxCallbackPanel2_EndCallback"/>
        <PanelCollection>
          <dx:PanelContent>
            <div id="divBotones" class="col-xs-12 btn-group CeroPM" data-spy="affix" data-offset-top="100" style="padding-top:15px; color:white;width:100%;z-index:1 !important;background-color:#EFEFEF;">
                <button type="button" runat="server" id="btnSave" class="btn btnSave" title="alt+1" Style="margin: 0px; padding: 0px; background-image: url('Assets/images/BtnGuardar.png');height:30px;width:103px;" onclick="ASPxCallbackPanel2.PerformCallback('Save');"><span class="badge">Alt+1</span></button>
                <button type="button" runat="server" id="btnEnviarSolicitante" class="btn btnEnviarSolicitante" title="alt+2" Style="margin: 0px; padding: 0px;background-image:url('Assets/images/BtnEnviar.png');height:30px;width:103px;" onclick="selEnviar('Enviar');"><span class="badge">Alt+2</span></button>
                <button type="button" runat="server" id="btnRechazar" class="btn btnRechazar" title="alt+4" Style="margin: 0px; padding: 0px;background-image:url('Assets/images/BtnRechazar.png');height:30px;width:103px;" onclick="selEnviar('Rechazar');"><span class="badge">Alt+4</span></button>
                <button type="button" runat="server" id="btnAsignAutor" class="btn btnAsignAutor" title="alt+3" Style="margin: 0px; padding: 0px;background-image:url('Assets/images/BtnAsignarAutor.png');height:30px;width:103px;" onclick="ASPxCallbackPanel2.PerformCallback('Save2');"><span class="badge">Alt+3</span></button>
                <button type="button" runat="server" id="btnEnviarDM" visible="false" class="btn btnEnviarDM" title="alt+5" Style="margin: 0px; padding: 0px;background-image:url('Assets/images/BtnAsignarAutor.png');height:30px;width:103px;" onclick="selEnviar('EnviarDM');"><span class="badge">Alt+5</span></button>
                
<%--                <img ID="btnSave" runat="server" class="btn btnSave" title="alt+1" Style="margin: 0px; padding: 0px;" onclick="ASPxCallbackPanel2.PerformCallback('Save');" src="Assets/images/BtnGuardar.png"/>
                <img ID="btnEnviarSolicitante" runat="server" class="btn btnEnviarSolicitante" title="alt+2" Style="margin: 0px; padding: 0px;" onclick="selEnviar('Enviar');" src="Assets/images/BtnEnviar.png" />
                <img ID="btnRechazar" runat="server" class="btn btnRechazar" title="alt+4" Style="margin: 0px; padding: 0px;" onclick="selEnviar('Rechazar');"  src="Assets/images/BtnRechazar.png" />
                <img ID="btnAsignAutor" runat="server" class="btn btnAsignAutor" title="alt+3" Style="margin: 0px; padding: 0px;" onclick="ASPxCallbackPanel2.PerformCallback('Save2');" src="Assets/images/BtnAsignarAutor.png" />
                <img ID="btnEnviarDM" runat="server" visible="false" class="btn btnEnviarDM" title="alt+5" Style="margin: 0px; padding: 0px;" onclick="selEnviar('EnviarDM');" src="Assets/images/BtnEnviarDM.png" />--%>
                <asp:ImageButton ID="btnRegresar" class="btn btnRegresar" title="alt+6" Style="margin: 0px; padding: 0px;" runat="server" ImageUrl="~/Assets/images/BtnSalir.png" OnClick="btnRegresar_Click" />
                <div class="BtnGpoIniFin">
                    <a class="BtnIniFin" id="BtnInicio" href="#Inicio" title="Inicio" onmouseover="rotate('imgaRotarI');" onmouseout="rotate('imgaRotarI');"><i id="imgaRotarI" class="glyphicon glyphicon glyphicon-circle-arrow-up"></i><span class="badge badge-xs badge-info">Alt+9</span></a>
                    <a class="BtnIniFin" id="BtnFinal" href="#pieForma" title="Historial" onmouseover="rotate('imgaRotarF');" onmouseout="rotate('imgaRotarF');"><i id="imgaRotarF" class="glyphicon glyphicon glyphicon-circle-arrow-down"></i><span class="badge badge-xs badge-info">Alt+0</span></a>
                </div>
            </div>
            <div id="mdlComentarios" class="modal fade" role="dialog">
                <div class="modal-dialog" runat="server">
                    <div class="modal-content" runat="server">
                        <div class="modal-header" runat="server">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4>Comentarios</h4>
                            <asp:TextBox ID="txtAprobar" runat="server" CssClass="form-control input-sm hidden"></asp:TextBox>
                            <asp:TextBox ID="txttipo" runat="server" CssClass="form-control input-sm hidden"></asp:TextBox>
                        </div>
                        <div class="modal-body" runat="server">
                            <div class="row">
                                <label class="text-form col-sm-3">Comentarios</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="comentariosEnviar" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div>
                                <button type="button" class="btn btn-primary" onclick="enviar();" data-dismiss="modal">Confirmar</button>
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Regresar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="jumbotron CeroPM" style="padding: 5px;">
                <div class="row CeroPM">
                <div class="col-xs-12">
                    <div class="col-xs-12 col-sm-8 form-group BGFormularioTitulo">
                        <h2 class="HeaderSpanNombreFormulario">SOLICITUD DE PROCESO MRO</h2>
                        <asp:Label ID="lblcodigoSts" runat="server" CssClass="hidden"/>
                        <asp:Label ID="lblSigPerf" runat="server" CssClass="hidden"/>
                        <asp:Literal ID="ltlSts" runat="server"></asp:Literal>
                    </div>
                    <div class="col-xs-12 col-sm-4 CeroPM" style="padding-top:3px;">
                        <div class="row form-group RowsControl">
                            <label class="text-form col-xs-4" style="padding: 1px;margin: 0px;">No. Documento</label>
                            <div class="col-xs-4 " style="color:red; font-weight:800;">
                                <asp:TextBox ID="lblnDoc" runat="server" style="background-color: transparent !important; border:0px;" Enabled="false"/>
                            </div>
                        </div>
                        <div class="row form-group RowsControl">
                            <label class="text-form col-xs-4" style="padding: 1px;margin: 0px;">Solicitante</label>
                            <div class="col-xs-8" style="color:limegreen;">
                                <asp:Label ID="lblDocSolicitante" runat="server"/>
                            </div>
                        </div>
                        <div class="row form-group RowsControl">
                            <label class="text-form col-xs-4" style="padding: 1px;margin: 0px;">Fecha de solicitud</label>
                            <div class="col-xs-8" style="color:limegreen;">
                                <asp:Label ID="lblDocFechaSol" runat="server"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <form class="container">
                <section id="sSolicitante" class="row CeroPM">
                <div class="col-xs-12" id="dvautor">
                    <div class="panel panel-info" id="infoSolicitante">
                        <div class="panel-heading">
                            <h3 class="panel-title">Información del Solicitante</h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i></span>
                            <span class="pull-right" id="btnColapsable" style="padding-right: 20px;cursor: pointer;"><i class="glyphicon glyphicon glyphicon-sort"></i><span class="badge badge-xs badge-info" style="font-size:9px;">Alt+8</span></span>
                        </div>
                        <div class="panel-body">
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Operación</label>
                                <div class="col-sm-4">
                                    <asp:RadioButton ID="rbAlta" runat="server" GroupName="operacion" Text="&nbsp;Alta" onchange="ASPxCallbackPanel2.PerformCallback('rbAlta');" />&nbsp;
                                    <asp:RadioButton ID="rbModificacion" runat="server" GroupName="operacion" Text="&nbsp;Modificación" onchange="ASPxCallbackPanel2.PerformCallback('rbModificacion');" />&nbsp;
                                    <asp:RadioButton ID="rbBaja" runat="server" GroupName="operacion" Text="&nbsp;Baja" onchange="ASPxCallbackPanel2.PerformCallback('rbBaja');" />
                                </div>
                                <label id="lblProd" runat="server" class="text-form col-sm-2">Selecciona Producto</label>
                                <div class="col-sm-4">
                                    <dx:ASPxComboBox  CssClass="form-control input-sm Campos" ID="cmbProducto" ClientInstanceName="cmbProducto" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents ValueChanged="function(s, e) { PrecargaProducto(s.GetValue()); }"/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div id="dvReemplazaOtro" runat="server" class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Reemplaza a otro</label>
                                <div class="col-sm-4">
                                    <asp:RadioButton ID="rbSi" runat="server" GroupName="Reemplaza" Text="&nbsp;Si" onchange="ASPxCallbackPanel2.PerformCallback('rbSi');"/>&nbsp;
                                    <asp:RadioButton ID="rbNo" runat="server" GroupName="Reemplaza" Text="&nbsp;No"  onchange="ASPxCallbackPanel2.PerformCallback('rbNo');"/>&nbsp;
                                    <%--<asp:TextBox ID="remplazaOtro" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>--%>
                                </div>
                                <label id="lblcual" runat="server" class="text-form col-sm-2">Cuál artículo</label>
                                <div class="col-sm-4">
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbCualArticulo" ClientInstanceName="cmbCualArticulo" runat="server" IncrementalFilteringMode="Contains" 
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
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-1">Descripción</label>
                                <div class="col-sm-11">
                                    <asp:TextBox ID="txtdescripcion" runat="server" style="width:100%" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Marca</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnCodigoMarca" value='<%# Eval("CodigoMarca")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbMarca" ClientInstanceName="cmbMarca" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true"  ReadOnlyStyle-ForeColor="DarkGray">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Modelo</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtModelo" runat="server" style="width:100%" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <br/>
                                <h5>ARCHIVOS ADJUNTOS</h5>
                                <img id="addArchAd" runat="server" src="Assets/Images/New.png" alt="add without file" title="add" style="cursor: pointer;" onclick="openModal('archivos');" />
                                <img id="delArchAd" runat="server"  class="img" src="Assets/Images/trash_can.png" alt="Remove Selected" style="cursor: pointer" onclick="return DisableSelectedArchivo();" />
                                <img id="delAllArchAd" runat="server" class="img" src="Assets/Images/delete_all.png" alt="Remove Selected" style="cursor: pointer" onclick="return xgrdArchivos.PerformCallback('Delete');" />
                                <dx:ASPxGridView ID="xgrdArchivos" runat="server" AutoGenerateColumns="true"
                                    Width="100%" Font-Names="Segoe UI"
                                    OnCustomCallback="xgrdArchivos_CustomCallback"
                                    OnHtmlDataCellPrepared="xgrdArchivos_HtmlDataCellPrepared"
                                    ClientInstanceName="xgrdArchivos" Theme="Metropolis">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Name="ctrlPArchivosID" VisibleIndex="0" Width="10px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TipoDocumento" Caption="Tipo" VisibleIndex="1" Width="33%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="descripcion" Caption="Descripción" VisibleIndex="2" Width="34%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="archivo" Caption="Archivo" VisibleIndex="3" Width="33%">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Styles>
                                        <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                                        <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                                        <Header BackColor="#F2F2F2" HorizontalAlign="Center" Font-Bold="true" CssClass="text-center"></Header>
                                    </Styles>
                                    <SettingsPager Mode="ShowPager" PageSize="20" />
                                    <Settings ShowFilterRow="True" />
                                    <ClientSideEvents EndCallback="OnArchivosEndCallback" />
                                </dx:ASPxGridView>
                                <div id="mdlArchivo" class="modal fade" role="dialog">
                                    <div class="modal-dialog" runat="server">
                                        <div class="modal-content" runat="server">
                                            <div class="modal-header" runat="server">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4>Archivo</h4>
                                            </div>
                                            <div class="modal-body" runat="server">
                                                <div class="row">
                                                    <label class="text-form col-sm-3">Tipo Documento</label>
                                                    <div class="col-sm-5">
                                                        <dx:ASPxComboBox class="form-control input-sm Campos" ID="cmbTipoDoc" ClientInstanceName="cmbTipoDoc" runat="server" IncrementalFilteringMode="Contains" 
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
                                                <div class="row">
                                                    <label class="text-form col-sm-3">Descripción</label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtdescripcionArchivo" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                   <label class="text-form col-sm-3">Archivo</label>
                                                   <div class="col-sm-5">
                                                        <dx:ASPxUploadControl ID="uplGFileArchivo" runat="server" Theme="SoftOrange" 
                                                            ClientInstanceName="CIuplGFileArchivo" ShowProgressPanel="True"
                                                            NullText="Click here to browse files..." Size="35"
                                                            OnFileUploadComplete="CIuplGFileArchivo_FileUploadComplete" CssClass="labelGral"
                                                            Width="100%">
                                                            <ClientSideEvents 
                                                                FileUploadComplete="function(s, e) { CIuplGFileArchivo_OnFileUploadComplete(s,e); }"
                                                                    FilesUploadComplete="function(s, e) { CIuplGFileArchivo_OnFilesUploadComplete(e); }"
                                                                FileUploadStart="function(s, e) { CIuplGFileArchivo_OnUploadStart(); }"
                                                                TextChanged="function(s, e) { UpdateUploadGFileArchivoButton(); }">                                                                            
                                                            </ClientSideEvents>
                                                            <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".pdf, .doc, .png, .jpg, .xlsx">                                                                            
                                                            </ValidationSettings>
                                                            <ButtonStyle CssClass="labelGral" Font-Size="10pt"></ButtonStyle>
                                                        </dx:ASPxUploadControl>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <dx:ASPxButton ID="btnUploadGFileArchivo" runat="server" AutoPostBack="False" Theme="SoftOrange" 
                                                            Text="Upload File" ClientInstanceName="btnUploadGFileArchivo"
                                                            Width="100px" ClientEnabled="False">
                                                            <ClientSideEvents Click="function(s, e) { CIuplGFileArchivo.Upload(); }" />
                                                        </dx:ASPxButton>
                                                   </div>
                                                   <div class="col-sm-3">
                                                       <asp:TextBox ID="txtFileArchivo" runat="server"  CssClass="form-control input-sm Campos" disabled="disabled"></asp:TextBox>
                                                   </div>
                                                </div>
                                                <br />
                                                <div>
                                                    <button type="button" class="btn btn-primary" onclick="AddedArchivos();" data-dismiss="modal">Add</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br/>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Departamento</label>
                                <div class="col-sm-4">
                                        <input type="hidden" id="hdnCodigoDepto" value='<%# Eval("CodigoDepto")%>' runat="server"/>
                                        <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbDepa" ClientInstanceName="cmbDepa" runat="server" IncrementalFilteringMode="Contains" 
                                            FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                            PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                            <ValidationSettings>
                                                <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                            </ValidationSettings>
                                            <ClientSideEvents/>
                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                        </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Subcuenta</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnSubcuenta" value='<%# Eval("subcuenta")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbSubcuenta" ClientInstanceName ="cmbSubcuenta" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
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
                                <label class="text-form col-sm-2">Equipos donde se utiliza (Máquina)</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnCodigoMaquina" value='<%# Eval("CodigoMaquina")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbMaquina" ClientInstanceName="cmbMaquina" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Función de la máquina</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtfuncionMaquina" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Número de Activo fijo</label>
                                <div class="col-sm-10">
                                    <input type="hidden" id="hdnActFijo" value='<%# Eval("codActFijo")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbActFijo" ClientInstanceName="cmbActFijo" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
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
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos Campos6Cols" ID="cmbSubCat1" ClientInstanceName="cmbSubCat1" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
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
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos Campos6Cols" ID="cmbSubCat2" ClientInstanceName="cmbSubCat2" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
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
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos Campos6Cols" ID="cmbSubCat3" ClientInstanceName="cmbSubCat3" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
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
                            <br/>
                            <dx:ASPxGridView ID="xgrdTipoArticulo" runat="server" AutoGenerateColumns="true" Width="100%" Font-Names="Segoe UI"
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
                                        <DataItemTemplate>
                                            <input type="text" style="width:100%;border:none;background-color:transparent;" value='<%# Eval("comentarios") %>' onchange="saveComment('<%# Eval("codigoTipoArticulo") %>',this.value)" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles>
                                    <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                                    <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                                    <Header BackColor="#F2F2F2" HorizontalAlign="Center" Font-Bold="true" CssClass="text-center"></Header>
                                </Styles>
                                <SettingsPager Mode="ShowPager" PageSize="20" />
                                <Settings ShowFilterRow="True" />
                                <ClientSideEvents EndCallback="OnTipoArticuloEndCallback" />
                            </dx:ASPxGridView>

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
                                                    <Header BackColor="#F2F2F2" HorizontalAlign="Center" Font-Bold="true" CssClass="text-center"></Header>
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
                                <label class="text-form col-sm-2">Consumo estimado anual</label>
                                <div class="col-sm-4">
                                   <asp:TextBox ID="txtConsEstimado" CssClass="form-control input-sm Campos" onChange="recalcular()" runat="server" TextMode="Number" step="0.01"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-2">Unidad de medida</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnUM" value='<%# Eval("unidadMedida")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbUM" ClientInstanceName="cmbUM" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
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
                                <label class="text-form col-sm-2">Cantidad mínima a mantener</label>
                                <div class="col-sm-4">
                                   <asp:TextBox ID="txtCantMinima" runat="server" TextMode="Number" step="0.01" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-2">Fecha requerida del artículo</label>
                                <div class="col-sm-4">
                                    <dx:ASPxDateEdit ID="xDateFechaReq" runat="server" CssClass="form-control input-sm Campos"
                                        DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua">                                                             
                                        <TimeSectionProperties>
                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                        </TimeSectionProperties>
                                    </dx:ASPxDateEdit>  
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Número de OQ/Solicitud de Cotización</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnOQ" value='<%# Eval("numOQ")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbOQ" ClientInstanceName="cmbOQ" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Acción de contención en caso de Inventario 0</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtcomoAyudarStockCero" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Precio</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtPrecio" runat="server" TextMode="Number" step="0.01" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-2">Moneda</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnCodigomoneda" value='<%# Eval("Codigomoneda")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbMoneda" ClientInstanceName="cmbMoneda" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM" style="padding-top:10px;">
                                <label class="text-form col-sm-2">Proveedor Sugerido</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnCodigoProveedor" value='<%# Eval("CodigoProveedor")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbProveedor" ClientInstanceName="cmbProveedor" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Estatus del contrato (No. plan)</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hfnCodigoPlan" value='<%# Eval("CodigoPlan")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbPlan" ClientInstanceName="cmbPlan" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
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
                                <label class="text-form col-sm-2">Número de orden o contrato</label>
                                <div class="col-sm-4">
                                   <asp:TextBox ID="txtContrato" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-2">Es un artículo que se puede reparar</label>
                                <div class="col-sm-4">
                                    <asp:RadioButton ID="rbReparaSi" runat="server" GroupName="Repara" Text="&nbsp;Si" onchange="ASPxCallbackPanel2.PerformCallback('rbRepSi');"/>&nbsp;
                                    <asp:RadioButton ID="rbReparaNo" runat="server" GroupName="Repara" Text="&nbsp;No"  onchange="ASPxCallbackPanel2.PerformCallback('rbRepNo');"/>&nbsp;
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </section>
                <section id="sComprador" class="row CeroPM">
                <div class="col-xs-12" id="dvcomprador">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Información del Comprador</h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i></span>
                        </div>                    
                        <div class="panel-body">
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
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">GL Class</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnGLClass" value='<%# Eval("GlClass")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbGlClass" ClientInstanceName="cmbGlClass" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" Enabled="<%# disabledComp %>" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Texto de búsqueda</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtTextBusq" runat="server" CssClass="form-control input-sm Campos"/>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Proveedor</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnCodProvComp" value='<%# Eval("CodigoProveedor_comp")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbProveedorComp" ClientInstanceName="cmbProveedorComp" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">País de origen proveedor</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnPaisOrigen" value='<%# Eval("PaisOrigen")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbPaisOrigen" ClientInstanceName="cmbPaisOrigen" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
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
                                <label class="text-form col-sm-2">Precio Unitario</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtPrecioU" onChange="recalcular()" TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-2">Moneda</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnCodigomonedaComprador" value='<%# Eval("Codigomoneda")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbMonedaComprador" ClientInstanceName="cmbMonedaComprador" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
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
                                <label class="text-form col-sm-2">Múltiplo</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtMultiplo" TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-2">Unidad de Medida</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnCodigoUM" value='<%# Eval("CodigoUM")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbCodigoUM" ClientInstanceName="cmbCodigoUM" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
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
                                <label class="text-form col-sm-2">Comprador</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnComprador" value='<%# Eval("codigoComprador")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbComprador" ClientInstanceName="cmbComprador" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Tiempo de Entrega (días)</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDiasEntrega" TextMode="Number" onChange="recalcularReorden()" step="1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Método de Coste Sales/Inventory</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnMtdoCosteInv" value='<%# Eval("MTDOCoste_Inv")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbMtdoCosteInv" ClientInstanceName="cmbMtdoCosteInv" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Método de Coste Sales/Purchasing</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnMtdoCostePursh" value='<%# Eval("MTDOCoste_Pursh")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbMtdoCostePursh" ClientInstanceName="cmbMtdoCostePursh" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <fieldset style="padding:10px;border: 1px solid #B5D2EA;border-radius:5px;">
                                <legend style="font-size:12px;font-weight:600;border:none;width: auto;padding:2px;color:#8CBADF;margin-bottom:0px;">Datos del Empaque</legend>
                                <div class="row form-group CeroPM">
                                    <label class="text-form col-sm-1">Tipo de Empaque</label>
                                    <div class="col-sm-3">
                                    <input type="hidden" id="hdnTipoEmpaque" value='<%# Eval("codigo_tipoEmpaque")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos Campos6Cols" ID="cmbTipoEmaque" ClientInstanceName="cmbTipoEmaque" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                    </div>
                                    <label class="text-form col-sm-1">Piezas por Empaque</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPiezaEmp" runat="server" TextMode="Number" step="1" CssClass="form-control input-sm Campos Campos6Cols"></asp:TextBox>
                                    </div>
                                    <label class="text-form col-sm-1">Unidad de Medida</label>
                                    <div class="col-sm-3">
                                        <input type="hidden" id="hdnUMEmpq" value='<%# Eval("UMEmpaque")%>' runat="server"/>
                                        <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbUmEmpaque" ClientInstanceName="cmbUmEmpaque" runat="server" IncrementalFilteringMode="Contains" 
                                            FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
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
                                    <label class="text-form col-sm-1">Alto</label>
                                    <div class="col-sm-3">
                                       <asp:TextBox ID="txtAlto" runat="server" TextMode="Number" step="1" CssClass="form-control input-sm Campos Campos6Cols"></asp:TextBox>
                                    </div>
                                    <label class="text-form col-sm-1">Ancho</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtAncho" runat="server" TextMode="Number" step="1" CssClass="form-control input-sm Campos Campos6Cols"></asp:TextBox>
                                    </div>
                                    <label class="text-form col-sm-1">Largo</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtLargo" runat="server" TextMode="Number" step="1" CssClass="form-control input-sm Campos Campos6Cols"></asp:TextBox>
                                    </div>
                                </div>
                            </fieldset>
                            <br />
                            <div class="row form-group CeroPM">  
                                <label class="text-form col-sm-2">Purch 1/Categoría</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnPursh1" value='<%# Eval("pursh1")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbPursh1" ClientInstanceName="cmbPursh1" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Purch 2/Categoría</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnPursh2" value='<%# Eval("pursh2")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbPursh2" ClientInstanceName="cmbPursh2" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
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
                                <label class="text-form col-sm-2">Familia</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnFamilia" value='<%# Eval("codigoFamilia")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbfamilia" ClientInstanceName="cmbfamilia" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                        </div>
                     </div>
                </div>
            </section>
                <section id="sPlaneador" class="row CeroPM">
                <div class="col-xs-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Información del Planeador</h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i></span>
                        </div>
                        <div class="panel-body">
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-2">Branch Plant</label>
                                <div class="col-sm-4">

                                    <input type="hidden" id="hdnbranch" value='<%# Eval("branch")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbbranch" ClientInstanceName="cmbbranch" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>

                                </div>
                                <label class="text-form col-sm-2">Cantidad de Punto de Reorden (Stock Mínimo)</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtStockMin" ToolTip="AUTOMATICO= RESULTA(CONSUMO/365 X T.E )+ (CONSUMO/365* STOCK SEGURIDAD)" TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group CeroPM"> 
                                <label class="text-form col-sm-2">Cantidad a Pedir(Stock Máximo)</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtStockMax" TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div> 
                                <label class="text-form col-sm-2">Días de Stock de Seguridad</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hndDias" value='<%# Eval("diasStok")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbDias" ClientInstanceName="cmbDias" onChange="recalcularReorden()" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"  
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>
                                    </dx:ASPxComboBox>

                                </div>

                            </div>
                            <div class="row form-group CeroPM">                                
                                <label class="text-form col-sm-2">Planeador</label>
                                <div class="col-sm-4">
                                    <input type="hidden" id="hdnPlaneador" value='<%# Eval("codigoPlaneador")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbPlaneador" ClientInstanceName="cmbPlaneador" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="text-form col-sm-2">Categoría de Conteo Cíclico</label>
                                <div class="col-sm-4">
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos" ID="cmbConteoCiclico" ClientInstanceName="cmbConteoCiclico" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
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
                        <div class="panel-body">
                            <fieldset style="padding:10px;border: 1px solid #B5D2EA;border-radius:5px;">
                                <legend style="font-size:12px;font-weight:600;border:none;width: auto;padding:2px;color:#8CBADF;margin-bottom:0px;">Datos de Almacenaje</legend>
                                <div class="row form-group CeroPM">
                                    <label class="text-form col-sm-1">Ubicación Primaria</label>
                                    <div class="col-sm-3">
                                        <input type="hidden" id="hndubicacionPrim" value='<%# Eval("ubicacionPrim")%>' runat="server"/>
                                        <dx:ASPxComboBox CssClass="form-control input-sm Campos Campos6Cols" ID="cmbUbicacionPrim" ClientInstanceName="cmbUbicacionPrim" runat="server" IncrementalFilteringMode="Contains" 
                                            FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                            PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                            <ValidationSettings>
                                                <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                            </ValidationSettings>
                                            <ClientSideEvents/>
                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                        </dx:ASPxComboBox>
                                    </div>
                                    <label class="text-form col-sm-1">Ubicación Secundaria</label>
                                    <div class="col-sm-3">
                                        <input type="hidden" id="hndubicacionSec" value='<%# Eval("ubicacionSec")%>' runat="server"/>
                                        <dx:ASPxComboBox CssClass="form-control input-sm Campos Campos6Cols" ID="cmbUbicacionSec" ClientInstanceName="cmbUbicacionSec" runat="server" IncrementalFilteringMode="Contains" 
                                            FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                            PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                            <ValidationSettings>
                                                <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                            </ValidationSettings>
                                            <ClientSideEvents/>
                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                        </dx:ASPxComboBox>
                                    </div>
                                    <label class="text-form col-sm-1">Unidad de Medida</label>
                                    <div class="col-sm-3">
                                        <input type="hidden" id="hdnumAlmacen" value='<%# Eval("umAlmacen")%>' runat="server"/>
                                        <dx:ASPxComboBox CssClass="form-control input-sm Campos Campos6Cols" ID="cmbUMAlmacen"  ClientInstanceName="cmbUMAlmacen" runat="server" IncrementalFilteringMode="Contains" 
                                            FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Width="100%" Paddings-Padding="0px" Theme="Aqua"  
                                            PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="Select a option"/>
                                            </ValidationSettings>
                                            <ClientSideEvents/>
                                            <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                        </dx:ASPxComboBox>
                                    </div>
                                </div>
                                <div class="row form-group CeroPM">
                                    <label class="text-form col-sm-1">Alto</label>
                                    <div class="col-sm-3">
                                       <asp:TextBox ID="txtAltoAlm" TextMode="Number" runat="server" CssClass="form-control input-sm Campos Campos6Cols"></asp:TextBox>
                                    </div>
                                    <label class="text-form col-sm-1">Ancho</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtAnchoAlm" TextMode="Number" runat="server" CssClass="form-control input-sm Campos Campos6Cols"></asp:TextBox>
                                    </div>
                                    <label class="text-form col-sm-1">Largo</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtLargoAlm" TextMode="Number" runat="server" CssClass="form-control input-sm Campos Campos6Cols"></asp:TextBox>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </section>
                <section id="sMantenimiento" class="row CeroPM">
                <div class="col-xs-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Información de Mantenimiento</h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i></span>
                        </div>
                        <div class="panel-body">
                            <div class="row form-group CeroPM">
                                <label class="text-form col-sm-1">Monto Total en la primera compra</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtCantidad"  TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos Campos6Cols"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-1">Monto Mensual Inv Promedio</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtTotal"  TextMode="Number" step="0.1" runat="server" CssClass="form-control input-sm Campos Campos6Cols"></asp:TextBox>
                                </div>
                                <label class="text-form col-sm-1">Moneda</label>
                                <div class="col-sm-3">
                                    <input type="hidden" id="hdnMoneda" value='<%# Eval("monedaMtto")%>' runat="server"/>
                                    <dx:ASPxComboBox CssClass="form-control input-sm Campos Campos6Cols" ID="cmbMonedaMtoo" ClientInstanceName="cmbMonedaMtoo" runat="server" IncrementalFilteringMode="Contains" 
                                        FilterMinLength="0" EnableCallbackMode="True" CallbackPageSize="20" FocusedStyle-Border-BorderColor="#3399ff" Paddings-Padding="0px" Theme="Aqua"
                                        PopupVerticalAlign="Above" PopupHorizontalAlign="Center" ItemStyle-SelectedStyle-Font-Italic="true">
                                        <ValidationSettings>
                                            <RequiredField  IsRequired="true" ErrorText="Select a option"/>
                                        </ValidationSettings>
                                        <ClientSideEvents/>
                                        <ButtonStyle BackColor="#0099FF"></ButtonStyle>                                                                                                                       
                                    </dx:ASPxComboBox>
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
                                <label class="text-form col-sm-2">Número de parte</label>
                                <label class="text-form col-sm-1"><span class="label label-success pull-right" style="padding:6px;"><asp:label id="lblstock2" runat="server" Text="M: Con Stock"></asp:label></span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtCodigoArticulo" runat="server" CssClass="form-control input-sm Campos"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            </form>
            <div id="pieForma" class="container">
            <div class="col-xs-12" style="background-color:dimgray;height:25px;font-weight:600;color:white;text-align:center;font-size:16px;">
                Historal del documento
            </div>
            <ul class="nav nav-tabs" style="padding-left:5px;">
                <li class="active"><a data-toggle="tab" href="#home">Aprobaciones</a></li>
                <li><a data-toggle="tab" href="#menu1">Historial</a></li>
            </ul>
            <div class="tab-content" style="padding:15px;">
                <div id="home" class="tab-pane fade in active">
                    <dx:ASPxGridView ID="xgrdAprobaciones" runat="server" AutoGenerateColumns="true" Width="100%" Font-Names="Segoe UI"
                        OnCustomCallback="xgrdAprobaciones_CustomCallback" OnHtmlDataCellPrepared="xgrdAprobaciones_HtmlDataCellPrepared"
                        ClientInstanceName="xgrdAprobaciones" Theme="Metropolis">
                        <Columns>
                            <dx:GridViewDataTextColumn  FieldName="codigoPerfil" Caption="Autoriza"/>
                            <dx:GridViewDataTextColumn  FieldName="codigoEmpleado" Caption="Nombre del Autorizador"/>
                        </Columns>
                        <Styles>
                            <AlternatingRow BackColor="#F2F2F2"></AlternatingRow>
                            <RowHotTrack BackColor="#CEECF5"></RowHotTrack>
                            <Header BackColor="#F2F2F2" HorizontalAlign="Center" Font-Bold="true" CssClass="text-center"></Header>
                        </Styles>
                        <SettingsPager Mode="ShowPager" PageSize="20" />
                        <Settings ShowFilterRow="True" />
                        <ClientSideEvents EndCallback="OnAprobacionesEndCallback" />
                    </dx:ASPxGridView>
                </div>
                <div id="menu1" class="tab-pane fade">
                    <h3>Historial</h3>
                    <p><asp:TextBox ID="txtComentarios" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control input-sm hidden"></asp:TextBox></p>
                    <dx:ASPxGridView ID="xgrdHistorial" runat="server" AutoGenerateColumns="true" Width="100%" Font-Names="Segoe UI"
                        ClientInstanceName="xgrdHistorial" Theme="Metropolis">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="fecha" Caption="Fecha" VisibleIndex="0" Width="15%"/>
                            <dx:GridViewDataTextColumn FieldName="NombreCompleto" Caption="Usuario" VisibleIndex="1" Width="25%"/>
                            <dx:GridViewDataTextColumn FieldName="accion" Caption="" VisibleIndex="2" Width="10%"/>
                            <dx:GridViewDataTextColumn FieldName="comentarios" Caption="Comentarios" VisibleIndex="3" Width="50%"/>
                        </Columns>
                        <Styles>
                            <AlternatingRow BackColor="#F2F2F2"/>
                            <RowHotTrack BackColor="#CEECF5"/>
                            <Header BackColor="#F2F2F2" HorizontalAlign="Center" Font-Bold="true" CssClass="text-center"/>
                        </Styles>
                        <SettingsPager Mode="ShowPager" PageSize="20" />
                        <Settings ShowFilterRow="True" />
                        <ClientSideEvents/>
                    </dx:ASPxGridView>
                </div>
            </div>
        </div>
          </dx:PanelContent>
          </PanelCollection>
      </dx:ASPxCallbackPanel>
     </div>
    </div>
</asp:Content>
