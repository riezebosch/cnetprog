using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Xunit;

namespace cnetprog
{
    public class DynamicDemo
    {
        [Fact]
        public void WatIsHetVerschilTussenDynamicEnObject()
        {
            var i = 1;
            object o = 1;
            dynamic d = 1;

            Assert.Equal(2, i * 2);
            Assert.Equal(2, ((int)o) * 2);
            Assert.Equal(2, d * 2);
        }

        [Fact]
        public void MaarWatHebJeDaarDanAan()
        {
            dynamic d = new MijnEigenDynamicObject();
            d.GoedeMorgen();

            Assert.Throws<NietZoGrofInJeSmoelException>(() => d.SlechteMorgen());
        }

        [Fact]
        public void EenDynamicKanNietAlsInterfaceFungeren()
        {
            dynamic mock = new MijnEigenDynamicObject();
            Assert.Throws<RuntimeBinderException>(() => DoeIetsMetEenInterface(mock));
        }

        [Fact]
        public void EenDynamicObjectKanLeukFungerenAlsEenSoortStateBag()
        {
            dynamic bag = new ExpandoObject();
            bag.Voornaam = "jan";

            // Dit mag je ongeveer lezen als onderstaande.
            // De gekozen naam is eigenlijk niet meer dan
            // een string die binnenkomt via de binder.
            //   bag.SetProperty("Voornaam", "Jan");

            Assert.Equal("jan", bag.Voornaam);
        }

        private bool DoeIetsMetEenInterface(IComparable comparable)
        {
            return comparable.CompareTo(3) == 0;
        }

        private class MijnEigenDynamicObject : DynamicObject
        {
            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                if (binder.Name.ToLower().Contains("slechte"))
                {
                    throw new NietZoGrofInJeSmoelException(binder.Name);
                }

                result = null;
                return true;
            }
        }

        private class NietZoGrofInJeSmoelException : Exception
        {
            public NietZoGrofInJeSmoelException(string message) : base(message)
            {
            }
        }
    }
}
