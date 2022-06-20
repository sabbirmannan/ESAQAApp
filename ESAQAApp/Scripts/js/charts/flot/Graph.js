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
    $("#divYearOfSL").hide();
    $("#divMonthOfSL").hide();

    $("#ddlYear").val('');
    $("#ddlMonth").val('');
    $("#ddlRiver").val('');
    $("#ddlParameters").val('');
    $("#ddlUnit").val('');
    $("#ddlEQS").val('');

    $("#btnGetData").click(function () {
        var year = $("#ddlYear option:selected").val();
        var MonthID = $("#ddlMonth option:selected").val();
        var riverID = $("#ddlRiver option:selected").val();
        var riverName = $("#ddlRiver option:selected").text();
        var parameters = $("#ddlParameters option:selected").val();
        var eqs = $("#ddlEQS option:selected").val();

        var strategicLocation = $("#ddlStrategicLocation option:selected").val();
        var reportType = $("#ddlReportType option:selected").val();
        year_Y_axis = $('#ddlParameters option:selected').val() + ' ' + $('#ddlUnit option:selected').text();

        if (MonthID == "") {
            GetFlotData(riverID, riverName, parameters, 'pH-flot-chart', year, eqs);

            $("#divYearOfSL").show();
            $("#divMonthOfSL").hide();
            $('.legendLabel').removeClass('legendLabelCustom');
        } else {
            GetFlotDataOfMonth(year, MonthID, riverID, parameters, eqs, 'pH-flot-chart');

            $("#divYearOfSL").hide();
            $("#divMonthOfSL").show();
        }

        $("#graphReport").show();
    });

    $("#ddlYear").select2({
        placeholder: "Select a year...",
        allowClear: true
    }).on("change", function (e) {
        var value = $("#ddlYear option:selected").val();
        //$("#HydrologicalInformation_RiverFlowCondition").val(rfc);
    });

    $("#ddlRiver").select2({
        placeholder: "Water source...",
        allowClear: true
    }).on("change", function (e) {
        var value = $("#ddlRiver option:selected").val();
        //$("#HydrologicalInformation_RiverFlowCondition").val(rfc);
    });

    //$("#ddlStrategicLocation").select2({
    //    placeholder: "Sample location...",
    //    allowClear: true
    //}).on("change", function (e) {
    //    var value = $("#ddlStrategicLocation option:selected").val();
    //    //$("#HydrologicalInformation_RiverFlowCondition").val(rfc);
    //});

    $("#ddlMonth").select2({
        placeholder: "Select a month...",
        allowClear: true
    }).on("change", function (e) {
        var value = $("#ddlMonth option:selected").val();
    });

    $("#ddlReportType").select2({
        placeholder: "View type...",
        allowClear: true
    }).on("change", function (e) {
        var value = $("#ddlReportType option:selected").val();
    });

    $("#ddlParameters").select2({
        placeholder: "Parameters...",
        allowClear: true
    }).on("change", function (e) {
        var value = $("#ddlParameters option:selected").val();
        $("#ddlUnit").val(value);
    });

    $("#ddlEQS").select2({
        placeholder: "E.Q.S...",
        allowClear: true
    }).on("change", function (e) {
        var value = $("#ddlEQS option:selected").val();
    });

    $("#btnActionApply").click(function () {
        var expType = $('#ddlActions').val();
        if (expType == "") {
            Notification("Warning", "Please select an action!", "warning");
            $('#ddlActions').focus();
        } else if (expType == "pdf") {
            $('#tblPrintFlotTable').tableExport({
                type: 'pdf',
                //ignoreColumn: [0, 7],
                htmlContent: 'false',
                pdfFontSize: 6,
                pdfLeftMargin: 15,
                escape: 'false'
            });
        } else if (expType == "xls") {
            $('#tblPrintFlotTable').tableExport({
                type: 'excel',
                //ignoreColumn: [0, 7],
                escape: 'false'
            });
        } else if (expType == "doc") {
            $('#tblPrintFlotTable').tableExport({
                type: 'doc',
                //ignoreColumn: [0, 7],
                escape: 'false'
            });
        } else if (expType == "csv") {
            $('#tblPrintFlotTable').tableExport({
                type: 'csv',
                //ignoreColumn: [0, 7],
                escape: 'false'
            });
        }
    });
});

