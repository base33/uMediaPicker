using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.presentation.umbracobase;
using umbraco.cms.businesslogic.media;

namespace uMediaPicker.Base
{
    [RestExtension("uMediaPicker")]
    public class uMediaPicker
    {
        [RestExtensionMethod(allowAll = true, returnXml = false)]
        public static string GetMediaChildren(int dir)
        {
            Media parent = new Media(dir);

            string markup = "<ul class='jqueryFileTree' style='display: none;'>";
            markup += "<li class='directory collapsed'><a href='#' rel='/this/folder/'>Folder Name</a></li>";
            markup += "<li class='file ext_txt'><a href='#' rel='/this/folder/filename.txt'>filename.txt</a></li>";
            markup += "</ul>";

            return markup;
        }
    }
}