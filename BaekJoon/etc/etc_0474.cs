using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 7
이름 : 배성훈
내용 : 침투계획 세우기
    문제번호 : 1606번

    수학, 애드 혹, 많은 조건 분기 문제다
    음이 아닌 좌표만 주어지므로 규칙성을 파악해 풀었다

    아이디어는 다음과 0, n을 축으로 삼고 해당 대각선을 기준으로 풀었다
    0, n 만 따로놀고, i > 0에 대해서는 i, n - i는 (1, n - 1) + (i - 1) 의 값과 일치한다
    그리고, 0, n과 1, n - 1은 6 * n - 1 차이 난다
    이렇게 제출하니 이상없이 통과했다

    계차수열이므로 1백만, 1백만 좌표는 int 범위를 넘어갈 것이라 추측해 long으로 했다
    실제로 조 단위였다!
*/

namespace BaekJoon.etc
{
    internal class etc_0474
    {

        static void Main474(string[] args)
        {

            int[] pos = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int idx = pos[0] + pos[1];

            long ret = 1;
            for (int i = 1; i <= idx; i++)
            {

                ret += i * 6;
            }

            if (pos[1] != 0)
            {

                ret -= idx * 6;
                ret += pos[1];
            }

            Console.WriteLine(ret);
        }
    }
#if other
import java.io.*;
import java.util.*;

public class Main {
    public static void main(String[] args) throws IOException {
        BufferedReader bf = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st = new StringTokenizer(bf.readLine());
        int tx = Integer.parseInt(st.nextToken());
        int ty = Integer.parseInt(st.nextToken());
        int x = 0;
        int y = 0;
        long i = 1;
        long s = 1;
        while(x++ != tx+ty) s+=i++*6;
        if(tx == x && ty == y) {
            System.out.println(s);
            return;
        }
        while(tx != x-- && ty != y++) s++;
        System.out.println(s-(i-1)*6);
    }
}
#endif
}
