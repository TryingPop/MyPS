using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 15
이름 : 배성훈
내용 : 결혼식
    문제번호 : 2317번

    해석, 그리디 문제다
    -자리에 대입연산자를 잘못 넣어 여러 번 틀렸다

    주된 아이디어는 다음과 같다
    만약 사자 부족 사람이 20, 30이 입력되면

    20 ~ 30의 그냥 사람의 키는 무시해도된다
    20, 30 사이에 오름차순으로 넣으면

    20, 21, 30 또는 20, 24, 27, 30 과 같은 형태가 되고
    인접한 키 차이는 모두 10과 같다 즉, 20, 30과 같은 결과를 낸다

    그래서 사자 부족의 가장 작은 사람보다 작은 사람이 존재하는지, 
    혹은 사자부족의 큰 사람보다 큰 사람이 존재하는지 확인해주면 된다

    그리고 사자부족 사람 사이에 해당 사람을 놓고 사자부족으로 취급하면 된다
    이러한 방법을 반복하면 결국 사자부족 이외의 사람들은 가장 큰 사람과 가장 작은 사람 키만 캐치하면 된다

    그리고 사자부족의 사람들의 키를 캐치해야한다
    그러면 큰 사람이나 작은 사람을 어디에 놓는가가 주된 문제가된다

    큰 사람의 경우 끝쪽에 놓아 끝쪽 사람과 키 비교한 것과
    사자 부족 내에 가장 큰 사람 옆에 놓는 경우를 비교해 차이가 최소가 되는 경우를 찾아 확인해야한다

    이렇게 로직을 짜니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0231
    {

        static void Main231(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int k = ReadInt(sr);

            int[] lion = new int[k];

            int lionMin = int.MaxValue;
            int lionMax = 0;
            for (int i = 0; i < k; i++)
            {

                int cur = ReadInt(sr);
                lion[i] = cur;
                if (lionMin > cur) lionMin = cur;
                if (lionMax < cur) lionMax = cur;
            }

            int humanMin = int.MaxValue;
            int humanMax = -1;
            for (int i = k; i < n; i++)
            {

                int cur = ReadInt(sr);

                if (cur < humanMin) humanMin = cur;
                if (cur > humanMax) humanMax = cur;
            }

            sr.Close();

            long total = 0;
            for (int i = 1; i < k; i++)
            {

                int diff = lion[i] - lion[i - 1];
                diff = diff < 0 ? -diff : diff;
                total += diff;
            }

            long ret = total;

            if (humanMax > -1)
            {

                if (humanMin < lionMin)
                {

                    int chk1 = lion[0] - humanMin;
                    int chk2 = lion[k - 1] - humanMin;
                    int chk3 = 2 * (lionMin - humanMin);

                    int chkMin = chk1 < chk2 ? chk1 : chk2;
                    chkMin = chkMin < chk3 ? chkMin : chk3;

                    ret = total + chkMin;
                    total = ret;
                }
                
                if (humanMax > lionMax)
                {

                    int chk1 = humanMax - lion[0];
                    int chk2 = humanMax - lion[k - 1];
                    int chk3 = 2 * (humanMax - lionMax);

                    int chkMin = chk1 < chk2 ? chk1 : chk2;
                    chkMin = chkMin < chk3 ? chkMin : chk3;

                    ret = total + chkMin;
                }
            }

            Console.WriteLine(ret);
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
}
