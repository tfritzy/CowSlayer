using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decal : MonoBehaviour
{
    public float FadeStartTime;
    public float FadeDuration;

    private Material material;
    private float birthTime;
    private float fadeSpeed;
    private Color color;

    public void Setup(float fadeOffset, float fadeDuration)
    {
        this.FadeDuration = fadeDuration;
        this.FadeStartTime = Time.time + fadeOffset;

        // m = rise / run = alpha / duration = 1 / duration
        this.fadeSpeed = -1 / fadeDuration;

        GameObject.Destroy(this.gameObject, fadeDuration + fadeOffset);
    }

    void Start()
    {
        this.material = this.GetComponent<MeshRenderer>().material;
        this.color = material.color;
        this.birthTime = Time.time;
    }

    void Update()
    {
        if (Time.time > FadeStartTime)
        {
            this.color.a = fadeSpeed * (Time.time - FadeStartTime) + 1;
            material.color = this.color;
        }
    }
}
