using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 13
이름 : 배성훈
내용 : 경운기
    문제번호 : 10477번

    수학, 브루트 포스 알고리즘 문제다
    원리를 알면 찾으면 쉽게 풀린다

    초당 이동 거리가 우선 2의 제곱수이므로
    앞의 값들을 모두 합쳐도 다음 수가 더 크기 때문에
    뒤로 가는 경우는 존재할 수 없다

    그리고 2의 제곱수의 합으로는 총합보다 작은 아무 수나 만들 수 있다
    예를들어 1 + 2 + 4 라면,
    7보다 작은 모든 수를 1, 2, 4의 덧셈으로 표현 가능하다!
    1 + 2 + 4 + 8 + 16 이면 31 이하의 모든 수를 1, 2, 4, 8, 16의 합으로 표현 가능하다

    이 두개의 생각을 하면 구역 내에 이제 빗변과 만나는 점의 개수로 들어갈 수 있다
    주어진 구역 내에 x, y좌표의 합이 0, 1, 3, 7, 15, .... 인 빗변과 만나는 점들의 개수가 정답이 된다
    빗변의 합은 잘 보면 (2^n) - 1의 형태다!
    주어진 구역의 작은 값을 min, 큰 값을 max라 하자

    그러면, 0, 1, 3, 7, 15, ... < min인 경우
    각각 1, 2, 4, 8, 16, ... 개의 점이 포함된다

    이제 걸치는 경우 이 때는, 우선 min + 1 값을 넘지 못한다
    각 빗변에서 만날 수 있는 점의 개수는 많아야 (2^n) - 1과 min 중 작은 값인 calc개 만큼 가능하다

    상한은 calc 임을 알았다 이제 걸칠 때 만나는 경우를 보면
    가장 마지막 빗변의 만나는 점의 개수는 min + max - (빗변의 합) + 2임을 알 수 있다
    물론 min + 1 을 넘지 못한다

    그리고 min + max < 빗변의 합보다 작다면 만날 수 없다
    해당 아이디어로 푸는 것은 나왔으나, 식으로 옮기는데 시간이 걸렸다;
    2중 min을 써서 조금 복잡해 보인다;

    방법은 왼쪽 끝값과 오른쪽 끝값 차이 + 1 만큼씩 
    더해주는 다른 사람 풀이가 깔끔해 보인다

    문제 자체는 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0210
    {

        static void Main210(string[] args)
        {

            int[] twoPow = new int[30];
            twoPow[0] = 1;
            for (int i = 1; i < twoPow.Length; i++)
            {

                twoPow[i] = twoPow[i - 1] * 2;
            }

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt(sr);

            while(test-- > 0)
            {

                int x = ReadInt(sr);
                int y = ReadInt(sr);

                /*
                // 이전 풀이 방법
                int min = x < y ? x : y;
                int chk = x < y ? y : x;

                chk += min;
                int ret = 0;

                for (int i = 0; i < 30; i++)
                {

                    if (chk < twoPow[i] - 1) break;

                    int calc = Math.Min(twoPow[i] - 1, min);
                    ret += Math.Min(chk - twoPow[i] + 2, calc + 1);
                } 
                */ 

                // 다른 사람 아이디어 참고
                int min = x < y ? x : y;
                int max = x < y ? y : x;

                int ret = 0;

                for (int i = 0; i < 30; i++)
                {

                    if (min + max < twoPow[i] - 1) break;

                    // 해당 식은
                    // 아래 식을 풀어 쓴 것
                    // ret += twoPow[i]                                             // 전체 점의 개수
                    //      - (twoPow[i] - 1 - Math.Min(min, twoPow[i] - 1))        // 왼쪽에 벗어나는 점의 개수
                    //      - (twoPow[i] - 1 - Math.Min(max, twoPow[i] - 1));       // 오른쪽에 벗어나는 점의 개수
                    ret += Math.Min(max, twoPow[i] - 1) 
                        + Math.Min(min, twoPow[i] - 1) 
                        + 2 - twoPow[i];
                }

                sw.WriteLine(ret);
            }
            sw.Close();
            sr.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
for _ in range(int(input())):
  a,b = map(int,input().split())
  s = ans = 0
  while s <= a + b:
    ans += s+1 - (s - min(a,s)) - (s - min(b,s))
    s += s + 1
  print(ans)
#endif
}
