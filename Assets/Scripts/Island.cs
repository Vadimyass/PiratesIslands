using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Island : MonoBehaviour
{
    Rigidbody _rigidbody;
    [NonSerialized] public GameObject NextIsland;

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
        _logRB.AddForce((NextIsland.transform.position - transform.position)*20);//Forcing to the next island
        LogDown(_instantiatedLog);
        enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Character character))
        {
            character._recentIsland = new Vector3(transform.position.x, 0.24f,transform.position.z);
            character._nextIsland = new Vector3(NextIsland.transform.position.x, 0.24f,NextIsland.transform.position.z);
            character._recentIslandRef = this;

        }

    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.TryGetComponent(out Character character))
    //    {
    //        if (character.IsGeneral)
    //        {
    //            _instantiatedLog = null;
    //            enabled = false;
    //        }
    //    }
    //}
    private void AnimationOfIsland()
    {
        NextIsland.transform.position = Vector3.MoveTowards(NextIsland.transform.position, new Vector3(NextIsland.transform.position.x, 0, NextIsland.transform.position.z), 0.007f);
        if(NextIsland.transform.position.y == 0)
        {
            CancelInvoke();
        }
    }
}
