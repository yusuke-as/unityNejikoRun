using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    Vector3 objectDiff;

    public GameObject photoTarget;
    public float followSpeed;
    // Start is called before the first frame update
    void Start()
    {
        objectDiff = photoTarget.transform.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            photoTarget.transform.position - objectDiff,
            Time.deltaTime * followSpeed
            ) ;
    }
}
