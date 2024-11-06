using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 25
이름 : 배성훈
내용 : 파일 합치기 3
    문제번호 : 13975

    소설을 합칠 때, 연속으로 합칠 필요가 없다!
    그래서 작은거끼리 빨리 합쳐서 푸는 문제이다!

    해당 단원의 문제가 아니나 30_01의 문제에서 넘어온거기에 30장에 있다!
    직접 자료형을 만들었는데 PriorityQueue랑 성능 차이가 없다
    오히려 사용 메모리만 더 늘었다... 정답 도출에는 이상없다!
*/

namespace BaekJoon._30
{
    internal class _30_07
    {

        static void Main7(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            int test = int.Parse(sr.ReadLine());
            // PriorityQueue<long, long> q = new PriorityQueue<long, long>(10_000);
            MyData myData = new(1);

            for (int t = 0; t < test; t++)
            {

                int len = int.Parse(sr.ReadLine());
                if (myData.Size < len) myData.ReSize(len);

                int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                
                for (int i = 0; i < len; i++)
                {

                    // q.Enqueue(temp[i], temp[i]);
                    myData.Add(temp[i]);
                }

                long result = 0;

                long num = 0;
                long calc = 0;
                if (myData.Count == 1) 
                { 
                    
                    sw.WriteLine(myData.Get());
                    continue;
                }

                while(true)
                {

                    calc += myData.Get();
                    num++;
                    if (num == 1) continue;

                    num = 0;
                    result += calc;
                    myData.Add(calc);
                    calc = 0;
                    if (myData.Count == 1) break;
                }

                myData.Get();
                sw.WriteLine(result);
            }

            sr.Close();
            sw.Close();
        }

        public class MyData
        {

            long[] _arr;
            int _cnt = 0;
            int _capacity = 0;
            public int Count => _cnt;
            public int Size => _capacity;
            public MyData(int capacity)
            {

                _arr = new long[capacity];
                _capacity = capacity;
            }

            public void ReSize(int _newCapacity)
            {

                long[] newArr = new long[_newCapacity];
                Array.Copy(_arr, 0, newArr, 0, _arr.Length);
                _arr = newArr;
                _capacity = _newCapacity;
            }

            public void Add(long _add)
            {

                // 이진 탐색!
                // 한도 초과면 2배 확장!
                if (_cnt + 1 > _capacity)
                {

                    int newCapacity = _capacity == 0 ? 1 : 2 * _capacity;
                    ReSize(newCapacity);
                }

                _arr[_cnt++] = _add;

                ChkUp(_cnt - 1);
            }

            void ChkUp(int _idx)
            {

                long cur = _arr[_idx];

                int chk = _idx;

                while(chk != 0)
                {

                    int next = (chk - 1) / 2;

                    if (cur < _arr[next])
                    {

                        Swap(chk, next);
                        chk = next;
                    }
                    else break;
                }
            }

            int AllDown(int _idx)
            {

                int chk = _idx;

                while (chk * 2 + 1 < _cnt)
                {

                    int left = chk * 2 + 1;
                    int right = chk * 2 + 2;

                    if (right >= _cnt) 
                    {

                        Swap(chk, left);
                        chk = left;
                        break;
                    }

                    int next = _arr[left] > _arr[right] ? right : left;
                    Swap(chk, next);
                    chk = next;
                }

                return chk;
            }


            void Swap(int _idx1, int _idx2)
            {

                long temp = _arr[_idx1];
                _arr[_idx1] = _arr[_idx2];
                _arr[_idx2] = temp;
            }

            public long Get()
            {

                long result = _arr[0];
                _arr[0] = 0;

                int chk = AllDown(0);

                if (chk != --_cnt)
                {

                    Swap(chk, _cnt);
                    ChkUp(chk);
                }
                return result;
            }
        }
    }
}
