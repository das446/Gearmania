using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitchOneTime : PuzzleSwitch {

	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.GetComponent<Gear>()) {
			other.transform.parent = null;
			other.transform.rotation = Quaternion.identity;
			//see enableCol
		}
	}

	/// <summary>
	/// Callback to draw gizmos only if the object is selected.
	/// </summary>
	void OnDrawGizmosSelected()
	{
		
	}
}
