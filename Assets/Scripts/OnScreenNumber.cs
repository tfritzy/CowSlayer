using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenNumber : MonoBehaviour
{
    public int Value;
    private const float LifeSpan = 1f;

    private Color textColor
    {
        get
        {
            return this.GetComponent<Text>().color;
        }
        set {
            this.GetComponent<Text>().color = value;
        }
    }

    private Image _icon;
    private Image icon
    {
        get
        {
            if (_icon == null)
            {
                _icon = transform.Find("Icon").GetComponent<Image>();
            }

            return _icon;
        }
    }

    private float birthTime;
    private void Start()
    {
        birthTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > birthTime + LifeSpan)
        {
            Delete();
        }
        LessenColorOverTime();
    }

    private void LessenColorOverTime()
    {
        float alpha = (LifeSpan - (Time.time - birthTime)) / LifeSpan;

        // Set Icon alpha
        Color currentColor = icon.color;
        currentColor.a = alpha;
        icon.color = currentColor;

        // Set text alpha
        Color currentTextColor = textColor;
        currentTextColor.a = alpha;
        textColor = currentTextColor;
    }

    public void SetValue(int value, GameObject owner){
        this.Value = value;
        this.GetComponent<Text>().text = value.ToString();
        SetColor();
        this.transform.position = owner.transform.position;
        this.GetComponent<Rigidbody>().velocity = Vector3.up * 10;
    }

    private void SetColor(){
        if (Value > 1000){
            this.textColor = Color.magenta;
        } else if (Value > 500){
            this.textColor = Color.red;
        } else if (Value > 100){
            this.textColor = Color.yellow;
        } else if (Value > 30){
            this.textColor = new Color(1f, .5f, 0);
        } else {
            this.textColor = Color.white;
        }
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
