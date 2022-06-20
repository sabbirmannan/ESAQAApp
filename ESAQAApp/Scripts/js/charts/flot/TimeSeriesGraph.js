function Print(divName) {
    var fileName = moment(new Date()).format('YYYYMMDDHHmmss');

    html2canvas($('#' + divName), {
        onrendered: function (canvas) {
            var img = canvas.toDataURL("image/png");
            download(img, fileName + ".png", "image/png");
        }
    });
}

var objJsonData;
var plot;
var year_Y_axis;

$(document).ready(function () {
    $("#graphReport").hide();

    $("#ddlStrategicLocation").select2({
        placeholder: "Strategic Location...",
        allowClear: true
    }).on("change", function (e) {
        var value = $("#ddlStrategicLocation option:selected").val();
    });

    $("#ddlParameters").select2({
        placeholder: "Parameters...",
        allowClear: true
    }).on("change", function (e) {
        var value = $("#ddlParameters option:selected").val();
        $("#ddlUnit").val(value);
    });

    $("#ddlFromYear").select2({
        placeholder: "Select a year...",
        allowClear: true
    }).on("change", function (e) {
        var value = $("#ddlFromYear option:selected").val();
    });

    $("#ddlToYear").select2({
        placeholder: "Select a year...",
        allowClear: true
    }).on("change", function (e) {
        var value = $("#ddlToYear option:selected").val();
    });

    $("#ddlEQS").select2({
        placeholder: "E.Q.S...",
        allowClear: true
    }).on("change", function (e) {
        var value = $("#ddlEQS option:selected").val();
    });

    $("#btnGetData").click(function () {
        var strategicLocationID = $("#ddlStrategicLocation option:selected").val();
        var parameter = $("#ddlParameters option:selected").val();
        var fromYear = $("#ddlFromYear option:selected").val();
        var toYear = $("#ddlToYear option:selected").val();
        var eqs = $("#ddlEQS option:selected").val();

        if (strategicLocationID != "" && parameter != "" && fromYear != "" && toYear != "") {
            year_Y_axis = $('#ddlParameters option:selected').val() + ' ' + $('#ddlUnit option:selected').text();
            GetFlotData(strategicLocationID, parameter, fromYear, toYear, eqs);
        } else {
            Notification('Information', 'Please select appropriate parameters to get time series graph report.', 'info');
        }
    });

    $("#btnShow").click(function () {
        ShowDialog();
    });

    $("#btnActionApply").click(function () {
        var expType = $('#ddlActions').val();
        if (expType == "") {
            Notification("Warning", "Please select an action!", "warning");
            $('#ddlActions').focus();
        } else if (expType == "xls") {
            $('#tblYearSeriesData').tableExport({
                type: 'excel',
                //ignoreColumn: [0, 7],
                escape: 'false'
            });
        } else if (expType == "doc") {
            $('#tblYearSeriesData').tableExport({
                type: 'doc',
                //ignoreColumn: [0, 7],
                escape: 'false'
            });
        } else if (expType == "csv") {
            $('#tblYearSeriesData').tableExport({
                type: 'csv',
                //ignoreColumn: [0, 7],
                escape: 'false'
            });
        }
    });
});

