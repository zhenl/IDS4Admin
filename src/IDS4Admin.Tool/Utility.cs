using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDS4Admin.Tool
{
    internal class Utility
    {
        public static string CreateConfigureFiles(IDS4Config config)
        {
            if (!Directory.Exists("Output")) Directory.CreateDirectory("Output");
            var path = "Output";
            var templatepath = "templates";
            var folders = Directory.GetDirectories(templatepath);
            foreach(var folder in folders)
            {
                var files=Directory.GetFiles(folder);
                foreach(var file in files)
                {
                    var content=File.ReadAllText(file);
                    var result = TreateToken(content, config);
                    var outpath="";
                    if (folder.Contains("admin")) outpath = Path.Combine(path, "admin");
                    else outpath = Path.Combine(path, "sts");
                    if(!Directory.Exists(outpath)) Directory.CreateDirectory(outpath);
                    var filename = Path.GetFileName(file).Replace("template","json");
                    outpath= Path.Combine(outpath, filename);
                    File.WriteAllText(outpath, result);
                }
            }
            return "Configure files are created. You can find them on Output folder.";
        }

        private static string TreateToken(string content, IDS4Config config)
        {
            var res = content;
            if (!string.IsNullOrEmpty(content))
            {
                res = res.Replace("[ADMINURI]", config.AdminUri);
                res = res.Replace("[STSURI]", config.STSUri);
                res = res.Replace("[DBTYPE]", config.DbType);
                res = res.Replace("[CONNECTIONSTRING]", config.DbConnString);
                res = res.Replace("[GETSTSURLS]", GetUrls(config.STSUri));
                res = res.Replace("[GETADMINURLS]", GetUrls(config.AdminUri));
                res = res.Replace("[GETSTSBAEPATH]", GetBasePath(config.STSUri));
                res = res.Replace("[GETADMINBAEPATH]", GetBasePath(config.AdminUri));
            }
            return res;
        }

        private static string GetBasePath(string sTSUri)
        {
            var res = "";
            if (!string.IsNullOrEmpty(sTSUri))
            {
                var arr= sTSUri.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if(arr.Length > 2) res= @", ""BasePath"":""/" + arr[2] +@"""";
            }
            return res;
        }

        private static string GetUrls(string sTSUri)
        {
            var res = "";
            if (!string.IsNullOrEmpty(sTSUri))
            {
                var arr = sTSUri.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length >= 2) res = @", ""urls"":""" + arr[0] + "//" + arr[1] +@"""";
            }
            return res;
        }
    }
}
