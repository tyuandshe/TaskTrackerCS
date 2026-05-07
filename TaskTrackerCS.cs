// TaskTrackerCS.cs 鈥?绾?C# WinForms 浠诲姟杩借釜鍣紙.NET Framework 4.0 鍗曟枃浠剁紪璇戠増锛?
// 缂栬瘧鍛戒护锛?
//   C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /nologo /codepage:65001 /target:winexe /out:TaskTrackerCS.exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll TaskTrackerCS.cs
//
// 渚濊禆锛歋ystem.Windows.Forms.dll, System.Drawing.dll锛堝潎涓?.NET 鍐呯疆锛?
// 鏁版嵁锛歴ave.ini 淇濆瓨鍦?EXE 鍚岀洰褰曚笅

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

// 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
// Win32 API
// 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
static class WinApi
{
    // 鈹€鈹€ 绐楀彛鎷栧姩鐢?Form 鑷韩鐨?Location锛屼笉渚濊禆 WinApi 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);
}

// 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
// 鏁版嵁妯″瀷
// 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
public class TaskItem
{
    public string Id        { get; set; }
    public string Title     { get; set; }
    public string Deadline  { get; set; }
    public string Priority  { get; set; }
    public bool   Done      { get; set; }
    public double TotalMs   { get; set; }
    public bool   Notified  { get; set; }
    public string CreatedAt { get; set; }
}

public class TaskData
{
    public List<TaskItem> Tasks { get; set; }
    public TaskData() { Tasks = new List<TaskItem>(); }
}

// 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
// 棰滆壊涓婚
// 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
static class C
{
    public static readonly Color BG        = Color.FromArgb(15,  15,  26 );
    public static readonly Color SURFACE   = Color.FromArgb(22,  22,  42 );
    public static readonly Color SURFACE2  = Color.FromArgb(30,  30,  56 );
    public static readonly Color BORDER    = Color.FromArgb(45,  45,  80 );
    public static readonly Color TEXT      = Color.FromArgb(216, 216, 240);
    public static readonly Color MUTED     = Color.FromArgb(102, 102, 160);
    public static readonly Color ACCENT    = Color.FromArgb(124, 106, 245);
    public static readonly Color ACCENT2   = Color.FromArgb(167, 139, 250);
    public static readonly Color DANGER    = Color.FromArgb(240,  96,  96);
    public static readonly Color WARN      = Color.FromArgb(245, 166,  35);
    public static readonly Color OK        = Color.FromArgb( 78, 205, 196);
    public static readonly Color HIGH     = Color.FromArgb(255,  75,  75);
    public static readonly Color MID       = Color.FromArgb(245, 166,  35);
    public static readonly Color LOW      = Color.FromArgb( 78, 205, 196);

    public static readonly Color PRI_BTN_BG  = Color.FromArgb(28,  28,  52);
    public static readonly Color PRI_HIGH_SEL = Color.FromArgb(80,   5,   5);
    public static readonly Color PRI_MID_SEL   = Color.FromArgb(80,  50,   5);
    public static readonly Color PRI_LOW_SEL   = Color.FromArgb( 5,  80,  70);

    public static Color PriBg(string pri, bool selected)
    {
        if (!selected) return PRI_BTN_BG;
        if (pri == "high") return PRI_HIGH_SEL;
        if (pri == "mid")  return PRI_MID_SEL;
        return PRI_LOW_SEL;
    }

    public static Color PriText(string pri, bool selected)
    {
        if (!selected)
        {
            if (pri == "high") return HIGH;
            if (pri == "mid")  return MID;
            return LOW;
        }
        // 閫変腑鏃舵洿浜殑鏂囧瓧
        if (pri == "high") return Color.FromArgb(255, 130, 130);
        if (pri == "mid")  return Color.FromArgb(255, 210,  80);
        return Color.FromArgb(120, 230, 220);
    }
}

// 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
// 浠诲姟鍗＄墖鐘舵€?
// 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
struct CardInfo
{
    public Panel Card, DlFrame;
    public CheckBox CheckBox;
    public Label TitleLabel, DateLabel, CdLabel, PriBadge, DelBtn;
    public ProgressBar ProgressBar;
    public TaskItem Task;
}

// 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
// 涓荤獥鍙?
// 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
public class MainForm : Form
{
    // 鈹€鈹€ INI 鏂囦欢璺緞锛圗XE 鍚岀洰褰曪級鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€
    static string GetIniPath()
    {
        string dir = ".";
        try { dir = Application.StartupPath; } catch { }
        if (string.IsNullOrEmpty(dir) || dir == ".")
            try { dir = Path.GetDirectoryName(Application.ExecutablePath); } catch { }
        if (string.IsNullOrEmpty(dir)) dir = ".";
        return Path.Combine(dir, "save.ini");
    }

