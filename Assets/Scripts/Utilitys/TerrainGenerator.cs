using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Terrain))]
public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] Vector2Int terrainSize; //Terrain�̃T�C�Y
    [SerializeField] float height; //Terrain�̍���
    [SerializeField] float relief; //�n�`�̊��炩��

    float seedX, seedY;

    public void Generate()
    {
        //Terrain�֘A�̃R���|�[�l���g���擾����
        Terrain terrain = GetComponent<Terrain>();
        TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
        TerrainData terrainData = terrain.terrainData;

        //�V�[�h�l��ݒ肷��
        seedX = Random.value * 100f;
        seedY = Random.value * 100f;

        //Terrain�̃T�C�Y��ύX����
        terrainData.size = new Vector3(terrainSize.x, height, terrainSize.y);

        //�n�`�Ɋւ���ϐ���p�ӂ���
        int heightmapSize = terrainData.heightmapResolution; //�n�`�̑傫��
        float[,] heightmap = new float[heightmapSize, heightmapSize]; //�n�`�̃f�[�^(0 �` 1)

        //�n�`��ύX����
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

        //�n�`�̃f�[�^��n��
        terrainData.SetHeights(0, 0, heightmap);

        //�����TerrainData��n��
        terrain.terrainData = terrainData;
        terrainCollider.terrainData = terrainData;
    }
}

/// <summary>
/// �C���X�y�N�^�[�Ɂu�����v�̃{�^�������
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

        if (GUILayout.Button("����"))
        {
            terrainGenerator.Generate();
        }
    }
}
#endif