function GetFlotData(riverID, riverName, ParamType, DivID, Year, EQS) {
    $.ajax({
        type: "GET",
        //url: '@Url.Action("GetFlotGraphData", "Common")',
        url: "../Common/GetFlotGraphData",
        data: {
            LocationID: riverID,
            ParamType: ParamType,
            Year: Year,
            EQS: EQS
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true,
        success: function (data) {
            objJsonData = JSON.parse(JSON.stringify(data));
            PrintFlotTable(data, riverName, ParamType);

            plot = $.plot($("#pH-flot-chart"), objJsonData, year_legend_container_options);
            //dynamically yAxis Labeling
            var axes = plot.getAxes();
            axes.yaxis.options.axisLabel = year_Y_axis;
            plot.setupGrid();
            plot.draw();

            console.log(JSON.stringify(objJsonData));
            $("#lblReportOfGraphicalView").text(ParamType);
        }, error: function (jqXhr, exception) {
            var errorMsg = AjaxErrorHandle(jqXhr, exception);
            Notification('Error', errorMsg, 'danger');
        }
    });
}

var monthData = [], eqsArray = [], eqsMinArray = [], eqsMaxArray = [];
var monthTicks = [], label = [];

function GetFlotDataOfMonth(Year, Month, RiverID, Parameter, EQS, DivID) {
    $.ajax({
        type: "GET",
        //url: '@Url.Action("GetFlotGraphDataOfMonth", "Common")',
        url: "../Common/GetFlotGraphDataOfMonth",
        data: {
            Year: Year,
            Month: Month,
            RiverID: RiverID,
            Parameter: Parameter,
            EQS: EQS
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true,
        success: function (responseData) {
            monthData = [], eqsArray = [], eqsMinArray = [], eqsMaxArray = [];
            label = [];
            tabular = [];

            var data = responseData.split('rossnebula')[0];
            var unitData = responseData.split('rossnebula')[1];

            var param, unit, eqs, minEqs, maxEqs;
            if (unitData != '0') {
                param = unitData.split('###')[0];
                unit = unitData.split('###')[1];
                eqs = unitData.split('###')[2];
                minEqs = (eqs.indexOf('-') > -1) ? eqs.split('-')[0] : '';
                maxEqs = (eqs.indexOf('-') > -1) ? eqs.split('-')[1] : '';
            }

            if (data.length > 0) {
                try {
                    var strMonthData = data.split('***')[0];
                    var strXAxis = data.split('***')[1];

                    label = strMonthData.split('===');
                    monthData.push([0, 0]);
                    for (var i = 0; i < label.length; i++) {
                        monthData.push([i + 1, parseFloat(label[i]).toFixed(4)]);
                    }
                    //console.log(JSON.stringify(monthData));

                    //generating eqs
                    if (eqs != '' && minEqs == '' && maxEqs == '') {
                        for (var i = 0; i <= label.length; i++) {
                            eqsArray.push([i, parseFloat(eqs).toFixed(4)]);
                        }
                    } else if (minEqs != '' && maxEqs != '') {
                        for (var i = 0; i <= label.length; i++) {
                            eqsMinArray.push([i, parseFloat(minEqs).toFixed(4)]);
                            eqsMaxArray.push([i, parseFloat(maxEqs).toFixed(4)]);
                        }
                    }
                    //

                    label = strXAxis.split('===');
                    monthTicks.push([0, '0']);
                    for (var i = 0; i < label.length; i++) {
                        monthTicks.push([i + 1, label[i]]);
                    }
                    //console.log(JSON.stringify(monthTicks));

                    for (var i = 0; i <= label.length; i++) {
                        var tabularData = {
                            "Name": monthTicks[i][1],
                            "Value": monthData[i][1]
                        };

                        tabular.push(tabularData);
                    }
                    //console.log(JSON.stringify(monthData));

                    $("#month-flot-chart, div.flot-x-axis.flot-x1-axis.xAxis.x1Axis").html('');
                    if (eqsArray.length > 0) {
                        plot = $.plot($("#month-flot-chart"), [{ data: monthData, color: getRandomColor() }, { data: eqsArray, color: getRandomColor(), label: "E.Q.S" }], month_legend_container_options);
                    } else if (eqsMinArray.length > 0 && eqsMaxArray.length > 0) {
                        plot = $.plot($("#month-flot-chart"), [{ data: monthData, color: getRandomColor() }, { data: eqsMinArray, color: getRandomColor(), label: "E.Q.S (MIN)", points: { symbol: "square", fillColor: "#058DC7" } }, { data: eqsMaxArray, color: getRandomColor(), label: "E.Q.S (MAX)", points: { symbol: "square", fillColor: "#ED561B" } }], month_legend_container_options);
                    } else {
                        plot = $.plot($("#month-flot-chart"), [{ data: monthData, color: getRandomColor() }], month_legend_container_options);
                    }

                    //dynamically yAxis Labeling :: rony
                    var axes = plot.getAxes();
                    axes.yaxis.options.axisLabel = year_Y_axis;
                    plot.setupGrid();
                    plot.draw();

                    $("#lblReportOfGraphicalView").text(Parameter);

                    PrintFlotTableForMonth(tabular);

                    $('.legendLabel').addClass('legendLabelCustom');
                } catch (err) {
                    Notification('An error occured', err.message, 'danger');
                }
            } else {
                Notification('No Data', 'No data found.', 'warning');
            }
        }, error: function (jqXhr, exception) {
            var errorMsg = AjaxErrorHandle(jqXhr, exception);
            Notification('Error', errorMsg, 'danger');
        }
    });
}

function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }

    return color;
}

