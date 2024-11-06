using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 톱니바퀴
    문제번호 : 14891번

    구현, 시뮬레이션 문제다
    조건대로 구현하고 하나씩 시뮬레이션 돌렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0628
    {

        static void Main628(string[] args)
        {

            StreamReader sr;

            string[] gear;
            int[] top;

            Solve();

            void Solve()
            {

                Input();

                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    int s = ReadInt();
                    int dir = ReadInt() == 1 ? 1 : -1;

                    Operator(s, dir);
                }

                sr.Close();
                int ret = GetRet();

                Console.WriteLine(ret);
            }

            int GetRet()
            {

                int ret = 0;
                int[] score = { 1, 2, 4, 8 };
                for (int i = 0; i < 4; i++)
                {

                    ret += gear[i][top[i]] == '1' ? score[i] : 0;
                }

                return ret;
            }

            void Operator(int _s, int _dir)
            {

                _s--;
                int l = _s;
                while(l > 0)
                {

                    if (!IsRotate(l - 1, l)) break;
                    l--;
                }

                int r = _s;
                while(r < 3)
                {

                    if (!IsRotate(r, r + 1)) break;
                    r++;
                }

                int dir = (_s - l) % 2 == 0 ? _dir : -_dir;
                for (int i = l; i <= r; i++)
                {

                    Rotate(i, dir);
                    dir *= -1;
                }
            }

            bool IsRotate(int _l, int _r)
            {

                int right = (top[_l] + 2) % 8;
                int left = (top[_r] + 6) % 8;

                return gear[_l][right] != gear[_r][left];
            }

            void Rotate(int _s, int _dir)
            {

                top[_s] = top[_s] - _dir;
                if (top[_s] < 0) top[_s] += 8;
                if (top[_s] >= 8) top[_s] -= 8;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                gear = new string[4];
                for (int i = 0; i < 4; i++)
                {

                    gear[i] = sr.ReadLine();
                }

                top = new int[4];
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;

namespace BackjunAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int wheelCount = 4, cogCount = 8;
            int[][] wheels = new int[wheelCount][];
            int[] totalWheelRotation = new int[wheelCount];
            Initialize();
            int rotationCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < rotationCount; i++)
            {
                string[] input = Console.ReadLine().Split(' ');
                int wheelIndex = int.Parse(input[0]) - 1;
                int direction = int.Parse(input[1]);
                RotateWheel(wheelIndex, direction);
            }
            Console.WriteLine(GetScore());

            void Initialize()
            {
                for (int w = 0; w < wheelCount; w++)
                {
                    wheels[w] = new int[cogCount];
                    string input = Console.ReadLine();
                    for (int c = 0; c < cogCount; c++)
                    {
                        wheels[w][c] = GetBinary(input[c]);//N=0, S=1 wheels[w][0] is 12 direction state, clockwise from 0~cogCount
                    }
                }
            }

            void RotateWheel(int index, int direction, int propDir = 0)
            {
                if (index > 0 && (propDir == 0 || propDir == -1))
                {
                    int nexind = index - 1;
                    int nexdir = -direction;
                    if (wheels[index][cogCount / 4 * 3] + wheels[nexind][cogCount / 4] == 1)
                    {
                        RotateWheel(nexind, nexdir, -1);
                    }
                }
                if (index < wheelCount - 1 && (propDir == 0 || propDir == 1))
                {
                    int nexind = index + 1;
                    int nexdir = -direction;
                    if (wheels[index][cogCount / 4] + wheels[nexind][cogCount / 4 * 3] == 1)
                    {
                        RotateWheel(nexind, nexdir, 1);
                    }
                }
                RotateInADirection(index, direction);
                return;
            }

            void RotateInADirection(int index, int direction)
            {
                switch (direction)
                {
                    case 1:
                        int tmp = wheels[index][cogCount - 1];
                        for (int c = cogCount - 1; c > 0; c--)
                        {
                            wheels[index][c] = wheels[index][c - 1];
                        }
                        wheels[index][0] = tmp;
                        break;
                    case -1:
                        int tmp2 = wheels[index][0];
                        for (int c = 0; c < cogCount - 1; c++)
                        {
                            wheels[index][c] = wheels[index][c + 1];
                        }
                        wheels[index][cogCount - 1] = tmp2;
                        break;
                }
            }

            int GetScore()
            {
                int score = 0;
                int scoreMultiplier = 1;
                for (int w = 0; w < wheelCount; w++)
                {
                    scoreMultiplier *= (w == 0) ? 1 : 2;
                    score += scoreMultiplier * wheels[w][0];
                }
                return score;
            }

            int GetBinary(char c)
            {
                return (c == '0') ? 0 : 1;
            }
        }
    }
}
#elif other2
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

int[,] shaft = new int[4, 8];
string[] line;
for (int i = 0; i < 4; i++)
{
    line = Console.ReadLine().Split(' ');
    for (int j = 0; j < 8; j++)
    {
        shaft[i, j] = line[0][j] - '0';
    }
 }

int S = int.Parse(Console.ReadLine());


while(S-->0)
{
    line = Console.ReadLine().Split(' ');
    // INDEX는 축 번호에서 1 빼야한다!!
    Rotate(int.Parse(line[0]) - 1, int.Parse(line[1]));
    //PrintShaft();
}
    PrintScore();



void PrintShaft()
{
    for(int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 8; j++)
            Console.Write("{0} ", shaft[i, j] == 0 ? "N" : "S");
        Console.WriteLine();
    }
}

void PrintScore()
{
    int score = 0;
    if (shaft[0, 0] == 1)
        score += 1;
    if (shaft[1, 0] == 1)
        score += 2;
    if (shaft[2, 0] == 1)
        score += 4;
    if (shaft[3, 0] == 1)
        score += 8;
    Console.WriteLine(score);
}

