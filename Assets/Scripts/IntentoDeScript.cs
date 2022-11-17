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

    public enum StateSelector{
        Waiting,
        Selection,
        Moving,
        Realease,
        Scale,
        Rotation,
        //... the states that we need
    }

    //
    [SerializeField]
    StateSelector currentState = StateSelector.Waiting;

    //The selected object variable
    public GameObject selectedItem;

    private void Update()
    {
        //Instead of if we can use Switch
        switch (currentState)
        {
            case StateSelector.Waiting:
                currentState = StateSelector.Selection;
                break;

            case StateSelector.Moving:
                MoveItem();
                break;

            case StateSelector.Selection:
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
        

    void SelectItem()
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

                    currentState = StateSelector.Moving;

                    //Hide Cursor
                    Cursor.visible = false;
                }
            }
        }
        
    }

    void MoveItem()
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
            selectedItem.transform.position = hitInfo.point + ((Vector3.up * selectedItem.transform.localScale.y) / 2);
        }

        //Show the object
        selectedItem.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            currentState = StateSelector.Realease;
        }
    }

    void RealeaseItem()
    {
        //Realease the object
        selectedItem = null;

        currentState = StateSelector.Waiting;

        //Show Cursor
        Cursor.visible = true;
    }
}
