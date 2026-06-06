using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLoader : MonoBehaviour
{
    public void LoadStoryGraphAsync(string address, Action<StoryGraphData> onLoaded)
    {
        AsyncOperationHandle<StoryGraphData> handle = Addressables.LoadAssetAsync<StoryGraphData>(address);
        handle.Completed += operation =>
        {
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                onLoaded?.Invoke(operation.Result);
            }
            else
            {
                Debug.LogError($"Failed to load StoryGraphData from address: {address}");
            }
        };
    }

    public void LoadEnemyAsync(string address, Action<EnemyCharacterData> onLoaded)
    {
        AsyncOperationHandle<EnemyCharacterData> handle = Addressables.LoadAssetAsync<EnemyCharacterData>(address);
        handle.Completed += operation =>
        {
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                onLoaded?.Invoke(operation.Result);
            }
            else
            {
                Debug.LogError($"Failed to load EnemyCharacterData from address: {address}");
            }
        };
    }
}