void Rotate(int s, int dir, int from = -1)
{
    // 끝
    if (s < 0 || s > 3)
        return;
    // 시작점
    if (from == -1)
    {
        // CW
        if (dir == 1)
        {
            // 시작 축 1칸 회전 // 시계방향
            int t = shaft[s, 7];
            for (int i = 7; i > 0; i--)
                shaft[s, i] = shaft[s, i - 1];
            shaft[s, 0] = t;
            // 옆의 축 회전 // 반대방향으로
            Rotate(s - 1, -1 * dir, s);
            Rotate(s + 1, -1 * dir, s);
        }
        // CCW
        else if (dir == -1)
        {
            // 시작 축 1칸 회전 // 반시계
            int t = shaft[s, 0];
            for (int i = 0; i <= 6; i++)
                shaft[s, i] = shaft[s, i + 1];
            shaft[s, 7] = t;
            // 옆의 축 회전 // 반대방향으로
            Rotate(s - 1, -1 * dir, s);
            Rotate(s + 1, -1 * dir, s);
        }
    }
    // 왼쪽에 있는 축 // 오른쪽에서 전달받음
    else if (s < from)
    {
        // 현재 축이 시계 방향으로 회전한다
        if(dir== 1)
        {
            // 현재 축의 2번과 오른쪽 축의 5번 비교한다.
            if (shaft[s, 2] == shaft[s + 1, 5])
                return;
            // 시계방향으로 회전한다.
            int t = shaft[s, 7];
            for (int i = 7; i > 0; i--)
                shaft[s, i] = shaft[s, i - 1];
            shaft[s, 0] = t;
            // 왼쪽으로 전달한다.
            Rotate(s - 1, -1 * dir, s);
        }
        // 현재 축이 반시계 방향으로 회전한다.
        else if(dir == -1)
        {
            // 현재 축의 2번과 오른쪽 축의 7번 비교한다.
            if (shaft[s, 2] == shaft[s + 1, 7])
                return;
            // 반시계 방향으로 회전한다.
            int t = shaft[s, 0];
            for (int i = 0; i <= 6; i++)
                shaft[s, i] = shaft[s, i + 1];
            shaft[s, 7] = t;
            // 왼쪽으로 전달한다.
            Rotate(s - 1, -1 * dir, s);
        }
    }
    // 오른쪽에 있는 축 // 왼쪽에서 전달받음
    else if (s > from)
    {
        // 현재 축이 시계방향으로 회전한다
        if(dir == 1)
        {
            //현재 축의 6번과 왼쪽 축의 1번 비교한다.
            if (shaft[s, 6] == shaft[s - 1, 1])
                return;
            // 시계방향으로 회전한다.
            int t = shaft[s, 7];
            for (int i = 7; i > 0; i--)
                shaft[s, i] = shaft[s, i - 1];
            shaft[s, 0] = t;
            // 오른쪽으로 전달한다.
            Rotate(s + 1, -1 * dir, s);
        }
        //현재 축이 반시계 방향으로 회전한다.
        if(dir == -1)
        {
            // 현재 축의 6번과 왼쪽 축의 3번 비교한다.
            if (shaft[s, 6] == shaft[s - 1, 3])
                return;
            // 반시계 방향으로 회전한다.
            int t = shaft[s, 0];
            for (int i = 0; i <= 6; i++)
                shaft[s, i] = shaft[s, i + 1];
            shaft[s, 7] = t;
            // 오른쪽으로 전달한다.
            Rotate(s + 1, -1 * dir, s);
        }
    }
}

#elif other3
using System;
using System.IO;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
const int CogCount = 4;
const int CogToothCount = 8;
var cog = new int[CogCount];
for (int i = 0; i < CogCount; i++)
{
    var n = 0;
    for (int j = 0; j < 8; j++)
        n = (n << 1) + sr.Read() - '0';
    cog[i] = n;
    if (sr.Read() == '\r')
        sr.Read();
}
var k = ScanInt();
for (int i = 0; i < k; i++)
{
    var n = sr.Read() - '0' - 1;
    sr.Read();
    var clockWise = sr.Read() != '-';
    if (!clockWise)
        sr.Read();
    int min = n;
    int max = n;

    while (min > 0 && (
        ((cog[min - 1] & (1 << 5)) >> 5) !=
        ((cog[min] & (1 << 1)) >> 1)
        )) min--;
    while (max < CogCount - 1 && (
        ((cog[max] & (1 << 5)) >> 5) !=
        ((cog[max + 1] & (1 << 1)) >> 1)
        )) max++;
    for (int j = min; j <= max; j++)
        Rotate(j, (n - j) % 2 == 0 ? clockWise : !clockWise);

    if (sr.Read() == '\r')
        sr.Read();
}

var score = 0;
for (int i = 0; i < CogCount; i++)
{
    if ((cog[i] & (1 << (CogToothCount - 1))) != 0)
        score += 1 << i;
}
Console.Write(score);

void Rotate(int index, bool clockWise)
{
    var curCog = cog[index];
    if (clockWise)
        curCog = (curCog >> 1) + ((curCog & 1) << 7);
    else
        curCog = ((curCog << 1) + ((curCog & (1 << 7)) >> 7)) & ((1 << CogToothCount) - 1);
    cog[index] = curCog;
}

int ScanInt()
{
    int c, ret = 0;
    while ((c = sr.Read()) != ' ' && c != '\n' && c != -1)
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        ret = 10 * ret + c - '0';
    }
    return ret;
}
#endif
}
