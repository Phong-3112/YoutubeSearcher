# Youtube Search

    �E����: C#
    �E�R�}���h���C����1�̈������w�肵�ċN������
    �E�����Ŏw�肳�ꂽ�L�[���[�h���g����YouTube�̓�����������A���X�g�ŏo�͂���
    �E�o�͂��铮��̍���: �^�C�g���A���e�ҁi�`���l�����j�AURL
    �EYouTube�̓��挟���ɂ�YouTube Data API�𗘗p����B�uhttps://developers.google.com/youtube/v3/docs/�v

## Usage

    exe�t�H���_���_�E���[�h���A�uSetting.ini�v��API�L�[�������Ă���v���O�����𗘗p���Ă��������B

    �Eini�t�@�C���iSetting.ini�j
    [Setting]
    - Url               : �ڑ���URL
    - Output            : �o�̓t�@�C����
    - Log               : ���O�t�@�C����
    - OutputDir         : �o�̓t�@�C�� �o�͐�f�B���N�g��
    - LogDir            : ���O�t�@�C�� �o�͐�f�B���N�g��

    [Para](https://developers.google.com/youtube/v3/docs/search/list#�p�����[�^)
    - Part   
    - maxResults        
    - type              
    - q                 
    - key               

    �ECLI Command: YoutubeSearch.exe [search_keyword]

## Example Result

```bash
>.\YoutubeSearch.exe dota

URL: https://www.googleapis.com/youtube/v3/search
���M�f�[�^: part=snippet&maxResults=10&type=video&q=dota&key=[your_API_key]
--------------------------------------------------------------------------------------------
�^�C�g�� : Dota 2 The International 2021 - Main Event Day 2
���e�ҁFdota2
URL�Fhttps://youtube.com/watch?v=pQHtrT3rRjQ
--------------------------------------------------------------------------------------------
�^�C�g�� : Dota 2 The International 10 - Main Event Day 1
���e�ҁFdota2
URL�Fhttps://youtube.com/watch?v=ZEhQ3uELLt8
--------------------------------------------------------------------------------------------
�^�C�g�� : [ES] Dota 2 The International 2021 - Main Event Day 2
���e�ҁFdota2
URL�Fhttps://youtube.com/watch?v=G5XESekRzBU
--------------------------------------------------------------------------------------------
�^�C�g�� : ? T1 VS PSG.LGD LIVE - THE INTERNATIONAL 2021 - TI10 DOTA 2
���e�ҁFKardel
URL�Fhttps://youtube.com/watch?v=a7t_oBG8Usg
--------------------------------------------------------------------------------------------
�^�C�g�� : [CN] Dota 2 The International 2021 - Main Event Day 2
���e�ҁFdota2
URL�Fhttps://youtube.com/watch?v=XOYfS5JluxA
--------------------------------------------------------------------------------------------
�^�C�g�� : [RU] Dota 2 The International 2021 - Main Event Day 2
���e�ҁFdota2
URL�Fhttps://youtube.com/watch?v=sPEKbJ8rS3E
--------------------------------------------------------------------------------------------
�^�C�g�� : ??????? PSG.LGD vs T1 (BO3) TI10 Main Event Day 2 Lakoi Dota 2
���e�ҁFLakoi DotA2
URL�Fhttps://youtube.com/watch?v=1hz4MgwxK-4
--------------------------------------------------------------------------------------------
�^�C�g�� : FNATIC vs UNDYING - TI10 SEA vs NA ELIMINATION - The International 2021 Dota 2 Highlights
���e�ҁFDotA Digest
URL�Fhttps://youtube.com/watch?v=rk0PPMwMnaQ
--------------------------------------------------------------------------------------------
�^�C�g�� : T1 vs PSG.LGD | The International 10 Dota 2 | Upper Bracket R1 BO3 | Caster VEENOMON ft. Oddie
���e�ҁFWxC Indonesia
URL�Fhttps://youtube.com/watch?v=YPwbaXmXhAw
--------------------------------------------------------------------------------------------
�^�C�g�� : FNATIC vs UNDYING + ASTER vs QUINCY CREW - TI10 PLAYOFFS ELIMINATION - THE INTERNATIONAL 10 DOTA 2
���e�ҁFNoobFromUA
URL�Fhttps://youtube.com/watch?v=CpfxgifyfHA
--------------------------------------------------------------------------------------------
```
