using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager_Lecture : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;

    string _gameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        _gameId = Application.platform == RuntimePlatform.IPhonePlayer
           ? _iOSGameId
           : _androidGameId;

        Advertisement.Initialize(_gameId, _testMode, true, this);
        Debug.Log("Initializing Ads System... " + _gameId);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Initialization Complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

}
