using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 14
이름 : 배성훈
내용 : 종이 접기
    문제번호 : 1802번

    분할 정복 문제다
    로직을 잘못 설정해 한 번 틀렸다

    처음에는 그냥 중앙 값만 비교해서 뒤집히면 맞는거 아닌가 생각했다
    그렇게 제출하니 16%에서 틀렸다

    그리고, 조금 더 고민해보니,
    종이를 1번으로 반으로 접으면 왼쪽을 L, 오른쪽을 R이라할 때,
    L의 윗쪽은 그대로이고 R의 위쪽은 반대로 바뀐다

    그래서 이후 접는 것에 대해 L의 값과 R의 값이 서로 뒤집혀야한다!
    이후에 이부분을 바꿔 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0224
    {

        static void Main224(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int n = ReadInt(sr);

            for (int i = 0; i < n; i++)
            {

                string str = sr.ReadLine();

                bool ret = DivideConquer(str, 0, str.Length - 1);

                if (ret) sw.WriteLine("YES");
                else sw.WriteLine("NO");
            }

            sr.Close();
            sw.Close();
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

        static bool DivideConquer(string _str, int _start, int _end)
        {

            if (_start == _end) return true;

            int mid = (_start + _end) / 2;

            int left = (_start + mid - 1) / 2;
            int right = (_end + mid + 1) / 2;

            for (int i = _start; i < mid; i++)
            {

                if (_str[i] == _str[_end - i]) return false;
            }

            bool ret = DivideConquer(_str, _start, mid - 1);
            ret = ret && DivideConquer(_str, mid + 1, _end);
            return ret;
        }
    }
}