function PrintFlotTable(data, riverName, ParamType) {
    var htmlContent;
    var counter = 0;

    if (data.length > 0) {
        htmlContent = '<table id="tblPrintFlotTable" class="table table-striped b-t b-light">';
        htmlContent += '<thead>';
        htmlContent += '<tr class="text-left"><th>Parameter</th><th colspan="13">' + ParamType + '</th></tr>';
        htmlContent += '<tr class="text-left"><th>River</th><th colspan="13">' + riverName + '</th></tr>';
        htmlContent += '<tr class="text-left"><th>Year</th><th colspan="13">' + $("#ddlYear option:selected").val() + '</th></tr>';
        htmlContent += '<tr><th class="text-left">Serial</th><th>Strategic Locations</th><th class="text-right">Jan</th><th class="text-right">Feb</th><th class="text-right">Mar</th><th class="text-right">Apr</th><th class="text-right">May</th><th class="text-right">Jun</th><th class="text-right">Jul</th><th class="text-right">Aug</th><th class="text-right">Sep</th><th class="text-right">Oct</th><th class="text-right">Nov</th><th class="text-right">Dec</th></tr>';
        htmlContent += '</thead>';
        htmlContent += '<tbody id="tblPrintFlotTableBody" class="small">';

        $.each(data, function (key, value) {
            var jsonData = JSON.stringify(value);
            var objData = $.parseJSON(jsonData);
            var label = objData.label;

            htmlContent += '<tr><td class="text-left">' + (++counter) + '</td><td class="text-left">' + label + '</td>';
            var dataArray = objData.data;
            $.each(dataArray, function (valKey, dataValue) {
                if (dataValue[0] !== 0) {
                    htmlContent += '<td class="text-right">' + parseFloat(dataValue[1]).toFixed(2) + '</td>';
                }
            });

            htmlContent += '</tr>';
        });

        htmlContent += '</tbody></table>';
        $("#divGenerateTable").html(htmlContent);
    }
}

function PrintFlotTableForMonth(data) {
    var htmlContent;
    var counter = 0;
    //console.log(JSON.stringify(data));

    if (data.length > 0) {
        htmlContent = '<table id="tblPrintFlotTable" class="table table-striped b-t b-light">';
        htmlContent += '<thead>';
        htmlContent += '<tr class="text-left"><th width="100px">Parameter</th><th colspan="13">' + $("#ddlParameters option:selected").val() + '</th></tr>';
        htmlContent += '<tr class="text-left"><th>River</th><th colspan="13">' + $("#ddlRiver option:selected").text() + '</th></tr>';
        htmlContent += '<tr class="text-left"><th>Month</th><th colspan="13">' + $("#ddlMonth option:selected").val() + ', ' + $("#ddlYear option:selected").val() + '</th></tr>';
        htmlContent += '<tr><th class="text-left">Serial</th><th class="text-left">Strategic Locations</th><th class="text-right">' + $("#ddlParameters option:selected").val() + '</th></tr>';
        htmlContent += '</thead>';
        htmlContent += '<tbody id="tblPrintFlotTableBody" class="small">';

        $.each(data, function (key, value) {
            if (value.Name != '0') {
                htmlContent += '<tr><td class="text-left">' + (++counter) + '</td><td class="text-left">' + value.Name + '</td><td class="text-right">' + value.Value + '</td></tr>';
            }
        });

        htmlContent += '</tbody></table>';
        $("#divGenerateTable").html(htmlContent);
    }
}

