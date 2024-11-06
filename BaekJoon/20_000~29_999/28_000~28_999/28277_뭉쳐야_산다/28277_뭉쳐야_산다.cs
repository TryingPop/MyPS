using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 27
이름 : 배성훈
내용 : 뭉쳐야 산다
    문제번호 : 28277번

    작은 집합에서 큰 집합으로 합치는 테크닉 문제다
    처음보는 알고리즘이라 찾아봤다
    그러니 스몰 투 라지로 많이 불린다고 한다

    아이디어는 다음과 같다 두 집합 중 작은 집합을 기준으로 
    모든 원소를 빼서 큰 집합에 넣으면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_1003
    {

        static void Main1003(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            HashSet<int>[] set;
            int n, q;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int i = 0; i < q; i++)
                {

                    int op = ReadInt();

                    if (op == 1)
                    {

                        int f = ReadInt();
                        int b = ReadInt();

                        UnionSet(f, b);
                    }
                    else
                    {

                        int idx = ReadInt();
                        sw.Write($"{set[idx].Count}\n");
                    }
                }

                sr.Close();
                sw.Close();
            }

            void UnionSet(int _i, int _j)
            {

                if (set[_i].Count < set[_j].Count)
                {

                    var temp = set[_i];
                    set[_i] = set[_j];
                    set[_j] = temp;
                }

                foreach (int ele in set[_j])
                {

                    set[_i].Add(ele);
                }

                set[_j].Clear();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                q = ReadInt();

                set = new HashSet<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    int len = ReadInt();
                    set[i] = new(len);
                    for (int j = 0; j < len; j++)
                    {

                        int ele = ReadInt();
                        set[i].Add(ele);
                    }
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
using System.IO;
using System.Text;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        StreamReader sr = new(Console.OpenStandardInput());
        string[] nq = sr.ReadLine().Split(' ');
        int n = int.Parse(nq[0]), q = int.Parse(nq[1]);
        SortedSet<int>[] array = new SortedSet<int>[n];
        for (int i = 0; i < n; i++)
        {
            array[i] = new();
            string s = sr.ReadLine();
            int value = 0;
            for (int j = s.IndexOf(' ') + 1; j < s.Length; j++)
            {
                if (s[j] == ' ')
                {
                    array[i].Add(value);
                    value = 0;
                }
                else
                    value = value * 10 + s[j] - '0';
            }
            array[i].Add(value);
        }
        StringBuilder sb = new();
        for (int i = 0; i < q; i++)
        {
            int[] query = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int a = query[1] - 1;
            if (query[0] == 1)
            {
                int b = query[2] - 1;
                if (array[a].Count > array[b].Count)
                {
                    while (array[b].Count > 0)
                    {
                        int v = array[b].Min;
                        array[a].Add(v);
                        array[b].Remove(v);
                    }
                }
                else
                {
                    while (array[a].Count > 0)
                    {
                        int v = array[a].Min;
                        array[b].Add(v);
                        array[a].Remove(v);
                    }
                    (array[b], array[a]) = (array[a], array[b]);
                }
            }
            else
                sb.Append(array[a].Count).Append('\n');
        }
        sr.Close();
        sb.Remove(sb.Length - 1, 1);
        BufferedStream bs = new(Console.OpenStandardOutput());
        string answer = sb.ToString();
        bs.Write(Encoding.Default.GetBytes(answer), 0, Encoding.Default.GetByteCount(answer));
        bs.Close();
    }
}
#endif
}
