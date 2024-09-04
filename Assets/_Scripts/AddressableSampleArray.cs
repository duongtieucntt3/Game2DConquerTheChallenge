using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableSampleArray : MonoBehaviour
{
    [SerializeField] private AssetReference[] _levelPrefabs;
    public int currentLevel = 1;
    private GameObject _currentLevelInstance;

    async void Start()
    {
        await LoadLevelPrefab(currentLevel);
    }

    public async void OnNextLevelButtonClicked()
    {
        currentLevel++;


        UnloadCurrentLevel();


        await LoadLevelPrefab(currentLevel);
    }

    private async UniTask LoadLevelPrefab(int level)
    {
        if (level <= 0 || level > _levelPrefabs.Length)
        {
            Debug.LogError("Invalid level index.");
            return;
        }

        AssetReference assetReference = _levelPrefabs[level - 1];
        GameObject result = await assetReference.GetGameObject2();
        _currentLevelInstance = Instantiate(result);
    }

    private void UnloadCurrentLevel()
    {
        if (_currentLevelInstance != null)
        {
            Destroy(_currentLevelInstance);
        }
    }
}

public static class AddressableUniTaskExtensions
{
    public static async UniTask<GameObject> GetGameObject2(this AssetReference assetReference)
    {
        var asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(assetReference);
        GameObject result = await asyncOperationHandle.Task.AsUniTask();
        return result;
    }
}
