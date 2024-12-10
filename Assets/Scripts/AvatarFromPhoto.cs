using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Samples.AvatarCreatorElements;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AvatarFromPhoto : MonoBehaviour
{
    [SerializeField] private PhotoCaptureElement _photoCaptureElement;
    [SerializeField] private AvatarHandler _avatarHandler;
    [SerializeField] private Button _buttonConfirm;

    private AssetProvider _assetProvider;

    private Texture2D _photo;

    [SerializeField] private GameObject _avatar;
    [SerializeField] private SpawnPlayer _spawnPlayer;
    [SerializeField] private AvatarRotator _avatarRotator;


    [Inject]
    public void Construct()
    {
        Debug.Log("ZENJECT");
        PlayerPrefs.DeleteAll();
    }
    void OnEnable()
    {
        _photoCaptureElement.OnPhotoCaptured.AddListener(PhotoShowed);
        _buttonConfirm.onClick.AddListener(LoadAvatar);
    }
    private void OnDisable()
    {
        _photoCaptureElement.OnPhotoCaptured.RemoveListener(PhotoShowed);
        _buttonConfirm.onClick.RemoveListener(LoadAvatar);
    }
    private void PhotoShowed(Texture2D photo)
    {
        _photo = photo;
    }
    public void SetAvatarToSpawn()
    {
        _spawnPlayer.SetPlayer(_avatar);
    }
    private async void LoadAvatar()
    {
        _avatar = await _avatarHandler.LoadAvatarFromSelfie(_photo);
        _avatarRotator.SetPlayerOnRotation(_avatar);
        if (_assetProvider != null)
        {
            _assetProvider.SetPlayer(_avatar);
        }
        else
        {
            Debug.Log("We cant SetPlayer in assetProvider because it is null");
        }
    }
    public string Texture2DToBase64(Texture2D texture)
    {
        byte[] imageData = texture.EncodeToPNG();
        return Convert.ToBase64String(imageData);
    }
}
