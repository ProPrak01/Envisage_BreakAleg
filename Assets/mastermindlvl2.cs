using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mastermindlvl2 : MonoBehaviour
{
    [SerializeField] private GameObject removeobject1;
    [SerializeField] private GameObject removeobject2;

    public bool[][] dooropenarray;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (dooropenarray[1][1] == true && dooropenarray[3][3] == true)
        {
           
        }
    }
}
