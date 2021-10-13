using System;
using System.IO;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace YoutubeSearch
{
    public class SettingInfo
    {
        /// <summary>
        /// [Setting] 基本設定
        /// </summary>
        public string Url { get; set; }         // URL
        public string Output { get; set; }      // 出力ファイル名
        public string Log { get; set; }         // ログファイル名
        public string OutputDir { get; set; }   // 出力ファイルディレクトリ
        public string LogDir { get; set; }      // ログファイルディレクトリ

        /// <summary>
        /// [Para] パラメータ設定
        /// </summary>
        public string part { get; set; }        // Part
        public int maxResults { get; set; }     // 出力件数制限
        public string type { get; set; }        // 検索対象（画像：video、プレイリスト：playlist、チャネル：channel）
        public string q { get; set; }           // 検索キーワード
        public string key { get; set; }         //　APIキー

        public Dictionary<string, string> ParamDict = new Dictionary<string, string> { };
        public string OutputFilePath { get; set; }  // 出力ファイル絶対パス

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SettingInfo()
        {
            var iniFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                            Program.INI_FILE_NAME);
            var ini = new Ini(iniFileName);
            var sectionName1 = "Setting";
            var sectionName2 = "Para";

            if (!File.Exists(iniFileName))
            {
                // iniファイルがなければリソースから読み込みして作成
                using (var sw = new StreamWriter(iniFileName, false, System.Text.Encoding.GetEncoding("utf-16")))
                {
                    sw.Write(Properties.Resources.DefaultSetting);
                    Ini.FilePath = iniFileName;
                }
            }
            // [Setting]
            Url = Ini.GetValueString(sectionName1, GetNm(() => Url));
            Output = Ini.GetValueString(sectionName1, GetNm(() => Output));
            Log = Ini.GetValueString(sectionName1, GetNm(() => Log));
            OutputDir = Ini.GetValueString(sectionName1, GetNm(() => OutputDir));
            LogDir = Ini.GetValueString(sectionName1, GetNm(() => LogDir));

            // [Para]
            part = Ini.GetValueString(sectionName2, GetNm(() => part));
            maxResults = Ini.GetValueInt(sectionName2, GetNm(() => maxResults));
            type = Ini.GetValueString(sectionName2, GetNm(() => type));
            q = Ini.GetValueString(sectionName2, GetNm(() => q));
            key = Ini.GetValueString(sectionName2, GetNm(() => key));

            ParamDict.Add("part", part);
            ParamDict.Add("maxResults", maxResults.ToString());
            ParamDict.Add("type", type);
            ParamDict.Add("q", q);
            ParamDict.Add("key", key);

            // 未設定？
            if (Output == "")
            {
                Output = Program.OUTPUT_FILE_NAME;
            }
            if (OutputDir == "")
            {
                OutputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
            }
            if (Log == "")
            {
                Log = Program.LOG_FILE_NAME;
            }
            if (LogDir == "")
            {
                LogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            }
        }

        /// <summary>
        /// プロパティ名取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        private static string GetNm<T>(Expression<Func<T>> e)
        {
            var member = (MemberExpression)e.Body;
            return member.Member.Name;
        }
    }
}
