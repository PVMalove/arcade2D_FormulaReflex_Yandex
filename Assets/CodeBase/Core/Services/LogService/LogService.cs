﻿using UnityEngine;

namespace CodeBase.Core.Services.LogService
{
    public class LogService : ILogService
    {
        private const bool isStateLog = false;
        private const bool isLog = false;
        private const bool isYandexLog = false;
        private const bool isAudioLog = false;
        
        public void Log(string msg, object obj)
        {
            if (isLog)
            {
                string className = obj.GetType().Name;
                Debug.Log($"[State] -> [{className}] -> {msg}");
            }
        }

        public void LogState(string msg, object obj)
        {
            if (isStateLog)
            {
                string className = obj.GetType().Name;
                Debug.Log($"[State] -> [{className}] -> {msg}");
            }
        }

        public void LogYandex(string msg, object obj)
        {
            if (isYandexLog)
            {
                string className = obj.GetType().Name;
                Debug.Log($"[YandexGame] -> [{className}] -> {msg}");
            }
        }

        public void LogAudio(string msg, object obj)
        {
            if (isAudioLog)
            {
                string className = obj.GetType().Name;
                Debug.Log($"[Audio] --> [{className}] -> {msg}");
            }
        }
        
        public void LogError(string msg) => 
            Debug.LogError(msg);

        public void LogWarning(string msg) => 
            Debug.LogWarning(msg);
    }
}