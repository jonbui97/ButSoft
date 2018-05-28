using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 3f;
    public Vector3 Offset = new Vector3(0, 0.01f, 0);
    // Use this for initialization
    void Start () {
		Destroy(gameObject, DestroyTime);
        transform.localPosition += Offset;
        StartCoroutine(ResetCount());
    }

    private IEnumerator ResetCount()
    {
        yield return new WaitForSeconds(DestroyTime);
    }
}
