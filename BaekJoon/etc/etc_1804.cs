using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 3
이름 : 배성훈
내용 : 스테판 쿼리
    문제번호 : 14654번

    구현, 시뮬레이션 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1804
    {

        static void Main1804(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int[] arr1 = new int[n];
            int[] arr2 = new int[n];

            ReadArr(arr1);
            ReadArr(arr2);

            int ret = 1, t1 = 0, t2 = 0, cur = 0, team = 0;
            
            for (int i = 0; i < n; i++)
            {

                if (IsWinQ(arr1[i], arr2[i], t1, t2))
                {

                    if (team == 1) 
                    { 
                        
                        cur++;
                        ret = Math.Max(cur, ret);
                    }
                    else
                    {

                        team = 1;
                        cur = 1;
                    }

                    t1++;
                    t2 = 0;
                }
                else
                {

                    if (team == 2)
                    {

                        cur++;
                        ret = Math.Max(cur, ret);
                    }
                    else
                    {

                        team = 2;
                        cur = 1;
                    }

                    t2++;
                    t1 = 0;
                }
            }

            Console.Write(ret);

            // 쿼리 팀이 이기는지 확인
            bool IsWinQ(int _v1, int _v2, int _t1, int _t2)
            {

                if (_v1 == _v2) return _t1 < _t2;
                int ret = _v1 - _v2;
                if (ret < 0) ret += 3;

                return ret == 1;
            }

            void ReadArr(int[] _arr)
            {

                for (int i = 0; i < n; i++)
                {

                    _arr[i] = ReadInt();
                }
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

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
