using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

    public readonly string[] CharacterKeys = new string[] { "Character00", "Character01", "Character02", "Character03" }; // NO HARDCODING!

    public Map CurrentMap { get; set; }

    public string PlayerName { get; set; }
    public string PlayerKey { get; set; }

}