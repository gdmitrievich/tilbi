using UnityEditor;

[CustomEditor(typeof(Test))]
[CanEditMultipleObjects]
public class TestEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Test test = (Test)target;

		EditorGUILayout.Toggle("Is Replayable", test.IsReplayable);
		EditorGUILayout.Toggle("Is Faild", test.IsFaild);
	}
}