using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Terrain))]
public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] Vector2Int terrainSize; //Terrainのサイズ
    [SerializeField] float height; //Terrainの高さ
    [SerializeField] float relief; //地形の滑らかさ

    float seedX, seedY;

    public void Generate()
    {
        //Terrain関連のコンポーネントを取得する
        Terrain terrain = GetComponent<Terrain>();
        TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
        TerrainData terrainData = terrain.terrainData;

        //シード値を設定する
        seedX = Random.value * 100f;
        seedY = Random.value * 100f;

        //Terrainのサイズを変更する
        terrainData.size = new Vector3(terrainSize.x, height, terrainSize.y);

        //地形に関する変数を用意する
        int heightmapSize = terrainData.heightmapResolution; //地形の大きさ
        float[,] heightmap = new float[heightmapSize, heightmapSize]; //地形のデータ(0 〜 1)

        //地形を変更する
        for (int y = 0; y < heightmapSize; y++)
        {
            for (int x = 0; x < heightmapSize; x++)
            {
                float sampleX = seedX + x / relief;
                float sampleY = seedY + y / relief;
                float noise = Mathf.PerlinNoise(sampleX, sampleY);

                heightmap[x, y] = noise;
            }
        }

        //地形のデータを渡す
        terrainData.SetHeights(0, 0, heightmap);

        //作ったTerrainDataを渡す
        terrain.terrainData = terrainData;
        terrainCollider.terrainData = terrainData;
    }
}

/// <summary>
/// インスペクターに「生成」のボタンを作る
/// </summary>
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainGenerator terrainGenerator = target as TerrainGenerator;

        base.OnInspectorGUI();

        EditorGUILayout.Space();

        if (GUILayout.Button("生成"))
        {
            terrainGenerator.Generate();
        }
    }
}
#endif