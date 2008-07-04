using System;
using System.Web;
using System.IO;
using System.Xml;
using System.Text;

namespace Magazine
{
    public class CreateXML
    {
        public static void CreatePagesXMLFile()
        {
            string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/pages"), "*.jpg");
            XmlDocument pages = new XmlDocument();
            StringBuilder node = new StringBuilder();

            node.Append("<content width=\"1329\" height=\"1713\" bgcolor=\"cccccc\" loadercolor=\"ffffff\" panelcolor=\"5d5d61\" buttoncolor=\"5d5d61\" textcolor=\"ffffff\">");
            for (int i=1; i<=files.Length; i++)
            {
                node.Append(string.Format("	<page src=\"pages/{0}.jpg\"/>", i));
            }
            node.Append("</content>");

            pages.LoadXml(node.ToString());
            pages.Save(HttpContext.Current.Server.MapPath("~/xml/Pages.xml"));
        }
    }
}