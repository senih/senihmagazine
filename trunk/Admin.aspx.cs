using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using Magazine;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            Panel1.Visible = true;
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/PDF/");
        if (FileUpload.HasFile)
        {
            String fileExtension = Path.GetExtension(FileUpload.FileName).ToLower();
            if (fileExtension == ".pdf")
            {
                try
                {
                    FileUpload.SaveAs(path + "temp.pdf");
                    Label1.Text = "File uploaded!";
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.ToString();
                }
            }
            else
            {
                Label1.Text = "You can upload only *.pdf files";
            }
        }
        else
            Label1.Text = "Select file to upload!";
    }
    protected void btnCreateImages_Click(object sender, EventArgs e)
    {
        string[] pdffiles = Directory.GetFiles((Server.MapPath("~/PDF")), "*.pdf");
        string inputfile = pdffiles[0];
        string[] jpgfiles = Directory.GetFiles(Server.MapPath("~/pages"));
        foreach (string file in jpgfiles)
        {
            File.Delete(file);
        }
       // Label2.Text = CreateImages.CreateImagesGhostScript(inputfile);
        File.Delete(inputfile);
        Panel2.Visible = false;
        Panel3.Visible = true;
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/magazine.html");
    }
    protected void btnUploadOnServer_Click(object sender, EventArgs e)
    {
        //string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/pages"), "*.jpg");
        //foreach (string file in files)
        //{
        //    int pos = file.LastIndexOf("\\");
        //    string temp = file.Substring(pos + 1);
        //    Label4.Text = Upload.UploadOnWebServer(file, temp, HostTextBox.Text, UserTextBox.Text, PasswordTextBox.Text);
        //}

        //Label4.Text = "Files uploaded on web server!";

//        Label4.Text = Upload.UploadUsingClient(HostTextBox.Text, UserTextBox.Text, PasswordTextBox.Text);
    }
    protected void btnCreateXML_Click(object sender, EventArgs e)
    {
        //string[] jpgfiles = Directory.GetFiles(Server.MapPath("~/pages"));
        //foreach (string file in jpgfiles)
        //{
        //    CreateImages.ResizeImages(file);
        //}
        //CreateXML.CreatePagesXMLFile();
        Label3.Text = "XML file created!";
        Panel3.Visible = false;
        Panel4.Visible = true;
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Panel4.Visible = false;
        Panel5.Visible = true;
        Label5.Text = "Your magazine is ready for publishing!";
    }
}
