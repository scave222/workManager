using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Concurrent.Futures;
using AndroidX.Work;
using Google.Common.Util.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using workManager.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SimpleListenableWorkerCall))]
namespace workManager.Droid
{
    public class SimpleListenableWorker : ListenableWorker, CallbackToFutureAdapter.IResolver
    {
        public const string TAG = "SimpleListenableWorker";
        private static Context context = global::Android.App.Application.Context;
        public SimpleListenableWorker(Context context, WorkerParameters workerParams) : base(context, workerParams)
        {
        }
        public override IListenableFuture StartWork()
        {
            Log.Debug(TAG, "Started.");
            return CallbackToFutureAdapter.GetFuture(this);
        }
        public Java.Lang.Object AttachCompleter(CallbackToFutureAdapter.Completer p0)
        {
            Log.Debug(TAG, $"Executing.");

            //Switch to background thread.
            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                Toast.MakeText(context, "Oh Greeter Service is Destroyed.", ToastLength.Long).Show();

                return true;
            });
            //Task.Run(async () =>
            //{
            //    //Perform a process here, simulated by a delay for 5 seconds.
            //    Toast.MakeText(context, "Oh Greeter Service is Destroyed.", ToastLength.Long).Show();
            //    await Task.Delay(5000);

            //    Log.Debug(TAG, "Completed.");

            //    //Set a Success Result on the completer and return it.
            //    return p0.Set(Result.InvokeSuccess());
            //});

            return TAG;
        }

    }

    public class SimpleListenableWorkerCall : ISimpleListenableWorkerCall
    {
        private static Context context = global::Android.App.Application.Context;
        public void SimpleListenableWrk()
        {
            var simpleListenableWorkerRequest =
            new OneTimeWorkRequest.Builder(typeof(SimpleListenableWorker))
            .AddTag(SimpleListenableWorker.TAG)
            .Build();

            WorkManager.GetInstance(context).BeginUniqueWork(
            SimpleListenableWorker.TAG, ExistingWorkPolicy.Keep, simpleListenableWorkerRequest)
            .Enqueue();
        }
    }
}