using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 6
이름 : 배성훈
내용 : 행운을 빌어요
    문제번호 : 31713번

    수학, 브루트포스 문제다
    그리디하게 풀었다
    n이 6 이상인 순간 넘어가는 순간 적당한 자연수 x, y가 존재해 3x + 4y = n으로 표현 가능하다
    1, 2, 5 반례처리를 안해서 한 번 틀렸다;

    아이디어는 다음과 같다
    만약 일단 줄기와 잎사귀 최대한 짝을 지어 나머지가 최소가 되게한다
    그리고 잎사귀가 몇 장남았는지, 줄기가 몇 장남았는지 경우를 확인한다
    
    그리고 줄기와 잎사귀를 최소한으로 추가한다
    여기서 잎사귀가 부족한 경우면 잎사귀 3 *로 엮는 다고 생각해 최소한으로 잎사귀를 보충한다

    그리고 줄기가 부족한 경우면, 1, 2, 5를 제외하면 줄기만 최소한으로 보충하면 된다
    그러면 잎사귀를 재분배하면 알아서 클로버가 완성된다
    이제 1, 2, 5만 반례 처리해주면 된다
    2, 5개의 경우 줄기 추가 이외에 잎사귀가 1개가 더 있어야 가능하다
    1개의 경우는 줄기 추가 이외에 잎사귀가 2개 더 있어야 클로버를 만들 수 있다

    이렇게 제출하니 64ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0469
    {

        static void Main469(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();

            while(test-- > 0)
            {

                int f = ReadInt();
                int b = ReadInt();

                int ret = 0;
                if (f * 4 < b)
                {

                    // 줄기가 부족한 경우
                    int calc = b - f * 4;
                    calc--;
                    ret = 1 + (calc / 4);

                    // 반례처리
                    if (b == 5) ret++;
                    else if (b == 2) ret++;
                    else if (b == 1) ret += 2;
                }
                else if (b < f * 3)
                {

                    // 잎사귀가 부족한 경우
                    ret = f * 3 - b;
                }

                sw.WriteLine(ret);
            }

            sr.Close();
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
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class Main {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringBuilder sb = new StringBuilder();
        for (int T = Integer.parseInt(br.readLine()); T > 0; T--) {
            StringTokenizer st = new StringTokenizer(br.readLine());
            int A = Integer.parseInt(st.nextToken()), B = Integer.parseInt(st.nextToken());
            if (B <= A * 3)
                sb.append(A * 3 - B).append('\n');
            else if (B <= A * 4)
                sb.append(0).append('\n');
            else {
                int i;
                for (i = A + 1; ; i++)
                    if (B <= i * 4)
                        break;
                if (B <= i * 3)
                    sb.append(i - A + i * 3 - B).append('\n');
                else
                    sb.append(i - A).append('\n');
            }
        }
        System.out.println(sb);
    }
}
#endif
}
