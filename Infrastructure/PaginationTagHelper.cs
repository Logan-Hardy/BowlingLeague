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
        private IUrlHelperFactory urlInfo;
        public PaginationTagHelper (IUrlHelperFactory uhf)
        {
            urlInfo = uhf;
        }

        public PageNumberingInfo PageInfo { get; set; }
        //public string TeamName { get; set; }

        //Our own dictionary (key value pairs) that we are creating 
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);
            
            TagBuilder finishedTag = new TagBuilder("div");
            
            for (int iCount = 1; iCount <= PageInfo.NumPages; iCount++)
            {
                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pageNum"] = iCount;
                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);
                //individualTag.Attributes["href"] = "/?pageNum=" + iCount;
                individualTag.InnerHtml.Append(iCount.ToString());

                finishedTag.InnerHtml.AppendHtml(individualTag);
            }
            
            output.Content.AppendHtml(finishedTag.InnerHtml);
            
        }
    }
}
