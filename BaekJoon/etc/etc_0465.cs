using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 6
이름 : 배성훈
내용 : 유통기한
    문제번호 : 26083번

    구현, 조건 많은 분기 문제다
    yy, mm, dd 처리로 한 번 틀렸다
    그리고 표기법을 다 읽고 결과 도출하는 로직을 잘못짜서 한 번 더 틀렸다
    이를 수정하니 100ms에 이상없이 통과했다
    
    아이디어는 다음과 같다
    각각의 읽는 법에 따라 유효성 -> 유통기한 내외를 검사한다
    그리고 결과들을 종합해서 결론을 도출했다
*/

namespace BaekJoon.etc
{
    internal class etc_0465
    {

        static void Main465(string[] args)
        {

            string INVALID = "invalid\n";
            string SAFE = "safe\n";
            string UNSAFE = "unsafe\n";

            int[] day = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 4);
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536);

            int YY = ReadInt();
            int MM = ReadInt();
            int DD = ReadInt();

            int len = ReadInt();
            int[] chk = new int[3];
            for (int i = 0; i < len; i++)
            {

                int yy = ReadInt();
                int mm = ReadInt();
                int dd = ReadInt();

                // 읽는법에 따라 유효성 -> 유통기한 순서로 확인
                if (ChkInvalidDate(yy, mm, dd)) chk[0]++;
                else if (ChkSafe(yy, mm, dd)) chk[1]++;
                else chk[2]++;

                if (ChkInvalidDate(dd, mm, yy)) chk[0]++;
                else if (ChkSafe(dd, mm, yy)) chk[1]++;
                else chk[2]++;

                if (ChkInvalidDate(dd, yy, mm)) chk[0]++;
                else if (ChkSafe(dd, yy, mm)) chk[1]++;
                else chk[2]++;

                // 모은 결과로 결론 도출
                if (chk[0] == 3) sw.Write(INVALID);
                else if (chk[1] + chk[0] == 3) sw.Write(SAFE);
                else sw.Write(UNSAFE);

                chk[0] = 0;
                chk[1] = 0;
                chk[2] = 0;
            }

            sr.Close();
            sw.Close();

            bool ChkSafe(int _yy, int _mm, int _dd)
            {

                if (YY < _yy) return true;
                else if (YY > _yy) return false;

                if (MM < _mm) return true;
                else if (MM > _mm) return false;

                if (DD <= _dd) return true;
                else return false;
            }

            bool ChkInvalidDate(int _yy, int _mm, int _dd)
            {

                if (_mm > 12 || _mm < 1 || _dd < 1) return true;

                if (_mm == 2 && _yy % 4 == 0)
                {

                    if (_dd <= 29) return false;
                    else return true;
                }

                if (_dd <= day[_mm]) return false;
                return true;
            }

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
}
