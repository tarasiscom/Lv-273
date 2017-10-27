using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Parsing.DataClasses;


namespace Parsing
{
    public interface IParser
    {
        string Url { get; set; }

        void ChangeUrl(string url);

        List<Direction> Directions { get; }

        List<Speciality> Specialities { get; }

        List<University> Universities { get; }

        HtmlNodeCollection RetreiveNodes(string xPath);

        HtmlNode RetreiveNode(string xPath);

        void GetInfo(int idUniv, string district, HtmlNode univNode, IEnumerable<HtmlNode> nodes, Dictionary<string, string> specFields);
    }
}
