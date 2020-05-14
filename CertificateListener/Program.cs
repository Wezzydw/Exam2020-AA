using System;
using System.Runtime.ConstrainedExecution;

namespace CertificateListener
{
    class Program
    {
        static void Main(string[] args)
        {   
            Certificate certificate = new Certificate();
            certificate.mUserUid = "SluEwNNVe6gjGmZD1Z3STvLelOa2";
            FirebaseConnector fbConnector = new FirebaseConnector();
            Console.WriteLine(fbConnector.ReadCertificates().Result.Count);
            Console.WriteLine(fbConnector.getUserEmail(certificate).Result.mEmail);
        }
    }
}
