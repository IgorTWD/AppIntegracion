using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;

    public PlayerData PlayerData => _playerData;

    public string Name => _playerData.Name;
    public Color Color => _playerData.Color;

    public UnityEvent OnPlayerUpdated = new UnityEvent();

    public void UpdatePlayer(PlayerData playerData)
    {
        if (!playerData.Equals(_playerData))
        {
            _playerData = playerData;
            OnPlayerUpdated.Invoke();
        }
    }
}
