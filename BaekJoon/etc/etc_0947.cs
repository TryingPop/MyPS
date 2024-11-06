using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 6
이름 : 배성훈
내용 : 개구리 징검다리 건너기
    문제번호 : 21873번

    애드 혹, 해 구성하기 문제다
    총 시행횟수를 출력안해 3번 틀렸다

    아이디어는 다음과 같다
    시뮬레이션 돌려보면, 색깔별로 1칸, 2칸, 3칸, 4칸, ... , n칸,
    n칸, n칸, n - 1칸, n - 2칸 ,... , 1칸 순으로 이동한다

    총 시행횟수는 n x (n + 2)번이고,
    색깔을 바꾸면서 1 ~ n까지 올려가고
    n번 출력하고
    n ~ 1까지 내려가면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0947
    {

        static void Main947(string[] args)
        {

            StreamWriter sw;
            StringBuilder sb;

            int n;

            Solve();
            void Solve()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 16);
                sb = new(40);

                n = int.Parse(Console.ReadLine());

                int color = 2;
                sb.Append(n * (n + 2));
                sb.Append('\n');
                sw.Write(sb);
                sb.Clear();
                for (int i = 1; i <= n; i++)
                {

                    color = color == 1 ? 2 : 1;
                    for (int j = 1; j <= i; j++)
                    {

                        sb.Append(color);
                        sb.Append(' ');
                        sb.Append(j);
                        sb.Append('\n');
                        sw.Write(sb);
                        sb.Clear();
                    }
                }

                sw.Flush();
                color = color == 1 ? 2 : 1;
                for (int i = 1; i <= n; i++)
                {

                    sb.Append(color);
                    sb.Append(' ');
                    sb.Append(i);
                    sb.Append('\n');
                    sw.Write(sb);
                    sb.Clear();
                }

                for (int i = 0; i < n; i++)
                {

                    color = color == 1 ? 2 : 1;
                    for (int j = 1 + i; j <= n; j++)
                    {

                        sb.Append(color);
                        sb.Append(' ');
                        sb.Append(j);
                        sb.Append('\n');
                        sw.Write(sb);
                        sb.Clear();
                    }
                }

                sw.Close();
            }
        }
    }
}
