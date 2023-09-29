using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    public Color emptyColor = Color.grey;
    public Color groundColor = Color.white;
    public Color deathColor = Color.red;

    // Start is called before the first frame update
    void Start()
    { 
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the other layer is layer 7, set to red
        if(other != null)
        {
            var layer = other.gameObject.layer;
            if(layer == 6)
            {
                _spriteRenderer.color = groundColor;
            } else if (layer == 7)
            {
                _spriteRenderer.color = deathColor;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other != null ) {
            _spriteRenderer.color = emptyColor;
        }
    }
}
