using Firebase.Database;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSaveManager : MonoBehaviour
{
    private const string PLAYER_KEY = "PLAYER_KEY";
    private FirebaseDatabase _database;
    private DatabaseReference _ref;

    public PlayerData LastPlayerData { get; private set; }
    public PlayerUpdatedEvent OnPlayerUpdated = new PlayerUpdatedEvent();

    // Start is called before the first frame update
    void Start()
    {
        _database = FirebaseDatabase.DefaultInstance;
        _ref = _database.GetReference(PLAYER_KEY);
        _ref.ValueChanged += HandleValueChanged;
    }

    private void OnDestroy()
    {
        _ref.ValueChanged -= HandleValueChanged;
        _ref = null;
        _database = null;

    }

    public void SavePlayer(PlayerData player)
    {
        if (!player.Equals(LastPlayerData))
        {
            PlayerPrefs.SetString(PLAYER_KEY, JsonUtility.ToJson(player));
            _database.GetReference(PLAYER_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(player));

        }
    }

    //public PlayerData? LoadPlayer()
    //{
    //    if (SaveExists())
    //    {
    //        return.JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(PLAYER_KEY));
    //    }
    //}

    public async Task<PlayerData?> LoadPlayer()
    {
        var dataSnapshot = await _database.GetReference(PLAYER_KEY).GetValueAsync();
        if (!dataSnapshot.Exists)
        {
            return null;
        }
        return JsonUtility.FromJson<PlayerData>(dataSnapshot.GetRawJsonValue());
    }

    public async Task<bool> SaveExists()
    {
        var dataSnapshot = await _database.GetReference(PLAYER_KEY).GetValueAsync();
        return dataSnapshot.Exists;
    }

    public void EraseSave()
    {
        _database.GetReference(PLAYER_KEY).RemoveValueAsync();
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs e)
    {
        throw new NotImplementedException();
    }
    public class PlayerUpdatedEvent : UnityEvent<PlayerData>
    {

    }
}
  

