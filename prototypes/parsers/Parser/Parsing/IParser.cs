using HtmlAgilityPack;
using Parsing.DataClasses;
using System.Collections.Generic;

namespace Parsing
{
    public interface IParser
    {
        void ChangeUrl(string url);
        HtmlNodeCollection RetreiveNodes(string xPath);
        HtmlNode RetreiveNode(string xPath);
        District GetDistrict(int ID, string districtName);

        University GetUniversityInfo(int id, string district, string name, string adress, string webSite);
        IEnumerable<Direction> GetDirections();
        List<Speciality> GetSpecialities();
        IEnumerable<University> GetUniversities();
        void GetInfo(ref int id,ref int idFac, int idUniv, string district, HtmlNode univNode, IEnumerable<HtmlNode> nodes, Dictionary<string, string> specFields);


        string Url { get; set; }
    }
}