var year_legend_container_options = {
    legend: {
        show: true,
        noColumns: 4,
        container: $('#pH-legend-container')
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
        borderWidth: 0
    },
    xaxis: {
        ticks: [[0, ''], [1, 'Jan'], [2, 'Feb'], [3, 'Mar'], [4, 'Apr'], [5, 'May'], [6, 'Jun'], [7, 'Jul'], [8, 'Aug'], [9, 'Sep'], [10, 'Oct'], [11, 'Nov'], [12, 'Dec']],
        tickDecimals: 0,
        axisLabel: 'Months',
        axisLabelUseCanvas: true
    },
    yaxis: {
        position: "left",
        axisLabel: "Values",
        ticks: 1,
        tickSize: 2,
        tickDecimals: 2,
        tickFormatter: function (v, axis) {
            //rony
            if (v % 10 == 0) {
                return v;
            } else {
                return "";
            }
        }
        //transform: function (v) { return -v; },  
        //inverseTransform: function (v) { return -v; }  
    },
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
            return "<span>" + label + "</span> <span>" + yval + "</span> in <span>" + monthName + "</span>";
        },
        defaultTheme: false,
        shifts: {
            x: 0,
            y: 20
        }
    }
};

var month_legend_container_options = {
    valueLabels: {
        show: true
    },
    legend: {
        show: true,
        labelBoxBorderColor: "#000000",
        position: "nw"
    },
    series: {
        lines: {
            show: true,
            fill: false,
            lineWidth: 2
        },
        //points: {
        //    show: true
        //},
        grow: {
            active: true,
            steps: 5
        },
        shadowSize: 0
    },
    grid: {
        hoverable: true,
        clickable: true,
        tickColor: "#f0f0f0",
        borderWidth: 1,
        color: '#f0f0f0',
        labelMargin: 8
    },
    colors: ["#65bd77"],
    xaxis: {
        mode: "datetime",
        minTickSize: [1, "month"],
        timeformat: " %y",
        axisLabel: "Strategic Location",
        ticks: monthTicks
    },
    yaxis: {
        position: "left",
        axisLabel: "Value",
        axisLabelUseCanvas: true,
        ticks: 1,
        tickSize: 15,
        tickDecimals: 2,
        tickFormatter: function (v, axis) {
            //rony
            if (v % 15 == 0) {
                return v;
            } else {
                return "";
            }
        }
    },
    tooltip: true,
    tooltipOpts: {
        content: function (label, xval, yval, flotItem) {
            return "<span>" + monthTicks[xval][1] + "</span>: <span>" + yval + "</span>";
        },
        defaultTheme: false,
        shifts: {
            x: 0,
            y: 20
        }
    }
};

function GetFlotPieData(LocationID, ParamType, DivID, Year, Month) {
    $.ajax({
        type: "GET",
        url: '@Url.Action("GetFlotPieGraphData", "Common")',
        //url: "/Common/GetFlotPieGraphData",
        data: {
            LocationID: LocationID,
            ParamType: ParamType,
            Year: Year,
            Month: Month
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true,
        success: function (data) {
            objJsonData = JSON.parse(JSON.stringify(data));
            //console.log(JSON.stringify(data));
            plot = $.plot($("#BOD-flot-pie"), objJsonData, pie_options);
        }, error: function (jqXhr, exception) {
            var errorMsg = AjaxErrorHandle(jqXhr, exception);
            Notification('Error', errorMsg, 'danger');
        }
    });
}

var pie_options = {
    series: {
        pie: {
            combine: {
                color: "#999",
                threshold: 0.05
            },
            innerRadius: 0.3,
            show: true
        }
    },
    legend: {
        show: false
    },
    grid: {
        hoverable: true,
        clickable: false
    },
    tooltip: true,
    tooltipOpts: {
        content: function (label, x, y) {
            return y + ", " + label;
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