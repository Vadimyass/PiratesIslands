using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    Rigidbody _rigidbody;
    [NonSerialized] public GameObject NextIsland;

    private GameObject _logPrefab;

    private GameObject _instantiatedLog;

    public static Action<GameObject> LogDown = delegate { };

    private float _speed = 0.3f;

    private void Start()
    {
        _logPrefab = Resources.Load<GameObject>("Prefabs/Log");
    }
    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0) && _instantiatedLog == null)
        {
            _instantiatedLog = Instantiate(_logPrefab,Vector3.zero, Quaternion.Euler(transform.rotation.eulerAngles),this.transform);
            _instantiatedLog.transform.localPosition = _logPrefab.transform.position;
            _rigidbody = _instantiatedLog.GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
            print(_instantiatedLog.name);
        }

        if (Input.GetMouseButton(0))
        {
            _instantiatedLog.transform.localScale += Vector3.Lerp(_instantiatedLog.transform.localScale, Vector3.up * Time.deltaTime * _speed, 3);
            _instantiatedLog.transform.localPosition += Vector3.up * Time.deltaTime * _speed;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce((NextIsland.transform.position - transform.position)*30);//Forcing to the next island
            print(NextIsland.transform.position - transform.position);
            LogDown(NextIsland);
            enabled = false;
        }
    }
}
