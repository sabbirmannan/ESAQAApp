function Print(divName) {
    var River = $("#ddlRivers option:selected").text();
    var Parameter = $("#ddlParameters option:selected").val();
    var Year = $("#ddlYear option:selected").val();
    var fileName = River + '_' + Parameter + '_' + Year + '_' + moment(new Date()).format('YYYYMMDDHHmmss');

    html2canvas($('#' + divName), {
        onrendered: function (canvas) {
            var img = canvas.toDataURL("image/png");
            //window.open(img);
            download(img, fileName + ".png", "image/png");
        },
        height: 500
    });
}

GetFlotData(5, 'BOD', 2003, 'preview_canvas_flot_chart');

$("#ddlParameters").select2({
    placeholder: "Parameters...",
    allowClear: true
}).on("change", function (e) {
    var RiverID = $("#ddlRivers option:selected").val();
    var ParameterID = $("#ddlParameters option:selected").val();
    var Year = $("#ddlYear option:selected").val();
    GetFlotData(RiverID, ParameterID, Year, 'preview_canvas_flot_chart');
    //GetFlotPieData(5, 'PhLab', '', 2003, 1);
});

$("#ddlParameters").change(function () {
    var RiverID = $("#ddlRivers option:selected").val();
    var ParameterID = $("#ddlParameters option:selected").val();
    var Year = $("#ddlYear option:selected").val();
    GetFlotData(RiverID, ParameterID, Year, 'preview_canvas_flot_chart');
    //GetFlotPieData(5, 'PhLab', '', 2003, 1);
});

$("#ddlRivers").select2({
    placeholder: "Water source...",
    allowClear: true
}).on("change", function (e) {
    var RiverID = $("#ddlRivers option:selected").val();
    var ParameterID = $("#ddlParameters option:selected").val();
    var Year = $("#ddlYear option:selected").val();
    GetFlotData(RiverID, ParameterID, Year, 'preview_canvas_flot_chart');
});

$("#ddlYear").change(function () {
    var RiverID = $("#ddlRivers option:selected").val();
    var ParameterID = $("#ddlParameters option:selected").val();
    var Year = $("#ddlYear option:selected").val();
    GetFlotData(RiverID, ParameterID, Year, 'preview_canvas_flot_chart');
});

function GetFlotData(LocationID, ParameterID, Year, DivID) {
    $.ajax({
        type: "GET",
        url: "/Common/GetFlotGraphData",
        data: {
            LocationID: LocationID,
            ParamType: ParameterID,
            Year: Year
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true,
        success: function (data) {
            objJsonData = JSON.parse(JSON.stringify(data));
            plot = $.plot($("#" + DivID), objJsonData, preview_legend_container_options);

            //if (ParameterID === 'PhLab') {
            //    plot = $.plot($("#" + DivID), objJsonData, preview_legend_container_options);
            //    //$("#" + DivID).bind("plotclick", function (event, pos, item) {
            //    //if (item) {
            //    //console.log(item);
            //    //GetFlotPieData(5, 'PhLab', 'BOD-flot-chart', 2017, item.dataIndex);
            //    //}
            //    //});
            //}
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            Notification('Error', errorMsg, 'danger');
        }
    });
}

var preview_legend_container_options = {
    legend: {
        show: true,
        noColumns: 4,
        container: $('#preview_legend_container_options')
    },
    valueLabels: {
        show: true
    },
    series: {
        lines: {
            show: true,
            lineWidth: 2,
            align: 'center',
            fill: false
        },
        points: {
            show: true
        },
        shadowSize: 2
    },
    grid: {
        hoverable: true,
        clickable: true,
        tickColor: "#F8F8F8",
        borderWidth: 0
    },
    //colors: ["#616EE1", "#89CB4E", "#F384F2"],
    xaxis: {
        ticks: [[0, ''], [1, 'J'], [2, 'F'], [3, 'M'], [4, 'A'], [5, 'M'], [6, 'J'], [7, 'J'], [8, 'A'], [9, 'S'], [10, 'O'], [11, 'N'], [12, 'D']],
        tickDecimals: 0,
        axisLabelUseCanvas: true
    },
    yaxis: {
        ticks: 1,
        tickSize: 1,
        tickDecimals: 2,
        tickSize: 2
    },
    tooltip: true,
    tooltipOpts: {
        content: function (label, xval, yval) {
            return "%s of %x.1 is: " + yval;
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
        url: "/Common/GetFlotPieGraphData",
        data: { LocationID: LocationID, ParamType: ParamType, Year: Year, Month: Month },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true,
        success: function (data) {
            objJsonData = JSON.parse(JSON.stringify(data));
            console.log(JSON.stringify(data));
            plot = $.plot($("#BOD-flot-pie"), objJsonData, pie_options);
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
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
        content: "%p.0%, %s", // show percentages, rounding to 2 decimal places
        shifts: {
            x: 20,
            y: 0
        },
        defaultTheme: false
    }
};

function ShowDialog() {
    $('#dialog').dialog({
        resizable: false,
        height: 240,
        width: "auto",
        position: {
            my: "center",
            at: "center",
            of: "#Ammonia"
        },
        draggable: true,
        modal: true
    }).parent().draggable({
        containment: '#Ammonia',
        opacity: 0.70
    });
}
