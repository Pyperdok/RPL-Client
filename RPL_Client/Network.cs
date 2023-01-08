using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace RPL_Client
{
    static public class Network
    {
    static public string Cookie;
        /*Отправка запроса на сервер. Возвращает Dictionary<string, string> с заголовками и телом запроса
        В случае ошибки запроса возвращает Dictionary<string, string> со статусом кода ошибки*/
        static public Dictionary<string, string> http(string url, string method = "GET", string body = "")
        {
            WebRequest request = WebRequest.Create($"{url}");

            request.ContentType = "application/json";
            request.Headers.Add("Cookie", Cookie);
            request.Method = $"{method}";
           // request.Headers["Cookie"] = RPLWindow.cook

            if (method == "POST")
            {
                byte[] data = Encoding.ASCII.GetBytes(body);

                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();

                Console.WriteLine(responseFromServer);

                reader.Close();
                dataStream.Close();
                response.Close();
                //Console.WriteLine(response.Headers["Cookie"]);

                Dictionary<string, string> result = new Dictionary<string, string>();
                string[] HeadersNames = response.Headers.AllKeys;

                for (int i = 0; i < HeadersNames.Length; i++)
                {
                    result.Add(HeadersNames[i], response.Headers[HeadersNames[i]]);
                }

                string StatusResponse = response.StatusCode.ToString();


                result.Add("StatusCode", StatusResponse);


                result.Add("Data", responseFromServer);

                Console.WriteLine(result);

                return result;
            }
            catch (WebException ex)
            {
                string StatusResponse = ((HttpWebResponse)ex.Response).StatusCode.ToString();
                Console.WriteLine(StatusResponse);
                Dictionary<string, string> result = new Dictionary<string, string>() { { "StatusCode", StatusResponse } };

                return result;
            }
        }
    }
}
