using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace FakeSDK
{
    class FakeManager
    {
        private TaskCompletionSource<bool> ready = new TaskCompletionSource<bool>();

        public FakeManager()
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(3);
            dt.Tick += (sender, ev) =>
            {
                ready.SetResult(true);
                dt.Stop();
            };
            dt.Start();
        }

        public async Task<int> GetAnIntegerAsync()
        {
            if (!await ready.Task)
            {
                throw new InvalidOperationException("init failed!");
            }

            Debug.WriteLine("GetAnIntegerAsync() get called");
            return 0;
        }
    }
}
