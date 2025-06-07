using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 9
이름 : 배성훈
내용 : N과 M (8)
    문제번호 : 15657번

    백트래킹 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0168
    {

        static void Main168(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[] arr = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            sr.Close();

            Array.Sort(arr);
            int[] temp = new int[info[1]];
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 1 << 13);
            DFS(arr, info, 0, 0, temp, sw);
            sw.Close();
        }

        static void DFS(int[] _arr, int[] _info, int _minIdx, int _depth, int[] _temp, StreamWriter _sw)
        {

            if (_depth == _info[1])
            {

                for (int i = 0; i < _temp.Length; i++)
                {

                    _sw.Write(_temp[i]);
                    _sw.Write(' ');
                }
                _sw.Write('\n');
                return;
            }

            for (int i = _minIdx; i < _arr.Length; i++)
            {

                _temp[_depth] = _arr[i];
                DFS(_arr, _info, i, _depth + 1, _temp, _sw);
            }
        }

    }
}
