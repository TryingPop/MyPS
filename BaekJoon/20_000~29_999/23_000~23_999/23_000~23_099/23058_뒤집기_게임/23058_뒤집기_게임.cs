using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 뒤집기 게임
    문제번호 : 23058번

    브루트포스, 비트마스킹 문제다
    어떻게 풀지 아이디어가 안떠올라, 다른 사람 아이디어를 참고했다
    아이디어는 다음과 같다

    2번 뒤집으면 원위치이기에
    특정 행과 열만 뒤집고 나머지는 한개씩 뒤집는 경우를 모두 찾는다
    이 중에 최소값인 경우가 정답이 된다

    그래서 O, X여부이고 크기가 64 이하이기에 long으로 비트마스킹 연산을 했다
    1을 long으로 캐스팅 안하면, int로 인식하기에 비트마스킹에서 제대로된 결과를 못 얻는다!
    이외는 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0365
    {

        static void Main365(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();

            long board = 0;

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    int cur = ReadInt();
                    board |= (long)cur << (n * i + j);
                }
            }

            sr.Close();

            int ret = n * n;
            for (int i = 0; i < (1 << (2 * n)); i++)
            {

                long calc = board;
                int add = 0;
                for (int j = 0; j < n; j++)
                {

                    if (((1 << j) & i) == 0) continue;
                    calc = ReverseRow(calc, j);
                    add++;
                }

                for (int j = 0; j < n; j++)
                {

                    if (((1 << (j + n)) & i) == 0) continue;
                    calc = ReverseCol(calc, j);
                    add++;
                }


                int chk = FindOne(calc);
                chk = n * n - chk < chk ? n * n - chk : chk;

                chk += add;
                ret = chk < ret ? chk : ret;
            }

            Console.WriteLine(ret);
            
            int FindOne(long _n)
            {

                int ret = 0;
                for (int i = 0; i < 64; i++)
                {

                    if ((((long)1 << i) & _n) == 0) continue;
                    ret++;
                }

                return ret;
            }

            long ReverseRow(long _board, int _row)
            {

                for (int i = 0; i < n; i++)
                {
                    _board ^= (long)1 << (_row * n + i);
                }

                return _board;
            }



            long ReverseCol(long _board, int _col)
            {

                for (int i = 0; i < n; i++)
                {

                    _board ^= (long)1 << (n * i + _col);
                }

                return _board;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
