using HtmlAgilityPack;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData
{
    public static class HtmlHelper
    {
        public static HtmlDocument GetHtmlByUrl(this string url,int codepage = 936)
        {
            if (url.IsNullOrEmpty()) throw new Exception("url错误");
            HtmlDocument doc = null;
            try
            {
                HtmlWeb htmlweb = new HtmlWeb();
                htmlweb.OverrideEncoding = Encoding.GetEncoding(codepage);
                = htmlweb.Load(url);
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
