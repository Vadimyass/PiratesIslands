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
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && _instantiatedLog == null)
        {
            _instantiatedLog = Instantiate(_logPrefab, _logPrefab.transform.position, Quaternion.identity);
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
            _rigidbody.AddForce(new Vector3(-20, 0, 0));
            LogDown(NextIsland);
            this.enabled = false;
        }
    }
}
