using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 5
이름 : 배성훈
내용 : 케이크(?) 자르기
    문제번호 : 14445번

    수학, 기하학, 애드 혹 문제다
    처음에는 오로지 가로 세로로만 잘라 N = M x K인
    M - 1 + K - 1의 최솟값인줄 알았으나,
    범위가 10^18이라 폴라드 로와 밀러 라빈 판정법이 필요한데
    골드 5라 의문을 느껴 다시 읽어 보았다

    바깥에 생크림이 있다기에 생크림양을 조절해야하므로 
    위 방법은 생크림이 일정치 않아 사용할 수 없는 방법이다

    그리고 원하는 세팅으로 자를 수 있다기에
    n이 짝수면 중심을 지나고 바깥쪽의 길이가 1 / n 으로 자르면
    그러면 중심과 거리는 같으므로 바깥 너비가 같고 
    이는 부피가 같게 자른 것이므로 n / 2에 다 잘린다
    
    n이 홀수면, 두 선이 중심을 지나고 왼쪽 아래 모서리 쪽과 거리가
    1 / 4 x n 이 되게 자른다
    첫 번째 사람에게 양쪽의 부분을 넘겨주면 ㄴ ㄱ에 있는 케익을 넘겨주면
    1 / n이된다

    이후 나머지는 n - 1 / n 개 있고 n - 1 명이 남는다
    이후는 중심을 지나고 변의 길이가 1 / n 잘라서 주면 (n - 1) / 2에 자를 수 있다
    그러면 총 (n + 1) / 2번에 다 잘라지고 모두 균일한 케익이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0945
    {

        static void Main945(string[] args)
        {

            long n = long.Parse(Console.ReadLine());

            if (n == 1) Console.Write('0');
            else Console.Write((n + 1) >> 1);
        }
    }

#if other
// #include <stdio.h>
int main(){
    long long int n;
    scanf("%lld", &n);
    printf("%lld", n == 1 ? 0 : (n + 1) / 2);
}
#endif
}
