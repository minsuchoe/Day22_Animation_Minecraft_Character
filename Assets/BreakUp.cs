using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakUp : MonoBehaviour
{
    public Texture[] cracks;
    public ParticleSystem fx;

    int numHits = 0;
    float lastHitTime;
    float hitTimeThreadhold = 0.2f;

    public void Hit()
    {
        if (lastHitTime + hitTimeThreadhold > Time.time)
        {
            numHits++;
            CancelInvoke();
            if (numHits < cracks.Length)
                GetComponent<Renderer>().material.SetTexture("_DetailMask", cracks[numHits]);
            else
            {
                Destroy(gameObject);
                var clone = Instantiate(fx, transform.position, Camera.main.transform.rotation);  //Quaternion.identity
                Destroy(clone.gameObject, 2f);
            }
            Invoke("Heal", 2);
        }
        lastHitTime = Time.time;
    }

    void Heal()
    {
        numHits = 0;
        GetComponent<Renderer>().material.SetTexture("_DetailMask", cracks[0]);
    }
}
