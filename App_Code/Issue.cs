using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Magazine
{
    public class Issue
    {
        protected string _issueID;
        protected string _issueDirectory;
        protected string _height;
        protected string _resolution;
        protected string _quality;
        protected string _txtAntialiasing;
        protected string _graphAntialiasing;

        public string IssueId
        {
            get { return _issueID; }
            set { _issueID = value; }
        }

        public string IssueDirectory
        {
            get { return _issueDirectory; }
            set { _issueDirectory = value; }
        }

        public string Resolution
        {
            get { return _resolution; }
            set { _resolution = value; }
        }

        public string Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }

        public string TextAntialiasing
        {
            get { return _txtAntialiasing; }
            set { _txtAntialiasing = value; }
        }

        public string GraphicsAntialiasing
        {
            get { return _graphAntialiasing; }
            set { _graphAntialiasing = value; }
        }

        public Issue()
        {
        }
    }
}
