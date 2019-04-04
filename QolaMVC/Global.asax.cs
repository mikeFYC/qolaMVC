using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Net.Mail;
using System.Diagnostics;
using System.ComponentModel;
using QolaMVC.Models;
using QolaMVC.Controllers;

namespace QolaMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);



        }

        protected void Application_Error(object sender, EventArgs e)
        {

            string userinfo = "";
            string location = "";
            string htmlBody = "";
            string sessionExpire = "";
            UserModel userEX = new UserModel();
            Exception exc = Server.GetLastError();

            if (Session["check"] == null)
            {
                sessionExpire = "Session Expired<br><br>";
            }
            htmlBody = exc.Message;

            var httpContext = ((HttpApplication)sender).Context;
            httpContext.Response.Clear();
            //httpContext.ClearError();

            if (Session["USER"] == null)
            {
                ExecuteErrorActionInHomeController(httpContext, exc);
                userEX = (UserModel)Session["USER"];

                if (userEX != null)
                {
                    userinfo = userEX.FirstName + " " + userEX.LastName + "<br>" + userEX.ID + "<br><br>";
                }

                var st = new StackTrace(exc, true); // create the stack trace
                var query = st.GetFrames()         // get the frames
                                .Select(frame => new
                                {                   // get the info
                                FileName = frame.GetFileName(),
                                    LineNumber = frame.GetFileLineNumber(),
                                    ColumnNumber = frame.GetFileColumnNumber(),
                                    Method = frame.GetMethod(),
                                    Class = frame.GetMethod().DeclaringType,
                                });
                foreach (var single in query.ToList())
                {
                    if (single.FileName != null)
                    {
                        location += single.FileName + "<br>" + single.Method + "<br><br>";
                    }
                }

                SendEmail(userinfo, location, htmlBody, sessionExpire);
                //Server.ClearError();
            }

            //Response.Redirect("/Login/ErrorPage");
            


        }
        
        private void SendEmail(string userinfo, string location, string htmlBody, string sessionExpire)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("mike@qola.ca");
            mail.To.Add("mike@qola.ca");
            mail.To.Add("ashley@qola.ca");
            mail.Subject = "QOLA Error Happened";
            mail.IsBodyHtml = true;

            mail.Body = sessionExpire + userinfo + location + htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("mike@qola.ca", "Password123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail); 
        }

        private void ExecuteErrorActionInHomeController(HttpContext httpContext, Exception exception)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "Home";
            routeData.Values["action"] = "ERROR";
            using (Controller controller = new HomeController())
            {
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if (cookie != null && cookie.Value != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
        }
    }
}
