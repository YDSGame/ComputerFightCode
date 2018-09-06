using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    public GameObject mychil;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        print(h);
        Quaternion my = Quaternion.LookRotation(Vector3.forward);
        transform.Translate(Vector3.forward * v * 10 * Time.deltaTime);
        mychil.transform.Rotate(Vector3.up * h * 100 * Time.deltaTime);

    }
}
