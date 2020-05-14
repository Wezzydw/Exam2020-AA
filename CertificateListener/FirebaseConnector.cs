using System;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CertificateListener
{
    public class FirebaseConnector
    {
        private static readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"certificate-project.json";
        private FirestoreDb db;
        private List<Certificate> _certificates;
        

        public FirebaseConnector()
        {
            
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            this.db = FirestoreDb.Create("certificate-project-fbc50");
            _certificates = new List<Certificate>();
        }

      
        public async Task<List<Certificate>> ReadCertificates()
        {
            _certificates.Clear();
            CollectionReference certRef = db.Collection("certificates");
            QuerySnapshot snapshot = await certRef.GetSnapshotAsync();
            foreach (DocumentSnapshot docSnap in snapshot.Documents)
            {
                Certificate cert1 = new Certificate();
                Dictionary<string, object> cert = docSnap.ToDictionary();
                foreach (KeyValuePair<string, object> pair in cert)
                {
                    if ("mName" == pair.Key) cert1.mName = (String)pair.Value;
                    if ("mPhoto" == pair.Key) cert1.mPhoto = (String)pair.Value;
                    if ("mExpirationDate" == pair.Key) cert1.mExpirationDate = (String)pair.Value;
                     Console.WriteLine(pair.Key);

                }
                 _certificates.Add(cert1);
            }
            return _certificates;
        }


    }
}
