using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.cms.businesslogic.media;
using umbraco.cms.businesslogic;

namespace uMediaPicker.umbraco.plugins.uMediaPicker
{
    /// <summary>
    /// Summary description for SearchMedia
    /// </summary>
    public class SearchMedia : IHttpHandler
    {
        ContentType folder = ContentType.GetByAlias("Folder");

        public void ProcessRequest(HttpContext context)
        {
            string term = context.Request.QueryString["search"].ToLower();
            string markup = " <ul class='jqueryFileTree'>";
            markup += SearchMediaRoot(term);
            markup += "</ul>";
            context.Response.ContentType = "text/plain";
            context.Response.Write(markup);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected string SearchMediaRoot(string term)
        {
            Media media = new Media(-1);
            return SearchMediaChildren(media, term);
        }

        protected string SearchMediaChildren(Media parent, string term)
        {
            string markup = "";
            foreach (Media child in parent.Children)
            {
                if (child.Text.ToLower().Contains(term) && child.ContentType != folder && child.Children.Count() == 0)
                {
                    string umbracoFile = child.getProperty("umbracoFile").Value.ToString();
                    markup += string.Format(
                                "<li class='searchPreview ext_{0}'><a href='#' rel='{1}[split]{2}[split]{3}'>{3}</a></li>",
                                GetClassExtension(umbracoFile),
                                umbracoFile,
                                child.Id,
                                child.Text
                                );
                }
                else if (child.Children.Count() > 0)
                {
                    markup += SearchMediaChildren(child, term);
                }
            }
            return markup;
        }

        protected string GetClassExtension(string umbracoFile)
        {
            return umbracoFile.Split('.').Last();
        }
    }
}