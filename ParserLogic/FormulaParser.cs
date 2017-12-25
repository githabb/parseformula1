using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace ParserLogic
{
    public class FormulaParser : IParser<RaceModel>
    {
        public RaceModel Parse(IHtmlDocument document)
        {
            var trace = document.GetElementsByClassName("ResultsArchiveTitle").First();

            var raceName = trace.InnerHtml.Trim();
            string name = raceName.Substring(0, raceName.IndexOf('\n'));


            var tbody = document.GetElementsByTagName("tbody").First();

            var trs = tbody.GetElementsByTagName("tr");

            var list = new List<RacingResult>();
            foreach (var item in trs)
            {
                var racingResult = new RacingResult();

                racingResult.Pos = item.Children[1].InnerHtml;

                racingResult.No = int.Parse(item.Children[2].InnerHtml);

                racingResult.DriverFirstName = item.Children[3].Children[0].InnerHtml;

                racingResult.DriverLastName = item.Children[3].Children[1].InnerHtml;

                racingResult.DriverShortName = item.Children[3].Children[2].InnerHtml;

                racingResult.Car = item.Children[4].InnerHtml;

                racingResult.Laps = int.Parse(item.Children[5].InnerHtml);

                string timeStr = item.Children[6].InnerHtml;
                if (item.Children[6].ChildElementCount == 0)
                {
                    if (timeStr == "DNF")
                    {
                        racingResult.Time = null;
                    }
                    else
                    {
                        racingResult.Time = TimeSpan.Parse(timeStr, CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    string secondStr = timeStr.Substring(0, timeStr.IndexOf('<'));
                    decimal seconds = decimal.Parse(secondStr, CultureInfo.InvariantCulture);
                    racingResult.Retired = new TimeSpan(0, 0, 0, (int)seconds, (int)((seconds - (int)seconds) * 1000));

                }

                racingResult.Pts = int.Parse(item.Children[7].InnerHtml);

                list.Add(racingResult);
            }

            var table = document.GetElementsByClassName("resultsarchive-col-right").First().OuterHtml;
            //document.Body.InnerHtml = table;
            //table = document.Source.Text;

            return new RaceModel() {RaceName = name, RaceResults = list.ToArray(), ResultTable = table};
        }
    }

}

