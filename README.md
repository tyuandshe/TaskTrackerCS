# TaskTrackerCS

绾?C# WinForms 浠诲姟杩借釜鍣紝鍗曟枃浠剁紪璇戯紝鏃犱换浣曠涓夋柟渚濊禆銆?
## 鍔熻兘鐗规€?
- **娣辫壊涓婚**锛氱幇浠ｅ寲鏆楄壊 UI锛屾姢鐪奸厤鑹?- **浼樺厛绾у垎绫?*锛氶珮 / 涓?/ 浣?涓夋。锛岄鑹插尯鍒?- **鎴鏃堕棿**锛氭敮鎸佽缃埅姝㈡椂闂达紝瀹炴椂鍊掕鏃舵樉绀?- **鎵樼洏杩愯**锛氭渶灏忓寲鍒扮郴缁熸墭鐩橈紝涓嶅崰鐢ㄤ换鍔℃爮
- **瓒呮椂鎻愰啋**锛氳窛鎴 1 鍒嗛挓鏃剁獥鍙ｉ棯鐑佹彁绀?- **鏁版嵁鎸佷箙鍖?*锛欼NI 鏂囦欢瀛樺偍锛坄save.ini`锛夛紝鍦?EXE 鍚岀洰褰曚笅
- **闆朵緷璧?*锛氫粎浣跨敤 .NET Framework 鍐呯疆搴擄紙System.Windows.Forms.dll, System.Drawing.dll锛?
## 缂栬瘧

```batch
C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /nologo /codepage:65001 /target:winexe /out:TaskTrackerCS.exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll TaskTrackerCS.cs
```

鎴栦娇鐢ㄥ凡缂栬瘧鐨?`TaskTrackerCS.exe`锛堣 Releases锛夈€?
## 浣跨敤璇存槑

1. 杩愯 `TaskTrackerCS.exe`
2. 棣栨杩愯浼氬湪鍚岀洰褰曠敓鎴?`save.ini` 淇濆瓨浠诲姟鏁版嵁
3. 杈撳叆浠诲姟鍚嶇О 鈫?鍥炶溅鎴栫偣鍑汇€屾坊鍔犱换鍔°€?4. 鍙€夛細璁剧疆鎴鏃堕棿鍜屼紭鍏堢骇
5. 鐐瑰嚮銆屾渶灏忓寲銆嶆寜閽垨鏍囬鏍忓彸涓婅 `鈥昤 闅愯棌鍒版墭鐩?6. 宸﹂敭鐐瑰嚮鎵樼洏鍥炬爣鎭㈠绐楀彛

## 鏁版嵁鏂囦欢

| 鏂囦欢 | 璇存槑 |
|------|------|
| `TaskTrackerCS.exe` | 涓荤▼搴?|
| `save.ini` | 浠诲姟鏁版嵁锛堣嚜鍔ㄧ敓鎴愶紝鍕挎墜鍔ㄧ紪杈戯級 |
| `debug.log` | 璋冭瘯鏃ュ織锛堝彲閫夛級 |

> 鈿狅笍 `save.ini` 鍖呭惈涓汉浠诲姟鏁版嵁锛屽厠闅嗕粨搴撳悗璇峰嬁鎻愪氦姝ゆ枃浠躲€?
## 鎶€鏈粏鑺?
- .NET Framework 4.0 + C# WinForms
- Win32 API锛圥/Invoke锛夛細`user32.dll` 绐楀彛缃《銆侀棯鐑?- 甯冨眬锛氭樉寮?`SetBounds()` 缁濆瀹氫綅锛堥潪 Dock/Anchor锛?- 瀛樺偍锛欼NI 绾枃鏈紝鏃?System.Web.Extensions 渚濊禆

## License

MIT
