using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IPRangeDemo.Test
{
    [TestClass]
    public class IPv4HelperTest
    {
        private IPv4Helper iPv4Helper;

        [TestInitialize]
        public void StartUp()
        {
            iPv4Helper = new IPv4Helper();
        }

        [DataTestMethod]
        [DataRow("192.168.101.1", "192.168.100.1", "192.168.255.1")]
        [DataRow("1.1.1.1", "1.1.1.1", "1.1.1.1")]
        [DataRow("1.1.1.1", "1.1.1.1", "1.1.1.255")]
        public void ShouldReturnTrueForIpInRange(string ipPoint, string ipFrom, string ipTo)
        {
            var result = iPv4Helper.IsInRange(ipPoint, ipFrom, ipTo);
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("192.168.99.1", "192.168.100.1", "192.168.255.1")]
        [DataRow("1.1.1.1", "1.1.1.2", "1.1.1.1")]
        [DataRow("1.1.1.1", "1.1.2.1", "1.1.1.255")]
        public void ShouldReturnFalseForIpOutsideOfRange(string ipPoint, string ipFrom, string ipTo)
        {
            var result = iPv4Helper.IsInRange(ipPoint, ipFrom, ipTo);
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("192.168.0.1", 3232235521)]
        [DataRow("64.233.187.99", 1089059683)]
        public void ShouldReturnValidDecimalRepresentation(string ip, long ipAsDecimal)
        {
            var actualIpAsDecimal = iPv4Helper.GetDecimalRepresentation(ip);

            Assert.AreEqual(ipAsDecimal, actualIpAsDecimal);
        }

        [DataTestMethod]
        [DataRow("195.158.0.256")] // outside of valid ipv4 range
        [DataRow("2001:0db8:85a3:0000:0000:8a2e:0370:7334")] // ipv6
        [DataRow("localhost")]
        [DataRow("0")]
        [DataRow("")]
        public void ShouldFailWithArgumentExceptionForInvalidArgs(string ip)
        {
            Assert.ThrowsException<ArgumentException>(() => iPv4Helper.GetDecimalRepresentation(ip));
        }
    }
}