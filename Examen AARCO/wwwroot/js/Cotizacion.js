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

    localStorage.setItem("path", '');


    $('#slctMarca').on('change', function () {
        var marca = $('#slctMarca option:selected').text();
        var parametros = { Marca: marca };
        SolicitudPostAjax("https://localhost:7279/Cotizacion/ObtieneSubMarcas", parametros, respuestaTest);
    });
    $('#slctSubMarca').on('change', function () {
        var Submarca = $('#slctSubMarca option:selected').text();
        var parametros = { SubMarca: Submarca };
        SolicitudPostAjax("https://localhost:7279/Cotizacion/ObtieneModelos", parametros, respuestaTest1);
    });
    $('#slctModelo').on('change', function () {
        var Modelo = $('#slctModelo option:selected').text();
        var parametros = { Modelo: Modelo };
        SolicitudPostAjax("https://localhost:7279/Cotizacion/ObtieneDescipcion", parametros, respuestaTest2);
    });

    $('#slctSubMarca').append($('<option></option>').val(-1).html("Seleccione una submarca"));
    $('#slctModelo').append($('<option></option>').val(-1).html("Seleccione un modelo"));
    $('#slctDescripcion').append($('<option></option>').val(-1).html("Seleccione una descripcion"));
    $('#slctColonia').append($('<option></option>').val(-1).html("Seleccione una colonia"));
 


    $('#txtCP').on('keyup', function () {
        if ($('#txtCP').val().length >= 5) {
           var CP = $('#txtCP').val();
            var parametros = { CP: CP };
            SolicitudPostAjax("https://localhost:7279/Cotizacion/ObtenerCP", parametros, respuestaTest3);
        }

       
    });

   
    $("#btnCotizar").on("click", function () {
        var Descripcion = $('#slctDescripcion').val();
        var parametros = { DescripcionId: Descripcion };
        SolicitudPostAjax("https://localhost:7279/Cotizacion/Peticion", parametros, respuestaTest4);

    });
});



function MensajeNotificacionOK(mensaje, tamanio, funcion) {
    bootbox.confirm({
        closeButton: false,
        centerVertical: true,
        message: mensaje,
        buttons: {
            confirm: {
                label: '<i class="fa fa-check"></i> Aceptar',
                className: 'btn btn-success'
            },
            cancel: {
                label: '<i class="fa fa-times"></i> Cancelar',
                className: 'btn btn-danger visually-hidden'
            }
        },
        callback: function (result) {
            if (result) {
                funcion();
            }
        },
        size: tamanio
    });
}
function respuestaTest(resp) {
    parse = JSON.parse(resp);
    if (parse.estatus ==  EstatusRespuestaSession.OK) {

        $("#slctSubMarca").empty().append('<option selected disabled>Seleccione una submarca</option>');
      


        $.each(parse.data, function (i, p) {
            if (p.subMarca != null) {
                $('#slctSubMarca').append($('<option></option>').val(p.id).html(p.subMarca));
            }
        });
       
    } else if (resp.estatus == EstatusRespuestaSession.ERROR) {
        alert(resp.mensaje, "large");
    }
}

function respuestaTest1(resp) {
    parse = JSON.parse(resp);
    if (parse.estatus ==  EstatusRespuestaSession.OK) {

        $("#slctModelo").empty().append('<option selected disabled>Seleccione un modelo</option>');


        $.each(parse.data, function (i, p) {
            if (p.modelo != null) {
                $('#slctModelo').append($('<option></option>').val(p.id).html(p.modelo));
            }
        });

    } else if (resp.estatus == EstatusRespuestaSession.ERROR) {
        alert(resp.mensaje, "large");
    }
}

function respuestaTest2(resp) {
    parse = JSON.parse(resp);
    if (parse.estatus ==  EstatusRespuestaSession.OK) {

      
        $("#slctDescripcion").empty().append('<option selected disabled>Seleccione una descripcion</option>');

        $.each(parse.data, function (i, p) {
            if (p.descripcion != null) {
                $('#slctDescripcion').append($('<option></option>').val(p.descripcionId).html(p.descripcion));
            }
        });
    } else if (resp.estatus == EstatusRespuestaSession.ERROR) {
        alert(resp.mensaje, "large");
    }
}

