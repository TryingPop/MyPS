using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 24
이름 : 배성훈
내용 : 순위 계산
    문제번호 : 17449번

    수학, 그리디, 애드 혹 문제다
    등수 값이 높은걸 찾는 방법은 쉽다
    이는 등수를 최대한 높이는 방법으로 가면 된다
    이는 자기보다 낮거나 같은 등수가 나오면 등수를 높인다

    반대로 등수 값이 낮은걸 찾는 방법에 신경 쓸 필요가 있다
    예제로 보면
        3
        4
        2 5 4 5
    2등은 자기보다 높아 등수가 3등에서 -> 4등이 된다
    중복 경우도 없고 5등은 자기보다 낮으니 신경쓸 필요가 없다
    이제 4등인 경우는 공동 4등이 가능하다
    그래서 공동 4등으로 있는다

    이후 5명인 경우 -> 다음 등수는 최소 6등이 되어야 하는데,
    앞에서 공동 4등이 불가능한 경우다
    이는 앞에서 5등이고 공동 5등이 최대이다

    공동 4등에서 -> 공동 5등으로 가는 관점을 잘 보면
    중복인원을 a, 등수차를 b라하면
    다음 중복인원은 a = a - b + 1 이 된다
    이렇게 진행하니 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_1075
    {

        static void Main1075(string[] args)
        {

            int[] arr;
            int s, n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Console.Write(GetHigh());
                Console.Write(' ');
                Console.Write(GetLow());

                int GetLow()
                {

                    int ret = s;

                    for (int i = 0; i < n; i++)
                    {

                        if (arr[i] <= ret) ret++;
                    }

                    return ret;
                }

                int GetHigh()
                {

                    int ret = s;
                    int same = 0;

                    for (int i = 0; i < n; i++)
                    {

                        if (arr[i] < ret) ret++;
                        else if (arr[i] == ret) same++;
                        else if (arr[i] > ret + same) continue;
                        else
                        {

                            int diff = arr[i] - ret;
                            same -= diff - 1;
                            ret = arr[i];
                        }
                    }

                    return ret;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                s = ReadInt();
                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();

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
    }

#if other
// #include <iostream>

using namespace std;

int main()
{
	ios::sync_with_stdio(0);
	cin.tie(0);
	
	int R, N;
	cin >> R >> N;

	int low = R;
	int high = R;
	int pass = low;

	for (int i = 0; i < N; i++)
	{
		int temp;
		cin >> temp;

		if (temp <= high)
			high++;

		if (temp < low)
		{
			pass++;
			low++;
		}

		else if (temp == low)
			pass += 1;

		if (temp > low && temp <= pass)
		{
			low = temp;
			pass += 1;
		}
	}

	cout << low << " " << high;
}
#endif
}
