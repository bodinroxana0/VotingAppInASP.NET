#pragma checksum "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d757deddbfeb5fc07b2a32d23b172f0b65166ca4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_AddCandidat), @"mvc.1.0.view", @"/Views/Shared/AddCandidat.cshtml")]
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
#nullable restore
#line 1 "C:\an4sem1\DATC\Web\Views\_ViewImports.cshtml"
using Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\an4sem1\DATC\Web\Views\_ViewImports.cshtml"
using Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d757deddbfeb5fc07b2a32d23b172f0b65166ca4", @"/Views/Shared/AddCandidat.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"74b0619e1a302f0598271da1847e697c39d57b88", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_AddCandidat : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Web.Models.Candidat>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
  
    ViewData["Title"] = "Adauga Candidat";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>");
#nullable restore
#line 6 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n<p>Aceasta pagina este folosita pentru a adauga candidati.</p>\r\n\r\n");
#nullable restore
#line 10 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
 using (Html.BeginForm())
{
   // @Html.ValidationSummary(true, "Login failed. Check your login details.");

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>\r\n        <fieldset>\r\n            <legend>Introduce Candidat</legend>\r\n            <div class=\"editor-label\">\r\n                ");
#nullable restore
#line 17 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
           Write(Html.LabelFor(u => u.Partid));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"editor-field\">\r\n                ");
#nullable restore
#line 20 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
           Write(Html.TextBoxFor(u => u.Partid));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 21 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
           Write(Html.ValidationMessageFor(u => u.Partid));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"editor-label\">\r\n                ");
#nullable restore
#line 24 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
           Write(Html.LabelFor(u => u.NumePrenume));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"editor-field\">\r\n                ");
#nullable restore
#line 27 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
           Write(Html.TextBoxFor(u => u.NumePrenume));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 28 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
           Write(Html.ValidationMessageFor(u => u.NumePrenume));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"editor-label\">\r\n                ");
#nullable restore
#line 31 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
           Write(Html.LabelFor(u => u.PartidSigla));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"editor-field\">\r\n                ");
#nullable restore
#line 34 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
           Write(Html.TextBoxFor(u => u.PartidSigla));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 35 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"
           Write(Html.ValidationMessageFor(u => u.PartidSigla));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <input type=\"submit\" value=\"Adauga Candidat\" />\r\n        </fieldset>\r\n    </div>\r\n");
#nullable restore
#line 40 "C:\an4sem1\DATC\Web\Views\Shared\AddCandidat.cshtml"

}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Web.Models.Candidat> Html { get; private set; }
    }
}
#pragma warning restore 1591
