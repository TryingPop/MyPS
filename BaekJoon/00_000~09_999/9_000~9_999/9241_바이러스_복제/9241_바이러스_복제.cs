using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 바이러스 복제
    문제번호 : 9241번

    그리디 알고리즘, 문자열, 구현 문제다
    너무 간단하게 생각하고 정답을 설정해서 몇몇 반례를 못 잡아냈다
    
    주된 아이디어는 다음과 같다
    왼쪽에서 부터 같은 것을 찾는다
    다른 순간 같은 끝지점을 기록한다

    다음으로 오른쪽에서 반대 순서로 탐색을 한다
    다른 점이 나오면 멈추고 같은 끝 지점을 기록한다
    여기서 발견한 왼쪽 전까지만 탐색해야한다!

        AA
        AAAA
        인 경우 2

        AAAA
        AA
        인 경우 0 

    이러한 반례가 있다
    이경우만 캐치하면 되는 줄알고 하드코딩을 하다가 2번 더 틀렸다
    그리고 그냥 왼쪽 전까지만 기록하고 로직대로 구현해서 맞췄다;
    그러면 이제 감염 후의 상황에서 왼쪽 같은 부분과 오른쪽 같은 부분을 떼어내고
    남은 부분이 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0261
    {

        static void Main261(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            string before = sr.ReadLine();
            string after = sr.ReadLine();

            sr.Close();

            int l = -1; 
            int r = -1;
            int bL = before.Length;
            int aL = after.Length;
            int minLen = bL < aL ? bL : aL;
            for (int i = 0; i < minLen; i++)
            {

                if (before[i] == after[i])
                {

                    l++;
                    continue;
                }

                break;
            }

            for (int i = 0; i < minLen; i++)
            {

                int rBefore = bL - i - 1;
                int rAfter = aL - i - 1;
                if (rBefore <= l || rAfter <= l) break;
                if (before[rBefore] == after[rAfter])
                {

                    r++;
                    continue;
                }
                break;
            }

            int ret = (aL - 1 - r) - l - 1;
            Console.WriteLine(ret);
        }
    }
}
