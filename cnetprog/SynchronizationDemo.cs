using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace cnetprog
{
    public class SynchronizationDemo
    {
        Action Done;

        [Fact]
        public void DezeTestMoetWachtenTotErErgensAndersWatGebeurdIs()
        {

            using (var mre = new ManualResetEventSlim())
            {
                Done += () => mre.Set();

                GaIetsUitvoerenFireAndForgetMaarRaiseEventWanneerKlaar();
                Assert.True(mre.WaitHandle.WaitOne(TimeSpan.FromSeconds(5)));
            }
        }

        private void GaIetsUitvoerenFireAndForgetMaarRaiseEventWanneerKlaar()
        {
            Task.Run(async () =>
            {
                await Task.Delay(500);
                Done?.Invoke();
            });
        }
    }
}
