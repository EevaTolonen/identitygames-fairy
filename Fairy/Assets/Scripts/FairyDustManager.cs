using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyDustManager : MonoBehaviour
{
    public GameObject[] dusts;
    bool collectableDusts = true;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        dusts = new GameObject[3];
    }

    // Update is called once per frame
    void Update()
    {
       // CanPlayerDraw();
    }

    // siis: kerätään viivoja, kun niitä ilmestyy, kun kerätty index 2, ei voi piirtää lisää
    // kun ekan piirtämisestä kulunut 3s, poistetaan se listasta ja siirretään loppuja indeksi
    // taaksepäin, ja voi piirtää

    private void CanPlayerDraw()
    {
        while (collectableDusts)
        {
            GameObject dust = GameObject.Find("Keijupoly");
            if (dust != null) dusts[index] = dust;
            else collectableDusts = false;
        }
    }
}
