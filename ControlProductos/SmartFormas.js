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