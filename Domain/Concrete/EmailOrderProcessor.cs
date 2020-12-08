using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using System.Net;
using System.Net.Mail;

namespace Domain.Concrete
{


    public class EmailSettings {

        public string MailToAddress = "smietana.bartosz@gmail.com";
        public string MailFromAddress = "smietana.bartosz@gmail.com";
        public bool UseSsl = true;
        public string Username = "smietana.bartosz@gmail.com";
        public string Password = "mulat501";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"C:\Users\smiet\Desktop\file";


    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings processor)
        {
            emailSettings = processor;

        }
        public void ProcessorOrder(Cart cart, ShippingDetails shippingInfo)
        {

            using (var smtpClient = new SmtpClient())
            {

                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username,
                          emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Nowe zamówienie")
                    .AppendLine("---")
                    .AppendLine("Produkty:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (wartość: {2:c}", line.Quantity,
                                      line.Product.Name,
                                      subtotal);
                }

                body.AppendFormat("Wartość całkowita: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Wysyłka dla:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? "")
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine("---")
                    .AppendFormat("Pakowanie prezentu: {0}",
                        shippingInfo.GiftWrap ? "Tak" : "Nie");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,   // od
                                       emailSettings.MailToAddress,     // do
                                       "Otrzymano nowe zamówienie!",          // temat
                                       body.ToString());                // treść

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);
            }


        }
    } }
