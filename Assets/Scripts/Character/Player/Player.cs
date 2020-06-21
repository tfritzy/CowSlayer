using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    private Rigidbody rb;
    public float MovementSpeed;
    public ItemGroup Inventory;
    public WornItemsGroup WornItems;
    public int Gold;

    private GameObject playerInventoryUI;

    private Joystick joystick { get { return Constants.GameObjects.Joystick; } }
    private bool isDashing;
    private const float dashDuration = .3f;
    private float dashStartTime;
    private Vector3 dashDirection;

    protected override void UpdateLoop()
    {
        SetDashStatus();
        base.UpdateLoop();
    }

    public override void Initialize() {
        base.Initialize();
        this.Health = 100;
        this.MaxHealth = Health;
        this.Damage = 1;
        this.AttackSpeed = 1;
        this.AttackRange = 2.1f;
        this.TargetFindRadius = 3;
        this.Allegiance = Allegiance.Player;
        this.Enemies = new HashSet<Allegiance>() {Allegiance.Cows};
        this.rb = this.GetComponent<Rigidbody>();
        this.Inventory = new ChestItemGroup("Inventory");
        this.Name = "Player";
        this.name = "Player";
        this.WornItems = new WornItemsGroup("Equiped Items");
    }

    protected override void SetVelocity()
    {
        if (isDashing)
        {
            rb.velocity = dashDirection * MovementSpeed * 3;
        }
        else
        {
            rb.velocity = GetInput() * MovementSpeed;
        }
        SetRotationWithVelocity();
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
        this.Inventory.OpenMenu(.32f, this.WornItems);
        Transform wornItems = this.WornItems.OpenMenu(.75f, this.Inventory);
        ConfigureStatsMenu(Instantiate(Constants.Prefabs.PlayerStatsWindow, wornItems).transform);
    }

    private void ConfigureStatsMenu(Transform statsMenu)
    {
        statsMenu.Find("Gold").GetComponent<Text>().text = $"{this.Gold} Gold";
    }

    protected override void OnDeath()
    {
        ShowDeathScreen();
        Body.Transform.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void Respawn()
    {
        UIActions.CloseAllWindows();
        Body.Transform.GetComponent<MeshRenderer>().material.color = Color.magenta;
        this.transform.position = Constants.GameObjects.SpawnPoint;
        this.Health = MaxHealth;
        this.IsDead = false;
    }

    private void ShowDeathScreen()
    {
        UIActions.CloseAllWindows();
        Instantiate(Constants.Prefabs.DeathScreenUI, Constants.GameObjects.InteractableUI);
    }
}
