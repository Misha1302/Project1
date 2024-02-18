#if UNITY_EDITOR
namespace NamehaveCat.Scripts.Debug
{
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEditor;
    using UnityEngine;

    public static class MenuItems
    {
        private const string PrefabName = "LevelBase";

        [MenuItem("Assets/Create/" + PrefabName)]
        [MenuItem("GameObject/" + PrefabName)]
        private static void SpawnPlayer() => SpawnPrefab(PrefabName);

        private static void SpawnPrefab(string prefabName)
        {
            var guids = AssetDatabase.FindAssets(prefabName, null);
            if (guids.Length == 0)
                Thrower.Throw(new NotFoundException($"Prefab with name {prefabName}"));

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            var gmObj = (GameObject)PrefabUtility.InstantiatePrefab(obj);

            gmObj.transform.position = Vector3.zero;
        }
    }
}
#endif