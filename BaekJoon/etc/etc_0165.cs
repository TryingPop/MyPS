using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 8
이름 : 배성훈
내용 : 차이를 최대로
    문제번호 : 10819번

    백트래킹 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0165
    {

        static void Main165(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());

            int n = ReadInt(sr);

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt(sr);
            }

            sr.Close();

            bool[] visit = new bool[n];
            int ret = 0;

            // 절대값이 100 이하인 숫자들만 입력된다!
            // 그래서 처음을 알리는 수를 -101로 설정
            DFS(arr, visit, -101, 0, 0, ref ret);

            Console.WriteLine(ret);
        }

        static void DFS(int[] _arr, bool[] _visit, int before, int _curVal, int _depth, ref int _ret)
        {

            // 탈출 조건
            if (_depth == _arr.Length)
            {

                if (_curVal > _ret) _ret = _curVal;
                return;
            }

            // 재귀
            for (int i = 0; i < _arr.Length; i++)
            {

                if (_visit[i]) continue;
                _visit[i] = true;
                int add = 0;

                
                // 처음을 알리는 수가 아니면 연산 시작
                if (before != -101)
                {

                    add = _arr[before] - _arr[i];
                    add = add < 0 ? -add : add;
                }

                DFS(_arr, _visit, i, _curVal + add, _depth + 1, ref _ret);
                _visit[i] = false;
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            bool plus = true;
            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }

                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }
}
