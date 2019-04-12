using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesTagHelpers.Infrastructure.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "wrap")]
    public class TableCellTagHelper : TagHelper
    {
        //public string Wrap { get; set; } = "demo";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.PreContent.SetHtmlContent("<b><i>");
            output.PostContent.SetHtmlContent("</b></i>");
        }
    }
}
