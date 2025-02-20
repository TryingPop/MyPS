using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 10
이름 : 배성훈
내용 : 연산자 끼워넣기 2
    문제번호 : 15658번

    백트래킹 문제
    DFS 탐색으로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0175
    {

        static void Main175(string[] args)
        {

            Console.ReadLine();
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int[] op = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[] ret = new int[2];
            ret[0] = -1_000_000_000;
            ret[1] = 1_000_000_000;

            DFS(arr, op, 1, arr[0], ret);

            Console.WriteLine(ret[0]);
            Console.WriteLine(ret[1]);
        }

        static void DFS(int[] _arr, int[] _op, int _depth, int _curVal, int[] _ret)
        {

            if (_depth == _arr.Length)
            {

                if (_curVal > _ret[0]) _ret[0] = _curVal;
                if (_curVal < _ret[1]) _ret[1] = _curVal;

                return;
            }

            for (int i = 0; i < 4; i++)
            {

                if (_op[i] == 0) continue;
                _op[i]--;

                int calc = Calc(_curVal, _arr[_depth], i);
                DFS(_arr, _op, _depth + 1, calc, _ret);
                _op[i]++;
            }
        }

        static int Calc(int _num1, int _num2, int _op)
        {

            switch (_op)
            {

                case 0:
                    return _num1 + _num2;

                case 1:
                    return _num1 - _num2;

                case 2:
                    return _num1 * _num2;

                default:
                    return _num1 / _num2;
            }
        }
    }
}
