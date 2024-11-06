using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 7
이름 : 배성훈
내용 : 귀찮은 해강이
    문제번호 : 24391번

    같은 그룹에 속했는지 확인해야하는 문제이다
    즉, 유니온 파인드를 쓰는 문제이다
    문제를 보자마자 이걸 써야한다는 느낌을 못받았다;

    한동안 새로운 진도보다, 일단 알고 있는 것을 쓸 수 있게 문제를 풀어야겠다
*/

namespace BaekJoon.etc
{
    internal class etc_0001
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[] groups = new int[info[0] + 1];
            for (int i = 0; i < info[0]; i++)
            {

                groups[i + 1] = i + 1;
            }

            Stack<int> s = new Stack<int>(info[0]);

            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                int f = Find(groups, temp[0], s);
                int b = Find(groups, temp[1], s);

                if (f != b)
                {

                    groups[f] = b;
                }
            }

            int[] target = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            sr.Close();

            int result = 0;
            int cur = target[0];
            for (int i = 1; i < target.Length; i++)
            {

                int f = Find(groups, cur, s);
                int b = Find(groups, target[i], s);

                if (f != b)
                {

                    result++;
                }

                cur = target[i];
            }

            Console.WriteLine(result);
        }

        static int Find(int[] _groups, int _chk, Stack<int> _calc)
        {

            while(_chk != _groups[_chk])
            {

                _calc.Push(_chk);
                _chk = _groups[_chk];
            }

            while(_calc.Count > 0)
            {

                _groups[_calc.Pop()] = _chk;
            }

            return _chk;
        }
    }
}
