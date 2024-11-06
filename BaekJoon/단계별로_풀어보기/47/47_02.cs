using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 6
이름 : 배성훈
내용 : 수상 택시
    문제번호 : 2836번

    주된 아이디어는 다음과 같다
    0 -> M으로 이동하는 방향과 반대인 경우
    즉, M -> 0 방향으로 이동하는 것만 계산하면 된다

    정방향의 경우 가는 길목에 순서 상관없이 내려주면 되기에 계산을 안한다
    0 -> M을 어차피 가야한다! 그래서 M 길이 안에 포함되어져 있다

    역방향의 경우 최대한 짧게 이동해야 하는데 이는 선긋기와 일치한다
    최대한 겹치게해서 이동해야한다!

    그런데, 주의할 점은 2배로 계산해줘야한다
    돌아간만큼 다시 앞으로 가야하기 때문이다

    그래서 M <= 10^9(10억)인데, 3M만큼 이동할 수 있고 이는 int범위를 벗어날 수 있다
    실제로 처음에 int 썼다가 틀렸다
*/

namespace BaekJoon._47
{
    internal class _47_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            (int start, int end)[] arr = new (int start, int end)[info[0]];

            for (int i = 0; i < info[0]; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                int f = int.Parse(temp[0]);
                int b = int.Parse(temp[1]);

                if (f <= b) continue;

                arr[i].start = b;
                arr[i].end = f;
            }

            sr.Close();

            Array.Sort(arr, (x, y) =>
            {

                int result = x.start - y.start;
                if (result == 0) result = x.end - y.end;
                return result;
            });

            int start = arr[0].start;
            int end = arr[0].end;
            long result = end - start;

            for (int i = 1; i < info[0]; i++)
            {

                if (end < arr[i].start)
                {

                    start = arr[i].start;
                    end = arr[i].end;
                    result += end - start;
                }
                else if (end < arr[i].end)
                {

                    result += arr[i].end - end;
                    end = arr[i].end;
                }
            }

            result *= 2;
            result += info[1];
            Console.WriteLine(result);
        }
    }
}
