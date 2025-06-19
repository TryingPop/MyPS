using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 3
이름 : 배성훈
내용 : 트리와 경로의 길이 2
    문제번호 : 12935번

    트리, 해 구성하기 문제다.
    아이디어는 다음과 같다.
    최대 노드의 갯수는 500개이다.

    트리에 노드의 갯수가 n(> 2)인 경우
    자식이 많아야 1개를 갖는 트리라 하자.
    그러면 단순 경로는 전체 노드의 갯수 - 3이된다.
    그래서 단순 경로가 400개 이하이면
    단순 경로 갯수 + 3개인 노드에 자식을 많아야 1개 갖게 한다.

    이후 자식이 400개를 초과하면,
    루트를 0으로, 루트의 왼쪽 자식을 1로하자.
    이후 1의 자식에 2번부터 1씩 증가시키며 100개를 갖게 한다.
    다음으로 2번 자식으로 (n / 100) - 1개의 자식을 부여한다.
    그러면 단순 경로의 갯수는 ((n / 100) - 1) x 100개가 된다.
    나머지 n % 100 + 100개는
    n의 오른쪽에 노드를 1개 놓는다.
    그러면 1번 자식들의 갯수로 단순경로가 100개 생긴다.
    이후 위처럼 오른쪽 자식의 서브트리로 자식이 많아야 1개 되게 트리를 n %100개를 이어 붙여만든다.
    그러면 우리가 원하는 트리가 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1513
    {

        static void Main1513(string[] args)
        {

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(Console.ReadLine());

            if (n <= 400)
            {

                int ret = n + 3;
                sw.Write(ret);
                sw.Write('\n');

                for (int i = 1; i < ret; i++)
                {

                    sw.Write($"{i - 1} {i}\n");
                }
            }
            else
            {

                int chk = n / 100;
                int twoChild = chk - 1;
                int r = n % 100;
                int ret = twoChild + 100 + 3 + r;

                sw.Write($"{ret}\n");
                sw.Write("0 1\n");
                int next = 2;
                for (int i = 0; i < 100; i++, next++)
                {

                    sw.Write($"1 {next}\n");
                }
                
                for (int i = 0; i < twoChild; i++, next++)
                {

                    sw.Write($"2 {next}\n");
                }

                sw.Write($"0 {next}\n");
                for (int i = 0; i < r; i++, next++)
                {

                    sw.Write($"{next} {next + 1}\n");
                }
            }
        }
    }
}
