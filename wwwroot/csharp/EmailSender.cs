using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Aditek.Models;

namespace Aditek
{
    public class EmailSender
    {
        
        IWebHostEnvironment hostingEnvironment;

        Email emailModel = new Email();
        bool valid = true;

        public void checkMail(Email model)
        {
            //model.address
            //model.attachment
            //model.date
            //model.description
            //model.email
            //model.job
            //model.name
            //model.phoneNumber
            //model.quadrature
            //model.shortDescription

            Console.WriteLine("Krastavac");
            Console.WriteLine(model.address + " " + " " + model.date + " " + model.description + " " +
                model.email + " " + " " + model.name + " " + model.phoneNumber + " ");
            //sendMail(model);
        }

        public void sendMail(Email model)
        {
            string myEmail = "aflovric@gmail.com";
            string message = "Ime: " + model.name + "\nKontakt: " + model.phoneNumber + "\nE-mail: " + model.email + "\nAdresa: " + model.address + "\n\nOpis posla: " + model.description;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            if (model.attachment != null)
            {
                foreach (var file in model.attachment)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        Attachment att = new Attachment(new MemoryStream(fileBytes), file.FileName);
                        mail.Attachments.Add(att);
                    }
                }
            }
            mail.From = new MailAddress("Adigrad <" + myEmail +">");
            mail.To.Add(myEmail);
            mail.Subject = model.subject;
            mail.Body = message;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("aflovric", "mUMiJa25") as ICredentialsByHost;
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            SmtpServer.Send(mail);
            mail.To.Clear();
            mail.Attachments.Clear();
            mail.To.Add(model.email);
            mail.Subject = "Potvrda o zaprimljenom zahtjevu";
            mail.Body = "Hvala vam što ste nam poslali zahtjev.\n\nOdgovorit ćemo vam u najkraćem mogučem roku.\n\nAdigrad";
            SmtpServer.Send(mail);
            mail.Dispose();
        }
    }
}