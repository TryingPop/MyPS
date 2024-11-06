using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 26
이름 : 배성훈
내용 : 개미굴
    문제번호 : 14725번

    문자열의 사전식 비교 판별이 필요하다
    ... 그냥 배열을 정렬하고 해봤더니 출력 초과가 뜬다

    찾아보니 트라이? 알고리즘을 써야한다
    서로 주소를 참조하기에 많이 무거운 자료형이라 한다

    Trie 자료구조 직접 만들고 싶었으나 처음 본 자료구조라 쉽게 만들어지지 않았다
    https://gamemakerslab.tistory.com/53
    사이트를 참고하여 문제 출력에 맞게 조금만 손봤다

    그런데, 다른 사람 풀이에 깔끔하게 잘 만들어진게 있어서 해당 코드를 전체 긁어왔다
    추후에 Trie자료구조를 만들 때, 해당 사람처럼 만들 수 있으면 좋겠다!
*/

namespace BaekJoon._42
{
    internal class _42_03
    {

        static void Main3(string[] args)
        {

#if Wrong
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            string[][] foods = new string[len][];
            int[] dp = new int[len];
            for (int i = 0; i < len; i++)
            {

                // 앞의 숫자는 버린다!
                int c;
                while((c = sr.Read()) != ' ') { }

                foods[i] = sr.ReadLine().Split(' ');
            }
            sr.Close();

            string[][] sortedFoods = new string[len][];

            MergeSort(foods, 0, len, sortedFoods);

            StringBuilder sb = new StringBuilder();

            Print(foods[0], dp[0], sb);
            for (int i = 1; i < len; i++)
            {

                Comp(foods[i - 1], foods[i], ref dp[i]);
                Print(foods[i], dp[i], sb);
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }
#else

            TrieNode node = new();
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            for (int i = 0; i < len; i++)
            {

                int c;
                while((c = sr.Read()) != ' ') { }

                string[] temp = sr.ReadLine().Split(' ');
                node.Insert(temp);
            }
            
            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
                node.Print(sw);
#endif
        }


#if Wrong
        static void MergeSort(string[][] _arr, int _startIdx, int _len, string[][] _cpy)
        {

            // 0개나 1개는 안한다!
            if (_len < 2) return;

            // 분할
            int midLen =_len / 2;
            MergeSort(_arr, _startIdx, midLen, _cpy);
            MergeSort(_arr, _startIdx + midLen, _len - midLen, _cpy);

            // 정복 투 포인트 알고리즘을 이용한다!
            int lp = _startIdx, rp = _startIdx + midLen;
            int cur = _startIdx;
            int notUse = 0;
            while (lp < _startIdx + midLen && rp < _startIdx + _len)
            {

                int comp = Comp(_arr[lp], _arr[rp], ref notUse);
                // 뒤에께 작은 경우만 뒤에꺼 넣고 앞에께 작으면 앞에 넣는다!
                if (comp > 0) _cpy[cur++] = _arr[rp++];
                else _cpy[cur++] = _arr[lp++];
            }

            while (lp < _startIdx + midLen)
            {

                _cpy[cur++] = _arr[lp++];
            }

            while(rp < _startIdx + _len)
            {

                _cpy[cur++] = _arr[rp++];
            }

            for (int i = _startIdx; i < _len; i++)
            {

                _arr[i] = _cpy[i];
            }
        }

        // 비교 잘된다!
        static int Comp(string[] _a, string[] _b, ref int _get)
        {

            int min = _a.Length < _b.Length ? _a.Length : _b.Length;

            for (int i = 0; i < min; i++)
            {

                int comp = _a[i].CompareTo(_b[i]);
                if (comp != 0)
                {

                    _get = i;
                    return comp;
                }
            }

            // 같으므로 길이 비교!
            // 긴게 큰값!
            // 그리고 길이 저장
            _get = min;
            return _a.Length.CompareTo(_b.Length);
        }

        static void Print(string[] _arr, int startI, StringBuilder _sb)
        {

            for (int i = startI; i < _arr.Length; i++)
            {

                for (int j = 0; j < i; j++)
                {

                    _sb.Append('-');
                    _sb.Append('-');
                }

                _sb.Append(_arr[i]);
                _sb.Append('\n');
            }
        }
#else

        class TrieNode
        {

            // 자시 찾기!
            Dictionary<string, TrieNode> child;

            public TrieNode()
            {

                child = new();
            }

            public void Insert(string[] _add)
            {

                TrieNode node = this;

                for (int i = 0; i < _add.Length; i++)
                {

                    if (!node.child.ContainsKey(_add[i]))
                    {

                        node.child[_add[i]] = new TrieNode();
                    }

                    node = node.child[_add[i]];
                }
            }


