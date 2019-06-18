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

    private FolderStatus m_folderStatus = FolderStatus.NONE;
    private string m_folderPath;

    private GameObject m_activeObj;
    private PlayerEntity m_activeEntity;


    private void OnEnable() 
    {
        SetActiveObject();
    }

    public override void OnInspectorGUI()
    {
        SetGUIStyles();
        FStatus();

        if(m_activeEntity == null || !m_activeEntity.CompareTag("Player"))
            SetActiveObject();

        if(m_folderStatus == FolderStatus.NONE)
        {
            if(GUILayout.Button("Set Extraction Data"))
            {
                Debug.Log("Set Folder");
                m_folderPath = EditorUtility.OpenFolderPanel("Set Extraction Folder", "", "");

                if(m_folderPath.Length > 0)
                    m_folderStatus = FolderStatus.SET;
            }
        }
        else
        {
            GUILayout.TextField("Current Path:");
            GUILayout.TextField(m_folderPath);
            if(GUILayout.Button("Extract Data"))
            {
                using(StreamWriter writer = new StreamWriter(m_folderPath + "/EntityData.txt", false))
                {
                    string entityData = m_activeEntity.SerializedEntityData();
                    writer.Write(entityData);
                }

                using(StreamWriter writer = new StreamWriter(m_folderPath + "/PlayerData.txt", false))
                {
                    string playerData  = m_activeEntity.SerializedPlayerData();
                    writer.Write(playerData);
                }
            }
            
            if(GUILayout.Button("Reset Folder Path"))
            {
                m_folderPath = null;
                m_folderStatus = FolderStatus.NONE;
            }
        }

        DrawDefaultInspector();
    }

    private void SetActiveObject()
    {
        m_activeObj = GameObject.FindGameObjectWithTag("Player");;
        m_activeEntity = m_activeObj.GetComponent<PlayerEntity>();
    }
    private void SetGUIStyles()
    {
        m_labelStyle = new GUIStyle();
        GUI.skin.label = m_labelStyle;

        if(m_defaultColor == null)
            m_defaultColor = GUI.backgroundColor;
    }

    private void FStatus()
    {
        if(m_folderStatus == FolderStatus.NONE)
        {
            GUI.backgroundColor = Color.red;
            GUILayout.TextField("FOLDER NOT SET");
            GUI.backgroundColor = Color.white;
        }
        else
        {
            GUI.backgroundColor = Color.green;
            GUILayout.TextField("FOLDER SET");
            GUI.backgroundColor = Color.white;
        }
    }
}

public enum FolderStatus
{
    SET,
    NONE
}

