using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float animationCooldown = 1f;
    [SerializeField] float currentTimer = 1f;
    [SerializeField] float adjustSpeed = -2f;
    
    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (0 >= currentTimer)
        {
            animator.SetTrigger("Hit");
            currentTimer = animationCooldown;
            levelGenerator.ChangeChunkMoveSpeed(adjustSpeed);
        }
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
    }
}
