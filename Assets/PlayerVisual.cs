using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private GameObject bodymesh_girl;
    [SerializeField] private GameObject bodymesh_boy;

    Color blue = new Color(0, 0, 1, 1);
    Color red = new Color(1, 0, 0, 1);
    public void SetPlayerColor(Color color)
    {

        if (color == blue)
        {
             bodymesh_boy.SetActive(true);
             bodymesh_girl.SetActive(false);
        }


        else if(color == red)
        {
            bodymesh_boy.SetActive(false) ;
            bodymesh_girl.SetActive(true);
        }
    }
}
