﻿namespace Management.Application.Common.Interfaces
{
    public interface ILoggerManager
    {
        void LogDebug(string message);
        void LogInfo(string message);
        void LogError(string message);
        void LogWarn(string message);
    }
}
