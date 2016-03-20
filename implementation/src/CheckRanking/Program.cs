using CheckRanking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WelfareSearchingTool
{
    class Program
    {
        static int Main(string[] args)
        {
            ResultCodeType resultCode = 0;
            if (args.Length > 0)
            {
                switch (args[0].ToLower())
                {
                    case "randcode":
                        var gongzu = new GongzuProcess();
                        var t = gongzu.GenerationRandCodeAsync();
                        t.Wait();

                        var cookies = GetAllCookies(gongzu.Handler.CookieContainer);
                        SSystem.Serializers.SerializeObjectHelper.Save(cookies, "cookies");

                        SSystem.Serializers.SerializeObjectHelper.Save(gongzu.Cache, "cache",SSystem.Serializers.SerializeType.Compressed);
                        resultCode = gongzu.ResultCode;
                        break;
                    case "check":
                        List<Cookie> cookies2 = SSystem.Serializers.SerializeObjectHelper.Load<List<Cookie>>("cookies");
                        var gongzu2 = new GongzuProcess();
                        gongzu2.RandCode = args[3];
                        gongzu2.UserName = args[1];
                        gongzu2.Password = args[2];
                        gongzu2.Cache = SSystem.Serializers.SerializeObjectHelper.Load<Dictionary<string,string>>("cache",SSystem.Serializers.SerializeType.Compressed);
                        foreach (var c in cookies2)
                        {
                            gongzu2.Handler.CookieContainer.Add(c);
                        }
                        try
                        {
                            var resut = gongzu2.Start();
                            if (!string.IsNullOrEmpty(gongzu2.Error))
                            {
                                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "exception.log"), gongzu2.Error);
                            }
                            else
                            {
                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(resut);
                                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "message.json"), json);
                            }
                        }
                        catch (Exception ex)
                        {
                            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "exception.log"), ex.Message + Environment.NewLine + ex.StackTrace);
                        }
                        resultCode = gongzu2.ResultCode;
                        Reset();
                        break;
                }

            }
            return (int)resultCode;
        }

        private static void Reset()
        {
            File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rand.jpg"));
        }

        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();

            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }
            return lstCookies;
        }
    }
}