function GetFlotData(strategicLocationID, parameter, fromYear, toYear, EQS) {
    $.ajax({
        type: "GET",
        url: "../Common/GetFlotGraphYearSeriesData",
        data: {
            StrategicLocationID: strategicLocationID,
            Parameter: parameter,
            StartYear: fromYear,
            EndYear: toYear,
            EQS: EQS
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true,
        success: function (data) {
            var count = Object.keys(data).length;

            if (count > 0) {
                objJsonData = JSON.parse(JSON.stringify(data));
                //console.log(JSON.stringify(data));
                plot = $.plot($("#series-flot-chart"), objJsonData, year_legend_container_options);
                $("#lblReportOfGraphicalView").text(year_Y_axis);

                //dynamically yAxis Labeling
                var axes = plot.getAxes();
                axes.yaxis.options.axisLabel = year_Y_axis;
                plot.setupGrid();
                plot.draw();

                PrintTabularFormatData(objJsonData);

                $("#graphReport").show();
            } else {
                $("#tblBodyYearSeriesData").html('');
                $("#graphReport").hide();
                Notification('No Data', 'Sorry, no data found in this related parameter.', 'warning');
            }
        }, error: function (jqXhr, exception) {
            var errorMsg = AjaxErrorHandle(jqXhr, exception);
            Notification('Error', errorMsg, 'danger');
        }
    });
}

function PrintTabularFormatData(data) {
    var htmlContent = '';

    $("#lblStrategicLocationsName").text($("#ddlStrategicLocation option:selected").text());
    $("#lblParameterName").text(year_Y_axis);
    var HtmlForEQS = '';
    var minVal = 0, maxVal = 0, val = 0;

    $.each(data, function (key, value) {
        var jsonData = JSON.stringify(value);
        var objData = $.parseJSON(jsonData);
        var label = objData.label;

        if (label.indexOf('MIN') == -1 && label.indexOf('MAX') == -1 && label.indexOf('E.Q.S') == -1) {
            htmlContent += '<tr><td class="text-left">' + label + '</td>';
            var dataArray = objData.data;
            $.each(dataArray, function (valKey, dataValue) {
                if (dataValue[0] !== 0) {
                    if (parseFloat(dataValue[1]).toFixed(2) > 0) {
                        htmlContent += '<td class="text-right">' + parseFloat(dataValue[1]).toFixed(2) + '</td>';
                    } else {
                        htmlContent += '<td class="text-center">-</td>';
                    }
                }
            });

            htmlContent += '</tr>';
        }

        if (label.indexOf('MIN') > -1 || label.indexOf('MAX') > -1) {
            var dataArray = objData.data;
            $.each(dataArray, function (valKey, dataValue) {
                if (dataValue[0] !== 0) {
                    if (label.indexOf('MIN') > -1) {
                        minVal = parseFloat(dataValue[1]).toFixed(2);
                    }

                    if (label.indexOf('MAX') > -1) {
                        maxVal = parseFloat(dataValue[1]).toFixed(2);
                    }
                }
            });
        }

        if (label.indexOf('E.Q.S') > -1 && label.indexOf('MIN') == -1 && label.indexOf('MAX') == -1) {
            var dataArray = objData.data;
            $.each(dataArray, function (valKey, dataValue) {
                if (dataValue[0] !== 0) {
                    val = parseFloat(dataValue[1]).toFixed(2);
                }
            });
        }
    });

    if (val === 0) {
        HtmlForEQS = '<tr><td colspan="13" class="text-center bold">E.Q.S for ' + $("#ddlParameters option:selected").val() + ' from ' + $("#ddlEQS option:selected").text().toLowerCase() + ' units ' + minVal + ' - ' + maxVal + ' ' + $('#ddlUnit option:selected').text().toLowerCase() + '</td></tr>';
    } else {
        HtmlForEQS = '<tr><td colspan="13" class="text-center bold">E.Q.S for ' + $("#ddlParameters option:selected").val() + ' from ' + $("#ddlEQS option:selected").text().toLowerCase() + ' units ' + val + ' ' + $('#ddlUnit option:selected').text().toLowerCase() + '</td></tr>';
    }
    htmlContent += HtmlForEQS;
    $("#tblBodyYearSeriesData").html(htmlContent);
}

var year_legend_container_options = {
    legend: {
        show: true,
        noColumns: 4,
        container: $('#series-legend-container')
    },
    valueLabels: {
        show: true
    },
    series: {
        lines: {
            show: true,
            lineWidth: 2,
            //fill: true,
            //fillOpacity: .3,
            align: 'center'
        },
        points: {
            show: true
        },
        shadowSize: 0
    },
    grid: {
        hoverable: true,
        clickable: true,
        tickColor: "#F8F8F8",
        borderWidth: 0,
        margin: 15
    },
    xaxis: {
        ticks: [[0, ''], [1, 'Jan'], [2, 'Feb'], [3, 'Mar'], [4, 'Apr'], [5, 'May'], [6, 'Jun'], [7, 'Jul'], [8, 'Aug'], [9, 'Sep'], [10, 'Oct'], [11, 'Nov'], [12, 'Dec']],
        tickDecimals: 0,
        axisLabel: 'Months',
        axisLabelUseCanvas: true,
        axisLabelFontSizePixels: 12,
        axisLabelFontFamily: 'Verdana, Arial'
    },
    yaxes: [{
        position: "left",
        axisLabelUseCanvas: true,
        axisLabelFontSizePixels: 12,
        axisLabelFontFamily: 'Verdana, Arial',
        axisLabelPadding: 20,
        tickFormatter: function (v, axis) {
            //rony
            if (v % 5 == 0) {
                return v;
            } else {
                return "";
            }
        }
    }, {
        position: "right",
        axisLabel: "Time Series",
        axisLabelUseCanvas: true,
        axisLabelFontSizePixels: 12,
        axisLabelFontFamily: 'Verdana, Arial',
        axisLabelPadding: 20
    }],
    tooltip: true,
    tooltipOpts: {
        content: "%x: %y.2",
        content: function (label, xval, yval, flotItem) {
            var errString;
            try {
                errString = this.axis[xval][2].index;
            } catch (err) {
                errString = err.message;
            }
            errString = errString.replace(/\D/g, '');
            var monthName = GetMonthName(parseInt(errString));
            //console.log(monthName);
            if (monthName != '')
                return "<span>" + year_Y_axis + "</span> <span>" + yval + "</span> in <span>" + monthName + ", " + label + "</span>";
            else
                return "<span>" + year_Y_axis + "</span> <span>" + yval + "</span>";
        },
        defaultTheme: false,
        shifts: {
            x: 0,
            y: 20
        }
    }
};

function GetMonthName(index) {
    switch (index) {
        case 1:
            return "January";
        case 2:
            return "February";
        case 3:
            return "March";
        case 4:
            return "April";
        case 5:
            return "May";
        case 6:
            return "June";
        case 7:
            return "July";
        case 8:
            return "August";
        case 9:
            return "September";
        case 10:
            return "October";
        case 11:
            return "November";
        case 12:
            return "December";

        default:
            return "";
    }
}

function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }

    return color;
}