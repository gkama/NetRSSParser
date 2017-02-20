using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace NetRSSParser.Yahoo
{
    public class StockQuote
    {
        //Variables
        public string Symbol { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        private int FromDay { get; set; }
        private int FromMonth { get; set; }
        private int FromYear { get; set; }
        private int ToDay { get; set; }
        private int ToMonth { get; set; }
        private int ToYear { get; set; }

        private string _ResolutionOfData;
        private string ResolutionOfData
        {
            get { return _ResolutionOfData; }
            set
            {
                if (value.Trim().ToLower() != "w" && value.Trim().ToLower() != "d")
                    throw new Exception("Resolution of data must be Weekly='w' or Daily='d'");
                else
                    this._ResolutionOfData = value.Trim().ToLower();
            }
        }
        public Dictionary<DateTime, ParsedData.Values> Data { get; set; }

        //URL's
        private string BASE_URL = "http://ichart.yahoo.com/table.csv?s=";
        public string URL = "";

        /// <summary>
        /// Initializes a new instance of a Stock Quote requested based on a company's stock name or symbol. For example
        /// Microsoft's symbol would be 'msft'
        /// </summary>
        /// <param name="Symbol">Stock name (e.g.: Microsoft=msft)</param>
        /// <param name="FromDate">Period From Date</param>
        /// <param name="ToDate">Period To Date</param>
        /// <param name="ResolutionOfData">Resolution of the data (Daily='d' or Weekly='w')</param>
        public StockQuote(string Symbol, DateTime FromDate, DateTime ToDate, string ResolutionOfData)
        {
            this.Symbol = Symbol;
            this.FromDate = FromDate;
            this.ToDate = ToDate;

            this.FromDay = FromDate.Day;
            this.FromMonth = FromDate.Month - 1;
            this.FromYear = FromDate.Year;
            this.ToDay = ToDate.Day;
            this.ToMonth = ToDate.Month - 1;
            this.ToYear = ToDate.Year;

            this.ResolutionOfData = ResolutionOfData;

            this.URL = BASE_URL + this.Symbol + "&a=" + this.FromDay + "&b=" + this.FromMonth + "&c=" + this.FromYear +
                "&d=" + this.ToDay + "&e=" + this.ToMonth + "&f=" + this.ToYear + "&g=" + this.ResolutionOfData + "&ignore=.csv";

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                StreamReader sr = new StreamReader(resp.GetResponseStream());
                string results = sr.ReadToEnd();
                sr.Close();

                ParsedData PD = new ParsedData(results);
                this.Data = PD.Data;
            }
            catch (Exception e) { throw e; }
        }


        //Classes
        public class ParsedData
        {
            public Dictionary<DateTime, Values> Data { get; set; }

            //Constructor
            public ParsedData(string data)
            {
                Data = new Dictionary<DateTime, Values>();
                try
                {
                    string[] splitData = data.Split('\n');
                    splitData = splitData.Skip(1).ToArray();
                    foreach (string s in splitData)
                    {
                        if (string.IsNullOrEmpty(s)) { }
                        else
                        {
                            string[] splitS = s.Split(',');
                            DateTime dt = DateTime.ParseExact(splitS[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            Values v = new Values(Convert.ToDouble(splitS[1]),
                                Convert.ToDouble(splitS[2]),
                                Convert.ToDouble(splitS[3]),
                                Convert.ToDouble(splitS[4]),
                                Convert.ToInt32(splitS[5]),
                                Convert.ToDouble(splitS[6]));
                            Data.Add(dt, v);
                        }
                    }
                }
                catch (Exception e) { throw e; }
            }
            //Values
            public class Values
            {
                public Double Open { get; set; }
                public Double High { get; set; }
                public Double Low { get; set; }
                public Double Close { get; set; }
                public int Volume { get; set; }
                public Double AdjClose { get; set; }

                //Constructor;
                public Values(Double Open, Double High, Double Low, Double Close, int Volume, Double AdjClose)
                {
                    this.Open = Open;
                    this.High = High;
                    this.Low = Low;
                    this.Close = Close;
                    this.Volume = Volume;
                    this.AdjClose = AdjClose;
                }
            }
        }
    }
}
