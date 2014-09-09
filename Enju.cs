using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Enju
{

    public class EnjuClient
    {
        public string Host { get; set; }
        public int Port { get; set; }

        const string Format = "http://{0}:{1}/cgi-lilfes/enju?sentence={2}";

        /// <summary>
        /// Initializes a new instance of the Enju class.
        /// </summary>
        /// <param name="port">Port number</param>
        /// <param name="host">Host name</param>
        public EnjuClient(int port = 10000, string host = "localhost")
        {
            this.Host = host;
            this.Port = port;
        }

        private WebRequest GetRequest(string sentence)
        {
            var url = string.Format(Format, Host, Port, Uri.EscapeDataString(sentence));
            var request = HttpWebRequest.Create(url);
            return request;
        }

        public async Task<Sentence> ParseAsync(string sentence)
        {
            var request = GetRequest(sentence);
            var response = await request.GetResponseAsync();
            return Sentence.Parse(response.GetResponseStream());
        }

        public Sentence Parse(string sentence)
        {
            var request = GetRequest(sentence);
            var response = request.GetResponse();
            return Sentence.Parse(response.GetResponseStream());
        }
    }
}
