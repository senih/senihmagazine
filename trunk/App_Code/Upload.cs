using System;
using System.Net;
using System.IO;
using System.Web;

namespace Magazine
{
    public class Upload
    {
        //public static string UploadOnWebServer(string path, string filename, string host, string user, string password)
        //{
        //    string status;
        //    try
        //    {
        //        FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(string.Format("ftp://{0}/pages/{1}", host, filename));
        //        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
        //        ftpRequest.UseBinary = true;
        //        ftpRequest.Timeout = -1;
        //        ftpRequest.Credentials = new NetworkCredential(user, password);
        //        FileInfo finfo = new FileInfo(path);
        //        //const int bufferLength = 4096;
        //        //byte[] buffer = new byte[bufferLength];
        //        byte[] fileContents = new byte[finfo.Length];
        //        int count = 0;
        //        int readBytes = 0;
        //        FileStream stream = File.OpenRead(path);
        //        Stream requestStream = ftpRequest.GetRequestStream();
        //        do
        //        {
        //            //readBytes = stream.Read(buffer, 0, bufferLength);
        //            readBytes = stream.Read(fileContents, 0, fileContents.Length);
        //            //requestStream.Write(buffer, 0, bufferLength);
        //            requestStream.Write(fileContents, 0, fileContents.Length);
        //            count += readBytes;
        //        }
        //        while (readBytes != 0);
        //        requestStream.Close();
        //        status = "Files uploaded on web server!";
        //    }
        //    catch (Exception ex)
        //    {
        //        status = ex.ToString();
        //    }
        //    return status;
        //}

        public static string UploadUsingClient(string issueId, string host, string user, string password)
        {
            string status;
            string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/content/" + issueId), "*.jpg");
            int port = 21;
            EnterpriseDT.Net.Ftp.FTPClient ftpclient = new EnterpriseDT.Net.Ftp.FTPClient();
            try
            {
                ftpclient.RemoteHost = host;
                ftpclient.ControlPort = port;
                ftpclient.Connect();
                ftpclient.Login(user, password);
                ftpclient.TransferType = EnterpriseDT.Net.Ftp.FTPTransferType.BINARY;
                ftpclient.ChDir("content");
                ftpclient.MkDir(issueId);
                ftpclient.ChDir(issueId);
                int totalFiles = files.Length;
                int uploadedFiles = 0;
                foreach (string file in files)
                {
                    int pos = file.LastIndexOf("\\");
                    string temp = file.Substring(pos + 1);
                    ftpclient.Put(file, temp);
                    uploadedFiles++;
                }

                ftpclient.Quit();
                status = "Files uploaded on web server at: " + DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            return status;
        }
    }
}