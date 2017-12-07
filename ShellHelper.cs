using System;
using System.Diagnostics;
public static class ShellHelper
{
    public static string Bash(this string cmd)
    {
        var escapedArgs = cmd.Replace("\"", "\\\"");
        
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{escapedArgs}\"",
                UseShellExecute = true,
                CreateNoWindow = true,
            }
        };
        process.Start();
        process.WaitForExit();
        return "";
    }
}