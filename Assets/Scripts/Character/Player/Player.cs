using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Rigidbody rb;
    public float MovementSpeed;
    public ItemGroup Inventory;
    public WornItemsGroup WornItems;

    private GameObject playerInventoryUI;

    private Joystick joystick { get { return Constants.GameObjects.Joystick; } }
    private bool isDashing;
    private const float dashDuration = .3f;
    private float dashStartTime;
    private float movementSpeedModifier = 1;
    private Vector3 dashDirection;

    protected override void UpdateLoop()
    {
        SetDashStatus();
        SetVelocity();
        CheckForTarget();
        Attack();
    }

    public override void Interact()
    {
        this.Inventory.OpenMenu(0f, this.WornItems);
        this.WornItems.OpenMenu(.5f, this.Inventory);
    }

    public override void Initialize() {
        base.Initialize();
        this.Health = 100;
        this.MaxHealth = Health;
        this.Damage = 1;
        this.AttackSpeed = 1;
        this.AttackRange = 1.5f;
        this.TargetFindRadius = 3;
        this.Allegiance = Allegiance.Player;
        this.Enemies = new HashSet<Allegiance>() {Allegiance.Cows};
        this.rb = this.GetComponent<Rigidbody>();
        this.Inventory = new ChestItemGroup();
        this.Name = "Player";
        this.name = "Player";
        this.WornItems = new WornItemsGroup();
    }

    private void SetVelocity()
    {
        if (isDashing)
        {
            rb.velocity = dashDirection * MovementSpeed * 3;
        }
        else
        {
            rb.velocity = GetInput() * MovementSpeed;
        }
        
    }

    private Vector3 GetInput()
    {
        Vector3 movementDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movementDirection.z += 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection.x += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementDirection.z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection.x -= 1;
        }

        if (movementDirection == Vector3.zero)
        {
            movementDirection = joystick.Direction;
        }

        return movementDirection;
    }

    private void SetDashStatus()
    {
        if (isDashing)
        {
            if (Time.time > dashStartTime + dashDuration)
            {
                isDashing = false;
            }
        }
        else
        {
            if (joystick.IsDashing)
            {
                isDashing = true;
                dashStartTime = Time.time;
                dashDirection = joystick.Direction;
            }
        }
    }

    public void OpenInventory(ItemGroup transferTarget = null)
    {
        this.Inventory.OpenMenu(0f, transferTarget);
    }
}
