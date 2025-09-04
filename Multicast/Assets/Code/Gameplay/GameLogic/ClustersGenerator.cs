using System;
using System.Collections.Generic;
using Random = System.Random;

namespace GameLogic.Gameplay.GameLogic
{
    public class ClustersGenerator : IClustersGenerator
    {
        private readonly int _minLength;
        private readonly int _maxLength;
        private readonly Random _random;
        
        public ClustersGenerator(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
            _random = new Random();
        }

        public List<string> GetClusterBy(Level data)
        {
            if (data == null)
                throw new Exception("Level data is can not be null");
            
            var allClusters = new List<string>();

            foreach (var word in data.Words)
            {
                if (string.IsNullOrEmpty(word) || word.Length < _minLength)
                    continue;

                var clusters = RandomSplit(word);
                allClusters.AddRange(clusters);
            }

            Shuffle(allClusters);

            return allClusters;
        }
        
        private List<string> RandomSplit(string word)
        {
            if (word.Length < _minLength)
                return new List<string> { word };

            var result = new List<string>();
            string remaining = word;

            while (remaining.Length > 0)
            {
                if (remaining.Length <= _maxLength)
                {
                    result.Add(remaining);
                    break;
                }

                int maxSize = Math.Min(_maxLength, remaining.Length - _minLength);
                int size = _random.Next(_minLength, maxSize + 1);

                result.Add(remaining.Substring(0, size));
                remaining = remaining.Substring(size);
            }

            return result;
        }
        
        private void Shuffle(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int j = _random.Next(i, list.Count);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}