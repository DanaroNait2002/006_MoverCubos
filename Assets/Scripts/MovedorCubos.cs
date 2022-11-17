using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedorCubos : MonoBehaviour
{
    public GameObject cube;

    private void Update()
    {
        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
        {
            if (cube == null)
            {
                SelectCube();
            } 
            else
            {
                ReleaseCube();
            }
        }

        if (cube != null)
        {
            MoveCube();
        }
    }

    void MoveCube()
    {
        Vector3 pos = Input.mousePosition;
        Ray rayo = Camera.main.ScreenPointToRay(pos);
        RaycastHit hitInfo;

        cube.SetActive(false);

        if (Physics.Raycast(rayo, out hitInfo) == true)
        {
            cube.transform.position = hitInfo.point + Vector3.up * cube.transform.localScale.y/2;
        }
        cube.SetActive(true);
    }

    void ReleaseCube()
    {
        cube = null;
    }

    void SelectCube()
    {
        Vector3 pos = Input.mousePosition;

        if (Application.platform == RuntimePlatform.Android)
        {
            pos = Input.GetTouch(0).position;
        }

        Ray rayo = Camera.main.ScreenPointToRay(pos);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayo, out hitInfo))
        {
            if (hitInfo.collider.tag.Equals("Clickable"))
            {
                cube = hitInfo.collider.gameObject;
            }
        }
        
    }
}
