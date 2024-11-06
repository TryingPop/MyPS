using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 28
이름 : 배성훈
내용 : Fly me to the Alpha Centauri
    문제 번호 : 1011번

    수학 문제다
    차이가 1 ~ 10까지 직접 정답을 찾아보고 규칙성을 찾아 풀었다
        1 -> 1
        2 -> 1 1
        3 -> 1 1 1
        4 -> 1 2 1
        5 -> 1 2 1 1
        6 -> 1 2 2 1
        7 -> 1 2 2 1 1
        8 -> 1 2 2 2 1
        9 -> 1 2 3 2 1
        10 -> 1 2 3 2 1 1

        ...

        n^2 -> 1 2 3 ... n ... 3 2 1
        이된다
        자리수가 늘어나는 때를 보면, n^2에서 1이 늘어나면 1자리 증가하고 n + 1 에서 또 1자리가 늘어난다
    이렇게 규칙성을 찾아 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0647
    {

        static void Main647(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                long[] sq = new long[65537];
                for (long i = 1; i < sq.Length; i++)
                {

                    sq[i] = i * i;
                }

                int test = ReadInt();
                while(test-- > 0)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    long find = t - f;

                    int l = 0;
                    int r = 65536;
                    while(l <= r)
                    {

                        int mid = (l + r) / 2;

                        if (sq[mid] <= find) l = mid + 1;
                        else r = mid - 1;
                    }

                    int idx = l - 1;

                    find -= sq[idx];
                    int ret;
                    if (find == 0) ret = 2 * idx - 1;
                    else if (find > idx) ret = 2 * idx + 1;
                    else ret = 2 * idx;
                    
                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
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
using System.Text;

class B_Diary
{
    static int Cal(int Start ,int Arrive)
    {
        int distance = Arrive - Start;  

        int Max = (int)Math.Sqrt(distance); 

        if (Max == Math.Sqrt(distance))
        {
            return (Max * 2 - 1);
        }
        else if (distance <= Max * Max + Max)
        {
            return (Max * 2);
        }
        else
        {
            return (Max * 2 + 1);
        }
    }



    static void Main()
    {
        StringBuilder builder = new StringBuilder();

        int input = int.Parse(Console.ReadLine());
           
        for (int i=0;i<input;i++)
        {
            int[] Num = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            builder.Append(Cal(Num[0], Num[1]) + "\n");

        }

        Console.WriteLine(builder);
    }

}
#endif
}
