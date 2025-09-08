using System.Collections.Generic;
using Gameplay.Levels.LevelCompletion;
using UnityEngine;

namespace Gameplay.Levels.Configs
{
    [CreateAssetMenu(menuName = "Configs/LevelCompletionConfig", fileName = "LevelCompletionConfig")]
    public class LevelCompletionConfig : ScriptableObject
    {
        [field: SerializeField] public List<LevelCompletionType> CompletionTypes { get; private set; }
    }
}