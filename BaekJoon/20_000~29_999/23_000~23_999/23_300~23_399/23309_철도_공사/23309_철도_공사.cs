using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 철도 공사
    문제번호 : 23309번

    연결 리스트관련 문제다
    처음에는 문제에 맞는 연결리스트를 구현해보는 문제인줄 알았다
    그래서 처음 연결리스트 형식의 구조를 만들어봤는데 시간초과였다
    고민해보니 다음 노드를 읽는 방법이 시작지점에서 끝지점을 찾아가는 형식이라 느린게 당연했다
    최악의 경우 찾는 과정만 50만 * 150만 이니 당연하다
    그리고 클래스 형태로 만들어서 재활용 없이 new 연산을 계속하니 시간초과가 당연하다

    그래서 결국 그냥 해당 문제에 맞는 배열로 풀었다
    그리고 상황에 맞게 배열로 구현하니 처음에는 1012ms -> 696ms로 통과했다
    가장 빠른 사람이 rust로 148ms이고 c언어도 252ms이다
    대부분 1초 내외니 빠르다고 생각한다

    연결리스트를 만들면서 순환하는 자료구조라 길이 부분을 넣는게 힘들었다
    모두 길이를 수정하자니 매번 길이만큼 수정해줘야하고 당장 아이디어가 안떠올라
    처음 제출에 1개만 있으니 클래스 밖에서 길이 변수를 저장했다

    지금 다시 만들라면 길이를 처음 부분만 설정하고 탐색을 하면서 while문에 한번당 -1을 할거같다
    아니면 Lazy 세그먼트 트리처럼 lazy변수를 두고 탐색할 때 길이 조절을 하는 식으로 할거 같다

    그리고, 재귀로 다음껄 넘어가자니 10만개 이상 되면 재귀 호출이 많아 스택 오버플로우가 필히 발생할거 같아
    while문으로 탐색했다 그리고 이전으로 가는건 구현하지 않았다
    그래도 예제 문제를 넣어보면 의도한 기능은 잘 구현되는거 같다
*/
namespace BaekJoon.etc
{
    internal class etc_0273
    {
        static void Main273(string[] args)
        {

#if Wrong
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);
            Node sub;
            int len = 1;
            {

                int first = ReadInt(sr);
                sub = new(first);
                int before = first;
                
                for (int i = 1; i < n; i++)
                {

                    int id = ReadInt(sr);
                    sub.InsertNext(before, id, len);
                    len++;
                    before = id;
                }
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            for (int i = 0; i < m; i++)
            {

                int op = ReadOrder(sr);
                int ret = 0;
                if ((op & (1 << 0)) == 1)
                {

                    int id = ReadInt(sr);
                    int add = ReadInt(sr);

                    if ((op & (1 << 1)) == 2)
                    {

                        ret = sub.InsertNext(id, add, len);
                    }
                    else
                    {

                        ret = sub.InsertBefore(id, add, len);
                    }

                    len++;
                }
                else
                {

                    int id = ReadInt(sr);
                    if ((op & (1 << 1)) == 2)
                    {

                        ret = sub.PopNext(id, len);
                    }
                    else
                    {

                        ret = sub.PopBefore(id, len);
                    }
                    len--;
                }

                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();
#else
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 1024 * 1024);

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            int[] nS = new int[1_000_001];
            int[] bS = new int[1_000_001];

            {

                int first = ReadInt(sr);

                nS[first] = first;
                int beforeState = first;
                for (int i = 1; i < n; i++)
                {

                    int nextState = ReadInt(sr);
                    nS[beforeState] = nextState;
                    bS[nextState] = beforeState;
                    beforeState = nextState;
                }

                nS[beforeState] = first;
                bS[first] = beforeState;
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536 * 8);

            for (int i = 0; i < m; i++)
            {

                int op = ReadOrder(sr);
                int ret = 0;
                int id = ReadInt(sr);
                if (op == 0)
                {

                    // 이전꺼 빼야한다
                    ret = bS[id];

                    int before = bS[ret];
                    int after = nS[ret];

                    bS[after] = before;
                    nS[before] = after;

                    bS[ret] = 0;
                    nS[ret] = 0;
                }
                else if (op == 1)
                {

                    // 이전꺼 넣기
                    int newId = ReadInt(sr);

                    ret = bS[id];
                    nS[newId] = id;
                    bS[newId] = ret;

                    nS[ret] = newId;
                    bS[id] = newId;
                }
                else if (op == 2)
                {

                    // 다음꺼 빼기
                    ret = nS[id];

                    int after = nS[ret];
                    int before = bS[ret];

                    bS[after] = before;
                    nS[before] = after;

                    nS[ret] = 0;
                    bS[ret] = 0;
                }
                else
                {

                    // 다음꺼 넣기
                    int newId = ReadInt(sr);

                    ret = nS[id];
                    nS[newId] = ret;
                    bS[newId] = id;

                    bS[ret] = newId;
                    nS[id] = newId;
                }

                sw.WriteLine(ret);
                if ((i % 5_000) == 1) sw.Flush();
            }

            sr.Close();
            sw.Close();
#endif
        }

        static int ReadOrder(StreamReader _sr)
        {

            int first = _sr.Read();
            int second = _sr.Read();
            _sr.Read();

            int ret = 0;
            if (first == 'B') ret |= 1 << 0;

            if (second == 'N') ret |= 1 << 1;

            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }

#if Wrong

        // 문제 형태에 맞는 연결리스트 구현?
        public class Node
        {

            private Node next;

            public int id;

            public Node(int _id)
            {

                id = _id;
                next = this;
            }

            private Node(int _id, Node _beforeNode)
            {

                id = _id;
                Node temp = _beforeNode.next;
                _beforeNode.next = this;
                next = temp;
            }

            public int InsertNext(int _id, int _newId, int _len)
            {

                Node cur = this;
                while(cur.id != _id)
                {

                    if (_len < 0) return -1;
                    _len--;
                    cur = cur.next;
                }

                Node add = new(_newId, cur);
                return add.next.id;
            }

            public int InsertBefore(int _id, int _newId, int _len)
            {

                Node cur = this;
                while(cur.next.id != _id)
                {

                    if (_len < 0) return -1;
                    _len--;
                    cur = cur.next;
                }

                new Node(_newId, cur);
                return cur.id;
            }

            public int PopNext(int _id, int _len)
            {

                Node cur = this;

                while(cur.id != _id)
                {

                    if (_len < 0) return -1;
                    _len--;
                    cur = cur.next;
                }

                Node pop = cur.next;
                cur.next = pop.next;

                pop.next = pop;

                return pop.id;
            }

            public int PopBefore(int _id, int _len)
            {

                Node cur = this;

                while(cur.next.next.id != _id)
                {

                    if (_len < 0) return -1;
                    _len--;
                    cur = cur.next;
                }

                Node pop = cur.next;
                cur.next = pop.next;
                pop.next = pop;

                return pop.id;
            }
        }
#endif
    }
}
