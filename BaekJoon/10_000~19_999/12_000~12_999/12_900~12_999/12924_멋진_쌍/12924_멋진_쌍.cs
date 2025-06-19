using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 19
이름 : 배성훈
내용 : 멋진 쌍
    문제번호 : 12924번

    브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1714
    {

        static void Main1714(string[] args)
        {

            int A, B;
            Input();

            GetRet();

            void GetRet()
            {

                HashSet<int> cnt = new(7);
                int[] tPow = new int[8];
                tPow[0] = 1;
                for (int i = 1; i < tPow.Length; i++)
                {

                    tPow[i] = tPow[i - 1] * 10;
                }

                int ret = 0;

                for (int i = 0; i < 7; i++)
                {

                    Chk(i);
                }

                Console.Write(ret);

                void Chk(int _idx)
                {

                    int s = Math.Max(A, tPow[_idx]);
                    int e = Math.Min(B, tPow[_idx + 1]);

                    for (int i = s; i < e; i++)
                    {

                        Find(i);
                    }

                    void Find(int _num)
                    {

                        cnt.Clear();
                        int other = _num;
                        for (int i = 0; i < _idx; i++)
                        {

                            int f = other % 10;
                            other = (other / 10) + f * tPow[_idx];
                            if (ChkValid(other)) cnt.Add(other);
                        }

                        ret += cnt.Count;

                        bool ChkValid(int _val)
                            => _num < _val && _val < B;
                    }
                }


            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                A = int.Parse(temp[0]);
                B = int.Parse(temp[1]) + 1;
            }
        }
    }
}
