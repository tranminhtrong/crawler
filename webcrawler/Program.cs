using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using webcrawler.util;
using System.IO;
using System;
using webcrawler.model;

namespace webcrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            };

            //Load trang web, nạp html vào document
            HtmlDocument document = htmlWeb.Load("http://www.24h.com.vn/ttcb/giavang/giavang.php");
            string inputFilePath = utils.getCompileFilePathAndFileExtension(); 

            var isFileExisted = System.IO.File.Exists(inputFilePath);
            if (isFileExisted == false) {
                var createFileStream = System.IO.File.Create(inputFilePath);
                createFileStream.Close();
            }
            // Read existing json data
            var jsonData = System.IO.File.ReadAllText(inputFilePath);
            // De-serialize to object or create new list
            var GoldInforList = JsonConvert.DeserializeObject<List<GoldInfors>>(jsonData)
                                  ?? new List<GoldInfors>();

            ///1. Su dung XPATH de tim Node
            ///

            ////Load các tag li trong tag ul
            //var threadItems = document.DocumentNode.SelectNodes("#div_ban_tin_gia_vang_2 > div.xem-them-gold").ToList();


            //foreach (var item in threadItems)
            //{
            //    //Extract các giá trị từ các tag con của tag li
            //    var linkNode = item.SelectSingleNode(".//a[contains(@class,'title')]");
            //    var link = linkNode.Attributes["href"].Value;
            //    var text = linkNode.InnerText;
            //    var readCount = item.SelectSingleNode(".//div[@class='folTypPost']/ul/li/b").InnerText;

            //    items.Add(new { text, readCount, link });
            //}

            ///2. Linq to object
            ///
            //var threadItems = document.DocumentNode.Descendants("ul")
            //    .First(node => node.Attributes.Contains("id") && node.Attributes["id"].Value == "threads")
            //    .ChildNodes.Where(node => node.Name == "li").ToList();

            //foreach (var item in threadItems)
            //{
            //    var linkNode = item.Descendants("a").First(node =>
            //    node.Attributes.Contains("class") && node.Attributes["class"].Value.Contains("title"));
            //    var link = linkNode.Attributes["href"].Value;
            //    var text = linkNode.InnerText;
            //    var readCount = item.Descendants("b").First().InnerText;

            //    items.Add(new { text, readCount, link });
            //}

            ///3. Su dung Fizzler
            ///
            var threadItems = document.DocumentNode.QuerySelectorAll("#div_ban_tin_gia_vang_2 > table tr").ToList();

            foreach (var item in threadItems)
            {
                var loaiVang = item.QuerySelector("td").InnerText;
                if (loaiVang.Equals("SJC TP HCM"))
                {
                    var nodes = item.ChildNodes;
                    string sessionName = "Open";
                    DateTime sessionDate = System.DateTime.Now;
                    string goldDistributorName = nodes[1].InnerText;
                    string buy = nodes[3].InnerText;
                    string sell = nodes[5].InnerText;

                    GoldInforList.Add(new GoldInfors() {
                        SessionName = sessionName,
                        SessionDate = sessionDate,
                        GoldDistributorName = goldDistributorName,
                        Buy = Convert.ToDouble(buy),
                        Sell = Convert.ToDouble(sell)
                    });
                }

                //var linkNode = item.QuerySelector("a.title");
                //var link = linkNode.Attributes["href"].Value;
                //var text = linkNode.InnerText;
                //var readCount = item.QuerySelector("div.folTypPost > ul > li > b").InnerText;

                //items.Add(new { link, text, readCount });


            }

            jsonData = JsonConvert.SerializeObject(GoldInforList, Formatting.Indented);
            System.IO.File.WriteAllText(inputFilePath, jsonData);

        }
    }
}
