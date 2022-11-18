using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentoDeScript : MonoBehaviour
{
    /*
     
    1 Seleccionar Objeros
        Al hacer click sucede algo
            Lanzar un rayo desde la camara hacia el ratón
            Comprobar si es donde el rayo da es un objeto movible
            Seleccionar Objeto

    2 Mover Objetos

    3 Soltar Objetos

     */

    public enum StateSelector
    {
        Waiting,
        SelectionMove,
        SelectionRotation,
        SelectionScale,
        Moving,
        Realease,
        Rotating,
        Scaling,
        SelectTypeCreation,
        CreatingCube,
        CreatingSphere,
        CreatingCylinder,
        //... the states that we need
    }

    //
    [SerializeField]
    StateSelector currentState = StateSelector.Waiting;

    [SerializeField]
    GameObject mainCanvas, creationCanvas;


    //The selected object variable
    public GameObject selectedItem, prefabCube, prefabSphere, prefabCylinder;

    Vector2 mousePos;


    private void Update()
    {
        //Instead of if we can use Switch
        switch (currentState)
        {
            /*case StateSelector.Waiting:
                currentState = StateSelector.SelectionMove;
                break;*/

            case StateSelector.Moving:
                MoveItem();
                break;

            case StateSelector.Rotating:
                RotateItem();
                break;

            case StateSelector.Scaling:
                ScaleItem();
                break;

            /*case StateSelector.CreatingCube:
                CreatingCube();
                break;

            case StateSelector.CreatingSphere:
                CreatingSphere();
                break;

            case StateSelector.CreatingCylinder:
                CreatingCylinder();
                break;*/

            case StateSelector.SelectionMove:
                SelectItem();
                break;

            case StateSelector.SelectionRotation:
                SelectItem();
                break;

            case StateSelector.SelectionScale:
                SelectItem();
                break;

            case StateSelector.Realease:
                RealeaseItem();
                break;
        }
        /*
        //If left click is Pressed
        if (Input.GetMouseButtonDown(0))
        {
            //If tthere´s an object selected
            if (selectedItem == null)
            {
                SelectItem();
            }
            //In case that the hitted object is not null
            else
            {
                RealeaseItem();
            }
        }

        //If there´s no object selected
        if (selectedItem != null)
        {
            MoveItem();
        }*/
    }


    public void SelectItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Send Ray from cam to the mouse's position
            Vector3 pos = Input.mousePosition;
            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayo, out hitInfo))
            {
                //Comparing the tag of the hitted object with an specific one
                if (hitInfo.collider.tag.Equals("Clickable"))
                {

                    //The hitted object is the selected object
                    selectedItem = hitInfo.collider.gameObject;

                    //El creo otra función para esto
                    switch (currentState)
                    {
                        case StateSelector.SelectionMove:
                            currentState = StateSelector.Moving;
                            break;

                        case StateSelector.SelectionRotation:
                            currentState = StateSelector.Rotating;
                            break;

                        case StateSelector.SelectionScale:
                            currentState = StateSelector.Scaling;
                            break;
                    }

                    //Hide Cursor
                    Cursor.visible = false;
                }
            }
        }
    }

    public void MoveItem()
    {
        //Send Ray from cam to the mouse's position
        Vector3 pos = Input.mousePosition;
        Ray rayo = Camera.main.ScreenPointToRay(pos);
        RaycastHit hitInfo;

        //Hide the object to avoid hitting it with the new Ray
        selectedItem.SetActive(false);

        if (Physics.Raycast(rayo, out hitInfo))
        {
            //Moving the object
            selectedItem.transform.position = hitInfo.point + ((Vector3.up * selectedItem.transform.localScale.y) * 1.5f);
        }

        //Show the object
        selectedItem.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            currentState = StateSelector.Realease;
        }
    }

    public void ButtonMoveItem()
    {
        currentState = StateSelector.SelectionMove;
    }
    public void RotateItem()
    {
        Vector2 mouseDelta = mousePos - (Vector2)Input.mousePosition;
        selectedItem.transform.Rotate(mouseDelta.y, mouseDelta.x, 0f);

        mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            selectedItem.GetComponent<Rigidbody>().isKinematic = false;
            currentState = StateSelector.Realease;
        }

    }

    public void ButtonRotateItem()
    {
        currentState = StateSelector.SelectionRotation;
    }

    public void ScaleItem()
    {
        selectedItem.transform.localScale += Vector3.one * Input.mouseScrollDelta.y;

        if (selectedItem.transform.localScale.x <= 1f)
        {
            selectedItem.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            currentState = StateSelector.Realease;
        }
    }

    public void ButtonScaleItem()
    {
        currentState = StateSelector.SelectionScale;
    }

    public void ButtonCreateItem()
    {
        mainCanvas.SetActive(false);
        creationCanvas.SetActive(true);

        //Crear un cubo, esfera o cilindro (+opcion de cancelar) en un canva
    }

    public void Creating(GameObject prefab)
    {
        selectedItem = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        currentState = StateSelector.Moving;
    }

    /*public void CreatingCube()
    {
        selectedItem =  Instantiate(prefabCube);
        //selectedItem = prefabCube;

        currentState = StateSelector.Moving;
    }

    public void CreatingSphere()
    {
        selectedItem = Instantiate(prefabSphere);
        //selectedItem = prefabSphere;

        currentState = StateSelector.Moving;
    }

    public void CreatingCylinder()
    {
        selectedItem = Instantiate(prefabCylinder, Vector3.zero, Quaternion.identity);
        //selectedItem = prefabCylinder;

        currentState = StateSelector.Moving;
    }*/

    public void RealeaseItem()
    {
        //Realease the object
        selectedItem = null;

        currentState = StateSelector.Waiting;

        //Show Cursor
        Cursor.visible = true;
    }

    public void ButtonCancel()
    {
        mainCanvas.SetActive(true);
        creationCanvas.SetActive(false);
        currentState = StateSelector.Waiting;
    }
}
