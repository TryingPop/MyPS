using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 5
이름 : 배성훈
내용 : 웹 브라우저 1
    문제번호 : 23294번

    구현, 자료 구조, 시뮬레이션, 덱 문제다.
    연결 리스트를 이용해 문제 상황을 구현했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1676
    {

        static void Main1676(string[] args)
        {

            const int A = 'A' - '0';
            const int B = 'B' - '0';
            const int C = 'C' - '0';
            const int F = 'F' - '0';

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int m = ReadInt();
            int c = ReadInt();

            int[] arr = new int[n + 1];

            for (int i = 1; i <= n; i++)
            {

                arr[i] = ReadInt();
            }

            MyData back = new(), front = new();
            int cur = 0, use = 0;

            while (m-- > 0)
            {

                int op = ReadInt();

                switch (op)
                {

                    case A:
                        int access = ReadInt();
                        QueryAccess(access);
                        break;

                    case B:
                        QueryBack();
                        break;

                    case F:
                        QueryFront();
                        break;

                    case C:
                        QueryCompact();
                        break;
                }
            }

            sw.Write($"{cur}\n");
            back.Print(sw);
            front.Print(sw);

            void QueryCompact()
            {

                back.Compact();
            }

            void QueryAccess(int _access)
            {

                front.Clear();
                if (cur != 0) back.AddTail(cur, arr[cur]);
                cur = _access;

                back.SetCache(c - arr[cur]);
                use = back.Use + arr[cur];
            }

            void QueryBack()
            {

                int chk = back.PopTail();
                if (chk == 0) return;

                front.AddTail(cur, arr[cur]);
                cur = chk;
            }

            void QueryFront()
            {

                int chk = front.PopTail();
                if (chk == 0) return;

                back.AddTail(cur, arr[cur]);
                cur = chk;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }

        public class MyData
        {

            private Node head, tail;
            private Node pool;
            private int use;
            public int Use => use;

            public MyData()
            {

                head = new();
                tail = new();

                head.next = tail;
                tail.prev = head;

                pool = null;
                use = 0;
            }

            public void Clear()
            {

                while (head.next != tail)
                {

                    PopNode(head.next);
                }
            }

            public void Print(StreamWriter _sw)
            {

                if (tail.prev == head)
                {

                    _sw.Write("-1\n");
                    return;
                }

                Node cur = tail.prev;

                while (cur != head)
                {

                    _sw.Write($"{cur.idx} ");
                    cur = cur.prev;
                }

                _sw.Write('\n');
            }

            public void SetCache(int _max)
            {

                while (_max < use && head.next != tail)
                {

                    PopHead();
                }
            }

            public void Compact()
            {

                Node cur = tail.prev;

                while (cur != head)
                {

                    Node prev = cur.prev;

                    if (cur.ChkIdx(prev)) PopNode(cur);
                    cur = prev;
                }
            }

            public int PopTail()
            {

                if (tail.prev == head) return 0;
                int ret = tail.prev.idx;
                PopNode(tail.prev);
                return ret;
            }

            private void PopHead()
            {

                if (head.next == tail) return;
                PopNode(head.next);
            }

            public void AddTail(int _idx, int _size)
            {

                Node newNode = GetNode(_idx, _size);

                Node prev = tail.prev;
                tail.prev = newNode;
                newNode.prev = prev;

                prev.next = newNode;
                newNode.next = tail;
            }

            private void PopNode(Node _remove)
            {

                Node prev = _remove.prev;
                Node next = _remove.next;

                prev.next = next;
                next.prev = prev;

                _remove.next = pool;
                pool = _remove;
                use -= _remove.size;
            }

            private Node GetNode(int _idx, int _size)
            {

                Node ret;
                if (pool == null) ret = new();
                else
                {
                    ret = pool;
                    pool = pool.next;
                }

                ret.Set(_idx, _size);
                use += _size;

                return ret;
            }

            private class Node
            {

                internal Node prev;
                internal Node next;

                internal int idx, size;

                internal void Set(int _idx, int _size)
                {

                    idx = _idx;
                    size = _size;
                }

                internal bool ChkIdx(Node _other)
                {

                    return idx == _other.idx;
                }
            }
        }
    }
}
