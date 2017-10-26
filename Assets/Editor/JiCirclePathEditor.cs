﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JiCirclePath))]
public class JiCirclePathEditor : Editor
{
    JiCirclePath _targetScript;

    public override void OnInspectorGUI()
    {
        _targetScript = (JiCirclePath)target;

        base.OnInspectorGUI();

        // Path visible property
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Always Visible");
        _targetScript.m_alwaysVisable = EditorGUILayout.Toggle(_targetScript.m_alwaysVisable);
        EditorGUILayout.EndHorizontal();

        // Path name property.
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Path Name");
        _targetScript.m_PathName = EditorGUILayout.TextField(_targetScript.m_PathName);
        if (string.IsNullOrEmpty(_targetScript.m_PathName))
        {
            _targetScript.m_PathName = _targetScript.transform.parent.name;
        }
        EditorGUILayout.EndHorizontal();

        if (string.IsNullOrEmpty(_targetScript.m_PathName))
        {
            _targetScript.m_PathName = "New Path";
        }

        // Exploration segment count control.
        EditorGUILayout.BeginHorizontal();
        _targetScript.m_CtrolNodeCount = Mathf.Max(2, EditorGUILayout.IntField("Control Node Count", _targetScript.m_CtrolNodeCount));
        EditorGUILayout.EndHorizontal();

        // Remove node?
        if (_targetScript.m_CtrolNode.Count > _targetScript.m_CtrolNodeCount)
        {
            if (EditorUtility.DisplayDialog("Remove path node?",
                "Shortening the node list will permantently destory parts of your path. This operation cannot be undone.",
                "OK", "Cancel"))
            {
                int removeCount = _targetScript.m_CtrolNode.Count - _targetScript.m_CtrolNodeCount;
                _targetScript.m_CtrolNode.RemoveRange(_targetScript.m_CtrolNode.Count - removeCount, removeCount);
                Undo.RecordObject(_targetScript, "Remove point");
            }
            else
            {
                _targetScript.m_CtrolNodeCount = _targetScript.m_CtrolNode.Count;
            }
        }

        // Add node?
        if (_targetScript.m_CtrolNode.Count < _targetScript.m_CtrolNodeCount)
        {
            for (int i = 0; i < _targetScript.m_CtrolNodeCount - _targetScript.m_CtrolNode.Count; i++)
            {
                _targetScript.m_CtrolNode.Add(Vector3.zero);
                Undo.RecordObject(_targetScript, "Add node");
            }
        }

        // Display control nodes
        EditorGUI.indentLevel = 2;
        _targetScript.m_CtrolNode[0] = EditorGUILayout.
            Vector3Field("Start Node: ", _targetScript.m_CtrolNode[0]);
        int endIndex = _targetScript.m_CtrolNodeCount - 1;
        _targetScript.m_CtrolNode[endIndex] = EditorGUILayout.
            Vector3Field("End Node: ", _targetScript.m_CtrolNode[endIndex]);


        // Save path data
        if (GUILayout.Button("Generate Data"))
        {
            SavePathData(_targetScript);
        }

        // Load path data
        if (GUILayout.Button("Load Data"))
        {
            LoadPathData(_targetScript);
        }

        if(GUILayout.Button("GenerateCircle"))
        {
            _targetScript.GenerateCirclePath();
            Undo.RecordObject(_targetScript, "GenerateCircleData");
        }

        if (GUI.changed)
        {
            Undo.RecordObject(_targetScript, "Change the path");
        }
    }

    void OnSceneGUI()
    {

        GUIStyle style = new GUIStyle();
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;

        if (_targetScript == null) return;
        if (_targetScript.m_CtrolNode == null) return;

        if (_targetScript.m_CtrolNode.Count > 0)
        {
            //path begin and end labels:
            Handles.Label(_targetScript.m_CtrolNode[0], "'" + _targetScript.m_PathName + "' Begin", style);
            Handles.Label(_targetScript.m_CtrolNode[_targetScript.m_CtrolNode.Count - 1], "'" + _targetScript.m_PathName + "' End", style);

            //node handle display:       
            _targetScript.m_CtrolNode[0] = Handles.PositionHandle(_targetScript.m_CtrolNode[0], Quaternion.identity);
            Undo.RecordObject(_targetScript, "Change path node");

            int lastIndex = _targetScript.m_CtrolNodeCount - 1;
            _targetScript.m_CtrolNode[lastIndex] = 
                Handles.PositionHandle(_targetScript.m_CtrolNode[lastIndex], Quaternion.identity);
            Undo.RecordObject(_targetScript, "Change path node");
        }
    }


    private void SavePathData(JiPath pathScript)
    {
        string path = EditorUtility.SaveFilePanel("Save Path Data", "Assets/", pathScript.m_PathName + "_Path", "asset");
        if (string.IsNullOrEmpty(path)) return;

        path = FileUtil.GetProjectRelativePath(path);

        JiPathData data = new JiPathData(pathScript.m_PathName, new List<Vector3>(pathScript.m_CtrolNode));

        AssetDatabase.CreateAsset(data, path);
        AssetDatabase.SaveAssets();
    }


    private void LoadPathData(JiPath pathScript)
    {
        string path = EditorUtility.OpenFilePanel("Load data", "Assets/", "asset");
        if (string.IsNullOrEmpty(path)) return;

        path = FileUtil.GetProjectRelativePath(path);

        var data = AssetDatabase.LoadAssetAtPath(path, typeof(JiPathData)) as JiPathData;
        if (data == null) return;

        pathScript.m_PathName = data.m_pathName;
        pathScript.m_CtrolNode.Clear();

        foreach (var node in data.m_controlPoints)
            pathScript.m_CtrolNode.Add(node);
        pathScript.m_CtrolNodeCount = pathScript.m_CtrolNode.Count;

    }

}
