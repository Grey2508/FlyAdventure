using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawner))]
public class SpawnerInspector : Editor
{
    private void OnSceneGUI()
    {
        Spawner spawner = target as Spawner;

        Quaternion handleRotation = Quaternion.identity; //rotate of gizmos

        Vector2[] points = new Vector2[spawner.PointsCount];

        for (int i = 0; i < spawner.PointsCount; i++)
        {
            points[i] = spawner.GetPoint(i);
        }

        for (int i = 0; i < points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            Vector2 point = Handles.DoPositionHandle(points[i], handleRotation);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spawner, "Move Point");
                EditorUtility.SetDirty(spawner);

                spawner.SetPoint(i, point);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Spawner spawner = target as Spawner;

        if (GUILayout.Button("Add point"))
        {
            Undo.RecordObject(spawner, "Add point");
            spawner.AddPoint();
            EditorUtility.SetDirty(spawner);
        }

        if (GUILayout.Button("Remove last point"))
        {
            Undo.RecordObject(spawner, "Remove last point");
            spawner.RemovePoint();
            EditorUtility.SetDirty(spawner);
        }
    }
}
