using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadMultiple : MonoBehaviour
{
    // List of labels (or even addresses!) to load. Could also be just one single label
    public List<string> keys = new List<string>();

    AsyncOperationHandle<IList<GameObject>> loadHandle;

    // Just for demonstration purposes, to spawn the assets to the side of the objects
    float x = 0;

    // Start is called before the first frame update
    void Start()
    {
        loadHandle = Addressables.LoadAssetsAsync<GameObject>(
            keys,
            Loaded,
            Addressables.MergeMode.UseFirst,
            false);
        loadHandle.Completed += Handle_Completed;
    }
    // Called everytime an asset has been loaded
    private void Loaded(GameObject addressableObj)
    {
        Instantiate<GameObject>(addressableObj,
            new Vector3(x * 2.0f, 0, 0),
            Quaternion.identity,
            transform); ;

        x++;
    }
    // Called after the entire operation has been completed
    private void Handle_Completed(AsyncOperationHandle<IList<GameObject>> operation)
    {
        if (operation.Status != AsyncOperationStatus.Succeeded)
            Debug.LogWarning("Some assets failed to load.");
    }
    private void OnDestroy()
    {
        Addressables.Release(loadHandle);
    }
}
