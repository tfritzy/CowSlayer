using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropIndicator : MonoBehaviour
{
    public Transform Owner;
    private float scale;
    private Image image;
    private Button button;
    private GameObject icon;

    public void SetOwner(Transform owner)
    {
        this.Owner = owner;
        this.scale = owner.localScale.x;
        this.image = this.GetComponent<Image>();
        this.button = this.GetComponent<Button>();
        this.icon = this.transform.Find("Icon").gameObject;
    }

    private void Update()
    {
        if (Owner == null)
        {
            Destroy(this.gameObject);
            return;
        }

        transform.position = Constants.Persistant.Camera.WorldToScreenPoint(Owner.position) + new Vector3(0, 100 * scale);
        if ((Constants.Persistant.Player.transform.position - Owner.position).magnitude < 2)
        {
            image.enabled = true;
            button.enabled = true;
            this.icon.SetActive(true);
        }
        else
        {
            image.enabled = false;
            button.enabled = false;
            this.icon.SetActive(false);
        }
    }

    public void PickUp()
    {
        if (Vector3.Distance(Owner.position, Constants.Persistant.Player.transform.position) < 3f)
        {
            Owner.GetComponent<DropContainer>().PickUp(Constants.Persistant.PlayerScript);
        }
    }
}
