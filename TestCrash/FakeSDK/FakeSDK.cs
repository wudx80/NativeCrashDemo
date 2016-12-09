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

        private FakeSDK()
        {

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

        private void FakeSDK_Resuming(object sender, EventArgs e)
        {
            // no op
        }

        private async void FakeSDK_Suspending(object sender, SuspendingEventArgs e)
        {
            int a = await getAnInteger();
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
