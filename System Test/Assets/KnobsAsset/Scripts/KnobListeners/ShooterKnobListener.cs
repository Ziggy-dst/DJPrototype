using System;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace KnobsAsset
{
    /// <summary>
    /// Knob listener for assigning a knob value to change the transform values of an object.
    /// </summary>
    public class ShooterKnobListener : KnobListener
    {
        [System.Serializable]
        private enum VariableTypes
        {
            DETECT_ANGLE, DETECT_RADIUS, DAMAGE_PER_BULLET, MOVE_SPEED, SHOOT_INTERVAL, BULLET_SPEED
        }
        
        [Tooltip("The game object that the knob will affect")]
        [SerializeField] private GameObject gameObjectToManipulate = default;

        [Tooltip("The part of the variable to affect positively")]
        [SerializeField] private VariableTypes PositiveVariableTypes = VariableTypes.DETECT_ANGLE;
        
        [Tooltip("The part of the variable to affect negatively")]
        [SerializeField] private VariableTypes NegativeVariableTypes = VariableTypes.DETECT_ANGLE;

        [Tooltip("Minimum value to set the positive value to")]
        [SerializeField] private float MinValueForPositive = 0;

        [Tooltip("Maximum value to set the positive value to")]
        [SerializeField] private float MaxValueForPositive = 0;
        
        [Tooltip("Minimum value to set the negative value to")]
        [SerializeField] private float MinValueForNegative = 0;

        [Tooltip("Maximum value to set the negative value to")]
        [SerializeField] private float MaxValueForNegative = 0;

        [Tooltip("Whether or not the min and max values are adding to the initial values of the variable")]
        [SerializeField] private bool RelativeToInitialValue = true;

        private float initialDetectAngle;
        private float initialDetectRadius;
        private float initialDamagePerBullet;
        private float initialBulletSpeed;
        private float initialMoveSpeed;
        private float initialShootInterval;
        private GameObject playerGunInstance;
        private GameObject playerBulletPool;
        private List<DamageOnTouch> playerBulletInstanceDamageOnTouches;
        private List<Projectile> playerBulletInstanceProjectiles;

        void Awake()
        {
            if (gameObjectToManipulate == null)
            {
                Debug.LogException(new MissingReferenceException("A Game Object to manipulate is required"), this);
                return;
            }

            initialDetectAngle = gameObjectToManipulate.GetComponent<MMConeOfVision>().VisionAngle;
            initialDetectRadius = gameObjectToManipulate.GetComponent<MMConeOfVision>().VisionRadius;
            initialDamagePerBullet = gameObjectToManipulate.GetComponent<CharacterHandleWeapon>().InitialWeapon
                .GetComponent<MMSimpleObjectPooler>().GameObjectToPool.GetComponent<DamageOnTouch>().MaxDamageCaused;
            initialBulletSpeed = gameObjectToManipulate.GetComponent<CharacterHandleWeapon>().InitialWeapon
                .GetComponent<MMSimpleObjectPooler>().GameObjectToPool.GetComponent<Projectile>().Speed;
            initialMoveSpeed = gameObjectToManipulate.GetComponent<CharacterMovement>().MovementSpeed;
            initialShootInterval = gameObjectToManipulate.GetComponent<CharacterHandleWeapon>().InitialWeapon
                .GetComponent<ProjectileWeapon>().TimeBetweenUses;
        }

        private void Start()
        {
            playerBulletInstanceDamageOnTouches = new List<DamageOnTouch>();
            playerBulletInstanceProjectiles = new List<Projectile>();
        }

        private void Update()
        {
            if (playerBulletPool == null) playerBulletPool = GameObject.Find("[SimpleObjectPooler] PlayerBullet");
            foreach (var damageOnTouch in playerBulletPool.GetComponentsInChildren<DamageOnTouch>())
            {
                if(!playerBulletInstanceDamageOnTouches.Contains(damageOnTouch)) playerBulletInstanceDamageOnTouches.Add(damageOnTouch);
            }
            foreach (var projectile in playerBulletPool.GetComponentsInChildren<Projectile>())
            {
                if(!playerBulletInstanceProjectiles.Contains(projectile)) playerBulletInstanceProjectiles.Add(projectile);
            }
            if(playerGunInstance == null) playerGunInstance = GameObject.Find("PlayerGun");
        }

        public override void OnKnobValueChange(float knobPercentValue)
        {
            float positiveVariableValue = Mathf.Lerp(MinValueForPositive, MaxValueForPositive, knobPercentValue);
            switch (PositiveVariableTypes)
            {
                case VariableTypes.DETECT_ANGLE:
                    gameObjectToManipulate.GetComponent<MMConeOfVision>().VisionAngle = positiveVariableValue + (RelativeToInitialValue ? initialDetectAngle : 0);
                    break;
                case VariableTypes.DETECT_RADIUS:
                    gameObjectToManipulate.GetComponent<MMConeOfVision>().VisionRadius = positiveVariableValue + (RelativeToInitialValue ? initialDetectRadius : 0);
                    break;
                case VariableTypes.DAMAGE_PER_BULLET:
                    if (playerBulletInstanceDamageOnTouches != null)
                    {
                        foreach (var bulletDamageOnTouch in playerBulletInstanceDamageOnTouches)
                        {
                            bulletDamageOnTouch.GetComponent<DamageOnTouch>().MaxDamageCaused = positiveVariableValue + (RelativeToInitialValue ? initialDamagePerBullet : 0);
                            bulletDamageOnTouch.GetComponent<DamageOnTouch>().MinDamageCaused = positiveVariableValue + (RelativeToInitialValue ? initialDamagePerBullet : 0);
                        }
                    }
                    break;
                case VariableTypes.BULLET_SPEED:
                    if (playerBulletInstanceProjectiles != null)
                    {
                        foreach (var bulletProjectile in playerBulletInstanceProjectiles)
                        {
                            bulletProjectile.GetComponent<Projectile>().Speed = positiveVariableValue + (RelativeToInitialValue ? initialDamagePerBullet : 0);
                        }
                    }
                    break;
                case VariableTypes.MOVE_SPEED:
                    gameObjectToManipulate.GetComponent<CharacterMovement>().MovementSpeed = positiveVariableValue + (RelativeToInitialValue ? initialMoveSpeed : 0);
                    break;
                case VariableTypes.SHOOT_INTERVAL:
                    if(playerGunInstance != null) playerGunInstance.GetComponent<ProjectileWeapon>().TimeBetweenUses = positiveVariableValue + (RelativeToInitialValue ? initialShootInterval : 0);
                    break;
                default:
                    Debug.LogException(new System.InvalidOperationException("Invalid PositiveVariableTypes value " + PositiveVariableTypes), this);
                    return;
            }
            
            float negativeVariableValue = Mathf.Lerp(MaxValueForNegative, MinValueForNegative, knobPercentValue);
            switch (NegativeVariableTypes)
            {
                case VariableTypes.DETECT_ANGLE:
                    gameObjectToManipulate.GetComponent<MMConeOfVision>().VisionAngle = negativeVariableValue + (RelativeToInitialValue ? initialDetectAngle : 0);
                    break;
                case VariableTypes.DETECT_RADIUS:
                    gameObjectToManipulate.GetComponent<MMConeOfVision>().VisionRadius = negativeVariableValue + (RelativeToInitialValue ? initialDetectRadius : 0);
                    break;
                case VariableTypes.DAMAGE_PER_BULLET:
                    if (playerBulletInstanceDamageOnTouches != null)
                    {
                        foreach (var bulletDamageOnTouch in playerBulletInstanceDamageOnTouches)
                        {
                            bulletDamageOnTouch.GetComponent<DamageOnTouch>().MaxDamageCaused = negativeVariableValue + (RelativeToInitialValue ? initialDamagePerBullet : 0);
                            bulletDamageOnTouch.GetComponent<DamageOnTouch>().MinDamageCaused = negativeVariableValue + (RelativeToInitialValue ? initialDamagePerBullet : 0);
                        }
                    }
                    break;
                case VariableTypes.BULLET_SPEED:
                    if (playerBulletInstanceProjectiles != null)
                    {
                        foreach (var bulletProjectile in playerBulletInstanceProjectiles)
                        {
                            bulletProjectile.GetComponent<Projectile>().Speed = negativeVariableValue + (RelativeToInitialValue ? initialDamagePerBullet : 0);
                        }
                    }
                    break;
                case VariableTypes.MOVE_SPEED:
                    gameObjectToManipulate.GetComponent<CharacterMovement>().MovementSpeed = negativeVariableValue + (RelativeToInitialValue ? initialMoveSpeed : 0);
                    break;
                case VariableTypes.SHOOT_INTERVAL:
                    if(playerGunInstance != null) playerGunInstance.GetComponent<ProjectileWeapon>().TimeBetweenUses = negativeVariableValue + (RelativeToInitialValue ? initialShootInterval : 0);
                    break;
                default:
                    Debug.LogException(new System.InvalidOperationException("Invalid PositiveVariableTypes value " + PositiveVariableTypes), this);
                    return;
            }
        }
    }
}
