using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[ExecuteInEditMode]
public class Gates : MonoBehaviour
{
    public int NumberToOperate;
    public TextMeshProUGUI TextOnGate;
    void Start()
    {
      
    }
    public void Update()
    {
        TextOnGate = GetComponentInChildren<TextMeshProUGUI>();
        TextOnGate.text = "-" + NumberToOperate;
    }
}
