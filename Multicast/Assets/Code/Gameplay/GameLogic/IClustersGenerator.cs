using System.Collections.Generic;

namespace GameLogic.Gameplay.GameLogic
{
    public interface IClustersGenerator
    {
        List<string> GetClusterBy(Level data);
    }
}