using System.Web;
using System.Web.Optimization;

namespace BAC007
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/css/font.css",
                      "~/Scripts/js/select2/select2.css",
                      "~/Scripts/js/select2/theme.css",
                      "~/Scripts/js/fuelux/fuelux.css",
                      "~/Scripts/js/datepicker/datepicker.css",
                      "~/Scripts/js/slider/slider.css",
                      "~/Scripts/js/calendar/bootstrap_calendar.css",
                      "~/Scripts/js/gritter/css/jquery.gritter.css",
                      "~/Content/css/app.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/2.1.1/jquery.min.js",
                        "~/Scripts/js/app.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/charts").Include(
                        "~/Scripts/js/charts/sparkline/jquery.sparkline.min.js",
                        "~/Scripts/js/charts/easypiechart/jquery.easy-pie-chart.js",
                        "~/Scripts/js/charts/flot/jquery.flot.min.js",
                        "~/Scripts/js/charts/flot/jquery.flot.tooltip.min.js",
                        "~/Scripts/js/charts/flot/jquery.flot.resize.js",
                        "~/Scripts/js/charts/flot/jquery.flot.categories.js",
                //"~/Scripts/js/charts/flot/jquery.flot.orderBars.js",
                        "~/Scripts/js/charts/flot/jquery.flot.axislabels.js",
                        "~/Scripts/js/charts/flot/jquery.flot.pie.min.js",
                        "~/Scripts/js/charts/flot/jquery.flot.grow.js",
                        "~/Scripts/js/charts/flot/download.js",
                        "~/Scripts/js/charts/flot/demo.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/calendar").Include(
                        "~/Scripts/js/calendar/bootstrap_calendar.js",
                        "~/Scripts/js/gritter/js/jquery.gritter.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/sortable").Include(
                        "~/Scripts/js/sortable/jquery.sortable.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/plugin").Include(
                        "~/Scripts/js/fuelux/fuelux.js", //fuelux
                        "~/Scripts/js/parsley/parsley.min.js", //parsley
                        "~/Scripts/js/datepicker/bootstrap-datepicker.js", //datepicker
                //"~/Scripts/js/datepicker/jquery-ui-timepicker-addon.js", //timepicker
                        "~/Scripts/js/slider/bootstrap-slider.js", //slider
                        "~/Scripts/js/file-input/bootstrap-filestyle.min.js", //file-input
                        "~/Scripts/moment.min.js", //moment > combodate, datetime formatting
                        "~/Scripts/s/combodate/combodate.js", //combodate
                        "~/Scripts/js/select2/select2.min.js", //select2 dropdown
                        "~/Scripts/js/wysiwyg/jquery.hotkeys.js", //wysiwyg
                        "~/Scripts/js/wysiwyg/bootstrap-wysiwyg.js", //wysiwyg
                        "~/Scripts/js/markdown/epiceditor.min.js", //markdown
                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/js/app.plugin.js",
                        "~/Scripts/js/export.js",
                        "~/Scripts/print.report.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/tableexport").Include(
                        "~/Scripts/js/tableexport/tableExport.js",
                        "~/Scripts/js/tableexport/jquery.base64.js",
                        "~/Scripts/js/tableexport/html2canvas.js",
                        "~/Scripts/js/tableexport/sprintf.js",
                        "~/Scripts/js/tableexport/jspdf.js",
                        "~/Scripts/js/tableexport/base64.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/clientbusiness").Include(
                        "~/Scripts/my.menu.current.js",
                        "~/Scripts/common.js"
                        ));

            bundles.Add(new StyleBundle("~/bundles/intro").Include(
                      "~/Scripts/js/intro/introjs.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/intro-js").Include(
                        "~/Scripts/js/intro/intro.min.js"
                        ));

            //Gallery contents:css start
            bundles.Add(new StyleBundle("~/gallery/css").Include(
                      "~/Content/gallery/css/prettyPhoto.css",
                      "~/Content/gallery/css/camera.css",
                      "~/Content/gallery/css/gallery-bootrap.css",
                      "~/Content/gallery/css/theme.css",
                      "~/Content/gallery/css/skins/tango/skin.css",
                      "~/Content/gallery/css/bootstrap-responsive.css"
                      ));

            bundles.Add(new ScriptBundle("~/gallery/js").Include(
                        "~/Content/gallery/js/jquery.easing.1.3.js",
                        "~/Content/gallery/js/jquery.mobile.customized.min.js",
                        "~/Content/gallery/js/camera.js",
                        "~/Content/gallery/js/bootstrap.js",
                        "~/Content/gallery/js/jquery.prettyPhoto.js",
                        "~/Content/gallery/js/jquery.jcarousel.js",
                        "~/Content/gallery/js/jquery.tweet.js",
                        "~/Content/gallery/js/myscript.js"
                        ));

            #region Map CSS :: start
            bundles.Add(new StyleBundle("~/leaflet/styles").Include(
                      "~/Content/css/leaflet.css",
                      "~/Content/css/leaflet.draw.css",
                      "~/Content/css/custom.css",
                      "~/Content/css/L.Control.Pan.css",
                      "~/Content/css/leaflet.label.css"
                      ));
            #endregion

            #region Map Scripts :: start
            bundles.Add(new ScriptBundle("~/leaflet/js").Include(
                //leaflet
                        "~/Scripts/map/leaflet-src.js",
                        "~/Scripts/map/leaflet.ajax.js",
                //leaflet for labeling
                        "~/Scripts/map/esri-leaflet.js",
                //leaflet for control pan
                        "~/Scripts/map/L.Control.Pan.js",
                        "~/Scripts/map/Leaflet.draw.js",
                        "~/Scripts/map/Leaflet.Draw.Event.js",
                        "~/Scripts/map/leaflet.label.js",                //leaflet for toolbar
                        "~/Scripts/map/Toolbar.js",
                        "~/Scripts/map/Tooltip.js",
                //leaflet extension
                        "~/Scripts/map/GeometryUtil.js",
                        "~/Scripts/map/LatLngUtil.js",
                        "~/Scripts/map/LineUtil.Intersect.js",
                        "~/Scripts/map/Polygon.Intersect.js",
                        "~/Scripts/map/Polyline.Intersect.js",
                        "~/Scripts/map/TouchEvents.js",
                //leaflet for drawing
                        "~/Scripts/map/DrawToolbar.js",
                        "~/Scripts/map/Draw.Feature.js",
                        "~/Scripts/map/Draw.SimpleShape.js",
                        "~/Scripts/map/Draw.Polyline.js",
                        "~/Scripts/map/Draw.Circle.js",
                        "~/Scripts/map/Draw.Marker.js",
                        "~/Scripts/map/Draw.Polygon.js",
                        "~/Scripts/map/Draw.Rectangle.js",
                //leaflet for edit in map
                        "~/Scripts/map/EditToolbar.js",
                        "~/Scripts/map/EditToolbar.Edit.js",
                        "~/Scripts/map/EditToolbar.Delete.js",
                        "~/Scripts/map/Control.Draw.js",
                        "~/Scripts/map/Edit.Poly.js",
                        "~/Scripts/map/Edit.SimpleShape.js",
                        "~/Scripts/map/Edit.Circle.js",
                        "~/Scripts/map/Edit.Rectangle.js",
                        "~/Scripts/map/Edit.Marker.js",
                //leaflet for loader/ spin
                        "~/Scripts/map/spin.js",
                        "~/Scripts/map/leaflet.spin.js",
                        "~/Scripts/map/Leaflet.Search.js",
                        "~/Scripts/map/FunctionButton.js",
                        "~/Scripts/map/ExportButton.js",
                //leaflet for print map
                        "~/Scripts/map/leaflet.easyPrint.js",
                        "~/Scripts/map/leaflet.map.print.js"
                        ));
            #endregion

            #region Tree List
            bundles.Add(new StyleBundle("~/treelist/css").Include(
                      "~/Scripts/js/treelist/jquery.collapsibleCheckboxTree.css"
                      ));

            bundles.Add(new ScriptBundle("~/treelist/js").Include(
                        "~/Scripts/js/treelist/jquery.collapsibleCheckboxTree.js"
                        ));
            #endregion
        }
    }
}
