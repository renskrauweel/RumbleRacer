using UnityEngine;

namespace Lib
{
    public class Constants
    {
        public const string AI_IP = "localhost";
        public const string AI_PORT = "8765";
        
        public static readonly string LOG_FOLDER_EXCEPTIONS = Application.persistentDataPath + "\\Logging\\exceptionlogs\\";
        public static readonly string LOG_FOLDER_REPLAY = Application.persistentDataPath + "\\logging\\replaylogs\\";
    }
}