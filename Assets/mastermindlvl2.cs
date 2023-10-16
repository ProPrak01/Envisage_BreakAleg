/**using System.Collections;
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
            dooropenarray[4, 3] = false;
            dooropenarray[1, 3] = false;
        }

        if(dooropenarray[2,4] == true && dooropenarray[3,1] == true)
        {
            slab[9].SetActive(false);
            slab[12].SetActive(false);
            k[1] = 1;
            dooropenarray[2, 4] = false;
            dooropenarray[3, 1] = false;
        }

        if (dooropenarray[2,5] == true && dooropenarray[1,2] == true)
        {
            slab[1].SetActive(false);
            slab[10].SetActive(false);
            k[2] = 1;
            dooropenarray[2, 5] = false;
            dooropenarray[1, 2] = false;
        }

        if (dooropenarray[3,4] == true && dooropenarray[1,5] == true)
        {
            slab[15].SetActive(false);
            slab[4].SetActive(false);
            k[3] = 1;
            dooropenarray[3, 4] = false;
            dooropenarray[1, 5] = false;
        }

        if (dooropenarray[4,2] == true && dooropenarray[3,5] == true)
        {
            slab[16].SetActive(false);
            slab[19].SetActive(false);
            k[4] = 1;
            dooropenarray[4, 2] = false;
            dooropenarray[3, 5] = false;
        }

        if (dooropenarray[4,1] == true && dooropenarray[3,6] == true)
        {
            slab[17].SetActive(false);
            slab[18].SetActive(false);
            k[5] = 1;
            dooropenarray[4, 1] = false;
            dooropenarray[3, 6] = false;
        }

        if (dooropenarray[4,4] == true && dooropenarray[4,5] == true)
        {
            slab[21].SetActive(false);
            slab[22].SetActive(false);
            k[6] = 1;
            dooropenarray[4, 4] = false;
            dooropenarray[4, 5] = false;
        }

        if (dooropenarray[1,6] == true && dooropenarray[2,2] == true)
        {
            slab[5].SetActive(false);
            slab[7].SetActive(false);
            k[7] = 1;
            dooropenarray[1, 6] = false;
            dooropenarray[2, 2] = false;
        }

        if (dooropenarray[2,3] == true && dooropenarray[2,6] == true)
        {
            slab[8].SetActive(false);
            slab[11].SetActive(false);
            k[8] = 1;
            dooropenarray[2, 3] = false;
            dooropenarray[2, 6] = false;
        }

        if (dooropenarray[1,1] == true && dooropenarray[3,3] == true)
        {
            slab[0].SetActive(false);
            slab[14].SetActive(false);
            k[9] = 1;
            dooropenarray[1, 1] = false;
            dooropenarray[3, 3] = false;
        }

        if (dooropenarray[3,2] == true && dooropenarray[2,1] == true)
        {
            slab[6].SetActive(false);
            slab[13].SetActive(false);
            k[10] = 1;
            dooropenarray[3, 2] = false;
            dooropenarray[2, 1] = false;
        }

        if (dooropenarray[1,4] == true && dooropenarray[4,6] == true)
        {
            slab[3].SetActive(false);
            slab[22].SetActive(false);
            k[11] = 1;
            dooropenarray[1, 4] = false;
            dooropenarray[4, 6] = false;
        }
        
        for(int m = 0; m < 12; m++)
        {
            if (k[m] == 0)
            {
                break;
            }
            else if( m == 11)
            {
                j = 1;
            }
        }

        if(j == 1)
        {
            door.GetComponent<dooropen>().i = true;

        }
    }
}**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mastermindlvl2 : MonoBehaviour
{
    public Material RedColor;
    public GameObject RemoveObject1;
    public GameObject RemoveObject2;
    public GameObject Door;
    public bool[,] DoorOpenArray = new bool[10, 10];
    public List<GameObject> Slabs = new List<GameObject>();
    public List<int> K = new List<int>();
    int index = 0;

    private void Start()
    {
        // Initialize K list with zeros
        for (int i = 0; i < 12; i++)
        {
            K.Add(0);
        }
    }

    private bool CheckCondition(int row1, int col1, int row2, int col2, int index1, int index2)
    {/**
        Debug.Log(DoorOpenArray[row1, col1]);
        Debug.Log(DoorOpenArray[row2, col2]);**/

        if (DoorOpenArray[row1, col1] && DoorOpenArray[row2, col2])
        {
            Debug.Log($"Disabling Slabs {index1} and {index2}");
            Debug.Log(DoorOpenArray[row1, col1]);
            Debug.Log(DoorOpenArray[row2, col2]);
            Slabs[index1].SetActive(false);
            Slabs[index2].SetActive(false);
            K[index] = 1;
            index++;
            DoorOpenArray[row1, col1] = false;
            DoorOpenArray[row2, col2] = false;
            return true;
        }
        return false;
    }

    void Update()
    {
        
        if(Input.GetKey(KeyCode.F))
        {
            for(int i = 0;i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    Debug.Log($"Slabs {i} and {j} is {DoorOpenArray[i, j]}");
                    Debug.Log("\n");
                }
            }
        }
        
        if (CheckCondition(4, 3, 1, 3, 0, 1) &&
            CheckCondition(2, 4, 3, 1, 2, 3) &&
            CheckCondition(2, 5, 1, 2, 4, 5) &&
            CheckCondition(3, 4, 1, 5, 6, 7) &&
            CheckCondition(4, 2, 3, 5, 8, 9) &&
            CheckCondition(4, 1, 3, 6, 10, 11) &&
            CheckCondition(4, 4, 4, 5, 12, 13) &&
            CheckCondition(1, 6, 2, 2, 14, 15) &&
            CheckCondition(2, 3, 2, 6, 16, 17) &&
            CheckCondition(1, 1, 3, 3, 18, 19) &&
            CheckCondition(3, 2, 2, 1, 20, 21) &&
            CheckCondition(1, 4, 4, 6, 22, 23)
        )
        {
            Door.GetComponent<dooropen>().i = true;
        }
    }
}

