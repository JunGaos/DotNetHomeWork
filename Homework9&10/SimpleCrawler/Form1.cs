using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCrawler
{
    public partial class Form1 : Form
    {
        Crawler myCrawler = new Crawler();
        BindingSource resultBindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = resultBindingSource;
            myCrawler.PageDownloaded += Crawler_PageDownloaded;
            myCrawler.CrawlerStopped += Crawler_CrawlerStopped;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            resultBindingSource.Clear();
            myCrawler.StartURL = textBoxURL.Text;

            Match match = Regex.Match(myCrawler.StartURL, Crawler.urlParse);
            if (match.Length == 0) return;
            string host = match.Groups["host"].Value;
            myCrawler.HostFilter = "^" + host + "$";
            myCrawler.FileFilter = "((.html?|.aspx|.jsp|.php)$|^[^.]+$)";

            //new Thread(myCrawler.Start).Start();
            Task task = Task.Run(() => myCrawler.Start());
            textBoxMessage.Text = "爬取已启动";
        }
        private void Crawler_CrawlerStopped(Crawler obj)
        {
            Action action = () => textBoxMessage.Text = "爬取已停止";
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
        private void Crawler_PageDownloaded(Crawler crawler, string url, string info)
        {
            var pageInfo = new { Index = resultBindingSource.Count + 1, URL = url, Status = info };
            Action action = () => { resultBindingSource.Add(pageInfo); };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
