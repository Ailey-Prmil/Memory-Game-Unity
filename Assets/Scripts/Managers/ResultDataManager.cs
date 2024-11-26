using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ResultDataManager
    {
        private static ResultDataManager instance;
        public static ResultDataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResultDataManager();
                    instance.LoadResultData();
                }
                return instance;
            }
        }

        public string filePath = Application.persistentDataPath + "/resultData.json";
        public List<GameResult> ResultList { get; private set; }

        public void LoadResultData()
        {
            Debug.Log(filePath);
            // Load data from file
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath); 
                GameResultList resultList = JsonUtility.FromJson<GameResultList>(json);
                ResultList = resultList.Results;
            }
            else
            {
                ResultList = new List<GameResult>();
            }

        }

        public void SaveResultData()
        {
            // Save data to file
            GameResultList resultData = new GameResultList();
            resultData.Results = ResultList;
            string json = JsonUtility.ToJson(resultData, true);
            File.WriteAllText(filePath, json);

        }

        public void AddResultData(GameResult result)
        {
            if (ResultList.Count == 0)
            {
                result.Id = 1;
            }
            else
            {
                result.Id = ResultList[^1].Id + 1; // Get Id of last element and increment by 1
            }

            ResultList.Add(result);
        }

        public List<GameResult> GetTopResults(int count)
        {
            // Return top results
            var sortedGameResultList = ResultList.OrderBy(gr => gr.Score).ToList();
            return sortedGameResultList.GetRange(0, sortedGameResultList.Count >= count ? count : sortedGameResultList.Count);
        }

    }
}
