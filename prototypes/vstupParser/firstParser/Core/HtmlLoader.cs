using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace firstParser.Core
{
    class HtmlLoader
    {
        readonly HttpClient cliennt;
        readonly string url;

        public HtmlLoader(IParcerSettings settings)
        {
            if (settings.Prefix != null)
                url = $"{settings.BaseUrl}{settings.Prefix}";
            else
                url = $"{settings.BaseUrl}";
            cliennt = new HttpClient();
        }

        public async Task<string> GetSourceByPageId()
        {
            var currentUrl = url;//.Replace("CurrentId", "");
            var response = await cliennt.GetAsync(currentUrl);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }
            return source;
        }
    }
}
