using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 30
이름 : 배성훈
내용 : 중력 큐
    문제번호 : 28078번

    구현, 덱 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1742
    {

        static void Main1742(string[] args)
        {

            int PUSH = 71626;
            int POP = 7094;
            int ROT = 7303633;
            int COUNT = 580588;
            int B = 50;
            int L = 60;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int[] myDeque = new int[n];
            int head = 0, tail = n - 1, cnt = 0;
            int b = 0, w = 0;
            // 0 : 가로 + 초기 상태, 1 : 세로 + 0을 왼쪽 90도 회전
            // 2 : 가로 + 초기 상태에서 180도 회전, 3 : 세로 + 0을 오른쪽 90도 회전
            int dir = 0;

            for (int i = 0; i < n; i++)
            {

                int op = ReadInt();

                if (op == PUSH)
                {

                    int val = ReadInt();
                    PushBack(val);
                }
                else if (op == POP)
                    PopFront();
                else if (op == ROT)
                {

                    int val = ReadInt();
                    if (val == L) dir++;
                    else dir--;

                    if (dir < 0) dir = 3;
                    else if (dir == 4) dir = 0;
                }
                else if (op == COUNT)
                {

                    int val = ReadInt();
                    if (val == B) sw.Write($"{b}\n");
                    else sw.Write($"{w}\n");
                }

                if (dir == 1) AllPopBack();
                else if (dir == 3) AllPopFront();
            }

            int Last()
                => myDeque[tail];

            int First() 
                => myDeque[head];

            void AllPopBack()
            {

                while (cnt > 0 && Last() == B)
                {

                    PopBack();
                }
            }

            void AllPopFront()
            {

                while (cnt > 0 && First() == B)
                {

                    PopFront();
                }
            }

            void PushBack(int _val)
            {

                tail++;
                if (tail == n) tail = 0;
                myDeque[tail] = _val;
                cnt++;
                if (_val == B) b++;
                else w++;
            }

            int PopBack()
            {

                if (cnt == 0) return 0;
                cnt--;

                int ret = myDeque[tail--];
                if (tail < 0) tail = n - 1;
                if (ret == B) b--;
                else w--;

                return ret;
            }

            int PopFront()
            {

                if (cnt == 0) return 0;
                cnt--;

                int ret = myDeque[head++];
                if (head == n) head = 0;

                if (ret == B) b--;
                else w--;

                return ret;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
