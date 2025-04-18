using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 9
이름 : 배성훈
내용 : 줄 세우기
    문제번호 : 2605번

    구현, 자료구조 문제다.
    자리 찾아가는 것과 출력 방향이 반대이므로
    이중 연결리스트를 구현해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1322
    {

        static void Main1322(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int[] nums = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            (int prev, int next)[] nodes = new (int prev, int next)[n + 2];
            int head = 0, tail = n + 1;

            nodes[head].next = tail;
            nodes[tail].prev = head;

            for (int i = 0; i < n; i++)
            {

                ReadNum(nums[i], i + 1);
            }

            int idx = head;
            for (int i = 0; i < n; i++)
            {

                idx = nodes[idx].next;
                Console.Write($"{idx} ");
            }

            void ReadNum(int _num, int _insert)
            {

                int prev = nodes[tail].prev;

                while (_num-- > 0) { prev = nodes[prev].prev; }

                Insert(prev, _insert);
            }

            void Insert(int _prev, int _insert)
            {

                int next = nodes[_prev].next;
                nodes[_prev].next = _insert;
                nodes[_insert].prev = _prev;
                nodes[_insert].next = next;
                nodes[next].prev = _insert;
            }
        }
    }
}
