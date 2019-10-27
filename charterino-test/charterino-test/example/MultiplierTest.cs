using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using charterino_bo.example;

namespace charterino_test
{
    class MultiplierTest
    {
        private CoolMultiplier multiplier = new CoolMultiplier();


        [Test]
        public void ShouldMultiplyNumbers()
        {
            int a = 10;
            int b = 5;

            int result = multiplier.multiply(a, b);

            Assert.AreEqual(result, 50);
        }
    }
}
