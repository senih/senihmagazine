using System;
using System.Web;
using System.IO;
using System.Xml;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;

namespace Magazine
{
    public class CreateXML
    {
        public static void CreatePagesXMLFile(string path)
        {
            string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/content/" + path), "*.jpg");
            Image image = Image.FromFile(files[0]);
            int width = image.Size.Width;
            int height = image.Size.Height;
            image.Dispose();
            XmlDocument pages = new XmlDocument();
            StringBuilder node = new StringBuilder();

            node.Append(string.Format("<content width=\"{0}\" height=\"{1}\" bgcolor=\"666159\" loadercolor=\"ffffff\" panelcolor=\"5d5d61\" buttoncolor=\"5d5d61\" textcolor=\"ffffff\">", width, height));
            for (int i=1; i<=files.Length; i++)
            {
                node.Append(string.Format("	<page src=\"content/{0}/{1}.jpg\"/>", path, i));
            }
            node.Append("</content>");

            pages.LoadXml(node.ToString());
            pages.Save(HttpContext.Current.Server.MapPath("~/xml/Pages.xml"));
        }
    }
}