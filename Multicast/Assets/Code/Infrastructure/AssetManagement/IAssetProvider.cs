using Cysharp.Threading.Tasks;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask<T> Load<T>(AssetPathType key);
        void Release(AssetPathType key);
        void ReleaseAll();
    }
}