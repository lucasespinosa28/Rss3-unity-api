using UnityEngine;
using UnityEngine.UIElements;

public class ImageFromUrl : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ImageFromUrl, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }

    readonly Texture2D m_Texture2D;
    readonly Image m_Image;
    private static readonly Texture _Texture = null;

    public ImageFromUrl() : this(_Texture)
    {

    }

    public ImageFromUrl(Texture texture)
    {
        m_Texture2D = Texture2D.CreateExternalTexture(texture.width, texture.height, TextureFormat.RGB24, false, false, texture.GetNativeTexturePtr());
        m_Image = new Image
        {
            image = m_Texture2D
        };
        Add(m_Image);
    }
}
