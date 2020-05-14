using System;

namespace CertificateListener
{
    class Program
    {
        static void Main(string[] args)
        {   
            FirebaseConnector fbConnector = new FirebaseConnector();
            Console.WriteLine(fbConnector.ReadCertificates().Result.Count);
        }
    }
}
