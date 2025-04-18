using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 1
이름 : 배성훈
내용 : 엑셀
    문제번호 : 2757번

    무턱대고 26진법으로 구했다가 예제에서 막혔다
    각자리수가 늘어날 때 숫자를 봤다
    그러니 3자리인 경우
        26^2 + 26 + 1이었고
    이로 n자리가 되는 가장 작은 숫자는 
        26^(n - 1) + 26^(n - 2) + ... + 1
    이 아닐까 추론했다

    2번째 자리 변경은 26씩 증가해야 변환되고 
    끝에서 3번째(10진법으로 100의 자리)는 26^2
    이렇게 진행하면서 귀납적으로 성립함을 알았다

    그래서 자리수를 찾고
    원래 숫자에서 해당 숫자를 뺐다
    그러면 26진법이 성립한다
    즉, 0 -> A, 1 -> B, ..., 25 -> Z에 대응된다

    이후 계산해주니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0137
    {

        static void Main137(string[] args)
        {

            int[] square = new int[7];
            int[] digit = new int[7];
            {

                int calc = 1;
                for (int i = 0; i < 7; i++)
                {

                    square[i] = calc;

                    calc = calc * 26;
                }

                digit[0] = 1;
                for (int i = 1; i < 7; i++)
                {

                    digit[i] = digit[i - 1] + square[i];
                }
            }
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            StringBuilder sb = new StringBuilder(40);

            while (true)
            {
                
                int row = ReadInt(sr);
                int col = ReadInt(sr);

                if (col == 0) break;
                int calcIdx = 0;
                for (int i = 6; i >= 0; i--)
                {

                    if (col >= digit[i])
                    {

                        col -= digit[i];
                        calcIdx = i;
                        break;
                    }
                }

                for (int i = calcIdx; i >= 0; i--)
                {

                    int chk = col / square[i];
                    char add = (char)(chk + 'A');

                    sb.Append(add);

                    col -= chk * square[i];
                }

                sb.Append(row);

                sw.Write(sb.ToString());
                sw.Write('\n');
                sb.Clear();
            }

            sw.Close();
            sr.Close();
        }


        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n' && c != 'C')
            {

                if (c == '\r' || c == 'R') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
