using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 30
이름 : 배성훈
내용 : 축구 전술
    문제번호 : 3977번

    예시를 직접 그래프를 그리고 확인해보니 이해했다
    해당 문제에서 찾아야할 것은 다른 모든 SCC로 갈 수 있는 SCC를 찾는 것이다

    타잔의 방법으로 했다
    재귀함수를 이용하는데, static 변수나, 전역변수를 사용하지 않아 메모리를 많이 먹는다
    시간도 빠른 사람의 2배 이상 걸린다.

    아이디어는 다음과 같다
    타잔 방법으로 SCC를 찾아가는데, 찾아가는 도중에 현재 노드에서 다른 그룹으로 가는 길이 있는 경우 topoLine에 기록한다
    여기서 조심할껀, _id[chk] > _id[_cur]인 경우 무턱대고 topoLine을 추가하면 -1을 담아 인덱스 에러 뜰 수 있다
        예를들어
            4 5
            0 1
            1 2
            1 3
            3 2
            2 0
        를 디버깅 해보면 된다!
        45_01에서는 id가 높은 값을 반환하면 다른 그룹에 포함되어져 있는 줄 알았는데, 
        해당 반례를 디버깅해보니, 같은 그룹에 있는 경우에도 반환할 수 있음을 알게 되었다!

    chk에 SCC 그룹이 있는지 확인하고, 있으면 현재 그룹과 해당 그룹에 간선을 topoLines에 기록했다
    그리고 topoLines의 간선을 토대로 SCC 그룹을 위상 정렬을 했다

    시작지점은 위상 정렬 과정에서 indegree가 0인 SCC 그룹에 포함된 원소들이 된다

    시작지점을 모르는 경우는 indegree가 0인 그룹이 2개 이상 존재하는 경우이다
    SCC 그룹은 코사라주로 만들어지는 과정이나 정의를 보면, 
    SCC 을 위상정렬 과정에서 적어도 indegree가 0인 SCC 그룹이 1개 존재해야한다!
    그래서 confused에 min == -1부분은 안넣었다!
*/

namespace BaekJoon._45
{
    internal class _45_03
    {

        static void Main3(string[] args)
        {

            int MAX = 100_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = int.Parse(sr.ReadLine());

            // 0번부터 시작
            List<int>[] lines = new List<int>[MAX];
            List<int>[] topoLines = new List<int>[MAX]; 
            
            // 타잔으로 찾아간다!
            // 확장하는 연산은 안하게 가질 수 있는 최대 사이즈로 잡는다!
            Stack<int> s = new Stack<int>(MAX);
            
            int[] groups = new int[MAX];
            int[] id = new int[MAX];
            int[] degree = new int[MAX];

            for (int t = 0; t < test; t++)
            {

                // 한줄 띄움
                if (t != 0) 
                { 
                    
                    sr.ReadLine();
                    sw.Write('\n');
                }

                // 노드 수, 간선 수
                int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                for (int i = 0; i < info[0]; i++)
                {

                    if (lines[i] == null) lines[i] = new();
                    else lines[i].Clear();

                    if (topoLines[i] == null) topoLines[i] = new();
                    else topoLines[i].Clear();

                    // 0 번부터 넣을 생각이다!
                    id[i] = -1;
                    groups[i] = -1;

                    degree[i] = 0;
                }

                for (int i = 0; i < info[1]; i++)
                {

                    int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                    lines[temp[0]].Add(temp[1]);
                }

                // 이제 탐색!
                int curId = -1;
                int groupId = -1;
                for (int i = 0; i < info[0]; i++)
                {

                    DFS(lines, topoLines, id, groups, i, s, ref curId, ref groupId);
                }

                // 이제 degree 확인
                for (int i = 0; i <= groupId; i++)
                {

                    for (int j = 0; j < topoLines[i].Count; j++)
                    {

                        int next = topoLines[i][j];
                        degree[next]++;
                    }
                }

                int min = -1;
                bool confused = false;
                for (int i = 0; i <= groupId; i++)
                {

                    if (degree[i] == 0)
                    {
                        
                        if (min == -1) min = i;
                        else confused = true;
                    }
                }

                // min == -1인 경우는 나올수 없다
                // SCC가 사이클을 이루는 경우인데, 이는 SCC의 정의에 의해 막힌다!
                if (confused)
                {

                    sw.Write("Confused\n");
                    continue;
                }
                
                for (int i = 0; i < info[0]; i++)
                {

                    if (groups[i] == min)
                    {

                        sw.Write(i);
                        sw.Write('\n');
                    }
                }
            }
            sr.Close();
            sw.Close();
        }

