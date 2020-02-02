using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelBar : MonoBehaviour
{
    private Image Bar { get; set; }
    private float StartingWidth { get; set; }
    private float StartingHeight { get; set; }
    
    public float NormalizedValue { get; set; }
    
    private void Start()
    {
        Bar = GetComponent<Image>();
        var rect = Bar.rectTransform.rect;
        StartingWidth = rect.width;
        StartingHeight = rect.width;
    }

    // Update is called once per frame
    private void Update()
    {
        var width = NormalizedValue * StartingWidth;
        Bar.rectTransform.sizeDelta = new Vector2(width, Bar.rectTransform.sizeDelta.y);
    }
}
