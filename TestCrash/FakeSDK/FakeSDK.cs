using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using System.Diagnostics;

namespace FakeSDK
{
    public class FakeSDK
    {
        private static FakeSDK instance;
        private FakeManager fakeMgr;

        private FakeSDK()
        {
            fakeMgr = new FakeManager();
        }

        public static FakeSDK getInstance()
        {
            if (instance == null)
            {
                instance = new FakeSDK();
            }
            return instance;
        }

        public void init()
        {
            Application.Current.Resuming += (sender, e) =>
            {
                if (Resuming != null)
                {
                    Resuming(sender, new EventArgs());
                }
            };

            Application.Current.Suspending += (sender, e) =>
            {
                if (Suspending != null)
                {
                    Suspending(sender, e);
                }
            };


            Suspending += FakeSDK_Suspending;
            Resuming += FakeSDK_Resuming;
        }

        private async void FakeSDK_Resuming(object sender, EventArgs e)
        {
            int a = await fakeMgr.GetAnIntegerAsync();
            int b = await getAnInteger();
            if (a > b)
            {
                Debug.WriteLine("Can't reach here!");
            }
        }

        private void FakeSDK_Suspending(object sender, SuspendingEventArgs e)
        {
            // no op
        }

        private async Task<int> getAnInteger()
        {
            Debug.WriteLine("getAnInteger() called");
            return 0;
        }

        private event EventHandler<SuspendingEventArgs> Suspending;
        private event EventHandler<EventArgs> Resuming;
    }
}
