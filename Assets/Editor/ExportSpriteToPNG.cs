using UnityEngine;
using UnityEditor;
using System.IO;

public class ExportSpriteToPNG
{
    [MenuItem("Tools/Export Selected Texture2D to PNG")]
    static void ExportSelectedTexture()
    {
        Object obj = Selection.activeObject;

        if (obj is Texture2D tex)
        {
            // Check if texture is readable
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

            if (importer != null && !importer.isReadable)
            {
                Debug.LogWarning("Texture is not readable. Enabling Read/Write and reimporting...");
                importer.isReadable = true;
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }

            // Create directory if needed
            string exportPath = "Assets/ExportedTextures";
            Directory.CreateDirectory(exportPath);

            // Encode and save as PNG
            byte[] pngData = tex.EncodeToPNG();
            string fileName = tex.name + ".png";
            string fullPath = Path.Combine(exportPath, fileName);
            File.WriteAllBytes(fullPath, pngData);

            AssetDatabase.Refresh();
            Debug.Log("Texture exported to: " + fullPath);
        }
        else
        {
            Debug.LogWarning("Please select a Texture2D asset (like a .psd, .png, etc).");
        }
    }
}
