﻿using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    public Coroutine StartExternalCoroutine(IEnumerator enumerator)
    {
        return StartCoroutine(enumerator);
    }

    public void StopExternalCoroutine(IEnumerator enumerator)
    {
        StopCoroutine(enumerator);
    }
}