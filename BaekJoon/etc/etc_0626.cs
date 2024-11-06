using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 빈도 정렬
    문제번호 : 2910번

    정렬, 해시 문제다
    아이디어는 다음과 같다
    숫자 점위가 10억까지이므로 숫자를 작은 수로 변형했다
    이에 딕셔너리 (해시)를 사용했다
    다음으로 조건에 맞게 정렬했다
*/

namespace BaekJoon.etc
{
    internal class etc_0626
    {

        static void Main626(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;

            int[] arr;
            int[] cnt;
            int[] start;

            Dictionary<int, int> nTi;
            Solve();

            void Solve()
            {

                Input();

                Array.Sort(arr, Comparer<int>.Create((x, y) => 
                {

                    int idx1 = nTi[x];
                    int idx2 = nTi[y];

                    int ret = cnt[idx2].CompareTo(cnt[idx1]);
                    if (ret != 0) return ret;

                    return start[idx1].CompareTo(start[idx2]);
                }));

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{arr[i]} ");
                }

                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                ReadInt();
                arr = new int[n];
                cnt = new int[n];
                start = new int[n];

                nTi = new(n);
                int idx = 0;
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    arr[i] = cur;

                    if (nTi.ContainsKey(cur))
                    {

                        int add = nTi[cur];
                        cnt[add]++;
                    }
                    else
                    {

                        nTi[cur] = idx;
                        idx++;
                        int add = nTi[cur];
                        cnt[add] = 1;
                        start[add] = i;
                    }
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while(( c= sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
            
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure
{
    /// <summary>
    /// 현재 만들어놓은 
    /// </summary>
    /// 
    struct LongCounter
    {
        public long number;
        public int count;
        public int appear;

        public LongCounter(long _number, int _count , int _appear)
        {
            number = _number;
            count = _count;
            appear = _appear;
        }

        public void ChangeCount(int _count)
        {
            count = _count;
        }
    }
    class _2910
    {
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

        static List<LongCounter> data = new List<LongCounter>();
        static int N;
        static long C;
        //public void Solve()
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())); // 만약 segfault오류나면 쓰지 말것.

            long[] input1 = Array.ConvertAll(sr.ReadLine().Split(), long.Parse);

            N = int.Parse(input1[0].ToString());
            C = input1[1];

            long[] input2 = Array.ConvertAll(sr.ReadLine().Split(), long.Parse);
            for(int i = 0; i < N; i++)
            {
                long tempdata = input2[i];
                bool foundsame = false;
                for(int index = 0; index < data.Count; index++)
                {
                    if(tempdata == data[index].number)
                    {
                        //Console.WriteLine("같은 숫자 발견" + tempdata + "이며 카운터를" + (data[index].count + 1) + "로 증가");
                        data[index] = new LongCounter(tempdata, data[index].count + 1, data[index].appear);
                        foundsame = true;
                        break;
                    }
                }
                if (!foundsame)
                {
                    //Console.WriteLine("처음으로 숫자 발견" + tempdata + "이며 " + i + "번째에 나타남.");
                    LongCounter tempcounter = new LongCounter(tempdata, 1, i);
                    data.Add(tempcounter);
                }
            }

            data.Sort((a,b) => {
                if (a.count == b.count)
                {
                    if (a.appear > b.appear)
                    {
                        return 1;
                    }
                    else if(a.appear < b.appear)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (a.count > b.count)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            });

            for(int i = 0; i < data.Count; i++)
            {
                for(int j = 0; j < data[i].count; j++)
                {
                    sw.Write(data[i].number + " ");
                }
            }

            sw.Close();
            sr.Close();
        }
    }


}

#endif
}
