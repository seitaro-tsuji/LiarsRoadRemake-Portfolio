using UnityEngine;

public static class GameUtils
{
    public static bool IsInLayerMask(Collider2D collider, LayerMask layerMask)
    {
        return (layerMask.value & (1 << collider.gameObject.layer)) != 0;
    }

    public static LayerMask GetLayerMask(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);

        if(layer == -1)
        {
            Debug.LogError($"Layer{layerName}‚ª‘¶Ý‚µ‚Ü‚¹‚ñB");
        }

        return 1 << layer;
    }
}
