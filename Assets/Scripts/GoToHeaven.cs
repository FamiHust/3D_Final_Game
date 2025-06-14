using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHeaven : MonoBehaviour
{
    public GameObject background;
    public float x;
    public GameObject obj;

    void Start()
    {
        x = 250;
        background = GameObject.Find("Background");
        this.transform.SetParent(background.transform);
        this.transform.localScale = Vector3.one;
        SoundManager.PlaySound(SoundType.Shuffle);
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, x += 1500*Time.deltaTime, transform.position.z);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        Destroy(obj);
    }
}
