using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    public int Value;
    public Vector3 startingPosition;

    void Update()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        this.transform.position = Constants.Persistant.Camera.WorldToScreenPoint(startingPosition + Vector3.up * 1);
    }

    private Color textColor
    {
        set {
            this.GetComponent<Text>().color = value;
        }
    }

    public void SetValue(int value, GameObject owner){
        this.Value = value;
        this.GetComponent<Text>().text = value.ToString();
        SetColor();
        startingPosition = owner.transform.position;
        SetPosition();
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
