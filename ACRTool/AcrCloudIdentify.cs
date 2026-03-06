/*
 * Created by SharpDevelop.
 * User: ADMINN
 * Date: 05-Mar-26
 * Time: 10:29 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ACRTool
{
	/// <summary>
	/// Description of AcrCloudIdentify.
	/// </summary>
	public class AcrCloudIdentify
	{
		// Configuration
        private const string RequestUrlPath = "/v1/identify";
        private const string SignatureVersion = "1";
        private const string HttpMethod = "POST";
        private const string DataType = "audio";

        private readonly int _timeoutMs;
        
        public string _host = GlobalVariables.host;//?GlobalVariables.ini.Read("ACR","host"):"identify-ap-southeast-1.acrcloud.com";
        public string _accessKey = GlobalVariables.accesskey;//?GlobalVariables.ini.Read("ACR","host"):"605b3cdbc04ec4de2cb6954306a5eb3a";
        public string _accessSecret = GlobalVariables.accesssecret;//?GlobalVariables.ini.Read("ACR","host"):"a76NRXgvInghU3vt8y4WHeQwWVaCnCADPVYt5foc";
        private string orgF;
        public AcrCloudIdentify(string fileDir, int timeoutMs = 10000)
        {
            _timeoutMs = timeoutMs;
            orgF = fileDir;
        }
        public void ProcessDirectory(string directoryPath, string searchPattern = "*.*")
        {
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Error: Directory not found: " + directoryPath);
                return;
            }

            string[] files = Directory.GetFiles(directoryPath, searchPattern);

            if (files.Length == 0)
            {
                Console.WriteLine("No files found in the directory.");
                return;
            }

            Console.WriteLine("Found " + files.Length + " files in " + directoryPath + "...");

            foreach (string filePath in files)
            {
                Console.WriteLine("\nProcessing: " + Path.GetFileName(filePath));
                try
                {
                    byte[] fileData = File.ReadAllBytes(filePath);
                    string result = Recognize(_host, _accessKey, _accessSecret, fileData, "audio", _timeoutMs);
                    Console.WriteLine("Result: " + result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error processing file: " + ex.Message);
                }
            }
        }
        public string Scan(){
			try {
				byte[] fileData = File.ReadAllBytes(orgF);
				return Recognize(_host, _accessKey, _accessSecret, fileData, "audio", _timeoutMs);
			} catch (Exception e) {
        		return "{\"status\":{ \"msg\": \"Failed - "+e.ToString()+"\", \"code\": 0, \"version\": \"1.0\" }}";
				//{ "msg": "Success", "code": 0, "version": "1.0" }
			}
        }

        public string Recognize(string host, string accessKey, string secretKey, byte[] queryData, string queryType, int timeout)
        {
			const string method = "POST";
			const string httpUrlPath = "/v1/identify";
			const string sigVersion = "1";
            string timestamp = GetUtcTimeSeconds();

            string reqURL = "http://" + host + httpUrlPath;

            string sigStr = method + "\n" + httpUrlPath + "\n" + accessKey + "\n" + queryType + "\n" + sigVersion + "\n" + timestamp;
            string signature = EncryptByHMACSHA1(sigStr, secretKey);

            var postParams = new Dictionary<string, object>();
            postParams.Add("access_key", accessKey);
            postParams.Add("sample_bytes", queryData.Length.ToString());
            postParams.Add("sample", queryData); // The file bytes
            postParams.Add("timestamp", timestamp);
            postParams.Add("signature", signature);
            postParams.Add("data_type", queryType);
            postParams.Add("signature_version", sigVersion);

            return PostHttp(reqURL, postParams, timeout);
        }

        private string PostHttp(string url, Dictionary<string, object> parameters, int timeout)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Timeout = timeout;
            request.Credentials = CredentialCache.DefaultCredentials;

            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
					const string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

                    foreach (KeyValuePair<string, object> param in parameters)
                    {
                        requestStream.Write(boundarybytes, 0, boundarybytes.Length);
                        
                        if (param.Value is byte[])
                        {
                            // It's the file
							const string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"sample\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                            string header = string.Format(headerTemplate, param.Key);
                            byte[] headerbytes = Encoding.UTF8.GetBytes(header);
                            requestStream.Write(headerbytes, 0, headerbytes.Length);

                            byte[] fileData = (byte[])param.Value;
                            requestStream.Write(fileData, 0, fileData.Length);
                        }
                        else
                        {
                            // It's a string parameter
                            string formitem = string.Format(formdataTemplate, param.Key, param.Value);
                            byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                            requestStream.Write(formitembytes, 0, formitembytes.Length);
                        }
                    }
                    requestStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (Stream errorStream = wex.Response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(errorStream))
                    {
                        return "HTTP Error: " + reader.ReadToEnd();
                    }
                }
                return "Error: " + wex.Message;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


        private string EncryptByHMACSHA1(string data, string key)
        {
            using (var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hashBytes);
            }
        }

        private string GetUtcTimeSeconds()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        }
	}
}