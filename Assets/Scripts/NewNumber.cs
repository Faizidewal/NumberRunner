using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewNumber : MonoBehaviour
{
    public int FirstNumber;
    void Start()
    {
        Destroy(transform.GetChild(0).gameObject);
        GameObject FirstNumberGame = Instantiate(Controller.Instance.Numbers[FirstNumber], transform.position, Quaternion.identity);
        FirstNumberGame.transform.parent = transform;
        GetComponentInChildren<Contains>().FirstNumber = FirstNumber;
    }
}
