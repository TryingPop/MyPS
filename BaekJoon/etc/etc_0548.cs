using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 책 나눠주기
    문제번호 : 9576번

    이분 매칭, 그리디 문제다
    이분매칭 연습용으로 풀었다
    그런데, 다른 사람 풀이를 보니 그리디가 빨라보인다
    
    그리디 방법은 etc_0551의 회의실 배정, 다른 문제에 적는다
    (간단히 n의 최대값이 10만으로 확장되었다고 보면 된다)
    여기서는 이분매칭으로 해결하고 넘어간다
*/

namespace BaekJoon.etc
{
    internal class etc_0548
    {

        static void Main548(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int[] go = new int[1_001];
            int[] to = new int[1_001];
            int[,] line = new int[1_001, 2];

            bool[] visit = new bool[1_001];
            int[] match = new int[1_001];

            Solve();

            sr.Close();
            sw.Close();

            void Solve()
            {

                int test = ReadInt();
                while(test-- > 0)
                {

                    int m = ReadInt();
                    int n = ReadInt();

                    for (int i = 1; i <= n; i++)
                    {

                        line[i, 0] = ReadInt();
                        line[i, 1] = ReadInt();
                    }

                    for (int i = 1; i <= m; i++)
                    {

                        match[i] = 0;
                    }

                    int ret = 0;
                    for (int i = 1; i <= n; i++)
                    {

                        for (int j = 1; j <= m; j++)
                        {

                            visit[j] = false;
                        }

                        if (DFS(i)) ret++;
                    }

                    sw.WriteLine(ret);
                }
            }

            bool DFS(int _n)
            {

                int f = line[_n, 0];
                int t = line[_n, 1];
                for (int i = f; i <= t; i++)
                {

                    int want = i;
                    if (visit[want]) continue;
                    visit[want] = true;

                    if (match[want] == 0 || DFS(match[want]))
                    {

                        match[want] = _n;
                        return true;
                    }
                }

                return false;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' '&& c != '\n')
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
using System.Collections.Generic;

public class Program
{
    struct Student : IComparable<Student>
    {
        public int a, b;
        public Student(int a, int b)
        {
            this.a = a; this.b = b;
        }
        public int CompareTo(Student other)
        {
            if (b == other.b)
                return a - other.a;
            return b - other.b;
        }
    }
    static void Main()
    {
        int t = int.Parse(Console.ReadLine());
        StringBuilder sb = new();
        for (int i = 0; i < t; i++)
        {
            string[] nm = Console.ReadLine().Split(' ');
            int n = int.Parse(nm[0]), m = int.Parse(nm[1]);
            List<Student> students = new();
            for (int j = 0; j < m; j++)
            {
                string[] ab = Console.ReadLine().Split(' ');
                int a = int.Parse(ab[0]), b = int.Parse(ab[1]);
                students.Add(new(a, b));
            }
            students.Sort();
            int answer = 0;
            for (int j = 1; j <= n; j++)
            {
                for (int k = 0; k < students.Count; k++)
                {
                    if (students[k].a <= j && students[k].b >= j)
                    {
                        answer++;
                        students.RemoveAt(k);
                        break;
                    }
                }
            }
            sb.Append(answer);
            if (i + 1 < t)
                sb.Append('\n');
        }
        Console.Write(sb.ToString());
    }
}
#elif other2
#endif
}
