using UnityEngine;

public class Background : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private float distance;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        meshRenderer.material.mainTextureOffset = (Vector2)transform.position / transform.localScale / distance;
    }
}
