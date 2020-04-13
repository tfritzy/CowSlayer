using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Rigidbody rb;
    public float MovementSpeed;
    public List<Item> Inventory;

    private GameObject playerInventoryUI;

    protected override void UpdateLoop()
    {
        this.rb.velocity = GetKeyboardInput() * MovementSpeed;
        CheckForTarget();
        Attack();
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
        this.Inventory = new List<Item>();
        this.Name = "Player";
        this.name = "Player";
        this.playerInventoryUI = Resources.Load<GameObject>($"{Constants.FilePaths.UIPrefabs}/PlayerInventoryUI");
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

    public void LogInventory(){
        string inventoryDescription = "";
        foreach (Item item in this.Inventory){
            inventoryDescription += $"{item},";
        }
        Debug.Log(inventoryDescription);
    }
}
