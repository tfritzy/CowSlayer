using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        ShootRayCast();
    }

    private void ShootRayCast()
    {
        Vector3? inputPos = null;
        if (Input.GetMouseButtonDown(0))
        {
            inputPos = Input.mousePosition;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            inputPos = Input.GetTouch(0).position;
        }
        if (!inputPos.HasValue)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(inputPos.Value);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, Constants.Layers.Character))
        {
            hit.collider.gameObject?.GetComponent<Interactable>().Interact();
        }
    }
}
