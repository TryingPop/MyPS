using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 3
이름 : 배성훈
내용 : 교차하지 않는 원의 현들의 최대집합
    문제번호 : 2673번

    dp문제다
    dp[s][e] = val를 s에서 시작해 e까지의 범위 중
    가장 많은 겹치지 않은 서로 다른 현의 갯수를 val로 저장하면 된다.
    그래서 각 간선을 조사하며,
    s, e인 범위를 나누어 값을 찾아가면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1243
    {

        static void Main1243(string[] args)
        {

            int n;
            int[][] dp;
            (int s, int e)[] edge;

            Solve();
            void Solve()
            {

                Input();

                SetDp();

                GetRet();
            }

            void GetRet()
            {

                Console.Write(DFS(1, 100));
                int DFS(int _s, int _e)
                {

                    if (100 < _s) return 0;
                    ref int ret = ref dp[_s][_e];
                    if (ret != -1) return ret;
                    ret = 0;

                    for (int i = 0; i < n; i++)
                    {

                        if (edge[i].s < _s || _e < edge[i].e) continue;

                        // 구간을 나눠서 찾는다.
                        ret = Math.Max(ret, DFS(_s, edge[i].s - 1) + DFS(edge[i].s + 1, edge[i].e - 1) + DFS(edge[i].e + 1, _e) + 1);
                    }

                    return ret;
                }
            }

            void SetDp()
            {

                dp = new int[101][];
                for (int i = 0; i <= 100; i++)
                {

                    dp[i] = new int[101];
                    Array.Fill(dp[i], -1);
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                edge = new (int s, int e)[n];
                for (int i = 0; i < n; i++)
                {

                    int s = ReadInt();
                    int e = ReadInt();

                    if (e < s)
                    {

                        int temp = s;
                        s = e;
                        e = temp;
                    }
                    edge[i] = (s, e);
                }

                sr.Close();
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

                        while((c = sr.Read()) != -1 && c != ' ' && c!= '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
using static IO;
public class IO{
public static IO Cin=new();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static implicit operator string(IO _)=>reader.ReadLine();
public static implicit operator char[](IO _)=>reader.ReadLine().ToArray();
public static implicit operator int(IO _)=>int.Parse(reader.ReadLine());
public static implicit operator double(IO _)=>double.Parse(reader.ReadLine());
public static implicit operator string[](IO _)=>reader.ReadLine().Split();
public static implicit operator int[](IO _)=>Array.ConvertAll(reader.ReadLine().Split(),int.Parse);
public void Deconstruct(out int a,out int b){int[] r=Cin;(a,b)=(r[0],r[1]);}
public void Deconstruct(out int a,out int b,out int c){int[] r=Cin;(a,b,c)=(r[0],r[1],r[2]);}
public static void Loop(int end,Action<int> action,int start = 0,in int add = 1){for(;start<end;start+=add)action(start);}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    public static void Coding() {
        HashSet<(int a,int b)> set = new();
        Loop(Cin, _ => {
            (int a,int b) = Cin;
            a--; b--;
            set.Add(a < b ? (a,b) : (b,a));
        });

        int?[,] memo = new int?[100,100];
        int dp(int s,int e) {
            if (s >= e) return 0;
            if (memo[s,e] is int ret) return ret;
            int exist = ret = set.Contains((s,e)) ? 1 : 0;
            Loop(e, m => {
                ret = Math.Max(ret, dp(s,m) + dp(m,e) + exist);
            },s+1);
            memo[s,e] = ret;
            return ret;
        }

        Cout = dp(0,99);
    }
}
#endif
}
