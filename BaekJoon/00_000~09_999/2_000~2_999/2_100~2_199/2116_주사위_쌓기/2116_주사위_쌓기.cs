using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 19
이름 : 배성훈
내용 : 주사위 쌓기
    문제번호 : 2116번

    구현, 브루트포스 문제다.
    아이디어는 단순하다.
    특정 주사위의 윗면이 정해지면 아랫면도 자동적으로 정해진다.
    그래서 윗면과 옆면이 아닌 면 중 최댓값을 누적한다.
    이후 다음 주사위에서 윗면은 이전 주사위의 밑면이다.
    그리고 앞의 방법을 계속한다.

    이를 이용해 로직을 보면
    현재 윗면 A으로 아랫면 B을 찾는다.
    A와 B를 제외한 나머지 중 최댓값을 값에 추가한다.
    이제 다음 윗면을 현재 아랫면으로 갱신해준다.
    이렇게 다음 주사위로 넘어가는 반복문을 작성하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1422
    {

        static void Main1422(string[] args)
        {

            int n;
            int[][] dice;

            Input();

            GetRet();

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                dice = new int[n][];

                for (int i = 0; i < n; i++)
                {

                    dice[i] = new int[6];
                    for (int j = 0; j < 6; j++)
                    {

                        dice[i][j] = ReadInt();
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }

            void GetRet()
            {

                int ret = 0;
                for (int i = 1; i <= 6; i++)
                {

                    ret = Math.Max(DFS(i), ret);
                }

                Console.Write(ret);

                int DFS(int _top)
                {

                    int bot = _top;
                    int ret = 0;
                    for (int i = 0; i < n; i++)
                    {

                        int topIdx = FindIdx(i, bot);
                        int botIdx = GetRevIdx(topIdx);

                        ret += GetMax(i, topIdx, botIdx);
                        bot = dice[i][botIdx];
                    }

                    return ret;

                    int GetMax(int _idx, int _popIdx1, int _popIdx2)
                    {

                        int ret = 0;
                        for (int i = 0; i < 6; i++)
                        {

                            if (i == _popIdx1 || i == _popIdx2) continue;
                            ret = Math.Max(ret, dice[_idx][i]);
                        }

                        return ret;
                    }

                    int GetRevIdx(int _idx)
                    {

                        switch (_idx)
                        {

                            case 0:
                                return 5;

                            case 1:
                                return 3;

                            case 2:
                                return 4;

                            case 3:
                                return 1;

                            case 4:
                                return 2;

                            case 5:
                                return 0;

                            default:
                                return -1;
                        }
                    }

                    int FindIdx(int _idx, int _val)
                    {

                        for (int i = 0; i < 6; i++)
                        {

                            if (dice[_idx][i] == _val) return i;
                        }

                        return -1;
                    }
                }
            }
        }
    }
}
