using HtmlAgilityPack;
using NewLife.Log;
using NewLife.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData
{
    public static class HtmlHelper
    {
        public static HtmlDocument GetHtmlByUrl(this string url)
        {
            WebClientX client = new WebClientX();
            client.Timeout = 1000 * 120;
            var text = client.GetHtml(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(text);
            return doc;
        }

        /// <summary>
        /// 65001 utf-8
        /// 936  gb2312
        /// </summary>
        /// <param name="url"></param>
        /// <param name="codepage"></param>
        /// <returns></returns>
        public static HtmlDocument GetHtmlByUrl(this string url,int codepage)
        {
            if (url.IsNullOrEmpty() || codepage < 0) throw new Exception("url错误");
            HtmlDocument doc = null;
            try
            {
                HtmlWeb htmlweb = new HtmlWeb();
                htmlweb.OverrideEncoding = Encoding.GetEncoding(codepage);
                doc = htmlweb.Load(url);
            }
            catch(Exception err)
            {
                XTrace.WriteException(err);
            }
            return doc;
        }
        //public static HtmlNode GetSingleNodeByDoc(this HtmlDocument doc, string singleXpath)
        //{
        //}

        public static HtmlNodeCollection GetInitialNodesByDoc(this HtmlDocument doc, string singleXpath,string nodesXpath)
        {
            if (doc != null || singleXpath.IsNullOrEmpty() || nodesXpath.IsNullOrEmpty()) throw new Exception("输入数据不合法");

            return doc.DocumentNode.SelectSingleNode(singleXpath).SelectNodes(nodesXpath);
        }
    }
}
