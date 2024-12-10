using ReadyPlayerMe.AvatarCreator;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    [SerializeField] private PhotoCaptureElement _photoCaptureElement;

    [SerializeField] private Image _material;
    [SerializeField] private Button _button;

    void Start()
    {
        //_photoCaptureElement.OnPhotoCaptured.AddListener(PhotoShowed);
        _button.onClick.AddListener(Hide);
    }
    private void OnDestroy() 
    {
        //_photoCaptureElement.OnPhotoCaptured.RemoveListener(PhotoShowed);
        _button.onClick.RemoveListener(Hide);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }

    public void PhotoShowed(Texture2D image)
    {
        if (image == null)
        {
            Debug.Log("NOTHING TO PICTURE");
        }
        Rect rect = new Rect(0, 0, 640, 480);
        Sprite sprite = Sprite.Create(image, rect, Vector2.zero);
        _material.sprite = sprite;
        gameObject.SetActive(true);
        Show();
    }

}
