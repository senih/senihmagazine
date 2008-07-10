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
using System.Text.RegularExpressions;
using Magazine;

public partial class Administration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            StatusLabel.Text = string.Empty;
        }
        Panel1.Visible = false;
        UploadPanel.Visible = false;
        btnPreview.Attributes.Add("OnClick", "window.open('magazine.html');");
        
    }
    protected string UploadPdfFile()
    {
        string status;
        string path = Server.MapPath("~/PDF/");
        if (FileUpload.HasFile)
        {
            String fileExtension = Path.GetExtension(FileUpload.FileName).ToLower();
            if (fileExtension == ".pdf")
            {
                try
                {
                    FileUpload.SaveAs(path + "temp.pdf");
                    status = "File uploaded!";
                }
                catch (Exception ex)
                {
                    status = ex.ToString();
                }
            }
            else
            {
                status = "You can upload only *.pdf files";
            }
        }
        else
            status = "Select file to upload!";
        return status;
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string temp2 = UploadPdfFile();
        if (temp2 == "File uploaded!")
        {
            string temp1 = NameTextBox.Text;
            int chk = temp1.IndexOf(" ");
            if ((IsAlphaNumeric(NameTextBox.Text)) && chk == -1)
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
                        if (StatusLabel.Text == "Image files created!")
                        {
                            MagazineData.CreateIssue(newIssue);
                            CreateXML.CreatePagesXMLFile(newIssue.IssueId);
                            btnPreview.Visible = true;
                            btnUploadOnServer.Visible = true;
                        }
                        else
                            StatusLabel.Text = "Error!";
                    }
                }
            }
            else StatusLabel.Text = "Enter valid Issue name";
        }
        else
            StatusLabel.Text = temp2;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        TitleLabel.Text = "New Issue";
        NameTextBox.Text = string.Empty;
        ResolutionDropDownList.SelectedIndex = 0;
        QualityTextBox.Text = string.Empty;
        TxtAliasingDropDownList.SelectedIndex = 0;
        GraphAliasingDropDownList.SelectedIndex = 0;
        IssuePanel.Visible = true;
        Panel1.Visible = false;
        NameTextBox.Visible = true;
        btnUpdate.Visible = false;
        btnCreate.Visible = true;
        btnPreview.Visible = false;
        btnUploadOnServer.Visible = false;
        IssueNameLabel.Visible = true;
        IssueNameLabel.Text = "Issue name: ";
    }
    protected void btnOpen_Click(object sender, EventArgs e)
    {
        TitleLabel.Text = "Open Issue";
        IssuePanel.Visible = false;
        Panel1.Visible = true;
        OpenDropDownList.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (FileUpload.HasFile)
            UploadPdfFile();
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
        CreateXML.CreatePagesXMLFile(newIssue.IssueId);
        Panel1.Visible = true;
        btnPreview.Visible = true;
        RequiredFieldValidator4.Visible = false;
        
    }

    protected void btnUploadOnServer_Click(object sender, EventArgs e)
    {
        IssuePanel.Visible = false;
        UploadPanel.Visible = true;
        TitleLabel.Text = "Upload issue";
    }
    protected void btnUploadOnWebServer_Click(object sender, EventArgs e)
    {
        StatusLabel.Text = Upload.UploadUsingClient(IssuesDropDownList.SelectedValue, HostTextBox.Text, UserTextBox.Text, PasswordTextBox.Text);
    }

    public static bool IsAlphaNumeric(string strToCheck)
    {
        Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9 ]");
        return !objAlphaNumericPattern.IsMatch(strToCheck);
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        if (OpenDropDownList.SelectedValue == string.Empty)
            StatusLabel.Text = "There are no current issues!";
        else
        {
            Issue existingIssue = MagazineData.OpenIssue(OpenDropDownList.SelectedValue);
            ResolutionDropDownList.Text = existingIssue.Resolution;
            QualityTextBox.Text = existingIssue.Quality;
            TxtAliasingDropDownList.Text = existingIssue.TextAntialiasing;
            GraphAliasingDropDownList.Text = existingIssue.GraphicsAntialiasing;
            CreateXML.CreatePagesXMLFile(existingIssue.IssueId);
            btnPreview.Visible = true;
            btnUploadOnServer.Visible = true;
            IssuePanel.Visible = true;
            Panel1.Visible = true;
            IssueNameLabel.Visible = true;
            NameTextBox.Visible = false;
            btnCreate.Visible = false;
            btnUpdate.Visible = true;
            IssueNameLabel.Text = "Issue name: " + existingIssue.IssueId.ToUpper();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (OpenDropDownList.SelectedValue == string.Empty)
            StatusLabel.Text = "There are no current issues!";
        else
        {
            CreateImages.DeleteImages(OpenDropDownList.SelectedValue);
            StatusLabel.Text = MagazineData.DeleteIssue(OpenDropDownList.SelectedValue);
            OpenDropDownList.DataBind();
            Panel1.Visible = true;
            IssuePanel.Visible = false;
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
    }
}
