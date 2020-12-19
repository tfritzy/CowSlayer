﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    private Rigidbody rb;
    public float MovementSpeed;
    public ItemGroup Inventory;
    public int Gold;
    private GameObject playerInventoryUI;

    private Joystick joystick { get { return Constants.Persistant.Joystick; } }
    private bool isDashing;
    private const float dashDuration = .3f;
    private float dashStartTime;
    private Vector3 dashDirection;
    public int Level
    {
        get
        {
            return GameState.Data.PlayerLevel;
        }
        set
        {
            GameState.Data.PlayerLevel = value;
        }
    }

    public int XP
    {
        get
        {
            return GameState.Data.PlayerXP;
        }
        set
        {
            GameState.Data.PlayerXP = value;
            if (XP >= maxLevelXP)
            {
                Level += 1;
                GameState.Data.PlayerXP = 0;
            }

            Constants.Persistant.XPBar.SetFillScale((float)XP / (float)maxLevelXP);
        }
    }

    private int maxLevelXP
    {
        get
        {
            return (int)(10 * Mathf.Pow(1.1f, Level));
        }
    }

    protected override void UpdateLoop()
    {
        SetDashStatus();
        base.UpdateLoop();
    }

    public override void Initialize() {
        base.Initialize();
        this.Allegiance = Allegiance.Player;
        this.Enemies = new HashSet<Allegiance>() {Allegiance.Cows};
        this.rb = this.GetComponent<Rigidbody>();
        this.Inventory = new ChestItemGroup("Inventory");
        this.Name = "Player";
        this.name = "Player";
        this.XP = GameState.Data.PlayerXP;
        this.PrimarySkill = new Spark();
    }

    protected override void SetInitialStats()
    {
        this.Health = 100;
        this.Damage = 1;
        this.AttackSpeed = 1f;
        this.MeleeAttackRange = 3f;
        this.RangedAttackRange = 12f;
        this.TargetFindRadius = 10f;
        this.MovementSpeed = 6f;
    }

    protected override void SetVelocity()
    {
        if (isDashing)
        {
            rb.velocity = dashDirection * MovementSpeed * 1.5f;
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
        statsMenu.Find("Background").GetComponent<Image>().color = Constants.UI.Colors.LightBase;
        statsMenu.Find("Outline").GetComponent<Image>().color = Constants.UI.Colors.Highlight;
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
        this.transform.position = Constants.Persistant.SpawnPoint;
        this.Health = MaxHealth;
        this.IsDead = false;
    }

    private void ShowDeathScreen()
    {
        UIActions.CloseAllWindows();
        Instantiate(Constants.Prefabs.DeathScreenUI, Constants.Persistant.InteractableUI);
    }
}
