using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Splines;
using System.Linq;
public class ExplosionRemove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartShooting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void StartShooting()
    {
        IEnumerator coroutine = Shooting();
        StartCoroutine(coroutine);
    }
    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
