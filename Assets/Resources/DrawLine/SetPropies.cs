using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPropies : MonoBehaviour {
    public Color m_color = new Color(1, 1, 1, 1);
    public float m_speed = 1.0f;

	// Use this for initialization
	void Start () {
        //GameObject line =GameObject.Instantiate(Resources.Load("DrawLine/Line") as GameObject);
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

        //Y轴缩放，速度值
        var uv2 = new Vector2[vertices.Length];
        for (int i = 0; i < uv2.Length; i++)
        {
            uv2[i] = new Vector2(transform.localScale.y, m_speed);
        }
        mesh.uv2 = uv2;
	}
}
