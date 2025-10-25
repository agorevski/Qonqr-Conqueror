using System;
using System.Collections.Generic;
using System.IO;

namespace Qonqr
{
    public static class Logger
    {
        private static readonly string LogDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

        public enum Action
        {
            Login,
            HarvestAll,
            Attack
        }

        static Logger()
        {
            // Ensure log directory exists
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
        }

        /// <summary>
        /// Logs an error message with full exception details
        /// </summary>
        public static void LogError(string context, Exception ex, string username = "")
        {
            string line = string.Format("{0}: ERROR in {1}: {2}\nStack Trace: {3}",
                DateTime.Now.ToString("u"), context, ex.Message, ex.StackTrace);
            LogLine(username, line, "error");
        }

        /// <summary>
        /// Logs an error message without exception
        /// </summary>
        public static void LogError(string context, string errorMessage, string username = "")
        {
            string line = string.Format("{0}: ERROR in {1}: {2}",
                DateTime.Now.ToString("u"), context, errorMessage);
            LogLine(username, line, "error");
        }

        /// <summary>
        /// Logs an informational message
        /// </summary>
        public static void LogInfo(string message, string username = "")
        {
            string line = string.Format("{0}: INFO: {1}",
                DateTime.Now.ToString("u"), message);
            LogLine(username, line, "info");
        }

        public static void LogLogin(string username)
        {
            string line = string.Format("{0}: {1} Logged In", 
                DateTime.Now.ToString("u"), username);
            LogLine(username, line);
        }

        public static void LogAttack(string username, LaunchApiCall launchData)
        {
            string line = string.Format("{0}: Formation {1}, Attacked {2} [{3}/{4}] K/D: {5}",
                DateTime.Now.ToString("u"), launchData.Summary.Breakdown.LaunchedFormationId, launchData.Zone.LeaderCodename,
                launchData.Summary.Breakdown.EnemyBotsDestroyed, launchData.Summary.Breakdown.EnemyBotsFaced, launchData.Summary.Breakdown.KillRatio);
            LogLine(username, line);
        }

        public static void LogData(string line)
        {
            LogLine("", line);
        }

        public static void LogHarvest(string username, string harvestAmount)
        {
            string line = string.Format("{0}: Harvested Bases For {1}",
                DateTime.Now.ToString("u"), harvestAmount);
            LogLine(username, line);
        }

        private static void LogLine(string username, string line, string logType = "history")
        {
            try
            {
                string filename = string.IsNullOrEmpty(username) 
                    ? $"app_{logType}.txt" 
                    : $"{username}_{logType}.txt";
                string filepath = Path.Combine(LogDirectory, filename);
                
                line = line + Environment.NewLine;
                
                // Use File.AppendAllText for simpler, safer file writing
                File.AppendAllText(filepath, line);
            }
            catch (Exception ex)
            {
                // If logging fails, write to console as fallback
                Console.WriteLine($"Failed to write log: {ex.Message}");
            }
        }

        public static List<string> ReadLogs(string username)
        {
            string filepath = Path.Combine(LogDirectory, username + "_history.txt");
            List<string> readLogs = new List<string>();

            try
            {
                if (File.Exists(filepath))
                {
                    using (StreamReader sr = new StreamReader(filepath))
                    {
                        while (!sr.EndOfStream)
                        {
                            readLogs.Add(sr.ReadLine());
                        }
                    }
                    readLogs.Reverse();
                }
            }
            catch (Exception ex)
            {
                LogError("ReadLogs", ex);
            }

            return readLogs;
        }

    }
}
