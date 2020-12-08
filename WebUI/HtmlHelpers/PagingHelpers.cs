using System;
using System.Text;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i < pagingInfo.TotalPages + 1; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("select");
                builder.Append(tag.ToString());


            }
            return MvcHtmlString.Create(builder.ToString());
        }



    }
}