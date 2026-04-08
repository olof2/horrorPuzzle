using UnityEngine;

public class TextureFix : MonoBehaviour
{
    Renderer renderer;

    public void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Vector3 scale = transform.localScale;
        renderer.material.mainTextureScale = new Vector2 (scale.x, scale.z);
    }
}
