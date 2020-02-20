﻿using System.Web.Optimization;

namespace WCRC
{
    public class BundleConfig
    {
        // 묶음에 대한 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=301862를 참조하세요.
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/FullCalendar/fullcalendarjs").Include(
                      "~/FullCalendar/packages/core/main.js",
                      "~/FullCalendar/packages/daygrid/main.js",
                      "~/FullCalendar/packages-premium/resource-common/main.js",
                      "~/FullCalendar/packages-premium/resource-daygrid/main.js"
                      ));

            // Modernizr의 개발 버전을 사용하여 개발하고 배우십시오. 그런 다음
            // 프로덕션에 사용할 준비를 하고 https://modernizr.com의 빌드 도구를 사용하여 필요한 테스트만 선택하세요.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/FullCalendar/fullcalendarcss").Include(
                     "~/FullCalendar/packages/core/main.css",
                     "~/FullCalendar/packages/daygrid/main.css"
                     ));
        }
    }
}
