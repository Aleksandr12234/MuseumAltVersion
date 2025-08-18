using UnityEngine;

public class SetBookTexture : MonoBehaviour
{
    private Texture _texture;

    private MaterialPropertyBlock _sharedProps;
    private Renderer _renderer;

    void Start()
    {
        _texture = transform.parent.gameObject.GetComponent<SteckFreeTextures>().GetTexture();
        if (_texture == null) return;

        _renderer = GetComponent<Renderer>();
        _sharedProps = new MaterialPropertyBlock();

        _renderer.GetPropertyBlock(_sharedProps);
        _sharedProps.SetTexture("_BaseMap", _texture);
        _renderer.SetPropertyBlock(_sharedProps);
    }

    void OnDestroy()
    {
        if (_sharedProps == null) return;
        _sharedProps.Clear();
        _sharedProps = null;
    }
}
