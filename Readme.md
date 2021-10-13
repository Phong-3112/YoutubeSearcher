# Youtube Search

    ・言語: C#
    ・コマンドラインで1つの引数を指定して起動する
    ・引数で指定されたキーワードを使ってYouTubeの動画を検索し、リストで出力する
    ・出力する動画の項目: タイトル、投稿者（チャネル名）、URL
    ・YouTubeの動画検索にはYouTube Data APIを利用する。「https://developers.google.com/youtube/v3/docs/」

## Usage

「Setting.ini」にAPIキーを代入してからプログラムを利用してください。

    ・iniファイル（Setting.ini）
    
    [Setting]
    - Url               : 接続先URL
    - Output            : 出力ファイル名
    - Log               : ログファイル名
    - OutputDir         : 出力ファイル 出力先ディレクトリ
    - LogDir            : ログファイル 出力先ディレクトリ

    [Para](https://developers.google.com/youtube/v3/docs/search/list#パラメータ)
    - Part   
    - maxResults        
    - type              
    - q                 
    - key               

    ・CLI Command: YoutubeSearch.exe [search_keyword]
