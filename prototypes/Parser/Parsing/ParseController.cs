using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using HtmlAgilityPack;

namespace Parsing
{
    class ParseController
    {
        string indexPage = "http://vstup.info";
        string year = "/2017";
       
        Dictionary<string, string> specialityFields = new Dictionary<string, string>();

        IParser parser;
        ISaver saver;
        
        int counter;

        public void Start(ISaver saver, IParser parser)
        {
            this.parser = parser;
            this.saver = saver;

            specialityFields.Add("Галузь","td/span[@title ='Галузь']");
            specialityFields.Add("Спеціальність","td/span[@title ='Спеціальність']");
            specialityFields.Add("Факультет","td/span[@title ='Факультет']");
            GetData();
        }

        private void GetData()
        {
            //Parser parser = new Parser(indexPage);
            //ISaver saver = new ShowInConsole();
            HtmlNodeCollection districtNodes = parser.RetreiveNodes("//table[@id='abet']/tbody/tr/td/a");
            foreach (HtmlNode node in districtNodes)
            {
                if (node.InnerText == "Вінницька область")
                {
                    Console.WriteLine(counter.ToString()); 
                    break;
                }

                if (node.InnerText != string.Empty)
                {
                    saver.SaveDistrict(parser.GetDistrict( node));
                    parser.ChangeUrl(indexPage + node.Attributes["href"].Value);
                    HtmlNodeCollection univercitiesNodes = parser.RetreiveNodes("//table[@id='vnzt0']/tbody/tr/td/a | //table[@id='vnzt1']/tbody/tr/td/a | //table[@id='vnzt2']/tbody/tr/td/a");

                    if (univercitiesNodes != null)
                    {
                        foreach (HtmlNode univ in univercitiesNodes)
                        {
                            if (univ.InnerText != string.Empty)
                            {
                                //some univercity links return 404 code
                                if (Parser.IsAvailable(indexPage + year + univ.Attributes["href"].Value.Remove(0, 1)))
                                {
                                    parser.ChangeUrl(indexPage + year + univ.Attributes["href"].Value.Remove(0, 1));

                                    //retrieve node with university information
                                    HtmlNode univNode = parser.RetreiveNode("//div/table[@id='about']");
                                    DataClasses.University u = parser.GetUniversityInfo(parser.RetreiveNode(univNode, "tr[1]/td[2]"));
                                    saver.SaveUniversity(u);
                   

                                    IEnumerable<HtmlNode> specialitiesNodes = parser.RetreiveNodes("//div[@class = 'tab-content']/div/table/tbody/tr");

                                    if (specialitiesNodes != null)
                                    {

                                        saver.SaveSpecialities(parser.GetSpecialityInfo(specialitiesNodes, specialityFields));
                                        Console.WriteLine("_______________________________________");

                                    }
                                }
                            }
                        }
                    }                   
                }        
            } 
        }
    }
}
