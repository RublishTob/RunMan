using ReadyPlayerMe.Samples.QuickStart;
using System.Collections;
using UnityEngine;

public class TriggerRotatePlatform : MonoBehaviour
{
    private ThirdPersonController _controller;
    [SerializeField] private GameObject rotatePlatform;

    private Transform me;
    private Quaternion to;

    [SerializeField] private bool doRotation = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out _controller))
        {
            doRotation = true;
        }
    }
    private void Start()
    {
        me = rotatePlatform.transform;
        to = me.rotation * Quaternion.Euler(0.0f, -90f, 0.0f);
    }
    private void Update()
    {
        if (doRotation == false)
        {
            return;
        }
        me.rotation = Quaternion.Lerp(me.rotation, to, Time.deltaTime);
        if (Quaternion.Angle(me.rotation, to) < 0.01f)
        {
            me.rotation = to;
            doRotation = false;
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
                //coroutine = null;
                me.rotation = to;
                yield break;
            }
        }
    }
}
