using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 19
이름 : 배성훈
내용 : 월드컵
    문제번호 : 6987번

    브루트포스, 백트래킹 문제다
    처음에는 경우의 수를 나눠서 접근했는데 경우의 수 분할을 제대로 안해서 2번 틀렸다
    승리 5번한 팀이 2개 이상 있으면 -> 성립 불가능하다
    꽤나 복잡해질거같아 컴퓨터에 연산을 맡기는 방법을 택하게 되었다
    DFS 함수 만드는 것도 꽤 고생했다

    아이디어는 다음과 같다
    6팀이 서로 다른 팀과 1번씩 경기하기에 
    각 팀별로 경기 수는 5이고, 전체 경기는 15번 이뤄진다
    경기 결과는 각각에 독립적으로 3개(승, 패, 무)가 존재하므로 총 3^15( < 250 * 250 * 250 < 16_000_000) 경우의 수가 있다

    먼저 팀 경기가 5회가 안되는 것은 입력과 동시에 바로 판별하고
    각 팀이 5회씩 경기한 경우만 DFS 탐색하게 했다
    기준은 전체 경기를 기준으로 n번째 경기이다 승, 패, 무를 넣어 
    현재 결과가 유효한지 확인했다

    유효 확인은 경기의 승패 결과를 팀에 기록하고, 입력 된 값을 초과하는 경우면 끊었다
    (덧셈 연산으로 두 양수 비교를 해야하나 전체 결과에서 빼가는 형식으로 해서 0 미만이면 유효하지 않다고 내렸다)

    이렇게 제출하니 60ms에 통과되었다

    DFS 로직을 설정하고 이를 구현하는 쪽에서 고민좀 했다
    먼저 노트로 구현할 상황을 정리하고, 상황에 필요한 변수들을 설정했다

    아래는 노트에 기록한 내용

    ///////////////////////////////////////////////////////////////
    총 15경기 중 i번째 결과를 순차적으로 변경시켜 가면서 유효한지 판별
    -> 유효한 경우 i + 1 번 경기 결과 변경시키로 간다
    -> 15번 경기까지 해서 유효한 경우가 있으면 1로 이외는 0

    유효는 경기 결과를 팀 승패판에 기록 -> 해당 팀의 승패판 입력값과 비교
    초과하면 유효 X 이외는 유효하다고 판별
    -> 두 개의 입력판이 필요? -> 차로 비교해서 0미만이면 유효 X로 하고 1개의 입력판으로 해결하자
    
    매 경기 결과 입력마다 경기판 갱신이 필요하니
    경기에 따른 매칭 팀 필요 & 승패무 결과도 수치화 해야한다

    문제 입력이 18개의 int니 6 * 3 다차원 배열로 설정하고
    6은 팀 넘버, 3은 승패무 를 나타내는 번호로 설정
    -> 값을 회수로
    /////////////////////////////////////////////////////////////////

    이러한 글을 코드로 표현한게 아래의 코드다
*/

namespace BaekJoon.etc
{
    internal class etc_0571
    {

