using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public class Contains : MonoBehaviour
{
    public int FirstNumber;
    public bool TallPos;
    public Vector3 TallYpos;
    public void Start()
    {
        Material m1 = Controller.Instance.ColorsForNumbers[Random.Range(0, Controller.Instance.ColorsForNumbers.Count)];
        GetComponentInChildren<MeshRenderer>().material = m1;
    }
    public void Update()
    {
        if (TallPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, TallYpos, 5 * Time.deltaTime);
            if (transform.localPosition == TallYpos)
            {
                TallPos = false;
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Number")
        {
            collision.gameObject.transform.tag = "Untagged";
            if (AudioManager.instance)
            {
                AudioManager.instance.Play("Merge");
            }
            collision.gameObject.transform.parent = transform.parent;
            collision.gameObject.transform.parent.GetComponent<Controller>().MakeANumber();
        }
        if (collision.gameObject.tag == "Step")
        {
            GameObject temp = transform.parent.gameObject;
            gameObject.transform.parent = null;
            if (temp.transform.childCount == 0)
            {
                temp.GetComponent<Controller>().End = false;
                StartCoroutine(Ui.Instance.DeclareWin());
            }
        }
        if (collision.gameObject.tag == "RedWall")
        {
            GameObject tempp = gameObject;
            Destroy(tempp.GetComponent<Rigidbody>());
            Destroy(tempp.GetComponent<BoxCollider>());
            GameObject second = tempp.transform.GetChild(0).gameObject;
            second.AddComponent<BoxCollider>();
            second.AddComponent<Rigidbody>();
            GameObject tp = transform.parent.gameObject;
            tp.GetComponent<Controller>().Justnumber();
            tempp.transform.parent = null;
            if(tp.gameObject.transform.childCount==0)
            {
                Controller.Instance.Kill();
            }

        }
    }

    public void Gohere(Vector3 temp)
    {
        TallYpos = temp;
        TallPos = true;

    }
}
