using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    Transform tf;
    Transform activeTf;

    SpriteRenderer thisSprite;
    SpriteRenderer sprite;

    Color color;

    public float activeTime;
    public float activeStart;

    float alpha;
    public float alphaSet;
    public float alphaMultiplier;
    // Start is called before the first frame update
    void OnEnable()
    {
        tf = GameObject.FindGameObjectWithTag("Logo").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        sprite = tf.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        thisSprite.sprite = sprite.sprite;
        transform.position = tf.position;
        transform.localScale = tf.localScale;
        //transform.rotation = tf.rotation * 0.8f;
        Quaternion r = Quaternion.Euler(0,0, tf.rotation.z * 0.8f);

        transform.rotation = r;

        activeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1, 1, 1, alpha);
        thisSprite.color = color;
        if (Time.time >= activeStart + activeTime)
        {
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
