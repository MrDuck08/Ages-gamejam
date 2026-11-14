using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyVisualizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hullRenderer;
    [SerializeField] private Sprite[] hullSprites;
    [SerializeField] private SpriteRenderer trackRenderer;
    [SerializeField] private Sprite[] trackSprites;

    private void Start()
    {
        hullRenderer.sprite = hullSprites[Random.Range(0, hullSprites.Length)];
        trackRenderer.sprite = trackSprites[Random.Range(0, trackSprites.Length)];
        Destroy(this);
    }
}
