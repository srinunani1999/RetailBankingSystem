#pragma checksum "E:\RetailBankingSystem\RetailBankSystemClient\Views\Customer\GetCustomerDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d4e44297b98cb92e3b85bf616cd4954184cc7540"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_GetCustomerDetails), @"mvc.1.0.view", @"/Views/Customer/GetCustomerDetails.cshtml")]
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
#line 1 "E:\RetailBankingSystem\RetailBankSystemClient\Views\_ViewImports.cshtml"
using RetailBankSystemClient;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\RetailBankingSystem\RetailBankSystemClient\Views\_ViewImports.cshtml"
using RetailBankSystemClient.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d4e44297b98cb92e3b85bf616cd4954184cc7540", @"/Views/Customer/GetCustomerDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"22a73e3a792625f43bb39507199164f87925f61d", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_GetCustomerDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RetailBankSystemClient.ViewModels.CustomerViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "GetCustomer", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\RetailBankingSystem\RetailBankSystemClient\Views\Customer\GetCustomerDetails.cshtml"
  
    ViewData["Title"] = "GetCustomerDetails";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Details of ");
#nullable restore
#line 7 "E:\RetailBankingSystem\RetailBankSystemClient\Views\Customer\GetCustomerDetails.cshtml"
          Write(Model.CustomerId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n<div>\r\n    <h4>CustomerViewModel</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 14 "E:\RetailBankingSystem\RetailBankSystemClient\Views\Customer\GetCustomerDetails.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 17 "E:\RetailBankingSystem\RetailBankSystemClient\Views\Customer\GetCustomerDetails.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 20 "E:\RetailBankingSystem\RetailBankSystemClient\Views\Customer\GetCustomerDetails.cshtml"
       Write(Html.DisplayNameFor(model => model.Address));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 23 "E:\RetailBankingSystem\RetailBankSystemClient\Views\Customer\GetCustomerDetails.cshtml"
       Write(Html.DisplayFor(model => model.Address));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 26 "E:\RetailBankingSystem\RetailBankSystemClient\Views\Customer\GetCustomerDetails.cshtml"
       Write(Html.DisplayNameFor(model => model.DateOfBirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 29 "E:\RetailBankingSystem\RetailBankSystemClient\Views\Customer\GetCustomerDetails.cshtml"
       Write(Html.DisplayFor(model => model.DateOfBirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 32 "E:\RetailBankingSystem\RetailBankSystemClient\Views\Customer\GetCustomerDetails.cshtml"
       Write(Html.DisplayNameFor(model => model.PANno));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 35 "E:\RetailBankingSystem\RetailBankSystemClient\Views\Customer\GetCustomerDetails.cshtml"
       Write(Html.DisplayFor(model => model.PANno));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n   \r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d4e44297b98cb92e3b85bf616cd4954184cc75406755", async() => {
                WriteLiteral("Back to Customers List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RetailBankSystemClient.ViewModels.CustomerViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591