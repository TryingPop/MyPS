using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 18
이름 : 배성훈
내용 : 오버워치 월드컵
    문제번호 : 13923번

    구현, 문자열, 브루트포스 문제다.
    상황을 나눠서 해결했다.

    다른게 1개 유일하게 있다고 한다.
    잘못된 것을 기준으로 보면 두 가지 경우가 나온다.
        1. 안쓰인 알파벳이 나오는 경우 = 1개짜리 알파벳이 존재
        2. 이미 쓰인 알파벳이 나오는 경우 = n + 1개인 알파벳이 존재
    그리고 두 경우 모두 부족한 알파벳이 유일하게 존재하고 n - 1개가 나온다.
    이를 n - 1개인 알파벳으로 바꾸면 된다.

    먼저 1개짜리는 해당 알파벳 좌표를 바로 출력하면 된다.
    반면 n + 1개인 경우 해당 알파벳을 모두 조사한다.
    그리고 해당 알파벳을 보면 가로와 세로에 알파벳이 2개씩 있음을 확인할 수 있다.
    여기서는 N이 26으로 작아 일일히 조사하는 3 x N^2의 방법으로 찾았다.
    해당 알파벳의 좌표를 모두 저장한다면 N^2에 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1552
    {

        static void Main1552(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            string[] board = new string[26];
            int[] cnt = new int[26];

            string input;
            
            while (!string.IsNullOrEmpty((input = sr.ReadLine())))
            {

                n = int.Parse(input);
                Array.Fill(cnt, 0);

                for (int i = 0; i < n; i++)
                {

                    board[i] = sr.ReadLine();
                    for (int j = 0; j < n; j++)
                    {

                        int idx = board[i][j] - 'A';
                        cnt[idx]++;
                    }
                }

                int more = -1;
                int less = -1;
                int one = -1;
                for (int i = 0; i < 26; i++)
                {

                    if (cnt[i] == 1) one = i;
                    else if (cnt[i] == n - 1) less = i;
                    else if (cnt[i] == n + 1) more = i;
                }

                if (one != -1) FindOne(one, less);
                else FindMore(more, less);
            }

            // n + 1개의 경우
            void FindMore(int _more, int _less)
            {

                _more += 'A';
                _less += 'A';
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        if (board[i][j] != _more) continue;

                        int cnt = 0;
                        for (int k = 0; k < n; k++)
                        {

                            if (board[i][k] == _more) cnt++;
                            if (board[k][j] == _more) cnt++;
                        }

                        if (cnt < 4) continue;
                        sw.Write($"{i + 1} {j + 1} {(char)_less}\n");
                    }
                }
            }

            // 1개의 경우
            void FindOne(int _one, int _less)
            {

                _one += 'A';
                _less += 'A';
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        if (board[i][j] != _one) continue;
                        sw.Write($"{i + 1} {j + 1} {(char)_less}\n");
                    }
                }
            }
        }
    }
}
