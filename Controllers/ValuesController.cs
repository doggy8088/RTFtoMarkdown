using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReverseMarkdown;
using RtfPipe;
using static ReverseMarkdown.Config;

namespace rtfhandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RtfController : ControllerBase
    {
        [HttpPost]
        public string Post([FromBody] string rtf)
        {
            var html = Rtf.ToHtml(rtf);

            bool githubFlavored = true;
            bool removeComments = true;
            var config = new ReverseMarkdown.Config
            {
                UnknownTags = Config.UnknownTagsOption.PassThrough, // Include the unknown tag completely in the result (default as well)
                GithubFlavored = githubFlavored, // generate GitHub flavoured markdown, supported for BR, PRE and table tags
                RemoveComments = removeComments, // will ignore all comments
                SmartHrefHandling = true, // remove markdown output for links where appropriate
                TableWithoutHeaderRowHandling = TableWithoutHeaderRowHandlingOption.Default
            };
            var converter = new ReverseMarkdown.Converter(config);

            var markdown = converter.Convert(html);

            return markdown;
        }
    }
}