using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using WebsiteBlocker.Domain.Interfaces.Checks;

namespace WebsiteBlocker.Domain.Checks
{
    public class HtmlCheck : IWebsiteBlockerCheck
    {
        private IList<string> BlacklistedWords { get; }

        public HtmlCheck(IList<string> blacklistedWords)
        {
            BlacklistedWords = blacklistedWords;
        }

        public bool ValidateUrl(string url)
        {
            if(url == null || this.BlacklistedWords == null) throw new ArgumentNullException();

            var htmlStream = ReadHtml(url);
            if (htmlStream == null) throw new InvalidOperationException();

            var htmlDoc = LoadHtmlDocument(htmlStream);

            return htmlDoc.DocumentNode.Descendants().Any(CheckHtmlNodeText);
        }

        private bool CheckHtmlNodeText(HtmlNode node)
        {
            return this.BlacklistedWords.Any(x => Regex.Matches(node.InnerText, x, RegexOptions.IgnoreCase).Count > 0);
        }

        //Public and Virtual for mocking
        public virtual HtmlDocument LoadHtmlDocument(Stream htmlStream)
        {
            var doc = new HtmlDocument();
            doc.Load(htmlStream);
            return doc;
        }

        //Public and Virtual for mocking
        public virtual Stream ReadHtml(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var receiveStream = response.GetResponseStream();
                return receiveStream;
            }
            return null;
        }
    }
}
