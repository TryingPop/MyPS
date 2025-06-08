using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 22
이름 : 배성훈
내용 : 전공책
    문제번호 : 16508번

    처음에 찾는 문자열에 AAA처럼 중복되는 언어가 없는 줄 알았다
    그래서 비트마스킹으로 접근했고 14% 쯤에서 틀렸다;

    이후 중복이 있기에 int[] 형으로 바꿨고,
    DFS 탐색으로 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0078
    {

        static void Main78(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 중복되는 문자열을 입력 받을 수 있어
            // 카운팅 형식으로 넣는다
            int[] chk = new int[26];
            ReadString(sr, chk);

            int len = ReadInt(sr);
            int[] val = new int[len];
            int[][] same = new int[len][];

            for (int i = 0; i < len; i++)
            {

                same[i] = new int[26];
                val[i] = ReadInt(sr);
                ReadString(sr, same[i]);
            }

            sr.Close();

            int ret = 2_000_000;

            DFS(val, same, chk, 0, 0, ref ret);

            ret = ret == 2_000_000 ? -1 : ret;
            Console.WriteLine(ret);
        }

        static void DFS(int[] _val, int[][] _same, int[] _chk, int _curVal, int _idx, ref int _ret)
        {

            // 완료 확인
            bool success = true;

            for (int i = 0; i < 26; i++)
            {

                if (_chk[i] <= 0) continue;

                success = false;
                break;
            }

            if (success)
            {

                if (_curVal < _ret) _ret = _curVal;
                return;
            }
            else if (_idx == _val.Length)
            {

                return;
            }

            for (int i = _idx; i < _val.Length; i++)
            {

                for (int j = 0; j < 26; j++)
                {

                    _chk[j] -= _same[i][j];
                }
                DFS(_val, _same, _chk, _curVal + _val[i], i + 1, ref _ret);

                for (int j = 0; j < 26; j++)
                {

                    _chk[j] += _same[i][j];
                }
            }
        }

        static void ReadString(StreamReader _sr, int[] _board)
        {

            int c;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                int calc = (c - 'A');
                _board[calc]++;
            }
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
