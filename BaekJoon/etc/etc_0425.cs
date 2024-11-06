using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 1
이름 : 배성훈
내용 : 소트
    문제번호 : 1083번

    그리디 알고리즘, 정렬 문제다
    그리디하게 접근해서 풀었다

    아이디어는 다음과 같다
    맨 앞자리부터 변환 횟수 안에 가장 큰 값을 갖게한다
    그리고, 변환횟수가 남아있다면 두번째자리, ...., n번째자리까지 진행한다
    중간에 변환횟수가 0이면 탈출한다

    버블 정렬하는 부분을 잘못 세팅해서 한 번 틀렸다
    왼쪽에서 오른쪽으로 1칸씩 미는 부분을 오른쪽부터 진행해줘야하는데, 
    이를 왼쪽부터 오른쪽으로 1칸씩 미는 진행을 해서 한 번 틀렸다;

    이외는 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0425
    {

        static void Main425(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());
            StreamWriter sw = new(Console.OpenStandardOutput());

            int n = ReadInt();

            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            int c = ReadInt();
            sr.Close();

            for (int i = 0; i < n; i++)
            {

                // 바꿀 수 있는 큰 값 찾기
                int chk = i;
                for (int j = i + 1; j < n; j++)
                {

                    if (arr[chk] < arr[j] && j - i <= c) chk = j;
                }

                // 바꾸기
                int temp = arr[chk];
                for (int j = chk; j >= i + 1; j--)
                {

                    arr[j] = arr[j - 1];
                }
                arr[i] = temp;
                c -= chk - i;

                // 남은 변환횟수 확인
                if (c == 0) break;
            }

            for (int i = 0; i < n; i++)
            {

                sw.Write(arr[i]);
                sw.Write(' ');
            }

            sw.Close();

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
using System.Text;

public class Program
{

    public static void Swap(ref int x, ref int y)
    {
        int temp = x;
        x = y;
        y = temp;
    }
    public static void Solve()
    {
        for (int i = 0; i < N; ++i)
        {
            int Max = a[i];
            int index = i;
            // [10] [20] [30] [40] [50] [60] [70]
            for (int j = i + 1; j < N; ++j)
            {
                if (S - (j - i) >= 0)
                {
                    if (Max < a[j])
                    {
                        Max = a[j];
                        index = j;
                    }
                }
            }
            for (int j = index; j > i; --j)
            {
                Swap(ref a[j], ref a[j - 1]);
            }
            a[i] = Max;
            S -= (index - i);
            if (S <= 0) break;
        }
    }
    static int N;
    static int[] a;
    static int S;
    public static void Main()
    {
        StringBuilder sb = new StringBuilder();
        N = int.Parse(Console.ReadLine());
        a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        S = int.Parse(Console.ReadLine());
        Solve();
        sb.AppendJoin(" ", a);
        Console.WriteLine(sb);
    }
}
#endif
}
