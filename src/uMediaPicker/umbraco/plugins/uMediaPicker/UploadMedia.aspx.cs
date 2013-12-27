using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.BasePages;
using umbraco.cms.businesslogic.media;
using umbraco.BusinessLogic;
using System.IO;

namespace uMediaPicker.umbraco.plugins.uMediaPicker
{
    public partial class UploadMedia : UmbracoEnsuredPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlPreviews.Visible = false;
                pnlUpload.Visible = true;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int mediaId = -1;
            if (!int.TryParse(hdnSelectedFolder.Value, out mediaId))
                mediaId = -1;

            string fileName = uploadMedia.FileName; // System.IO.Path.GetExtension(uploadMedia.FileName);
            Media media = Media.MakeNew(fileName, MediaType.GetByAlias("File"), new User(0), mediaId);
            string mediaRootPath = Server.MapPath("~\\media\\");
            string storagePath = mediaRootPath + media.Id.ToString();

            Directory.CreateDirectory(storagePath);
            uploadMedia.SaveAs(storagePath + "\\" + uploadMedia.FileName);
            string filePath = string.Format("/media/{0}/{1}", media.Id, uploadMedia.FileName);
            media.getProperty("umbracoFile").Value = filePath;
            media.Save();
            pnlUpload.Visible = false;
            pnlPreviews.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "previewPath", "var previewPath = '" + filePath + "[split]" + media.Id + "';", true);
        }
    }
}