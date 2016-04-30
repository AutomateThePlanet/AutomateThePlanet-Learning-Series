// <copyright file="HttpAdapter.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>

using System.IO;
using System.Net;
using System.Text;
using JenkinsCSharpApi.Interfaces;

namespace JenkinsCSharpApi.Infrastructure
{
    /// <summary>
    /// Creates and executes web requests.
    /// </summary>
    public class HttpAdapter : IHttpAdapter
    {
        /// <summary>
        /// Performs the get request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The server response.</returns>
        public string Get(string url)
        {
            string responseString = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var httpResponse = (HttpWebResponse)request.GetResponse();
            Stream resStream = httpResponse.GetResponseStream();
            var reader = new StreamReader(resStream);
            responseString = reader.ReadToEnd();
            resStream.Close();
            reader.Close();

            return responseString;
        }

        /// <summary>
        /// Creates the post request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="postData">The post data.</param>
        /// <returns>The response from the server.</returns>
        public string Post(string url, string postData)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }
}