        static int DFS(List<int>[] _lines, List<int>[] _topoLines, int[] _id, int[] _group, int _cur, Stack<int> _calc, ref int _curId, ref int _groupId)
        {

            // 포문에서 걸리는 경우!
            if (_id[_cur] != -1) return -1;
            _id[_cur] = ++_curId;
            int result = _cur;
            _calc.Push(_cur);

            for (int i = 0; i < _lines[_cur].Count; i++)
            {

                int next = _lines[_cur][i];

                if (_id[next] != -1)
                {

                    if (_group[next] != -1) 
                    { 
                        
                        if (!_topoLines[_groupId + 1].Contains(_group[next])) _topoLines[_groupId + 1].Add(_group[next]); 
                    }
                    else if (_id[next] < _id[result]) result = next;
                    continue;
                }

                int chk = DFS(_lines, _topoLines, _id, _group, next, _calc, ref _curId, ref _groupId);

                // 44_01과 달리 여기서는 이어져 있는지 확인해줘야한다
                //
                // DFS 돌고 그룹이 탄생할 수도 있는데
                // 길이 이어져 있는지 확인해야하기 때문이다!
                // 
                // 44_01에서는 _id[chk] < _id[result] <= _id[_cur]이고,
                // DFS 탐색 후 그룹이 만들어졌다면, _id[chk] > _id[_cur]에서 끊기 때문이다
                //
                // 여기를 다음과 같이 수정했다 
                // if (_id[chk] > _id[_cur]) _topoLines[_groupId + 1].Add(_group[chk]);
                if (_group[chk] != -1)
                {

                    if (!_topoLines[_groupId + 1].Contains(_group[chk])) _topoLines[_groupId + 1].Add(_group[chk]);
                }
                else if (_id[chk] < _id[result]) result = chk;
            }

            // SCC 발견!
            if (result == _cur)
            {

                _groupId++;
                while(_calc.Count > 0)
                {

                    int next = _calc.Pop();
                    _group[next] = _groupId;
                    if (next == _cur) break;
                }
            }

            return result;
        }
    }
    /*
    for (int i = 0; i < _lines[_cur].Count; i++)
    {

        int next = _lines[_cur][i];

        if (_id[next] != -1)
        {

            if (_group[next] != -1) _topoLines[_groupId + 1].Add(_group[next]);
            else if (_id[next] < _id[result]) result = next;
            continue;
        }

        int chk = DFS(_lines, _topoLines, _id, _group, next, _calc, ref _curId, ref _groupId);

        if (_id[chk] < _id[result]) result = chk;
        // 여기가 문제인거 같다!
        else if (_id[chk] > _id[_cur]) _topoLines[_groupId + 1].Add(_group[chk]);
    }     


    for (int i = 0; i < _lines[_cur].Count; i++)
    {

        int next = _lines[_cur][i];

        if (_id[next] != -1)
        {

            if (_group[next] != -1) 
            { 

                if (!_topoLines[_groupId + 1].Contains(_group[next])) _topoLines[_groupId + 1].Add(_group[next]); 
            }
            else if (_id[next] < _id[result]) result = next;
            continue;
        }

        int chk = DFS(_lines, _topoLines, _id, _group, next, _calc, ref _curId, ref _groupId);
        if (_group[chk] != -1)
        {

            if (!_topoLines[_groupId + 1].Contains(_group[chk])) _topoLines[_groupId + 1].Add(_group[chk]);
        }
        else if (_id[chk] < _id[result]) result = chk;
    }
    */

#if other
using System.Text;

public record struct Node(List<int> Children, int Id, int SCC)
{
    public Node() : this(new(), -1, -1) { }

    public readonly bool HasId => Id >= 0;
    public readonly bool Finished => SCC >= 0;

    public void Clear()
    {
        Children.Clear();
        Id = -1;
        SCC = -1;
    }
}

public class Program
{
    const int Max = 100_000;
    static readonly Node[] _nodes = new Node[Max];
    static int _curId = 0;
    static readonly Stack<int> _stack = new();
    static readonly List<int> _indegree = new();
    static int _sccCount;

    static Program()
    {
        for (int i = 0; i < _nodes.Length; i++)
        {
            _nodes[i] = new();
        }
    }

    private static void Main(string[] args)
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        var sb = new StringBuilder();
        var t = ScanInt(sr);
        for (int i = 0; i < t; i++)
        {
            int nodeCount = ScanInt(sr), m = ScanInt(sr);
            for (int j = 0; j < m; j++)
            {
                int a = ScanInt(sr), b = ScanInt(sr);
                _nodes[a].Children.Add(b);
            }
            for (int node = 0; node < nodeCount; node++)
            {
                if (!_nodes[node].Finished)
                    DPS(node);
            }
            for (int node = 0; node < nodeCount; node++)
            {
                foreach (var child in _nodes[node].Children)
                {
                    if (_nodes[node].SCC != _nodes[child].SCC)
                        _indegree[_nodes[child].SCC]++;
                }
            }

            var zeroScc = -1;
            var confused = false;
            for (int sccIndex = 0; sccIndex < _indegree.Count; sccIndex++)
            {
                int indegree = _indegree[sccIndex];
                if (indegree == 0)
                {
                    if (zeroScc == -1)
                        zeroScc = sccIndex;
                    else
                    {
                        confused = true;
                        break;
                    }
                }
            }
            if (confused)
            {
                sb.Append("Confused").Append('\n', 2);
            }
            else
            {
                for (int nodeIndex = 0; nodeIndex < nodeCount; nodeIndex++)
                {
                    if (_nodes[nodeIndex].SCC == zeroScc)
                        sb.Append(nodeIndex).Append('\n');
                }
                sb.Append('\n');
            }


            for (int node = 0; node < nodeCount; node++)
            {
                _nodes[node].Clear();
            }
            _curId = 0;
            _sccCount = 0;
            _indegree.Clear();
            if (i < t - 1 && sr.Read() == '\r')
                sr.Read();
        }
        sb.Length -= 2;
        Console.Write(sb);
    }

    static int DPS(int nodeIndex)
    {
        ref var node = ref _nodes[nodeIndex];
        var parent = node.Id = _curId++;
        _stack.Push(nodeIndex);
        foreach (var childIndex in node.Children)
        {
            ref var child = ref _nodes[childIndex];
            var childParent = child.Id;
            if (!child.HasId) childParent = DPS(childIndex);
            if (child.Finished) continue;
            if (parent > childParent)
                parent = childParent;
        }
        if (parent == node.Id)
        {
            int elem;
            do
            {
                elem = _stack.Pop();
                _nodes[elem].SCC = _sccCount;
            } while (elem != nodeIndex);
            _indegree.Add(0);
            _sccCount++;
        }
        return parent;
    }

    static int ScanInt(StreamReader sr)
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
}
#endif
}
