using System.Diagnostics;
using System.IO;

namespace Lib.Services
{
    public class StreamService
    {
        public string PythonPath { get; set; }
        public string ScriptPath { get; set; }
        
        public StreamService()
        {
            PythonPath = Constants.PYTHONPATH;
            ScriptPath = Constants.SCRIPTPATH;
        }

        public void StreamOut(string input)
        {
            stream_read_write(ScriptPath, input);
        }
        
        private void stream_read_write(string scriptPath, string input)
        {
            ProcessStartInfo start = new ProcessStartInfo();

            start.FileName = PythonPath;
            start.Arguments = string.Format("{0} {1}", scriptPath, input);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardInput = true;
            start.CreateNoWindow = true;

            using (Process process = Process.Start(start))
            using (StreamWriter writer = process.StandardInput)
            using (StreamReader reader = process.StandardOutput)
            {
                UnityEngine.Debug.Log("WRITING: " + input);
                writer.WriteLine(input);

                UnityEngine.Debug.Log("RECEIVED: "+reader.ReadToEnd());
            }
        }
    }
}