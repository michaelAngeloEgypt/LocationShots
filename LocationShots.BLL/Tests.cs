using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace LocationShots.BLL
{
    class Tests
    {
        public static void InsertImageInWord(string wordPath, string imagePath)
        {
            Word._Application wordApp = new Word.Application();
            wordApp.Visible = true;
            Word._Document wordDoc = wordApp.Documents.Open(wordPath, ReadOnly: false, Visible: true);

            int count = wordDoc.Bookmarks.Count;
            for (int i = 1; i < count + 1; i++)
            {
                object oRange = wordDoc.Bookmarks[i].Range;
                object saveWithDocument = true;
                object missing = Type.Missing;
                wordDoc.InlineShapes.AddPicture(imagePath, ref missing, ref saveWithDocument, ref oRange);
            }

            wordDoc.Save();
            wordApp.Quit();
            wordDoc = null;
            wordApp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
