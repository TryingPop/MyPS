using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 14
이름 : 배성훈
내용 : 숫자 맞추기
    문제번호 : 4335번

    구현 문제다

    아이디어는 다음과 같다
    num을 입력 받았을 때, too lower을 외치는 순간
    해당 값보다 작으므로 하한값을 확인한 뒤
    num이 하한보다 크면 하한을 num으로 올린다

    too high인 경우는
    확인해서 상한을 수정한다

    그리고 결과를 외쳤을 때, 상한과 하한이
    결과값이랑 비교해서 정직한지 않한지 확인했다

    다만 상한과 하한의 초기값을 잘못 설정해서 한 번 틀렸다
    그리고 여기서 상한과 하한은 일반적인 상한과 하한을 의미하는게 아니다
    상한의 경우 결과가 해당값보다 무조건 작아야한다는 의미로 썼다
    하한도 해당값 보다 무조건 커야함을 의미한다
    즉, 같은 경우는 포함안시킨다!
*/

namespace BaekJoon.etc
{
    internal class etc_0226
    {

        static void Main226(string[] args)
        {

            string HONEST = "Stan may be honest";
            string DISHONEST = "Stan is dishonest";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int sup = 11;
            int inf = 0;

            while (true)
            {

                int num = ReadInt(sr);

                if (num == 0) break;

                string[] temp = sr.ReadLine().Split(' ');

                if (temp[1][0] == 'h')
                {

                    // 상한 재설정
                    // 현재 값이 크다고 했으므로
                    // 해당 값보다 작아야한다
                    // 즉 상한이 num보다 크면 상한을 num으로 수정해야한다
                    if (sup > num) sup = num;
                }
                else if (temp[1][0] == 'l')
                {

                    // 하한 재설정
                    if (inf < num) inf = num;
                }
                else
                {

                    
                    if (inf >= num || sup <= num) sw.WriteLine(DISHONEST);
                    else sw.WriteLine(HONEST);

                    sup = 11;
                    inf = 0;
                }
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
    }
}
