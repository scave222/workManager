using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using workManager.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(simpleWorkerCall))]
namespace workManager.Droid
{
    public class SimpleWorker : Worker
    {
        public const string TAG = "SimpleWorker";
        private static Context context = global::Android.App.Application.Context;

        public SimpleWorker(Context context, WorkerParameters workerParams) :
            base(context, workerParams)
        {
        }

        public override Result DoWork()
        {
            Log.Debug(TAG, "Started.");
            
            //Toast.MakeText(context, "Start Greetings from our First Service", ToastLength.Long).Show();
            //Perform a process here, simulated by sleeping for 5 seconds.

            Thread.Sleep(5000);

            Log.Debug(TAG, "Completed.");
            //Toast.MakeText(context, "End Greetings from our First Service", ToastLength.Long).Show();

            return Result.InvokeSuccess();
        }
     
    }

    public class simpleWorkerCall : IsimpleWorkerCall
    {
        private static Context context = global::Android.App.Application.Context;
        public void hh()
        {
            var simpleWorkerRequest = new OneTimeWorkRequest.Builder(typeof(SimpleWorker))
            .AddTag(SimpleWorker.TAG)
            .Build();

            WorkManager.GetInstance(context).BeginUniqueWork(
            SimpleWorker.TAG, ExistingWorkPolicy.Keep, simpleWorkerRequest)
            .Enqueue();
        }
    }
}