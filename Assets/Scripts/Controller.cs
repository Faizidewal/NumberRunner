using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public class Controller : MonoBehaviour
{
    public Vector3 startPos, endPos;
    public bool Movement;
    public float Speed;
    public List<GameObject> Numbers;
    public int FirstNumber;
    public static Controller Instance;
    public string TotalNumber;
    public int RunningNumber;
    public List<Material> ColorsForNumbers;
    public bool End;
    public List<GameObject> numberinline;



    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Destroy(transform.GetChild(0).gameObject);
        Speed = 1.5f;
        GameObject FirstNumberGame = Instantiate(Numbers[FirstNumber], transform.position, Quaternion.identity);
        FirstNumberGame.transform.parent = transform;
        GetComponentInChildren<Contains>().FirstNumber = FirstNumber;
        FirstNumberGame.transform.tag = "Untagged";
        Movement = true;
    }
    void Update()
    {
        if (Movement)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Movement = true;
            }
            if (Input.GetMouseButton(0))
            {
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                if (Movement)
                {
                    endPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    Vector3 diff = endPos - startPos;
                    transform.position += new Vector3(diff.x, 0, 0) * 360 * Time.smoothDeltaTime;
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.15f, 1.15f), transform.position.y, transform.position.z);
                    startPos = endPos;
                }
            }
        }
        if (End)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Gates")
        {
            if (AudioManager.instance)
            {
                AudioManager.instance.Play("change");
            }
            int i = collision.gameObject.transform.parent.GetComponent<Gates>().NumberToOperate;
            int j = RunningNumber - i;
            if (j > 0)
            {
                ChangeNumber(RunningNumber - i);
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
            else
            {
                Kill();
            }
        }
        float nowtop = 0;
        if (collision.gameObject.tag == "Goal")
        {
            Movement = false;
            End = true;
            CameraMovement.Instance.AllMostOver();
            transform.position = new Vector3(0, 0.8f, transform.position.z);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<Contains>().Gohere(new Vector3(0, nowtop, 0));
                nowtop += 0.5f;
            }
        }
    }
    public void Kill()
    {
        Movement = false;
        int ch = transform.childCount;
        for (int k = 0; k < ch; k++)
        {
            GameObject tempp = transform.GetChild(0).gameObject;
            Destroy(tempp.GetComponent<BoxCollider>());
            Destroy(tempp.GetComponent<Rigidbody>());
            tempp.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();
            tempp.transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
            tempp.transform.parent = null;
        }
        StartCoroutine(Ui.Instance.DeclareFail());
    }
    public void MakeANumber()
    {
       numberinline = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            numberinline.Add(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.childCount; j++)
            {
                if (numberinline[i].transform.localPosition.x < numberinline[j].transform.localPosition.x)
                {
                    GameObject Temp = numberinline[i];
                    numberinline[i] = numberinline[j];
                    numberinline[j] = Temp;
                }
            }
        }
        float nowpos = 0;
        for (int i = 0; i < numberinline.Count; i++)
        {
            numberinline[i].transform.localPosition = new Vector3(nowpos, 0, 0);
            nowpos += 0.35f;
        }
        Justnumber();
    }

    public void Justnumber()
    {
        TotalNumber = "";
        for (int i = 0; i < numberinline.Count; i++)
        {
            TotalNumber += numberinline[i].transform.GetComponent<Contains>().FirstNumber.ToString();
        }
        if (TotalNumber != "")
        {
            RunningNumber = int.Parse(TotalNumber);
        }
    }
    public void ChangeNumber(int i)
    {
        string num = i.ToString();
        for (int k = 0; k < transform.childCount; k++)
        {
            Destroy(transform.GetChild(k).gameObject);
        }
        float nowpos = 0;
        for (int j = 0; j < num.Length; j++)
        {
            GameObject FirstNumberGame = Instantiate(Numbers[int.Parse(num[j].ToString())], transform.position, Quaternion.identity);
            FirstNumberGame.transform.parent = transform;
            FirstNumberGame.GetComponentInChildren<Contains>().FirstNumber = int.Parse(num[j].ToString());
            FirstNumberGame.transform.localPosition = new Vector3(nowpos, 0, 0);
            nowpos += 0.35f;
        }
    }
}
