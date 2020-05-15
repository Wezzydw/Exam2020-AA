using System;
using System.Runtime.ConstrainedExecution;

namespace CertificateListener
{
    class Program
    {
        static void Main(string[] args)
        {   
            MailService ms = new MailService();
            /* Certificate certificate = new Certificate();
             certificate.mUserUid = "SluEwNNVe6gjGmZD1Z3STvLelOa2";
             FirebaseConnector fbConnector = new FirebaseConnector();
             certificate = fbConnector.ReadCertificates().Result.Find(x => x.mExpirationDate.Contains("May 29, 2020"));
             Console.WriteLine(certificate.mExpirationDate);
             DateTime expirationDate = DateTime.Parse(certificate.mExpirationDate);
             Console.WriteLine(expirationDate.ToString());
             Console.WriteLine(fbConnector.getUserEmail(certificate).Result.mEmail);*/
            ms.HandleUsersAndCertificates();
        }
    }
}
