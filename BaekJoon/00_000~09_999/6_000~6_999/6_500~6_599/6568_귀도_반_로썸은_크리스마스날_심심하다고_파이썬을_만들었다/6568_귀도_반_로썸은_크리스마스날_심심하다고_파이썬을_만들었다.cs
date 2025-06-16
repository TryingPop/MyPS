using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 16
이름 : 배성훈
내용 : 귀도 반 로썸은 크리스마스날 심심하다고 파이썬을 만들었다
    문제번호 : 6568번

    구현, 시뮬레이션 문제다.
    CS 사전 지식이 필요하다;
    처음 32개의 입력은 메모리에 저장되는 값이다.

    그리고 0번 메모리를 읽으며 조건대로 진행해 가는데,
    프로그램 탈출 코드가 나올 때까지 진행한다.

    그리고나서 acc의 값을 출력하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1705
    {

        static void Main1705(string[] args)
        {

            int EOF = -1;

            int NUM_OP = 0b_111_00000;
            int NUM_VAL = 0b_000_11111;

            int NUM_MAX_PC = 1 << 5;
            int NUM_MAX_ACC = 1 << 8;

            const int OP_STA = 0b_000_00000;
            const int OP_LDA = 0b_001_00000;
            const int OP_BEQ = 0b_010_00000;
            const int OP_NOP = 0b_011_00000;
            const int OP_DEC = 0b_100_00000;
            const int OP_INC = 0b_101_00000;
            const int OP_JMP = 0b_110_00000;
            const int OP_HLT = 0b_111_00000;

            
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] mem = new int[NUM_MAX_PC];

            while (Input())
            {

                GetRet();
            }

            void GetRet()
            {

                bool flag = true;
                int pc = 0;
                int acc = 0;
                int op;

                while (flag)
                {

                    Active();
                }

                for (int i = 8 - 1; i >= 0; i--)
                {

                    bool ret = (acc & (1 << i)) != 0;
                    sw.Write(ret ? 1 : 0);
                }

                sw.Write('\n');

                void Active()
                {

                    op = mem[pc++];
                    if (pc < 0) pc += NUM_MAX_PC;
                    else if (NUM_MAX_PC == pc) pc -= NUM_MAX_PC;

                    switch (op & NUM_OP)
                    {

                        case OP_STA:
                            STA();
                            break;

                        case OP_LDA:
                            LDA();
                            break;

                        case OP_BEQ:
                            BEQ();
                            break;

                        case OP_NOP:
                            NOP();
                            break;

                        case OP_DEC:
                            DEC();
                            break;

                        case OP_INC:
                            INC();
                            break;

                        case OP_JMP:
                            JMP();
                            break;

                        case OP_HLT:
                            HLT();
                            break;
                    }
                }

                void STA()
                {

                    mem[op & NUM_VAL] = acc;
                }

                void LDA()
                {

                    acc = mem[op & NUM_VAL];
                }

                void BEQ()
                {

                    pc = acc == 0 ? op & NUM_VAL : pc;
                }

                void NOP() { }

                void DEC()
                {

                    acc--;
                    if (acc < 0) acc += NUM_MAX_ACC;
                }

                void INC()
                {

                    acc++;
                    if (acc == NUM_MAX_ACC) acc = 0;
                }

                void JMP()
                {

                    pc = op & NUM_VAL;
                }

                void HLT()
                {

                    flag = false;
                    return;
                }
            }

            bool Input()
            {

                for (int i = 0; i < 32; i++)
                {

                    int input = ReadOp();
                    if (input == EOF) return false;

                    mem[i] = input;
                }

                return true;
            }

            int ReadOp()
            {

                int ret = 0;

                while (TryReadOp()) ;

                return ret;

                bool TryReadOp()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    else if (c == EOF || c == '.')
                    {

                        ret = EOF;
                        return false;
                    }
                    ret = c - '0';

                    for (int i = 1; i < 8; i++)
                    {

                        ret = (ret << 1) | (sr.Read() - '0');
                    }

                    return false;
                }
            }
        }
    }
}
