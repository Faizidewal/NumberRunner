using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    Vector3 Offset, temp;
    public bool follow;
    public static CameraMovement Instance;
    public float Xpos;
    public bool SideView;
    public GameObject SideCam;
    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
    }
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
            transform.position = Vector3.MoveTowards(transform.position, temp, 5 * Time.smoothDeltaTime);
        }
        if(SideView)
        {
            transform.position = Vector3.MoveTowards(transform.position,SideCam.transform.position,5*Time.smoothDeltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation,SideCam.transform.rotation,5*Time.smoothDeltaTime);
        }
      
    }
    public void AllMostOver()
    {
        follow = false;
        SideView = true;
    }
}
