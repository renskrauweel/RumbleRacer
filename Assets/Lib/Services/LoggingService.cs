using System;
using System.IO;
using UnityEngine;

namespace Lib.Services
{
    public class LoggingService
    {
        private string exceptionLogDir = Constants.LOG_FOLDER_EXCEPTIONS;
        private string replayLogDir = Constants.LOG_FOLDER_REPLAY;
        
        public void LogException(string condition, string stacktrace)
        {
            if (!Directory.Exists(exceptionLogDir)) Directory.CreateDirectory(exceptionLogDir);
            using (StreamWriter sw = File.AppendText(exceptionLogDir+DateTime.Now.ToString("dd-MM-yyyy")+"_log.txt"))
            {
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToShortDateString()} -- {String.Format("{0} -- {1}", condition, stacktrace)}");
            }
        }

        public Guid GetCarReplayGuid()
        {
            return Guid.NewGuid();
        }

        public void LogCarReplay(Guid guid, float timeInMs, Vector3 position, Quaternion rotation)
        {
            using (StreamWriter sw = File.AppendText(replayLogDir+guid+".txt"))
            {
                sw.WriteLine($"{timeInMs} -- {position} -- {rotation}");
            }
        }
    }
}