using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuItemListener : MonoBehaviour, IPointerClickHandler
{

    public GameObject prefabToSpawn;
    private MenuManager _menuManager;
    
    void Start()
    {
        _menuManager = FindObjectOfType<MenuManager>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _menuManager.onMenuItemClicked(prefabToSpawn);
    }
}