    // 鈹€鈹€ 璋冭瘯鏃ュ織锛堝啓鍒?EXE 鍚岀洰褰?debug.log锛夆攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€
    static void Log(string msg)
    {
        try { File.AppendAllText(
            Path.Combine(Path.GetDirectoryName(GetIniPath()) ?? ".", "debug.log"),
            DateTime.Now.ToString("HH:mm:ss") + " " + msg + "\r\n",
            Encoding.UTF8); }
        catch { }
    }

    TaskData _data;
    List<TaskItem> Tasks { get { return _data.Tasks; } }

    Timer _timer;
    bool _paused;

    Label _lblMin, _lblClose;
    TextBox _txtTask, _txtDeadline;
    Label _btnHigh, _btnMid, _btnLow;
    FlowLayoutPanel _listPanel;
    Label _emptyLabel;
    Panel _titleBar, _inputPanel;
    string _currentPri = "mid";
    NotifyIcon _trayIcon;
    readonly Dictionary<string, CardInfo> _cards = new Dictionary<string, CardInfo>();

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    public MainForm()
    {
        Log("=== MainForm ctor start ===");
        Log("StartupPath=" + Application.StartupPath);
        Log("ExecutablePath=" + Application.ExecutablePath);
        Log("INI path=" + GetIniPath());

        _data = LoadData();
        Log("LoadData done, task count=" + _data.Tasks.Count);

        Text            = "TaskTracker";
        FormBorderStyle = FormBorderStyle.None;
        StartPosition   = FormStartPosition.Manual;
        BackColor       = C.BG;
        TransparencyKey = C.BG;
        ShowInTaskbar   = false;
        TopMost         = false;
        Width           = 360;
        Height          = 600;
        AutoScaleMode   = AutoScaleMode.None;

        var scr = Screen.PrimaryScreen.WorkingArea;
        Left = scr.Right  - Width - 20;
        Top  = scr.Bottom - Height - 20;

        BuildUI();
        InitTray();
        StartTimer();

        Shown += (s, e) => { Visible = false; Log("=== MainForm shown, hidden ==="); };
        Log("=== MainForm ctor done ===");
    }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // INI 鏁版嵁瀛樺彇
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    TaskData LoadData()
    {
        var data = new TaskData();
        string ini = GetIniPath();
        try
        {
            Log("LoadData: checking " + ini);
            if (!File.Exists(ini))
            {
                Log("LoadData: file not found");
                return data;
            }

            var lines = File.ReadAllLines(ini, Encoding.UTF8);
            Log("LoadData: read " + lines.Length + " lines");

            TaskItem current = null;

            foreach (var raw in lines)
            {
                string line = raw.Trim();
                if (string.IsNullOrEmpty(line) || line.StartsWith("#")) continue;

                // 瑙ｆ瀽 [task_<id>] 鑺?
                // 鍏煎鏃ф暟鎹細[task_xxx] 鎴?[task_task_xxx]锛堝鍐欎簡涓€娆″墠缂€锛?
                if (line.StartsWith("[task_") && line.EndsWith("]"))
                {
                    // 淇濆瓨涓婁竴涓换鍔?
                    if (current != null && !string.IsNullOrEmpty(current.Title))
                    {
                        data.Tasks.Add(current);
                        Log("LoadData: added task id=" + current.Id + " title=" + current.Title);
                    }

                    // 浠?"[task_xxx]" 鎴?"[task_task_xxx]" 涓彁鍙栫函 id "xxx"
                    string sectionName = line.Substring(1, line.Length - 2); // 鍘绘帀 []
                    string id = sectionName;
                    // 寰幆鍘绘帀鎵€鏈夊墠瀵?"task_" 鍓嶇紑锛堝吋瀹规棫鐗堝啓鍏ョ殑鑴忔暟鎹級
                    while (id.StartsWith("task_"))
                        id = id.Substring(5);
                    current = new TaskItem { Id = id };
                    Log("LoadData: new section raw=" + sectionName + " cleanedId=" + id);
                }
                else if (current != null && line.Contains("="))
                {
                    int eq = line.IndexOf('=');
                    if (eq < 0) continue;
                    string key = line.Substring(0, eq).Trim();
                    string val = line.Substring(eq + 1).Trim();

                    switch (key)
                    {
                        case "Title":    current.Title    = val; break;
                        case "Deadline": current.Deadline = val; break;
                        case "Priority": current.Priority = val; break;
                        case "Done":     current.Done     = (val == "1"); break;
                        case "TotalMs":  { double d; double.TryParse(val, out d); current.TotalMs = d; } break;
                        case "Notified": current.Notified = (val == "1"); break;
                        case "CreatedAt":current.CreatedAt = val; break;
                    }
                }
            }

            // 淇濆瓨鏈€鍚庝竴涓换鍔?
            if (current != null && !string.IsNullOrEmpty(current.Title))
            {
                data.Tasks.Add(current);
                Log("LoadData: added last task id=" + current.Id);
            }

            Log("LoadData: total loaded=" + data.Tasks.Count);
        }
        catch (Exception ex)
        {
            Log("LoadData ERROR: " + ex.Message + " | " + ex.StackTrace);
        }
        return data;
    }