        static void Main571(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new(Console.OpenStandardOutput());

            int[] team1 = { 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };
            int[] team2 = { 1, 2, 3, 4, 5, 2, 3, 4, 5, 3, 4, 5, 4, 5, 5 };

            int[] dir1 = { 0, 1, 2 };
            int[] dir2 = { 2, 1, 0 };

            int[,] team = new int[6, 3];
            Solve();
            
            sr.Close();
            sw.Close();

            void Solve()
            {

                
                for (int i = 0; i < 4; i++)
                {


                    bool ret = true;
                    for (int j = 0; j < 6; j++)
                    {

                        int chk = 0;
                        for (int k = 0; k < 3; k++)
                        {

                            int cur = ReadInt();
                            team[j, k] = cur;
                            chk += cur;
                        }

                        if (chk != 5) ret = false;
                    }

                    if(ret) ret = DFS();
                    sw.Write(ret ? 1 : 0);
                    sw.Write(' ');
                }
            }
            
            bool DFS(int _depth = 0)
            {

                for (int i = 0; i < 6; i++)
                {

                    for (int j = 0; j < 3; j++)
                    {

                        if (team[i, j] < 0) return false;
                    }
                }

                if (_depth == 15) return true;

                int t1 = team1[_depth];
                int t2 = team2[_depth];
                for (int i = 0; i < 3; i++)
                {

                    team[t1, dir1[i]]--;
                    team[t2, dir2[i]]--;
                    bool chk = DFS(_depth + 1);
                    if (chk) return true;

                    team[t1, dir1[i]]++;
                    team[t2, dir2[i]]++;
                }

                return false;
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
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static int[,] map;
        static List<(int, int)> match = new();
        static bool answer;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                for (int j = i + 1; j < 6; j++)
                    match.Add((i, j));
            }
            for(int i = 0; i < 4; i++)
            {
                answer = false;
                map = new int[6, 3];
                int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
                for(int j = 0; j < 6; j++)
                {
                    for(int k = 0; k < 3; k++)
                    {
                        map[j, k] = arr[j * 3 + k];
                    }
                }
                dfs(0);
                if (answer)
                    sb.Append("1 ");
                else
                    sb.Append("0 ");
            }
            
            output.Write(sb);

            input.Close();
            output.Close();
        }
        static void dfs(int depth)
        {
            if (answer) return;
            if(depth == 15)
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 3; j++)
                        if (map[i, j] != 0)
                            return;
                }
                answer = true;
                return;
            }

            (int a, int b) = match[depth];
            for(int i = 0; i < 3; i++)
            {
                if (map[a, i] == 0 || map[b, 2 - i] == 0) continue; 
                map[a, i]--;
                map[b, 2 - i]--;
                dfs(depth + 1);
                map[a, i]++;
                map[b, 2 - i]++;
            }
        }
    }
}
#elif other2
using System.ComponentModel.Design;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Xml.XPath;

var sr = new StreamReader(Console.OpenStandardInput());
var sw = new StreamWriter(Console.OpenStandardOutput());

var result = new List<int>();
var combinations = new List<(int X, int Y)>();
for (int i = 0; i < 6; i++)
{
    for (int j = i + 1; j < 6; j++)
    {
        combinations.Add((i, j));
    }
}

var winArrary = new int[6];
var drawArrary = new int[6];
var loseArrary = new int[6];

var ans = 0;
for (var T = 0; T < 4; T++)
{
    var inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    for (int j = 0; j < 6; j++)
    {
        winArrary[j] = inputs[j * 3];
        drawArrary[j] = inputs[j * 3 + 1];
        loseArrary[j] = inputs[j * 3 + 2];
    }

    ans = 0;
    DFS();
    result.Add(ans);
}

void DFS(int count = 0)
{
    if (count == 15)
    {
        if (winArrary.Sum() == 0 && drawArrary.Sum() == 0 && drawArrary.Sum() == 0)
        {
            ans = 1;
        }

        return;
    }

    var X = combinations[count].X;
    var Y = combinations[count].Y;

    // 이겼을 때
    if (winArrary[X] > 0 && loseArrary[Y] > 0)
    {
        winArrary[X] -= 1;
        loseArrary[Y] -= 1;
        DFS(count + 1);
        winArrary[X] += 1;
        loseArrary[Y] += 1;
    }

    // 졌을 때
    if (loseArrary[X] > 0 && winArrary[Y] > 0)
    {
        loseArrary[X] -= 1;
        winArrary[Y] -= 1;
        DFS(count + 1);
        loseArrary[X] += 1;
        winArrary[Y] += 1;
    }

    // 비겼을 때
    if (drawArrary[X] > 0 && drawArrary[Y] > 0)
    {
        drawArrary[X] -= 1;
        drawArrary[Y] -= 1;
        DFS(count + 1);
        drawArrary[X] += 1;
        drawArrary[Y] += 1;
    }
}

sw.WriteLine(string.Join(" ", result));

sw.Flush();
sw.Close();
sr.Close();
#endif
}
