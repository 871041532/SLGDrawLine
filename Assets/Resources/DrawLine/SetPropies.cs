using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPropies : MonoBehaviour {
    public Color m_color = new Color(1, 1, 1, 1);
    public float m_speed = 1.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Transform transform = GetComponent<Transform>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        var vertices = mesh.vertices;
        //颜色
        var colors = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = m_color;
        }
        mesh.colors = colors;

        //法线是Y轴拉长
        var normals = new Vector3[vertices.Length];
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = new Vector3(1.0f/transform.localScale.y, 0, 0);
        }
        mesh.normals = normals;

        //切线是速度
        var tangents = new Vector4[vertices.Length];
        for (int i = 0; i < tangents.Length; i++)
        {
            tangents[i] = new Vector4(1.0f/m_speed,0,0,0);
        }
        mesh.tangents = tangents;
	}
}
