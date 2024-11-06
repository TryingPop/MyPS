using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 3
이름 : 배성훈
내용 : 회사 문화 2
    문제번호 : 14268번

    세그먼트 트리, 오일러 경로 테크닉 문제다
    .... 입력 문제로 엄청나게 틀렸다
    하나씩 읽으면서 수를 기록하는게 아닌 그냥 줄을 통으로 읽고 int로 변형하니 통과했다;
    아마 줄 마지막쪽에 띄움 표시가 이 2개 이상있는거 같다

    오일러 경로 테크닉은 다음과 같다
    트리가 주어질 때, DFS로 탐색해서 idx를 재 분배한다

    그러면 자식노드들에 한해 i ~ j 로 구간설정이 가능하다
    이는 해당 자식 노드들에게 동시에 값을 추가하거나 더할 수 있게된다
    범위에 값을 넣는 방법으로 세그먼트 트리를 이용한다

    이제 구간에 값을 추가하는 것은 세그먼트 트리의 인덱스가 범위를 나타낼 수 있으므로
    값을 추가할 값으로 해서 제출하니 입력문제를 제외하고는 이상없이 통과했다
    ...질문 게시판에 입력이 이상하다는 글을 보고 입력을 수정하니 통과했다;
*/

namespace BaekJoon._53
{
    internal class _53_01
    {

        static void Main1(string[] args)
        {

#if lazySeg
            StreamReader sr;
            StreamWriter sw;

            (int s, int e)[] nTi;

            (int n, int lazy)[] seg;

            List<int>[] line;
            int n, m;
            int idx;

            Solve();
            void Solve()
            {

                Init();

                for (int i = 0; i < m; i++)
                {

                    int op = ReadInt();

                    if (op == 1) Query1(ReadInt(), ReadInt());
                    else Query2(ReadInt());
                }

                sr.Close();
                sw.Close();
            }

            void Query1(int _idx, int _w)
            {

                Update(1, n, nTi[_idx].s, nTi[_idx].e, _w);
            }

            void Query2(int _idx)
            {

                int ret = GetVal(1, n, nTi[_idx].s);
                sw.Write($"{ret}\n");
            }

            void EulerDFS(int _n)
            {

                nTi[_n].s = ++idx;
                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    EulerDFS(next);
                }

                nTi[_n].e = idx;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 16);

                n = ReadInt();
                m = ReadInt();

                line = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                    int p = ReadInt();
                    if (i == 1) continue;
                    line[p].Add(i);
                }

                nTi = new (int s, int e)[n + 1];
                idx = 0;

                EulerDFS(1);

                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new (int n, int lazy)[1 << log];
            }

            void Update(int _s, int _e, int _chkS, int _chkE, int _add, int _idx = 0)
            {

                if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].n += (_e - _s + 1) * _add;

                    if (_s != _e)
                    {

                        seg[_idx * 2 + 1].lazy += _add;
                        seg[_idx * 2 + 2].lazy += _add;
                    }

                    return;
                }

                if (_chkE < _s || _e < _chkS || _chkE < _chkS || _e < _s) return;

                int mid = (_s + _e) >> 1;
                Update(_s, mid, _chkS, _chkE, _add, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _add, _idx * 2 + 2);

                seg[_idx].n += seg[_idx * 2 + 1].n + seg[_idx * 2 + 2].n;
            }

            int GetVal(int _s, int _e, int _chk, int _idx = 0)
            {

                if (seg[_idx].lazy != 0)
                {

                    seg[_idx].n += (_e - _s + 1) * seg[_idx].lazy;

                    if (_s != _e)
                    {

                        seg[_idx * 2 + 1].lazy += seg[_idx].lazy;
                        seg[_idx * 2 + 2].lazy += seg[_idx].lazy;
                    }

                    seg[_idx].lazy = 0;
                }

                if (_chk < _s || _e < _chk) return 0;

                if (_s == _e) return seg[_idx].n;

                int mid = (_s + _e) >> 1;
                return GetVal(_s, mid, _chk, _idx * 2 + 1) 
                    + GetVal(mid + 1, _e, _chk, _idx * 2 + 2);
            }

            int ReadInt()
            {

                int c = sr.Read();
                if (c == -1) return 0;

                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n' && c != '\t')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
#else

            StreamReader sr;
            StreamWriter sw;

            (int s, int e)[] nTi;
            int[] seg;
            List<int>[] line;
            int n, m;
            int idx;
            Solve();

            void Solve()
            {

                Init();

                for (int i = 0; i < m; i++)
                {

                    int[] temp = sr.ReadLine().Split().Select(int.Parse).ToArray();

                    int op = temp[0];

                    if (op == 1) Query1(temp[1], temp[2]);
                    else Query2(temp[1]);
                }

                sr.Close();
                sw.Close();
            }

            void Query1(int _idx, int _w)
            {

                Update(1, n, nTi[_idx].s, nTi[_idx].e, _w);
            }

            void Query2(int _idx)
            {

                int ret = GetVal(1, n, nTi[_idx].s);
                sw.Write($"{ret}\n");
            }

            void EulerDFS(int _n)
            {

                nTi[_n].s = ++idx;
                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    EulerDFS(next);
                }

                nTi[_n].e = idx;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 16);

                string[] temp = sr.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);

                temp = sr.ReadLine().Split();
                line = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                    if (i == 1) continue;
                    int p = int.Parse(temp[i - 1]);
                    line[p].Add(i);
                }

                nTi = new (int s, int e)[n + 1];
                idx = 0;

                EulerDFS(1);

                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new int[1 << log];
            }

            void Update(int _s, int _e, int _chkS, int _chkE, int _add, int _idx = 0)
            {

                if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx] += _add;
                    return;
                }

                if (_chkE < _s || _e < _chkS || _chkE < _chkS || _e < _s) return;

                int mid = (_s + _e) >> 1;
                Update(_s, mid, _chkS, _chkE, _add, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _add, _idx * 2 + 2);
            }

            int GetVal(int _s, int _e, int _chk, int _idx = 0)
            {

                if (_s == _e) return seg[_idx];

                int mid = (_s + _e) >> 1;
                int ret = seg[_idx];
                if (mid < _chk) ret += GetVal(mid + 1, _e, _chk, _idx * 2 + 2);
                else ret += GetVal(_s, mid, _chk, _idx * 2 + 1);

                return ret;
            }
