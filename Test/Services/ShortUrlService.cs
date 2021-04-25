using Dll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Services
{
    public static class ShortUrlService
    {
        /// <summary>
        ///数据库获取原URL
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        public static string GetLongUrl(string url)
        {
            string[] str = url.Split('/');
            url = str[str.Length - 1];
            using (var db = new ShortUrldbEntities1())
            {
                string sql = $"select * from t_ShortUrl where Code COLLATE Chinese_PRC_CS_AS = '{url}'";
                var code = db.Database.SqlQuery<t_ShortUrl>(sql).FirstOrDefault();
                if (code != null)
                    return code.Url;

            }
            return string.Empty;
        }
        /// <summary>
        /// 获取短链接并插入数据库
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string DataBaseShortUrl(string url)
        {
            using (var db = new ShortUrldbEntities1())
            {
                string sql = $"select * from t_ShortUrl where Url COLLATE Chinese_PRC_CS_AS = '{url}'";
                var turl = db.Database.SqlQuery<t_ShortUrl>(sql).FirstOrDefault();
                if (turl != null)
                {
                    return turl.Code;
                }
                else
                {
                    var shorUrlList =UntilHelper.GetShortUrl(url);
                    foreach (var item in shorUrlList)
                    {
                        sql = $"select * from t_ShortUrl where Code COLLATE Chinese_PRC_CS_AS = '{url}'";
                        var code = db.Database.SqlQuery<t_ShortUrl>(sql).FirstOrDefault();
                        if (code == null)
                        {
                            db.t_ShortUrl.Add(new t_ShortUrl
                            {
                                Code = item,
                                Url = url
                            });
                            db.SaveChanges();
                            return item;
                        }

                    }
                    return "短链接冲突";
                }

            }


        }
    }
}