using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 3
이름 : 배성훈
내용 : 하늘에서 별똥별이 빗발친다
    문제번호 : 14658번

    브루트포스 문제다
    아이디어는 그리디하게 접근했다
    별의 개수가 100개 밖에 안되어서 별의 r과 c를 끝점에
    트램펄린 모서리를 설정해 놓아 최대 개수를 세었다

    처음에는 특정 1개의 star에 한해 r, c를 설정했으나
            *
        *       *
            * 
    이런 반례를 보고 r, c의 좌표에 대해서 조사하니 이상없이 통과했다
    
*/

namespace BaekJoon.etc
{
    internal class etc_0675
    {

        static void Main675(string[] args)
        {

            StreamReader sr;
            int n, m, l, k;
            (int r, int c)[] star;
            (int r, int c) pos;
            int ret = 0;

            Solve();

            void Solve()
            {

                Input();

                for (int i = 0; i < k; i++)
                {

                    for (int j = 0; j < k; j++)
                    {

                        pos = (star[i].r, star[j].c);
                        int count = Count();
                        ret = ret < count ? count : ret;
                    }
                }

                Console.WriteLine(k - ret);
            }


            int Count()
            {

                int ret = 0;
                for (int i = 0; i < k; i++)
                {

                    if (pos.r > star[i].r || pos.r + l < star[i].r
                        || pos.c > star[i].c || pos.c + l < star[i].c) continue;

                    ret++;
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                l = ReadInt();
                k = ReadInt();

                star = new (int r, int c)[k];

                for (int i = 0; i < k; i++)
                {

                    star[i] = (ReadInt(), ReadInt());
                }

                sr.Close();
            }

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
using System;

// 오늘은 함수형 프로그래밍을 해보겠습니다. 매개변수가 많습니다.
// 시간 초과!
namespace no14658try1
{
    internal class Program
    {
        // 별의 위치를 나타낼 수 있고
        // 정사각형의 왼쪽 위의 위치를 기준으로 하여 트램펄린의 위치를 표현할 수 있습니다.
        struct Vector
        {
            public int x;
            public int y;
        }


        static Vector[] GetTrampolinePositions(Vector star1, Vector star2, int size)
        {
            Vector[] result;
            bool isEqualX = star1.x == star2.x;
            bool isEqualY = star1.y == star2.y;

            if (isEqualX && isEqualY) // case 1
            {
                result = new Vector[4]
                {
                    new Vector() { x = star1.x, y = star1.y },
                    new Vector() { x = star1.x, y = star1.y - size },
                    new Vector() { x = star1.x - size, y = star1.y },
                    new Vector() { x = star1.x - size, y = star1.y - size }
                };

            }
            else if (isEqualX || isEqualY) // case 2
            {
                result = new Vector[0]; // case 2의 상황은 case1이 연산할 수 있습니다.
            }
            else // case 3
            {
                result = new Vector[2];

                if ((star1.x - star2.x) * (star1.y - star2.y) > 0)
                {
                    result[0] = new Vector()
                    {
                        x = Math.Max(star1.x, star2.x) - size,
                        y = Math.Min(star1.y, star2.y)
                    };
                    result[1] = new Vector()
                    {
                        x = Math.Min(star1.x, star2.x),
                        y = Math.Max(star1.y, star2.y) - size
                    };
                }
                else
                {
                    result[0] = new Vector()
                    {
                        x = Math.Min(star1.x, star2.x),
                        y = Math.Min(star1.y, star2.y)
                    };
                    result[1] = new Vector()
                    {
                        x = Math.Max(star1.x, star2.x) - size,
                        y = Math.Max(star1.y, star2.y) - size
                    };
                }
            }

            return result;
        }



        static Vector[] GetTrampolinePositions(Vector star, int size)
        {
            Vector[] result = new Vector[size * 4];
            
            for (int point = 0; point < size; ++point)
            {
                result[point] = new Vector() { x = star.x, y = star.y - point };
                result[point + size] = new Vector() { x = star.x - point, y = star.y - size };
                result[point + size * 2] = new Vector() { x = star.x - size, y = star.y - size + point };
                result[point + size * 3] = new Vector() { x = star.x - size + point, y = star.y };
            }

            return result;
        }

        static int GetCatchedStarCount(Vector[] stars, int size, Vector trampolinePosition)
        {
            int result = 0;

            for (int index = 0; index < stars.Length; ++index)
            {
                if (stars[index].x < trampolinePosition.x || stars[index].x > trampolinePosition.x + size + 1) continue;
                if (stars[index].y < trampolinePosition.y || stars[index].y > trampolinePosition.y + size + 1) continue;
                ++result;
            }

            return result;
        }

        static void Main(string[] args)
        {
            int result = 0;

            string[] nums = Console.ReadLine().Split(' ');
            
            int N = int.Parse(nums[0]);
            int M = int.Parse(nums[1]);
            int L = int.Parse(nums[2]);
            int K = int.Parse(nums[3]);
            Vector[] stars = new Vector[K];

            for (int index = 0; index < K; ++index)
            {
                string[] posString = Console.ReadLine().Split(' ');

                stars[index] = new Vector() { x = int.Parse(posString[0]), y = int.Parse(posString[1]) };
            }

            for (int index1 = 0; index1 < K; ++index1)
            {
                for (int index2 = index1; index2 < K; ++index2)
                {
                    Vector[] trampolines = GetTrampolinePositions(stars[index1], stars[index2], L);

                    for (int trampolinesIndex = 0; trampolinesIndex < trampolines.Length; ++trampolinesIndex)
                    {
                        result = Math.Max(result, GetCatchedStarCount(stars, L, trampolines[trampolinesIndex]));
                    }
                }
            }

            Console.WriteLine(K - result);
        }
    }
}
#endif
}
