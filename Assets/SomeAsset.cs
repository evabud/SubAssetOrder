using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class SomeAsset : ScriptableObject
{
    public List<SomeSubAsset> subassets;

    [ContextMenu("Clear all subassets")]
    void ClearAllSubs()
    {
        var listOfAllAssets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this));
        if (listOfAllAssets.Length > 1)
        {
            foreach (var o in listOfAllAssets)
            {
                if (o && !AssetDatabase.IsMainAsset(o))
                    AssetDatabase.RemoveObjectFromAsset(o);
            }
        }

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
    
    [ContextMenu("Add 10 subassets")]
    void Add10Subassets()
    {
        if(subassets == null)
            subassets = new List<SomeSubAsset>();
        
        for (int i = 0; i < 10; i++)
        {
            var so = ScriptableObject.CreateInstance<SomeSubAsset>();
            so.myIndex = i;
            so.name = "SubAsset " + i;
            AssetDatabase.AddObjectToAsset(so, this);
            subassets.Add(so);
        }

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

    [ContextMenu("Remove and re-add all subassets")]
    void RemoveAndReAddAssets()
    {
        var currentSubAssets = new List<SomeSubAsset>(this.subassets);
        foreach (var so in this.subassets)
        {
            AssetDatabase.RemoveObjectFromAsset(so);
        }
        AssetDatabase.SaveAssets();

        foreach (var so in currentSubAssets)
        {
            AssetDatabase.AddObjectToAsset(so, this);
        }
        AssetDatabase.SaveAssets();
    }
}
