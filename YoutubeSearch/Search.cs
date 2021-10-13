using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YoutubeSearch
{
    class Search
    {
        public string Url = "";     // 接続先URL
        public HttpStatusCode StatusCode = HttpStatusCode.NotFound;
        public Encoding encoding = Encoding.GetEncoding("shift-jis");
        private Dictionary<string, string> ParamDict = Program.setting.ParamDict;

        private HttpClient client = new HttpClient();
        private string ParamString
        {
            get
            {
                var str = "";
                foreach (var key in ParamDict.Keys)
                {
                    if (str != "")
                    {
                        str += "&";
                    }
                    str += $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(ParamDict[key])}";
                }
                return str;
            }
        }

        public Search()
        {
        }

        /// <summary>
        /// Search(POST)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> PostAsyncString()
        {
            Program.logger.Info($"URL: {Url}");
            Console.WriteLine($"URL: {Url}");
            Program.logger.Info($"送信データ: {ParamString}");
            Console.WriteLine($"送信データ: {ParamString}");
            Console.WriteLine("--------------------------------------------------------------------------------------------");

            var content = new FormUrlEncodedContent(ParamDict);
            content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
            content.Headers.Add("X-HTTP-Method-Override", "GET");

            using (var response = await client.PostAsync(Url, content).ConfigureAwait(false))
            {
                // 取得失敗なら例外
                response.EnsureSuccessStatusCode();
                StatusCode = response.StatusCode;


                // 文字エンコーディング取得
                encoding = await GetEncodingAsync(response);

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream, encoding, true) as TextReader)
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        /// <summary>
        /// 文字エンコーディング取得
        /// </summary>
        /// <param name="responce"></param>
        /// <returns></returns>
        private static async Task<Encoding> GetEncodingAsync(HttpResponseMessage responce)
        {
            // HTTPヘッダーから取得
            var charset1 = responce.Content.Headers.ContentType.CharSet;
            if (!string.IsNullOrEmpty(charset1))
            {
                try
                {
                    return Encoding.GetEncoding(charset1);
                }
                catch
                {
                }
            }

            string html = null;
            using (var ms = new MemoryStream())
            {
                await responce.Content.LoadIntoBufferAsync();
                await responce.Content.CopyToAsync(ms);
                ms.Position = 0;
                using (var tr = (new StreamReader(ms, Encoding.GetEncoding("shift-jis"), true)) as TextReader)
                {
                    html = await tr.ReadToEndAsync();
                }
            }

            // charset を探して取得
            var charset2 = new Regex(@"<[^>]*\bcharset\s*=\s*[""']?(?<charset>\w+)\b",
                                RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var match = charset2.Match(html);

            if (match.Success)
            {
                try
                {
                    return Encoding.GetEncoding(match.Groups["charset"].Value);
                }
                catch
                {
                }
            }

            return Encoding.GetEncoding("shift_jis");
        }
    }
}
