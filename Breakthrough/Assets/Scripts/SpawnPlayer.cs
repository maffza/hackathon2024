using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (_playerPrefab != null)
        {
            GameObject playerInstance = Instantiate(_playerPrefab);
            playerInstance.transform.position = transform.position;
        }
        else
        {
            Debug.LogError("Player prefab not set");
        }
    }
}
