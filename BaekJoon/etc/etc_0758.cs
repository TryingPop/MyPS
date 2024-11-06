using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 15
이름 : 배성훈
내용 : 소수 경로
    문제번호 : 1963번

    수학, 정수론, BFS 문제다
    먼저 소수간의 간선들을 모두 잇고,
    BFS(== 다익스트라)로 최단 거리를 찾아갔다
    만약 끝점에 도달하면 바로 탈출하면 된다

    모든 경로를 돌았음에도 찾지 못했다면 
    -1을 반환해 IMPO를 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0758
    {

        static void Main758(string[] args)
        {

            string IMPO = "Impossible\n";

            StreamReader sr;
            StreamWriter sw;

            List<int>[] line;
            bool[] notPrime;
            Queue<int> q;
            bool[] visit;
            int[] dis;
            int[] arr;
            int[] digit;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    int s = ReadInt();
                    int e = ReadInt();

                    int ret = DisBFS(s, e);

                    if (ret != -1) sw.Write($"{ret}\n");
                    else sw.Write(IMPO);
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                SetPrime();

                line = new List<int>[10_000];
                for (int i = 1000; i < 10_000; i++)
                {

                    line[i] = new();
                }

                visit = new bool[10_000];
                dis = new int[10_000];
                q = new(9_000);

                digit = new int[4] { 1_000, 100, 10, 1 };
                arr = new int[4];
                ConnLine();

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            }

            void SetPrime()
            {

                notPrime = new bool[10_000];

                for (int i = 2; i < 10_000; i++)
                {

                    if (notPrime[i]) continue;

                    for (int j = i + i; j < 10_000; j += i)
                    {

                        notPrime[j] = true;
                    }
                }
            }

            void ConnLine()
            {

                for (int i = 1_001; i < 10_000; i++)
                {

                    if (notPrime[i] || visit[i]) continue;

                    ConnBFS(i);
                }
            }

            void ConnBFS(int _n)
            {

                visit[_n] = true;
                q.Enqueue(_n);

                while(q.Count > 0)
                {

                    int node = q.Dequeue();

                    IntToArr(node);

                    for (int d = 0; d < 4; d++)
                    {

                        for (int a = 1; a < 10; a++)
                        {

                            if (ChkNext(d, a)) break;

                            int next = node + a * digit[d];
                            if (notPrime[next]) continue;

                            line[node].Add(next);
                            line[next].Add(node);

                            if (visit[next])
                            {

                                visit[next] = true;
                                q.Enqueue(next);
                            }
                        }
                    }
                }
            }

            int DisBFS(int _s, int _e)
            {

                if (_s == _e) return 0;

                Array.Fill(dis, 10_000);
                Array.Fill(visit, false);

                q.Clear();
                dis[_s] = 0;
                q.Enqueue(_s);
                while(q.Count > 0)
                {

                    int node = q.Dequeue();
                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i];
                        if (visit[next]) continue;
                        visit[next] = true;

                        dis[next] = dis[node] + 1;
                        if (next == _e) return dis[next];
                        q.Enqueue(next);
                    }
                }

                return -1;
            }

            void IntToArr(int _n)
            {

                for (int i = 0; i < 4; i++)
                {

                    arr[i] = _n / digit[i];
                    _n %= digit[i];
                }
            }

            bool ChkNext(int _digit, int _add)
            {

                return arr[_digit] + _add > 9;
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
using System;
using System.IO;
using System.Collections.Generic;

namespace 너비우선탐색
{
    class 소수경로
    {
        static int a, b;
        
        static bool[] prime = new bool[10000];
        static int[] visit;
        
        static void BFS()
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(a);
            visit[a] = 1;
            
            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                
                for (int i = 0; i < 4; i++)
                {
                    int ten = (int)Math.Pow(10, i);
                    
                    for (int j = 0; j < 10; j++)
                    {
                        int front = node / (ten * 10);
                        int back = node % ten;
                        
                        int num = (front * (ten * 10)) + (j * ten) + back;
                        
                        if (num >= 1000 && !prime[num] && visit[num] == 0)
                        {
                            visit[num] += visit[node] + 1;
                            queue.Enqueue(num);
                        }
                    }
                }
            }
        }
        
        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            
            prime[0] = true;
            prime[1] = true;
            
            for (int i = 2; i < 10000; i++)
            {
                if (!prime[i])
                {
                    for (int j = 2; i * j < 10000; j++)
                    {
                        prime[i * j] = true;
                    }
                }
            }
            
            int t = int.Parse(sr.ReadLine());
            while (t-- > 0)
            {
                string[] ab = sr.ReadLine().Split();
                a = int.Parse(ab[0]);
                b = int.Parse(ab[1]);
                
                visit = new int[10000];
                
                BFS();
                if (visit[b] == 0)
                {
                    sw.WriteLine("Impossible");
                }
                else
                {
                    sw.WriteLine(visit[b] - 1);
                }
            }
            
            sw.Flush();
            sw.Close();
            sr.Close();
        }
    }
}
#elif other2
using Microsoft.VisualBasic;

var sr = new StreamReader(Console.OpenStandardInput());
var sw = new StreamWriter(Console.OpenStandardOutput());

var T = int.Parse(sr.ReadLine());
var sosu = new bool[10000];
sosu[0] = true;
sosu[1] = true;
eratos();

void eratos()
{
    for (var i = 2; i <= 100; i++)
    {
        if (sosu[i]) continue;
        for (var j = i * 2; j < 10000; j += i)
        {
            sosu[j] = true;
        }
    }
}

for (var t = 0; t < T; t++)
{
    var inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    var left = inputs[0];
    var right = inputs[1];
    var result = -1;

    BFS();
    if (result == -1) sw.WriteLine("Impossible");
    else sw.WriteLine(result);

    void BFS()
    {
        var visited = new bool[10000];
        Queue<(List<int> num, int cnt)> q = new Queue<(List<int> num, int cnt)>();
        var leftString = left.ToString();
        var first = new List<int>();
        for (var digit = 0; digit < 4; digit++)
        {
            first.Add(leftString[digit] - '0');
        }

        q.Enqueue((first, 0));

        while (q.Count > 0)
        {
            var current = q.Dequeue();
            var num = current.num;
            var count = current.cnt;

            var currentNum = 0;
            var m = 1000;
            for (var d = 0; d < 4; d++)
            {
                currentNum += num[d] * m;
                m /= 10;
            }

            if (currentNum == right)
            {
                result = count;
                break;
            }

            for (var digit = 0; digit < 4; digit++)
            {
                var temp = num[digit];
                for (var nextNum = 0; nextNum <= 9; nextNum++)
                {
                    if (digit == 0 && nextNum == 0) continue;
                    num[digit] = nextNum;
                    var numInt = 0;
                    var multiply = 1000;
                    for (var d = 0; d < 4; d++)
                    {
                        numInt += num[d] * multiply;
                        multiply /= 10;
                    }

                    if (sosu[numInt]) continue;
                    if (visited[numInt]) continue;
                    visited[numInt] = true;
                    q.Enqueue((num.ToList(), count + 1));
                }

                num[digit] = temp;
            }
        }
    }
}

sr.Close();
sw.Close();
#endif
}
