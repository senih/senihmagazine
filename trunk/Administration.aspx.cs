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

public partial class Administration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            StatusLabel.Text = string.Empty;
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
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Issue newIssue = new Issue();
        newIssue.IssueId = NameTextBox.Text;
        newIssue.IssueDirectory = ConfigurationManager.AppSettings["imagepath"] + NameTextBox.Text;
        if (Directory.Exists(newIssue.IssueDirectory))
            StatusLabel.Text = "Folder already exists!";
        else
        {
            if (!File.Exists(HttpContext.Current.Server.MapPath("~/pdf") + @"\temp.pdf"))
                StatusLabel.Text = "Please upload PDF file first!";
            else
            {
                Directory.CreateDirectory(newIssue.IssueDirectory);
                newIssue.Resolution = ResolutionDropDownList.SelectedValue;
                newIssue.Quality = QualityTextBox.Text;
                newIssue.TextAntialiasing = TxtAliasingDropDownList.SelectedValue;
                newIssue.GraphicsAntialiasing = GraphAliasingDropDownList.SelectedValue;
                StatusLabel.Text = CreateImages.CreateImagesGhostScript(newIssue);
                string pdfpath = HttpContext.Current.Server.MapPath("~/pdf");
                string sourcePDF = pdfpath + @"\temp.pdf";
                string destinationPDF = newIssue.IssueDirectory + @"\" + newIssue.IssueId + ".pdf";
                File.Move(sourcePDF, destinationPDF);
                MagazineData.CreateIssue(newIssue);
                CreateXML.CreatePagesXMLFile(newIssue.IssueId);
                btnPreview.Visible = true;
            }
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        TitleLabel.Text = "New Issue";
        IssuePanel.Visible = true;
        OpenDropDownList.Visible = false;
        NameTextBox.Visible = true;
        btnUpdate.Visible = false;
        btnCreate.Visible = true;
        btnPreview.Visible = false;
    }
    protected void btnOpen_Click(object sender, EventArgs e)
    {
        TitleLabel.Text = "Open Issue";
        IssuePanel.Visible = true;
        NameTextBox.Visible = false;
        OpenDropDownList.Visible = true;
        btnCreate.Visible = false;
        btnUpdate.Visible = true;
        btnPreview.Visible = false;
    }
    protected void OpenDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Issue existingIssue = MagazineData.OpenIssue(OpenDropDownList.SelectedValue);
        ResolutionDropDownList.Text = existingIssue.Resolution;
        QualityTextBox.Text = existingIssue.Quality;
        TxtAliasingDropDownList.Text = existingIssue.TextAntialiasing;
        GraphAliasingDropDownList.Text = existingIssue.GraphicsAntialiasing;
        btnPreview.Visible = true;
        btnPreview.Visible = true;
        CreateXML.CreatePagesXMLFile(existingIssue.IssueId);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string[] pdfFiles = Directory.GetFiles(Server.MapPath("~/pdf"));
        string[] orgPdfFiles = Directory.GetFiles(Server.MapPath("~/content/" + OpenDropDownList.SelectedValue), "*.pdf");
        if (pdfFiles.Length == 0)
            File.Copy(orgPdfFiles[0], Server.MapPath("~/pdf/") + "temp.pdf");
        string[] jpgFiles = Directory.GetFiles(Server.MapPath("~/content/" + OpenDropDownList.SelectedValue), "*.jpg");
        foreach (string file in jpgFiles)
            File.Delete(file);

        Issue newIssue = new Issue();
        newIssue.IssueId = OpenDropDownList.SelectedValue;
        newIssue.IssueDirectory = ConfigurationManager.AppSettings["imagepath"] + OpenDropDownList.SelectedValue;
        newIssue.Resolution = ResolutionDropDownList.SelectedValue;
        newIssue.Quality = QualityTextBox.Text;
        newIssue.TextAntialiasing = TxtAliasingDropDownList.SelectedValue;
        newIssue.GraphicsAntialiasing = GraphAliasingDropDownList.SelectedValue;
        StatusLabel.Text = CreateImages.CreateImagesGhostScript(newIssue);
        string pdfpath = HttpContext.Current.Server.MapPath("~/pdf");
        string sourcePDF = pdfpath + @"\temp.pdf";
        string destinationPDF = newIssue.IssueDirectory + @"\" + newIssue.IssueId + ".pdf";
        File.Copy(sourcePDF, destinationPDF, true);
        File.Delete(sourcePDF);
        MagazineData.UpdateIssue(newIssue);
        btnPreview.Visible = true;
        CreateXML.CreatePagesXMLFile(newIssue.IssueId);
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/magazine.html");
    }
}
