﻿using DG.Tweening;
using UnityEngine;

public class ShieldBehaviour : ExtraWeaponBehaviour
{
    private Sequence _sequence;

    public float distance;
    public GameObject Player;
    public GameObject ShieldProjectile;
    public GameObject ShieldSpawnPosition;
    public float travelTime;

    public override void TriggerEffect()
    {
        ShieldProjectile.SetActive(true);
        var spawnPosition = ShieldSpawnPosition.transform.position;
        ShieldProjectile.transform.position = spawnPosition;

        var targetPosition = spawnPosition + Player.transform.forward * distance;

        _sequence?.Kill();
        _sequence = DOTween.Sequence();
        _sequence.Append(ShieldProjectile.transform.DOMove(targetPosition, travelTime));
        _sequence.Append(DOVirtual.Float(0, 1, travelTime,
            f =>
            {
                ShieldProjectile.transform.position =
                    Vector3.Lerp(targetPosition, ShieldSpawnPosition.transform.position, f);
            }));
        _sequence.AppendCallback(() => ShieldProjectile.SetActive(false));
        _sequence.Play();
    }

    private void Start()
    {
        ShieldProjectile.SetActive(false);
    }
}