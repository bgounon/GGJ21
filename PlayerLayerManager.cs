using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushBack"))
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushBack"))
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 2;
        }
    }
}
