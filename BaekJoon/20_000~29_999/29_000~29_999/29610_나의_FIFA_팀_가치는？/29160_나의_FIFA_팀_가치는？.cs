using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 15
이름 : 배성훈
내용 : 나의 FIFA 팀 가치는?
    문제번호 : 29160번

    우선 순위 큐를 쓰는 문제다
    직접 자료구조를 만들어 풀었다

    다만 arr 크기 변경하고 해당 capacity 수정 안해서 1번,
    선수가 없는데도 값어치가 떨어지는 부분 캐치 못해서 1번
    총 2번 틀렸다;

    248ms로 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0039
    {

        static void Main39(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int k = ReadInt(sr);

            MyData[] data = new MyData[12];
            for (int i = 1; i < 12; i++)
            {

                data[i] = new(100);
            }

            for (int i = 0; i < n; i++)
            {

                int pos = ReadInt(sr);
                data[pos].Add(ReadInt(sr));
            }

            sr.Close();

            for (int i = 0; i < k; i++)
            {

                for (int j = 1; j < 12; j++)
                {

                    data[j].HighValueMinus();
                }
            }

            int ret = 0;
            for (int i = 1; i < 12; i++)
            {

                ret += data[i].GetHighValue();
            }

            Console.WriteLine(ret);
        }

        public class MyData
        {

            private int[] _arr;
            private int _capacity;
            private int _count = 0;

            public MyData(int _size)
            {
                if (_size < 0) _size = 0;
                _capacity = _size;
                _arr = new int[_capacity];
            }

            public int Count => _count;
            
            private void SizeUp()
            {

                int doubleCapacity = _capacity == 0 ? 2 : _capacity * 2;

                int[] newArr = new int[doubleCapacity];

                Array.Copy(_arr, 0, newArr, 0, _capacity);
                _capacity = doubleCapacity;
                _arr = newArr;
            }

            // Add << 내림차순~!
            public void Add(int _add)
            {

                if(_capacity == _count) SizeUp();

                // 이제 ~ 옮기기~?
                int cur = _count++;
                _arr[cur] = _add;

                // 오름 점검!
                while (cur != 0)
                {

                    int p = (cur - 1) / 2;
                    if (_arr[p] < _arr[cur])
                    {

                        Swap(p, cur);
                        cur = p;
                    }
                    else break;
                }
            }

            public int GetHighValue()
            {

                int ret = _arr[0];
                return ret;
            }

            public void HighValueMinus()
            {

                if (_arr[0] <= 1) return;

                _arr[0]--;

                int cur = 0;
                while (true)
                {

                    int l = cur * 2 + 1;
                    int r = cur * 2 + 2;

                    if (r < _count)
                    {

                        if (_arr[l] > _arr[r] && _arr[l] > _arr[cur])
                        {

                            Swap(l, cur);
                            cur = l;
                        }
                        else if (_arr[r] > _arr[cur])
                        {

                            Swap(r, cur);
                            cur = r;
                        }
                        else break;
                    }
                    else if (l < _count)
                    {

                        if (_arr[l] > _arr[cur])
                        {

                            Swap(l, cur);
                            cur = l;
                        }
                        else break;
                    }
                    else break;
                }
            }

            private void Swap(int _idx1, int _idx2)
            {

                int temp = _arr[_idx1];
                _arr[_idx1] = _arr[_idx2];
                _arr[_idx2] = temp;
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
using StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
using StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
int[] NK = sr.ReadLine().Split().Select(int.Parse).ToArray();
int N = NK[0], K = NK[1];
PriorityQueue<int, int>[] queue = new PriorityQueue<int, int>[12];
for(int i = 1; i < 12; i++) queue[i] = new PriorityQueue<int, int>(Comparer<int>.Create((x, y) => y - x));
for(int i = 0; i < N; i++) {
    int[] PW = sr.ReadLine().Split().Select(int.Parse).ToArray();
    int P = PW[0], W = PW[1];
    queue[P].Enqueue(W, W);
}

for(int year = 1; year <= K; year++) {
    for(int p = 1; p <= 11; p++) {
        if(queue[p].Count == 0) continue;
        int w = queue[p].Dequeue();
        queue[p].Enqueue(w - 1, w - 1);
    }
}
long value = 0;
for(int p = 1; p <= 11; p++) {
    if(queue[p].Count > 0) value += queue[p].Dequeue();
}
sw.WriteLine(value);
#endif
}
