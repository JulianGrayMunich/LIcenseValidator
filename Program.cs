using System;
using Microsoft.Win32;
using System.Globalization;


namespace LicenseCreator
{
    class Program
    {
        public static void Main(string[] args)
        {


             // displays the license remaining period
            //
#pragma warning disable CA1416
#pragma warning disable CS8600
#pragma warning disable CS8602
#pragma warning disable CS8604

            Console.WriteLine("Check Software Key");
            Console.WriteLine();

            string strSoftwareLicenseTag = "";


            try
            {
                // Extract the license settings
                strSoftwareLicenseTag = System.Configuration.ConfigurationManager.AppSettings["SoftwareLicenseTag"];

                // To set the software reference date
                string strReferenceDateSubKey = @"SOFTWARE\Portunus_" + strSoftwareLicenseTag;
                string strDurationSubKey = @"SOFTWARE\Diebus_" + strSoftwareLicenseTag;

                RegistryKey key = Registry.CurrentUser.OpenSubKey(strDurationSubKey);
                string strSoftwareValidityPeriod = key.GetValue("TempusValide", "No Value").ToString();

                key = Registry.CurrentUser.OpenSubKey(strReferenceDateSubKey);
                string strReferenceDate = key.GetValue("Clavis", "No Value").ToString();

                DateTime InstallDate = DateTime.ParseExact(strReferenceDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime TodayDate = DateTime.Today;

                TimeSpan interval = (TodayDate - InstallDate);
                int iRemainingDays = Convert.ToInt16(strSoftwareValidityPeriod) - interval.Days;

                Console.WriteLine("License '" + strSoftwareLicenseTag + "' validated");
                Console.WriteLine("Installation date: " + strReferenceDate);
                Console.WriteLine("Validity period: " + strSoftwareValidityPeriod + " days");
                Console.WriteLine("Remaining period: " + iRemainingDays + " days");
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("The license validation has failed");
                Console.WriteLine("Most likely, '" + strSoftwareLicenseTag +"' has not been created");
                Console.WriteLine("Run the License Creator software to create the license");
                Console.WriteLine("");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                Environment.Exit(0);
  
            }





        }
    }
}