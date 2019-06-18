using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using V2;
using System.IO;

[CustomEditor(typeof(EnemyEntity))]
public class EnemyJsonExtractor : Editor
{
    public GUIStyle m_labelStyle;

    private Color m_defaultColor;
    private string m_folderPath;

    private GameObject m_activeObj;
    private EnemyEntity m_activeEntity;


    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        if (m_activeEntity == null)
            SetActiveObject();

        if (GUILayout.Button("Extract Data"))
        {
            string path = EditorUtility.OpenFolderPanel("Set Save Path", "/Data/", "");

            using (StreamWriter writer = new StreamWriter(path + "/EntityData.txt", false))
            {
                string entityData = m_activeEntity.SerializedEntityData();
                writer.Write(entityData);
            }

            using (StreamWriter writer = new StreamWriter(path + "/EnemyData.txt", false))
            {
                string enemyData = m_activeEntity.SerializedEnemyData();
                writer.Write(enemyData);
            }

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        DrawDefaultInspector();
    }

    private void SetActiveObject()
    {
        if(Selection.activeGameObject.CompareTag("Enemy"))
        {
            m_activeObj = Selection.activeGameObject; ;
            m_activeEntity = m_activeObj.GetComponent<EnemyEntity>();
        }
    }
}