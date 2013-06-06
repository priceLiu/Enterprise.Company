using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HtmlAgilityPack;

namespace ConsoleApplication
{
    class Program
    {
        const string url = "http://www.cardbaobao.com/bank/bankwdsearch.asp?page=1&search_word=&sheng=100&shi=373&qu=2096&sjid=&bankid=36&tpa=";
        static void Main(string[] args)
        {
            HtmlWeb web=new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["id"];
                Console.WriteLine(att.Value);
            }
            Console.Read();
        }
    }
}
