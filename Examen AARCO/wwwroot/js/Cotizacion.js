$(document).ready(function () {
    (function (plugin) {
        function init(lstCats) {

            if (lstCats !== "") {
                CatalogosList = JSON.parse(lstCats);
                _llenarSelects(CatalogosList);
            }
        }
        plugin.init = init;
    })(APP);

    localStorage.setItem("path", 'https://localhost:7174/api/');


    $('#slctMarca').on('change', function () {
        var marca = $('#slctMarca option:selected').text();
        var parametros = { Marca: marca };
        SolicitudPostAjax(localStorage.getItem("path") + "SubMarcas", parametros, respuestaTest);
    });

    $('#slctSubMarca').append($('<option></option>').val(-1).html("Seleccione una submarca"));
    $('#slctModelo').append($('<option></option>').val(-1).html("Seleccione un modelo"));
    $('#slctDescripcion').append($('<option></option>').val(-1).html("Seleccione una descripcion"));


});



function respuestaTest(resp) {
    if (resp.estatus === EstatusRespuestaSession.OK) {

        $('#slctSubMarca').append($('<option></option>').val(-1).html("Seleccione una submarca"));
        $.each(resp.data, function (i, p) {
            if (p.submarca != null) {
                $('#slctSubMarca').append($('<option></option>').val(p.id).html(p.submarca));
            }
        });

        //slctEstatus
        $('#slctModelo').append($('<option></option>').val(-1).html("Seleccione un modelo"));
        $.each(cats.data, function (i, p) {
            if (p.modelo != null) {
                $('#slctModelo').append($('<option></option>').val(p.id).html(p.modelo));
            }
        });

        //slctEstatus
        $('#slctDescripcion').append($('<option></option>').val(-1).html("Seleccione una descripcion"));
        $.each(cats.data, function (i, p) {
            if (p.descripcion != null) {
                $('#slctDescripcion').append($('<option></option>').val(p.id).html(p.descripcion));
            }
        });
    } else if (resp.estatus == EstatusRespuestaSession.ERROR) {
        alert(resp.mensaje, "large");
    }
   
   

}

function SolicitudPostAjax(urlAction, parameters, functionCallbackSuccess, functionCallbackError = null) {
    $.ajax({
        url: urlAction,
        type: "POST",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(parameters),
        dataType: "json",
        cache: false,
        beforeSend: function () { },
        success: function (data) {
            functionCallbackSuccess(data);
        },
        error: function (jqXHR) {
            $("#loading").fadeOut();
            var mensaje = '';

            if (jqXHR.status === 0) {
                mensaje = 'No esta conectado, verifique su conexión.';
            }
            else if (jqXHR.status == 404) {
                mensaje = 'No se encontró la página solicitada, ERROR:404';
            }
            else if (jqXHR.status == 500) {
                mensaje = "Error interno del servidor, ERROR:500";
            }
            else {
                mensaje = 'Error no detectado : ' + jqXHR.responseText;
            }

            if (functionCallbackSuccess == null) {
                alerta(mensaje);
            }

            if (functionCallbackError != null) {
                var data = mensaje;
                functionCallbackError(data);
            }


        }
    });
}


function _llenarSelects(cats) {
    $('#slctMarca').append($('<option></option>').val(-1).html("Seleccione una marca"));
    $.each(cats.data, function (i, p) {
        if (p.marca != null) {
            $('#slctMarca').append($('<option></option>').val(p.id).html(p.marca));
        }
    });
}
var APP = {};
