using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 11
이름 : 배성훈
내용 : 크로스 컨트리
    문제번호 : 9017번

    구현 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0508
    {

        static void Main508(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();

            int[] rank = new int[1_000];
            int[] team = new int[201];
            int[] sum = new int[201];
            int[] five = new int[201];

            while(test-- > 0)
            {

                int n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    int t = ReadInt();
                    rank[i] = t;
                    team[t]++;
                }

                int pop = 0;
                for (int i = 0; i < n; i++)
                {

                    int t = rank[i];
                    if (team[t] < 6)
                    {

                        pop++;
                        continue;
                    }

                    team[t]++;
                    if (team[t] <= 10) sum[t] += i - pop;
                    else if (team[t] == 11) five[t] = i - pop;
                }

                int min = 6_000;
                int ret = -1;
                int chk = 1_000;
                for (int i = 1; i <= 200; i++)
                {

                    if (team[i] < 12 || min < sum[i]) continue;
                    if (sum[i] == min && five[i] < chk)
                    {

                        ret = i;
                        chk = five[i];
                    }
                    else if (sum[i] < min)
                    {

                        ret = i;
                        min = sum[i];
                        chk = five[i];
                    }
                }

                sw.WriteLine(ret);

                for (int i = 1; i <= 200; i++)
                {

                    team[i] = 0;
                    five[i] = 0;
                    sum[i] = 0;
                }
            }
            sr.Close();
            sw.Close();

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
int T = int.Parse(Console.ReadLine());
int[] Team, RunnerCnt, TeamScore, Runner5th;
System.Text.StringBuilder sb = new System.Text.StringBuilder();
for(int _=0; _<T; _++)
{
    int N=int.Parse(Console.ReadLine());
    Team= new int[201];
    RunnerCnt = new int[201];
    Runner5th = new int[201];
    TeamScore = new int[201];
    int[] GoalIn = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
    for(int i=0; i<N; i++)
    {
        Team[GoalIn[i]]++;
    }
    int scr = 0;
    for(int i = 0; i < N; i++)
    {
        if (Team[GoalIn[i]] < 6) continue;
        scr++;
        RunnerCnt[GoalIn[i]]++;
        if (RunnerCnt[GoalIn[i]] < 5)
        {
            TeamScore[GoalIn[i]] += scr;
        }
        else if (RunnerCnt[GoalIn[i]] == 5)
        {
            Runner5th[GoalIn[i]] = scr;
        }
    }
    int winner = 0, minScr = int.MaxValue;
    for(int i=1;i<201; i++)
    {
        if (TeamScore[i] == 0) continue;
        if (TeamScore[i] < minScr)
        {
            winner = i;minScr = TeamScore[i];
        }else if (TeamScore[i] == minScr)
        {
            if (Runner5th[i] < Runner5th[winner])
            {
                winner = i;
            }
        }
    }
    sb.AppendLine(winner.ToString());
}
Console.Write(sb);
#elif other2
using System.Text;

namespace BOJ_9017
{
    class Program
    {
        static void Main()
        {
            using StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            using StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            StringBuilder sb = new StringBuilder();
            int t = int.Parse(sr.ReadLine());
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            List<(int,int)> list = new List<(int,int)>();

            while (t-- > 0)
            {
                dict.Clear();
                list.Clear();
                
                int n = int.Parse(sr.ReadLine());
                
                int[] inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

                for (int i = 0; i < n; i++)
                {
                    dict.TryAdd(inputs[i], new List<int>());
                    dict[inputs[i]].Add(i + 1);
                }
                
                int rank = 1;
                for (int i = 0; i < n; i++)
                {
                    if (dict[inputs[i]].Count < 6) continue;
                    
                    list.Add((inputs[i], rank));
                    rank++;
                }
                
                dict.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    dict.TryAdd(list[i].Item1, new List<int>());
                    dict[list[i].Item1].Add(list[i].Item2);
                }

                int sumScore = int.MaxValue;
                int score = int.MaxValue;
                int answer = 0;
                foreach (var item in dict)
                {
                    int sum = item.Value.Take(4).Sum();

                    if (sum == sumScore)
                    {
                        sumScore = sum;

                        if (item.Value[4] < score)
                        {
                            score = item.Value[4];
                            answer = item.Key;
                        }
                    }
                    else if (sum < sumScore)
                    {
                        sumScore = sum;
                        score = item.Value[4];
                        answer = item.Key;
                    }
                }

                sb.AppendLine(answer.ToString());
            }
            
            sw.Write(sb);
        }
    }
}

#endif
}
