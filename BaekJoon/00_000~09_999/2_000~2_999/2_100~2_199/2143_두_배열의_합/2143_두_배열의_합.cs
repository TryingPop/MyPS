using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 5
이름 : 배성훈
내용 : 두 배열의 합
    문제번호 : 2143번

    누적 합, 해시 문제다.
    a와 b의 부분합이 t인 개수를 찾아야 한다.
    그래서 a의 부분합들과 개수를 dictionary 자료구조에 저장했다.
    이후 b의 누적합 sum에 대해 t - sum 인 개수를 누적해 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1155
    {

        static void Main1155(string[] args)
        {

            int t;
            int n, m;
            int[] a, b;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Dictionary<int, int> dic = new(1_000_000);

                for (int s = 0; s < n; s++)
                {

                    for (int e = s + 1; e <= n; e++)
                    {

                        int sum = a[e] - a[s];
                        if (dic.ContainsKey(sum)) dic[sum]++;
                        else dic[sum] = 1;
                    }
                }

                long ret = 0;
                for (int s = 0; s <= m; s++)
                {

                    for (int e = s + 1; e <= m; e++)
                    {

                        int chk = t - (b[e] - b[s]);
                        if (dic.ContainsKey(chk)) ret += dic[chk];
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                t = ReadInt();

                n = ReadInt();
                a = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    a[i] = ReadInt() + a[i - 1];
                    
                }

                m = ReadInt();
                b = new int[m + 1];
                for (int i = 1; i <= m; i++)
                {

                    b[i] = ReadInt() + b[i - 1];
                }

                sr.Close();
                int ReadInt()
                {

                    int ret = 0;
                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == -1) return true;

                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n' && c != '\r')
                        {

                            ret = ret * 10 + c - '0';
                        }

                        if (c == '\r') sr.Read();
                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
        }
    }

#if other
using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        int t = int.Parse(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());
        int[] a = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int m = int.Parse(Console.ReadLine());
        int[] b = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        for (int i = 1; i < n; i++)
        {
            a[i] += a[i - 1];
        }
        Dictionary<int, int> dic = new();
        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                int sum = a[j] - (i == 0 ? 0 : a[i - 1]);
                if (!dic.ContainsKey(sum))
                    dic.Add(sum, 0);
                dic[sum]++;
            }
        }
        for (int i = 1; i < m; i++)
        {
            b[i] += b[i - 1];
        }
        long answer = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = i; j < m; j++)
            {
                int sum = b[j] - (i == 0 ? 0 : b[i - 1]);
                if (dic.ContainsKey(t - sum))
                    answer += dic[t - sum];
            }
        }
        Console.Write(answer);
    }
}
#endif
}
