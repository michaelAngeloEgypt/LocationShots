using LocationShots.BLL.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocationShots
{
    public static class Tests
    {
        public static void Main(String[] args)
        {
            try
            {
                //TestTime();
                TestMergeImage();
            }
            catch (Exception x)
            {
                XLogger.Error(x);
            }

        }

        private static void TestTime()
        {
            Stopwatch timer = Stopwatch.StartNew();
            XLogger.Info("BEGIN:\t Scheduled Task Execution");

            Thread.Sleep(2500);

            TimeSpan timespan = timer.Elapsed;
            var current = String.Format("{0:00}:{1:00}:{2:00}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 10);
            //var proposed = String.Format("HH:mm:ss.SSS");       //java, ignore
            var proposed = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
            var proposed2 = timespan.ToStandardElapsedFormat();

        }

        private static void TestMergeImage()
        {
            var testWordPath = @"C:\IO\Mick\8-10 Ailsa Street - Redland Bay\result.docx";
            var imageToInsert = @"C:\IO\Mick\8-10 Ailsa Street - Redland Bay\Aerial.png";


            WordInteropUtils.CreateDocument(testWordPath);
            WordInteropUtils.InsertImageInWord(testWordPath, imageToInsert);
        }

    }
}
