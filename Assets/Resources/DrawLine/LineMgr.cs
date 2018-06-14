using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class LineMgr
{
    #region singleton
    private static LineMgr _instance = null;
    private LineMgr() { }
    public static LineMgr Instance
    {
        get {
            if (_instance==null)
            {
                _instance = new LineMgr();
            }
            return _instance;
        }
        private set { }
    }
    #endregion
}
