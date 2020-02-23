using System.Diagnostics;

namespace BacklogKiller.ClassLibrary.Services
{
    public class MsDosService
    {
        public string Execute(string command)
        {
            var cmd = "/c " + command;
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    Arguments = cmd,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                }
            };
            proc.Start();

            var output = proc.StandardOutput.ReadToEnd();

            return output;
        }
    }
}
