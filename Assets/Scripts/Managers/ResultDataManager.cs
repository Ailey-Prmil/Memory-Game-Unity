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
        public List<GameResult> ResultList4x4 { get; private set; }
        public List<GameResult> ResultList6x6 { get; private set; }
        public List<GameResult> ResultList8x8 { get; private set; }

        private Dictionary<int, List<GameResult>> ResultList;
        private ResultDataManager()
        {
            ResultList = new Dictionary<int, List<GameResult>>()
            {
                {4, ResultList4x4},
                {6, ResultList6x6},
                {8, ResultList8x8}
            };
        }

        public void LoadResultData()
        {
            Debug.Log(filePath);
            // Load data from file
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath); 
                GameResultList resultList = JsonUtility.FromJson<GameResultList>(json);
                ResultList4x4 = resultList.Results4x4;
                ResultList6x6 = resultList.Results6x6;
                ResultList8x8 = resultList.Results8x8;
            }
            else
            {
                ResultList4x4 = new List<GameResult>();
                ResultList6x6 = new List<GameResult>();
                ResultList8x8 = new List<GameResult>();
            }

            ResultList[4] = ResultList4x4;
            ResultList[6] = ResultList6x6;
            ResultList[8] = ResultList8x8;
        }

        public void SaveResultData()
        {
            // Save data to file
            GameResultList resultData = new GameResultList();
            resultData.Results4x4 = ResultList4x4;
            resultData.Results6x6 = ResultList6x6;
            resultData.Results8x8 = ResultList8x8;
            string json = JsonUtility.ToJson(resultData, true);
            File.WriteAllText(filePath, json);

        }

        public void AddResultData(GameResult result, int gridDim)
        {
            if (gridDim != 4 && gridDim != 6 && gridDim != 8)
            {
                Debug.LogError("Invalid grid dimension: " + gridDim);
                return;
            }

            if (ResultList[gridDim].Count == 0)
            {
                result.Id = 1;
            }
            else
            {
                result.Id = ResultList[gridDim][^1].Id + 1; // Get Id of last element and increment by 1
            }

            ResultList[gridDim].Add(result);
        }

        public List<GameResult> GetTopResults(int count, int gridDim)
        {
            // Return top results
            var sortedGameResultList = ResultList[gridDim].OrderBy(gr => gr.Score).ToList();
            return sortedGameResultList.GetRange(0, sortedGameResultList.Count >= count ? count : sortedGameResultList.Count);
        }

    }
}
