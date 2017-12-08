using System;
using System.IO;
using System.Net;

namespace Parsing
{
    static class ErrorsLog
    {
        //404 not found
        public static bool IsAvailable(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }

            catch (WebException ex)
            {
                using (StreamWriter sw = new StreamWriter("errorlog.txt", true))
                {
                    sw.WriteLine(String.Format("date:{0}; time{1}; error:{2} - {3} ",
                                                DateTime.Now.Date, DateTime.Now.TimeOfDay, url, (((HttpWebResponse)ex.Response).StatusCode)));
                    sw.WriteLine();
                }
                return false;
            }
        }
    }
}
