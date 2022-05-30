using Core.ReviewsCollection.Services;
using HtmlAgilityPack;
using System.IO;
using System.Net;

namespace ReviewsCollection.Services
{
    public class HtmlService : IHtmlService
    {
        public HtmlDocument GetDocumentFromUrl(string url)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.AcceptEncoding, "UTF-8");
            byte[] b = webClient.DownloadData(url);
            MemoryStream ms = new MemoryStream(b);
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.Load(ms, System.Text.Encoding.UTF8);

            return htmlDocument;
        }
    }
}