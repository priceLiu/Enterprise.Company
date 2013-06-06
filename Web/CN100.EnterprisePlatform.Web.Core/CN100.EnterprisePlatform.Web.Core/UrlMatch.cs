using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
namespace CN100.EnterprisePlatform.Web.Core
{
    public class UrlMatch
    {
        private Dictionary<string, UrlItem> mUrls = new Dictionary<string, UrlItem>();

        private List<UrlItem> mMatchs = new List<UrlItem>();

        public void Add(params UrlItem[] regexurl)
        {
            mMatchs.AddRange(regexurl);

        }

        public void LoadDefault()
        {
            string path = System.IO.Path.GetDirectoryName(typeof(UrlMatch).Assembly.Location) + @"\urlmatch.ini";
            if (System.IO.File.Exists(path))
            {
                LoadFile(path);
            }
        }

        public void LoadFile(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNode root = doc["urls"];
            foreach (XmlNode url in root.ChildNodes)
            {
                UrlItem item = new UrlItem();
                item.UrlRegex = url.Attributes["match"].InnerText;
                item.Expiry = int.Parse(url.Attributes["expiry"].InnerText);
                mMatchs.Add(item);
            }
        }

        private UrlItem Builder(string url)
        {
            lock (mUrls)
            {
                for (int i = 0; i < mMatchs.Count; i++)
                {
                    if (Regex.IsMatch(url, mMatchs[i].UrlRegex))
                    {
                        mUrls.Add(url, mMatchs[i]);
                        return mMatchs[i];
                    }
                }
                mUrls.Add(url, null);
                return null;
            }
        }

        public UrlItem Match(string url)
        {
            UrlItem result = null;
            if (!mUrls.TryGetValue(url, out result))
            {
                Builder(url);
            }
            return result;
        }
    }
    public class UrlItem
    {
        public int Expiry
        {
            get;
            set;
        }
        public string UrlRegex
        {
            get;
            set;
        }
    }
}
