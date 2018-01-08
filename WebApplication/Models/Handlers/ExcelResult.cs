using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Models.Handlers
{
    public class ExcelResult : ActionResult
    {
        /// <summary>
            /// Creates an instance of the class that gives the Excel file
            /// </summary>
            /// <param name="fileName">filename for export</param>
            /// <param name="report">dataset for export</param>
        public ExcelResult(string fileName, string report)
        {
            Filename = fileName;
            Report = report;
        }

        public string Report { get; private set; }
        public string Filename { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.BufferOutput = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
            string.Format("attachment; filename={0}", Filename));
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(Report);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}