using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.editorControls.userControlGrapper;

namespace uMediaPicker.usercontrols.uMediaPicker
{
    public partial class MediaPreview : UserControl, IUsercontrolDataEditor
    {
        protected object _value;

        public object value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        protected string UmbracoFile = "";
        protected string FileType = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Media menu = new Media(Convert.ToInt32(Request.QueryString["id"]));

            if (menu == null)
            {
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
                UmbracoFile = menu.getProperty("umbracoFile").Value.ToString();
                if (UmbracoFile == "")
                    this.Visible = false;
                FileType = UmbracoFile.Split('.').Last();
            }
        }
    }
}