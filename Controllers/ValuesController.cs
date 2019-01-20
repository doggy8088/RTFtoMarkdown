using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RtfPipe;

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
            var config = new ReverseMarkdown.Config(ReverseMarkdown.Config.UnknownTagsOption.PassThrough, githubFlavored : githubFlavored, removeComments : removeComments);
            var converter = new ReverseMarkdown.Converter(config);

            // var converter = new ReverseMarkdown.Converter();

            var markdown = converter.Convert(html);

            return markdown;
        }
    }
}