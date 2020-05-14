using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace CertificateListener
{
    public class MailService
    {
        private string _email;
        private string _pw;
        private bool isLessThan90 = false;
        private bool isLessThan30 = false;
        private List<Certificate> _certificates;
        private FirebaseConnector _fbc;
       
        public MailService()
        {
            
            string[] lines = null;
            if (System.IO.File.Exists(@"../../../../../../../../../../email.emsettings"))
            {
                lines = System.IO.File.ReadAllLines(@"../../../../../../../../../../email.emsettings");
            }
            else if (System.IO.File.Exists(@"../../../../../email.emsettings"))
            {
                lines = System.IO.File.ReadAllLines(@"../../../../../email.emsettings");
            }
            else if (System.IO.File.Exists(@"../../email.emsettings"))
            {
                lines = System.IO.File.ReadAllLines(@"../../email.emsettings");
            }
            else
            {
                throw new IOException("Filepath not found for email.emsettings "
                                      + Directory.GetCurrentDirectory());
            }

            _fbc = new FirebaseConnector();
            _certificates = new List<Certificate>();
            _email = lines[0];
            _pw = lines[1];

        }

        /// <summary>
        /// Generates and sends an email to the receiver with the specified message
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="message"></param>
        public void SendMail(MailMessage mail)
        {

            if (mail == null)
            {
                throw new NullReferenceException();
            }
            //Test for valid mail
            if (mail.To[0].Address.Contains("@") && mail.To[0].Address.Contains("."))
            {
                try
                {
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(_email, _pw);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new ArgumentException("Email address does not follow any standards " + mail.To[0].Address);
        }
        /// <summary>
        /// Takes a certificate and runs it through a date check, which returns a color code,
        /// depending on how far from it's expiration date today is. 
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns>String</returns>
        public bool CheckStatusOfCertificates(Certificate certificate)
        {
            DateTime expirationDate = DateTime.Parse(certificate.mExpirationDate);

            if (certificate == null)
            {
                throw new NullReferenceException();
            }
            DateTime now = DateTime.Now;

            if (expirationDate.ToOADate() - now.ToOADate() == 90)
            {
                return isLessThan90 = true;
            }
            else if (expirationDate.ToOADate() - now.ToOADate() == 30)
            {
               return isLessThan30 = true;
            }

            return false;

        }

        /// <summary>
        /// Has the responsibility of getting all users, and running their certificates through datechecks,
        /// and then sends an email if needed
        /// </summary>
        public void HandleUsersAndCertificates()
        {
            FirebaseConnector fbc = new FirebaseConnector();
            _certificates = fbc.ReadCertificates().Result;

            foreach (Certificate certificate in _certificates)
            {
                if (CheckStatusOfCertificates(certificate) == true)
                {
                    MailMessage mail = BuildEmailForUser(_fbc.getUserEmail(certificate).Result, certificate);
                    SendMail(mail);
                }
                
            }

        }

    
        /// <summary>
        /// Builds and returns an email either to notify the user of expiring certificates.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="certificatesToBeNotified"></param>
        /// <returns></returns>
        public MailMessage BuildEmailForUser(User user, Certificate certificate)
        {
            if (user == null || certificate == null)
            {
                throw new NullReferenceException("User or certificate list is null");
            }

            if (!user.mEmail.Contains("@") || !user.mEmail.Contains("."))
            {
                throw new ArgumentException("user email is not a valid email address");
            }

            if (user.mName.Equals(""))
            {
                throw new InvalidDataException("Name is empty");
            }


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("exameasv@gmail.com");
            mail.To.Add(user.mEmail);
            mail.Subject = "Your Certificates are about to expire";
            mail.Body = $"Dear {user.mName}  " +
                        $"\nThese Certificates will expire within 3 months: \n";
            
                mail.Body += certificate.mName +
                             "\n";
            

            mail.Body += $"\n" +
                         $"Sincerely the Certificate team";
            return mail;
        }
    
  
    }
}
