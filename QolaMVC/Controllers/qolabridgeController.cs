using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class qolabridgeController : Controller
    {
        // GET: qolabridge
        public void Index()
        {
            //if (Request.UrlReferrer.Authority == "qola.ca")
            //{
                PostDataToCRM();
            //}
        }



        protected void PostDataToCRM()
        {
            string remoteUrl = "https://crm.qola.ca/CRMBridge";
            Dictionary<string, string> collections = new Dictionary<string, string>();
            collections.Add("qola_sessionid", EncryptAesManaged("1"));
            collections.Add("user_id", EncryptAesManaged("1"));
            collections.Add("user_name", EncryptAesManaged("test"));
            collections.Add("first_name", EncryptAesManaged("first name"));
            collections.Add("last_name", EncryptAesManaged("last name"));
            collections.Add("home_id", EncryptAesManaged("1,2,3"));
            collections.Add("email", EncryptAesManaged("test@test.com"));

            string html = "<html><head>";
            html += "</head><body onload='document.forms[0].submit()'>";
            html += string.Format("<form name='PostForm' method='POST' action='{0}'>", remoteUrl);
            foreach (string key in collections.Keys)
            {
                html += string.Format("<input name='{0}' type='text' value='{1}'>", key, collections[key]);
            }
            html += "</form></body></html>";
            Response.Clear();
            Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.HeaderEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.Charset = "ISO-8859-1";
            Response.Write(html);
            Response.End();
        }


        static string EncryptAesManaged(string raw)
        {
            // Create Aes that generates a new key and initialization vector (IV).    
            // Same key must be used in encryption and decryption    
            using (AesManaged aes = new AesManaged())
            {
                aes.KeySize = 256;
                // Encrypt string    
                byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                // Print encrypted string    
                return System.Text.Encoding.UTF8.GetString(encrypted);
                // Decrypt the bytes to a string.    
                //string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                // Print decrypted string. It should be same as raw data    
                //Console.WriteLine("Decrypted data: {decrypted}");
            }

        }
        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return encrypted;
        }
        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }


    }



}





