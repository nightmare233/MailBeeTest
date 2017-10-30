using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMailBee
{
    class Program
    {
        static void Main(string[] args)
        {
            string emailserver = "pop3.126.com";
            string account = "frankfeng24@126.com";
            string password = "Aa00000000";
            log4net.Config.XmlConfigurator.Configure();
            //Pop3EmailServer.GetQuickDownloadLastMail();
            Pop3EmailServer.GetEntireDownloadLastMail(emailserver,account,password);
            //Pop3EmailServer.GetDownloadEntireMails(emailserver,account,password,100);
            //Console.ReadKey();
        }
    }
}
