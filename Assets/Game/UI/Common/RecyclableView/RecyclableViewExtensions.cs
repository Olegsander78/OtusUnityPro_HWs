using UnityEngine;


public static class RecyclableViewExtensions
{
    public static void SetTopAnchor(this RectTransform rectTransform)
    {
        //Saving to reapply after anchoring. Width and height changes if anchoring is change. 
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        //Setting top anchor 
        rectTransform.anchorMin = new Vector2(0.5f, 1);
        rectTransform.anchorMax = new Vector2(0.5f, 1);
        rectTransform.pivot = new Vector2(0.5f, 1);

        //Reapply size
        rectTransform.sizeDelta = new Vector2(width, height);
    }

    public static void SetTopLeftAnchor(this RectTransform rectTransform)
    {
        //Saving to reapply after anchoring. Width and height changes if anchoring is change. 
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        //Setting top anchor 
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);

        //Reapply size
        rectTransform.sizeDelta = new Vector2(width, height);
    }

    public static Vector3[] GetCorners(this RectTransform rectTransform)
    {
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        return corners;
    }

    public static float MaxY(this RectTransform rectTransform)
    {
        return rectTransform.GetCorners()[1].y;
    }

    public static float MinY(this RectTransform rectTransform)
    {
        return rectTransform.GetCorners()[0].y;
    }

    public static float MaxX(this RectTransform rectTransform)
    {
        return rectTransform.GetCorners()[2].x;
    }

    public static float MinX(this RectTransform rectTransform)
    {
        return rectTransform.GetCorners()[0].x;
    }
}