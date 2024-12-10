using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    [SerializeField] private RawImage _image;
    [SerializeField] private ChangeImage _changeImage;

    private WebCamTexture webCamTexture;

    public void StartWeb()
    {
        webCamTexture = new WebCamTexture();

        if (!webCamTexture.isPlaying)
            webCamTexture.Play();

        _image.texture = webCamTexture;
    }

    public void TakePhoto()
    {
        _texture = new Texture2D(webCamTexture.width, webCamTexture.height);
        _texture.SetPixels(webCamTexture.GetPixels());
        _changeImage.PhotoShowed(_texture);
    }



}
