using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearPlatform : Ground, IPuzzleObject {
    public void TurnOff()
    {
        gameObject.SetActive(true);
    }

    public void TurnOn()
    {
        gameObject.SetActive(false);
    }

}
