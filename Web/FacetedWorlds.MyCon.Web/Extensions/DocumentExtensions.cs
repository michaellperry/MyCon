using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web.Extensions
{
    public static class DocumentExtensions
    {
        public static MvcHtmlString AsHtml(this IEnumerable<DocumentSegment> segments)
        {
            if (segments == null)
                return new MvcHtmlString(String.Empty);

            string raw = string.Join("", segments.Select(segment => segment.Text).ToArray());
            var lines = raw.Split('\r').Where(l => !String.IsNullOrWhiteSpace(l));
            var paragraphs = lines.Select(l => String.Format("<p>{0}</p>", HttpUtility.HtmlEncode(l)));
            var html = string.Join("", paragraphs.ToArray());
            return new MvcHtmlString(html);
        }
    }
}