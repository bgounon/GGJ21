using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class MenuManager : MonoBehaviour
{
    public float rotationSpeed = 2f;
    private bool isItemSelection = false;
    private GameObject currentItem = null;
    private GameObject player;
    private GameManager gameManager;
    private Sound sound;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        sound = FindObjectOfType<Sound>();
        player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
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
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0 && isItemSelection)
        {
            rotateElementToRight();
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0 && isItemSelection)
        {
            rotateElementToLeft();
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
            if (isAllowedToSelectItem())
            {
                sound.buttonSound();
                spawnAndFollowItem(prefabToSpawn);
            }
        }
    }

    public void onEmptyPanelClicked()
    {
        if (isItemSelection)
        {
            sound.buttonSound();
            Score currentItemScoreScript = currentItem.GetComponent<Score>();
            if (currentItemScoreScript.isOnTheGround)
            {
                gameManager.addScore(currentItemScoreScript.scorePenalty);
            }
            destroyCurrentItem();
        }
    }

    private void spawnAndFollowItem(GameObject itemToSpawn)
    {
        var mousePosition = Input.mousePosition;
        currentItem = Instantiate(itemToSpawn, mousePosition, Quaternion.identity);
        setItemSelection(true);
    }

    private void changeItemSelection(GameObject newItem)
    {
        destroyCurrentItem();
        spawnAndFollowItem(newItem);
    }

    private void destroyCurrentItem()
    {
        setItemSelection(false);
        DestroyImmediate(currentItem);
        currentItem = null;
    }

    private void followCursor()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        currentItem.transform.localPosition = pz;
    }

    private void onMouseClickWhileSelection()
    {
        Score currentItemScoreScript = currentItem.GetComponent<Score>();
        // If we want to optimize put it in coroutine
        // If you click on the world
        if (!IsPointerOverUIObject())
        {
            // Need to detect if an other object is under the mouse
            var currentItemCollider = currentItem.GetComponent<Collider2D>();
            Collider2D[] otherColliders = Physics2D.OverlapAreaAll(currentItemCollider.bounds.min, currentItemCollider.bounds.max);

            var item = otherColliders.FirstOrDefault(item => item.CompareTag("Obstacle"));
            if (item)
            {
                print("Other item under");
            }
            else if (gameManager.currentScore < currentItemScoreScript.scorePenalty) {
                print("Item is too expensive");
            }
            else
            {
                sound.buttonSound();
                currentItem.tag = "Obstacle";
                currentItemScoreScript.isOnTheGround = true;
                gameManager.removeScore(currentItemScoreScript.scorePenalty);

                setItemSelection(false);
                currentItem = null;
            }
        }
    }

    private void onMouseClickWithoutSelection()
    {
        if(!isAllowedToSelectItem())
        {
            return;
        }
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null && hit.transform.gameObject.CompareTag("Obstacle"))
        {
            print("Hit obstacle");
            currentItem = hit.transform.gameObject;
            currentItem.tag = "Untagged";
            setItemSelection(true);
        }   
    }

    private void rotateElementToRight()
    {
        currentItem.transform.Rotate(new Vector3(0,0,1) * rotationSpeed, Space.Self);
    }

    private void rotateElementToLeft()
    {
        currentItem.transform.Rotate(new Vector3(0,0,-1) * rotationSpeed, Space.Self);
    }
    
    private static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void setItemSelection(bool value)
    {
        var collider = currentItem.GetComponent<Collider2D>();
        collider.enabled = !value;

        isItemSelection = value;
    }
    
    public bool isReadyToStart()
    {
        return !isItemSelection;
    }

    private bool isAllowedToSelectItem()
    {
        return gameManager.isAllowedToSelectItem();
    }
}