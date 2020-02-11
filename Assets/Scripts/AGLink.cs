using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGLink : MonoBehaviour
{
    public void ArmorGamesPageLink()
    {
        Application.ExternalEval("window.open(\"https://armor.ag/MoreGames\")");
    }

    public void ArmorGamesFacebookPageLink()
    {
        Application.ExternalEval("window.open(\"https://www.facebook.com/ArmorGames\")");
    }
}
