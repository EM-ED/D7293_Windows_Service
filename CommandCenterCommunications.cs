using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace D7293_Windows_Service
{
    internal class CommandCenterCommunications
    {
        IPAddress ServerEndPoint { get; set; }
        public CommandCenterCommunications(IPAddress ServerEndPoint_)
        {
            ServerEndPoint = ServerEndPoint_;

        }

        public async Task PostRequestAsync(Dictionary<string, string> Key_And_Values)
        {

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Set the content type header to indicate JSON
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                    // Serialize the data to JSON
                    string jsonBody = JsonConvert.SerializeObject(Key_And_Values);

                    // Create the content to be sent
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send the POST request
                    await httpClient.PostAsync(ServerEndPoint.ToString(), content);
                }
            }
            catch
            {

            }


        }

    }
}
