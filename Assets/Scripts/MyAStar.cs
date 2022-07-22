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

    //����L���������|
    public List<Vector3> GetPath() => pathList;

    PathNode GetBestNode()
    {
        PathNode pn = null;
        return pn;
    }

    //AStar�B�⧹��A�̷Ӷ}�l�B�����إ߸��|
    void BuildPath(Vector3 startPosit, Vector3 endPosit, PathNode startNode, PathNode endNode)
    {

    }

    //��J�}�l�B������m�A�H����astar�B����|
    public bool PerformAStar(Vector3 startPosit, Vector3 endPosit)
    {
        //���׽T�{
        int startFloor = 0;
        int endFloor = 0;
        //�����p��A���n��
        //if (startPosit.y > 4) startFloor = 1;
        //if (endPosit.y > 4) endFloor = 1;

        //���m�̪񪺸`�I
        PathNode startNode = terrain.GetNodeFromPosition(startPosit, startFloor);
        PathNode endNode = terrain.GetNodeFromPosition(endPosit, endFloor);

        //�p�G����S�`�I�A�t�⥢��
        if(startNode == null||endNode == null)
        {
            Debug.Log("Can't find node in AStar map");
            return false;
        }
        //�p�G�����`�I�M�}�l�`�I�ۦP�A�إ߸`�I�A�t�⦨�\
        else if (startNode == endNode)
        {
            BuildPath(startPosit, endPosit, startNode, endNode);
            return true;
        }

        //�t��y�{
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