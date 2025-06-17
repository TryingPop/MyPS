using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 4
이름 : 배성훈
내용 : 지그재그 부분배열
    문제번호 : 25571번

    증가 감소 증가 형태나
    감소 증가 감소 형태인 길이가 2 이상인 
    연속된 부분 수열을 모두 찾는 문제다

    수열을 읽어오면서 
    한 번에 확인하는 깔끔한 코드가 안떠올라 그냥 두번 연산해!라는 마인드로
    증가 감소 증가 감소 ... 로 진행하는 경우랑
    감소 증가 감소 증가 ... 로 진행하는 경우 두 번 확인했다

    길이가 끊기는 경우 해당 길이가 갖는 모든 경우의 수를 더해줬다
    조합 n C 2이다! -> 최대 길이가 4이면 4 C 2 = 4 * 3 / 2
    이는 해당 수열 안에서 중복하지 않는 두 수를 선택해 작은것을 시작지점, 큰 것을 끝지점으로 하면 되기 때문이다

    증가 감소 증가 감소, 
    감소 증가 감소 증가 판별은 서로 독립된 구간을 판별하는 것이기에
    중복해서 세는 경우도 없다

    그리고 마지막까지 쭉 이어지는 경우가 나올 수 있기에 맨 뒤에 값을 채워넣는 연산을 한다
    결과값 자료형은 long으로 했다
    수열의 길이가 10만이고, 최악의 경우 지그재그 부분배열의 최장이 10만인 경우면 45억을 초과하기 때문이다

    그래서 제출하니 이상없이 220ms로 통과했다
    처음에 숫자를 따로 nums[i]에 저장해서 했는데 216ms가 나왔고, 뒤에 배열에 넣는거 없이 바로 탐색을 진행했는데,
    불필요한 메모리 낭비 같아 배열에 넣지 않고 바로 검증하는 과정을 했다 그런데 4ms 느리게 나왔다

*/

namespace BaekJoon.etc
{
    internal class etc_0148
    {

        static void Main148(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt(sr);

            while(test-- > 0)
            {

                int len = ReadInt(sr);
                long ret = 0;
                
                int before = ReadInt(sr);

                bool isUp = false;
                long chkLen1 = 0;
                long chkLen2 = 0;

                for (int i = 1; i < len; i++)
                {

                    isUp = !isUp;
                    int cur = ReadInt(sr);
                    // 증가 감소 증가 감소 ... 연산
                    if (isUp && before < cur) chkLen1 += 1;
                    else if (!isUp && before > cur) chkLen1 += 1;
                    else
                    {

                        if (chkLen1 != 0) ret += (chkLen1 * (chkLen1 + 1)) / 2;
                        chkLen1 = 0;
                    }

                    // 감소 증가 감소 증가 ... 연산
                    if (isUp && before > cur) chkLen2 += 1;
                    else if (!isUp && before < cur) chkLen2 += 1;
                    else
                    {

                        if (chkLen2 != 0) ret += (chkLen2 * (chkLen2 + 1)) / 2;
                        chkLen2 = 0;
                    }

                    before = cur;
                }

                ret += (chkLen1 * (chkLen1 + 1)) / 2;
                ret += (chkLen2 * (chkLen2 + 1)) / 2;

                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;
            while((c = _sr.Read())!= -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }
                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }
}
