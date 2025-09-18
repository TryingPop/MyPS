using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 18
이름 : 배성훈
내용 : 소수 부분 수열
    문제번호 : 6884번

    소수 판정 문제다.
    값의 범위가 1억으로 매우 크다.
    
    편의상 200만 이하는 빠르게 찾기 위해 에라토스 테네스로 모두 찾았다.
    난이도가 실버이기에 200만 이상은 적을거라 생각하고 naive하게 sqrt(n)에 찾게 했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1902
    {

        static void Main1902(string[] args)
        {

            string N = "This sequence is anti-primed.\n";
            string HEAD = "Shortest primed subsequence is length ";
            string MID = ": ";
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();
            int n;
            int[] arr = new int[10_001];

            bool[] notPrime;

            FillPrime();

            for (int i = 0; i < t; i++)
            {

                n = ReadInt();
                for (int j = 1; j <= n; j++)
                {

                    arr[j] = ReadInt() + arr[j - 1];
                }

                int ret1 = 0;
                int ret2 = 0;
                for (int len = 2; len <= n; len++)
                {

                    bool flag = false;
                    for (int tail = len; tail <= n; tail++)
                    {

                        int sum = arr[tail] - arr[tail - len];
                        if (sum < notPrime.Length)
                        {

                            if (notPrime[sum]) continue;
                            flag = true;

                            ret1 = len;
                            ret2 = tail - len + 1;
                        }
                        else
                        {

                            if (ChkNotPrime(sum)) continue;
                            flag = true;

                            ret1 = len;
                            ret2 = tail - len + 1;
                        }

                        if (flag) break;
                    }

                    if (flag) break;
                }

                Output(ret1, ret2);
            }

            bool ChkNotPrime(int val)
            {

                for (int i = 2; i * i <= val; i++)
                {

                    if (val % i == 0) return true;
                }

                return false;
            }

            void Output(int len, int start)
            {

                if (len == 0) 
                { 
                    
                    sw.Write(N);
                    return;
                }

                sw.Write(HEAD);
                sw.Write(len);
                sw.Write(MID);
                for (int i = 0; i < len; i++)
                {

                    sw.Write(arr[i + start] - arr[i + start - 1]);
                    sw.Write(' ');
                }

                sw.Write('\n');
            }

            void FillPrime()
            {

                notPrime = new bool[2_000_001];
                notPrime[0] = true;
                notPrime[1] = true;
                for (int i = 2; i < notPrime.Length; i++)
                {

                    if (notPrime[i]) continue;

                    for (int j = i + i; j < notPrime.Length; j += i) 
                    {

                        notPrime[j] = true;
                    }
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
