﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PlayerProperty))]
public class PlayerPropertyEditor : Editor
{
    PlayerProperty _targetScript;


    public override void OnInspectorGUI()
    {
        _targetScript = (PlayerProperty)target;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("God Mode");
        _targetScript.m_tgm = EditorGUILayout.Toggle(_targetScript.m_tgm);
        EditorGUILayout.EndHorizontal();

        #region movement
        EditorGUILayout.PrefixLabel("Movement");

        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Vertical Speed");
        _targetScript.m_verticalSpeed = EditorGUILayout.FloatField(_targetScript.m_verticalSpeed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Slow Vertical Speed");
        _targetScript.m_slowVerticalSpeed = EditorGUILayout.FloatField(_targetScript.m_slowVerticalSpeed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Horizontal Speed");
        _targetScript.m_horizontalSpeed = EditorGUILayout.FloatField(_targetScript.m_horizontalSpeed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Slow Horizontal Speed");
        _targetScript.m_slowHorizontalSpeed = EditorGUILayout.FloatField(_targetScript.m_slowHorizontalSpeed);
        EditorGUILayout.EndHorizontal();



        EditorGUI.indentLevel--;
        #endregion

        #region shoot

        EditorGUILayout.PrefixLabel("Shoot");

        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Shoot Interval");
        _targetScript.m_shootInterval = EditorGUILayout.FloatField(_targetScript.m_shootInterval);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Bullet Speed");
        _targetScript.m_bulletSpeed = EditorGUILayout.FloatField(_targetScript.m_bulletSpeed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Bullet Damage");
        _targetScript.m_bulletDamage = EditorGUILayout.IntField(_targetScript.m_bulletDamage);
        EditorGUILayout.EndHorizontal();

        EditorGUI.indentLevel--;
        #endregion

        #region state
        EditorGUILayout.PrefixLabel("State");

        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Player State");
        _targetScript.m_playerState = (PlayerProperty.PlayerStateType)EditorGUILayout.EnumPopup(_targetScript.m_playerState);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Player Move State");
        _targetScript.m_playerMoveState = (PlayerProperty.PlayerMoveType)EditorGUILayout.EnumPopup(_targetScript.m_playerMoveState);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Player Health");
        _targetScript.m_playerHealth = EditorGUILayout.IntField(_targetScript.m_playerHealth);
        EditorGUILayout.EndHorizontal();


        EditorGUI.indentLevel--;

        #endregion
                           
        #region Reference
        EditorGUILayout.PrefixLabel("Reference");

        EditorGUI.indentLevel++;

        _targetScript.m_spriteReference = EditorGUILayout.ObjectField("Player SpriteRenderer", _targetScript.m_spriteReference, typeof(SpriteRenderer), true) as SpriteRenderer;

        EditorGUI.indentLevel--;
        #endregion
    }
}
