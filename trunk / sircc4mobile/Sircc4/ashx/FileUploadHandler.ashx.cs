using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Sircc4.ashx
{
    /// <summary>
    /// Summary description for FileUploadHandler1
    /// </summary>
    public class FileUploadHandler1 : IHttpHandler
    {

        #region IHttpHandler Members
        public bool IsReusable
        {
            get { return false; }
        }
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //Uploaded File Deletion
                if (context.Request.QueryString.Count > 0)
                {
                    string filePath = @"c:\DownloadedFiles\" + context.Request.QueryString[0].ToString();
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                }
                //File Upload
                else
                {
                    string fileName = Path.GetFileName(context.Request.Files[0].FileName);
                    string location = @"c:\DownloadedFiles\" + fileName;
                    context.Request.Files[0].SaveAs(location);
                }
            }
            catch
            {
            }
        }
        #endregion
    }
}