using UnityEngine;

namespace Gameplay.Levels.Configs
{
    [CreateAssetMenu(menuName = "Configs/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField, Range(0, 10)] public int MinClusterLength { get; private set; }
        [field: SerializeField, Range(0, 10)] public int MaxClusterLength { get; private set; }
        [field: SerializeField, Range(0, 10)] public int WordsCount { get; private set; }

        private void OnValidate()
        {
            if (MinClusterLength > MaxClusterLength)
                MinClusterLength = MaxClusterLength;
        }
    }
}