using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OpenPack : MonoBehaviour
{
    public float updated;
    public float max;
    public Image bar;

    public GameObject prefab;
    public GameObject pack;
    public GameObject c1;
    public GameObject c2;
    public GameObject c3;
    public GameObject c4;
    public GameObject c5;

    public int clickedCard;

    void Start()
    {
        max = 100f;
        updated = 100f;

        StartCoroutine(Wait());
    }

    void Update()
    {
        bar.fillAmount = updated / max;

        if (updated < 0)
        {
            updated = 0;
        }

        updated -= 50 * Time.deltaTime;

        if (clickedCard == 5)
        {
            StartCoroutine(Return());
        }
    }

    public void Click()
    {
        clickedCard++;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(prefab, pack.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.8f);
        Instantiate(prefab, pack.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.8f);
        Instantiate(prefab, pack.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.8f);
        Instantiate(prefab, pack.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.8f);
        Instantiate(prefab, pack.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1.5f);
        Destroy(pack);

        yield return new WaitForSeconds(0.5f);
        c1.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        c2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        c3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        c4.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        c5.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Return()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Shop");
    }
}
