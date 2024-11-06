using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 6
이름 : 배성훈
내용 : 최소 힙
    문제번호 : 1927번

    우선순위 큐 문제다
    우선순위 큐 구현을 확인하기 위해 다시 푼 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0866
    {

        static void Main866(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            PriorityQueue<int, int> q;

            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                int n = ReadInt();

                while (n-- > 0)
                {

                    int op = ReadInt();

                    if (op == 0)
                    {

                        if (q.Count == 0) sw.Write("0\n");
                        else sw.Write($"{q.Dequeue()}\n");
                    }
                    else q.Enqueue(op, op);
                }

                sw.Close();
                sr.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 655536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                q = new();
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

        class PriorityQueue<TElement, TPriority>
        {

            // 비교 방법
            private Comparer<TPriority> comp;   // 우선순위 비교 방법
            private TPriority[] compArr;        // 우선순위 배열
            private TElement[] varArr;          // 값 배열

            private int capacity;               // 배열의 용량
            private int len;                    // 자료구조에 들어간 데이터의 수

            public int Count => len;

            public TElement Peek => varArr[0];  // 가장 우선순위가 높은 원소 반환

            public PriorityQueue(int _capacity = 100)
            {

                /*
        
                용량만 설정
                */
                capacity = _capacity;
                compArr = new TPriority[capacity];
                varArr = new TElement[capacity];

                len = 0;

                // 해당 자료형의 기본 비교방법으로 우선순위를 비교한다
                comp = Comparer<TPriority>.Default;
            }

            public PriorityQueue(Comparer<TPriority> _comp)
            {

                capacity = 100;
                compArr = new TPriority[capacity];
                varArr = new TElement[capacity];

                len = 0;

                comp = _comp;
            }

            public PriorityQueue(int _capacity, Comparer<TPriority> _comp)
            {

                capacity = _capacity;
                compArr = new TPriority[capacity];
                varArr = new TElement[capacity];

                len = 0;

                comp = _comp;
            }

            public void Enqueue(TElement _ele, TPriority _priority)
            {

                /*
        
                원소를 넣는다 ele는 값, priority는 우선순위 값
                */
                if (capacity == len)
                {

                    // 배열이 꽉찬 경우 2배로 늘린다
                    int newCapacity = capacity == 0 ? 1 : capacity * 2;
                    SizeUp(newCapacity);
                }

                compArr[len] = _priority;
                varArr[len] = _ele;

                Up(len);
                len++;
            }

            public TElement Dequeue()
            {

                /*
        
                우선순위가 가장 높은 원소를 뺀다
                */
                TElement ret = varArr[0];
                len--;
                Swap(0, len);
                Down(0);

                return ret;
            }

            private void Up(int _idx)
            {

                /*
        
                부모와 비교하는 연산
                */
                while (_idx > 0)
                {

                    // 현재 노드의 부모노드
                    int parent = (_idx - 1) / 2;

                    // 부모노드의 우선순위가 높으면 자기자리이므로 종료
                    if (comp.Compare(compArr[_idx], compArr[parent]) >= 0) break;

                    // 부모의 우선순위가 낮아 자리 변환이 이뤄진다
                    Swap(_idx, parent);
                    _idx = parent;
                }
            }

            private void Down(int _idx)
            {

                /*
         
                자식과 비교하는 연산 
                */
                while (_idx < len)
                {

                    int l = _idx * 2 + 1;
                    int r = _idx * 2 + 2;

                    if (r < len)
                    {

                        // 왼쪽자식이 더 작고, 현재 노드가 왼쪽 자식보다 작은 경우
                        if (comp.Compare(compArr[l], compArr[r]) < 0
                            && comp.Compare(compArr[l], compArr[_idx]) < 0)
                        {

                            // 왼쪽 자식과 자리 변환하고 왼쪽 자식에서 다시 내림연산
                            Swap(_idx, l);
                            _idx = l;
                            continue;
                        }
                        // 오른쪽 자식이 더 작고, 현재 노드가 오른쪽 자식보다 작은 경우
                        else if (comp.Compare(compArr[r], compArr[_idx]) < 0)
                        {
                            
                    
                            // 오른쪽 자식과 자리 변환하고 오른쪽 자식에서 다시 내림연산
                            Swap(_idx, r);
                            _idx = r;
                            continue;
                        }
                    }
                    else if (r == len)
                    {

                        // 왼쪽 자식만 있고, 왼쪽자식보다 낮은 경우
                        if (comp.Compare(compArr[l], compArr[_idx]) < 0)
                        {

                            // 자리만 변환
                            // 더 이상의 자식이 없음이 보장되니 더 탐색안한다
                            Swap(_idx, l);
                            _idx = l;
                        }
                    }

                    // 자리 변환이 없으면 자기 자리에 있으므로 종료
                    break;
                }
            }

            private void SizeUp(int _newCapacity)
            {

                /*
         
                배열의 크기 증가 
                */
                Array.Resize(ref compArr, _newCapacity);
                Array.Resize(ref varArr, _newCapacity);

                capacity = _newCapacity;
            }

            private void Swap(int _idx1, int _idx2)
            {

                /*
         
                인덱스 2개 값 변환 
                */
                TPriority temp1 = compArr[_idx1];
                compArr[_idx1] = compArr[_idx2];
                compArr[_idx2] = temp1;

                TElement temp2 = varArr[_idx1];
                varArr[_idx1] = varArr[_idx2];
                varArr[_idx2] = temp2;
            }
        }
    }
}
