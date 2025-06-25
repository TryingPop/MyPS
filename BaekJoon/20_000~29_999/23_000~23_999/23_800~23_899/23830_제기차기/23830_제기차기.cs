using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 7
이름 : 배성훈
내용 : 제기차기
    문제번호 : 23830번

    이분 탐색 문제다.
    변수 선언을 잘못해 여러 번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_1527
    {

        static void Main1527(string[] args)
        {

            int n, p, q, r;
            int[] arr;
            long s;
            long sum;

            Input();

            GetRet();

            void GetRet()
            {

                int l = 1;
                int r = 100_002;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;
                    if (Chk(mid)) r = mid - 1;
                    else l = mid + 1;
                }

                if (r == 100_002) Console.Write(-1);
                else Console.Write(r + 1);
            }

            bool Chk(int _val)
            {

                long add = Lo(_val);
                long sub = n - Up(_val + r);

                long chkSum = sum - sub * p + add * q;

                return s <= chkSum;
            }

            int Lo(int _val)
            {

                int l = 0;
                int r = n - 1;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;
                    if (arr[mid] < _val) l = mid + 1;
                    else r = mid - 1;
                }

                return l;
            }

            int Up(int _val)
            {

                int l = 0;
                int r = n - 1;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;
                    if (arr[mid] <= _val) l = mid + 1;
                    else r = mid - 1;
                }

                return r + 1;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];
                sum = 0;
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                    sum += arr[i];
                }

                p = ReadInt();
                q = ReadInt();
                r = ReadInt();
                s = ReadLong();

                Array.Sort(arr);

                long ReadLong()
                {

                    long ret = 0;

                    while (TryReadLong()) ;
                    return ret;

                    bool TryReadLong()
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
}
