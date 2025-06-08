using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 23
이름 : 배성훈
내용 : CPU
    문제번호 : 16506번

    구현 문제다
    하드코딩으로 풀었다
    다 풀고나니 여러 메서드를 써서 각각 표현했는데,
    메서드의 양을 줄일 수 있어 보인다
    예를들어, rd, ra, rb 숫자를 입력 받는 경우 시작 번호와 길이, 
    그리고 변환할 숫자를 넘기면 될거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0338
    {

        static void Main338(string[] args)
        {

            Dictionary<string, int> opCode = new(23);
            opCode["ADD"] = 0b_0000_00;
            opCode["ADDC"] = 0b_0000_10;
            opCode["SUB"] = 0b_0001_00;
            opCode["SUBC"] = 0b_0001_10;
            opCode["MOV"] = 0b_0010_00;
            opCode["MOVC"] = 0b_0010_10;
            opCode["AND"] = 0b_0011_00;
            opCode["ANDC"] = 0b_0011_10;
            opCode["OR"] = 0b_0100_00;
            opCode["ORC"] = 0b_0100_10;
            opCode["NOT"] = 0b_0101_00;
            opCode["MULT"] = 0b_0110_00;
            opCode["MULTC"] = 0b_0110_10;
            opCode["LSFTL"] = 0b_0111_00;
            opCode["LSFTLC"] = 0b_0111_10;
            opCode["LSFTR"] = 0b_1000_00;
            opCode["LSFTRC"] = 0b_1000_10;
            opCode["ASFTR"] = 0b_1001_00;
            opCode["ASFTRC"] = 0b_1001_10;
            opCode["RL"] = 0b_1010_00;
            opCode["RLC"] = 0b_1010_10;
            opCode["RR"] = 0b_1011_00;
            opCode["RRC"] = 0b_1011_10;

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            char[] ret = new char[16];
            int test = int.Parse(sr.ReadLine());

            while(test-- > 0)
            {

                string[] temp = sr.ReadLine().Split(' ');

                RecordOpCode(temp[0]);
                RecordRD(int.Parse(temp[1]));
                if (IsRAZero(temp[0])) RecordRAZero();
                else RecordRA(int.Parse(temp[2]));

                if (IsRBR(temp[0])) RecordRBR(int.Parse(temp[3]));
                else RecordRB(int.Parse(temp[3]));

                for (int i = 0; i < ret.Length; i++)
                {

                    sw.Write(ret[i]);
                }
                sw.Write('\n');
            }

            sr.Close();
            sw.Close();
            void RecordOpCode(string _op)
            {

                if (!opCode.ContainsKey(_op)) return;

                int code = opCode[_op];
                for (int i = 0; i < 6; i++)
                {

                    if ((code & (1 << (5 - i))) != 0) ret[i] = '1';
                    else ret[i] = '0';
                }
            }

            void RecordRD(int _n)
            {

                for (int i = 2; i >= 0; i--)
                {

                    if ((_n & (1 << i)) != 0) ret[8 - i] = '1';
                    else ret[8 - i] = '0';
                }
            }

            bool IsRAZero(string _op)
            {

                if (_op == "NOT" || _op == "MOV" || _op == "MOVC") return true;
                return false;
            }

            void RecordRAZero()
            {

                ret[9] = '0';
                ret[10] = '0';
                ret[11] = '0';
            }

            void RecordRA(int _n)
            {

                for (int i = 2; i >= 0; i--)
                {

                    ret[11 - i] = (_n & (1 << i)) != 0 ? '1' : '0';
                }
            }

            bool IsRBR(string _op)
            {

                if (_op[^1] == 'C') return false;
                return true;
            }

            void RecordRBR(int _n)
            {

                for (int i = 2; i >= 0; i--)
                {

                    ret[14 - i] = (_n & (1 << i)) != 0 ? '1' : '0';
                }
                ret[15] = '0';
            }

            void RecordRB(int _n)
            {

                for (int i = 3; i >= 0; i--)
                {

                    ret[15 - i] = (_n & (1 << i)) != 0 ? '1' : '0';
                }
            }
        }
    }
}
