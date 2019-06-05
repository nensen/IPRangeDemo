using System;
using System.Net;
using System.Net.Sockets;

namespace IPRangeDemo
{
    public class IPv4Helper
    {
        public bool IsValid(string ip)
        {
            if (IPAddress.TryParse(ip, out IPAddress address))
            {
                return address.AddressFamily == AddressFamily.InterNetwork && ip != "0";
            }

            return false;
        }

        public long GetDecimalRepresentation(string ip)
        {
            if (!IsValid(ip))
            {
                throw new ArgumentException();
            }

            var ipParts = ip.Split('.');

            var decimalRepresentationOfIpAddress =
               Double.Parse(ipParts[0]) * Math.Pow(256, 3) +
               Double.Parse(ipParts[1]) * Math.Pow(256, 2) +
               Double.Parse(ipParts[2]) * 256 +
               Double.Parse(ipParts[3]);

            return (long)decimalRepresentationOfIpAddress;
        }

        public bool IsInRange(long point, long from, long to)
        {
            return point >= from && point <= to;
        }

        public bool IsInRange(string ip, string ipFrom, string ipTo)
        {
            long fromDec = GetDecimalRepresentation(ipFrom);
            long toDec = GetDecimalRepresentation(ipTo);
            long pointDec = GetDecimalRepresentation(ip);

            return IsInRange(pointDec, fromDec, toDec);
        }
    }
}