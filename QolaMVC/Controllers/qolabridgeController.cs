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
            if (Request.UrlReferrer.Authority == "2.qola.ca" || Request.UrlReferrer.Authority == "localhost:56272")
            {
                PostDataToCRM();
            }
            else if (Request.UrlReferrer.Authority == "crm.qola.ca")
            {
                if (Request.Form.Count > 0)
                {
                    string before_decoder = Request.Form["action_type"];
                    string ActionType = AES256.DecryptText(before_decoder);
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
                            Destination_page = AES256.DecryptText(Request.Form["Destination_page"]);

                        if (Request.Form["user_id"] != null)
                            user_id = Int32.Parse(AES256.DecryptText(Request.Form["user_id"]));

                        if (Request.Form["user_id"] != null)
                            qola_sessionid = AES256.DecryptText(Request.Form["qola_sessionid"]);

                        ProcessRedirect(user_id, Destination_page);
                    }
                }
            }

        }

        protected void PostDataToCRM()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;
            string sessionID = HttpContext.Session.SessionID;


            string homeidlist = "";
            if (user.Home.Substring(0, 1) == "0")
            {
                var homeall = HomeDAL.GetHomeCollections_mike();
                foreach(var single in homeall)
                {
                    homeidlist += single.Id.ToString() + ",";
                }
            }
            else
            {
                homeidlist = user.Home.ToString();
            }

            if (homeidlist.Substring(homeidlist.Length - 1, 1) == ",")
            {
                homeidlist = homeidlist.Substring(0, homeidlist.Length - 1);
            }


            string remoteUrl = "https://crmtest.qola.ca/crm-bridge/auth/login ";
            Dictionary<string, string> collections = new Dictionary<string, string>();
            collections.Add("qola_sessionid", AES256.EncryptText(sessionID));
            collections.Add("user_id", AES256.EncryptText(user.ID.ToString()));
            collections.Add("user_name", AES256.EncryptText(user.UserName));
            collections.Add("first_name", AES256.EncryptText(user.FirstName));
            collections.Add("last_name", AES256.EncryptText(user.LastName));
            collections.Add("current_home_id", AES256.EncryptText(home.Id.ToString()));
            collections.Add("home_id", AES256.EncryptText(homeidlist));
            collections.Add("email", AES256.EncryptText(user.Email));
            collections.Add("designation_id", AES256.EncryptText(user.UserType.ToString()));

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

        [HttpPost]
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



    }


    public class AES256
    {
        protected static string _encKey = "KyFe&64F0";
        public static string EncryptText(string input)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(_encKey);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        public static string DecryptText(string input)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(_encKey);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
            //end public byte[] AES_Encrypt
        }

        private static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
                //end public byte[] AES_Decrypt
            }

            return decryptedBytes;
        }

        //end class SimpleAES
    }



}





