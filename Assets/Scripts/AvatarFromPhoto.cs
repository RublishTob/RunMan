using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Core;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class AvatarFromPhoto : MonoBehaviour
{
    [SerializeField] private PhotoCaptureElement _photoCaptureElement;
    [SerializeField] private Button _buttonConfirm;

    private AvatarManager _avatarManager;

    private Texture2D _photo;

    private GameObject _avatar;

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
        if(_photo == null)
        {
            Debug.Log("NO PHOTO");
            return;
        }
        var startTime = Time.time;

        GameObject avatar;

        var avatarProps = new AvatarProperties();
        avatarProps.Id = "67529f7c9ae073e982e0934c";
        avatarProps.Partner = "runtemp";
        avatarProps.BodyType = BodyType.FullBody;
        avatarProps.Base64Image = Texture2DToBase64(_photo);
        avatarProps.Assets = new();

        var avatarResponse = await _avatarManager.CreateAvatarAsync(avatarProps);
        avatar = avatarResponse.AvatarObject;

        if (avatar == null)
        {
            Debug.Log("CANT LOAD AVATAR");
        }


        SDKLogger.Log(nameof(AvatarFromPhoto), $"AVATAR LOADED {Time.time - startTime:F2}s");
        _avatar = avatar;
    }
    public string Texture2DToBase64(Texture2D texture)
    {
        byte[] imageData = texture.EncodeToPNG();
        return Convert.ToBase64String(imageData);
    }
}
