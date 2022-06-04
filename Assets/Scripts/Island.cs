using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Island : MonoBehaviour
{
    Rigidbody _rigidbody;
    [NonSerialized] public GameObject NextIsland;

    [SerializeField] private GameObject _logPrefab;
    [SerializeField] private Rigidbody _logRB;

    [SerializeField] public GameObject _instantiatedLog;

    public static Action<Island> LevelOver = delegate { };
    public static Action<GameObject> LogDown = delegate { };

    private float _speed = 0.3f;

    [Inject]
    private void Construct(WoddenLog logPrefab, Rigidbody logRB)
    {
        _logPrefab = logPrefab.gameObject;
        _logRB = logRB;
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
        Debug.Log("Touched screen");
        _instantiatedLog = Instantiate(_logPrefab,Vector3.zero, Quaternion.Euler(transform.rotation.eulerAngles),this.transform);
        _instantiatedLog.transform.localPosition = _logPrefab.transform.position;
        _logRB = _instantiatedLog.GetComponent<Rigidbody>();
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
        Debug.Log("Stop Touching screen");
        _logRB.isKinematic = false;
        _logRB.AddForce((NextIsland.transform.position - transform.position)*20);//Forcing to the next island
        LogDown(_instantiatedLog);
        enabled = false;
    }
    /*void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0) && _instantiatedLog == null)
        {
            _instantiatedLog = Instantiate(_logPrefab,Vector3.zero, Quaternion.Euler(transform.rotation.eulerAngles),this.transform);
            _instantiatedLog.transform.localPosition = _logPrefab.transform.position;
            _logRB.isKinematic = true;
        }

        if (Input.GetMouseButton(0))
        {
            _instantiatedLog.transform.localScale += Vector3.Lerp(_instantiatedLog.transform.localScale, Vector3.up * Time.deltaTime * _speed, 3);
            _instantiatedLog.transform.localPosition += Vector3.up * Time.deltaTime * _speed;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _logRB.isKinematic = false;
            _logRB.AddForce((NextIsland.transform.position - transform.position)*20);//Forcing to the next island
            LogDown(_instantiatedLog);
            enabled = false;
        }
    }*/

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
