using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mastermindlvl2 : MonoBehaviour
{
    public Material redcolor;

    [SerializeField] private GameObject removeobject1;
    [SerializeField] private GameObject removeobject2;
    public GameObject door;
    public bool[][] dooropenarray;
    public GameObject[] slab;
    public int[] k  = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    // Start is called before the first frame update
    public int j=0;
    void Start()
    {
        slab[11] = GameObject.FindGameObjectWithTag("11");
        slab[12] = GameObject.FindGameObjectWithTag("12");
        slab[13] = GameObject.FindGameObjectWithTag("13");
        slab[14] = GameObject.FindGameObjectWithTag("14");
        slab[15] = GameObject.FindGameObjectWithTag("15");
        slab[16] = GameObject.FindGameObjectWithTag("16");
        slab[21] = GameObject.FindGameObjectWithTag("21");
        slab[22] = GameObject.FindGameObjectWithTag("22");
        slab[23] = GameObject.FindGameObjectWithTag("24");
        slab[25] = GameObject.FindGameObjectWithTag("25");
        slab[26] = GameObject.FindGameObjectWithTag("26");
        slab[31] = GameObject.FindGameObjectWithTag("31");
        slab[32] = GameObject.FindGameObjectWithTag("32");
        slab[33] = GameObject.FindGameObjectWithTag("33");
        slab[34] = GameObject.FindGameObjectWithTag("34");
        slab[35] = GameObject.FindGameObjectWithTag("35");
        slab[36] = GameObject.FindGameObjectWithTag("36");
        slab[41] = GameObject.FindGameObjectWithTag("41");
        slab[42] = GameObject.FindGameObjectWithTag("42");
        slab[43] = GameObject.FindGameObjectWithTag("43");
        slab[44] = GameObject.FindGameObjectWithTag("44");
        slab[45] = GameObject.FindGameObjectWithTag("45");
        slab[46] = GameObject.FindGameObjectWithTag("46");
        

    }
    public bool IsTagNotPresent(string tag)
    {
        // Find all GameObjects with the specified tag.
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        // Check if no GameObjects with the specified tag were found.
        return objectsWithTag.Length == 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (dooropenarray[4][3] == true && dooropenarray[1][3] == true)
        {
            slab[43].GetComponent<Renderer>().material = redcolor;
           // Destroy(slab[43]);
          //  Destroy(slab[43]);
           // k[23] = k[24] = 1;
            //door.GetComponent<dooropen>().i = true;

        }
        else if(dooropenarray[2][4] == true && dooropenarray[3][1] == true)
        {
            Destroy(slab[24]);
            Destroy(slab[31]);
            k[0] =k[1]= 1;
        }
        else if (dooropenarray[2][5] == true && dooropenarray[1][2] == true)
        {
            Destroy(slab[25]);
            Destroy(slab[12]);
            k[2] = k[3] = 1;

        }
        else if (dooropenarray[3][4] == true && dooropenarray[1][5] == true)
        {
            Destroy(slab[43]);
            Destroy(slab[43]);
            k[4] = k[5] = 1;

        }
        else if (dooropenarray[4][2] == true && dooropenarray[3][5] == true)
        {
            Destroy(slab[42]);
            Destroy(slab[35]);
            k[6] = k[7] = 1;

        }
        else if (dooropenarray[4][1] == true && dooropenarray[3][6] == true)
        {
            Destroy(slab[41]);
            Destroy(slab[36]);
            k[8] = k[9] = 1;

        }
        else if (dooropenarray[4][4] == true && dooropenarray[4][5] == true)
        {
            Destroy(slab[44]);
            Destroy(slab[45]);
            k[10] = k[11] = 1;

        }
        else if (dooropenarray[1][6] == true && dooropenarray[2][2] == true)
        {
            Destroy(slab[16]);
            Destroy(slab[22]);
            k[12] = k[13] = 1;

        }
        else if (dooropenarray[2][3] == true && dooropenarray[2][6] == true)
        {
            Destroy(slab[23]);
            Destroy(slab[26]);
            k[14] = k[15] = 1;

        }
        else if (dooropenarray[1][1] == true && dooropenarray[3][3] == true)
        {
            Destroy(slab[11]);
            Destroy(slab[33]);
            k[16] = k[17] = 1;

        }
        else if (dooropenarray[3][2] == true && dooropenarray[2][1] == true)
        {
            Destroy(slab[32]);
            Destroy(slab[21]);
            k[18] = k[19] = 1;

        }
        else if (dooropenarray[1][4] == true && dooropenarray[4][6] == true)
        {
            Destroy(slab[14]);
            Destroy(slab[46]);
            k[20] = k[21] = 1;

        }
        for(int m = 0; m < 24; m++)
        {
            if (k[m] == 0)
            {
                break;
            }
            if( m == 23)
            {
                j = 1;
            }
        }
        if(j == 1)
        {
            door.GetComponent<dooropen>().i = true;

        }
    }
}
