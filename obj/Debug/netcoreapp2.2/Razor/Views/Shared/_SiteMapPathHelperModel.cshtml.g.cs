#pragma checksum "C:\Job\MyProject\CafeAPI\CafeAPI\Views\Shared\_SiteMapPathHelperModel.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aaea18a49968f4cdae9aa6e0108b629172600725"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__SiteMapPathHelperModel), @"mvc.1.0.view", @"/Views/Shared/_SiteMapPathHelperModel.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_SiteMapPathHelperModel.cshtml", typeof(AspNetCore.Views_Shared__SiteMapPathHelperModel))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Job\MyProject\CafeAPI\CafeAPI\Views\_ViewImports.cshtml"
using CafeAPI;

#line default
#line hidden
#line 2 "C:\Job\MyProject\CafeAPI\CafeAPI\Views\_ViewImports.cshtml"
using CafeAPI.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aaea18a49968f4cdae9aa6e0108b629172600725", @"/Views/Shared/_SiteMapPathHelperModel.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ee48e5a6b602dd42923d2945facb3623a8d25b1", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__SiteMapPathHelperModel : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(38, 6, true);
            WriteLiteral("\r\n\r\n\r\n");
            EndContext();
            BeginContext(45, 40, false);
#line 4 "C:\Job\MyProject\CafeAPI\CafeAPI\Views\Shared\_SiteMapPathHelperModel.cshtml"
Write(Html.ActionLink("Home", "Index", "Home"));

#line default
#line hidden
            EndContext();
            BeginContext(85, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 6 "C:\Job\MyProject\CafeAPI\CafeAPI\Views\Shared\_SiteMapPathHelperModel.cshtml"
 if (ViewContext.RouteData.Values["controller"].ToString() != "Home")
{

#line default
#line hidden
            BeginContext(163, 4, true);
            WriteLiteral("    ");
            EndContext();
            BeginContext(169, 4, true);
            WriteLiteral(" >  ");
            EndContext();
            BeginContext(174, 134, false);
#line 8 "C:\Job\MyProject\CafeAPI\CafeAPI\Views\Shared\_SiteMapPathHelperModel.cshtml"
     Write(Html.ActionLink(ViewContext.RouteData.Values["controller"].ToString(), "Index", ViewContext.RouteData.Values["controller"].ToString()));

#line default
#line hidden
            EndContext();
            BeginContext(308, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 9 "C:\Job\MyProject\CafeAPI\CafeAPI\Views\Shared\_SiteMapPathHelperModel.cshtml"
}

#line default
#line hidden
            BeginContext(313, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 11 "C:\Job\MyProject\CafeAPI\CafeAPI\Views\Shared\_SiteMapPathHelperModel.cshtml"
 if (ViewContext.RouteData.Values["action"].ToString() != "Index")
{

#line default
#line hidden
            BeginContext(386, 4, true);
            WriteLiteral("    ");
            EndContext();
            BeginContext(392, 3, true);
            WriteLiteral(" > ");
            EndContext();
            BeginContext(396, 172, false);
#line 13 "C:\Job\MyProject\CafeAPI\CafeAPI\Views\Shared\_SiteMapPathHelperModel.cshtml"
    Write(Html.ActionLink(ViewContext.RouteData.Values["action"].ToString(), ViewContext.RouteData.Values["action"].ToString(), ViewContext.RouteData.Values["controller"].ToString()));

#line default
#line hidden
            EndContext();
            BeginContext(568, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 14 "C:\Job\MyProject\CafeAPI\CafeAPI\Views\Shared\_SiteMapPathHelperModel.cshtml"
}

#line default
#line hidden
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
