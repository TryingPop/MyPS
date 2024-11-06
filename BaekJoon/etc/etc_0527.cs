using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

/*
날짜 : 2024. 4. 14
이름 : 배성훈
내용 : 이중 우선순위 큐
    문제번호 : 7662번

    우선순위 큐, 트리를 사용한 집합과 맵 문제다
    해당 자료구조를 구현해보고 싶어, 직접 구현했었다
    처음에는 힙으로 했다 배열형태로 했는데, 
    값을 중앙에 추가할 때 한칸씩 밀어내는 식으로 만드니 시간초과가 떴다!

    그래서 찾아보니 AVL 트리를 써야한다고 봤고
    해당 기능을 보고 문제에 맞춰 시도했다
    https://walbatrossw.github.io/data-structure/2018/10/26/ds-avl-tree.html

    클래스 형식으로 노드를 구현해서 시간초과 떴다 
    -> 배열로 해볼려고 했으나, 이 경우 회전에서 N의 시간이 걸려버린다;

    아이디어는 다음과 같다
    4개의 우선순위 큐를 썼다
    2개는 최대, 최소로 했다 나머지 2개는 사용한 최대, 사용한 최소이다
    그리고 최소 값을 빼는 경우 사용된 최소값인지 확인하고 뺀 값을 사용한 최대값에 값을 넣어준다
    이렇게 사용 여부를 확인하면서 빼갔다
    해당 아이디어로 제출하니 이상없이 통과했다

    다만, 사용한 최대값 쪽에 내림차순으로 정렬해야 하는데 이 부분을 빼버려; 엄청나게 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0527
    {

        static void Main527(string[] args)
        {

#if Wrong

            // 어느 사이트에서 우선순위 큐를 구현하는 코드를 봤는데,
            // 해당 코드는 좋지 않다!
            int MAX = 1_000_001;

            int INSERT = 'I' - '0';
            int DELETE = 'D' - '0';

            string EMPTY = "EMPTY\n";
            int[] myArr = new int[2 * MAX];

            int head;
            int count;

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();

            while(test-- > 0)
            {

                int n = ReadInt();
                head = MAX;
                count = 0;

                for (int i = 0; i < n; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    if (f == INSERT) Insert(b);
                    else
                    {

                        if (b == 1) Delete(false);
                        else Delete(true);
                    }
                }

                if (count == 0) sw.Write(EMPTY);
                else sw.Write($"{myArr[head + count - 1]} {myArr[head]}\n");
            }

            sr.Close();
            sw.Close();

            void Delete(bool _isFront = true)
            {

                if (count == 0) return;
                count--;
                if (_isFront) head++;
            }

            void Insert(int _x)
            {

                // 시간초과 뜨는 코드다!
                // 순차적으로 큰 값을 넣으면 N^2의 시간이 된다!
                int l = head;
                int r = head + count - 1;

                while(l <= r)
                {

                    int mid = (l + r) / 2;
                    if (myArr[mid] < _x) l = mid + 1;
                    else r = mid - 1;
                }

                int s = r + 1;
                int tail = head + count - 1;
                for (int i = tail; i >= s; i--)
                {

                    myArr[i + 1] = myArr[i];
                }
                myArr[s] = _x;
                count++;
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
#elif TimeOut

            string EMPTY = "EMPTY\n";
            int INSERT = 'I' - '0';
            int DELETE = 'D' - '0';

            StreamReader sr = new(Console.OpenStandardInput());
            StreamWriter sw = new(Console.OpenStandardOutput());

            int test = ReadInt();
            MyData arr = new();

            while(test-- > 0)
            {

                arr.Clear();
                int n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    if (f == INSERT) arr.Insert(b);
                    else if (f == DELETE) arr.Delete(b);
                }

                if (arr.Count == 0) sw.Write(EMPTY);
                else sw.Write($"{arr.Max} {arr.Min}\n");
            }

            sr.Close();
            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
#else

            string EMPTY = "EMPTY\n";
            int INSERT = 'I' - '0';

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 16);

            int test = ReadInt();

            Comparer<int> dec = Comparer<int>.Create((x, y) => y.CompareTo(x));

            PriorityQueue<int, int> min = new(1_000_000);
            PriorityQueue<int, int> max = new(1_000_000, dec);

            PriorityQueue<int, int> popMin = new(1_000_000);
            PriorityQueue<int, int> popMax = new(1_000_000, dec);

            while(test-- > 0)
            {

                int n = ReadInt();
                int count = 0;
                for (int i = 0; i < n; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    if (f == INSERT)
                    {

                        min.Enqueue(b, b);
                        max.Enqueue(b, b);
                        count++;
                    }
                    else
                    {

                        if (count == 0) continue;
                        count--;

                        if (b == 1)
                        {

                            int a = GetVal(max, popMax);
                            popMin.Enqueue(a, a);
                        }
                        else if (b == -1)
                        {

                            int a = GetVal(min, popMin);
                            popMax.Enqueue(a, a);
                        }
                    }
                }

                if (count == 0) sw.Write(EMPTY);
                else
                {

                    int ret1 = GetVal(max, popMax);
                    int ret2 = GetVal(min, popMin);

                    sw.Write($"{ret1} {ret2}\n");
                }

                min.Clear();
                max.Clear();
                popMin.Clear();
                popMax.Clear();
            }

            sr.Close();
            sw.Close();

            int GetVal(PriorityQueue<int, int> _get, PriorityQueue<int, int> _chk)
            {

                while(_chk.Count > 0 && _get.Peek() == _chk.Peek())
                {

                    _chk.Dequeue();
                    _get.Dequeue();
                }

                return _get.Dequeue();
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
#endif
        }

#if TimeOut
        class MyData
        {

            private Node root;
            private int count = 0;
            public int Count => count;

            public void Clear()
            {

                root = null;
                count = 0;
            }

            private Node RotL(Node _parent)
            {

                Node newParent = _parent.left;
                Node nullNode = newParent.right;

                newParent.right = _parent;
                _parent.left = nullNode;

                _parent.height = Math.Max(Height(_parent.left), Height(_parent.right)) + 1;
                newParent.height = Math.Max(Height(newParent.left), Height(newParent.right)) + 1;

                return newParent;
            }

            private Node RotR(Node _parent)
            {

                Node newParent = _parent.right;
                Node nullNode = newParent.left;

                newParent.left = _parent;
                _parent.right = nullNode;

                _parent.height = Math.Max(Height(_parent.left), Height(_parent.right)) + 1;
                newParent.height = Math.Max(Height(newParent.left), Height(newParent.right)) + 1;

                return newParent;
            }

            public void Insert(int _val)
            {

                root = InsertNode(root, _val);
            }

            private Node InsertNode(Node _node, int _val)
            {

                if (_node == null)
                {

                    Node ret = new Node();
                    ret.val = _val;
                    count++;
                    return ret;
                }

                if (_val < _node.val) _node.left = InsertNode(_node.left, _val);
                else _node.right = InsertNode(_node.right, _val);

                _node.height = Math.Max(Height(_node.left), Height(_node.right));

                _node = SetBalance(_node);

                return _node;
            }

            public int Max
            {

                get
                {

                    Node ret = root;
                    while(ret.right != null)
                    {

                        ret = ret.right;
                    }

                    return ret.val;
                }
            }

            public int Min
            {

                get
                {

                    Node ret = root;
                    while(ret.left != null)
                    {

                        ret = ret.left;
                    }

                    return ret.val;
                }
            }

            public void Delete(int _val)
            {

                if (count == 0) return;

                if (_val > 0)
                {

                    DeleteMax(root);
                    return;
                }

                if (_val < 0)
                {

                    DeleteMin(root);
                    return;
                }
            }

            private Node DeleteMax(Node _node)
            {

                if (_node.right == null)
                {

                    Node temp = _node.left;
                    _node = null;
                    count--;
                    return temp;
                }

                Node rightNode = DeleteMax(_node.right);
                _node.right = rightNode;

                _node.height = Math.Max(Height(_node.left), Height(_node.right));

                SetBalance(_node);
                return _node;
            }

            private Node DeleteMin(Node _node)
            {

                if (_node.left == null) 
                {

                    Node temp = _node.right;
                    _node = null;
                    count--;
                    return temp;
                }

                Node leftNode = DeleteMin(_node.left);
                _node.left = leftNode;

                _node.height = Math.Max(Height(_node.left), Height(_node.right));

                SetBalance(_node);
                return _node;
            }
            

            private Node SetBalance(Node _node)
            {

                int curB = GetBalance(_node);

                if (curB > 1)
                {

                    int leftB = GetBalance(_node.left);
                    if (leftB > 0) return RotL(_node);

                    if (leftB < 0)
                    {

                        _node.left = RotR(_node.left);
                        return RotL(_node);
                    }
                }
                
                if (curB < -1)
                {

                    int rightB = GetBalance(_node.right);
                    if (rightB < 0) return RotR(_node);

                    if (rightB > 0)
                    {

                        _node.right = RotL(_node.right);
                        return RotR(_node);
                    }
                }

                return _node;
            }

            private int GetBalance(Node _node)
            {

                if (_node == null) return 0;

                return Height(_node.left) - Height(_node.right);
            }

            private int Height(Node _node)
            {

                if (_node == null) return -1;
                return _node.height;
            }

            public class Node 
            {

                public Node left;
                public Node right;

                public int val;
                public int height;
            }
        }
#endif


#if other
using System;
using System.Collections.Generic;
using System.IO;

var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int c, t = 0;
while ((c = sr.Read()) != '\n')
{
    t *= 10;
    t += c - '0';
}

var min = new DeletableHeap(true);
var max = new DeletableHeap(false);
var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
Span<char> outChunk = stackalloc char[11];
while (t-- > 0)
{
    var k = 0;
    while ((c = sr.Read()) != '\n')
    {
        k *= 10;
        k += c - '0';
    }

    var cnt = 0;
    while (k-- > 0)
    {
        var isI = sr.Read() == 'I';
        sr.Read();
        var n = 0;
        if ((c = sr.Read()) == '-')
            while ((c = sr.Read()) != '\n')
            {
                n *= 10;
                n -= c - '0';
            }
        else
        {
            n = c - '0';
            while ((c = sr.Read()) != '\n')
            {
                n *= 10;
                n += c - '0';
            }
        }

        if (isI)
        {
            min.Enqueue(n);
            max.Enqueue(n);
            cnt++;
        }
        else if (cnt > 0)
        {
            cnt--;
            if (n == 1)
                min.Remove(max.Dequeue());
            else
                max.Remove(min.Dequeue());
        }
    }

    if (cnt != 0)
    {
        max.Dequeue().TryFormat(outChunk, out var len);
        sw.Write(outChunk[..len]);
        sw.Write(' ');
        min.Dequeue().TryFormat(outChunk, out len);
        sw.Write(outChunk[..len]);
        sw.Write('\n');
    }
    else sw.Write("EMPTY\n");
    min.Clear();
    max.Clear();
}
sr.Close();
sw.Close();

struct DeletableHeap
{
    const int InputMax = 1_000_000;
    PriorityQueue<int, int> _data = new(InputMax);
    PriorityQueue<int, int> _deleted = new(InputMax);
    bool _sign;

    public DeletableHeap(bool sign) => _sign = sign;

    public void Enqueue(int o) => _data.Enqueue(o, _sign ? o : ~o);
    public int Dequeue()
    {
        int ret = _data.Dequeue();
        while (_deleted.TryPeek(out var d, out _) && d == ret)
        {
            _deleted.Dequeue();
            ret = _data.Dequeue();
        }
        return ret;
    }
    public void Remove(int o) => _deleted.Enqueue(o, _sign ? o : ~o);
    public void Clear()
    {
        _data.Clear();
        _deleted.Clear();
    }
}
#elif other2
using System.Text;

StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int t = int.Parse(sr.ReadLine());
while(t-- > 0)
{
    int n = int.Parse(sr.ReadLine());

    PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
    PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>();
    Dictionary<int, int> minOut = new(), maxOut = new();


    int listCount = 0;
    for(int i = 0; i < n; i++)
    {
        string[] ss = sr.ReadLine().Split();

        int key = int.Parse(ss[1]);

        switch (ss[0])
        {
            case "I":
                minHeap.Enqueue(key, key);
                maxHeap.Enqueue(key, ~key);
                listCount++;
                break;
            case "D":

                if (listCount == 0)
                    continue;
                listCount--;

                switch (key)
                {
                    case -1:
                        while(true)
                        {
                            int pop = minHeap.Dequeue();

                            if (maxOut.ContainsKey(pop) && maxOut[pop] > 0)
                                maxOut[pop]--;
                            else
                            {
                                if(minOut.ContainsKey(pop))
                                    minOut[pop]++;
                                else
                                    minOut.Add(pop, 1);
                                break;
                            }
                        }
                        break;
                    case 1:

                        while (true)
                        {
                            int pop = maxHeap.Dequeue();

                            if (minOut.ContainsKey(pop) && minOut[pop] > 0)
                                minOut[pop]--;
                            else
                            {
                                if (maxOut.ContainsKey(pop))
                                    maxOut[pop]++;
                                else
                                    maxOut.Add(pop, 1);
                                break;
                            }
                        }

                        break;
                }
                break;
        }
    }
    if (listCount == 0)
        sw.WriteLine("EMPTY");
    else
    {
        int minValue, maxValue;

        while (true)
        {
            int pop = minHeap.Dequeue();

            if (maxOut.ContainsKey(pop) && maxOut[pop] > 0)
                maxOut[pop]--;
            else
            {
                minValue = pop;
                break;
            }
        }
        while (true)
        {
            int pop = maxHeap.Dequeue();

            if (minOut.ContainsKey(pop) && minOut[pop] > 0)
                minOut[pop]--;
            else
            {
                maxValue = pop;
                break;
            }
        }
        sw.WriteLine(maxValue + " " + minValue);

    }
}

sw.Flush();
sr.Close();
sw.Close();
#elif other3
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
class Programs
{
    class MyPriorityQueue<T> where T : IComparable
    {
        private List<T> arr = new List<T>();
        private int idx = 0;
        private int size = 0;
        private readonly IComparer<T> comparer = null;
        public MyPriorityQueue(IComparer<T> comparer = null)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
        }
        public void Push(T n)
        {
            arr.Add(n);
            size++;
            idx = size - 1;
            while (comparer.Compare(arr[idx], arr[(idx - 1) / 2]) > 0)//자식이 부모보다 크다.==루트가 제일 큰 값
            {
                Swap(idx, (idx - 1) / 2);
                idx = (idx - 1) / 2;
            }
        }
        private void Swap(int a, int b)
        {
            T temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
        public T Pop()
        {
            if (size.Equals(0))
            {
                return default(T);
            }
            T t = arr[0];
            arr[0] = arr[size - 1];
            arr.RemoveAt(size - 1);
            size--;
            int current = 0, child = 0;
            while ((current * 2) + 1 <= size - 1)
            {
                child = current * 2 + 1;
                if (child + 1 <= size - 1)
                {
                    if (comparer.Compare(arr[child], arr[child + 1]) < 0)
                    {
                        child++;
                    }
                }
                if (comparer.Compare(arr[current], arr[child]) > 0)
                {
                    break;
                }
                Swap(current, child);
                current = child;
            }
            return t;
        }
        public int Size()
        {
            return size;
        }
        public bool Empty()
        {
            if (size.Equals(0))
            {
                return true;
            }
            return false;
        }
        public T top()
        {
            if (size == 0)
            {
                return default(T);
            }
            return arr[0];
        }
        public void Clear()
        {
            arr.Clear();
            size = 0;
        }
    }
    static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), Encoding.Default);
    static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), Encoding.Default);
    static Dictionary<int, int> dic = new Dictionary<int, int>();
    static void Main(String[] args)
    {
        int t = int.Parse(sr.ReadLine());
        MyPriorityQueue<int> maxPq = new MyPriorityQueue<int>();
        var cmp = Comparer<int>.Create((x, y) => { return y.CompareTo(x); });
        MyPriorityQueue<int> minPq = new MyPriorityQueue<int>(cmp);
        for (int a = 0; a < t; a++)
        {
            int n = int.Parse(sr.ReadLine());
            dic.Clear();
            maxPq.Clear();
            minPq.Clear();
            for (int i = 0; i < n; i++)
            {
                string[] s = sr.ReadLine().Split();
                int value = int.Parse(s[1]);
                if (s[0] == "I")
                {
                    maxPq.Push(value);
                    minPq.Push(value);
                    if (dic.ContainsKey(value))
                    {
                        dic[value]++;
                    }
                    else
                    {
                        dic.Add(value, 1);
                    }
                }
                else
                {
                    if (value == 1)
                    {
                        if (dic.Count > 0)
                        {
                                
                            dic[maxPq.top()]--;
                            if (dic[maxPq.top()] == 0)
                            {
                                dic.Remove(maxPq.top());
                            }
                            maxPq.Pop();
                           
                        }
                    }
                    else
                    {
                        if (dic.Count > 0)
                        {
                            dic[minPq.top()]--;
                            if (dic[minPq.top()] == 0)
                            {
                                dic.Remove(minPq.top());
                            }
                            minPq.Pop();
                        }
                    }

                }
                while (dic.Count > 0 && !dic.ContainsKey(maxPq.top()))
                {
                    maxPq.Pop();
                }
                while (dic.Count > 0 && !dic.ContainsKey(minPq.top()))
                {
                    minPq.Pop();
                }
            }
            if (dic.Count == 0)
            {
                sw.WriteLine("EMPTY");
            }
            else
            {
                int max = maxPq.top(), min = minPq.top();
                sw.WriteLine($"{max} {min}");
            }
        }
        sw.Dispose();
    }
}
#elif other4
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var t = Int32.Parse(sr.ReadLine());
        var idxGen = 0;

        while (t-- > 0)
        {
            var set = new SortedSet<(int val, int idx)>();
            var q = Int32.Parse(sr.ReadLine());

            while (q-- > 0)
            {
                var query = sr.ReadLine().Split(' ');
                if (query[0] == "I")
                {
                    set.Add((Int32.Parse(query[1]), idxGen++));
                }
                else
                {
                    if (!set.Any())
                        continue;

                    if (query[1] == "1")
                    {
                        set.Remove(set.Max);
                    }
                    else
                    {
                        set.Remove(set.Min);
                    }
                }
            }

            if (!set.Any())
                sw.WriteLine("EMPTY");
            else
                sw.WriteLine($"{set.Max.val} {set.Min.val}");
        }
    }
}
#elif other5
using System;
using System.Text;

namespace no7662try3
{
    internal class Program
    {
        const int MAX_HEAP = 0;
        const int MIN_HEAP = 1;
        const bool DEBUG = true;
        const bool RELEASE = false;
        const bool STATUS = RELEASE;

        // 최소 힙과 최대 힙을 활용하고 원소에는 각 힙의 인덱스 번호를 가지고 있도록 하자.
        class HeapElement
        {
            public int data;
            public int[] heapIndex;

            public override string ToString()
            {
                return $"{{data : {data}, index : [{heapIndex[0]}, {heapIndex[1]}] }}";
            }
        }

        static StringBuilder result;
        static HeapElement[,] heap;
        static int heapSize;

        static void Init()
        {
            heap = new HeapElement[2, 1_000_001];
            heapSize = 0;
        }
        static void PrintHeap()
        {

            Console.Write("Max heap : ");

            for (int index = 1, x = 1; index <= heapSize; ++index)
            {
                if (x < index + 1)
                {
                    Console.WriteLine();
                    x <<= 1;
                }
                Console.Write($"{heap[MAX_HEAP, index]} ");
            }
            Console.WriteLine();
            Console.Write("Min heap : ");

            for (int index = 1, x = 1; index <= heapSize; ++index)
            {
                if (x < index + 1)
                {
                    Console.WriteLine();
                    x <<= 1;
                }
                Console.Write($"{heap[MIN_HEAP, index]} ");
            }
            Console.WriteLine();
        }
        // Swap은 잘못 없어.
        static void Swap(int targetHeap, int leftIndex, int rightIndex)
        {
            HeapElement temp = heap[targetHeap, leftIndex];
            heap[targetHeap, leftIndex] = heap[targetHeap, rightIndex];
            heap[targetHeap, rightIndex] = temp;
            heap[targetHeap, leftIndex].heapIndex[targetHeap] = leftIndex;
            heap[targetHeap, rightIndex].heapIndex[targetHeap] = rightIndex;
        }
        // very suspicious!
        static void Pop(int targetHeap)
        {
            if (STATUS)
            {
                Console.WriteLine($"Pop({targetHeap}) : 호출됨");
            }

            // 해당 힙의 머리 원소 제거
            // 그다음 옆의 힙의 해당 위치의 원소 제거
            // 각 heap Heapify 진행 -> 빈곳에 가장 마지막의 원소 넣기(가장 00에서 반대이므로) -> 새로 채워진 그 원소가 자식 원소와 교체할 수 있는가 여부 판단

            if (heapSize < 1)
            {
                return;
            }

            if (STATUS)
            {
                Console.WriteLine($"Pop({targetHeap}) : heapify 이전 힙의 상황");
                PrintHeap();
                Console.WriteLine($"Pop({targetHeap}) : heapify 진행");
                Console.WriteLine($"Pop({targetHeap}) : 힙에서 제거될 원소 : {heap[targetHeap, 1]}");
                Console.WriteLine($"Pop({targetHeap}) : 최대 힙에서 빈칸을 대체할 원소 : {heap[MAX_HEAP, heapSize]}");
                Console.WriteLine($"Pop({targetHeap}) : 최소 힙에서 빈칸을 대체할 원소 : {heap[MIN_HEAP, heapSize]}");
            }

            
            HeapElement temp = heap[targetHeap, 1];
            int minHeapIndex = temp.heapIndex[MIN_HEAP];
            int maxHeapIndex = temp.heapIndex[MAX_HEAP];
            --heapSize;


            // 빈칸을 채우는 경우 heapify를 두번 해야 함. 빈칸에서 자식까지, 혹은 빈칸에서 부모까지

            // min heap heapify
            Swap(MIN_HEAP, minHeapIndex, heapSize + 1);
            
            // heapify : 빈칸에서 자식의 말단까지 heapify
            for (int index = minHeapIndex; (index << 1) <= heapSize;)
            {
                int compareChildIndex = (index << 1);
                if (compareChildIndex + 1 <= heapSize)
                {
                    if (heap[MIN_HEAP, compareChildIndex].data > heap[MIN_HEAP, compareChildIndex + 1].data)
                    {
                        ++compareChildIndex;
                    }
                }

                if (heap[MIN_HEAP, index].data > heap[MIN_HEAP, compareChildIndex].data)
                {
                    Swap(MIN_HEAP, index, compareChildIndex);
                    index = compareChildIndex;
                }
                else break;
            }
            // heapify : 빈칸에서 부모 노드의 끝까지 heapify
            // push의 heapify와 동일합니다.
            for (int index = minHeapIndex; (index >> 1) > 0; index >>= 1)
            {
                if (heap[MIN_HEAP, index].data >= heap[MIN_HEAP, index >> 1].data) break;

                Swap(MIN_HEAP, index, index >> 1);
            }

            // 최대 힙 상황 : 큰 수 -> 빈칸 -> 작은 수
            // max heap heapify
            Swap(MAX_HEAP, maxHeapIndex, heapSize + 1);

            // heapify : 두가지의 문제 상황중 하나가 발생하게 됨 : 큰 수인 빈칸의 부모 - 빈 칸 - 작은 수인 빈칸의 자식일때
            // 빈칸의 부모보다 큰 숫자인 빈칸이거나, 빈칸의 자식보다 작은 숫자인 빈칸인 경우 중 하나 문제 상황을 해결해야 한다.
            // heapify : 빈칸 -> 작은수 관계에서 빈칸이 자식의 작은 수보다 작은 문제 상황을 해결하는 루프문
            for (int index = maxHeapIndex; (index << 1) <= heapSize;)
            {
                int compareChildIndex = index << 1;
                if (compareChildIndex + 1 <= heapSize)
                {
                    if (heap[MAX_HEAP, compareChildIndex].data < heap[MAX_HEAP, compareChildIndex + 1].data)
                    {
                        ++compareChildIndex;
                    }
                }

                if (heap[MAX_HEAP, index].data < heap[MAX_HEAP, compareChildIndex].data)
                {
                    Swap(MAX_HEAP, index, compareChildIndex);
                    index = compareChildIndex;
                }
                else break;
            }
            // heapify : 큰 수 -> 빈칸 관계에서 빈칸이 자신의 부모보다 더 큰 수인 문제 상황을 해결하는 루프문
            for (int index = maxHeapIndex; (index >> 1) > 0; index >>= 1)
            {
                if (heap[MAX_HEAP, index].data <= heap[MAX_HEAP, index >> 1].data) break;

                Swap(MAX_HEAP, index, index >> 1);
            }


            if (STATUS)
            {
                Console.WriteLine($"Pop({targetHeap}) : heapify 이후의 힙의 상황");
                PrintHeap();
                Console.WriteLine($"Pop({targetHeap}) : 종료됨");
            }

        }
        static void Push(int _data)
        {
            // 새로운 원소 생성 후 각 힙에 넣기
            ++heapSize;
            HeapElement newElement = new HeapElement()
            {
                data = _data,
                heapIndex = new int[] { heapSize, heapSize }
            };
            heap[MIN_HEAP, heapSize] = newElement;
            heap[MAX_HEAP, heapSize] = newElement;
            
            for (int index = heapSize; (index >> 1) > 0; index >>= 1)
            {
                if (heap[MIN_HEAP, index].data >= heap[MIN_HEAP, (index >> 1)].data) // 부모와 자식의 크기 관계가 적절한 상태입니다.
                {
                    break; // 만약 조건을 불만족하면 즉시 루프 탈출
                }

                // 스왑
                Swap(MIN_HEAP, index, index >> 1);
            }
            for (int index = heapSize; (index >> 1) > 0; index >>= 1)
            {
                if (heap[MAX_HEAP, index].data <= heap[MAX_HEAP, (index >> 1)].data) // 부모와 자식의 크기 관계가 적절한 상태입니다.
                {
                    break; // 만약 조건을 불만족하면 즉시 루프 탈출
                }

                // 스왑
                Swap(MAX_HEAP, index, index >> 1);
            }
        }
        // 아무래도 잘못이 없는 것 같다.
        static void Print()
        {
            if (heapSize < 1)
            {
                result.AppendLine("EMPTY");
            }
            else
            {
                result.AppendLine($"{heap[MAX_HEAP, 1].data} {heap[MIN_HEAP, 1].data}");
            }
        }

        static void Main(string[] args)
        {
            result = new StringBuilder();

            int T = int.Parse(Console.ReadLine());
            for (int i = 0; i < T; ++i)
            {
                Init();
                int k = int.Parse(Console.ReadLine());

                for (int j = 0; j < k; ++j)
                {
                    string[] commandLine = Console.ReadLine().Split(' ');
                    switch (commandLine[0])
                    {
                        case "I":
                            Push(int.Parse(commandLine[1]));
                            break;
                        case "D":
                            if (commandLine[1].Equals("1")) Pop(MAX_HEAP);
                            else Pop(MIN_HEAP);
                            break;
                        default:
                            break;
                    }
                }

                if (STATUS)
                {
                    Console.WriteLine("연산 종료 이후 힙의 상황");
                    PrintHeap();
                }

                Print();
            }

            Console.Write(result);
        }
    }
}
#endif
    }
}
