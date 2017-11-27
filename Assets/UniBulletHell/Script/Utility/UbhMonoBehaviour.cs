﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Ubh mono behaviour.
/// </summary>
public abstract class UbhMonoBehaviour : MonoBehaviour
{
    Transform _Transform;
    Renderer _Renderer;
    Rigidbody _Rigidbody;
    Rigidbody2D _Rigidbody2D;

    public Transform transform
    {
        get
        {
            if (_Transform == null) {
                _Transform = GetComponent<Transform>();
            }
            return _Transform;
        }
    }

    public Renderer renderer
    {
        get
        {
            if (_Renderer == null) {
                _Renderer = GetComponent<Renderer>();
            }
            return _Renderer;
        }
    }

    public Rigidbody rigidbody
    {
        get
        {
            if (_Rigidbody == null) {
                _Rigidbody = GetComponent<Rigidbody>();
            }
            return _Rigidbody;
        }
    }

    public Rigidbody2D rigidbody2D
    {
        get
        {
            if (_Rigidbody2D == null) {
                _Rigidbody2D = GetComponent<Rigidbody2D>();
            }
            return _Rigidbody2D;
        }
    }
}
