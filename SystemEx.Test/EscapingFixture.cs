using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace SystemEx.Test
{
    [TestFixture]
    public class EscapingFixture
    {
        [Test]
        public void StringEncoding()
        {
            Assert.AreEqual("\"a\\ra\"", Escaping.StringEncode("a\ra"));
            Assert.AreEqual("\"a\\na\"", Escaping.StringEncode("a\na"));
            Assert.AreEqual("\"a\\ta\"", Escaping.StringEncode("a\ta"));
        }
    }
}
