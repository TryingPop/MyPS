using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 14
이름 : 배성훈
내용 : 만족도 점수
    문제번호 : 16501번

    DFS + 브루트포스로 풀었다
    팀을 조합으로 넣어야 하는데 순열로 넣어 같은 경우를 4번 중복 연산한다
    순열로 넣고 해도 속도가 빠르기에 따로 조합으로 안했다;
*/

namespace BaekJoon.etc
{
    internal class etc_0228
    {

        static void Main228(string[] args)
        {

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[] teams = new int[arr.Length];
            
            bool[] visit = new bool[arr.Length];
            float[] calc = new float[arr.Length / 2];
            visit[0] = true;
            teams[0] = arr[0];
            float ret = DFS(arr, teams, visit, calc, 1);

            Console.WriteLine($"{ret:0.0#}");
        }

        static float DFS(int[] _arr, int[] _team, bool[] _visit, float[] _calc, int _depth)
        {

            if (_depth == _arr.Length)
            {

                // 팀 평균 계산
                for (int i = 0; i < 4; i++)
                {

                    _calc[i] = (_team[2 * i] + _team[2 * i + 1]) / 2.0f;
                }

                // 경기 할 때
                // 1 - |(평균 차이) / 10| 의 최소값을 찾아야 한다
                // 이는 |(평균차이)| 의 최대값 일 때와 동형이다
                // 그래서 평균차이의 최대값을 찾는다
                float diff = 0.0f;
                for (int i = 0; i < _calc.Length / 2; i++)
                {

                    float calc = _calc[2 * i] - _calc[2 * i + 1];
                    calc = calc < 0 ? -calc : calc;

                    if (diff < calc) diff = calc;
                }

                // 평균 차이 최대값을 찾았으니
                // 만족도의 최소값(하한)을 찾는다
                diff = MathF.Round(1 - (diff / 10), 2);
                // 하한을 반환
                return diff;
            }

            // 하한의 최댓값을 반환한다
            float ret = 0.0f;
            for (int i = 0; i < _arr.Length; i++)
            {

                if (_visit[i]) continue;
                _visit[i] = true;

                _team[_depth] = _arr[i];

                // 하한을 받는다
                float calc = DFS(_arr, _team, _visit, _calc, _depth + 1);

                // 하한의 최댓값 찾기
                if (calc > ret) ret = calc;
                _visit[i] = false;
            }

            return ret;
        }
    }

#if other
public static class PS
{
    private static int[] skillLvl;
    private static double[] avgSkillLvl;
    private static int[] seq;
    private static bool[] visited;
    private static double max;

    static PS()
    {
        skillLvl = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        avgSkillLvl = new double[4];
        seq = new int[8];
        visited = new bool[8];
        max = 0;
    }

    public static void Main()
    {
        DFS(0);

        string s = max.ToString();

        if (s.Length < 4)
            Console.Write("{0:F1}", max);
        else
            Console.Write("{0:F2}", max);
    }

    private static void DFS(int depth)
    {
        if (depth == 8)
        {
            for (int i = 0; i < 4; i++)
            {
                avgSkillLvl[i] = (double)(skillLvl[seq[i]] + skillLvl[seq[7 - i]]) / 2;
            }

            Array.Sort(avgSkillLvl);

            max = Math.Max(max, Math.Min(GetSatisfactionRate(avgSkillLvl[0], avgSkillLvl[1]),
                GetSatisfactionRate(avgSkillLvl[2], avgSkillLvl[3])));

            return;
        }

        for (int i = 0; i < 8; i++)
        {
            if (!visited[i])
            {
                visited[i] = true;
                seq[depth] = i;
                DFS(depth + 1);
                visited[i] = false;
            }
        }
    }

    private static double GetSatisfactionRate(double lvl1, double lvl2) => 1 - Math.Abs(lvl1 - lvl2) / 10;
}
#elif other2
import sys
from itertools import combinations
input = sys.stdin.readline

// # 1. 코트의 만족도 점수 계산 함수 생성
def return_score(people) :
// # 1-1. 인원의 실력 점수 리스트 생성 후 정렬
    array = []
    for idx in people :
        array.append(ability[idx])
    array.sort()
// # 1-2. 팀을 나눠 만족도 점수 계산
    score = 1 - (abs((array[0] + array[3])/2 - (array[1] + array[2])/2)/10)
// # 1-3. 계산된 점수 리턴
    return score
def solution(ability) :
    ans = -float('inf')
// # 2. 1번 코트에 들어갈 인원 조합 구하기
    combination = combinations(range(1, 9), 4)
    for court1 in combination :
// # 3. 2번 코트에 들어갈 인원 조합 구하기
        court2 = [i for i in range(1, 9) if i not in court1]
// # 4. 두 코트 중 낮은 만족도 구하기
// # 5. 만족도의 최댓값 업데이트
        ans = max(ans, min(return_score(court1), return_score(court2)))
// # 6. 결과 출력
    print(round(ans, 2))
    
if __name__ == '__main__' :
    ability = [0] + list(map(float, input().split()))
    solution(ability)

#endif
}
