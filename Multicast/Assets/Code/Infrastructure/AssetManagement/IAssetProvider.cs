using Cysharp.Threading.Tasks;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask<T> Load<T>(string key);
        void Release(string key);
        void ReleaseAll();
    }
}