    void SaveData()
    {
        string ini = GetIniPath();
        try
        {
            var sb = new StringBuilder();
            sb.AppendLine("# TaskTracker save file");
            sb.AppendLine("# Do not edit manually");
            sb.AppendLine();

            foreach (var task in Tasks)
            {
                if (string.IsNullOrEmpty(task.Id)) continue;
                // 闃插尽鎬ф竻娲楋細纭繚 id 涓嶅惈 "task_" 鍓嶇紑
                string cleanId = task.Id;
                while (cleanId.StartsWith("task_"))
                    cleanId = cleanId.Substring(5);
                sb.AppendLine("[task_" + cleanId + "]");
                sb.AppendLine("Title="     + (task.Title    ?? ""));
                sb.AppendLine("Deadline=" + (task.Deadline ?? ""));
                sb.AppendLine("Priority=" + (task.Priority ?? "mid"));
                sb.AppendLine("Done="     + (task.Done     ? "1" : "0"));
                sb.AppendLine("TotalMs="  + task.TotalMs.ToString("F0"));
                sb.AppendLine("Notified=" + (task.Notified ? "1" : "0"));
                sb.AppendLine("CreatedAt=" + (task.CreatedAt ?? ""));
                sb.AppendLine();
            }

            Log("SaveData: writing " + Tasks.Count + " tasks to " + ini);
            File.WriteAllText(ini, sb.ToString(), Encoding.UTF8);
            Log("SaveData: write success");
        }
        catch (Exception ex)
        {
            Log("SaveData ERROR: " + ex.Message);
        }
    }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // 璁℃椂鍣?
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    void StartTimer()
    {
        _timer = new Timer { Interval = 1000 };
        _timer.Tick += (s, e) => { if (!_paused) RefreshCountdowns(); };
        _timer.Start();
    }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // 鎵樼洏鍥炬爣
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    void InitTray()
    {
        _trayIcon = new NotifyIcon
        {
            Icon    = CreateTrayIcon(),
            Text    = "浠诲姟杩借釜鍣?,
            Visible = true
        };
        _trayIcon.MouseClick += (s, e) =>
        {
            if (e.Button == MouseButtons.Left) ToggleWindow();
        };
        _trayIcon.ContextMenuStrip = BuildTrayMenu();
    }

    ContextMenuStrip BuildTrayMenu()
    {
        var menu = new ContextMenuStrip();
        menu.BackColor = C.SURFACE2;
        menu.ForeColor = C.TEXT;

        Action<string, EventHandler> Add = (text, h) =>
        {
            var mi = new ToolStripMenuItem(text);
            mi.BackColor = C.SURFACE2;
            mi.ForeColor = C.TEXT;
            mi.Font = new Font("Microsoft YaHei UI", 9);
            mi.Click += h;
            menu.Items.Add(mi);
        };

        Add("鏄剧ず绐楀彛", (s, e) => ShowWindow());
        Add("鍒锋柊", (s, e) => RebuildTasks());
        Add(_paused ? "缁х画鍊掕鏃? : "鏆傚仠鍊掕鏃?, (s, e) => { _paused = !_paused; RebuildTasks(); });
        Add("閫€鍑?, (s, e) => ExitApp());
        return menu;
    }

    Icon CreateTrayIcon()
    {
        using (var bmp = new Bitmap(32, 32))
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            using (var br = new SolidBrush(Color.FromArgb(124, 106, 245)))
                g.FillEllipse(br, 1, 1, 30, 30);
            using (var pen = new Pen(Color.White, 2.5f))
            {
                g.DrawLine(pen, 8, 17, 13, 23);
                g.DrawLine(pen, 13, 23, 23, 11);
            }
            return Icon.FromHandle(bmp.GetHicon());
        }
    }

    void ToggleWindow()  { if (Visible) HideWindow(); else ShowWindow(); }

    void ShowWindow()
    {
        Show();
        WindowState = FormWindowState.Normal;
        Activate();
        WinApi.SetForegroundWindow(Handle);
    }

    void HideWindow() { Hide(); }

    void ExitApp()
    {
        Log("ExitApp: saving " + Tasks.Count + " tasks");
        SaveData();
        _trayIcon.Visible = false;
        _trayIcon.Dispose();
        _timer.Stop();
        Application.Exit();
    }

