using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 28 
이름 : 배성훈
내용 : Date bugs
    문제번호 : 6335번

    구현, 브루트 포스 문제다
    출력 조건을 지키지 않아 여러 번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0917
    {

        static void Main917(string[] args)
        {

            int MAX = 10_000;
            string Quit = "0";
            string NO = "Unknown bugs detected.\n";

            StreamReader sr;
            StreamWriter sw;

            HashSet<int> cur;
            HashSet<int> next;

            Solve();
            void Solve()
            {

                Init();

                int cnt = 0;
                string temp;
                while((temp = sr.ReadLine().Trim()) != Quit)
                {

                    cnt++;
                    if (1 < cnt) sw.Write('\n');

                    sw.Write($"Case #{cnt}:\n");
                    int n = int.Parse(temp);

                    string[] input = sr.ReadLine().Split();
                    if (n == 1)
                    {

                        sw.Write($"The actual year is {input[0]}.\n");
                        continue;
                    }

                    FillInit(input);

                    for (int i = 1; i < n; i++)
                    {

                        input = sr.ReadLine().Split();
                        Find(input);
                    }


                    if (cur.Count == 0)
                        sw.Write(NO);
                    else
                        sw.Write($"The actual year is {cur.Min()}.\n");
                }

                sr.Close();
                sw.Close();
            }

            void Find(string[] _input)
            {

                next.Clear();
                int y = int.Parse(_input[0]);
                int add = int.Parse(_input[2]) - int.Parse(_input[1]);

                int year = y;
                while (year < MAX)
                {

                    if (cur.Contains(year)) next.Add(year);
                    year += add;
                }

                HashSet<int> temp = cur;
                cur = next;
                next = temp;
            }


            void FillInit(string[] _input)
            {

                cur.Clear();

                int y = int.Parse(_input[0]);
                int add = int.Parse(_input[2]) - int.Parse(_input[1]);
                int year = y;
                while (year < MAX)
                {

                    cur.Add(year);
                    year += add;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                cur = new(MAX);
                next = new(MAX);
            }
        }
    }

#if other
// #include <stdio.h>

int R[30], A[30], S[30], n;
bool check(int x) {
    for (int i=0; i<n; ++i)
        if ((x-A[i]) % S[i] != R[i])
            return 0;
    return 1;
}

int main() {
    for (int cse=1; scanf("%d", &n) && n; ++cse) {
        int y, a, b, result = 0;
        for (int i=0; i<n; ++i) {
            scanf("%d%d%d", &y, A+i, &b);
            if (y < A[i] || y >= b)
                result = 100001;
            else {
                S[i] = b - A[i];
                R[i] = y - A[i];
                if (y > result)
                    result = y;
            }
        }

        for (; result<10000; ++result)
            if (check(result))
                break;

        printf("Case #%d:\n", cse);
        if (result < 10000)
             printf("The actual year is %d.\n\n", result);
        else puts("Unknown bugs detected.\n");
    }
}
#endif
}
