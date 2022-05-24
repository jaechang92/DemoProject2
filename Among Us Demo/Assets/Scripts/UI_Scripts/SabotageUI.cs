using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum E_Sabotage
{
    sabotage_reactor,
    sabotage_O2,
    sabotage_electricity,
    sabotage_comms,
    sabotage_Doors,
}
public class SabotageUI : MonoBehaviour
{
    public E_Sabotage sabotage;
}
