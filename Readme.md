# Youtube Search

    ・言語: C#
    ・コマンドラインで1つの引数を指定して起動する
    ・引数で指定されたキーワードを使ってYouTubeの動画を検索し、リストで出力する
    ・出力する動画の項目: タイトル、投稿者（チャネル名）、URL
    ・YouTubeの動画検索にはYouTube Data APIを利用する。「https://developers.google.com/youtube/v3/docs/」

## Usage

    exeフォルダをダウロードし、「Setting.ini」にAPIキーを代入してからプログラムを利用してください。

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

## Example Result

```bash
>.\YoutubeSearch.exe dota

URL: https://www.googleapis.com/youtube/v3/search
送信データ: part=snippet&maxResults=10&type=video&q=dota&key=[your_API_key]
--------------------------------------------------------------------------------------------
タイトル : Dota 2 The International 2021 - Main Event Day 2
投稿者：dota2
URL：https://youtube.com/watch?v=pQHtrT3rRjQ
--------------------------------------------------------------------------------------------
タイトル : Dota 2 The International 10 - Main Event Day 1
投稿者：dota2
URL：https://youtube.com/watch?v=ZEhQ3uELLt8
--------------------------------------------------------------------------------------------
タイトル : [ES] Dota 2 The International 2021 - Main Event Day 2
投稿者：dota2
URL：https://youtube.com/watch?v=G5XESekRzBU
--------------------------------------------------------------------------------------------
タイトル : ? T1 VS PSG.LGD LIVE - THE INTERNATIONAL 2021 - TI10 DOTA 2
投稿者：Kardel
URL：https://youtube.com/watch?v=a7t_oBG8Usg
--------------------------------------------------------------------------------------------
タイトル : [CN] Dota 2 The International 2021 - Main Event Day 2
投稿者：dota2
URL：https://youtube.com/watch?v=XOYfS5JluxA
--------------------------------------------------------------------------------------------
タイトル : [RU] Dota 2 The International 2021 - Main Event Day 2
投稿者：dota2
URL：https://youtube.com/watch?v=sPEKbJ8rS3E
--------------------------------------------------------------------------------------------
タイトル : ??????? PSG.LGD vs T1 (BO3) TI10 Main Event Day 2 Lakoi Dota 2
投稿者：Lakoi DotA2
URL：https://youtube.com/watch?v=1hz4MgwxK-4
--------------------------------------------------------------------------------------------
タイトル : FNATIC vs UNDYING - TI10 SEA vs NA ELIMINATION - The International 2021 Dota 2 Highlights
投稿者：DotA Digest
URL：https://youtube.com/watch?v=rk0PPMwMnaQ
--------------------------------------------------------------------------------------------
タイトル : T1 vs PSG.LGD | The International 10 Dota 2 | Upper Bracket R1 BO3 | Caster VEENOMON ft. Oddie
投稿者：WxC Indonesia
URL：https://youtube.com/watch?v=YPwbaXmXhAw
--------------------------------------------------------------------------------------------
タイトル : FNATIC vs UNDYING + ASTER vs QUINCY CREW - TI10 PLAYOFFS ELIMINATION - THE INTERNATIONAL 10 DOTA 2
投稿者：NoobFromUA
URL：https://youtube.com/watch?v=CpfxgifyfHA
--------------------------------------------------------------------------------------------
```
