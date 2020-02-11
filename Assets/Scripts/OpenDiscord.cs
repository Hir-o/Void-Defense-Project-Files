using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDiscord : MonoBehaviour
{
    public void OpenDiscordLink() { Application.ExternalEval("window.open(\"https://discord.gg/RKDSWcb\")"); }
}