    void FlashWindow() { try { WinApi.FlashWindow(Handle, true); } catch { } }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // UI 鏋勫缓
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    void BuildUI()
    {
        SuspendLayout();

        // 鈹€鈹€ 鏍囬鏍忥紙缁濆瀹氫綅锛岄珮搴?0锛夆攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€
        _titleBar = new Panel
        {
            BackColor = C.SURFACE,
            Cursor    = Cursors.SizeAll
        };
        bool _dragging = false;
        Point _cursorStart = Point.Empty, _formStart = Point.Empty;

        _titleBar.MouseDown += (s, e) =>
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _cursorStart = Cursor.Position;
                _formStart = Location;
            }
        };
        _titleBar.MouseMove += (s, e) =>
        {
            if (_dragging)
            {
                int dx = Cursor.Position.X - _cursorStart.X;
                int dy = Cursor.Position.Y - _cursorStart.Y;
                Location = new Point(_formStart.X + dx, _formStart.Y + dy);
            }
        };
        _titleBar.MouseUp += (s, e) => { if (e.Button == MouseButtons.Left) _dragging = false; };
        Controls.Add(_titleBar);

        var lblTitle = new Label
        {
            Text     = "\u26A1 浠诲姟杩借釜鍣?,
            Font     = new Font("Microsoft YaHei UI", 11, FontStyle.Bold),
            ForeColor= C.ACCENT2,
            BackColor= C.SURFACE,
            AutoSize = false,
            Location = new Point(12, 0),
            Size     = new Size(280, 40),
            TextAlign= ContentAlignment.MiddleLeft,
            Cursor   = Cursors.SizeAll
        };
        lblTitle.MouseDown += (s, e) =>
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _cursorStart = Cursor.Position;
                _formStart = Location;
            }
        };
        lblTitle.MouseMove += (s, e) =>
        {
            if (_dragging)
            {
                int dx = Cursor.Position.X - _cursorStart.X;
                int dy = Cursor.Position.Y - _cursorStart.Y;
                Location = new Point(_formStart.X + dx, _formStart.Y + dy);
            }
        };
        lblTitle.MouseUp += (s, e) => { if (e.Button == MouseButtons.Left) _dragging = false; };
        _titleBar.Controls.Add(lblTitle);

        _lblMin = new Label
        {
            Text     = "\u2500",
            Font     = new Font("Segoe UI", 13, FontStyle.Bold),
            ForeColor= C.MUTED,
            BackColor= C.SURFACE,
            AutoSize = false,
            Location = new Point(Width - 80, 0),
            Size     = new Size(36, 40),
            TextAlign= ContentAlignment.MiddleCenter,
            Cursor   = Cursors.Hand
        };
        _lblMin.MouseEnter += (s, e) => { _lblMin.ForeColor = C.TEXT; };
        _lblMin.MouseLeave += (s, e) => { _lblMin.ForeColor = C.MUTED; };
        _lblMin.MouseClick += (s, e) => HideWindow();
        _titleBar.Controls.Add(_lblMin);

        _lblClose = new Label
        {
            Text     = "\u2716",
            Font     = new Font("Segoe UI", 11),
            ForeColor= C.MUTED,
            BackColor= C.SURFACE,
            AutoSize = false,
            Location = new Point(Width - 44, 0),
            Size     = new Size(36, 40),
            TextAlign= ContentAlignment.MiddleCenter,
            Cursor   = Cursors.Hand
        };
        _lblClose.MouseEnter += (s, e) => { _lblClose.ForeColor = C.DANGER; };
        _lblClose.MouseLeave += (s, e) => { _lblClose.ForeColor = C.MUTED; };
        _lblClose.MouseClick += (s, e) => ExitApp();
        _titleBar.Controls.Add(_lblClose);

        // 鈹€鈹€ 杈撳叆闈㈡澘锛堢粷瀵瑰畾浣嶏紝楂樺害114锛屼綅浜庢爣棰樻爮涓嬫柟锛夆攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€
        _inputPanel = new Panel { BackColor = C.SURFACE };
        Controls.Add(_inputPanel);

        _txtTask = new TextBox
        {
            Font        = new Font("Microsoft YaHei UI", 11),
            ForeColor   = C.MUTED,
            BackColor   = C.BG,
            BorderStyle = BorderStyle.None,
            Location    = new Point(10, 10),
            Size        = new Size(340, 22),
            Text        = "杈撳叆浠诲姟鍚嶇О锛屽洖杞︽坊鍔?.."
        };
        _txtTask.GotFocus  += (s, e) => {
            if (_txtTask.Text == "杈撳叆浠诲姟鍚嶇О锛屽洖杞︽坊鍔?..") { _txtTask.Text = ""; _txtTask.ForeColor = C.TEXT; } };
        _txtTask.LostFocus += (s, e) => {
            if (_txtTask.Text == "") { _txtTask.Text = "杈撳叆浠诲姟鍚嶇О锛屽洖杞︽坊鍔?.."; _txtTask.ForeColor = C.MUTED; } };
        _txtTask.KeyDown   += (s, e) => { if (e.KeyCode == Keys.Enter) AddTask(); };
        _inputPanel.Controls.Add(_txtTask);

        // 鈹€鈹€ 鎴鏃堕棿琛?鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€
        var metaRow = new Panel { Location = new Point(0, 40), Size = new Size(360, 28), BackColor = C.SURFACE };
        _inputPanel.Controls.Add(metaRow);

        metaRow.Controls.Add(new Label
        {
            Text     = "\u23F0",
            Font     = new Font("Segoe UI", 9),
            ForeColor= C.MUTED,
            BackColor= C.SURFACE,
            Location = new Point(10, 4),
            Size     = new Size(18, 22)
        });

        _txtDeadline = new TextBox
        {
            Font        = new Font("Microsoft YaHei UI", 10),
            ForeColor   = C.MUTED,
            BackColor   = C.BG,
            BorderStyle = BorderStyle.None,
            Location    = new Point(28, 3),
            Size        = new Size(160, 20),
            Text        = "鎴鏃堕棿 yyyy-MM-dd HH:mm"
        };
        _txtDeadline.GotFocus += (s, e) =>
        {
            if (_txtDeadline.Text == "鎴鏃堕棿 yyyy-MM-dd HH:mm")
            {
                _txtDeadline.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm");
                _txtDeadline.ForeColor = C.TEXT;
            }
        };
        _txtDeadline.LostFocus += (s, e) =>
        {
            if (_txtDeadline.Text == "") { _txtDeadline.Text = "鎴鏃堕棿 yyyy-MM-dd HH:mm"; _txtDeadline.ForeColor = C.MUTED; }
        };
        _txtDeadline.MouseClick += (s, e) =>
        {
            if (_txtDeadline.Text == "鎴鏃堕棿 yyyy-MM-dd HH:mm")
            { _txtDeadline.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm"); _txtDeadline.ForeColor = C.TEXT; }
        };
        metaRow.Controls.Add(_txtDeadline);

        // 鈹€鈹€ 浼樺厛绾ф寜閽?鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€
        var priPanel = new Panel { Location = new Point(195, 0), Size = new Size(150, 28), BackColor = C.SURFACE };
        metaRow.Controls.Add(priPanel);

        _btnHigh = MakePriBtn("楂?, new Point(0, 2), "high");
        _btnMid  = MakePriBtn("涓?, new Point(46, 2), "mid");
        _btnLow  = MakePriBtn("浣?, new Point(92, 2), "low");
        priPanel.Controls.AddRange(new Control[] { _btnHigh, _btnMid, _btnLow });

        // 娣诲姞鎸夐挳
        var addBtn = new Label
        {
            Text     = "\u2795 娣诲姞浠诲姟",
            Font     = new Font("Microsoft YaHei UI", 10, FontStyle.Bold),
            ForeColor= Color.White,
            BackColor= C.ACCENT,
            TextAlign= ContentAlignment.MiddleCenter,
            Location = new Point(10, 76),
            Size     = new Size(340, 26),
            Cursor   = Cursors.Hand
        };
        addBtn.MouseClick += (s, e) => AddTask();
        addBtn.MouseEnter += (s, e) => { addBtn.BackColor = Color.FromArgb(144, 126, 255); };
        addBtn.MouseLeave += (s, e) => { addBtn.BackColor = C.ACCENT; };
        _inputPanel.Controls.Add(addBtn);

        // 鈹€鈹€ 浠诲姟鍒楄〃锛堟櫘閫?Panel锛岀粷瀵瑰畾浣嶅湪鏍囬鏍?杈撳叆闈㈡澘涓嬫柟锛夆攢鈹€鈹€鈹€鈹€
        _listPanel = new FlowLayoutPanel
        {
            BackColor     = C.BG,
            AutoScroll    = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents  = false,
            Padding       = new Padding(6, 4, 6, 6)
        };
        Controls.Add(_listPanel);

        // 鈹€鈹€ 鏄惧紡甯冨眬锛氶鍏堢敓鎴愭墍鏈夋帶浠讹紝鍐嶇敤 ResizeLayout 缁熶竴璁句綅缃?鈹€鈹€
        ResizeLayout();
        SizeChanged += (s, e) => ResizeLayout();

        _emptyLabel = new Label
        {
            Name       = "__empty__",
            Text       = "\uD83D\uDCCB 鏆傛棤浠诲姟\n娣诲姞涓€涓紑濮嬭拷韪惂",
            Font       = new Font("Microsoft YaHei UI", 11),
            ForeColor  = C.MUTED,
            BackColor  = C.BG,
            TextAlign  = ContentAlignment.MiddleCenter,
            Size       = new Size(330, 60),
            AutoSize   = false
        };
        _listPanel.Controls.Add(_emptyLabel);

        // 鍙抽敭鑿滃崟
        var ctx = new ContextMenuStrip { BackColor = C.SURFACE2, ForeColor = C.TEXT };
        Action<string, EventHandler> ctxAdd = (t, h) =>
        {
            var m = new ToolStripMenuItem(t);
            m.BackColor = C.SURFACE2; m.ForeColor = C.TEXT;
            m.Font = new Font("Microsoft YaHei UI", 9);
            m.Click += h;
            ctx.Items.Add(m);
        };
        ctxAdd("\uD83D\uDD04 鍒锋柊鏄剧ず",   (s, e) => RebuildTasks());
        ctxAdd(_paused ? "缁х画鍊掕鏃? : "鏆傚仠鍊掕鏃?, (s, e) => { _paused = !_paused; RebuildTasks(); });
        ctxAdd("\uD83D\uDCCB 鏄剧ず绐楀彛",    (s, e) => ShowWindow());
        ctxAdd("\u2716 鍏抽棴",             (s, e) => ExitApp());
        ContextMenuStrip = ctx;

        // 棣栬疆甯冨眬
        ResizeLayout();
        // 绐楀彛澶у皬鍙樺寲鏃堕噸绠?
        SizeChanged += (s, e) => ResizeLayout();
        SetPriority("mid");
        RebuildTasks();
    }

    Label MakePriBtn(string text, Point loc, string pri)
    {
        var lbl = new Label
        {
            Text     = text,
            Font     = new Font("Microsoft YaHei UI", 9, FontStyle.Bold),
            ForeColor= C.PriText(pri, false),
            BackColor= C.PriBg(pri, false),
            TextAlign= ContentAlignment.MiddleCenter,
            Location = loc,
            Size     = new Size(42, 22),
            Cursor   = Cursors.Hand,
            Tag      = pri
        };
        lbl.MouseClick += (s, e) => SetPriority((string)((Label)s).Tag);
        return lbl;
    }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // 甯冨眬閲嶇畻锛堟樉寮?Bounds锛岀粷瀵瑰畾浣嶅彇浠?Dock 宓屽锛?
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    void ResizeLayout()
    {
        const int TITLE_H = 40;
        const int INPUT_H = 114;
        int W = Width;
        int H = Height;

        // 鏍囬鏍忥細鍏ㄥ
        _titleBar.SetBounds(0, 0, W, TITLE_H);
        // 杈撳叆闈㈡澘锛氭爣棰樻爮涓嬫柟
        _inputPanel.SetBounds(0, TITLE_H, W, INPUT_H);
        // 鍒楄〃锛氬～婊″墿浣欏叏閮?
        int listTop = TITLE_H + INPUT_H;
        _listPanel.SetBounds(0, listTop, W, Math.Max(0, H - listTop));
    }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // 浼樺厛绾ф寜閽紙寮虹儓瀵规瘮锛氶€変腑=褰╄壊鑳屾櫙+杈规+浜枃瀛楋級
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    void SetPriority(string pri)
    {
        _currentPri = pri;
        bool sh = pri == "high", sm = pri == "mid", sl = pri == "low";

        _btnHigh.BackColor  = C.PriBg("high", sh);
        _btnHigh.ForeColor = C.PriText("high", sh);
        _btnHigh.BorderStyle = sh ? BorderStyle.FixedSingle : BorderStyle.None;

        _btnMid.BackColor  = C.PriBg("mid", sm);
        _btnMid.ForeColor  = C.PriText("mid", sm);
        _btnMid.BorderStyle = sm ? BorderStyle.FixedSingle : BorderStyle.None;

        _btnLow.BackColor  = C.PriBg("low", sl);
        _btnLow.ForeColor  = C.PriText("low", sl);
        _btnLow.BorderStyle = sl ? BorderStyle.FixedSingle : BorderStyle.None;
    }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // 浠诲姟鎿嶄綔
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    void AddTask()
    {
        string raw = _txtTask.Text.Trim();
        if (raw == "" || raw == "杈撳叆浠诲姟鍚嶇О锛屽洖杞︽坊鍔?..") return;

        string dlRaw = _txtDeadline.Text.Trim();
        string dlIso = null;
        double totalMs = 0;

        if (dlRaw != "" && dlRaw != "鎴鏃堕棿 yyyy-MM-dd HH:mm")
        {
            DateTime dt;
            if (DateTime.TryParse(dlRaw, out dt))
            {
                dlIso   = dt.ToString("yyyy-MM-ddTHH:mm");
                totalMs = (dt - DateTime.Now).TotalMilliseconds;
            }
        }

        var task = new TaskItem
        {
            Id        = DateTime.Now.Ticks.ToString(),
            Title     = raw,
            Deadline  = dlIso,
            Priority  = _currentPri,
            Done      = false,
            TotalMs   = totalMs,
            Notified  = false,
            CreatedAt = DateTime.Now.ToString("o")
        };

        Tasks.Insert(0, task);
        SaveData();
        Log("AddTask: added '" + raw + "' id=" + task.Id + " total tasks=" + Tasks.Count);

        _txtTask.Text      = "杈撳叆浠诲姟鍚嶇О锛屽洖杞︽坊鍔?..";
        _txtTask.ForeColor = C.MUTED;
        _txtDeadline.Text = "鎴鏃堕棿 yyyy-MM-dd HH:mm";
        _txtDeadline.ForeColor = C.MUTED;

        RebuildTasks();
    }

    void DeleteTask(string id)
    {
        Tasks.RemoveAll(t => t.Id == id);
        SaveData();
        RebuildTasks();
    }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // 閲嶅缓鍒楄〃
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    void RebuildTasks()
    {
        _listPanel.SuspendLayout();
        _cards.Clear();
        var toRemove = _listPanel.Controls.Cast<Control>().Where(c => c.Name != "__empty__").ToList();
        foreach (var c in toRemove) _listPanel.Controls.Remove(c);

        _emptyLabel.Visible = (Tasks.Count == 0);
        foreach (var t in Tasks) CreateCard(t);
        _listPanel.ResumeLayout(true);
        _listPanel.PerformLayout();
        Log("RebuildTasks: " + Tasks.Count + " cards created");
    }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // 鍒涘缓鍗＄墖
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    void CreateCard(TaskItem task)
    {
        Log("CreateCard: id=" + task.Id + " title=" + task.Title);
        Color priColor = task.Priority == "high" ? C.HIGH
                       : task.Priority == "mid"  ? C.MID : C.LOW;

        DateTime dtTmp;
        bool hasDeadline = !string.IsNullOrEmpty(task.Deadline)
                        && DateTime.TryParse(task.Deadline, out dtTmp);

        int cardH = hasDeadline ? 112 : 64;

        var outer = new Panel
        {
            Name     = "outer_" + task.Id,
            BackColor= priColor,
            Size     = new Size(348, cardH),
            Margin   = new Padding(0, 0, 0, 4)
        };

        var card = new Panel
        {
            Name     = "card_" + task.Id,
            BackColor= C.SURFACE,
            Dock     = DockStyle.Fill,
            Margin   = new Padding(1, 1, 1, 1)
        };
        outer.Controls.Add(card);

        var bar = new Panel { BackColor = priColor, Dock = DockStyle.Left, Size = new Size(3, cardH - 2) };
        card.Controls.Add(bar);

        var body = new Panel { BackColor = C.SURFACE, Location = new Point(6, 0), Size = new Size(334, cardH - 10) };
        card.Controls.Add(body);

        // 鈹€鈹€ 椤惰 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€
        var topRow = new Panel { BackColor = C.SURFACE, Location = new Point(0, 0), Size = new Size(334, 26) };
        body.Controls.Add(topRow);

        var cb = new CheckBox
        {
            AutoSize = false, Size = new Size(22, 22), Location = new Point(0, 2),
            Checked = task.Done, BackColor = C.SURFACE, FlatStyle = FlatStyle.Popup
        };
        cb.CheckedChanged += (s, e) => { task.Done = cb.Checked; SaveData(); RebuildTasks(); };
        topRow.Controls.Add(cb);

        var titleLbl = new Label
        {
            Text      = task.Title,
            Font      = new Font("Microsoft YaHei UI", 10, FontStyle.Regular),
            ForeColor = task.Done ? C.MUTED : C.TEXT,
            BackColor = C.SURFACE,
            Location  = new Point(24, 0),
            Size      = new Size(268, 26),
            TextAlign = ContentAlignment.MiddleLeft
        };
        topRow.Controls.Add(titleLbl);

        var delBtn = new Label
        {
            Text     = "\u2716", Font = new Font("Segoe UI", 9),
            ForeColor= C.MUTED, BackColor = C.SURFACE,
            Location = new Point(314, 2), Size = new Size(22, 22),
            TextAlign= ContentAlignment.MiddleCenter, Cursor = Cursors.Hand
        };
        delBtn.MouseEnter += (s, e) => { delBtn.ForeColor = C.DANGER; };
        delBtn.MouseLeave += (s, e) => { delBtn.ForeColor = C.MUTED; };
        delBtn.MouseClick += (s, e) => DeleteTask(task.Id);
        topRow.Controls.Add(delBtn);

        // 鈹€鈹€ 浼樺厛绾у窘绔?鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€
        var priBadge = new Label
        {
            Text     = task.Priority == "high" ? "\uD83D\uDD34 楂? :
                       task.Priority == "mid"  ? "\uD83D\uDFE0 涓? : "\uD83D\uDFE2 浣?,
            Font     = new Font("Microsoft YaHei UI", 8, FontStyle.Bold),
            ForeColor= priColor, BackColor = C.PriBg(task.Priority, true),
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(24, 28), Size = new Size(50, 16),
            TextAlign= ContentAlignment.MiddleCenter
        };
        body.Controls.Add(priBadge);

        // 鈹€鈹€ 鎴 + 鍊掕鏃?鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€
        Panel dlFrame = null;
        Label dateLbl = null, cdLbl = null;
        ProgressBar pb = null;

        DateTime dt;
        if (hasDeadline && DateTime.TryParse(task.Deadline, out dt))
        {
            dlFrame = new Panel { BackColor = C.SURFACE2, Location = new Point(0, 47), Size = new Size(334, 47) };
            body.Controls.Add(dlFrame);

            dateLbl = new Label
            {
                Text     = "\uD83D\uDCC5 鎴锛? + dt.ToString("yyyy骞碝M鏈坉d鏃?HH:mm"),
                Font     = new Font("Microsoft YaHei UI", 8),
                ForeColor= C.MUTED, BackColor = C.SURFACE2,
                Location = new Point(6, 4), Size = new Size(320, 16),
                TextAlign= ContentAlignment.MiddleLeft
            };
            dlFrame.Controls.Add(dateLbl);

            cdLbl = new Label
            {
                Text     = "",
                Font     = new Font("Consolas", 10, FontStyle.Bold),
                ForeColor= C.OK, BackColor = C.SURFACE2,
                Location = new Point(6, 20), Size = new Size(240, 18),
                TextAlign= ContentAlignment.MiddleLeft
            };
            dlFrame.Controls.Add(cdLbl);

            pb = new ProgressBar
            {
                Location  = new Point(6, 38),
                Size      = new Size(316, 3),
                Minimum   = 0, Maximum = 100, Value = 100,
                BackColor = C.BORDER, ForeColor = C.OK,
                Style     = ProgressBarStyle.Continuous
            };
            dlFrame.Controls.Add(pb);
        }

        _listPanel.Controls.Add(outer);
        Log("CreateCard: outer added, size=" + outer.Size + " visible=" + outer.Visible);

        _cards[task.Id] = new CardInfo
        {
            Card = outer, DlFrame = dlFrame, CheckBox = cb,
            TitleLabel = titleLbl, DateLabel = dateLbl, CdLabel = cdLbl,
            PriBadge = priBadge, DelBtn = delBtn, ProgressBar = pb, Task = task
        };
    }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // 鍊掕鏃跺埛鏂?
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    void RefreshCountdowns()
    {
        var now = DateTime.Now;
        var dead = new List<string>();

        foreach (var kvp in _cards)
        {
            var id = kvp.Key; var info = kvp.Value; var task = info.Task;
            if (!Tasks.Any(t => t.Id == id)) { dead.Add(id); continue; }
            if (string.IsNullOrEmpty(task.Deadline)) continue;
            DateTime deadline;
            if (!DateTime.TryParse(task.Deadline, out deadline)) continue;

            double diffMs = (deadline - now).TotalMilliseconds;
            bool overdue  = diffMs < 0;

            Color color = overdue ? C.DANGER
                : diffMs < 3600000 ? C.WARN : C.OK;

            string elapsed = overdue
                ? "-" + FormatElapsed(Math.Abs(diffMs))
                : FormatElapsed(diffMs);

            if (info.CdLabel != null)
            {
                info.CdLabel.Text = (overdue ? "宸茶秴鏃?" : "鍓╀綑 ") + elapsed;
                info.CdLabel.ForeColor = color;
            }

            double pct = task.TotalMs > 0
                ? Math.Max(0, Math.Min(100, diffMs / task.TotalMs * 100))
                : overdue ? 0 : 100;

            if (info.ProgressBar != null)
            {
                info.ProgressBar.Value     = (int)pct;
                info.ProgressBar.ForeColor = color;
            }

            // 瓒呮椂涓旀湭瀹屾垚锛氬崱鐗囪儗鏅彉娣辩孩
            if (overdue && !task.Done)
                info.Card.BackColor = Color.FromArgb(50, 12, 12);
            else
                info.Card.BackColor = priColor(task.Priority);

            if (diffMs <= 60000 && diffMs > 0 && !task.Notified)
            {
                task.Notified = true;
                SaveData();
                FlashWindow();
            }
        }
        foreach (var id in dead) _cards.Remove(id);
    }

    Color priColor(string p)
    {
        return p == "high" ? C.HIGH : p == "mid" ? C.MID : C.LOW;
    }

    string FormatElapsed(double ms)
    {
        double s = ms / 1000.0;
        int d = (int)(s / 86400); s %= 86400;
        int h = (int)(s / 3600); s %= 3600;
        int m = (int)(s / 60);   s = (int)(s % 60);
        if (d > 0) return string.Format("{0}澶﹞1:D2}:{2:D2}:{3:D2}", d, h, m, (int)s);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", h, m, (int)s);
    }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // 鍙抽敭鑿滃崟
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        if (e.Button == MouseButtons.Right && ContextMenuStrip != null)
            ContextMenuStrip.Show(this, e.Location);
    }

    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    // 鍏ュ彛
    // 鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲鈺愨晲
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}
