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
});


function _llenarSelects(cats) {
    //slctPuesto

    $('#slctMarca').append($('<option></option>').val(-1).html("Seleccione..."));
    $.each(cats.data, function (i, p) {

        $('#slctMarca').append($('<option></option>').val(i++).html(p.marca));
    });

    //slctMenu
    $('#slctMenu').append($('<option></option>').val(-1).html("Seleccione..."));
    $.each(data, function (i, p) {
        $('#slctMenu').append($('<option></option>').val(p.IdMenu).html(p.DescripcionMenu));
    });

    //slctEstatus
    $('#slctEstatus').append($('<option></option>').val(-1).html("Seleccione..."));
    $.each(cats.infoCatalogos.StatusUsers, function (i, p) {
        $('#slctEstatus').append($('<option></option>').val(p.IdEstatus).html(p.DescripcionUser));
    });

}
var APP = {};
