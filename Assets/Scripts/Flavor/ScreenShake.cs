using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [Range(0f, .5f)]
    public float shakePeriod = .3f;
    public float shakeTime = 3f;
    //public float shakeDst = 1;

    IEnumerator currCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //Shake();
        }
    }

    public void Shake(float shakeDst)
    {
        StopAllCoroutines();
        currCoroutine = ShakeC(shakeDst);
        StartCoroutine(currCoroutine);
    }

    IEnumerator ShakeC(float shakeDst)
    {
        Vector3 start = transform.position;
        Vector3 peak =  transform.position + Vector3.up * shakeDst ;

        float time = 0;
        while (time < shakeTime) {
            Vector3 cameraPos = Vector3.Lerp(start, peak, Mathf.Abs(Mathf.Sin(time *  Mathf.PI / shakePeriod)));
            cameraPos.y = cameraPos.y * Mathf.Sign(Mathf.Sin(time * Mathf.PI / shakePeriod));
            transform.position = cameraPos;
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = start;
    }
}
