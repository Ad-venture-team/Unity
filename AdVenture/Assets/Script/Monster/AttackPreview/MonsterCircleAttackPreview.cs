using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MonsterCircleAttackPreview : MonoBehaviour
{
    public int resolution;
    public LayerMask raycastLayer;

    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    private Mesh mesh;

    private float maxDuration;
    public float duration;

    List<Vector2> points = new List<Vector2>();

    private void Update()
    {
        duration += Time.deltaTime;
    }
    public void DrawCirclePreview(Transform _origine, Vector2 _dir, float _angle, float _radius)
    {
        mesh = new Mesh();
        mesh.name = "NewMesh";
        meshFilter.mesh = mesh;
        meshRenderer.sharedMaterial.SetFloat("_Range", _radius);
        transform.SetParent(_origine);
        Vector2 relativeOrigine = _origine.position - transform.position;
        Vector2 relativeDir = _dir - (Vector2)transform.position;
        points = new List<Vector2>();
        points.AddRange(CalculateMesh(relativeOrigine, relativeDir, _angle, _radius));
        DrawMesh(points);
    }

    private List<Vector2> CalculateMesh(Vector2 _origine, Vector2 _dir, float _angle, float _radius)
    {

        List<Vector2> points = new List<Vector2>();

        //Calculate raypoint
        int nRay = (int)_angle * resolution;
        for (int i = 0; i <= nRay; i++)
        {
            float currentAngle = ((_angle / (nRay - 1)) * i) - (_angle / 2);
            Vector3 targetDir = (Vector3)_origine + Quaternion.Euler(0, 0, currentAngle) * _dir;
            //Debug.DrawLine(transform.position, transform.position + targetDir.normalized * _radius,Color.red,5);

            RaycastHit hit;
            Vector3 result;
            if (Physics.Raycast(transform.position, targetDir.normalized, out hit, _radius, raycastLayer))
                result = hit.point;
            else
                result = targetDir.normalized * _radius;

            points.Add(result);
        }

        return points;
    }

    private void DrawMesh(List<Vector2> _points)
    {
        int vertexCount = _points.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector2.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = _points[i];
            if(i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i+1;
                triangles[i * 3 + 2] = i+2;
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        List<int> tempTriangle = triangles.ToList();
        tempTriangle.Reverse();
        mesh.triangles = tempTriangle.ToArray();
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

    private struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;
        public ViewCastInfo(bool _hit, Vector3 _point,float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
}
