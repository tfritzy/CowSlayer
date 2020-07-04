using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropIndicator : MonoBehaviour
{
    public Transform Owner;
    private float scale;

    public void SetOwner(Transform owner)
    {
        this.Owner = owner;
        this.scale = owner.localScale.x;
    }

    private void Update()
    {
        if (Owner == null)
        {
            Destroy(this.gameObject);
            return;
        }

        transform.position = Constants.Persistant.Camera.WorldToScreenPoint(Owner.position) + new Vector3(0, 100 * scale);
    }

    public void PickUp()
    {
        if (Vector3.Distance(Owner.position, Constants.Persistant.Player.transform.position) < 3f)
        {
            Owner.GetComponent<DropContainer>().PickUp(Constants.Persistant.PlayerScript);
        }
    }
}
