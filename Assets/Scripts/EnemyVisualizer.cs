using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyVisualizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hullRenderer;
    [SerializeField] private Sprite[] hullSprites;
    [SerializeField] private Animation trackRenderer;
    [SerializeField] private AnimationClip[] trackClips;

    private void Start()
    {
        hullRenderer.sprite = hullSprites[Random.Range(0, hullSprites.Length)];
        trackRenderer.clip = trackClips[Random.Range(0, trackClips.Length)];
        Destroy(this);
    }
}
