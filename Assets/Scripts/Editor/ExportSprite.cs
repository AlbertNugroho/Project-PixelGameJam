using UnityEngine;
using UnityEditor;
using System.IO;
public class ExportSprite : MonoBehaviour
{
    [MenuItem("Tools/Export Selected Sprite to PNG")]
    public static void ExportSelectedSpriteToPNG()
    {
        Object obj = Selection.activeObject;

        if (!(obj is Sprite sprite))
        {
            Debug.LogWarning("Please select a Sprite in the Project window.");
            return;
        }

        Texture2D sourceTex = sprite.texture;

        // Calculate pixel rect
        Rect rect = sprite.rect;
        int width = (int)rect.width;
        int height = (int)rect.height;

        Texture2D newTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        newTex.filterMode = FilterMode.Point;

        Color[] pixels = sourceTex.GetPixels(
            (int)rect.x,
            (int)rect.y,
            width,
            height
        );
        newTex.SetPixels(pixels);
        newTex.Apply();

        // Encode as PNG
        byte[] pngData = newTex.EncodeToPNG();
        if (pngData == null)
        {
            Debug.LogError("Failed to encode PNG.");
            return;
        }

        // Save
        string path = "Assets/ExportedSprites";
        Directory.CreateDirectory(path);

        string safeName = sprite.name.Replace(" ", "_");
        string fullPath = Path.Combine(path, safeName + ".png");
        File.WriteAllBytes(fullPath, pngData);

        AssetDatabase.Refresh();
        Debug.Log("Sprite exported to PNG: " + fullPath);
    }
}
