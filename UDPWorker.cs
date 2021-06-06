//using System;
//using System.ComponentModel;
//using System.Threading;

//namespace Gauge
//{
//    public class UDPWorker
//    {
//        private static BackgroundWorker backgroundWorker;

//        static void AZBZ()
//        {
//            backgroundWorker = new BackgroundWorker
//            {
//                WorkerReportsProgress = true,
//                WorkerSupportsCancellation = true
//            };
            
//            backgroundWorker.DoWork += backgroundWorker_DoWork;
//            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
//            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

//            backgroundWorker.RunWorkerAsync("Press Enter in the next 5 seconds to Cancel operation:");
            
//        }
        

//        static void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
//        {

//            for (int i = 0; i < 200; i++)
//            {

//                if (backgroundWorker.CancellationPending)
//                {

//                    e.Cancel = true;

//                    return;

//                }

//                backgroundWorker.ReportProgress(i);

//                Thread.Sleep(1000);

//                e.Result = 1000;

//            }

//        }

//        static void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
//        {

//            Console.WriteLine("Completed" + e.ProgressPercentage + "%");

//        }

//        /// <summary>    

//        /// Displays result of background performing operation.    

//        /// </summary>    

//        /// <param name="sender"></param>    

//        /// <param name="e"></param>    

//        static void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//        {

//            if (e.Cancelled)
//            {

//                Console.WriteLine("Operation Cancelled");

//            }

//            else if (e.Error != null)
//            {

//                Console.WriteLine("Error in Process :" + e.Error);

//            }

//            else
//            {

//                Console.WriteLine("Operation Completed :" + e.Result);

//            }

//        }

//    }

//}