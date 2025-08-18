using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteckFreeTextures : MonoBehaviour
{
    [SerializeField] private List<Texture> _stackTextures;

    public Texture GetTexture()
    {
        if (_stackTextures.Count == 0) return null;
        Texture result = _stackTextures[_stackTextures.Count - 1];
        _stackTextures.Remove(result);
        return result;
    }
}
