using Cysharp.Threading.Tasks;
using Lean.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableSampleArray : MonoBehaviour
{
    //public delegate void ClickAction();
    //public static event ClickAction OnClicked;
    [SerializeField] private AssetReference[] _levelPrefabs;
    public int currentLevel = 0;
    private GameObject _currentLevelInstance;
    [SerializeField] private TextMeshProUGUI textLevel;

    public async void OnPlayerLose()
    {
        UnLevels();
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        LoadLevel(currentLevel);

    }
    public async void UnLevels()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.07f));
        await UnloadCurrentLevel();

    }
    public async void OnNextLevelButtonClicked()
    {
        if (currentLevel < _levelPrefabs.Length)
        {
            currentLevel++;
            UnLevels();
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            UnLockNewLevel();
            LoadLevel(currentLevel);
        }
    }
    public void RewindLevel()
    {
        if (currentLevel > 5)
        {
            this.LoadLevel(currentLevel - 4);
        }
        else
        {
            this.LoadLevel(1);
        }

    }
    public async void LoadLevel(int level)
    {
        await LoadLevelPrefab(level);
        currentLevel = level;
        textLevel.text = currentLevel.ToString();
    }
    private async UniTask LoadLevelPrefab(int level)
    {
        if (level <= 0 || level > _levelPrefabs.Length) return;
        AssetReference assetReference = _levelPrefabs[level - 1];
        if (!assetReference.RuntimeKeyIsValid()) return;
        try
        {
            GameObject result = await assetReference.GetGameObject();
            if (result != null)
            {
                _currentLevelInstance = Instantiate(result);
            }

        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error loading level prefab: {ex.Message}");
        }
    }

    public async UniTask UnloadCurrentLevel()
    {
        if (_currentLevelInstance == null) return;
        Destroy(_currentLevelInstance);
        Addressables.ReleaseInstance(_currentLevelInstance);
        await UniTask.CompletedTask;


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
