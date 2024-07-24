using UnityEngine;
using UnityEngine.UI;

public class TextureToRenderTexture : MonoBehaviour
{
    public Texture2D sourceTexture; // 帶有alpha通道的源Texture
    public RenderTexture destinationRenderTexture; // 目標RenderTexture

    public RawImage rwimg;
    void Start()
    {
        // 確保源Texture和目標RenderTexture都存在
        if (sourceTexture == null)
        {
            Debug.LogError("Source texture not assigned!");
            return;
        }

        if (destinationRenderTexture == null)
        {
            Debug.LogError("Destination render texture not assigned!");
            return;
        }

        // 複製源Texture到目標RenderTexture
        //Graphics.Blit(sourceTexture, destinationRenderTexture);

        NewRenderTex();
    }

    void NewRenderTex(){
        RenderTexture m_rt = new RenderTexture(sourceTexture.width, sourceTexture.height, 0, RenderTextureFormat.ARGB32);
        m_rt.filterMode = FilterMode.Bilinear;
        m_rt.useMipMap = false;

        Graphics.Blit(sourceTexture, m_rt);

        rwimg.texture = m_rt;
    }
}
