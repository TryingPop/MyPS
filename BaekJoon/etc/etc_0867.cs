using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 7
이름 : 배성훈
내용 : 별 찍기 - 8
    문제번호 : 2445번

    구현 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0867
    {

        static void Main867(string[] args)
        {

            char STAR = '*';
            char EMPTY = ' ';
            char ENDL = '\n';
            StreamWriter sw;

            Solve();
            void Solve()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int n = int.Parse(Console.ReadLine());

                for (int i = 1; i < n; i++)
                {

                    for (int j = 0; j < i; j++)
                    {

                        sw.Write(STAR);
                    }

                    for (int j = 2 * (n - i); j > 0; j--)
                    {

                        sw.Write(EMPTY);
                    }

                    for (int j = 0; j < i; j++)
                    {

                        sw.Write(STAR);
                    }

                    sw.Write(ENDL);
                }

                for (int i = n; i > 0; i--)
                {

                    for (int j = 0; j < i; j++)
                    {

                        sw.Write(STAR);
                    }

                    for (int j = 2 * (n - i); j > 0; j--)
                    {

                        sw.Write(EMPTY);
                    }

                    for (int j = 0; j < i; j++)
                    {

                        sw.Write(STAR);
                    }

                    sw.Write(ENDL);
                }

                sw.Close();
            }
        }
    }

#if other
// #include<cstdio>
int n;
int main() {
	scanf("%d", &n);
	for (int i = 0; i < 2 * n - 1; i++) {
		for (int j = 0; j < 2 * n; j++) printf((2 * i - 2 * j + 1)*(2 * i + 2 * j - 4 * n + 3)<0 ? "*" : " ");
		puts("");
	}
	return 0;
}
#endif
}