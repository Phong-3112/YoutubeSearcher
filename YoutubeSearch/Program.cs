using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace YoutubeSearch
{
    static class Program
    {
        public const string LOG_FILE_NAME = "Log.txt";
        public const string OUTPUT_FILE_NAME = "output.txt";
        public const string INI_FILE_NAME = "Setting.ini";

        public static SettingInfo setting = new SettingInfo();
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            logger.Factory.Configuration.Variables.Add("filename", setting.Log);
            logger.Factory.Configuration.Variables.Add("savepath", setting.LogDir);
            logger.Factory.ReconfigExistingLoggers();


            // コマンドライン引数の値を変数に格納
            if (args != null && args.Length != 0)
            {
                setting.ParamDict["q"] = args[0];
            }
            else
            {
                Console.WriteLine("引数①：検索キーワードを指定なしです。プログラム終了。");
                logger.Warn("引数①：検索キーワードを指定なしです。プログラム終了。");
                Environment.Exit(1);
            }

            ExecSearchAsync().Wait();

        }

        /// <summary>
        /// 設定チェック
        /// </summary>
        /// <returns></returns>
        private static bool CheckSetting()
        {
            if (string.IsNullOrEmpty(setting.Url))
            {
                logger.Warn("URLが指定されていません。");
                return false;
            }
            else if (!Uri.IsWellFormedUriString(setting.Url, UriKind.Absolute))
            {
                logger.Warn("URLの形式が正しくありません。  " + setting.Url);
                return false;
            }

            if(string.IsNullOrEmpty(setting.key))
            {
                logger.Warn("APIキーが指定されていません。Setting.iniファイルのkey値を指定してください。");
                return false;
            }

            if(string.IsNullOrEmpty(setting.part))
            {
                logger.Warn("Partが指定されていません。Setting.iniファイルのpart値を指定してください。");
                return false;
            }

            if (string.IsNullOrEmpty(setting.ParamDict["q"]))
            {
                logger.Warn("検索キーワードが指定されていません。");
                return false;
            }

            var invalidName = Path.GetInvalidFileNameChars();
            if (setting.Output.IndexOfAny(invalidName) >= 0)
            {
                logger.Warn("出力ファイル名に使用できない文字が含まれています。  " + setting.Output);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Search処理
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> ExecSearchAsync()
        {
            // 出力ファイル名が未指定？
            if (string.IsNullOrEmpty(setting.Output))
            {
                setting.Output = "output.txt";
            }
            // 出力先ディレクトリが未指定？
            if (string.IsNullOrEmpty(setting.OutputDir))
            {
                setting.OutputDir = AppDomain.CurrentDomain.BaseDirectory;
            }

            
            var outputFilePath = Path.Combine(setting.OutputDir, setting.Output);

            var search = new Search()
            {
                Url = setting.Url
            };

            // 入力チェック
            if (!CheckSetting())
            {
                return false;
            }

            try
            {
                // POST
                var jsonString = await search.PostAsyncString();
                
                // jsonStringの処理
                var fileString = String.Empty;
                var docs = JsonDocument.Parse(jsonString);
                var rootElement = docs.RootElement;
                var items = rootElement.GetProperty("items");
                foreach (var item in items.EnumerateArray())
                {
                    var videoId = item.GetProperty("id").GetProperty("videoId").GetString();
                    var snippet = item.GetProperty("snippet");
                    var Title = snippet.GetProperty("title").GetString();
                    var channelTitle = snippet.GetProperty("channelTitle").GetString();
                    fileString += $"タイトル : {Title}\n";
                    fileString += $"投稿者：{channelTitle}\n";
                    fileString += $"URL：https://youtube.com/watch?v={videoId}\n";
                    fileString += "--------------------------------------------------------------------------------------------\n";
                }

                if (search.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    // HTTPステータスコードが不正
                    logger.Error($"ErrNo = {(int)search.StatusCode} Msg = {search.StatusCode}");
                    return false;
                }
                else
                {
                    // 出力先ディレクトリが存在しなければ新規作成
                    var outputDirNm = Path.GetDirectoryName(outputFilePath);
                    if (Directory.Exists(outputDirNm) == false)
                    {
                        Directory.CreateDirectory(outputDirNm);
                    }

                    // ファイル出力
                    using (var sw = new StreamWriter(outputFilePath, false, search.encoding))
                    {
                        { 
                            sw.Write(fileString);
                        }
                    }
                }
                Console.WriteLine(fileString);
                logger.Info("正常に終了しました。");
                return true;
            }
            catch (OperationCanceledException)
            {
                logger.Error("処理を中断しました。");
                return false;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return false;
            }
        }
    }
}
