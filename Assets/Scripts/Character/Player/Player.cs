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

    protected override void UpdateLoop()
    {
        this.rb.velocity = GetKeyboardInput() * MovementSpeed;
        CheckForTarget();
        Attack();
    }

    public override void Interact()
    {
        this.Inventory.OpenMenu(0f, this.WornItems);
        this.WornItems.OpenMenu(.5f, this.Inventory);
    }

    protected override void Initialize() {
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

    private Vector3 GetKeyboardInput()
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
        return movementDirection;
    }

    public void OpenInventory(ItemGroup transferTarget = null)
    {
        this.Inventory.OpenMenu(0f, transferTarget);
    }
}
