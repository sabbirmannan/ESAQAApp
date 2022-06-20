function Print(divName) {
    var fileName = moment(new Date()).format('YYYYMMDDHHmmss');

    html2canvas($('#' + divName), {
        onrendered: function (canvas) {
            var img = canvas.toDataURL("image/png");
            download(img, fileName + ".png", "image/png");
        },
        height: 500
    });
}

var objJsonData;
var plot;

$(document).ready(function () {
    $("#graphReport, #maxGraphReport, #minGraphReport, #averageGraphReport").hide();

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

        if (strategicLocationID != "" && parameter != "" && fromYear != "" && toYear != "") {
            GetFlotData(strategicLocationID, parameter, fromYear, toYear);
        } else {
            Notification('Information', 'Please select appropriate parameters to get time series graph report.', 'info');
        }
    });
});

var data1, xAxis1, data2, xAxis2, data3, xAxis3, dataAll;
var jsonData1, jsonXAxis1, jsonData2, jsonXAxis2, jsonData3, jsonXAxis3, jsonAllData;
var myTicks = [];

function GetFlotData(strategicLocationID, parameter, fromYear, toYear) {
    $.ajax({
        type: "GET",
        url: "/Common/GetFlotTimeSeriesData",
        data: {
            StrategicLocationID: strategicLocationID,
            Parameter: parameter,
            FromYear: fromYear,
            ToYear: toYear
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true,
        success: function (data) {
            try {
                var dataGroup = data.split('#');

                data1 = dataGroup[0].split('*')[0];
                xAxis1 = dataGroup[0].split('*')[1];
                data2 = dataGroup[1].split('*')[0];
                xAxis2 = dataGroup[1].split('*')[1];
                data3 = dataGroup[2].split('*')[0];
                xAxis3 = dataGroup[2].split('*')[1];
                dataAll = '[' + data1 + ',' + data2 + ',' + data3 + ']';

                jsonData1 = JSON.parse(data1.toString());
                jsonData2 = JSON.parse(data2.toString());
                jsonData3 = JSON.parse(data3.toString());
                jsonAllData = JSON.parse(dataAll.toString());

                var ddd = xAxis3.split(',');
                for (var i = 0; i < ddd.length; i++) {
                    myTicks.push([i, ddd[i]]);
                }

                if ($('#ddlReportViewActions option:selected').val() === "merge") {
                    MergedViewGraph(jsonData1, jsonData2, jsonData3);

                    $("#graphReport").show();
                    $("#maxGraphReport, #minGraphReport, #averageGraphReport").hide();
                } else if ($('#ddlReportViewActions option:selected').val() === "split") {
                    SplitedViewGraph(jsonData1, jsonData2, jsonData3);

                    $("#graphReport").hide();
                    $("#maxGraphReport, #minGraphReport, #averageGraphReport").show();
                } else {
                    Notification('Information', 'Please select a report view.', 'info');
                    $('#ddlReportViewActions').focus();
                    $("#graphReport, #maxGraphReport, #minGraphReport, #averageGraphReport").hide();
                }

                var lable = "<b>" + $("#ddlParameters option:selected").text() + "</b> parameter time series of <b>" + $("#ddlStrategicLocation option:selected").text() + "</b> from <b>" + $("#ddlFromYear option:selected").val() + "</b>-<b>" + $("#ddlToYear option:selected").val() + "</b>";
                $(".lblReportOfGraphicalView").html(lable);
            } catch (err) {
                Notification('An error occured', err.message, 'danger');
            }
        }, error: function (jqXhr, exception) {
            var errorMsg = AjaxErrorHandle(jqXhr, exception);
            Notification('Error', errorMsg, 'danger');
        }
    });
}

function MergedViewGraph(jsonData1, jsonData2, jsonData3) {
    $("#pH-flot-chart").length && $.plot($("#pH-flot-chart"), [
        { data: jsonData1, color: "#F42A41" }, //max
        { data: jsonData2, color: "#8EC165" }, //min
        { data: jsonData3, color: "#095BA7" } //average
    ], LineChartOptions);
}

function SplitedViewGraph(jsonData1, jsonData2, jsonData3) {
    $("#max-flot-chart").length && $.plot($("#max-flot-chart"), [{ data: jsonData1, color: "#F42A41" }], LineChartOptions);
    $("#min-flot-chart").length && $.plot($("#min-flot-chart"), [{ data: jsonData2, color: "#8EC165" }], LineChartOptions);
    $("#average-flot-chart").length && $.plot($("#average-flot-chart"), [{ data: jsonData3, color: "#095BA7" }], LineChartOptions);
}

var LineChartOptions = {
    valueLabels: {
        show: true
    },
    series: {
        lines: {
            show: true,
            //fill: true,
            //fillColor: {
            //    colors: [{
            //        opacity: 0.0
            //    }, {
            //        opacity: 0.7
            //    }]
            //},
            lineWidth: 1
        },
        points: {
            radius: 3,
            show: true
        },
        grow: {
            active: true,
            steps: 50
        },
        shadowSize: 2
    },
    grid: {
        hoverable: true,
        clickable: true,
        tickColor: "#f0f0f0",
        borderWidth: 2,
        color: '#f0f0f0'
    },
    colors: ["#65bd77"],
    xaxis: {
        mode: "datetime",
        minTickSize: [1, "month"],
        timeformat: " %y",
        axisLabel: "Year",
        ticks: myTicks
    },
    yaxis: {
        position: "left",
        axisLabel: "Value",
        axisLabelUseCanvas: true,
        ticks: 1,
        tickSize: 1,
        tickSize: 1
    },
    tooltip: true,
    tooltipOpts: {
        content: "chart: %x.1 is %y.4",
        defaultTheme: false,
        shifts: {
            x: 0,
            y: 20
        }
    }
};