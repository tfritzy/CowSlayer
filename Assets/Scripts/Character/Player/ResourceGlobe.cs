using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ResourceGlobe : MonoBehaviour
{
    private Text message;

    // Start is called before the first frame update
    void Start()
    {
        this.message = transform.Find("Message").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckStatus() < 20)
        {
            DisplayDrinkPotionMessage();
        }
        else
        {
            this.message.text = string.Empty;
        }
    }

    /// <summary>
    /// Drinks the potion of the corresponding resource type.
    /// </summary>
    public abstract void DrinkPotion();

    /// <summary>
    /// The percentage of resources left on the character for this resource type.
    /// </summary>
    public abstract float CheckStatus();

    /// <summary>
    /// Displays a message on the globe saying tap to drink a potion.
    /// </summary>
    public virtual void DisplayDrinkPotionMessage()
    {
        this.message.text = "Tap to Drink Potion";
    }
}
