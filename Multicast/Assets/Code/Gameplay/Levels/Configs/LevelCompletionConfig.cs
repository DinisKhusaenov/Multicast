using System.Collections.Generic;
using Gameplay.Levels.LevelCompletion;
using Gameplay.StaticData;
using UnityEngine;

namespace Gameplay.Levels.Configs
{
    [CreateAssetMenu(menuName = "Configs/LevelCompletionConfig", fileName = "LevelCompletionConfig")]
    public class LevelCompletionConfig : ScriptableObject, IStaticData
    {
        [field: SerializeField] public List<LevelCompletionType> CompletionTypes { get; private set; }
    }
}