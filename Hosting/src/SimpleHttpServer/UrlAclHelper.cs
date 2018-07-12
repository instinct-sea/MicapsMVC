using System;
using System.Diagnostics;

namespace Meteo.Breeze.Server.Simple
{
    internal static class UrlAclHelper
    {
        public static void AddAddress(string address)
        {
            try
            {
                UrlAcl(address, Environment.UserDomainName, Environment.UserName, false);
            }
            catch (Exception) { }
        }

        public static void RemoveAddress(string address)
        {
            try
            {
                UrlAcl(address, Environment.UserDomainName, Environment.UserName, true);
            }
            catch (Exception) { }
        }

        private static void UrlAcl(string address, string domain, string user, bool delete)
        {
            string args;
            if (delete)
            {
                args = String.Format(@"http delete urlacl url={0}", address);
            }
            else
            {
                args = string.Format(@"http add urlacl url={0} user={1}\{2} listen=yes", address, domain, user);
            }
            ProcessStartInfo psi = new ProcessStartInfo("netsh", args)
            {
                Verb = "runas",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false
            };
            Process.Start(psi).WaitForExit();
        }
    }
}
