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
            certificate = fbConnector.ReadCertificates().Result.Find(x => x.mUserUid.Contains("SluEwNNVe6gjGmZD1Z3STvLelOa2"));
            Console.WriteLine(certificate.mExpirationDate);
            Console.WriteLine(fbConnector.getUserEmail(certificate).Result.mEmail);
            
        }
    }
}
