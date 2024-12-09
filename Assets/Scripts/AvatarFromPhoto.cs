using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Core;
using ReadyPlayerMe.Samples.AvatarCreatorElements;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class AvatarFromPhoto : MonoBehaviour
{
    [SerializeField] private PhotoCaptureElement _photoCaptureElement;
    [SerializeField] private AvatarHandler _avatarHandler;
    [SerializeField] private Button _buttonConfirm;

    private AvatarManager _avatarManager;

    private Texture2D _photo;

    [SerializeField] private GameObject _avatar;

    private readonly CancellationTokenSource cancellationTokenSource = new();

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        _avatarManager = new AvatarManager(token: cancellationTokenSource.Token);

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
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
    }
    private void PhotoShowed(Texture2D photo)
    {
        _photo = photo;
    }
    private async void LoadAvatar()
    {
        _avatar = await _avatarHandler.LoadAvatarFromSelfie(_photo);
        AssetProvider.Instance.SetPlayer(_avatar);
    }
    public string Texture2DToBase64(Texture2D texture)
    {
        byte[] imageData = texture.EncodeToPNG();
        return Convert.ToBase64String(imageData);
    }
}
