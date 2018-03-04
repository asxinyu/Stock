using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockData.Entity;
namespace StockData
{
    class Program
    {
        static void Main(string[] args)
        {
            XTrace.UseConsole();
            StockHelper.AnalysisFirst();
            //StockHelper.SpliteDB();
            //GroupKind.ParseGroupInfo();
            //StockHisText.PraseHistoryData();
            Console.WriteLine("完成");
            Console.Read();
        }
    }
}
