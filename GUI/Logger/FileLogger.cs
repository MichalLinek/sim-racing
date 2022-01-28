using System;
using System.IO;

namespace GUI.Logger
{
    class FileLogger
    {
        private static readonly string FILE_NAME = $"logs/{DateTime.Now.ToString("dd.MM-hh.mm.ss")}.txt";

        static FileLogger()
        {
            Directory.CreateDirectory("logs");
            File.AppendAllText(FILE_NAME, $"[INFO] [{DateTime.Now.ToString("hh.mm.ss")}] Log Created");
        }
 
        public static void Log(LogTypeEnum type, string data)
        {
            string prefix = "";
            switch(type)
            {
                case LogTypeEnum.Info:
                    {
                        prefix = "[INFO]";
                        break;
                    }
                case LogTypeEnum.Warning:
                    {
                        prefix = "[WARNING]";
                        break;
                    }
                case LogTypeEnum.Error:
                    {
                        prefix = "[ERROR]";
                        break;
                    }
                case LogTypeEnum.Critical:
                    {
                        prefix = "[CRITICAL]";
                        break;
                    }
            }
           
            data = $"\n{prefix} [{DateTime.Now.ToString("hh.mm.ss")}] {data}";
            File.AppendAllText(FILE_NAME, data);
        }

        public static void LogInfo (string data) => Log(LogTypeEnum.Info, data);
        public static void LogWarning(string data) => Log(LogTypeEnum.Warning, data);
        public static void LogError(string data) => Log(LogTypeEnum.Error, data);
        public static void LogCritical(string data) => Log(LogTypeEnum.Critical, data);
    }
}
