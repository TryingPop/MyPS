using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 20
이름 : 배성훈
내용 : A Hero Named Magnus
    문제번호 : 31134번

    수학, 사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1125
    {

        static void Main1125(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();

            for (int i = 0; i < t; i++)
            {

                long n = ReadInt();
                n = ((n - 1) << 1) + 1;
                sw.Write($"{n}\n");
            }

            sr.Close();
            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include<bits/stdc++.h>
using namespace std;

long long n, a;
int main()
{
	ios::sync_with_stdio(false);
	cin.tie(0);
	
	cin>>n;
	while(n--)
	{
		cin>>a;
		cout<<2*a-1<<"\n";
	}
}
#endif
}
