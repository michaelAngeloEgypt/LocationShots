using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace LocationShots.BLL.Utils
{
    public static class WordInteropUtils
    {

        public static void CreateDocument(string fileName)
        {
            Word._Application app = new Word.Application();
            object oMissing = System.Reflection.Missing.Value;
            Word._Document oDoc = app.Documents.Add(ref oMissing, ref oMissing,
            ref oMissing, ref oMissing);
            app.ActiveDocument.SaveAs2(fileName);

            oDoc.Save();
            app.Quit();
            oDoc = null;
            app = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static void InsertImageInWord(string wordPath, string imagePath)
        {
            Word._Application wordApp = new Word.Application();
            wordApp.Visible = true;
            Word._Document wordDoc = wordApp.Documents.Open(wordPath, ReadOnly: false, Visible: true);


            /* -- insert at Bookmark
            int count = wordDoc.Bookmarks.Count;
            for (int i = 1; i < count + 1; i++)
            {
                object oRange = wordDoc.Bookmarks[i].Range;
                object saveWithDocument = true;
                object missing = Type.Missing;
                wordDoc.InlineShapes.AddPicture(imagePath, ref missing, ref saveWithDocument, ref oRange);
            }
            */

            object oEndOfDoc = "\\endofdoc";
            object saveWithDocument = true;
            object missing = System.Reflection.Missing.Value;
            object wrdRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            wordDoc.InlineShapes.AddPicture(imagePath, ref missing, ref saveWithDocument, ref wrdRng);

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
