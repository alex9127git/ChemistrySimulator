using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinerator : Factory
{
    protected override IEnumerator ProduceItems()
    {
        while (true)
        {
            inputItems.Clear();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