            public void Print(StreamWriter _sw, int i = 0)
            {

                TrieNode node = this;
                if (node.child.Count == 0) return;

                var keys = child.Keys.OrderBy(x => x);

                foreach (string key in keys)
                {

                    for (int j = 0; j < i; j++)
                    {

                        _sw.Write("--");
                    }
                    _sw.WriteLine(key);
                    node.child[key].Print(_sw, i + 1);
                }
            }
        }
#endif
    }

#if others

    // 다른사람 풀이 정말 깔끔해서 긁어왔다!
    public static class PS
    {
        public class Node : IComparable<Node>
        {
            public string name;
            public Dictionary<string, Node> children = new();

            int IComparable<Node>.CompareTo(Node other)
            {
                return string.Compare(name, other.name);
            }
        }

        public static StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));
        public static Node root = new();

        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            string[] input;
            Node cur;

            while (n-- > 0)
            {
                input = Console.ReadLine().Split();
                cur = root;

                for (int i = 1; i < input.Length; i++)
                {
                    cur.children.TryAdd(input[i], new Node { name = input[i] });
                    cur = cur.children[input[i]];
                }
            }

            Node[] sorted = new Node[root.children.Count];
            int idx = 0;

            foreach (var child in root.children)
            {
                sorted[idx++] = child.Value;
            }

            Array.Sort(sorted);

            for (int i = 0; i < sorted.Length; i++)
            {
                DFS(0, sorted[i]);
            }

            sw.Close();
        }

        public static void DFS(int depth, Node node)
        {
            for (int i = 0; i < depth; i++)
            {
                sw.Write('-');
                sw.Write('-');
            }

            sw.WriteLine(node.name);

            Node[] sorted = new Node[node.children.Count];
            int idx = 0;

            foreach (var child in node.children)
            {
                sorted[idx++] = child.Value;
            }

            Array.Sort(sorted);

            for (int i = 0; i < sorted.Length; i++)
            {
                DFS(depth + 1, sorted[i]);
            }
        }
    }
#elif other2
    using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

    var n = ScanInt();
    var root = new Node("");
    Span<char> buffer = stackalloc char[15];
    while (n-- > 0)
    {
        var k = ScanInt();
        var curNode = root;
        while (k-- > 0)
        {
            curNode = curNode.Get(ScanString(buffer));
        }
    }
    root.Sort();

    using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
    Write(root, -1);

    void Write(Node node, int depth)
    {
        if (depth >= 0)
        {
            for (int i = 0; i < 2 * depth; i++)
                sw.Write('-');
            sw.WriteLine(node.Name);
        }
        foreach (var c in node.Children)
            Write(c, depth + 1);
    }

    int ScanInt()
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

    ReadOnlySpan<char> ScanString(Span<char> buffer)
    {
        int c, count = 0;
        while (!((c = sr.Read()) is ' ' or '\n' or -1))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            buffer[count++] = (char)c;
        }
        return buffer[..count];
    }

    class Node
    {
        readonly string name;
        readonly List<Node> linked = new();
        static Comparer comparer = new();

        public Node(string name)
        {
            this.name = name;
        }

        public Node Get(ReadOnlySpan<char> item)
        {
            foreach (var o in linked)
            {
                if (item.Equals(o.name, StringComparison.Ordinal))
                {
                    return o;
                }
            }

            var output = new Node(item.ToString());
            linked.Add(output);
            return output;
        }

        public void Sort()
        {
            linked.Sort(comparer);
            foreach (var o in linked)
                o.Sort();
        }

        public IEnumerable<Node> Children => linked;

        public string Name => name;

        class Comparer : IComparer<Node>
        {
            public int Compare(Node? x, Node? y) =>
                x?.Name.CompareTo(y?.Name) ?? -1;
        }
    }
#elif other3

    StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
    StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

    int n = int.Parse(sr.ReadLine());
    Cave top = new Cave("");

    for (int i=0; i<n; i++)
    {
        string[] input = sr.ReadLine().Split();
        int m = int.Parse(input[0]);
        Cave cur = top;
        for (int j = 1; j <= m; j++)
            cur = cur.setNext(input[j]);
    }

    PrintCave(top, -1);


    sr.Close();
    sw.Close();
    //------------

    void PrintCave(Cave cur, int depth)
    {
        for (int i = 0; i < depth; i++)
            sw.Write("--");
        if(depth >= 0)
            sw.WriteLine(cur.name);
        foreach (var kvp in cur.next)
            PrintCave(kvp.Value, depth + 1);
    }

    class Cave
    {
        public string name;
        public SortedList<string, Cave> next;
        public Cave(string name)
        {
            this.name = name;
            this.next = new();
        }
        public Cave setNext(string name)
        {
            if(!next.ContainsKey(name))
                next.Add(name, new Cave(name));
            return next[name];
        }
    }
#endif
}
