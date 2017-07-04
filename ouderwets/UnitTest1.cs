using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ouderwets
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RethrowVanException()
        {
            try
            {
                RethrowException(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine();
            
            try
            {
                RethrowException(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void RethrowException(bool zoalshethoort)
        {
            try
            {
                int.Parse("asdf");
            }
            catch (Exception ex)
            {
                if (zoalshethoort)
                {
                    throw;
                }

                throw ex;
            }
        }

    }
}
