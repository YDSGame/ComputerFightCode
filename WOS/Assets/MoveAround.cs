using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour {

    public GameObject aroundOB;
    Transform target;
    Vector3 v;
    public float dist;
    public bool d;
    float ddd;
    public Vector3 dds;
    private void Start()
    {
        target = aroundOB.transform;
        v = transform.position - aroundOB.transform.position;
    }
    private void Update()
    {
        //v = Quaternion.AngleAxis(Time.deltaTime * 10f, Vector3.forward) * v;
        //transform.position = aroundOB.transform.position + v;
        if (d)
        {
            //transform.position = target.position + (transform.position - target.position).normalized * dist;            
            transform.RotateAround(target.position, Vector3.down, 90f * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(target.position - transform.position) * Quaternion.EulerAngles(dds);
            //ddd += Time.deltaTime;
            //if (ddd >= 1f)
            //{
            //    d = false;
            //    ddd = 0;
            //}
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
        Debug.Log("ddd");

        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            Debug.Log("ddd");

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Untagged"))
        {
            Debug.Log("ddd");

        }
    }
    //    var q = transform.rotation;
    //    transform.RotateAround(point, Vector3.forward, 20*Time.deltaTime);
    // transform.rotation = q;
    //Alternately you can use a vector and just reposition your object:

    // #pragma strict

    // var point : Vector3;
    // var speed = 20.0;

    //    private var v : Vector3;

    // function Start()
    //    {
    //        v = transform.position - point;
    //    }

    //    function Update()
    //    {
    //        v = Quaternion.AngleAxis(Time.deltaTime * speed, Vector3.forward) * v;
    //        transform.position = point + v;
    //    }
}
