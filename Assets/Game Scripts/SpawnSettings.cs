﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType { CHEDDAR, SWISS, AMERICAN, HAM, TURKEY, PEPPERONI, SALAMI, LETTUCE, ONION, TOMATO, TOPBREAD, BOTTOMBREAD};

public class SpawnSettings : MonoBehaviour
{
    public SpawnType type;
}
