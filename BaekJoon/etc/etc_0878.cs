using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

/*
날짜 : 2024. 8. 14
이름 : 배성훈
내용 : 키로거
    문제번호 : 5397번

    스택, 연결리스트 문제다
    스택 방법이 안떠올라 연결리스트로 해결했다
    그냥 class로 구현하면 new 연산이 많아 시간초과 날거 같았고
    두 포인터와 이전 노드와 다음 노드 참고를 index를 이용해 노드를 썼다

    특정 ptr에서 넣고 빼고 옮기기를 반복하니
    2개의 스택으로도 해결가능한거 아닌가 생각이 들었다

    아래는 다른 사람이 스택 2개를 이용해 푼 풀이다
*/

namespace BaekJoon.etc
{
    internal class etc_0878
    {

        public class MyLinkedList<T>
        {

            public class Node<T>
            {

                private T val;              // 보관한 값
                private Node<T> prev;       // 이전 노드
                private Node<T> next;       // 다음 노드

                public T Val
                {

                    set { val = value; }
                    get { return val; }
                }

                public Node<T> Prev
                {

                    set { prev = value; }
                    get { return prev; }
                }

                public Node<T> Next
                {

                    set { next = value; }
                    get { return next; }
                }

                public Node() { }

                public Node(T _val)
                {

                    val = _val;
                }
            }

            private Node<T> head;
            private Node<T> tail;

            public Node<T> First => head;
            public Node<T> Last => tail;

            public T this[int _idx]
            {

                get
                {

                    Node<T> ret = head.Next;

                    for (int i = 0; i < _idx; i++)
                    {

                        if (IsLast(ret)) throw new IndexOutOfRangeException();
                        ret = ret.Next;
                    }

                    return ret.Val;
                }
            }

            public bool IsFirst(Node<T> _chk) => _chk == head;

            public bool IsLast(Node<T> _chk) => _chk == tail;

            public MyLinkedList()
            {

                head = new();
                tail = new();

                head.Next = tail;
                tail.Prev = head;
            }

            /// <summary>
            /// prev에 뒤에 insert 노드 삽입한다
            /// </summary>
            public void Insert(Node<T> _prev, Node<T> _insert)
            {

                if (IsLast(_prev)) throw new Exception("Last는 뒤에 이어붙일 수 없습니다.");
                if (IsFirst(_insert)) throw new Exception("First는 앞에 이어붙일 수 없습니다.");

                Node<T> tempNext = _prev.Next;
                _prev.Next = _insert;

                _insert.Prev = _prev;
                _insert.Next = tempNext;

                if (tempNext != null) tempNext.Prev = _insert;
            }

            /// <summary>
            /// insert 값을 가진 노드를 생성하고,
            /// prev 뒤에 삽입한다
            /// </summary>
            public void Insert(Node<T> _prev, T _insert)
            {

                if (IsLast(_prev)) throw new Exception("Last는 뒤에 이어붙일 수 없습니다.");
                Node<T> insertNode = new(_insert);
                Insert(_prev, insertNode);
            }

            /// <summary>
            /// 해당 노드를 끊는다
            /// </summary>
            public void Pop(Node<T> _pop)
            {

                if (IsFirst(_pop)) throw new Exception("First는 뺄 수 없습니다.");
                else if (IsLast(_pop)) throw new Exception("Last는 뺄 수 없습니다.");

                Node<T> tempPrev = _pop.Prev;
                Node<T> tempNext = _pop.Next;

                _pop.Next = null;
                _pop.Prev = null;

                if (tempPrev != null) tempPrev.Next = tempNext;
                if (tempNext != null) tempNext.Prev = tempPrev;
            }
        }

        static void Main878(string[] args)
        {

            int HEAD = 0;
            int TAIL = 1_000_001;
            (int prev, int next, char c)[] node;
            int curPtr, addPtr;

            StreamReader sr;
            StreamWriter sw;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while (test-- > 0)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                addPtr = 1;
                curPtr = HEAD;
                node[HEAD].next = TAIL;
                node[TAIL].prev = HEAD;

                int c;
                while ((c = sr.Read()) != -1 && c != '\r' && c != '\n')
                {

                    ReadKey(c);
                }

                if (c == '\r') sr.Read();
            }

            void GetRet()
            {

                int curPtr = node[HEAD].next;

                while (curPtr != TAIL)
                {

                    sw.Write(node[curPtr].c);
                    curPtr = node[curPtr].next;
                }

                sw.Write('\n');
            }

            void Insert()
            {

                int tempNext = node[curPtr].next;
                node[curPtr].next = addPtr;
                node[tempNext].prev = addPtr;

                node[addPtr].prev = curPtr;
                node[addPtr].next = tempNext;

                curPtr = addPtr;
                addPtr++;
            }

            void Pop()
            {

                int tempPrev = node[curPtr].prev;
                int tempNext = node[curPtr].next;

                node[tempPrev].next = tempNext;
                node[tempNext].prev = tempPrev;

                curPtr = tempPrev;
            }

            void ReadKey(int _key)
            {

                if (_key == '<')
                {

                    if (curPtr == HEAD) return;
                    curPtr = node[curPtr].prev;
                    return;
                }
                else if (_key == '>')
                {

                    if (node[curPtr].next == TAIL) return;
                    curPtr = node[curPtr].next;
                    return;
                }
                else if (_key == '-')
                {

                    if (curPtr == HEAD) return;
                    Pop();
                    return;
                }

                node[addPtr].c = (char)_key;
                Insert();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                node = new (int prev, int next, char c)[1_000_002];
                node[HEAD].prev = -1;
                node[TAIL].next = -1;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include <cstdio>

char rbuf[1 << 17];
char wbuf[1 << 17];
int idx, ridx, widx;

inline void bflush(){
    fwrite(wbuf, 1, widx, stdout);
    widx = 0;
}

inline void write(char c){
    if(widx == (1<<17)) bflush();
    wbuf[widx++] = c;
}

inline char read(){
    if(idx == ridx){
        ridx = fread(rbuf, 1, 1<<17, stdin);
        if(!ridx) return 0;
        idx = 0;
    }
    return rbuf[idx++];
}

inline int readInt(){
    int sum = 0;
    char now = read();
    
    while(now <= 32) now = read();
    while(now >= 48) sum = sum * 10 + now - '0', now = read();
    
    return sum;
}

inline void readChar(char c[]){
    int index = 0;
    char now = read();
    
    while(now <= 32) now = read();
    while(now >= 45) c[index++] = now, now = read();
    c[index] = '\0';
}

int main(void) {
    int T = readInt(), LI, RI;
    char S[1000001], LS[1000001], RS[1000001];
    while (T--) {
        readChar(S);
        LI = 0, RI = 0;
        for (int i = 0; S[i]; i++) {
            if (S[i] == '<') {
                if (LI != 0) RS[RI++] = LS[--LI];
            }
            else if (S[i] == '>') {
                if (RI != 0) LS[LI++] = RS[--RI];
            }
            else if (S[i] == '-') {
                if (LI != 0) LI--;
            }
            else LS[LI++] = S[i];
        }

        for (int i = 0; i < LI; i++) write(LS[i]);
        for (int i = RI - 1; i >= 0; i--) write(RS[i]);
        write('\n');
    }
    bflush();
}
#endif
}
