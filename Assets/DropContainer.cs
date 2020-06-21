using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropContainer : MonoBehaviour
{
    public Drop drop;
    protected float lastPickupAttemptTime;
    protected const float minTimeOnGround = .5f;
    protected float birthTime;

    public void SetDrop(Drop drop)
    {
        this.drop = drop;
        lastPickupAttemptTime = Time.time;
        birthTime = Time.time;
        drop.SetModel(transform);
        if (!drop.HasAutoPickup)
        {
            GameObject dropIndicator = drop.GetDropIndicator();
            dropIndicator.GetComponent<DropIndicator>().SetOwner(transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!drop.HasAutoPickup)
        {
            return;
        }

        if (Time.time < lastPickupAttemptTime + .5f)
        {
            return;
        }

        if (Time.time < birthTime + minTimeOnGround)
        {
            return;
        }

        if (!other.CompareTag(Constants.Tags.Player))
        {
            return;
        }

        PickUp(other.GetComponent<Player>());
    }

    public void PickUp(Player player)
    {
        bool success = drop.GiveDropToPlayer(player);
        if (success)
        {
            Destroy(this.gameObject);
        }

        lastPickupAttemptTime = Time.time;
    }

    private static GameObject _dropIndicator;
    protected static GameObject DropIndicator
    {
        get
        {
            if (_dropIndicator == null)
            {
                _dropIndicator = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/ItemPickupIndicator");
            }
            return _dropIndicator;
        }
    }
}
