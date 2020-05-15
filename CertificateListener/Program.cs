using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace CertificateListener
{
    class Program
    {
        static void Main(string[] args)
        {
            
            MailService ms = new MailService();
            do
            {
                int startTime = DateTime.Now.Millisecond;
                
                ms.HandleUsersAndCertificates();
                int endTime = DateTime.Now.Millisecond;
                int sleepTime = 86400000 - (endTime - startTime);

                Thread.Sleep(sleepTime);
            } while (true);
        }
    }
}
