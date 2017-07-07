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
            bool uitgevoerd = false;
            Done += () => uitgevoerd = true;

            GaIetsUitvoerenFireAndForgetMaarRaiseEventWanneerKlaar();
            Thread.Sleep(1000);

            Assert.True(uitgevoerd);
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
