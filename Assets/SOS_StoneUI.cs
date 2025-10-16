using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SOS_StoneUI : MonoBehaviour
{
    public Text stoneText;

    public int stoneCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateStone();
    }

    public void StoneGrab(GameObject stone)
    {
        StartCoroutine(GrabSTone(stone));
    }
    IEnumerator GrabSTone(GameObject stone)
    {
        if (stoneCount <= 44)
        {
            stoneCount++;
            UpdateStone();
            yield return new WaitForSeconds(0);
            Destroy(stone);
        }
    }

    public void UpdateStone()
    {
        stoneText.text = stoneCount + "/44";
    }
}