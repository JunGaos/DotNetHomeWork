using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace MyCrawler
{
    class Crawler
    {
        //代码已按照老师所给代码修改
        public event Action<Crawler> CrawlerStopped;
        public event Action<Crawler, string, string> PageDownloaded;

        //已下载的URL，key是URL，value表示是否下载成功
        private Dictionary<string, bool> downloded = new Dictionary<string, bool>();
        //URL解析表达式
        public static readonly string urlParse = @"^(?<site>https?://(?<host>[\w.-]+)(:\d+)?($|/))(\w+/)*(?<file>[^#?]*)";
        //待下载队列
        private Queue<string> waiting = new Queue<string>();

        public string HostFilter { get; set; } //主机过滤规则
        public string FileFilter { get; set; } //文件过滤规则
        public string StartURL { get; set; } //起始网址

        public void Crawl()
        {
            Console.WriteLine("开始爬行:");
            while (downloded.Count < 100 && waiting.Count > 0)
            {
                string url = waiting.Dequeue();
                try
                {
                    string html = DownLoad(url); // 下载的网页
                    downloded[url] = true;
                    PageDownloaded(this, url, "success");
                    Parse(html, url);//解析,并加入新的链接
                }
                catch (Exception ex)
                {
                    PageDownloaded(this, url, " Error:" + ex.Message);
                }
            }
            CrawlerStopped(this);
            Console.WriteLine("爬行结束");
        }

        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = downloded.Count.ToString()+".txt";
                File.WriteAllText(fileName, html, Encoding.UTF8);
                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        private void Parse(string html, string startUrl)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'](?<url>[^""'#>]+)[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                string linkUrl = match.Groups["url"].Value;
                if (linkUrl == null || linkUrl == "") continue;
                linkUrl = ChangeUrl(linkUrl, startUrl);//转绝对路径,解析出host和file两个部分，进行过滤
                Match linkUrlMatch = Regex.Match(linkUrl, urlParse);
                string host = linkUrlMatch.Groups["host"].Value;
                string file = linkUrlMatch.Groups["file"].Value;
                if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(file, FileFilter)
                  && !downloded.ContainsKey(linkUrl))
                {
                    waiting.Enqueue(linkUrl);
                }
            }
        }
        public void Start()
        {
            downloded.Clear();
            waiting.Clear();
            waiting.Enqueue(StartURL);
            Crawl();

        }
        private string ChangeUrl(string url, string pageUrl)   //相对路径转为绝对路径
        {
            if (url.Contains("://"))
            {
                return url;
            }
            if (url.StartsWith("/"))
            {
                Match urlMatch = Regex.Match(pageUrl, urlParse);
                String site = urlMatch.Groups["site"].Value;
                return site.EndsWith("/") ? site + url.Substring(1) : site + url;
            }
            if (url.StartsWith("../"))
            {
                url = url.Substring(3);
                int idx = pageUrl.LastIndexOf('/');
                return ChangeUrl(url, pageUrl.Substring(0, idx));
            }
            if (url.StartsWith("./"))
            {
                return ChangeUrl(url.Substring(2), pageUrl);
            }
            if (url.Contains("://"))
            {
                return url;
            }
            if (url.StartsWith("//"))
            {
                return "http:" + url;
            }

            int end = pageUrl.LastIndexOf("/");
            return pageUrl.Substring(0, end) + "/" + url;
        }
    }
}
