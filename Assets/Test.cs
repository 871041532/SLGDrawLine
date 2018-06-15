using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    private Stack<int> m_lineHandles = new Stack<int>();
	void Start () {
        m_lineHandles.Push(LineManager.Instance.DrawLine(new Vector3(-10,0,0),new Vector3(10,0,0),Color.green,1));
        m_lineHandles.Push(LineManager.Instance.DrawLine(new Vector3(0, 0, -10), new Vector3(0, 0, 10), Color.green, 1));
        m_lineHandles.Push(LineManager.Instance.DrawLine(new Vector3(0, -10, 0), new Vector3(0, 10, 0), Color.green, 1));
    }
	
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100,50),"生成"))
        {
            m_lineHandles.Push(LineManager.Instance.DrawLine(new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f)), new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f)), new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f),1), Random.Range(0f, 8f)));
        }
        if (GUI.Button(new Rect(100, 0, 100, 50), "回收"))
        {
            if (m_lineHandles.Count>0)
            {
                LineManager.Instance.BackLine(m_lineHandles.Pop());
            }
        }
    }
}
