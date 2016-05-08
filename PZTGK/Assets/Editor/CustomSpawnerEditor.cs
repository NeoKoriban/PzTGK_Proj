using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Spawner))]
public class CustomSpawnerEditor : Editor {

	Spawner spawner;
	SerializedObject GetTarget;
	SerializedProperty aiList;
	int aiListSize;
	List<bool> showAI;

	void OnEnable() {
		spawner = (Spawner)target;
		GetTarget = new SerializedObject (spawner);
		aiList = GetTarget.FindProperty ("enemyAI");
		showAI = new List<bool> ();
		for (int i=0; i<aiList.arraySize; i++) {
			showAI.Add (false);
		}
	}

	public override void OnInspectorGUI() {
		GetTarget.Update ();
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		EditorGUILayout.LabelField ("Define wave:", EditorStyles.boldLabel);
		spawner.startOfWave = EditorGUILayout.FloatField ("Start of wave (in seconds): ", spawner.startOfWave);
		spawner.numberOfEnemiesInWave = EditorGUILayout.IntField ("Number of enemies in wave: ", spawner.numberOfEnemiesInWave);
		spawner.enemySpawnInterval = EditorGUILayout.FloatField ("Interval between consecutive enemies spawns: ", spawner.enemySpawnInterval);
		aiListSize = aiList.arraySize;
		EditorGUILayout.Space ();
		EditorGUILayout.LabelField ("Define enemy AI: ", EditorStyles.boldLabel);
		aiListSize = EditorGUILayout.IntField ("Number of AI actions: ", aiListSize);
		if (aiListSize < 0)
			aiListSize = 0;
		if (aiListSize != aiList.arraySize) {
			while (aiListSize > aiList.arraySize) {
				aiList.InsertArrayElementAtIndex (aiList.arraySize);
				showAI.Add (false);
			}
			while (aiListSize < aiList.arraySize) {
				aiList.DeleteArrayElementAtIndex (aiList.arraySize - 1);
				if (showAI.Count > 0) {
					showAI.RemoveAt (showAI.Count - 1);
				}
			}
		}
		for (int i=0; i<aiList.arraySize; i++) { 
			showAI[i] = EditorGUILayout.Foldout(showAI[i], "AI action " + (i+1));
			if (showAI[i]) {
				SerializedProperty aiListRef = aiList.GetArrayElementAtIndex(i);
				SerializedProperty finalPointCoords = aiListRef.FindPropertyRelative("finalPointCoords");
				SerializedProperty lengthOfAction = aiListRef.FindPropertyRelative("lengthOfAction");
				SerializedProperty typeOfMove = aiListRef.FindPropertyRelative("typeOfMove");
				SerializedProperty shootInterval = aiListRef.FindPropertyRelative("shootInterval");

				EditorGUILayout.PropertyField(finalPointCoords);
				EditorGUILayout.PropertyField(lengthOfAction);
				EditorGUILayout.PropertyField(typeOfMove);
				EditorGUILayout.PropertyField(shootInterval);
				EditorGUILayout.Space();
			}
		}
		GetTarget.ApplyModifiedProperties ();
	}

}
