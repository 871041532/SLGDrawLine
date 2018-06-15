using System.Collections.Generic;
using UnityEngine;
using LineHandle = System.Int32;

class LineManager
{
    private GameObject m_root = null; 
    private GameObject m_origin = null;
    private Stack<GameObject> m_linePool = null;
    private Dictionary<int, GameObject> m_drawLines = null;
    private int m_index = 0;

    #region 外部接口
    //参数：起始点、终止点、颜色、速度
    public LineHandle DrawLine(Vector3 start,Vector3 end,Color color,float speed)
    {
        //从池中拿出
        GameObject line = getFromPool();
        Transform lineTransform = line.transform;
        Transform childTransform = lineTransform.GetChild(0);

        Mesh mesh = childTransform.GetComponent<MeshFilter>().mesh;
        var vertices = mesh.vertices;
        
        //设置颜色
        var colors = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }
        mesh.colors = colors;

        //设置浮transform位置朝向，子transform缩放
        Vector3 position = new Vector3((start.x+end.x)/2,(start.y+end.y)/2,(start.z+end.z)/2);
        Vector3 scale =new Vector3(1,Vector3.Distance(start,end),1);
        Vector3 direction = (end - start).normalized;
        lineTransform.localPosition = position;
        childTransform.localScale = scale;
        lineTransform.localRotation = Quaternion.LookRotation(direction);

        //设置Y轴缩放，速度值
        var uv2 = new Vector2[vertices.Length];
        for (int i = 0; i < uv2.Length; i++)
        {
            uv2[i] = new Vector2(childTransform.localScale.y / childTransform.localScale.x, speed);
        }
        mesh.uv2 = uv2;

        //放入字典
        m_drawLines.Add(++m_index, line);

        return m_index;
    }
    //参数：索引
    public void BackLine(LineHandle handle)
    {
        if(m_drawLines.ContainsKey(handle))
        {
            GameObject line = m_drawLines[handle];
            m_drawLines.Remove(handle);
            backToPool(line);
        }
    }
    #endregion

    #region 内部实现
    private void Init()
    {
        m_origin = Resources.Load("DrawLine/Line") as GameObject;
        m_linePool = new Stack<GameObject>(23);
        m_root = new GameObject("LineRoot");
        m_drawLines = new Dictionary<int, GameObject>(23);
        m_index = 0;
    }

    private GameObject getFromPool()
    {
        if (m_linePool.Count==0)
        {
            GameObject gameObject = GameObject.Instantiate(m_origin);
            gameObject.transform.parent = m_root.transform;
            return gameObject;
        }
        else
        {
            return m_linePool.Pop();
        }
    }

    private void backToPool(GameObject line)
    {
        line.transform.localPosition = new Vector3(1000,1000,1000);
        m_linePool.Push(line);
    }
    #endregion

    #region singleton
    private static LineManager _instance = null;
    private LineManager() { Init(); }
    public static LineManager Instance
    {
        get {
            if (_instance==null)
            {
                _instance = new LineManager();
            }
            return _instance;
        }
        private set { }
    }
    #endregion
}
