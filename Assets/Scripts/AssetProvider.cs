using UnityEngine;

public class AssetProvider : MonoBehaviour
{
    [ field: SerializeField] public GameObject Player {  get; private set; }

    public void SetPlayer(GameObject player)
    {
        Player = player;
        if (Player != null)
        {
            Debug.Log("Avatar is loaded");
        }
    }
}
