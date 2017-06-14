using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace Specialist.Web.Common.Mvc.ActionResults
{
    public class RssResult : ActionResult
    {
        public SyndicationFeed Feed { get; private set; }

        public RssResult(SyndicationFeed feed)
        {
            Feed = feed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";

            var rssFormatter = new Rss20FeedFormatter(Feed);
            using (var writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                rssFormatter.WriteTo(writer);
            }
        }
    }
}