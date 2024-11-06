using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/*
날짜 : 2024. 2. 25
이름 : 배성훈
내용 : 오델로
    문제번호 : 23081번

    앞의 오델로와 유사한 문제
    간단한 구현 문제다
    여기서는 현재 가장 많이 뒤집을 수 있는 곳을 찾을 뿐이다
    그래서 모든 점을 조사해서 몇 개 뒤집을 수 있는지 구현하는 문제다(브루트 포스)
*/

namespace BaekJoon.etc
{
    internal class etc_0089
    {

        static void Main89(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            int[,] board = new int[len, len];
            Queue<(int x, int y)> q = new(len * len);
            for (int i = 0; i < len; i++)
            {

                int c;
                for (int j = 0; j < len; j++)
                {

                    c = sr.Read();
                    if (c == '.') 
                    { 
                        
                        // 돌 놓을 곳 저장
                        // 이중 포문 탐색 방법으로 자동으로 y = row가 낮은 거부터,
                        // 이후 x = col가 낮은게 먼저 담긴다
                        board[i, j] = 0;
                        q.Enqueue((i, j));
                    }
                    else if (c == 'W') board[i, j] = 1;
                    else board[i, j] = 2;
                }

                c = sr.Read();
                if (c == '\r') sr.Read();
            }

            sr.Close();

            // 읽기

            int[] dirX = { -1, 1, 0, 0, -1, -1, 1, 1 };
            int[] dirY = { 0, 0, -1, 1, -1, 1, -1, 1 };
            int ret = -1;
            int retX = -1;
            int retY = -1;
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                int find = 0;
                for (int i = 0; i < 8; i++)
                {

                    int curX = node.x;
                    int curY = node.y;

                    int white = 0;
                    while (true)
                    {

                        curX += dirX[i];
                        curY += dirY[i];

                        
                        if (ChkInvalidPos(curX, curY, len)) break;

                        if (board[curX, curY] == 1) white++;
                        else if (white != 0 && board[curX, curY] == 2) 
                        { 
                            
                            find += white;
                            break;
                        }
                        else break;
                    }
                }

                // 갱신이 필요한 경우 바로 갱신
                // 그러면 y가 작은게 최우선으로 y가 같다면 x가 작은게 최우선으로 담긴다
                // 만약 y가 크고, x가 큰걸 담고 싶다면 >=으로 부등식을 바꾸면 된다
                // y가 크고 x가 작은거라면 입력 순서를 바꿔줘야한다
                if (find > ret) 
                { 
                    
                    ret = find;
                    retX = node.x;
                    retY = node.y;
                }
            }

            if (ret == 0) Console.WriteLine("PASS");
            else Console.Write($"{retY} {retX}\n{ret}");
        }

        static bool ChkInvalidPos(int _x, int _y, int _N)
        {

            if (_x < 0 || _x >= _N || _y < 0 || _y >= _N) return true;

            return false;
        }

    }
}
