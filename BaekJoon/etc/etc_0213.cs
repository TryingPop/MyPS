using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 24
이름 : 배성훈
내용 : 모자이크
    문제번호 : 2539번

    이분탐색 문제다
    정렬을 빠뜨려서 계속해서 틀렸다
    그리고 이분 탐색에서 조건을 설정해서 계속해서 틀렸다
    maxUse 이하인 최대값을 즉 조건문에 curUse <= maxUse 해야하는데
    curUse < maxUse를 계속해서 초과인 최소값에 - 1을 해서 틀렸다;

    이는 1, 2, 2, 2, 2, 2, 2, 2, 3인 정렬된 수열에서
    2 중에 가장 작은 항을 찾아야하는데 보면 2이다
    그런데 8을 계속 가리키라고 조건문을 넣어서 틀렸다

    그리고 코드를 수정하면서 Array.Sort를 빼먹어 또 틀리고 있었다;

    문제 풀이 아이디어는 맞는거 같아 전체 코드를 다시 짜고 확인하면서 발견했다
    이를 수정하니 이상없이 통과했다

    문제 아이디어는 다음과 같다
    밑 변에 붙여 색종이를 붙이기에 잘못된 부분을 모두 덮으려면 모자이크 부분의 가장 큰 y보다는
    색종이의 크기가 커야한다

    그리디하게 모자이크를 x축 오름차순으로 정렬하고, 
    색종이 크기를 설정해 왼쪽에서부터 색종이를 최소한으로 두며 모자이크 구멍을 하나씩 메꿔 나간다
    최소한은 모자이크 부분을 왼쪽 덮게 해서 붙인다 그리고 포함된 모자이크는 무시하고 
    포함안된 모자이크 중에 왼쪽 끝에 있는 모자이크에 색종이를 붙인다 이렇게 반복해가면
    색종이를 최소한으로 붙일 수 있다

    그리고, 사용한 색종이가 전체 색종이보다 많으면 색종이 크기를 늘려야하고
    사용한 색종이가 전체 색종이보다 작으면 길이를 줄여 다시 찾아본다
    색종이 크기 설정하는 것은 이분 탐색을 써서 했다

    이러한 아이디어로 제출하니 실수로 여러 번 틀렸지만 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0213
    {

        static void Main213(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt();
            int col = ReadInt();

            int maxUse = ReadInt();
            int len = ReadInt();

            Pos[] pos = new Pos[len];
            int left = 0;
            int right = 1_000_001;

            for (int i = 0; i < len; i++)
            {

                int curR = ReadInt();

                pos[i].Set(curR, ReadInt());
                left = left < curR ? curR : left;
            }

            sr.Close();

            Array.Sort(pos);

            while (left <= right)
            {

                int mid = (left + right) / 2;
                int curUse = 1;
                int curC = pos[0].c;

                for (int i = 1; i < len; i++)
                {

                    if (pos[i].c < curC + mid) continue; 
                    curUse++;
                    curC = pos[i].c;
                }

                // 현재 사용한게 적을 경우 크기를 줄여본다
                if (curUse <= maxUse) 
                { 
                    
                    right = mid - 1;
                }
                else left = mid + 1;
            }

            Console.WriteLine(right + 1);

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

        struct Pos : IComparable<Pos>
        {

            public int r;
            public int c;


            public int CompareTo(Pos other)
            {

                return c.CompareTo(other.c);
            }

            public void Set(int _r, int _c)
            {

                r = _r;
                c = _c;
            }
        }
    }

#if other
namespace ConsoleApp1
{
    class Program
    {
        static void _1219() // 모자이크
        {
            string input;
            int col, row;
            input = Console.ReadLine();
            col = Convert.ToInt32(input.Split()[0]);
            row = Convert.ToInt32(input.Split()[1]);

            bool[] table = new bool[row + 1];
            int page = Convert.ToInt32(Console.ReadLine());
            int black = Convert.ToInt32(Console.ReadLine());

            int min = 0;
            int max = col;

            int left, right;

            for (int i = 0; i < black; i++)
            {
                input = Console.ReadLine();
                left = Convert.ToInt32(input.Split()[0]);
                right = Convert.ToInt32(input.Split()[1]);

                if (left >= min)
                    min = left;

                table[right] = true;
            }

            int mid = -1;
            int count = 0;
            bool find = false;
            while (min < max)
            {
                mid = (min + max) / 2;
                for (int i = 1; i <= row; i++)
                {
                    if (table[i])
                        find = true;

                    if (find)
                    {
                        i += mid - 1;
                        count++;
                        find = false;
                    }
                }
                if (count > page)
                    min = mid + 1;
                else
                    max = mid;
                count = 0;
            }
            Console.WriteLine($"{max}");
        }

        static void Main(string[] args)
        {
            _1219();
        }
    }
}

#endif
}
