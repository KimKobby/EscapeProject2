using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    bool isFade;
    public Canvas canvas;
    private Color color;
    private float fadeTime = 2f;
    Color fadeoutcolor;
   [SerializeField] private float elapsedTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        color = canvas.transform.GetChild(0).GetComponent<Image>().color;
        fadeoutcolor = new Color(color.r, color.g, color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isFade)
        {
            float f = canvas.transform.GetChild(0).GetComponent<Image>().color.a;
            fadeTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeTime);
            // = Mathf.Lerp(color, fadeoutcolor, t);
        }
    }
}
