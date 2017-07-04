using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace cnetprog
{
    public class DelegateEnEventsDemo
    {
        private int aantal;

        [Fact]
        public void WatIsEenDelegate()
        {
            var f = new DemoDelegate(MijnMethode);
            Assert.Equal(0, f.Invoke());

            DemoDelegate f2 = MijnMethode;
            Assert.Equal(1, f2());
        }

        private int MijnMethode()
        {
            return aantal++;
        }

        [Fact]
        public void DelegatesZijnMultiCast()
        {
            aantal = 0;

            var f = new DemoDelegate(MijnMethode);
            f += MijnMethode;

            var result = f();
            Assert.Equal(1, result);
            Assert.Equal(2, aantal);
        }

        [Fact]
        public void WatIsEenEventDan()
        {
            var raiser = new EventDemo();
            raiser.DitIsOngeveerEenEvent = MijnMethode;
            raiser.DitIsOngeveerEenEvent += MijnMethode;
            raiser.DitIsOngeveerEenEvent();

            raiser.DitIsEenEvent += MijnMethode;
            //raiser.DitIsEenEvent();

            new EventDemo
            {
                DitIsOngeveerEenEvent = MijnMethode
            };
        }

        private class EventDemo
        {
            public event DemoDelegate DitIsEenEvent; // { add; remove; }
            public DemoDelegate DitIsOngeveerEenEvent { get; set; }

            private void ErTreedtIetsOpWatHetEventMoetRaisen()
            {
                DitIsEenEvent?.Invoke();
            }

            public event EventHandler<MyEventArgs> EenEventVolgensHetGeldendePattern;
        }

            public class MyEventArgs : EventArgs
            {
            }
        }

    public delegate int DemoDelegate();
}
