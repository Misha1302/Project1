using System.Collections;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform[] objects;
    [SerializeField] private float[] durations;

    private void FixedUpdate()
    {
        for (var i = 0; i < objects.Length; i++)
            StartCoroutine(Move(GameManager.Instance.PlayerController.transform.position, objects[i], durations[i]));
    }

    private static IEnumerator Move(Vector3 transformPosition, Transform o, float duration)
    {
        yield return new WaitForSeconds(duration);
        o.position = transformPosition;
    }
}