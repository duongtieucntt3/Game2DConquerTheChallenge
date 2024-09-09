using Cysharp.Threading.Tasks;
using Lean.Touch;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableSampleArray : MonoBehaviour
{
    [SerializeField] private AssetReference[] _levelPrefabs;
    public int currentLevel = 0;
    public GameObject _currentLevelInstance;

    private async void OnPlayerLose()
    {

        if (_currentLevelInstance != null)
        {
            UnloadCurrentLevel();
        }
        await LoadLevelPrefab(currentLevel);
    }

    public async void OnNextLevelButtonClicked()
    {
        if(currentLevel < _levelPrefabs.Length)
        {
            currentLevel++;
            UnloadCurrentLevel();
            UnLockNewLevel();
            await LoadLevelPrefab(currentLevel);
        }
        else
        {
            Debug.LogError("No more levels to load.");
        }

    }
    public async void LoadLevel(int level)
    {
        await LoadLevelPrefab(level);
    }
    private async UniTask LoadLevelPrefab(int level)
    {
        if (level <= 0 || level > _levelPrefabs.Length)
        {
            Debug.LogError("Invalid level index.");
            return;
        }
        AssetReference assetReference = _levelPrefabs[level - 1];
        if (!assetReference.RuntimeKeyIsValid())
        {
            Debug.LogError("Invalid asset reference.");
            return;
        }
        try
        {
            GameObject result = await assetReference.GetGameObject();
            if (result != null)
            {
                _currentLevelInstance = Instantiate(result);
            }
            else
            {
                Debug.LogError("Failed to load level prefab.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error loading level prefab: {ex.Message}");
        }
    }

    private void UnloadCurrentLevel()
    {
        if (_currentLevelInstance != null)
        {
            Destroy(_currentLevelInstance);
            // Gi?i phóng tài nguyên c?a ??i t??ng ?ã b? h?y
            Addressables.ReleaseInstance(_currentLevelInstance);
            _currentLevelInstance = null;
        }
    }
    private void UnLockNewLevel()
    {
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 0);
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (currentLevel >= reachedIndex)
        {
            PlayerPrefs.SetInt("ReachedIndex", currentLevel + 1);
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1);
            PlayerPrefs.Save();
        }
    }
}


public static class AddressableUniTaskExtensions
{
    public static async UniTask<GameObject> GetGameObject(this AssetReference assetReference)
    {
        var asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(assetReference);
        GameObject result = await asyncOperationHandle.Task.AsUniTask();
        return result;
    }
}
