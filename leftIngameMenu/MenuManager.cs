using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = System.Object;

public class MenuManager : MonoBehaviour
{
    private bool isItemSelection = false;
    private GameObject currentItem = null;

    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isItemSelection)
        {
            onMouseClickWhileSelection();
        }
        else if (Input.GetMouseButtonDown(0) && !isItemSelection)
        {
            onMouseClickWithoutSelection();
        }
    }

    private void FixedUpdate()
    {
        if (isItemSelection)
        {
            followCursor();
        }
    }

    public void onMenuItemClicked(GameObject prefabToSpawn)
    {
        Debug.Log($"Menu item clicked {prefabToSpawn}");
        if (isItemSelection)
        {
            changeItemSelection(prefabToSpawn);
        }
        else
        {
            spawnAndFollowItem(prefabToSpawn);
        }
    }

    public void onEmptyPanelClicked()
    {
        if (isItemSelection)
        {
            destroyCurrentItem();
        }
    }

    private void spawnAndFollowItem(GameObject itemToSpawn)
    {
        var mousePosition = Input.mousePosition;
        currentItem = Instantiate(itemToSpawn, mousePosition, Quaternion.identity);
        isItemSelection = true;
    }

    private void changeItemSelection(GameObject newItem)
    {
        destroyCurrentItem();
        spawnAndFollowItem(newItem);
    }

    private void destroyCurrentItem()
    {
        DestroyImmediate(currentItem);
        currentItem = null;
        isItemSelection = false;
    }

    private void followCursor()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        currentItem.transform.localPosition = pz;
    }

    private void onMouseClickWhileSelection()
    {
        // If we want to optimize put it in coroutine
        // If you click on the world
        if (!IsPointerOverUIObject())
        {
            // Need to detect if an other object is under the mouse
            var currentItemCollider = currentItem.GetComponent<Collider2D>();
            Collider2D[] otherColliders = Physics2D.OverlapAreaAll(currentItemCollider.bounds.min, currentItemCollider.bounds.max);

            var item = otherColliders.FirstOrDefault(item => item.CompareTag("placedItem"));
            if (item)
            {
                print("Other item under");
            }
            else
            {
                currentItem.tag = "placedItem";
                isItemSelection = false;
                currentItem = null;
            }
        }
    }

    private void onMouseClickWithoutSelection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null && hit.transform.gameObject.CompareTag("placedItem"))
        {
            currentItem = hit.transform.gameObject;
            currentItem.tag = "Untagged";
            isItemSelection = true;
        }   
    }
    
    private static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}