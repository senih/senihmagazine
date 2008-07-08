using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using UDCWRAPPERLib;
using System.Diagnostics;

namespace Magazine
{
    public class CreateImages
    {
        public static string CreateImagesMethod(string inputfile)
        {
            string status = "Error";
            UDCWRAPPERLib.Printer PDFPrinter = new UDCWRAPPERLib.Printer();
            UDCWRAPPERLib.Profile profile;
            Acrobat.AcroApp app;
            Acrobat.AcroAVDoc doc;
            Acrobat.AcroPDDoc document;

            try
            {
                PDFPrinter.SetAsDefaultPrinter();
                PDFPrinter.ImportProfileFromFile(HttpContext.Current.Server.MapPath("~/profile/myprofile.udc"));
                string pro = PDFPrinter.DefaultProfile;
                profile = (UDCWRAPPERLib.Profile)PDFPrinter.get_Profile("MyProfile");
                profile.Default = 1;
                pro = PDFPrinter.DefaultProfile;
                profile.PreDefinedImageFilePath = HttpContext.Current.Server.MapPath("~/pages");

                //string pro = PDFPrinter.DefaultProfile;
                //profile = (UDCWRAPPERLib.Profile) PDFPrinter.get_Profile(pro);
                //string printpath = HttpContext.Current.Server.MapPath("~/pages");
                //profile.name = "MyProfile";
                //profile.PreDefinedImageFilePath = printpath;
                //profile.ImageFileFormat = UDCWRAPPERLib.IMAGE_FORMAT.FMT_JPEG;
                //profile.PreDefinedImageFileName = "&[Page No].&[Image Type]";
                //int quality = 100;
                //int depth = 24;
                //profile.set_JPEGQuality(UDCWRAPPERLib.IMAGE_FORMAT.FMT_JPEG, quality);
                //profile.set_ImageFileColorDepth(UDCWRAPPERLib.IMAGE_FORMAT.FMT_JPEG, depth);
                //profile.PrintQualityX = 300;
                //profile.PrintQualityY = 300;
                //profile.PageWidth = 2657;
                //profile.PageHeight = 3425;
                //profile.PageOrientation = UDCWRAPPERLib.PAGE_ORIENTATION.PO_PORTRAIT;
                //profile.PostPrintAction = UDCWRAPPERLib.POST_PRINT_ACTION.PP_NONE;
                //profile.Default = 1;
                //profile.ExportToFile(HttpContext.Current.Server.MapPath("~/profile/myprofile.udc"));

                //PDFPrinter.SetAsDefaultPrinter();
                //PDFPrinter.ImportProfileFromFile(HttpContext.Current.Server.MapPath("~/profile/myprofile.udc"));
                

                app = (Acrobat.AcroApp)Microsoft.VisualBasic.Interaction.CreateObject("AcroExch.App", "");
                doc = (Acrobat.AcroAVDoc)Microsoft.VisualBasic.Interaction.CreateObject("AcroExch.AVDoc", "");

                bool temp = doc.Open(inputfile, "");
                if (!temp)
                {
                    throw new FileNotFoundException("PDF file not found!");
                }
                document = (Acrobat.AcroPDDoc)doc.GetPDDoc();
                int pagesNumber = document.GetNumPages();                
                doc.PrintPagesSilent(0, pagesNumber, -1, 0, 0);
                doc.Close(1);
                document.Close();
                app.Exit();

                Marshal.ReleaseComObject(document);
                Marshal.ReleaseComObject(doc);
                Marshal.ReleaseComObject(app);
                PDFPrinter.RemoveProfile("MyProfile");
                                
                status = "Image files created!";
            }
            catch (System.Exception ex)
            {
                status = ex.ToString();
            }

            return status;
        }

        public static void ResizeImages(string file)
        {
            Image image = Image.FromFile(file);
            int width = image.Size.Width;
            int height = image.Size.Height;
            width = width / 2;
            height = height / 2;
            Image newimage = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(newimage);
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gr.DrawImage(image, 0, 0, width, height);
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
            EncoderParameters param = new EncoderParameters(1);
            int quality = 90;
            param.Param[0] = new EncoderParameter(Encoder.Quality, quality);
           
            image.Dispose();
            if (File.Exists(file))
                File.Delete(file);
            FileStream fs = File.Create(file);
                        
            newimage.Save(fs, info[1], param);
            param.Dispose();
            newimage.Dispose();
        }

        public static string CreateImagesGhostScript(Issue newIssue)
        {
            string status = "Error!";
            string app = ConfigurationManager.AppSettings["gspath"] + "gswin32";
            string pdfpath = HttpContext.Current.Server.MapPath("~/pdf");
            string pdf = pdfpath + @"\temp.pdf";
            string output = newIssue.IssueDirectory + @"\%d.jpg ";
            string args = @" -sDEVICE=jpeg -r" + newIssue.Resolution + " -dJPEGQ=" + newIssue.Quality + " -dTextAlphaBits=" + newIssue.TextAntialiasing + " -dGraphicsAlphaBits=" + newIssue.GraphicsAntialiasing + " -dUseCIEColor=true -o " + output + pdf;
            try
            {
                ProcessStartInfo procInfo = new ProcessStartInfo(app, args);
                procInfo.WindowStyle = ProcessWindowStyle.Hidden;
                procInfo.RedirectStandardOutput = true;
                procInfo.RedirectStandardInput = true;
                procInfo.RedirectStandardError = true;
                procInfo.UseShellExecute = false;

                Process createImages = Process.Start(procInfo);
                StreamReader reader = createImages.StandardOutput;
                string temp = reader.ReadLine();
                reader.Close();
                createImages.Close();
                status = "Image files created!";
            }
            catch (System.Exception ex)
            {
                status = ex.ToString();
            }
            return status;
        }
    }
}
