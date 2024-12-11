using ReadyPlayerMe.Samples.QuickStart;
using System.Collections;
using UnityEngine;

public class TriggerRotatePlatform : MonoBehaviour
{
    private ThirdPersonController _controller;
    Coroutine coroutine;
    [SerializeField] private GameObject rotatePlatform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out _controller))
        {
            if (coroutine == null)
            {
                coroutine = StartCoroutine(Rotate(-90f, 1f));
            }
        }
    }
    IEnumerator Rotate(float angle, float intensity)
    {
        var me = rotatePlatform.transform;
        var to = me.rotation * Quaternion.Euler(0.0f, angle, 0.0f);
        while (true)
        {
            me.rotation = Quaternion.Lerp(me.rotation, to, intensity * Time.deltaTime);
            if (Quaternion.Angle(me.rotation, to) < 0.01f)
            {
                coroutine = null;
                me.rotation = to;
                yield break;
            }
        }
    }
}
