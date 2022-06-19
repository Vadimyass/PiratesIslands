﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Island : MonoBehaviour
{
    Rigidbody _rigidbody;
    [NonSerialized] public Island NextIsland;

    private Rigidbody _logRB;

    private WoddenPlank _instantiatedLog;

    public static Action<Island> LevelOver = delegate { };
    public static Action<WoddenPlank> LogDown = delegate { };

    private float _speed = 0.3f;
    private PlankPool _plankPool;

    [Inject]
    private void Construct( PlankPool plankPool )
    {
        _plankPool = plankPool;
    }
    private void OnEnable()
    {        
        Lean.Touch.LeanTouch.OnFingerDown += CreateLog;
        Lean.Touch.LeanTouch.OnFingerUpdate += IncreaseLog;
        Lean.Touch.LeanTouch.OnFingerUp += StopCreateLog;
        if (NextIsland == null)
        {
            LevelOver(this);
        }
        //InvokeRepeating(nameof(AnimationOfIsland), 1, 0.01f);
    }

    private void OnDisable()
    {
        Lean.Touch.LeanTouch.OnFingerDown -= CreateLog;
        Lean.Touch.LeanTouch.OnFingerUpdate -= IncreaseLog;
        Lean.Touch.LeanTouch.OnFingerUp -= StopCreateLog;
    }

    private void CreateLog(Lean.Touch.LeanFinger finger)
    {
        Debug.Log("Try Try to create Plank");
        _instantiatedLog = _plankPool.GetNextPlank(PlankReferenceData.PlankType.Wood, transform);
        _logRB = _instantiatedLog.Rigidbody;
        _logRB.isKinematic = true;
    }
    

    private void IncreaseLog(Lean.Touch.LeanFinger finger)
    {
        if (finger.Index != -42)
        {
            _instantiatedLog.transform.localScale += Vector3.Lerp(_instantiatedLog.transform.localScale,
                Vector3.up * Time.deltaTime * _speed, 3);
            _instantiatedLog.transform.localPosition += Vector3.up * Time.deltaTime * _speed;
        }
    }
    private void StopCreateLog(Lean.Touch.LeanFinger finger)
    {
        _logRB.isKinematic = false;
        var direction = Vector3.Normalize(NextIsland.transform.position - transform.position);
        _logRB.AddForce(direction*20);//Forcing to the next island
        LogDown(_instantiatedLog);
        enabled = false;
        StartCoroutine(_instantiatedLog.CheckOnFalling(direction));
    }

    private void AnimationOfIsland()
    {
        NextIsland.transform.position = Vector3.MoveTowards(NextIsland.transform.position, new Vector3(NextIsland.transform.position.x, 0, NextIsland.transform.position.z), 0.007f);
        if(NextIsland.transform.position.y == 0)
        {
            CancelInvoke();
        }
    }
}