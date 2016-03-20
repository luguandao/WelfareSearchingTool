using CheckRanking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WelfareSearchingTool
{
    internal class GongzuProcess : IDisposable
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RandCode { get; set; }
        public Dictionary<string, string> Cache;
        public ResultCodeType ResultCode { get; set; }

        public string Error { get; private set; }

        public GongzuProcess()
        {
            Cache = new Dictionary<string, string>();
            Client = InitClient();
        }

        public HttpClientHandler Handler { get; set; }
        public HttpClient Client { get; set; }

        public Message Start()
        {
            try
            {
                var t = StartAsync();
                t.Wait();
                return t.Result;
            }
            catch (AggregateException ex)
            {
                Error = ex.InnerException.Message + Environment.NewLine + ex.InnerException.StackTrace;

            }
            return null;
        }

        public async Task GenerationRandCodeAsync()
        {
            await LoginAsync();
            await GetRandCodeAsync();
        }

        private async Task<Message> StartAsync()
        {
            await PostLogin();
            await VisitPlatform();
            var result = await VisitRankingPage();
            await Logout();
            return result;
        }

        private HttpClient InitClient()
        {
            Handler = new HttpClientHandler();
            HttpClient client = new HttpClient(Handler);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; Touch; rv:11.0) like Gecko");

            return client;
        }
        private async Task LoginAsync()
        {

            var response = await Client.GetAsync("https://wwsso.szjs.gov.cn:8084/wwsso/login");

            CheckTrue(response.IsSuccessStatusCode, "无法打开登陆页");


            string content = await response.Content.ReadAsStringAsync();
            if (!content.Contains("统一身份认证平台"))
                throw new WebException("无法成功加载登录页");

            Cache.Add("Login", content);
        }

        private async Task GetRandCodeAsync()
        {
            var response = await Client.GetAsync("https://wwsso.szjs.gov.cn:8084/wwssoManager/commons/genImgCode.action?imgCodeType=imgCodeLogin");
            CheckTrue(response.IsSuccessStatusCode, "无法取得验证码图片");
            byte[] image = await response.Content.ReadAsByteArrayAsync();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rand.jpg");
            File.WriteAllBytes(path, image);
        }

        private async Task PostLogin()
        {
            var response = await Client.PostAsync("https://wwsso.szjs.gov.cn:8084/wwssoManager/commons/verifyImgCode.action?imgCodeType=imgCodeLogin ",
                new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>("imgCode",RandCode)
            }));
            CheckTrue(response.IsSuccessStatusCode, "验证码提交失败");
            string content = await response.Content.ReadAsStringAsync();
            if (content.ToLower() != "true")
            {
                ResultCode = ResultCodeType.ValidateCodeFailed;
                throw new WebException("验证码错误");
            }

            Client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, image/jxr, */*");
            Client.DefaultRequestHeaders.Add("Referer", "https://wwsso.szjs.gov.cn:8084/wwsso/login");
            Client.DefaultRequestHeaders.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.8,en-US;q=0.5,en;q=0.3");
            Client.DefaultRequestHeaders.Add("DNT", "1");

            response = await Client.PostAsync("https://wwsso.szjs.gov.cn:8084/wwsso/login", new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("sessionId",GetValueByRegex(@"sessionId.+?value=""(?<Value>.+?)""",Cache["Login"])),
                new KeyValuePair<string,string>("containerName",""),
                new KeyValuePair<string,string>("ie8Temp",""),
                new KeyValuePair<string,string>("certBase64",""),
                new KeyValuePair<string,string>("info",GetValueByRegex(@"var\s+info\s*=\s*'(?<Value>.+?)'",Cache["Login"])),
                new KeyValuePair<string,string>("signInfo",""),
                new KeyValuePair<string,string>("lt",GetValueByRegex(@"lt""\s+value=""(?<Value>.+?)""",Cache["Login"])),
                new KeyValuePair<string,string>("execution",GetValueByRegex(@"execution""\s+value=""(?<Value>.+?)""",Cache["Login"])),
                new KeyValuePair<string,string>("_eventId","submit"),
                new KeyValuePair<string,string>("loginMode","1"),
                new KeyValuePair<string,string>("userCode",UserName),
                new KeyValuePair<string,string>("password",SHA1(Password).ToLower()),
                new KeyValuePair<string,string>("passwordMd5",CreateMd5(Password).ToLower()),
                new KeyValuePair<string,string>("imgCodeType","imgCodeLogin"),
                new KeyValuePair<string,string>("imgCode",RandCode)
            }));

            CheckTrue(response.IsSuccessStatusCode, "用户登录失败");
            content = await response.Content.ReadAsStringAsync();
            if (!content.Contains("统一认证平台"))
            {
                ResultCode = ResultCodeType.UserNameOrPasswordFailed;
                throw new WebException("用户登录异常");
            }
        }

        private async Task VisitPlatform()
        {
            var response = await Client.GetAsync("http://bzflh.szjs.gov.cn/");
            CheckTrue(response.IsSuccessStatusCode, "访问平台失败");
            string content = await response.Content.ReadAsStringAsync();
            if (!content.Contains("This is my JSP page."))
            {
                ResultCode = ResultCodeType.PageLoadFailed;
                throw new WebException("无法访问平台");
            }

            var m = Regex.Match(content, @"location\.replace\(location\.protocol\+""//""\+location\.host\+""/(?<Value>\S+?)""");
            if (!m.Success)
            {
                throw new WebException("没有找到后缀");
            }
            string tag = m.Groups["Value"].Value;

            response = await Client.GetAsync("http://bzflh.szjs.gov.cn/" + tag);
            CheckTrue(response.IsSuccessStatusCode, "无法成功访问登陆平台");
            content = await response.Content.ReadAsStringAsync();
            if (!content.Contains("深圳市保障性住房轮候申请"))
            {
                ResultCode = ResultCodeType.PageLoadFailed;
                throw new WebException("无法成功访问登陆平台2");
            }

        }

        private async Task<Message> VisitRankingPage()
        {
            Client.DefaultRequestHeaders.Remove("Accept");
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");

            Client.DefaultRequestHeaders.Remove("Referer");
            Client.DefaultRequestHeaders.Add("Referer", "http://bzflh.szjs.gov.cn/TylhW/user.do?method=login");
            Client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            var response = await Client.PostAsync("http://bzflh.szjs.gov.cn/TylhW/GenerateTokenServlet",
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()));

            CheckTrue(response.IsSuccessStatusCode, "获取token失败");
            string token = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(token) || token.Length > 40)
            {
                throw new WebException("获取的token长度不正确，length=" + token.Length);
            }
            response = await Client.GetAsync(string.Format("http://bzflh.szjs.gov.cn/TylhW/rentAction.do?method=toRzInfo&csrftoken={0}", token));
            CheckTrue(response.IsSuccessStatusCode, "访问排名页面出错");
            string content = await response.Content.ReadAsStringAsync();
            if (!content.Contains("排位信息"))
            {
                ResultCode = ResultCodeType.PageLoadFailed;
                throw new WebException("无法打开排名页面");
            }

            Regex reg = new Regex(@"id=""NAME""[\s\S]+?value=""(?<Name>\S+)?""[\s\S]+?id=""IDEN18""[\s\S]+?value=""(?<Id>\d+)""[\s\S]+?id=""SORT_NAME""[\s\S]*?value=""第(?<RankingNum>\d+)名""[\s\S]+?id=""receipt""[\s\S]*?value=""(?<RecvNum>\S+)?""[\s\S]+?id=""SORT_NAME""[\s\S]*?value=""(?<RendNum>\d+)""[\s\S]+?可配租面积[\s\S]+?value=""(?<Area>[\s\S]+?)""", RegexOptions.Compiled);
            var m = reg.Match(content);
            if (m.Success)
            {
                return new Message
                {
                    Name = m.Groups["Name"].Value,
                    Id = m.Groups["Id"].Value,
                    RankingNum = int.Parse(m.Groups["RankingNum"].Value),
                    RendNum = int.Parse(m.Groups["RendNum"].Value),
                    Area = m.Groups["Area"].Value,
                    RecvNum = m.Groups["RecvNum"].Value
                };
            }
            ResultCode = ResultCodeType.CannotCatchingMessage;
            throw new WebException("无法成功读取到排名信息");
        }

        private async Task Logout()
        {
            var response = await Client.GetAsync("http://bzflh.szjs.gov.cn/TylhW/logout.jsp");
            CheckTrue(response.IsSuccessStatusCode, "无法成功加载退出登录");
            string content = await response.Content.ReadAsStringAsync();
            if (!content.Contains("top.location.href"))
            {
                throw new WebException("无法加载退出页面");
            }

            var m = Regex.Match(content, @"top\.location\.href='(?<Value>.+?)'");
            if (!m.Success)
            {
                throw new WebException("无法找到退出链接");
            }
            string url = m.Groups["Value"].Value;
            Client.DefaultRequestHeaders.Remove("Referer");
            Client.DefaultRequestHeaders.Add("Referer", "http://bzflh.szjs.gov.cn/TylhW/logout.jsp");
            response = await Client.GetAsync(url);
            content = await response.Content.ReadAsStringAsync();
            if (!content.Contains("用户名"))
            {
                throw new WebException("用户退出登录失败");
            }
        }

        public static string CreateMd5(string text)
        {
            var md = MD5.Create();
            var arr = md.ComputeHash(Encoding.UTF8.GetBytes(text));

            StringBuilder sb = new StringBuilder();
            foreach (var b in arr)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        private static string SHA1(string text)
        {
            byte[] cleanBytes = Encoding.Default.GetBytes(text);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        private string GetValueByRegex(string regex, string text)
        {
            var m = Regex.Match(text, regex);
            if (m.Success)
            {
                return m.Groups["Value"].Value;
            }
            return string.Empty;
        }

        private void CheckTrue(bool t, string error)
        {
            if (!t)
            {
                throw new WebException(error);
            }
        }

        public void Dispose()
        {
            if (Client != null)
            {
                Client.Dispose();
            }
            if (Handler != null)
            {
                Handler.Dispose();
            }
        }
    }

    public class Message
    {
        public string Name { get; set; }
        public int RankingNum { get; set; }
        public string Id { get; set; }
        public string RecvNum { get; set; }
        public int RendNum { get; set; }
        public string Area { get; set; }
    }
}
