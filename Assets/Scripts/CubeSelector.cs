using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSelector : MonoBehaviour
{
    //Materiales
    [SerializeField]
    public Material selected_Material, not_Selected_Material;

    [SerializeField]
    GameObject cube;

    /*void Update()
    {
        //Condicional para saber si se esta clickando o no el objeto
        if ((Input.touchCount >= 1) || (Input.GetMouseButton(0)))
        {

        }
    }*/

    private void OnMouseDrag()
    {
        Debug.Log("Hoooola");
    }
}
