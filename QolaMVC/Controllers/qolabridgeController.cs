using QolaMVC.DAL;
using QolaMVC.Models;
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
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] AESkey = mySHA256.ComputeHash(Encoding.Default.GetBytes("xxxxxxxxxx"));
            byte[] AESIV = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            if (Request.UrlReferrer.Authority == "qola.ca" || Request.UrlReferrer.Authority == "localhost:56272")
            {
                PostDataToCRM(AESkey, AESIV);
            }
            else if (Request.UrlReferrer.Authority == "crm.qola.ca")
            {
                if (Request.Form.Count > 0)
                {
                    string before_decoder = Request.Form["action_type"];
                    string ActionType = DecryptAesManaged(before_decoder, AESkey, AESIV);
                    if (ActionType == "Logout")
                    {
                        ProcessLogOutQola();
                    }
                    else if (ActionType == "Redirect")
                    {
                        string Destination_page = "";
                        int user_id = 0;
                        string qola_sessionid = "";

                        if (Request.Form["Destination_page"]!=null)
                            Destination_page = DecryptAesManaged(Request.Form["Destination_page"], AESkey, AESIV);

                        if (Request.Form["user_id"] != null)
                            user_id = Int32.Parse(DecryptAesManaged(Request.Form["user_id"], AESkey, AESIV));

                        if (Request.Form["user_id"] != null)
                            qola_sessionid = DecryptAesManaged(Request.Form["qola_sessionid"], AESkey, AESIV);

                        ProcessRedirect(user_id, Destination_page);
                    }
                }
            }

        }

        protected void PostDataToCRM(byte[] AESkey, byte[] AESIV)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;

            //string a = EncryptAesManaged("1", AESkey, AESIV);
            //string b = DecryptAesManaged(a, AESkey, AESIV);

            string remoteUrl = "https://crm.qola.ca/CRMBridge";
            Dictionary<string, string> collections = new Dictionary<string, string>();
            collections.Add("qola_sessionid", EncryptAesManaged("1", AESkey, AESIV));
            collections.Add("user_id", EncryptAesManaged(user.ID.ToString(), AESkey, AESIV));
            collections.Add("user_name", EncryptAesManaged(user.UserName, AESkey, AESIV));
            collections.Add("first_name", EncryptAesManaged(user.FirstName, AESkey, AESIV));
            collections.Add("last_name", EncryptAesManaged(user.LastName, AESkey, AESIV));
            collections.Add("home_id", EncryptAesManaged(home.Id.ToString(), AESkey, AESIV));
            collections.Add("email", EncryptAesManaged(user.Email, AESkey, AESIV));
            collections.Add("designation_id", EncryptAesManaged(user.UserType.ToString(), AESkey, AESIV));

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

        protected void ProcessLogOutQola()
        {
            string remoteUrl = "/Login/LogOut";
            string html = "<html><head>";
            html += "</head><body onload='document.forms[0].submit()'>";
            html += string.Format("<form name='PostForm' method='POST' action='{0}'>", remoteUrl);
            html += "</form></body></html>";
            Response.Clear();
            Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.HeaderEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.Charset = "ISO-8859-1";
            Response.Write(html);
            Response.End();
        }

        protected void ProcessRedirect(int user_id,string Destination_page)
        {
            var user = UserDAL.GetUserById(user_id);
            TempData["User"] = user;
            string remoteUrl = "/"+ Destination_page + "/Index";
            string html = "<html><head>";
            html += "</head><body onload='document.forms[0].submit()'>";
            html += string.Format("<form name='PostForm' method='POST' action='{0}'>", remoteUrl);
            html += "</form></body></html>";
            Response.Clear();
            Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.HeaderEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.Charset = "ISO-8859-1";
            Response.Write(html);
            Response.End();
        }

        static string EncryptAesManaged(string raw,byte[] key,byte[] iv)
        {
            // Create Aes that generates a new key and initialization vector (IV).    
            // Same key must be used in encryption and decryption    
            using (AesManaged aes = new AesManaged())
            {
                aes.KeySize = 256;
                aes.Key = key;
                aes.IV = iv;
                // Encrypt string   
                byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                // Print encrypted string   
                string S = Encoding.Default.GetString(encrypted);
                byte[] B = Encoding.Default.GetBytes(S);

                return S;
                // Decrypt the bytes to a string.    
                //string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                // Print decrypted string. It should be same as raw data    
                //Console.WriteLine("Decrypted data: {decrypted}");
            }

        }
        static string DecryptAesManaged(string raw, byte[] key, byte[] iv)
        {
            // Create Aes that generates a new key and initialization vector (IV).    
            // Same key must be used in encryption and decryption    
            using (AesManaged aes = new AesManaged())
            {
                aes.KeySize = 256;
                aes.Key = key;
                aes.IV = iv;
                // Encrypt string    
                byte[] encrypted = Encoding.Default.GetBytes(raw);
                // Print encrypted string    
                //return System.Text.Encoding.UTF8.GetString(encrypted);
                // Decrypt the bytes to a string.    
                string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                // Print decrypted string. It should be same as raw data    
                return decrypted;
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





