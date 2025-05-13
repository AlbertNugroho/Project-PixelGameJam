using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceMouse : MonoBehaviour
{
    SpriteRenderer sr;
    public GameObject preview;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        faceMouse();
    }

    private void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
        mousePosition.x - transform.position.x,
        mousePosition.y - transform.position.y
        );

        transform.right = direction;

        Vector3 scale = transform.localScale;
        scale.y = direction.x < 0 ? -Mathf.Abs(scale.y) : Mathf.Abs(scale.y);
        Vector3 previewscale = preview.transform.localScale;
        previewscale.y = direction.x < 0 ? -Mathf.Abs(scale.y) : Mathf.Abs(scale.y);
        transform.localScale = scale;
        preview.transform.localScale = previewscale;
    }
}
