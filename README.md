# TaskTrackerCS
纯 C# WinForms 任务追踪器，单文件编译，无任何第三方依赖。

## 功能特性
- **深色主题**：现代化暗色 UI，护眼配色
- **优先级分类**：高 / 中 / 低 三档，颜色区分
- **截止时间**：支持设置截止时间，实时倒计时显示
- **托盘运行**：最小化到系统托盘，不占用任务栏
- **超时提醒**：距截止 1 分钟时窗口闪烁提示
- **数据持久化**：INI 文件存储（`save.ini`），在 EXE 同目录下
- **零依赖**：仅使用 .NET Framework 内置库（System.Windows.Forms.dll, System.Drawing.dll）

## 编译
```batch
C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /nologo /codepage:65001 /target:winexe /out:TaskTrackerCS.exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll TaskTrackerCS.cs
```
或使用已编译的 `TaskTrackerCS.exe`（见 Releases）。

## 使用说明
1. 运行 `TaskTrackerCS.exe`
2. 首次运行会在同目录生成 `save.ini` 保存任务数据
3. 输入任务名称 → 回车或点击「添加任务」
4. 可选：设置截止时间和优先级
5. 点击「最小化」按钮或标题栏右上角 `―` 隐藏到托盘
6. 左键点击托盘图标恢复窗口

## 数据文件
|    文件             |       说明                    |
|---------------------|-------------------------------|
| `TaskTrackerCS.exe` | 主程序                        |
| `save.ini`          | 任务数据（自动生成，勿手动编辑） |
| `debug.log`         | 调试日志（可选）               |

## 技术细节
- .NET Framework 4.0 + C# WinForms
- Win32 API（P/Invoke）：`user32.dll` 窗口置顶、闪烁
- 布局：显式 `SetBounds()` 绝对定位（非 Dock/Anchor）
- 存储：INI 纯文本，无 System.Web.Extensions 依赖

## License
MIT
