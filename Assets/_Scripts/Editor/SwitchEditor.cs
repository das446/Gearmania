using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PuzzleSwitch))]
[CanEditMultipleObjects]
public class SwitchEditor : Editor {
	public override void OnInspectorGUI() {
		PuzzleSwitch script = (PuzzleSwitch) target;
		DrawDefaultInspector();
		if(script.puzzle.GetComponent<IPuzzleObject>()==null){

			EditorGUILayout.HelpBox("Puzzle must implement IPuzzleObject interface", MessageType.Error);
		} 
	}
}