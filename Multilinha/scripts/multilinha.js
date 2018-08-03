try {
    //Variavel global adicionada em cada página
    var dtFechasStr = dtfechas.split('/')
    dtFechasStr = new Date(dtFechasStr[2].split(" ")[0], dtFechasStr[1] - 1, dtFechasStr[0])
}
catch (err) { };

//Inicializar datepickers
$(function () {
    $(".datepicker").datepicker();

});

// Onchange de datepickers
$(function () {

    var $regexname = /^([1-9][0-9]{3})[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$/;


    //Funcao de OnChange do DatePicker BeginDate
    //Valida se é uma data válida, actualiza o DatePicker EndDate com o valor minimo
    $('#dpBeginDate').on('change', function () {
        var dtin = ($(this).val()).split("-");
        var dateParsed = new Date(dtin[0], dtin[1] - 1, dtin[2]);
        if (!$(this).val().match($regexname) && $(this).val() != "") {
            // there is a mismatch, hence show the error message
            $('#requiredformatdtini').removeClass('hidden');
            $('#requiredformatdtini').show();
        }
        else {
            // else, do not display message
            $("#requiredformatdtini").hide();

            $("#dpEndDate").datepicker("option", "minDate", dateParsed);
            if ($("#dpEndDate")[0] != null && $("#dpEndDate")[0].value != "")
                triggerChange();
            if ($("#dpEndDatePesquisa")[0] != null && $("#dpEndDatePesquisa")[0].value != "")
                triggerChangePesquisa();
        }
    });

    //Funcao de OnChange do DatePicker BeginDateTomorrow
    //Obriga a que a data minima seja o dia de amanha
    //Valida se é uma data válida, actualiza o DatePicker EndDate com o valor minimo
    $('#dpBeginDateTomorrow').on('change', function () {
        var dtin = ($(this).val()).split("-");
        var dateParsed = new Date(dtin[0], dtin[1] - 1, dtin[2]);
        var validDate = dateParsed.setHours(0, 0, 0, 0) > dtFechasStr;
        var regexVal = !$regexname.test($(this).val());
        var dontExistDateOnCalendar = !isValidDate(dtin[0], dtin[1] - 1, dtin[2]);
        if ($(this).val() != "" && (regexVal || !validDate || isNaN(dateParsed.getTime()) || dontExistDateOnCalendar)) {
            if (isNaN(dateParsed.getTime()) || regexVal || dontExistDateOnCalendar) {
                $('#requiredformatdtini')[0].innerHTML = "Formato incorreto";
            }
            else if (!validDate) {
                $('#requiredformatdtini')[0].innerHTML = "Data início igual ou inferior à de hoje";
            }

            // there is a mismatch, hence show the error message
            $('#requiredformatdtini').removeClass('hidden');
            $('#requiredformatdtini').show();
        }
        else {
            // else, do not display message
            $("#requiredformatdtini").hide();

            $("#dpEndDate").datepicker("option", "minDate", dateParsed);
            if ($("#dpEndDate")[0].value != "")
                triggerChange();
        }
    });

    //Funcao de OnChange do DatePicker EndDatePesquisa
    //Valida se é uma data válida, Se *BeginDate Tiver sido preenchido tem de ser superior
    $('#dpEndDatePesquisa').on('change', function () {
        var dtfim = ($(this).val()).split("-");
        var dtinicio = $('input[id*=dpBeginDate]')[0];
        var validDate = true;
        var dateParsed = new Date(dtfim[0], dtfim[1] - 1, dtfim[2]);
        if (dtinicio.value != "") {
            var dtInt = dtinicio.value.split("-");
            validDate = dateParsed.setHours(0, 0, 0, 0) >= new Date(dtInt[0], dtInt[1] - 1, dtInt[2]).setHours(0, 0, 0, 0); //verifica se data fim superior à data inicio
        }
        var regexVal = $regexname.test($(this).val()); //verifica regex
        var dontExistDateOnCalendar = !isValidDate(dtfim[0], dtfim[1] - 1, dtfim[2])
        if ($(this).val() != "") {
            if (isNaN(dateParsed.getTime()) || !regexVal || dontExistDateOnCalendar) {
                $('#requiredformatdtfn')[0].innerHTML = "Formato incorreto";
                //Mostra a msg
                $('#requiredformatdtfn').removeClass('hidden');
                $('#requiredformatdtfn').show();
            }
            else if (!validDate) {
                $('#requiredformatdtfn')[0].innerHTML = "Data fim inferior à de início";
                //Mostra a msg
                $('#requiredformatdtfn').removeClass('hidden');
                $('#requiredformatdtfn').show();
            }
            else {
                //Esconde a msg > campo correto e valido
                $("#requiredformatdtfn").hide()
            }

        }
        else {
            //Se o campo estiver vazio o btnPesquisar vai popular o field
            $("#requiredformatdtfn").hide();
        }
    });


    //Funcao de OnChange do DatePicker EndDate
    //Valida se é uma data válida, Se *BeginDate Tiver sido preenchido tem de ser superior
    $('#dpEndDate').on('change', function () {
        var dtfim = ($(this).val()).split("-");
        var dtinicio = $('input[id*=dpBeginDate]')[0];
        var dtInt = dtinicio.value.split("-");
        var dateParsed = new Date(dtfim[0], dtfim[1] - 1, dtfim[2]);
        var validDate = true;
        if (dtinicio.value != "") {
            validDate = dateParsed.setHours(0, 0, 0, 0) > new Date(dtInt[0], dtInt[1] - 1, dtInt[2]).setHours(0, 0, 0, 0); //valida data fim maior q data inicio
        }
        var regexVal = !$regexname.test($(this).val());
        var dontExistDateOnCalendar = !isValidDate(dtfim[0], dtfim[1] - 1, dtfim[2])
        if ($(this).val() != "" && (regexVal || !validDate || isNaN(dateParsed.getTime()) || dontExistDateOnCalendar)) {
            if (isNaN(dateParsed.getTime()) || regexVal || dontExistDateOnCalendar) {
                $('#requiredformatdtfn')[0].innerHTML = "Formato incorreto";
            }
            else if (!validDate) {
                $('#requiredformatdtfn')[0].innerHTML = "Data fim igual ou inferior à de início";
            }

            // there is a mismatch, hence show the error message
            $('#requiredformatdtfn').removeClass('hidden');
            $('#requiredformatdtfn').show();
        }
        else {
            // else, do not display message
            $("#requiredformatdtfn").hide();
        }
    });
});


function fAccordionController() {
    
    var accLst = $.find('.accordion');
    
    if (accLst && accLst.length > 0) {
        $.each(accLst, function (index, value) {

            var val = $(value);

            val.click(function () {
                var panel = val.parent().next();

                if (panel.hasClass('hidden')) {
                    panel.removeClass('hidden');
                    val.addClass("active");
                }
                else {
                    panel.addClass('hidden');
                    val.removeClass("active");
                }
            })
        });
    }

    $("#CPH_btnOpenAll").on("click", function () {
        var hiddenLst = $(".row.hidden");
        if (hiddenLst && hiddenLst.length > 0)
            hiddenLst.each(function () {
                $(this).removeClass('hidden');
                var aaccordionLst = $("a.accordion");

                if (aaccordionLst && aaccordionLst.length > 0) {
                    aaccordionLst.each(function () {
                        $(this).addClass("active");
                    });
                }
            });
    });


    $("#CPH_btnCloseAll").on("click", function () {
        var closeAccordionLst = $(".closeAccordion");

        if (closeAccordionLst && closeAccordionLst.length > 0)
            closeAccordionLst.each(function () {
                $(this).addClass('hidden');

                var aaccordionLst = $("a.accordion");

                if (aaccordionLst && aaccordionLst.length > 0) {
                    aaccordionLst.each(function () {
                        $(this).removeClass("active");
                    });
                }
            });
    });
}

//Abrir Date Pickers
$(".dtField").datepicker({
    dateFormat: 'yy-mm-dd',
    changeMonth: true,
    changeYear: true,
});

$("#dpBeginDate").datepicker({
    dateFormat: 'yy-mm-dd',
    changeMonth: true,
    changeYear: true,
    onSelect: function (date) {
        $("#dpBeginDate").change();
        var date2 = $('#dpBeginDate').datepicker('getDate');
        date2.setDate(date2.getDate() + 1);
        $("#dpEndDate").datepicker("option", "minDate", date2);
        $("#dpEndDate").change();
        $("#dpEndDatePesquisa").datepicker("option", "minDate", date2);
        $("#dpEndDatePesquisa").change();
    }
});
//Inicializacao DatePicker BeginDateTomorrow
$("#dpBeginDateTomorrow").datepicker({
    dateFormat: 'yy-mm-dd',
    changeMonth: true,
    changeYear: true,
    minDate: '+1d',
    onSelect: function (date) {
        $('#dpBeginDateTomorrow').change();
        var date2 = $('#dpBeginDateTomorrow').datepicker('getDate');
        date2.setDate(date2.getDate() + 1);
        $("#dpEndDate").datepicker("option", "minDate", date2);
    },
    change: function (date) {
        var date2 = $('#dpBeginDateTomorrow').datepicker('getDate');
        date2.setDate(date2.getDate() + 2);
        $("#dpEndDate").datepicker("option", "minDate", date2);
    },
});

//Inicializacao DatePicker EndDate
$("#dpEndDate").datepicker({
    dateFormat: 'yy-mm-dd',
    changeMonth: true,
    changeYear: true,
    minDate: '+1d',
    onSelect: function (date) {
        $("#dpEndDate").change();
    }
});

//Inicializacao DatePicker EndDatePesquisa
$("#dpEndDatePesquisa").datepicker({
    dateFormat: 'yy-mm-dd',
    changeMonth: true,
    changeYear: true,
    onSelect: function (date) {
        $("#dpEndDatePesquisa").change();
    }
});

//Preenche o campo EndDatePesquisa se nao tiver preenchido com Data de hoje ou +1 dia do BeginDate
$("#btnPesquisar").click(function () {
    var rt = true;
    if ($("#dpEndDatePesquisa")[0] != null && $("#dpEndDatePesquisa")[0].value == '') {
        $("#lbrequired").show();

        if ($('#dpBeginDate')[0].value != "") {
            var date2 = $('#dpBeginDate').datepicker('getDate');
            $("#dpEndDatePesquisa").datepicker("setDate", date2.setDate(date2.getDate() + 1));
        }
        else {

            $('#dpEndDatePesquisa').datepicker("setDate", dtFechasStr)
        }
    }
    else {
        ValidaDatasPesquisa();

    }
})

///MULTILINHA
$('#txtdatainiciocontrato').datepicker("setDate", dtFechasStr)

function ValidaDatas() {
    var $regexname = /^([1-9][0-9]{3})[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$/;

    var dtfim = ($("#dpEndDate").val()).split("-");
    var dtinicio = $('input[id*=dpBeginDate]')[0];
    var dtInt = dtinicio.value.split("-");
    var dateParsed = new Date(dtfim[0], dtfim[1] - 1, dtfim[2]);
    var validDate = true;
    if (dtinicio.value != "") {
        validDate = dateParsed.setHours(0, 0, 0, 0) >= new Date(dtInt[0], dtInt[1] - 1, dtInt[2]).setHours(0, 0, 0, 0); //valida data fim maior q data inicio
    }
    var regexVal = !$regexname.test($("#dpEndDate").val());
    var dontExistDateOnCalendar = !isValidDate(dtfim[0], dtfim[1] - 1, dtfim[2])
    if ($("#dpEndDate").val() != "" && (regexVal || !validDate || isNaN(dateParsed.getTime()) || dontExistDateOnCalendar)) {
        if (isNaN(dateParsed.getTime()) || regexVal || dontExistDateOnCalendar) {
            $('#requiredformatdtfn')[0].innerHTML = "Formato incorreto";
        }
        else if (!validDate) {
            $('#requiredformatdtfn')[0].innerHTML = "Data fim inferior à de início";
        }

        // there is a mismatch, hence show the error message
        $('#requiredformatdtfn').removeClass('hidden');
        $('#requiredformatdtfn').show();
        //this.Page.args= false;
        return false;

    }
    else {
        // else, do not display message
        $("#requiredformatdtfn").hide();
        return true;
    }
}

function ValidaDatasTomorrow() {
    var $regexname = /^([1-9][0-9]{3})[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$/;
    $('#requiredformatdtfn')[0].innerHTML = "";
    $('#requiredformatdtini')[0].innerHTML = "";

    var dtfim = ($("#dpEndDate").val()).split("-");
    var dtinicio = $('input[id*=dpBeginDateTomorrow]')[0];
    var dtinicioDisable = dtinicio.disabled; //verifica se campo está editavel
    var dtInt = dtinicio.value.split("-");
    var dateParsed = new Date(dtfim[0], dtfim[1] - 1, dtfim[2]);
    var validDate = true;
    if (dtinicio.value != "") {
        validDate = dateParsed.setHours(0, 0, 0, 0) > new Date(dtInt[0], dtInt[1] - 1, dtInt[2]).setHours(0, 0, 0, 0); //valida data fim maior q data inicio
        validDateIni = new Date(dtInt[0], dtInt[1] - 1, dtInt[2]) > dtFechasStr; //valida data inicio maior q dia hoje
    }
    var regexVal = !$regexname.test($("#dpEndDate").val());
    var dontExistDateOnCalendar = !isValidDate(dtfim[0], dtfim[1] - 1, dtfim[2])
    if ($("#dpEndDate").val() != "" && (regexVal || !validDate || (!validDateIni && !dtinicioDisable) || isNaN(dateParsed.getTime()) || dontExistDateOnCalendar)) {
        if (isNaN(dateParsed.getTime()) || regexVal || dontExistDateOnCalendar) {
            $('#requiredformatdtfn')[0].innerHTML = "Formato incorreto";
        }
        else if (!validDate) {
            $('#requiredformatdtfn')[0].innerHTML = "Data fim igual ou inferior à de início";
        }
        else if (!validDateIni && !dtinicioDisable) {
            $('#requiredformatdtini')[0].innerHTML = "Data início igual ou inferior ao dia de hoje";
        }
        // there is a mismatch, hence show the error message
        $('#requiredformatdtfn').removeClass('hidden');
        $('#requiredformatdtfn').show();
        $('#requiredformatdtini').removeClass('hidden');
        $('#requiredformatdtini').show();
        //this.Page.args= false;
        return false;

    }
    else {
        // else, do not display message
        $("#requiredformatdtfn").hide();
        $('#requiredformatdtini').hide();
        return true;
    }
}

function ValidaDatasPesquisa() {
    var $regexname = /^([1-9][0-9]{3})[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$/;

    var dtfim;
    if (($("#dpEndDatePesquisa").val()) != null) {
        dtfim = ($("#dpEndDatePesquisa").val()).split("-");
    }
    var dtinicio = $('input[id*=dpBeginDate]')[0];

    if (dtfim != null && dtinicio != null) {
        var dtInt;
        if (dtinicio != null)
            dtInt = dtinicio.value.split("-");
        var dateParsed = new Date(9999, 12, 31);
        if (dtfim != "" && dtfim != null) {
            dateParsed = new Date(dtfim[0], dtfim[1] - 1, dtfim[2]);
        }
        var validDate = true;
        if (dtinicio.value != "") {
            validDate = dateParsed.setHours(0, 0, 0, 0) >= new Date(dtInt[0], dtInt[1] - 1, dtInt[2]).setHours(0, 0, 0, 0); //valida data fim maior q data inicio
        }
        var regexVal = !$regexname.test($("#dpEndDatePesquisa").val());
        var dontExistDateOnCalendar = !isValidDate(dtfim[0], dtfim[1] - 1, dtfim[2])
        if ($("#dpEndDatePesquisa").val() != "" && (regexVal || !validDate || isNaN(dateParsed.getTime()) || dontExistDateOnCalendar)) {
            if (isNaN(dateParsed.getTime()) || regexVal || dontExistDateOnCalendar) {
                $('#requiredformatdtfn')[0].innerHTML = "Formato incorreto";
            }
            else if (!validDate) {
                $('#requiredformatdtfn')[0].innerHTML = "Data fim inferior à de início";
            }

            // there is a mismatch, hence show the error message
            $('#requiredformatdtfn').removeClass('hidden');
            $('#requiredformatdtfn').show();
            //this.Page.args= false;
            return false;

        }
        else {
            // else, do not display message
            $("#requiredformatdtfn").hide();
            return true;
        }
    }
}


//Formato números
$("[class*=number]").keypress(function (e) {
    var theEvent = window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /^[0-9]+([,]{1}[0-9]{0,5}){0,1}$/;
    if (!regex.test(this.value + key))
        return false
});

$("[class*=number]").blur(function (e) {
    if (this.value.indexOf('.') != -1) {
        this.value = this.value.replace('.', ',');
    }

    var val = this.value.split(',');
    if (val.length == 1 && val[0] == "")
        this.value = this.value + '0,00';
    else if (val.length == 1)
        this.value = this.value + ',00';
    else if (val[1].length == 0)
        this.value = this.value + '00';
    else if (val[1].length == 1)
        this.value = this.value + '0';

});

//Formato números
$("[class=numberMin]").keypress(function (e) {
    var theEvent = window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /^[0-9]+([,]{1}[0-9]{0,4}){0,1}$/;
    if (!regex.test(this.value + key))
        return false
});

//Formato números
$("[class=numberMax]").keypress(function (e) {
    var theEvent = window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /^[0-9]+([,]{1}[0-9]{0,4}){0,1}$/;
    if (!regex.test(this.value + key))
        return false
});

$("[class=numberMin]").blur(function (e) {
    var val = this.value.split(',');
    if (val.length == 1 && val[0] == "")
        this.value = this.value + '0,00001';
    else if (val.length == 1)
        this.value = this.value + ',00001';
    else if (val[1].length == 0)
        this.value = this.value + '00001';
    else if (val[1].length == 1)
        this.value = this.value + '0001';
    else if (val[1].length == 2)
        this.value = this.value + '001';
    else if (val[1].length == 3)
        this.value = this.value + '01';
    else if (val[1].length == 4)
        this.value = this.value + '1';
});

$("[class=numberMax]").blur(function (e) {
    var val = this.value.split(',');
    if (val.length == 1 && val[0] == "")
        this.value = this.value + '0,00000';
    else if (val.length == 1)
        this.value = this.value + ',00000';
    else if (val[1].length == 0)
        this.value = this.value + '00000';
    else if (val[1].length == 1)
        this.value = this.value + '0000';
    else if (val[1].length == 2)
        this.value = this.value + '000';
    else if (val[1].length == 3)
        this.value = this.value + '00';
    else if (val[1].length == 4)
        this.value = this.value + '0';
});

$("[class=percentual]").keypress(function (e) {
    var theEvent = window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /^([0-9]{0,2})+([,]{1}[0-9]{0,9}){0,1}$/;
    if (!regex.test(this.value + key))
        return false

});

$("[class=percentual]").blur(function (e) {

    if (this.value.indexOf('.') != -1) {
        this.value = this.value.replace('.', ',');
    }
    var val = this.value.split(',');
    if (val.length == 1 && val[0] == "")
        this.value = this.value + '0,000000000';
    else if (val.length == 1)
        this.value = this.value + ',000000000';
    else if (val[1].length == 0)
        this.value = this.value + '000000000';
    else if (val[1].length == 1)
        this.value = this.value + '00000000';
    else if (val[1].length == 2)
        this.value = this.value + '0000000';
    else if (val[1].length == 3)
        this.value = this.value + '000000';
    else if (val[1].length == 4)
        this.value = this.value + '00000';
    else if (val[1].length == 5)
        this.value = this.value + '0000';
    else if (val[1].length == 6)
        this.value = this.value + '000';
    else if (val[1].length == 7)
        this.value = this.value + '00';
    else if (val[1].length == 8)
        this.value = this.value + '0';
});

function ValidaValores() {
    var b = ValidaPercentual();
    var a = ValidaMontantes();

    if (a && b)
        return true;
    else
        return false;

};

//valida valor minimo do percentual
function ValidaPercentual() {
    var a = true;
    var percentual = $('#TiposEncargo_txtPercentual');
    if (percentual.val() == '' || percentual.val() == '0' || percentual.val() == '0,000000000') {
        var page = window.location.pathname;
        //Se pagina definicao encargo, percentual tem de ser diferente de zero
        if (page.indexOf('DefinicaoCodigoEncargo') != -1) {
            $('#TiposEncargo_reqPercentual').show()
            a = false;
        }
    }
    else {
        $('#TiposEncargo_reqPercentual').hide()
        a = true;
    }

    return a;
};

//valida valor minimo dos montantes
function ValidaMontantes() {
    var a = true;
    var montantebase = $('#TiposEncargo_txtMontanteBase');
    var montanteMinimo = $('#TiposEncargo_txtMontanteMinimo');
    var montanteMaximo = $('#TiposEncargo_txtMontanteMaximo');

    if (montantebase.val() == '' || montantebase.val() == '0' || montantebase.val() == '0,00000') {
        var page = window.location.pathname;
        //Se pagina definicao encargo, montante base tem de ser diferente de zero
        if (page.indexOf('DefinicaoCodigoEncargo') != -1) {
            $('#TiposEncargo_reqMontanteBase').show()
            a = false;
        }
        else {
            //Montante Base pose ser zero no Negociado
            $('#TiposEncargo_reqMontanteBase').hide()
        }
    }
    else {
        $('#TiposEncargo_reqMontanteBase').hide();
    }

    if (montanteMinimo.val() == '' || montanteMinimo.val() == '0' || montanteMinimo.val() == '0,00000') {

        var page = window.location.pathname;
        //Se pagina definicao encargo, montante minimo tem de ser diferente de zero
        if (page.indexOf('DefinicaoCodigoEncargo') != -1) {
            $('#TiposEncargo_reqMontanteMinimo').show()
            a = false;
        }
        else {
            //Montante Minimo pode ser zero no Negociado
            $('#TiposEncargo_reqMontanteMinimo').hide();
        }
    }
    else {
        $('#TiposEncargo_reqMontanteMinimo').hide();
    }

    if (montanteMaximo.val() == '' || montanteMaximo.val() == '0' || montanteMaximo.val() == '0,00000') {
        var page = window.location.pathname;
        //Se pagina definicao encargo, montante maximo tem de ser diferente de zero
        if (page.indexOf('DefinicaoCodigoEncargo') != -1) {
            $('#TiposEncargo_reqMontanteMaximo').show()
            a = false;
        }
    }
    else {
        $('#TiposEncargo_reqMontanteMaximo').hide();
    }

    return a;
}

//Validacao, verifica se as labels de formato incorrecto Datas estao visiveis
function validateSearch() {
    if (($('#requiredformatdtfn') && $('#requiredformatdtfn')) && $('#requiredformatdtfn').is(":visible") || $('#requiredformatdtfn').is(":visible"))
        return false;
    return true;
}

function triggerChange() {
    $('#dpEndDate').trigger('change');
}

function triggerChangePesquisa() {
    $('#dpEndDatePesquisa').trigger('change');
}

//Verifica se a data é valida (Ex: yyyy-02-31) Valores que regex nao valida
function isValidDate(year, month, day) {
    var d = new Date(year, month, day);
    if (d.getFullYear() == year && d.getMonth() == month && d.getDate() == day) {
        return true;
    }
    return false;
}

function DisplayLoadingDiv() {
    if ($(":invalid").length < 1)
        $("#preloader").addClass("preloader")
}

function HideLoadingDiv() {
    $(".preloader").removeClass("preloader");
}

function submitForm() {
    if (!Page_IsValid) {
        return false
    }
    else
        return true;
}

function showload3() {
    if (submitForm()) {
        if (ValidaDatasTomorrow() && ValidaValores()) {
            //DisplayLoadingDiv();
        }
        else return false;
    }
    else
        return false;
}

function showload2() {
    if (submitForm()) {
        if (ValidaDatas()) {//DisplayLoadingDiv();
        }
        else return false;
    }
    else
        return false
}

function showload() {
    if (submitForm()) {
        if (ValidaDatasTomorrow()) {//DisplayLoadingDiv();
        }
        else return false;
    }
    else
        return false;
}

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function OnTreeClick(evt)
  {
   var src = window.event != window.undefined ? window.event.srcElement : evt.target;
   var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
   if(isChkBoxClick)
   {
       var parentTable = GetParentByTagName("table", src);
       var nxtSibling = parentTable.nextSibling;
       //check if nxt sibling is not null & is an element node
        if(nxtSibling && nxtSibling.nodeType == 1)
       {
            //if node has children    
           if(nxtSibling.tagName.toLowerCase() == "div")
           {
               //check or uncheck children at all levels
               CheckUncheckChildren(parentTable.nextSibling, src.checked);
           }
       }
   //check or uncheck parents at all levels
   CheckUncheckParents(src, src.checked);
   }
}

function CheckUncheckChildren(childContainer, check)
  {
   var childChkBoxes = childContainer.getElementsByTagName("input");
        var childChkBoxCount = childChkBoxes.length;
   for(var i=0;i<childChkBoxCount;i++)
   {
       childChkBoxes[i].checked = check;
   }
  }
 
function CheckUncheckParents(srcChild, check)
  {
   var parentDiv = GetParentByTagName("div", srcChild);
   var parentNodeTable = parentDiv.previousSibling;
   if(parentNodeTable)
   {
       var checkUncheckSwitch;
      //checkbox checked 
       if(check)
       {
           var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
           if(isAllSiblingsChecked)
                   checkUncheckSwitch = true;
           else
                   return; //do not need to check parent if any(one or more) child not checked
       }
       else //checkbox unchecked
       {
           checkUncheckSwitch = false;
       }
   
       var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
       if(inpElemsInParentTable.length > 0)
       {
           var parentNodeChkBox = inpElemsInParentTable[0];
           parentNodeChkBox.checked = checkUncheckSwitch;
           //do the same recursively
          CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
       }
   }
  }
 
function AreAllSiblingsChecked(chkBox)
  {
   var parentDiv = GetParentByTagName("div", chkBox);
   var childCount = parentDiv.childNodes.length;
   for(var i=0;i<childCount;i++)
   {
       if(parentDiv.childNodes[i].nodeType == 1)
       {
           //check if the child node is an element node
           if(parentDiv.childNodes[i].tagName.toLowerCase() == "table")
           {
               var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
               //if any of sibling nodes are not checked, return false
               if(!prevChkBox.checked)
               {
                   return false;
               }
           }
       }
   }
   return true;
  }
 
//utility function to get the container of an element by tagname
function GetParentByTagName(parentTagName, childElementObj)
  {
   var parent = childElementObj.parentNode;
   while(parent.tagName.toLowerCase() != parentTagName.toLowerCase())
       {
           parent = parent.parentNode;
       }
   return parent;
  }

