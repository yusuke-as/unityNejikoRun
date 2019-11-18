using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff;

    public GameObject target;
    public float followSpeed;
    // Start is called before the first frame update
    void Start()
    {
        diff = target.transform.position - transform.position;
    }


    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - diff,
            //0-1の割合
            Time.deltaTime * followSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
