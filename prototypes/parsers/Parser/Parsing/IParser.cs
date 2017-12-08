using System.Collections.Generic;
using Parsing.DataClasses;


namespace Parsing
{
    public interface IParser
    {
        //string Url { get; set; }

        //void ChangeUrl(string url);

        List<Direction> Directions { get; }

        List<Speciality> Specialities { get; }

        List<University> Universities { get; }

        List<Subject> Subjects { get; }

        List<SpecSub> SpecSubs { get; }

        List<District> Districts { get; }

        //HtmlNodeCollection RetreiveNodes(string xPath);

        //HtmlNode RetreiveNode(string xPath);

        //void GetInfo(int idDistrict, int idUniv, string district, HtmlNode univNode, IEnumerable<HtmlNode> nodes, Dictionary<string, string> specFields);
        
        string StartParsing();


    }
}
