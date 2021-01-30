using UnityEngine;

public class CreationPanelHider : MonoBehaviour
{
    public void hideCreationPannel()
    {
        gameObject.SetActive(false);
    }

    public void showCreationPannel()
    {
        gameObject.SetActive(true);
    }
}
