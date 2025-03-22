using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 11
이름 : 배성훈
내용 : 크로스워드
    문제번호 : 1706번

    구현, 정렬 파싱 문제다.
    CompareTo 함수를 이용해 문자열을 비교하니 틀린다.
    이후 그냥 내장된 Comp를 이용하니 이상없이 통과한다;

    msdn 문서를 참고하니 CompareTo 함수는 사전식이 기본이라 한다.
    https://learn.microsoft.com/ko-kr/dotnet/api/system.string.compareto?view=net-9.0
    
    그리고 호출자 참고에 보면,
        string s1 = "ani\u00ADmal";
        object o1 = "animal";

    s1과 o1을 비교하면 둘이 같은것으로 나온다.
    아마 object로 비교하면서 생기는 문제같다.
*/

namespace BaekJoon.etc
{
    internal class etc_1328
    {

        static void Main1328(string[] args)
        {

            int row, col;
            string[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int[][] chkR = new int[row][];
                int[][] chkC = new int[row][];
                StringBuilder sb = new(row + col);

                for (int r = 0; r < row; r++)
                {

                    chkR[r] = new int[col];
                    chkC[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        if (arr[r][c] == '#') continue;
                        ref int chk = ref chkR[r][c];
                        chk = 1;
                        c++;

                        for (; c < col; c++)
                        {

                            if (arr[r][c] == '#') break;
                            chkR[r][c] = -1;
                            chk++;
                        }
                    }
                }

                for (int c = 0; c < col; c++)
                {

                    for (int r = 0; r < row; r++)
                    {

                        if (arr[r][c] == '#') continue;
                        ref int chk = ref chkC[r][c];
                        chk = 1;
                        r++;

                        for (; r < row; r++)
                        {

                            if (arr[r][c] == '#') break;
                            chkC[r][c] = -1;
                            chk++;
                        }
                    }
                }

                string[] sortedArr = new string[row * col * 2];
                int len = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (chkR[r][c] <= 1) continue;

                        string cur = ReadString(c, chkR[r][c], r, false);
                        sortedArr[len++] = cur;
                    }
                }

                for (int c = 0; c < col; c++)
                {

                    for (int r = 0; r < row; r++)
                    {

                        if (chkC[r][c] <= 1) continue;

                        string cur = ReadString(r, chkC[r][c], c, true);
                        sortedArr[len++] = cur;
                    }
                }

                Array.Sort(sortedArr, 0, len);

                Console.Write(sortedArr[0]);

                string ReadString(int _s, int _len, int _fix, bool _isC)
                {

                    sb.Clear();
                    if (_isC)
                    {

                        for (int i = 0; i < _len; i++)
                        {

                            sb.Append(arr[_s + i][_fix]);
                        }
                    }
                    else
                    {

                        for (int i = 0; i < _len; i++)
                        {

                            sb.Append(arr[_fix][_s + i]);
                        }
                    }

                    return sb.ToString();
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] size = sr.ReadLine().Split();
                row = int.Parse(size[0]);
                col = int.Parse(size[1]);

                arr = new string[row];
                for (int r = 0; r < row; r++)
                {

                    arr[r] = sr.ReadLine();
                }
            }
        }
    }
}
