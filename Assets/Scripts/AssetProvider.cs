using UnityEngine;

public class AssetProvider : MonoBehaviour
{
    public static AssetProvider Instance;
    [ field: SerializeField] public GameObject Player {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void SetPlayer(GameObject player)
    {
        Player = player;
    }
}
