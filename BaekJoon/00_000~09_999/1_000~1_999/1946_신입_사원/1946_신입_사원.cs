using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 21
이름 : 배성훈
내용 : 신입 사원
    문제번호 : 1946번

    직접 구조체를 만들어 풀었다;
    그런데 해당 구조체를 직접 정의할 필요가 없었다

    처음에는 rank1로 확인하고 다음 rank2로 확인하는 2번 연산을해서 탈락자를 정했다
    그래서 936ms로 아슬아슬하게 통과했다

    다른사람의 풀이를 보고 생각해보니 불필요한 연산이었다!
    두 경우 실행결과가 동형인 상황이기에 변화가 없다

    그래서 rank2의 정렬을 빼니 724ms로 확 줄었다
*/

namespace BaekJoon.etc
{
    internal class etc_0075
    {

        static void Main75(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            int test = ReadInt(sr);
            Human[] humans = new Human[100_000];

            while(test-- > 0)
            {

                int len = ReadInt(sr);

                for (int i = 0; i < len; i++)
                {

                    int rank1 = ReadInt(sr);
                    int rank2 = ReadInt(sr);

                    humans[i].Set(rank1, rank2);
                }

                /*
                HeapSort(humans, len, true);

                int score = len;
                for (int i = 0; i < len; i++)
                {

                    int cur = humans[i].GetRank(false);
                    if (score < cur) humans[i].pass = false;
                    else score = cur;
                }
                */

                int ret = len;
                for (int i = 0; i < len; i++)
                {

                    if (!humans[i].pass) ret--;
                }

                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while ((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
        struct Human
        {

            private int rank1;
            private int rank2;

            public bool pass;

            public void Set(int _rank1, int _rank2)
            {

                rank1 = _rank1;
                rank2 = _rank2;

                pass = true;
            }

            public int GetRank(bool isOne)
            {

                if (isOne) return rank1;

                return rank2;
            }
        }

        static void HeapSort(Human[] _humans, int _len, bool _isOne)
        {

            for (int i = 1; i < _len; i++)
            {

                int cur = i;
                while(cur > 0)
                {

                    int p = (cur - 1) / 2;
                    if (_humans[p].GetRank(_isOne) < _humans[cur].GetRank(_isOne))
                    {

                        Swap(_humans, cur, p);
                        cur = p;
                    }
                    else break;
                }
            }

            Swap(_humans, 0, _len - 1);

            for (int i = _len - 2; i >= 1; i--)
            {

                int cur = 0;

                while (true)
                {

                    int l = cur * 2 + 1;
                    int r = cur * 2 + 2;

                    if (r <= i)
                    {

                        int curVal = _humans[cur].GetRank(_isOne);
                        int lVal = _humans[l].GetRank(_isOne);
                        int rVal = _humans[r].GetRank(_isOne);

                        if (lVal < rVal && curVal < rVal)
                        {

                            Swap(_humans, cur, r);
                            cur = r;
                        }
                        else if (curVal < lVal)
                        {

                            Swap(_humans, cur, l);
                            cur = l;
                        }
                        else break;
                    }
                    else if (l <= i)
                    {

                        if (_humans[cur].GetRank(_isOne) < _humans[l].GetRank(_isOne))
                        {

                            Swap(_humans, cur, l);
                            cur = l;
                        }
                        else break;
                    }
                    else break;
                }

                Swap(_humans, 0, i);
            }
        }

        static void Swap(Human[] _humans, int _idx1, int _idx2)
        {

            var temp = _humans[_idx1];
            _humans[_idx1] = _humans[_idx2];
            _humans[_idx2] = temp;
        }
    }
#if other
using System;
using System.IO;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
var tc = ScanInt();
var reportScores = new int[100000 + 1];
using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
while (tc-- > 0)
{
    var n = ScanInt();
    for (int i = 0; i < n; i++)
    {
        int a = ScanInt(), b = ScanInt();
        reportScores[a] = b;
    }
    var ret = 0;
    var minRank = int.MaxValue;
    for (int i = 1; i <= n; i++)
    {
        if (minRank > reportScores[i])
        {
            minRank = reportScores[i];
            ret++;
        }
    }
    sw.WriteLine(ret);
}

int ScanInt()
{
    int c, ret = 0;
    while (!((c = sr.Read()) is '\n' or ' ' or -1))
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        ret = 10 * ret + c - '0';
    }
    return ret;
}
#endif
}
