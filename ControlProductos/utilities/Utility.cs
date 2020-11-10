using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Security.Principal;
using System.IO;
using System.IO.Compression;
using System.Net.Mime;
using System.Xml.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


namespace ControlProductos.utilities
{
    public class Utility
    {
        public static string SendEmail(string sConectionSMTP, string sConectionPort, string sConectionUser, string sConectionPassword, bool bConectionSSL, string sFromAddress, string sEmailDisplayName, string sToAddress, string sCopyToAddress, string BCCAddress, string sSubject, string sBody, string sFileName = "")
        {
            try
            {
                MailAddress fromAddress = new MailAddress(sFromAddress, sEmailDisplayName);

                //Added this line here
                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
                SmtpClient smtp = new SmtpClient()
                {
                    Host = sConectionSMTP,
                    Port = int.Parse(sConectionPort),
                    EnableSsl = bConectionSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = (sConectionUser.Length == 0),
                    Credentials = (sConectionUser.Length > 0 ? new NetworkCredential(sConectionUser, sConectionPassword) : null)
                };

                char[] cSeparators = new char[2];
                cSeparators[0] = ';';
                cSeparators[1] = ',';
                string[] sListToAddress = sToAddress.Split(cSeparators);
                string[] sListCopyToAddress = sCopyToAddress.Split(cSeparators);
                string[] sListBlindCopyToAddress = BCCAddress.Split(cSeparators);

                MailMessage message = new MailMessage();
                message.From = fromAddress;
                foreach (string sTo in sListToAddress)
                    if (sTo != "")
                        message.To.Add(sTo);
                foreach (string sCopyTo in sListCopyToAddress)
                    if (sCopyTo != "")
                        message.CC.Add(sCopyTo);
                foreach (string sBlindCopyTo in sListBlindCopyToAddress)
                    if (sBlindCopyTo != "")
                        message.Bcc.Add(sBlindCopyTo);
                message.Subject = sSubject;
                message.Body = sBody;
                message.IsBodyHtml = true;
                if (sFileName.Trim() != "")
                {
                    //Agregamos archivo adjunto para enviar junto con el correo.
                    string file = sFileName;
                    // Create  the file attachment for this e-mail message.
                    Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                    // Add time stamp information for the file.
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(file);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                    // Add the file attachment to this e-mail message.
                    message.Attachments.Add(data);
                }
                if (message.From.Address != "" && (message.To.Count + message.CC.Count + message.Bcc.Count) > 0)
                    smtp.Send(message);
                return "Correo enviado";
            }
            catch (Exception ex)
            {
                return "Correo NO enviado por : " + ex.ToString();
            }
        }
        public static string SendEmail(string sToAddress, string sCopyToAddress, string BCCAddress, string sSubject, string sBody, string sFileName)
        {
            try
            {
                string sEmailDisplayName = "Sistema de Encuestas COVID";
                string SendMail = ConfigurationManager.AppSettings["SendMail"];
                string sConectionSMTP = ConfigurationManager.AppSettings["PROJECT_SMTP"];
                string sConectionPort = ConfigurationManager.AppSettings["PROJECT_SMTPPORT"];
                string sConectionUser = ConfigurationManager.AppSettings["PROJECT_SMTPUSER"];
                string sConectionPassword = ConfigurationManager.AppSettings["PROJECT_SMTPPSWD"];
                bool bConectionSSL = (ConfigurationManager.AppSettings["PROJECT_SMTPSSL"] == "TRUE");

                if (SendMail != "TRUE")
                    return "Envio de correo deshabilitado ";
                else
                {
                    MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["Email"], sEmailDisplayName);

                    //Added this line here
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);

                    SmtpClient smtp = new SmtpClient()
                    {
                        Host = sConectionSMTP,
                        Port = int.Parse(sConectionPort),
                        EnableSsl = bConectionSSL,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = (sConectionUser.Length == 0),
                        Credentials = (sConectionUser.Length > 0 ? new NetworkCredential(sConectionUser, sConectionPassword) : null)
                    };

                    char[] cSeparators = new char[2];
                    cSeparators[0] = ';';
                    cSeparators[1] = ',';
                    string[] sListToAddress = sToAddress.Split(cSeparators);
                    string[] sListCopyToAddress = sCopyToAddress.Split(cSeparators);
                    string[] sListBlindCopyToAddress = BCCAddress.Split(cSeparators);

                    MailMessage message = new MailMessage();
                    message.From = fromAddress;
                    foreach (string sTo in sListToAddress)
                        if (sTo != "")
                            message.To.Add(sTo);
                    foreach (string sCopyTo in sListCopyToAddress)
                        if (sCopyTo != "")
                            message.CC.Add(sCopyTo);
                    foreach (string sBlindCopyTo in sListBlindCopyToAddress)
                        if (sBlindCopyTo != "")
                            message.Bcc.Add(sBlindCopyTo);
                    message.Subject = sSubject;
                    message.Body = sBody;
                    message.IsBodyHtml = true;
                    if (sFileName.Trim() != "")
                    {
                        //Agregamos archivo adjunto para enviar junto con el correo.
                        string file = sFileName;
                        // Create  the file attachment for this e-mail message.
                        Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                        // Add time stamp information for the file.
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(file);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                        // Add the file attachment to this e-mail message.
                        message.Attachments.Add(data);
                    }
                    if (message.From.Address != "" && (message.To.Count + message.CC.Count + message.Bcc.Count) > 0)
                        smtp.Send(message);
                    return "Correo enviado";
                }
            }
            catch (Exception ex)
            {
                return "Correo NO enviado por " + ex.ToString();
            }
        }
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }

        public static String GetComputerUserName(String NameUser, String Delimiter)
        {
            String sRequest = String.Empty;

            if (NameUser.Length > 0)
                sRequest = GetSplit(NameUser, Delimiter);
            else
                if (System.Security.Principal.WindowsIdentity.GetCurrent().Name.Length > 0)
                sRequest = GetSplit(System.Security.Principal.WindowsIdentity.GetCurrent().Name, Delimiter);
            else
                sRequest = GetSplit(Environment.UserName, Delimiter);

            return sRequest;
        }
              
        private static String GetSplit(String sValue, String Delimiter)
        {
            String sRequest = String.Empty;

            if (sValue.Contains(Delimiter))
                sRequest = sValue.Split(new string[] { Delimiter }, StringSplitOptions.None)[1];
            else
                sRequest = sValue;

            return sRequest;
        }

        public static string ValNULLString(object obj)
        {
            if (obj == DBNull.Value)
                return string.Empty;
            else
                return obj.ToString().Trim();
        }
        public static int ValNULLInt(object obj)
        {
            if (obj == DBNull.Value || obj.ToString().Trim() == string.Empty)
                return 0;
            else
            {
                int res = 0;
                if (!int.TryParse(obj.ToString(), out res))
                    res = 0;
                return res;
            }
        }
        public static double ValNULLDouble(object obj)
        {
            if (obj == DBNull.Value || obj.ToString().Trim() == string.Empty)
                return 0;
            else
            {
                double res = 0;
                if (!double.TryParse(obj.ToString(), out res))
                    res = 0;
                return res;
            }
        }
        public static decimal ValNULLDecimal(object obj)
        {
            if (obj == DBNull.Value || obj.ToString().Trim() == string.Empty)
                return 0;
            else
            {
                decimal res = 0;
                if (!decimal.TryParse(obj.ToString(), out res))
                    res = 0;
                return res;
            }
        }
        public static DateTime ValNULLDateTime(object obj, string fmt)
        {
            if (obj == DBNull.Value || obj.ToString().Trim() == string.Empty)
                return DateTime.MinValue;
            else
            {
                DateTime res = DateTime.MinValue;
                if (!DateTime.TryParse(obj.ToString(), out res))
                {
                    try
                    {
                        char[] cSeparators = new char[2];
                        cSeparators[0] = '/';
                        cSeparators[1] = '-';
                        string[] objdta = obj.ToString().Split(cSeparators);

                        switch (fmt)
                        {
                            case "dmy":
                                if (objdta.GetUpperBound(0) == 2)
                                    res = new DateTime(int.Parse(objdta[2]), int.Parse(objdta[1]), int.Parse(objdta[0]));
                                else
                                    res = DateTime.MinValue;
                                break;
                            case "mdy":
                                if (objdta.GetUpperBound(0) == 2)
                                    res = new DateTime(int.Parse(objdta[2]), int.Parse(objdta[0]), int.Parse(objdta[1]));
                                else
                                    res = DateTime.MinValue;
                                break;
                            case "ymd":
                                if (objdta.GetUpperBound(0) == 2)
                                    res = new DateTime(int.Parse(objdta[0]), int.Parse(objdta[1]), int.Parse(objdta[2]));
                                else
                                    res = DateTime.MinValue;
                                break;
                            default:
                                res = DateTime.MinValue;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        res = DateTime.MinValue;
                    }
                }
                return res;
            }
        }

    }
}