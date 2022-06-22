using System;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Linq;

namespace GoldinAutoTradeApi.Helper
{
    public class APIBasicAuth : IHttpModule
    {
        private const string Realm = "GoldinAutoAPI";

        public void Init(HttpApplication context)
        {
            // Register event handlers
            context.AuthenticateRequest += OnApplicationAuthenticateRequest;
            context.EndRequest += OnApplicationEndRequest;
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        // TODO: Here is where you would validate the username and password.
        private static bool CheckPassword(string username, string password)
        {
            using (var context = new EF.GoldinAutoEntities())
            {
                return context.ApiUsers.Where(u => u.Username == username && u.Password == password).Count() > 0;
            }
        }

        private static void AuthenticateUser(string credentials)
        {
            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                if (CheckPassword(name, password))
                {
                    var identity = new GenericIdentity(name);
                    SetPrincipal(new GenericPrincipal(identity, null));
                }
                else
                {
                    // Invalid username or password.
                    HttpContext.Current.Response.StatusCode = 401;
                }
            }
            catch (FormatException)
            {
                // Credentials were not formatted correctly.
                HttpContext.Current.Response.StatusCode = 401;
            }
        }

        private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("basic",
                        StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    AuthenticateUser(authHeaderVal.Parameter);
                }
            }
        }

        // If the request was unauthorized, add the WWW-Authenticate header 
        // to the response.
        private static void OnApplicationEndRequest(object sender, EventArgs e)
        {

            var response = HttpContext.Current.Response;

            if (response.StatusCode != 200)
            {
                if (response.StatusCode == 401)
                {
                    response.ClearContent();
                    response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
                    response.Write("{\"ReturnCode\":\"-1\",\"ReturnMessage\":\"Authentication failed.\"}");
                }

                if (response.StatusCode == 403)
                {
                    response.ClearContent();
                    response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
                    response.Write("{\"ReturnCode\":\"-1\",\"ReturnMessage\":\"Authorization failed.\"}");
                }
            }

        }

        public void Dispose()
        {
        }
    }
}