function respuestaTest3(resp) {
    parse = JSON.parse(resp);
    parse2 = JSON.parse(parse.CatalogoJsonString);
    
    $('#txtEstado').val(parse2[0].Municipio.Estado.sEstado);
    $('#txtMunicipio').val(parse2[0].Municipio.sMunicipio);
    $("#slctColonia").empty().append('<option selected disabled>Seleccione una colonia</option>');

    $.each(parse2[0].Ubicacion, function (i, p) {
        if (p.iIdUbicacion != null) {
            $('#slctColonia').append($('<option></option>').val(p.iIdUbicacion).html(p.sUbicacion));
        }
    });
    
}

function respuestaTest4(resp) {
    parse = JSON.parse(resp);
     

    if (parse.PeticionFinalizada == true) {
        $.each(parse.aseguradora, function (i, p) {
            if (p.AseguradoraId == 1) {
                $('#HDI-div').removeClass("visually-hidden");
                const p2 = document.getElementById("HDI-p");
                p2.innerText = p.tarifa;


            }
            if (p.AseguradoraId == 2) {
                $('#CHUBB-div').removeClass("visually-hidden");
                const p2 = document.getElementById("CHUBB-p");
                p2.innerText = p.tarifa;
            }
            if (p.AseguradoraId == 3) {
                $('#QUALITAS-div').removeClass("visually-hidden");
                const p2 = document.getElementById("QUALITAS-p");
                p2.innerText = p.tarifa;
            }
            if (p.AseguradoraId == 4) {

                $('#AXA-div').removeClass("visually-hidden");
                const p2 = document.getElementById("AXA-p");
                p2.innerText = p.tarifa;
            }
            if (p.AseguradoraId == 5) {
                $('#ZURICH-div').removeClass("visually-hidden");
                const p2 = document.getElementById("CHUBB-p");
                p2.innerText = p.tarifa;
            }
            const p2 = document.getElementById("txtcot-p");
            p2.innerText = "  .: Cotización terminada:."

        });
    } else {
        var count = 0;
        var Descripcion = $('#slctDescripcion').val();
        var parametros = { DescripcionId: Descripcion };
        while (count <= 20) {
            setInterval(function () {
                
            }, 1000);
            count += 1

        }
        SolicitudPostAjax("https://localhost:7279/Cotizacion/Peticion", parametros, respuestaTest5)
        $("#ModalFiltro").modal('show');
        const p2 = document.getElementById("txtcot-p");
        p2.innerText = " .: Cotización en proceso:."
    }
}

function respuestaTest5(resp) {
    parse = JSON.parse(resp);
     

    if (parse.PeticionFinalizada == true) {

    } else {
        var count = 0;
        var Descripcion = $('#slctDescripcion').val();
        var parametros = { DescripcionId: Descripcion };
        while (count <= 20) {
            setInterval(function () {
                
            }, 1000);
            count += 1

        }
        SolicitudPostAjax("https://localhost:7279/Cotizacion/Peticion", parametros, respuestaTest4)
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

function SolicitudEstandarAjax(url, parametros, functionCallbackSuccess, functionCallbackError = null) {
    $.ajax({
        url: url,
        type: "GET",
        cache: false,
        traditional: true,
        contentType: "application/json; charset=utf-8",
        data: parametros,
        beforeSend: function () {

        },
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
            else if (exception != undefined && exception === 'timeout') {
                mensaje = 'Error de Time Out.';
            }
            else if (exception != undefined && exception === 'abort') {
                mensaje = 'Solicitud AJAX Abortada.';
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

var APP = {};
var EstatusRespuestaSession = { SIN_RESPUESTA: 0, OK: 1, ERROR: 2, SESIONEXPIRADA: 3 };