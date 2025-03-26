using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 26
이름 : 배성훈
내용 : Magickology
    문제번호 : 2189번

    구현 문제다.
    조건대로 브루트포스로 조사하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1472
    {

        static void Main1472(string[] args)
        {

            string SQUARE = "Square ";
            string MID = ": ";
            string RET0 = "Not a Magick Square\n";
            string RET1 = "Semi-Magick Square\n";
            string RET2 = "Weakly-Magick Square\n";
            string RET3 = "Strongly-Magick Square\n";
            string RET4 = "Magically-Magick Square\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            int[][] board;
            int[] arr;
            int i = 0;
            Init();

            while (Input())
            {

                i++;
                int type = GetRet();

                sw.Write($"{SQUARE}{i}{MID}");

                switch (type)
                {

                    case 0:
                        sw.Write(RET0);
                        break;

                    case 1:
                        sw.Write(RET1);
                        break;

                    case 2:
                        sw.Write(RET2);
                        break;

                    case 3:
                        sw.Write(RET3);
                        break;

                    case 4:
                        sw.Write(RET4);
                        break;
                }
            }

            int GetRet()
            {

                long sum = 0;
                for (int i = 0; i < n; i++)
                {

                    sum += board[0][i];
                }

                long chk1, chk2;
                for (int i = 0; i < n; i++)
                {

                    chk1 = 0;
                    chk2 = 0;
                    for (int j = 0; j < n; j++)
                    {

                        chk1 += board[i][j];
                        chk2 += board[j][i];
                    }

                    if (chk1 == sum && chk2 == sum) continue;
                    // 가로 또는 세로 합이 다른 경우 발견
                    return 0;
                }

                // 가로, 세로 합이 모두 같다.
                chk1 = 0;
                chk2 = 0;

                for (int i = 0; i < n; i++)
                {

                    chk1 += board[i][i];
                    chk2 += board[n - 1 - i][i];
                }

                // 대각선 합이 가로 합과 다른지 확인
                if (chk1 != sum || chk2 != sum) return 1;

                // 가로, 세로, 대각선의 합이 모두 같다.
                int len = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        arr[len++] = board[i][j];
                    }
                }


                Array.Sort(arr, 0, len);

                bool flag = true;
                for (int i = 1; i < len ; i++)
                {

                    // 같은게 존재하면 반마법
                    if (arr[i - 1] == arr[i]) return 2;
                    // 모두 1차이 나는지 확인
                    else if (arr[i - 1] + 1 != arr[i]) flag = false;
                }

                // 모두 1차이
                if (flag) return 4;
                // 모두 다르지만 1차이는 아니다.
                else return 3;
            }

            void Init()
            {

                board = new int[8][];
                for (int r = 0; r < 8; r++)
                {

                    board[r] = new int[8];
                }

                arr = new int[64];
            }

            bool Input()
            {

                n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        board[i][j] = ReadInt();
                    }
                }

                return n != 0;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt());
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;
                    bool positive = c != '-';

                    ret = positive ? c - '0' : 0;

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;

                    return false;
                }
            }
        }
    }
}
