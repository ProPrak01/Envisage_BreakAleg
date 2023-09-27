using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mastermindlvl2 : MonoBehaviour
{
    public Material redcolor;

    [SerializeField] private GameObject removeobject1;
    [SerializeField] private GameObject removeobject2;
    public GameObject door;
    public bool[,] dooropenarray = new bool[10, 10];
    public GameObject[] slab;
    public int[] k = new int[12];

    // Start is called before the first frame update
    public int j=0;
    void Start()
    {
       
        /**
         * 0-11
         * 1-12
         * 2-13
         * 3-14
         * 4-15
         * 5-16
         * 6-21
         * 7-22
         * 8-23
         * 9-24
         * 10-25
         * 11-26
         * 12-31
         * 13-32
         * 14-33
         * 15-34
         * 16-35
         * 17-36
         * 18- 41
         * 19-42
         * 20-43
         * 21-44
         * 22-45
         * 23-46
      
          **/
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

      // test code:::  slab[0].SetActive(false);
        
        if (dooropenarray[4,3] == true && dooropenarray[1,3] == true)
        {
            // slab[43].GetComponent<Renderer>().material = redcolor;
            slab[20].SetActive(false);
            slab[2].SetActive(false);
            k[0] = 1;
      
        }
        
        else if(dooropenarray[2,4] == true && dooropenarray[3,1] == true)
        {
            slab[9].SetActive(false);
            slab[12].SetActive(false);
            k[1] = 1;
        }
        else if (dooropenarray[2,5] == true && dooropenarray[1,2] == true)
        {
            slab[1].SetActive(false);
            slab[10].SetActive(false);
            k[2] = 1;
        }
        else if (dooropenarray[3,4] == true && dooropenarray[1,5] == true)
        {
            slab[15].SetActive(false);
            slab[4].SetActive(false);
           k[3] = 1;
        }
        else if (dooropenarray[4,2] == true && dooropenarray[3,5] == true)
        {
            slab[16].SetActive(false);
            slab[19].SetActive(false);
            k[4] = 1;

        }
        else if (dooropenarray[4,1] == true && dooropenarray[3,6] == true)
        {
            slab[17].SetActive(false);
            slab[18].SetActive(false);
           k[5] = 1;
            

        }
        else if (dooropenarray[4,4] == true && dooropenarray[4,5] == true)
        {
            slab[21].SetActive(false);
            slab[22].SetActive(false);
            k[6] = 1;

        }
        else if (dooropenarray[1,6] == true && dooropenarray[2,2] == true)
        {
            slab[5].SetActive(false);
            slab[7].SetActive(false);
            k[7] = 1;
        }
        else if (dooropenarray[2,3] == true && dooropenarray[2,6] == true)
        {
            slab[8].SetActive(false);
            slab[11].SetActive(false);
            k[8] = 1;

        }
        else if (dooropenarray[1,1] == true && dooropenarray[3,3] == true)
        {
            slab[0].SetActive(false);
            slab[14].SetActive(false);
            k[9] = 1;
        }
        else if (dooropenarray[3,2] == true && dooropenarray[2,1] == true)
        {
            slab[6].SetActive(false);
            slab[13].SetActive(false);
            k[10] = 1;
        }
        else if (dooropenarray[1,4] == true && dooropenarray[4,6] == true)
        {
            slab[3].SetActive(false);
            slab[22].SetActive(false);
            k[11] = 1;
        }
        
        for(int m = 0; m < 12; m++)
        {
            if (k[m] == 0)
            {
                break;
            }
            if( m == 11)
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
