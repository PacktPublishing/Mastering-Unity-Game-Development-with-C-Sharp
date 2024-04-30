using UnityEditor;
using UnityEngine;

public class CustomMeshPostprocessor : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        ModelImporter importer = assetImporter as ModelImporter;
        importer.isReadable = false; // Set Read/Write option to disabled
        importer.meshCompression = ModelImporterMeshCompression.Medium; // Set Mesh Compression to Medium
    }
}
