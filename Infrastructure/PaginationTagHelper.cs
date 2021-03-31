using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Infrastructure
{
    //target div element 
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper 
    {
        //factory for creating asp.net core iurlhelper instance
        private IUrlHelperFactory urlInfo;

        //constructor 
        public PaginationTagHelper (IUrlHelperFactory uhf)
        {
            urlInfo = uhf;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public PageNumberingInfo PageInfo { get; set; }
        //public string TeamName { get; set; }

        //Our own dictionary (key value pairs) that we are creating 
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        public bool PageClassesEnabled { get; set; }
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        //Overriding
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);
            
            TagBuilder finishedTag = new TagBuilder("div");
            
            //create a tags for each page
            for (int iCount = 1; iCount <= PageInfo.NumPages; iCount++)
            {
                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pageNum"] = iCount;
                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);

                if (PageClassesEnabled)
                {
                    individualTag.AddCssClass(PageClass);
                    //shorthand if statement to highlight the selected page
                    individualTag.AddCssClass(iCount == PageInfo.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                individualTag.InnerHtml.Append(iCount.ToString());

                finishedTag.InnerHtml.AppendHtml(individualTag);
            }
            
            output.Content.AppendHtml(finishedTag.InnerHtml);
            
        }
    }
}
