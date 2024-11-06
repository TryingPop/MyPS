/*
날짜 : 2024. 7. 17 
이름 : 배성훈
내용 : Heights
    문제번호 : 17236번

    수학, 기하학 문제다
    넓이가 같으므로 각 변들의 비율을 찾아낼 수 있다
    우선은 변들을 높이의 역수로 넣는다

    그리고 헤론의 공식을 써서 넓이 area를 찾아내면
    역수 길이일 때 높이를 알 수 있다
    
    우리가 찾고자 하는 넓이를 S라 하면 그리고 비례식을 세워보면
    S = (0.5)* (2S * (1 / h1)) * h1 = 2S * (0.5) * (1 / h1) * (h1 / 2S) * 2S = 4 * S * S * area
    => S = 1 / (4 * area)
    를 얻는다

    이렇게 찾아 제출하니 이상없이 통과했다
*/


namespace BaekJoon.etc
{
    internal class etc_0822
    {

        static void Main822(string[] args)
        {

            double h1, h2, h3;

            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                double r1 = 1 / h1;
                double r2 = 1 / h2;
                double r3 = 1 / h3;

                double sum = (r1 + r2 + r3) / 2;

                double area = 4 * Math.Sqrt(sum * (sum - r1) * (sum - r2) * (sum - r3));
                Console.Write($"{1 / area:0.0000000}");
            }

            void Init()
            {

                h1 = double.Parse(Console.ReadLine());
                h2 = double.Parse(Console.ReadLine());
                h3 = double.Parse(Console.ReadLine());
            }
        }
    }

#if other
// #include <stdio.h>
// #include <math.h>

int main()
{
	double h[3];
	scanf("%lf %lf %lf", &h[0], &h[1], &h[2]);

	double inv_h[3];
	for (int i = 0; i < 3; i++)
		inv_h[i] = 1 / h[i];
	
	double s = inv_h[0] + inv_h[1] + inv_h[2];
	printf("%lf", (1 / sqrt(s * (s - 2 * inv_h[0]) * (s - 2 * inv_h[1]) * (s - 2 * inv_h[2]))));
	return 0;
}
#endif
}
