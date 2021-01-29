using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPanelListener : MonoBehaviour, IPointerClickHandler
{
    
    private MenuManager _menuManager;
    
    void Start()
    {
        _menuManager = FindObjectOfType<MenuManager>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _menuManager.onEmptyPanelClicked();
    }
}
