using Microsoft.Win32.TaskScheduler;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using SystemTask = System.Threading.Tasks.Task;

class Program
{
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    const int SW_HIDE = 0;
    const int SW_SHOWMINIMIZED = 2;

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool FlashWindow(IntPtr hwnd, bool bInvert);


    static async SystemTask Main()
    {
        IntPtr handle = GetConsoleWindow();
        ShowWindow(handle, SW_HIDE); // Hide console window

        const string taskName = "ArduWareUpdater";

        if (!IsTaskScheduled(taskName))
        {
            ScheduleUpdaterTask();
            Console.WriteLine("Naplánovaná automatická kontrola aktualizácií.");
        }

        var checker = new UpdateChecker();
        var info = await checker.FetchLatestVersionAsync();

        if (info == null)
        {
            Console.WriteLine("Zlyhalo nacitavanie dat zo servera.");
            return;
        }

        if (checker.IsNewerVersion(info.LatestVersion))
        {
            ShowWindow(handle, SW_SHOWMINIMIZED); // Show minimized
            FlashWindow(handle, true); // Flash taskbar icon

            Console.WriteLine($"Je dostupna nova verzia v{info.LatestVersion}");
            Console.WriteLine($"Zmeny: {info.ReleaseNotes}");
            Console.WriteLine("Chcete nainstalovat teraz? (Y/N)");

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Y)
            {
                Console.WriteLine("Stahujem...");
                Process.Start(new ProcessStartInfo
                {
                    FileName = info.DownloadUrl,
                    UseShellExecute = true
                });
            }
            else
            {
                Console.WriteLine("Aktualizacia odlozena.");
            }
        }
        else
        {
            Console.WriteLine("Mate najnovsiu verziu.");
        }
        
    }

    public static void ScheduleUpdaterTask()
    {
        using (TaskService ts = new TaskService())
        {
            // Create a new task definition
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "Checks for ArduWare updates";

            // Trigger: Weekly, every Wednesday at 10:00 AM
            /*td.Triggers.Add(new WeeklyTrigger
            {
                StartBoundary = DateTime.Today.AddDays(1).AddHours(10),
                DaysOfWeek = DaysOfTheWeek.Wednesday
            });*/

            // Trigger: User logon 
            td.Triggers.Add(new LogonTrigger
            {
                Delay = TimeSpan.FromMinutes(1) // Delay to allow system to stabilize
            });

            // Action: Run your updater executable
            td.Actions.Add(new ExecAction(@"C:\Program Files\ArduWare\ArduWareUpdater.exe", null, null));

            // Register the task
            ts.RootFolder.RegisterTaskDefinition(@"ArduWareUpdate", td);
        }
    }

    public static bool IsTaskScheduled(string taskName)
    {
        using (TaskService ts = new TaskService())
        {
            return ts.GetTask(taskName) != null;
        }
    }
}

public class UpdateChecker
{
    private const string VersionUrl = "https://raw.githubusercontent.com/Simon06SVK/ArduWare/refs/heads/master/version.json?token=GHSAT0AAAAAADIRM5DFA7IQ35HJOPVF2WKK2FPE5JA";
    private const string CurrentVersion = "1.0"; // Replace with your actual version

    public async Task<VersionInfo> FetchLatestVersionAsync()
    {
        try
        {
            using var client = new HttpClient();
            string json = await client.GetStringAsync(VersionUrl);
            return JsonSerializer.Deserialize<VersionInfo>(json);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching version info: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error parsing version info: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
        return null;
}

    public bool IsNewerVersion(string latest)
    {
        return new Version(latest) > new Version(CurrentVersion);
    }
}
public class VersionInfo
{
    public string LatestVersion { get; set; }
    public string DownloadUrl { get; set; }
    public string ReleaseNotes { get; set; }
}

// Note: Ensure you have a version.json file at the specified URL with the following structure: 
// {
//   "LatestVersion": "1.1",
//   "DownloadUrl": "
//   "ReleaseNotes": "Added new features and fixed bugs."
// }
// Also, make sure to handle exceptions and edge cases in production code.
//   "
//     "ReleaseNotes": "Added new features and fixed bugs."
// }




