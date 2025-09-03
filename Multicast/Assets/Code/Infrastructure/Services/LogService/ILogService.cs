using System;

namespace Infrastructure.Services.LogService
{
    public interface ILogService
    {
        void Log(string msg);
        void LogState(string msg, object obj);
        void LogService(string msg, object obj);
        void LogAudio(string msg, object obj);
        void LogError(string msg);
        void LogError(Exception exception);
        void LogWarning(string msg);
    }
}