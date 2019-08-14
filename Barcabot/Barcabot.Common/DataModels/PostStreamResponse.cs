using System.IO;

namespace Barcabot.Common.DataModels
{
    public class PostStreamResponse
    {
        public string ResponseCode { get; set; }
        public Stream ResponseContent { get; set; }
    }
}