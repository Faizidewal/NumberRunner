using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCam : MonoBehaviour
{
    public GameObject player;
    Vector3 Offset, temp;
    public bool follow;
    public float Xpos;
    void Start()
    {
        follow = true;
        Xpos = transform.position.x;
        Offset = transform.position - player.transform.position;
    }
    void LateUpdate()
    {
        if (follow)
        {
            temp = player.transform.position + Offset;
            temp = new Vector3(Xpos, temp.y, temp.z);
            transform.position = Vector3.MoveTowards(transform.position, temp, 100 * Time.smoothDeltaTime);
        }
    }
}
