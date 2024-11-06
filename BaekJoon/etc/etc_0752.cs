using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 3
이름 : 배성훈
내용 : 자동차 공장
    문제번호 : 2820번

    오일러 경로 테크닉, 세그먼트 트리, 트리 문제다
    53_01을 푸는 중인데 로직이 잘못되었는가 싶어 먼저 푼 문제다
    그런데 해당 문제는 통과되고 여기는 통과가 안된다;

    오일러 경로 테크닉은 트리에서 dfs탐색을 통해
    기존 인덱스를 재설정하는 것을 의미한다

    이후 변형된 인덱스로 뿌리의 시작과 끝을 기록한 뒤
    해당 구간만큼 기록하는 행동을 할 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0752
    {

        static void Main752(string[] args)
        {

            int ONE = 'p' - '0';
            StreamReader sr;
            StreamWriter sw;

            (int s, int e)[] nTi;
            int[] seg;
            List<int>[] line;
            int n, m;
            int idx;

            int[] money;

            Solve();

            void Solve()
            {

                Init();

                for (int i = 0; i < m; i++)
                {

                    int op = ReadInt();

                    if (op == ONE)
                    {

                        idx = ReadInt();
                        int w = ReadInt();
                        Query1(idx, w);
                    }
                    else
                    {

                        idx = ReadInt();
                        Query2(idx);
                    }
                }

                sr.Close();
                sw.Close();
            }

            void Query1(int _idx, int _w)
            {

                Update(1, n, nTi[_idx].s + 1, nTi[_idx].e, _w);
            }

            void Query2(int _idx)
            {

                int ret = GetVal(1, n, nTi[_idx].s) + money[_idx];
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

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                line = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                money = new int[n + 1];
                money[1] = ReadInt();
                for (int i = 2; i <= n; i++)
                {

                    int f = ReadInt();
                    money[i] = f;

                    int b = ReadInt();
                    line[b].Add(i);
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

                if (_chkE < _s || _e < _chkS || _chkE < _chkS) return;

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

            int ReadInt()
            {

                int c = sr.Read();
                if (c == -1) return 0;

                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n' && c != '\t')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace 자동차_공장 {
    class SegTree {
        public int Count;
        private List<long> tree;
        private List<long> lazy;

        public SegTree(List<long> original) {
            Count = original.Count;
            int treeHeight = (int)Math.Ceiling(Math.Log2(Count));
            int listSize = (1 << (treeHeight+1));
            tree = new List<long>(new long[listSize]);
            lazy = new List<long>(new long[listSize]);
            Initialize(1, 0, Count-1, original);
        }

        public long Query(int start, int end) {
            return Query(1, start, end, 0, Count-1);
        }

        public void Update(int start, int end, long add) {
            Update(add, 1, start, end, 0, Count-1);
        }

        private void Initialize(int index, int start, int end, List<long> original) {
            if (start == end) {
                tree[index] = original[start];
            } else {
                int mid = (start+end) / 2;
                Initialize(index*2, start, mid, original);
                Initialize(index*2+1, mid+1, end, original);
                tree[index] = tree[index*2] + tree[index*2+1];
            }
        }

        private void Propagate(int index, int start, int end) {
            if (lazy[index] != 0) {
                tree[index] += lazy[index] * (end-start+1);
                if (start < end) {
                    lazy[index*2] += lazy[index];
                    lazy[index*2+1] += lazy[index];
                }
                lazy[index] = 0;
            }
        }

        private long Query(int index, int reqStart, int reqEnd, int treeStart, int treeEnd) {
            Propagate(index, treeStart, treeEnd);

            if (reqStart <= treeStart && treeEnd <= reqEnd) {
                return tree[index];
            } else if (treeStart <= reqEnd && reqStart <= treeEnd) {
                int treeMed = (treeStart + treeEnd) / 2;
                long left = Query(index*2, reqStart, reqEnd, treeStart, treeMed);
                long right = Query(index*2+1, reqStart, reqEnd, treeMed+1, treeEnd);
                return left + right;
            } else {
                return 0;
            }
        }

        private void Update(long add, int index, int reqStart, int reqEnd, int treeStart, int treeEnd) {
            Propagate(index, treeStart, treeEnd);

            if (reqStart <= treeStart && treeEnd <= reqEnd) {
                lazy[index] += add;
                Propagate(index, treeStart, treeEnd);
            } else if (treeStart <= reqEnd && reqStart <= treeEnd) {
                int treeMed = (treeStart + treeEnd) / 2;
                Update(add, index*2, reqStart, reqEnd, treeStart, treeMed);
                Update(add, index*2+1, reqStart, reqEnd, treeMed+1, treeEnd);
                tree[index] = tree[index*2] + tree[index*2+1];
            }
        }
    }

    class Range {
        public int Lower { get; }
        public int Upper { get; }

        public Range(int lower, int upper) {
            Lower = lower;
            Upper = upper;
        }
    }

    class Employee : IComparable<Employee> {
        public int ID { get; }
        public int Index { get; set; }
        public int Payment { get; }
        public int SeniorID { get; }
        public List<Employee> Juniors { get; }
        public Range IndexRange { get; set; }

        public Employee(int id, int payment, int seniorID) {
            ID = id;
            Index = -1;
            Payment = payment;
            SeniorID = seniorID;
            Juniors = new List<Employee>();
            IndexRange = null;
        }

        public int CompareTo(Employee other) {
            return Index.CompareTo(other.Index);
        }
    }

    class Program {
        static int Preorder(Employee node, int index, List<long> payments, Dictionary<int, int> indexDict) {
            node.Index = index;
            payments.Add(node.Payment);
            indexDict[node.ID] = index;
            int lower = index;
            int upper = index;

            foreach (Employee next in node.Juniors) {
                index = Preorder(next, index+1, payments, indexDict);
                upper = index;
            }

            node.IndexRange = new Range(lower, upper);
            return index;
        }

        static void Main(string[] args) {
            var tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var (n, m) = (tokens[0], tokens[1]);

            List<Employee> emps = new();
            int rootPay = int.Parse(Console.ReadLine());
            emps.Add(new Employee(1, rootPay, 0));

            for (int i = 2; i <= n; ++i) {
                tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var (pay, senior) = (tokens[0], tokens[1]);
                emps.Add(new Employee(i, pay, senior));
            }

            foreach (var emp in emps) {
                if (emp.SeniorID != 0) {
                    emps[emp.SeniorID-1].Juniors.Add(emp);
                }
            }

            List<long> preorderPayment = new List<long>(n);
            Dictionary<int, int> indexDict = new Dictionary<int, int>();
            Preorder(emps[0], 0, preorderPayment, indexDict);
            emps.Sort();
            SegTree tree = new SegTree(preorderPayment);

            using (StreamWriter writer = new StreamWriter(Console.OpenStandardOutput())) {
                for (int i = 0; i < m; ++i) {
                    string line = Console.ReadLine();
                    switch (line[0])
                    {
                        case 'p': {
                            tokens = line.Substring(2).Split().Select(int.Parse).ToArray();
                            var (senior, add) = (tokens[0], tokens[1]);
                            int index = indexDict[senior];
                            Range range = emps[index].IndexRange;
                            if (range.Lower < range.Upper) {
                                tree.Update(range.Lower+1, range.Upper, add);
                            }
                            break;
                        }

                        case 'u': {
                            int id = int.Parse(line.Substring(2));
                            int index = indexDict[id];
                            writer.WriteLine(tree.Query(index, index));
                            break;
                        }
                    }
                }
            }
        }
    }
}

#endif
}
