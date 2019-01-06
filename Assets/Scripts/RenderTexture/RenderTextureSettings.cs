using UnityEngine;

[CreateAssetMenu]
public class RenderTextureSettings : ScriptableObject
{
    public int Width;
    public int Height;
    public int Depth;
    public RenderTextureFormat Format;

    public RenderTexture GetRenderTexture()
    {
        return new RenderTexture(Width, Height, Depth, Format);
    }
}
