using Calabonga.Xml.Exports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models.Handlers;
using WebApplication.Models.Test;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        // This action renders the form
        public ActionResult Index()
        {
            AccountingContext context = new AccountingContext();
            var model = context.UploadedFiles;

            return View(model);
        }

        // This action handles the form POST and the upload
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, UploadedFileInfo upload)
        {
            //check file extension
            string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();

            if (extension != ".xls" && extension != ".xlsx")
            {
                ModelState.AddModelError("uploadError", "Supported file extensions: .xls, .xlsx");
                return View();
            }

            // extract only the filename
            var fileName = Path.GetFileName(file.FileName);

            //display adding date 
            var currentDate = DateTime.Now.ToString("d");

            // store the file inside ~/App_Data/uploads folder
            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), currentDate + fileName);

            using (AccountingContext context = new AccountingContext())
            {
                // Verify that the user selects a file
                if (file != null && file.ContentLength > 0)
                {

                    file.SaveAs(path);

                    // save the filename and path in UploadedFilesInfo table
                    upload.FileName = fileName;
                    upload.FilePath = path;

                    context.UploadedFiles.Add(upload);

                    context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("blankFileError", "You're trying to attach blank file.");
                    return View();
                }

                ExcelHandler excelHandler = new ExcelHandler();
                FileInfo fi1 = new FileInfo(path);

                var queryId = from customer in context.UploadedFiles
                              select customer.UploadedFileInfoId;

                int? currentId = queryId.ToList().Last();

                var id = currentId++ ?? 1;

                excelHandler.SelectDataFromFile(fi1, id);
            }

            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }

        public ExcelResult Export(int id)
        {

            string result = string.Empty;
            Workbook wb = new Workbook();

            // properties
            wb.Properties.Author = "Calabonga";
            wb.Properties.Created = DateTime.Today;
            wb.Properties.LastAutor = "Calabonga";
            wb.Properties.Version = "14";

            // options sheets
            wb.ExcelWorkbook.ActiveSheet = 1;
            wb.ExcelWorkbook.DisplayInkNotes = false;
            wb.ExcelWorkbook.FirstVisibleSheet = 1;
            wb.ExcelWorkbook.ProtectStructure = false;
            wb.ExcelWorkbook.WindowHeight = 800;
            wb.ExcelWorkbook.WindowTopX = 0;
            wb.ExcelWorkbook.WindowTopY = 0;
            wb.ExcelWorkbook.WindowWidth = 600;
            
            // First sheet 
            Worksheet ws1 = new Worksheet("Sheet");

            // Adding Headers
            ws1.AddCell(0, 0, "");
            ws1.AddCell(0, 1, "Начальное сальдо", mergeAcross: 1);
            ws1.AddCell(0, 3, "Текущие показатели", mergeAcross: 1);
            ws1.AddCell(0, 5, "Конечное сальдо", 1);
            ws1.AddCell(1, 0, "Б/сч");
            ws1.AddCell(1, 1, "Актив");
            ws1.AddCell(1, 2, "Пассив");
            ws1.AddCell(1, 3, "Актив");
            ws1.AddCell(1, 4, "Пассив");
            ws1.AddCell(1, 5, "Актив");
            ws1.AddCell(1, 6, "Пассив");

            // get data
            AccountingContext context = new AccountingContext();
            List<AccountField> people = context.Fields.Where(x => x.UploadedFileInfoId == id).ToList();

            int p = 0;
            // appending rows with data
            for (int i = 1; i <=people.Count; i++)
            {
                ws1.AddCell(i + 1, 0, people[p].FieldAccountUnit);
                ws1.AddCell(i + 1, 1, people[p].IncomingBalanceFieldAsset);
                ws1.AddCell(i + 1, 2, people[p].IncomingBalanceFieldLiability);
                ws1.AddCell(i + 1, 3, people[p].CurrentAssetsFieldAsset);
                ws1.AddCell(i + 1, 4, people[p].CurrentAssetsFieldLiability);
                ws1.AddCell(i + 1, 5, people[p].OutgoingBalanceFieldAsset);
                ws1.AddCell(i + 1, 6, people[p].OutgoingBalanceFieldLiability);
                p++;
            }

            wb.AddWorksheet(ws1);

            // generate xml 
            string workstring = wb.ExportToXML();

            // Send to user file
            return new ExcelResult("TurnoverBalanceSheet.xls", workstring);
        }
    

        public ActionResult Details(int id)
        {
            AccountingContext context = new AccountingContext();

            var model = context.Fields.Where(x => x.UploadedFileInfoId == id);

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}