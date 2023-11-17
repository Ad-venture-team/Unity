using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLineAttackPreview : MonoBehaviour
{
    public LayerMask raycastLayer;

    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    private Mesh mesh;

    private float maxDuration;
    private float duration;

    List<Vector2> points = new List<Vector2>();

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        duration += Time.deltaTime;
    }
    public void DrawLinePreview(Vector2 _origine, Vector2 _dir, float _range, float _width)
    {
        mesh = new Mesh();
        mesh.name = "NewMesh";
        meshFilter.mesh = mesh;
        transform.position = _origine;
        meshRenderer.sharedMaterial.SetFloat("_Range", _range);
        Vector2 relativeOrigine = _origine - (Vector2)transform.position;
        Vector2 relativeDir = _dir - (Vector2)transform.position;
        points = new List<Vector2>();
        points.AddRange(CalculateMesh(relativeOrigine, _dir, _range, _width));
        DrawMesh(points);
    }

    private List<Vector2> CalculateMesh(Vector3 _origine, Vector3 _dir, float _range, float _width)
    {

        List<Vector2> points = new List<Vector2>();

        //Calculate maxRange
        Vector3 targetDir;// = (Vector3)_origine * _dir;
        RaycastHit2D hit;
        Vector3 maxRange;
        //Debug.DrawLine(transform.position, transform.position + _dir.normalized * _range, Color.red,5);
        hit = Physics2D.Raycast(transform.position, _dir.normalized, _range, raycastLayer);
        if (hit.collider != null)
            maxRange = transform.InverseTransformPoint(hit.point);
        else
            maxRange = _dir.normalized * _range;

        //P0
        targetDir = _origine + Quaternion.Euler(0, 0, 90) * _dir.normalized;
        Debug.DrawLine(transform.position, transform.position + targetDir.normalized * _width/2, Color.green, 5);
        Vector2 p0 = targetDir * _width/2;
        points.Add(p0);

        //P1
        targetDir = _origine + Quaternion.Euler(0, 0, -90) * _dir.normalized;
        Debug.DrawLine(transform.position, transform.position + targetDir.normalized * _width / 2, Color.green, 5);
        Vector2 p1 = targetDir * _width / 2;
        points.Add(p1);

        //P3
        targetDir = _origine + Quaternion.Euler(0, 0, -90) * _dir.normalized;
        Debug.DrawLine(transform.position, transform.position + targetDir.normalized * _width / 2, Color.green, 5);
        Vector2 p3 = maxRange + targetDir * _width / 2;
        points.Add(p3);

        //P2
        targetDir = _origine + Quaternion.Euler(0, 0, 90) * _dir.normalized;
        Debug.DrawLine(transform.position, transform.position + targetDir.normalized * _width / 2, Color.green, 5);
        Vector2 p2 = maxRange + targetDir * _width / 2;
        points.Add(p2);


        return points;
    }

    private void DrawMesh(List<Vector2> _points)
    {
        int vertexCount = _points.Count;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        for (int i = 0; i < vertexCount; i++)
        {
            vertices[i] = _points[i];
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        List<int> tempTriangle = triangles.ToList();
        tempTriangle.Reverse();
        mesh.triangles = tempTriangle.ToArray();
        //mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }

    public void SetValue(float _duration, Action onEnd = null)
    {
        maxDuration = _duration;
        duration = 0;
        StartCoroutine(PlayPreview(onEnd));
    }

    private IEnumerator PlayPreview(Action onEnd)
    {
        while (duration <= maxDuration)
        {
            ChangeShaderValue(duration / maxDuration);
            yield return new WaitForFixedUpdate();
        }
        mesh.Clear();
        onEnd?.Invoke();
    }
    private void ChangeShaderValue(float t)
    {
        meshRenderer.sharedMaterial.SetFloat("_Value", t);
    }
}
