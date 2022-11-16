using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSelector : MonoBehaviour
{
    private GameObject selectedObject;
    Vector3 mOffset;

    void Update()
    {
        //Condicional de click izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            //Condicional de que el objeto clickado no es nulo
            if (selectedObject == null)
            {
                Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(rayo, out hitInfo))
                {
                    //Condicional de que el collider no es nulo
                    if (hitInfo.collider != null)
                    {
                        //Condicional de que el objeto clickado tiene la tag Clickable
                        if (!hitInfo.collider.CompareTag("Clickable"))
                        {
                            //Si no la tiene sale de la función
                            return;
                        }

                        //Convertimos nuestro objeto clickado en el selectedObject
                        selectedObject = hitInfo.collider.gameObject;
                        selectedObject.SetActive(false);

                        //Desabilitamos la vista del cursor
                        Cursor.visible = false;
                    }
                }
            }
            else
            {
                Ray rayo2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo2;
                if (Physics.Raycast(rayo2, out hitInfo2))
                {
                    mOffset = new Vector3(0f, selectedObject.transform.position.y, -0.75f);
                    var tmpPos = mOffset;

                    selectedObject.SetActive(true);
                    selectedObject.transform.position = hitInfo2.point + tmpPos;
                }
                selectedObject = null;
                Cursor.visible = true;
            }
        }

        //Condicional de que el objeto clickado no es igual a nulo
        if (selectedObject != null)
        {
            Ray rayo2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo2;
            if (Physics.Raycast(rayo2, out hitInfo2))
            {
                //mOffset = new Vector3(0f, selectedObject.transform.position.y, -0.75f);
                //var tmpPos = mOffset;

                selectedObject.SetActive(true);
                selectedObject.transform.position = hitInfo2.point;// + tmpPos;
            }

        }
    }








    /*private GameObject selectedObject;
    Vector3 mOffset;

    void Update()
    {
        //Condicional de click izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            //Condicional de que el objeto clickado no es nulo
            if (selectedObject == null)
            {
               

                //Condicional de que el collider no es nulo
                if (hit.collider != null) 
                {
                    //Condicional de que el objeto clickado tiene la tag Clickable
                    if (!hit.collider.CompareTag("Clikable"))
                        {
                            //Si no la tiene sale de la función
                            return;
                        }

                    //Convertimos nuestro objeto clickado en el selectedObject
                    selectedObject = hit.collider.gameObject;

                    //Desabilitamos la vista del cursor
                    UnityEngine.Cursor.visible = false
                }

            } else
            {

            }
        }

        //Condicional de que el objeto clickado no es igual a nulo
        if (selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

            //Esto levantará al objeto seleccionado un poco hacia arriba
            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
        }
    }

    //Función de calculos de posición
    private RaycastHit CastRay()
    {
        Vector3 screemMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);

        Vector3 screemMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToViewportPoint(screemMousePosFar);

        Vector3 worldMousePosNear = Camera.main.ScreenToViewportPoint(screemMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }*/

}
