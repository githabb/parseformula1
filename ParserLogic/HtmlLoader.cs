using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParserLogic
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(ParserSettings settings)
        {
            client = new HttpClient();
            url = settings.Link;
        }

        public async Task<string> GetPageSource()
        {
            var response = await client.GetAsync(url);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
