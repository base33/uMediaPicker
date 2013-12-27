using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.editorControls.userControlGrapper;
using umbraco.cms.businesslogic.media;

namespace uMediaPicker.usercontrols.uMediaPicker
{
    [ClientDependency.Core.ClientDependency(FilePath = "/usercontrols/uMediaPicker/js/uMediaPicker.js", Priority = 1)]
    public partial class uMediaPicker : System.Web.UI.UserControl, IUsercontrolDataEditor
    {
        protected int id;

        public object value
        {
            get
            {
                return fileId.Text != "" ? int.Parse(fileId.Text) : 0;
            }
            set
            {
                if(!IsPostBack)
                    id = value != null && ((string)value).Length > 0 ? Convert.ToInt32((string)value) : 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                id = fileId.Text != "" ? int.Parse(fileId.Text) : 0;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (id != 0)
            {
                fileId.Text = id.ToString();
                Media media = new Media(id);
                mediaName.InnerText = media.Text;
                string path = media.getProperty("umbracoFile") != null || media.getProperty("umbracoFile").Value != null ? media.getProperty("umbracoFile").Value.ToString() : "";
                fileType.Text = path.Split('.').Last();
                filePath.Text = path;
            }
        }
    }
}