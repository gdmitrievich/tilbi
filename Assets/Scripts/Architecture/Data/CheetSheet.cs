using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/CheetSheet")]
public class CheetSheet : Item
{
	public List<string> hints;
}