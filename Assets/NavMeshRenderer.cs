using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshRenderer : MonoBehaviour
{
	void Start() {
		GetComponent<NavMeshSurface>().BuildNavMesh();
	}
}
