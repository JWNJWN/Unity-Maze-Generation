using UnityEngine;
using System.Collections;
using System;

public static class ImageUtil {

    public static Texture2D GetImage(string imgPath)
    {
        return Resources.Load(imgPath) as Texture2D;
    }

    
}
