using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 8
이름 : 배성훈
내용 : 카드 섞기
    문제번호 : 1091번

    구현 시뮬레이션 문제다
    처음에는 그룹을 나누고, 그룹별 최소 사이클 주기를 확인하려고 했다
    그런데, 해당 경우 2개짜리 크기인데 0회, 1회 둘 다 만족하는 경우,
    그리고 2개인데 0회만 만족하는 경우, 2개인데 1회만 만족하는 경우,
    2개인데 0회, 1회 둘 다 불만족하는 경우 처럼 그룹에서 최소 회전이 영향을 못끼친다
    이를 파악 못해서 최소 횟수로 합동식 연산을 진행하다가 한 번 틀렸다

    그래서 방법을 바꿔 1번씩 회전시켜 조건을 만족하는지 보고, 
    회전해서 처음 경우로 돌아오면 중지하는 코드로 제출했다
    최소 주기는 각 그룹 수들의 lcm이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0487
    {

        static void Main487(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();

            int[] want = new int[n];
            for (int i = 0; i < n; i++)
            {

                want[i] = ReadInt();
            }

            int[] move = new int[n];
            for (int i = 0; i < n; i++)
            {

                move[i] = ReadInt();
            }

            sr.Close();

            // 주기 판별
            bool[] visit = new bool[n];
            int[] gN = new int[n];
            int groupLen = 0;
            for (int i = 0; i < n; i++)
            {

                if (visit[i]) continue;
                int cur = i;

                for (int j = 0; j < n; j++)
                {

                    if (visit[cur]) break;
                    visit[cur] = true;
                    gN[groupLen]++;
                    cur = move[cur];
                }

                groupLen++;
            }

            int len = gN[0];
            for (int i = 1; i < groupLen; i++)
            {

                int gcd = GetGCD(len, gN[i]);
                len *= (gN[i] / gcd);
            }

            // 주기 동안 회전 실시
            int ret = -1;
            int[] curState = new int[n];
            int[] nextState = new int[n];

            for (int i = 0; i < n; i++)
            {

                curState[i] = i % 3;
            }

            for (int i = 0; i < len; i++)
            {

                if (ChkArr(want, curState, n))
                {

                    // 못찾은 경우 회전한다
                    for(int j = 0; j < n; j++)
                    {

                        nextState[j] = curState[move[j]];
                    }

                    int[] temp = nextState;

                    nextState = curState;
                    curState = temp;
                    continue;
                }

                ret = i;
                break;
            }

            Console.WriteLine(ret);

            int GetGCD(int _a, int _b)
            {

                if (_a < _b)
                {

                    int temp = _a;
                    _a = _b;
                    _b = temp;
                }

                while(_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            bool ChkArr(int[] _f, int[] _b, int _len)
            {

                for (int i = 0; i < _len; i++)
                {

                    if (_f[i] != _b[i]) return true;
                }

                return false;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
int N = int.Parse(Console.ReadLine());

var P = Console.ReadLine().Split().Select(int.Parse).ToArray();
var S = Console.ReadLine().Split().Select(int.Parse).ToArray();

var deck = new int[N];
for (int i = 0, c = 0; i < N; i++, c = (c + 1) % 3)
    deck[i] = c;

var init = new int[N];
Array.Copy(deck, init, N);

int count = 0;
bool scammable = false;
while (true)
{
    if (Enumerable.SequenceEqual(deck, P))
    {
        scammable = true;
        break;
    }

    var temp = new int[N];
    Array.Copy(deck, temp, N);

    for (int i = 0; i < N; i++)
        deck[i] = temp[S[i]];

    if (Enumerable.SequenceEqual(deck, init))
        break;

    count++;
}

Console.Write(scammable == false ? -1 : count);
#endif
}
