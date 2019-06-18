using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using V2;
using System.IO;

[CustomEditor(typeof(PlayerEntity))]
public class PlayerJsonExtractor : Editor
{
    public GUIStyle m_labelStyle;

    private Color m_defaultColor;
    private string m_folderPath;

    private GameObject m_activeObj;
    private PlayerEntity m_activeEntity;


    private void OnEnable()
    {
        SetActiveObject();
    }

    public override void OnInspectorGUI()
    {
        if (m_activeEntity == null || !m_activeEntity.CompareTag("Player"))
            SetActiveObject();

        if (GUILayout.Button("Extract Data"))
        {
            string path = EditorUtility.OpenFolderPanel("Set Save Path", "/Data/", "");

            using (StreamWriter writer = new StreamWriter(path + "/EntityData.txt", false))
            {
                string entityData = m_activeEntity.SerializedEntityData();
                writer.Write(entityData);
            }

            using (StreamWriter writer = new StreamWriter(path + "/PlayerData.txt", false))
            {
                string playerData = m_activeEntity.SerializedPlayerData();
                writer.Write(playerData);
            }

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        DrawDefaultInspector();
    }

    private void SetActiveObject()
    {
        m_activeObj = GameObject.FindGameObjectWithTag("Player"); ;
        m_activeEntity = m_activeObj.GetComponent<PlayerEntity>();
    }
}