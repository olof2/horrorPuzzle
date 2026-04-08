using UnityEngine;

public class TextureFix : MonoBehaviour
{
    public Renderer renderer;

    private void Update()
    {
        Vector3 scale = transform.localScale;
        renderer.material.mainTextureScale = new Vector2 (scale.x, scale.z);
    }
}
