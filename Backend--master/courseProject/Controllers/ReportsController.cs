
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Pdf;
using iText.Layout.Properties;
using courseProject.Services.Students;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle;

using iText.Layout.Element;
using courseProject.Services.Students;

using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using courseProject.Services.Reports.PDF;
using courseProject.Services.Reports.EXCEL;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.Reports;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

           
        private readonly IPdfServices pdfServices;
        private readonly IExcelServices excelServices;

        public ReportsController(IPdfServices pdfServices , IExcelServices excelServices)
            {
              
            this.pdfServices = pdfServices;
            this.excelServices = excelServices;
        }


    



        // Endpoint to export Data to PDF
        [HttpGet("export-all-Data-To-PDF")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> ExportAllDataToPdf([FromQuery] ReportDTO reportDTO)
        {
            byte[] pdfBytes = null;
            string dataType = reportDTO.data.ToLower();


            switch (dataType)
            {
                case "student":
                    pdfBytes = await pdfServices.GenerateStudentsPdfAsync();
                    break;
                case "employee":
                    pdfBytes = await pdfServices.GenerateEmployeesPdfAsync();
                    break;
                case "course":
                    pdfBytes = await pdfServices.GenerateCoursePdfAsync();
                    break;
                default:
                    return BadRequest("Invalid data type specified.");
            }
            if (pdfBytes == null || pdfBytes.Length == 0)
            {
                return NotFound("No Data found to export.");
            }

                // Return the PDF file
                return File(pdfBytes, "application/pdf", $"{dataType}.pdf");
            }

    




        [HttpGet("export-all-data-to-excel")]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> ExportAllDataToExcel([FromQuery] ReportDTO reportDTO)
        {
            byte[] excelBytes = null;
            string dataType = reportDTO.data.ToLower();

            switch (dataType)
            {
                case "student":
                    // Generate Excel for Students
                    excelBytes = await excelServices.GenerateStudentsExcelAsync();
                    break;
                case "employee":
                    // Generate Excel for Employees
                    excelBytes = await excelServices.GenerateEmployeesExcelAsync();
                    break;
                case "course":
                    // Generate Excel for Courses
                    excelBytes = await excelServices.GenerateCourseExcelAsync();
                    break;
                default:
                    return BadRequest("Invalid data type specified.");
            }

            if (excelBytes == null || excelBytes.Length == 0)
            {
                return NotFound("No data found to export.");
            }

            // Return the Excel file
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{dataType}.xlsx");
        }
    }















    //[HttpGet("export-all")]
    //    public async Task<IActionResult> ExportAllDataToExcel()
    //    {
    //        try
    //        {
    //            var students = await _studentService.GetAllStudents();

    //            if (students == null || students.Count == 0)
    //            {
    //                return NotFound("No students found to export.");
    //            }

    //        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

    //        using (var memoryStream = new MemoryStream())
    //        {
    //            using (var package = new ExcelPackage(memoryStream))
    //            {
    //                var worksheet = package.Workbook.Worksheets.Add("Students");

    //                // Add the header row
    //                worksheet.Cells[1, 1].Value = "Date";
    //                worksheet.Cells[1, 2].Value = DateTime.Now.ToString("yyyy-MM-dd");
    //                worksheet.Cells[2, 1].Value = "Day";
    //                worksheet.Cells[2, 2].Value = DateTime.Now.DayOfWeek.ToString();
    //                worksheet.Cells[4, 1].Value = "Students Table";

    //                // Set styles
    //                worksheet.Cells[4, 1, 4, 3].Merge = true;
    //                worksheet.Cells[4, 1, 4, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
    //                worksheet.Cells[4, 1, 4, 3].Style.Font.Size = 20;
    //                worksheet.Cells[4, 1, 4, 3].Style.Font.Bold = true;

    //                // Define column headers
    //                var headerRow = new List<string[]> { new string[] { "#", "Name", "Email" } };
    //                worksheet.Cells[6, 1, 6, 3].LoadFromArrays(headerRow);
    //                worksheet.Cells[6, 1, 6, 3].Style.Font.Bold = true;
    //                worksheet.Cells[6, 1, 6, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

    //                // Add student data
    //                int rowIndex = 7;
    //                int index = 1;
    //                foreach (var student in students)
    //                {
    //                    worksheet.Cells[rowIndex, 1].Value = index++;
    //                    worksheet.Cells[rowIndex, 2].Value = student.userName;
    //                    worksheet.Cells[rowIndex, 3].Value = student.email;
    //                    rowIndex++;
    //                }

    //                // Auto-fit columns
    //                worksheet.Cells.AutoFitColumns();

    //                package.Save();
    //            }

    //            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "students.xlsx");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception (not shown in this example)
    //        return StatusCode(500, "An error occurred while generating the Excel file.");
    //    }
    //}
}




