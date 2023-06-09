﻿using System.Web;
using System.Web.Optimization;

namespace DoctorManage
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                        "~/Scripts/popper.js"));

            bundles.Add(new Bundle("~/bundles/jquery.dataTables").Include( 
                      
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/lib/datatables.net/jquery.dataTables.js",
                      "~/Scripts/lib/datatables.net/dataTables.responsive.js"));

            bundles.Add(new StyleBundle("~/Content/jquery.dataTables").Include(
                    "~/Content/jquery.dataTables.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/responsive.bootstrap.min.css",
                      
                      "~/Content/site.css"));
        }
    }
}
