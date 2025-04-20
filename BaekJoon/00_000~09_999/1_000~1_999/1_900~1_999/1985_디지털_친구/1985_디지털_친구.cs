using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 19
이름 : 배성훈
내용 : 디지털 친구
    문제번호 : 1985번

    문자열, 브루트포스 알고리즘 문제다
    조건대로 사용된 숫자가 같은지 확인하고
    같으면 프랜드로 출력 아니면 아래 과정을 진행한다

    만약 다르면 문자열 str1을 잡고 앞에서부터 변환과정을 실행시켰다
    그리고 str1에서도 사용된 숫자가 같지 않다면
    str1이 아닌 다른 str2 문자열로 변환 과정을 실행하면서 확인했다
    여기서 같은 경우가 발견되면 거의 친구로 출력하고 탈출한다

    이렇게 했는데도 탈출 못했다면 조건대로 다르다고 했다
    이렇게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0291
    {

        static void Main291(string[] args)
        {

            string R0 = "friends";
            string R1 = "almost friends";
            string R2 = "nothing";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int[] use1 = new int[10];
            int[] use2 = new int[10];

            int[] str1 = new int[102];
            int[] str2 = new int[102];

            for (int i = 0; i < 3; i++)
            {

                
                ReadCharArr(sr, use1, str1);
                ReadCharArr(sr, use2, str2);

                int ret = GetFriendType(str1, use1, str2, use2);
                sw.WriteLine(ret == 0 ? R0 : ret == 1 ? R1 : R2);
            }

            sr.Close();
            sw.Close();
        }

        static int GetFriendType(int[] _str1, int[] _use1, int[] _str2, int[] _use2)
        {

            if (ChkFriend(_use1, _use2)) return 0;

            // str1을 변환
            if (GetAlmost(_str1, _use1, _use2)) return 1;

            // str2를 변환
            if (GetAlmost(_str2, _use2, _use1)) return 1;

            return 2;
        }

        static bool GetAlmost(int[] _convertStr, int[] _convertUse, int[] _otherUse)
        {

            for (int i = 1; i <= _convertStr[0] - 1; i++)
            {

                int c = _convertStr[i];
                int n = _convertStr[i + 1];

                int cp = c == 9 ? 0 : c + 1;
                int cm = c == 0 ? 9 : c - 1;
                int np = n == 9 ? 0 : n + 1;
                int nm = n == 0 ? 9 : n - 1;

                if (i == 1 && (cm == 0 || cp == 0)) continue;
                _convertUse[c]--;
                _convertUse[n]--;

                _convertUse[cp]++;
                _convertUse[nm]++;
                if (ChkFriend(_convertUse, _otherUse)) return true;

                _convertUse[cp]--;
                _convertUse[nm]--;

                _convertUse[cm]++;
                _convertUse[np]++;
                if (ChkFriend(_convertUse, _otherUse)) return true;

                _convertUse[cm]--;
                _convertUse[np]--;

                _convertUse[c]++;
                _convertUse[n]++;
            }

            return false;
        }

        static bool ChkFriend(int[] _use1, int[] _use2)
        {

            for (int i = 0; i < 10; i++)
            {

                if ((_use1[i] == 0 && _use2[i] == 0)
                    || (_use1[i] > 0 && _use2[i] > 0)) continue;
                return false;
            }

            return true;
        }

        static void ReadCharArr(StreamReader _sr, int[] _use, int[] _arr)
        {

            for (int i = 0; i < 10; i++)
            {

                _use[i] = 0;
            }

            int c, idx = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                int add = c - '0';
                _arr[++idx] = add;
                _use[add]++;
            }

            _arr[0] = idx;
        }
    }
}
