using Ganss.Excel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace LocationShots.BLL
{
    public partial class Engine
    {
        public static List<TOCChoices> ReadTOC(string TOCPath)
        {
            if (string.IsNullOrWhiteSpace(TOCPath) || !File.Exists(TOCPath))
                throw new ArgumentException("Blank or non-existant path given", nameof(TOCPath));

            try
            {
                var res = new List<TOCChoices>();
                XSSFWorkbook tocWorkbook;
                var sheetNames = new List<string>();
                using (FileStream file = new FileStream(TOCPath, FileMode.Open, FileAccess.Read))
                {
                    tocWorkbook = new XSSFWorkbook(file);
                    for (int i = 1; i < tocWorkbook.NumberOfSheets; i++)
                        sheetNames.Add(tocWorkbook.GetSheetName(i));
                }
                var tocChoices = new ExcelMapper(@TOCPath) { TrackObjects = false };
                foreach (var sheetName in sheetNames)
                {
                    var screenshot = new TOCScreenshot() {Filename = $"{sheetName}.png" };
                    screenshot.Choices = tocChoices.Fetch<TOCChoices>(sheetName).ToList();
                }

                return res;
            }
            catch (Exception x)
            {
                x.Data.Add(nameof(TOCPath), TOCPath);
                throw;
            }
        }
    }
}
