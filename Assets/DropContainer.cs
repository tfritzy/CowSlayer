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

        // Make all children fly towards the player.
        foreach (Rigidbody child in this.transform.GetComponentsInChildren<Rigidbody>())
        {
            if (child.GetComponent<FlyTowardsObject>() == null)
            {
                FlyTowardsObject flyTowards = child.gameObject.AddComponent<FlyTowardsObject>();
                flyTowards.SetTarget(other.gameObject, RewardPlayer);
                child.useGravity = false;
            }
        }
    }

    private bool hasBeenRewardedAlready = false;
    public void RewardPlayer()
    {
        if (hasBeenRewardedAlready)
        {
            return;
        }

        drop.GiveDropToPlayer(Constants.Persistant.PlayerScript);
        GameObject textPopup = Instantiate(
            Constants.Prefabs.OnScreenNumber,
            Constants.Persistant.Camera.WorldToScreenPoint(Constants.Persistant.Player.transform.position),
            new Quaternion(),
            Constants.Persistant.Canvas.transform);
        textPopup.GetComponent<OnScreenNumber>().SetValue(drop.Quantity, Constants.Persistant.Player, drop.Icon);

        // Destroy all objects that are still in transit.
        foreach (Rigidbody child in this.transform.GetComponentsInChildren<Rigidbody>())
        {
            Destroy(child.gameObject);
        }

        hasBeenRewardedAlready = true;
        Destroy(this.gameObject);
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
