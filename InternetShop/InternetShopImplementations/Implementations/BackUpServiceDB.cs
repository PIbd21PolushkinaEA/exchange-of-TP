using InternetShopModel;
using InternetShopServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace InternetShopImplementations.Implementations
{
    public class BackUpServiceDB : IBackUpService
    {
        private AbstractDbContext context;

        public BackUpServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public BackUpServiceDB()
        {
            this.context = new AbstractDbContext();
        }
        public void ClientBackUpXML(int ClientId)
        {
            var baskets = context.Baskets.Where(rec => rec.ClientId == ClientId).ToList();
            var trprs = new List<ProductBasket>();
            foreach (var t in baskets)
            {
                foreach (var m in context.ProductsBasket.Where(rec => rec.BasketId == t.Id).ToList())
                {
                    trprs.Add(m);
                }
            }
            var prescriptions = context.Products.ToList();

            string pathT = "C:\\Users\\Евгения\\Desktop\\BasketsXML.xml";
            string pathTP = "C:\\Users\\Евгения\\Desktop\\ProductsBasketXML.xml";
            string pathP = "C:\\Users\\Евгения\\Desktop\\ProductsXML.xml";

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            using (FileStream fileStream = new FileStream(pathT, FileMode.Create))
            {
                using (XmlWriter writer = XmlWriter.Create(fileStream, settings))
                {
                    writer.WriteStartElement("Baskets");
                    foreach (var treat in baskets)
                    {
                        writer.WriteStartElement("Basket");
                        writer.WriteElementString("Id", treat.Id.ToString());
                        writer.WriteElementString("NameBuy", treat.NameBuy);
                        writer.WriteElementString("SumOfChoosedProducts", treat.SumOfChoosedProducts.ToString());
                        writer.WriteElementString("IsReserved", treat.IsReserved.ToString());
                        writer.WriteElementString("DateCreate", treat.DateCreate.ToShortDateString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }

            using (FileStream fileStream = new FileStream(pathTP, FileMode.Create))
            {
                using (XmlWriter writer = XmlWriter.Create(fileStream, settings))
                {
                    writer.WriteStartElement("ProductsBasket");
                    foreach (var tp in trprs)
                    {
                        writer.WriteStartElement("ProductBasket");
                        writer.WriteElementString("Id", tp.Id.ToString());
                        writer.WriteElementString("ProductId", tp.ProductId.ToString());
                        writer.WriteElementString("BasketId", tp.BasketId.ToString());
                        writer.WriteElementString("ProductName", tp.ProductName);
                        writer.WriteElementString("Count", tp.Count.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }

            using (FileStream fileStream = new FileStream(pathP, FileMode.Create))
            {
                using (XmlWriter writer = XmlWriter.Create(fileStream, settings))
                {
                    writer.WriteStartElement("Products");
                    foreach (var pres in prescriptions)
                    {
                        writer.WriteStartElement("Product");
                        writer.WriteElementString("Id", pres.Id.ToString());
                        writer.WriteElementString("ProductName", pres.ProductName);
                        writer.WriteElementString("Price", pres.Price.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }

            Client client = context.Clients.FirstOrDefault(rec => rec.Id == ClientId);
            SendEmail(client.Email, "Бекап БД в формате XML", "", new string[] { pathT, pathTP, pathP });
        }

        public void ClientBackUpJSON(int ClientId)
        {
            string pathT = "C:\\Users\\Евгения\\Desktop\\BasketsJSON.json";
            string pathTP = "C:\\Users\\Евгения\\Desktop\\ProductsBasketJSON.json";
            string pathP = "C:\\Users\\Евгения\\Desktop\\ProductsJSON.json";


            var basket = context.Baskets.Where(rec => rec.ClientId == ClientId).ToList();
            var trprs = new List<ProductBasket>();
            foreach (var t in basket)
            {
                foreach (var m in context.ProductsBasket.Where(rec => rec.BasketId == t.Id).ToList())
                {
                    trprs.Add(m);
                }
            }
            var products = context.Products.ToList();

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Basket>));
            using (FileStream fs = new FileStream(pathT, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, basket);
            }

            jsonFormatter = new DataContractJsonSerializer(typeof(List<ProductBasket>));
            using (FileStream fs = new FileStream(pathTP, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, trprs);
            }

            jsonFormatter = new DataContractJsonSerializer(typeof(List<Product>));
            using (FileStream fs = new FileStream(pathP, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, products);
            }

            Client client = context.Clients.FirstOrDefault(rec => rec.Id == ClientId);
            SendEmail(client.Email, "Бекап БД в формате JSON", "", new string[] { pathT, pathTP, pathP });
        }

        public void SendEmail(string mailAddress, string subject, string text, string[] path)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            try
            {
                objMailMessage.From = new MailAddress("labwork15kafis@gmail.com");
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                foreach (var el in path)
                {
                    objMailMessage.Attachments.Add(new Attachment(el));
                }

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new
                NetworkCredential("labwork15kafis@gmail.com", "passlab15");

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}
