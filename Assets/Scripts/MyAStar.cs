using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    WayPointTerrain terrain;

    List<PathNode> openList;
    List<PathNode> closeList;

    List<Vector3> pathList;

    static public AStar instance;

    public void Init(WayPointTerrain wpt)
    {
        terrain = wpt;
        openList = new List<PathNode>();
        closeList = new List<PathNode>();
        pathList = new List<Vector3>();
        instance = this;
    }

    //讓其他物件抓取路徑
    public List<Vector3> GetPath() => pathList;

    PathNode GetBestNode()
    {
        PathNode pn = null;
        return pn;
    }

    //AStar運算完後，依照開始、結束建立路徑
    void BuildPath(Vector3 startPosit, Vector3 endPosit, PathNode startNode, PathNode endNode)
    {

    }

    //輸入開始、結束位置，以執行astar運算路徑
    public bool PerformAStar(Vector3 startPosit, Vector3 endPosit)
    {
        //高度確認
        int startFloor = 0;
        int endFloor = 0;
        //先不計算，但要用
        //if (startPosit.y > 4) startFloor = 1;
        //if (endPosit.y > 4) endFloor = 1;

        //抓位置最近的節點
        PathNode startNode = terrain.GetNodeFromPosition(startPosit, startFloor);
        PathNode endNode = terrain.GetNodeFromPosition(endPosit, endFloor);

        //如果附近沒節點，演算失敗
        if(startNode == null||endNode == null)
        {
            Debug.Log("Can't find node in AStar map");
            return false;
        }
        //如果結束節點和開始節點相同，建立節點，演算成功
        else if (startNode == endNode)
        {
            BuildPath(startPosit, endPosit, startNode, endNode);
            return true;
        }

        //演算流程
        openList.Clear();
        closeList.Clear();
        terrain.ClearAStarInfo();
        PathNode nextNode;
        PathNode currentNode;
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Debug.Log("Check OpenList");
            currentNode = GetBestNode();
            if(currentNode == null)
            {
                Debug.Log("GetBestNode error");
                return false;
            }
            else if(currentNode == endNode)
            {

            }
        }

        return false;
    }
}