#endif
        }
    }

#if other
using System.Globalization;

var mn = Console.ReadLine()!.Trim().Split().Select(int.Parse).ToArray();
var (n, m) = (mn[0], mn[1]);
var bosses = Console.ReadLine()!.Trim().Split().Select(int.Parse).ToArray();

// build tree
var tree = new Dictionary<int, List<int>>();
for (var i = 1; i < n; i++) {
    if (!tree.TryGetValue(bosses[i], out var children))
        tree[bosses[i]] = children = new List<int>();
    children.Add(i + 1);
}

var orderByDfs = new List<int>();   // 조직도를 dfs 순서로 저장
var dfsIndex = new int[n + 1];
var lastChildIndexes = new int[n + 1];   // 각 직원에 대해 자신의 서브트리가 마지막으로 방문하는 직원 번호 저장
BuildOrganizationChartRecursive(1);

int BuildOrganizationChartRecursive(int cur) {
    orderByDfs.Add(cur);
    dfsIndex[cur] = orderByDfs.Count;

    if (!tree.TryGetValue(cur, out var children)) return lastChildIndexes[cur] = cur;
    foreach (var c in children) lastChildIndexes[cur] = BuildOrganizationChartRecursive(c); //마지막으로 방문한 직원 번호로 덮어씌운다.
    return lastChildIndexes[cur];
}

var lazy = new decimal[n * 4 + 1];
var compliments = new decimal[n + 1];

while (m-- > 0) {
    var inputArray = Console.ReadLine()!.Trim().Split().ToArray();
    var employee = int.Parse(inputArray[1]);
    switch (inputArray.Length) {
        case 3:
            Update(1, n, dfsIndex[employee], dfsIndex[lastChildIndexes[employee]], 1, decimal.Parse(inputArray[2], CultureInfo.InvariantCulture));
            break;
        case 2:
            Console.WriteLine(Sum(1, n, 1, dfsIndex[employee]).ToString(CultureInfo.InvariantCulture));
            break;
        default:
            throw new ArgumentException($"inputArray.Length={inputArray.Length}");
    }
}

void Update(int s, int e, int l, int r, int index, decimal diff) {
    PropagateLazyIfExist(s, e, index);

    if (s > r || e < l) return;
    if (s >= l && e <= r) {
        lazy[index] += diff;
        return;
    }

    Update(s, (s + e) / 2, l, r, index * 2, diff);
    Update((s + e) / 2 + 1, e, l, r, index * 2 + 1, diff);
}

decimal Sum(int s, int e, int index, int employee) {
    PropagateLazyIfExist(s, e, index);
    if (s > employee || e < employee) return 0;
    if (s >= employee && e <= employee) return compliments[s];
    return Sum(s, (s + e) / 2, index * 2, employee) + Sum((s + e) / 2 + 1, e, index * 2 + 1, employee);
}

void PropagateLazyIfExist(int segStart, int segEnd, int index) {
    if (lazy[index] == 0) return;
    
    if (segStart != segEnd) {
        lazy[index * 2] += lazy[index];
        lazy[index * 2 + 1] += lazy[index];
    }
    else {
        compliments[segStart] += lazy[index];
    }
    
    lazy[index] = 0;
}
#endif
}
