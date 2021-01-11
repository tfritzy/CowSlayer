using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenNumber : MonoBehaviour
{
    public int Value;
    private const float LifeSpan = 1f;

    private Text _text;
    private Text text
    {
        get
        {
            if (_text == null)
            {
                _text = transform.Find("Text").GetComponent<Text>();
            }

            return _text;
        }
    }

    private Image iconNextToNumber;
    private float birthTime;
    private void Start()
    {
        birthTime = Time.time;
        iconNextToNumber = transform.Find("Icon")?.GetComponent<Image>();
        this.transform.position += (Vector3)(Random.insideUnitCircle / 2);
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

        if (iconNextToNumber != null)
        {
            // Set Icon alpha
            Color currentColor = iconNextToNumber.color;
            currentColor.a = alpha;
            iconNextToNumber.color = currentColor;
        }

        // Set text alpha
        Color currentTextColor = text.color;
        currentTextColor.a = alpha;
        text.color = currentTextColor;
    }

    public void SetValue(int value, GameObject owner, Sprite icon)
    {
        this.Value = value;
        text.text = value.ToString();
        SetColor();
        this.transform.position = Constants.Persistant.Camera.WorldToScreenPoint(
            owner.transform.position + (Vector3)Random.insideUnitCircle / 5);
        this.GetComponent<Rigidbody>().velocity = Vector3.up * 100;

        if (iconNextToNumber != null)
        {
            iconNextToNumber.sprite = icon;
        }
    }

    private void SetColor()
    {
        if (Value > 1000)
        {
            this.text.color = Color.magenta;
        }
        else if (Value > 500)
        {
            this.text.color = Color.red;
        }
        else if (Value > 100)
        {
            this.text.color = Color.yellow;
        }
        else if (Value > 30)
        {
            this.text.color = new Color(1f, .5f, 0);
        }
        else
        {
            this.text.color = Color.white;
        }
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
