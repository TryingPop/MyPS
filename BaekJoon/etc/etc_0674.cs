using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 3
이름 : 배성훈
내용 : 캠프 준비
    문제번호 : 16938번

    브루트포스, 백트래킹, 비트마스킹 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0674
    {

        static void Main674(string[] args)
        {

            StreamReader sr;
            int n, l, r, x;
            int[] arr;
            int ret = 0;

            Solve();

            void Solve()
            {

                Input();
                DFS(0, 1_000_000_000, 0, 0);

                Console.WriteLine(ret);
            }

            void DFS(int _depth, int _min, int _max, int _sum)
            {

                if (l <= _sum && _sum <= r && _max - _min >= x) ret++;

                for (int i = _depth; i < n; i++)
                {

                    DFS(i + 1, Math.Min(_min, arr[i]), Math.Max(_max, arr[i]), _sum + arr[i]);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                l = ReadInt();
                r = ReadInt();
                x = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
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
def backtracking(idx, total, easy, hard):
    global cnt
    if idx == n:
        if l <= total <= r and hard-easy >= x:
            cnt += 1
        return
    
// # 현재 문제를 추가하지 않을때
    backtracking(idx+1, total, easy, hard)
    
    if not total:  // # 현재 문제가 가장 쉬울때
        backtracking(idx+1, A[idx], A[idx], A[idx])
    else:
        backtracking(idx+1, total+A[idx], easy, A[idx])
    
    

n, l, r, x = map(int, input().split())
A = sorted(map(int, input().split()))
cnt = 0
backtracking(0, 0, -1, -1)
print(cnt)
#endif
}
