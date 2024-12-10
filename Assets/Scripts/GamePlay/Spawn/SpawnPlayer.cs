using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private Vector3 spawnPosition => transform.position;

    public void SetPlayer(GameObject player)
    {
        player.transform.position = spawnPosition;
    }
    public void SetupPlayer()
    {

    }
}
