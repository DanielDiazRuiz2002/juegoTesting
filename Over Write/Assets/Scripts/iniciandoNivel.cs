using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iniciandoNivel : MonoBehaviour
{
    private RectTransform rectTransform;
    private float duration = 3f;
    private float targetScaleX = 1000f;
    private float targetScaleY = 670f;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); 
        StartCoroutine(ScaleOverTime(duration));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = rectTransform.localScale;
        Vector3 targetScale = new Vector3(targetScaleX, targetScaleY, originalScale.z);
        float currentTime = 0.0f;

        do
        {
            rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        rectTransform.localScale = targetScale;
    }
}
