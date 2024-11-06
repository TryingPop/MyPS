using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 죽음의 게임
    문제번호 : 17204번

    구현 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0550
    {

        static void Main550(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());
            int n = ReadInt();
            int m = ReadInt();

            int[] line = new int[n];
            for (int i = 0; i < n; i++)
            {

                line[i] = ReadInt();
            }
            sr.Close();

            int[] ret = new int[n];
            Array.Fill(ret, n + 2);
            ret[0] = 0;
            int cur = 0;
            while (ret[line[cur]] == n + 2)
            {

                ret[line[cur]] = ret[cur] + 1;
                cur = line[cur];
            }
            if (ret[m] == n + 2) Console.WriteLine(-1);
            else Console.WriteLine(ret[m]);



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

public class Program
{
    static void Main()
    {
        string[] nk = Console.ReadLine().Split(' ');
        int n = int.Parse(nk[0]), k = int.Parse(nk[1]);
        int[] next = new int[n];
        for (int i = 0; i < n; i++)
        {
            next[i] = int.Parse(Console.ReadLine());
        }
        bool[] visit = new bool[n];
        int cur = 0;
        for (int i = 0; ; i++)
        {
            if (visit[cur])
            {
                Console.Write(-1);
                break;
            }
            if (cur == k)
            {
                Console.Write(i);
                break;
            }
            visit[cur] = true;
            cur = next[cur];
        }
    }
}
#elif other2
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class Program
{
	public static void Main()
    {
        StreamReader sr = new StreamReader(Console.OpenStandardInput());

        var readLine = sr.ReadLine().Split();
        int n = int.Parse(readLine[0]);
        int k = int.Parse(readLine[1]);
        List<int> targetList = new List<int>();
        for (int i = 0; i < n; i++)
        {
            targetList.Add(int.Parse(sr.ReadLine()));
        }

        int step = 0;
        int curTarget = 0;
        HashSet<int> targetedSet = new HashSet<int>();
        while (curTarget != k)
        {
            if (targetedSet.Contains(curTarget))
            {
                step = -1;
                break;
            }
            targetedSet.Add(curTarget);
            curTarget = targetList[curTarget];
            step++;
        }

        Console.WriteLine(step.ToString());
    }
}
#endif
}
