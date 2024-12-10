using ReadyPlayerMe.Samples.QuickStart;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private Vector3 spawnPosition => transform.position;

    private GameObject spawnGameObject;

    [SerializeField] private ThirdPersonLoader _playerLoader;
    public void SetPlayer(GameObject player)
    {
        spawnGameObject = player;

    }
    public void SetupPlayer()
    {
        _playerLoader.SetPlayer(spawnGameObject);
    }
}
