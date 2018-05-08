using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour
{

    private BlockBreakerLevelManager LevelManager;

    void OnTriggerEnter2D(Collider2D trigger)
    {
        LevelManager = GameObject.FindObjectOfType<BlockBreakerLevelManager>();
        LevelManager.LoadLevel("BB_Lose Screen");
    }
}
