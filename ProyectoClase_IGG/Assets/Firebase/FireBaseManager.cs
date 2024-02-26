using Firebase;
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireBaseManager : MonoBehaviour
{
    private static FireBaseManager _instance;

    public static FireBaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //_instance = LazyInitFirebaseManager();
            }
            return _instance;
        }
    }
    private FirebaseAuth _auth;

    public FirebaseAuth Auth
    {
        get
        {
            if (_auth == null)
            {
                _auth = FirebaseAuth.GetAuth(App);
            }
            return _auth;
        }
    }

    private FirebaseApp _app;

    public FirebaseApp App
    {
        get
        {
            if (_app == null)
            {
                //_app = GetAppSynchronous();
            }
            return _app;
        }
    }

    public UnityEvent OnFirebaseInitialized = new UnityEvent();

    private async void Awake ()
    {
        if(_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
            var dependencyResult = await FirebaseApp.CheckAndFixDependenciesAsync();
            if(dependencyResult == DependencyStatus.Available)
            {
                _app = FirebaseApp.DefaultInstance;
                OnFirebaseInitialized.Invoke();
            }
            else
            {
                Debug.LogError($"Failed to initialize Firebase with {dependencyResult}");
            }
        }
        else
        {
            Debug.LogError($"An instance of {nameof(FireBaseManager)} already exists!");
        }
    }

    private void OnDestroy()
    {
        _auth = null;
        _app = null;    
        if (_instance == this)
        {
            _instance = null;
        }
    }



}
