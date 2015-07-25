using System;
using System.Collections.Generic;
using System.IO;

namespace Qonqr
{
    public static class Logger
    {
        public enum Action
        {
            Login,
            HarvestAll,
            Attack
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

        private static void LogLine(string username, string line)
        {
            string filepath = Directory.GetCurrentDirectory() + "\\" + username + "_history.txt";
            line = line + Environment.NewLine;
            using (FileStream fs = File.OpenWrite(filepath))
            {
                fs.Seek(0, SeekOrigin.End);
                foreach (char c in line)
                {
                    fs.WriteByte((byte)c);
                }
            }
        }

        public static List<string> ReadLogs(string username)
        {
            string filepath = Directory.GetCurrentDirectory() + "\\" + username + "_history.txt";
            List<string> readLogs = new List<string>();

            using (StreamReader sr = new StreamReader(filepath))
            {
                while (!sr.EndOfStream)
                {
                    readLogs.Add(sr.ReadLine());
                }
            }
            readLogs.Reverse();
            return readLogs;
        }

    }
}