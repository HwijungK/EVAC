using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaunchSite : Building
{
    AimPredictor ap;
    public ForceObject spaceCraft;
    public Transform head;

    // Dynamic on Upgrades
    
    [HideInInspector] public int maxProjectileCharges = 1;
    [HideInInspector] public float rechargeTime = 5f;

    public float reloadTime = 1;
    public int projectileCharges;
    float lastChargeTime;
    float lastFireTime;

    //Bullet Bar
    public Transform bulletBarPrefab;
    public int bulletBarCount;
    public float bulletBarDstFromBuilding;
    public float bulletBarGapDst;
    Transform[] bulletBars;
    /*Color emptyColor;
    Color endLerpColor;
    Color chargedColor;*/

    public SoundSO launchSound;

    public override void Awake()
    {
        base.Awake();
        ap = GetComponent<AimPredictor>();
        ap.SetForceObject(spaceCraft);

        // Instantiate BUllet Bars
        bulletBars = new Transform[bulletBarCount];
        for (int i = 0; i < bulletBarCount; i++)
        {
            bulletBars[i] = Instantiate(bulletBarPrefab, transform, false).transform;
            bulletBars[i].SetLocalPositionAndRotation(Vector2.up * (bulletBarDstFromBuilding + i * bulletBarGapDst), Quaternion.identity);
            bulletBars[i].gameObject.SetActive(false);
        }

    }
    public override void Start()
    {
        base.Start();
        projectileCharges = 0;
        lastChargeTime = Time.time - rechargeTime;
        lastFireTime = 0;
    }

    public override void Update()
    {
        // Recharge;
        if (projectileCharges < maxProjectileCharges)
        {
            if (lastChargeTime + rechargeTime < Time.time)
            {
                projectileCharges++;
                bulletBars[projectileCharges - 1].gameObject.SetActive(true);
                lastChargeTime = Time.time;
            }
        }

        
        if (selected)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            //if (lastFireTime + reloadTime <= Time.time) print("Reload working");
            //if (projectileCharges > 0) print("Projectile Charge exists");
            if (Input.GetMouseButtonDown(0) && lastFireTime + reloadTime <= Time.time && projectileCharges > 0)
            {
                lastFireTime = Time.time;
                if (projectileCharges == maxProjectileCharges)
                {
                    // prevents instead charging when shooting from full
                    lastChargeTime = Time.time;
                }
                projectileCharges--;
                bulletBars[projectileCharges].gameObject.SetActive(false);

                Shoot();
            }
        }
        base.Update();
    }
    public override void Select(bool select)
    {
        // checks that this is still a thing
        if (this != null && transform != null)
        {
            base.Select(select);
            if (ap != null)
            {
                ap.enabled = select;
            }
            if (TryGetComponent<LauncherHeadMover>(out LauncherHeadMover mover))
            {
                mover.enabled = select;
            }
        }
        
        
        
    }
    
    public void Shoot()
    {
        Vector2 vel = ap.GetStartingVelocity();
        Instantiate(spaceCraft, transform.position, Quaternion.identity).startingVelocity = vel;
        

        SoundManager.instance.PlaySound(launchSound);
    }

    public override void Upgrade()
    {
        base.Upgrade();
    }


    public override void SetUpgradeStats()
    {
        base.SetUpgradeStats();
        rechargeTime = buildingUpgradeSO.data[level - 1].floatData[0];
        maxProjectileCharges = buildingUpgradeSO.data[level - 1].intData[1];
        if (head != null && buildingUpgradeSO.data[level - 1].sprites.Length > 0)
        {
            head.GetComponent<SpriteRenderer>().sprite = buildingUpgradeSO.data[level - 1].sprites[1];
        }
    }

}
