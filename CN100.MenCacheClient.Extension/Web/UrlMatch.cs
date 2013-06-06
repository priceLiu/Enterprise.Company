using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace CN100.MenCacheClient.Extension.Web
{
    public class UrlMatch
    {
        private Dictionary<string, bool> mUrls = new Dictionary<string, bool>();

        private List<string> mMatchs = new List<string>();

        public void Add(params string[] regexurl)
        {
            mMatchs.AddRange(regexurl);

        }

        public void LoadDefault()
        {
            string path = System.IO.Path.GetDirectoryName(typeof(UrlMatch).Assembly.Location)+@"\urlmatch.ini";
            if (System.IO.File.Exists(path))
            {
                LoadFile(path);
            }
        }

        public void LoadFile(string file)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(file))
            {
                string item = reader.ReadLine();
                while (item != null)
                {
                    mMatchs.Add(item);
                    item = reader.ReadLine();
                }
            }
        }

        private void Builder(string url)
        {
            lock (mUrls)
            {
                for (int i = 0; i < mMatchs.Count; i++)
                {
                    if (Regex.IsMatch(url, mMatchs[i]))
                    {
                        mUrls.Add(url, true);
                        return;
                    }
                }
                mUrls.Add(url, false);
            }
        }

        public bool Match(string url)
        {
            bool result = false;
            if (!mUrls.TryGetValue(url, out result))
            {
                Builder(url);
            }
            return result;
        }
    }
}
