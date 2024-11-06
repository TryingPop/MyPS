using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 어두운 굴다리
    문제번호 : 17266번

    구현 이분탐색 문제다
    그리디하게 풀었다 
    시작지점과 가로등 사이의 거리, 끝지점과 가로등 사이의 거리
    그리고, 가로등 간의 거리의 절반 중 가장 큰게 모두 비추는 최소 높이가 된다

    밑에는 다른사람이 이분탐색으로 푼 풀이다
*/

namespace BaekJoon.etc
{
    internal class etc_0255
    {

        static void Main255(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            int maxDis = ReadInt(sr);
            int before = maxDis;
            for (int i = 1; i < m; i++)
            {

                int cur = ReadInt(sr);
                int dis = (cur - before + 1) / 2;
                if (maxDis < dis)
                {

                    maxDis = dis;
                }

                before = cur;
            }

            sr.Close();

            if (n - before > maxDis) maxDis = n - before;
            Console.WriteLine(maxDis);
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
// C#에서 빠른 입출력을 위해 사용
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

// [입력]
int N = int.Parse(sr.ReadLine()); // 굴다리의 길이
int M = int.Parse(sr.ReadLine()); // 가로등의 개수
int[] X = sr.ReadLine().Split().Select(int.Parse).ToArray(); // 가로등의 위치 (오름차순)

// 전형적인 매개변수 탐색 루틴
int H = N; // 일단 최대값으로 초기화
int min = 1; // == left
int max = N; // == right

while(min <= max) {
    int mid = (min + max) / 2;
    if(Posible(mid)) {
        H = Math.Min(H, mid);
        max = mid - 1;
    } else min = mid + 1;
}

// [출력]
sw.WriteLine(H);
sr.Close();
sw.Close();

// Possible(int h) : [높이 h가 주어졌을 때 문제 조건을 충족하는가?]
bool Posible(int h) {
    // 핵심 아이디어 : i번 가로등은 왼쪽으로 최소한 min만큼은 빛을 비춰야 한다.
    // min의 초기값이 0인 이유는 맨 왼쪽의 가로등이 0번 위치까지는 빛을 비춰야 하기 때문이다.
    int min = 0; 
    for(int i = 0; i < M; i++) {
        if(X[i] - h > min) return false; // 만약 X[i]번 가로등이 왼쪽으로 min까지 빛을 못 준다면 빈 틈이 생긴다. 바로 탈락!
        else min = X[i] + h; // min까지 빛을 줄 수 있다. min 값을 가로등의 오른쪽 범위로 재설정 한다.
    }
    return min >= N; // 마지막 가로등이 오른쪽으로 N까지 빛을 비출 수 있는지를 반환한다.
}
#endif
}
