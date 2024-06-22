// https://qiita.com/nkjzm/items/9124a48c1dcba1e7194c
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace nkjzm.Tests
{
    /// <summary>
    /// テスト用のUtilityクラス
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 対象のプレハブを読み込む
        /// </summary>
        /// <param name="fileName">対象プレハブのファイル名（拡張子不要）</param>
        /// <typeparam name="T">対象プレハブの型名</typeparam>
        /// <returns>プレハブの参照（見つからなかった場合はnullを返す）</returns>
        public static T LoadPrefab<T>(string fileName) where T : Object
        {
            var filePath = AssetDatabase.FindAssets($"{fileName} t:Prefab")
                .Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault(str => Path.GetFileNameWithoutExtension(str) == fileName);
            return AssetDatabase.LoadAssetAtPath<T>(filePath);
        }
    }
}
