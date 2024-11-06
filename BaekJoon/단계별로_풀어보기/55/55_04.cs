using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 16
이름 : 배성훈
내용 : 르모앙의 추측
    문제번호 : 17134번

    수학, 고속 푸리에 변환 문제다
    50만 이상에서 소수 확인없을 안해서 2번 틀렸다;
    이외에는 이상없이 통과했다

    아이디어는 다음과 같다
    에라토스테네스 체 이론으로 소수를 모두 찾는다
    그리고 홀수인 소수만 x에 넣고
    짝수인 세미소수를 y에 넣는다
    배열에 넣을 때 존재하면 1 + 0i을 없으면 0 + 0i으로 기록한다
    그리고 100만 범위까지기에 100만을 넘어서는 수는 확인하지 않는다

    이후 x, y를 곱해주면 해당 차수의 실수 부분의 항은 우리가 찾는 경우의 수가 된다
    이렇게 구한 뒤 숫자 입력을 받아 실수부분만 제출하니 이상없이 통과된다
*/
namespace BaekJoon._55
{
    internal class _55_04
    {

        static void Main4(string[] args)
        {

            Complex ONE = new Complex(1.0, 0.0);

            StreamReader sr;
            StreamWriter sw;

            Complex[] x, y;
            bool[] notPrime;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while (test-- > 0)
                {

                    int idx = ReadInt();
                    int ret = (int)Math.Round(x[idx].Real);

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                SetArr();

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                MyMultiply();
            }


            void SetArr()
            {

                notPrime = new bool[1_000_000];
                x = new Complex[1 << 21];
                y = new Complex[1 << 21];

                for (int i = 2; i < 1_000_000; i++)
                {

                    if (notPrime[i]) continue;

                    for (int j = i + i; j < 1_000_000; j += i)
                    {

                        notPrime[j] = true;
                    }
                }

                y[4] = ONE;
                for (int i = 3; i <= 500_000; i++)
                {

                    if (notPrime[i]) continue;

                    y[2 * i] = ONE;
                    x[i] = ONE;
                }

                for (int i = 500_001; i < 1_000_000; i++)
                {

                    if (notPrime[i]) continue;
                    x[i] = ONE;
                }
            }

            void FFT(Complex[] _arr, bool _inv)
            {

                int n = _arr.Length;
                Complex temp;
                for (int i = 1, j = 0; i < n; i++)
                {

                    int bit = (n >> 1);

                    while (((j ^= bit) & bit) == 0)
                    {

                        bit >>= 1;
                    }

                    if (i < j)
                    {

                        temp = _arr[i];
                        _arr[i] = _arr[j];
                        _arr[j] = temp;
                    }
                }

                Complex p, w;
                for (int i = 1; i < n; i <<= 1)
                {

                    double x = _inv ? Math.PI / i : -Math.PI / i;

                    w = new Complex(Math.Cos(x), Math.Sin(x));

                    for (int j = 0; j < n; j += (i << 1))
                    {

                        p = ONE;

                        for (int k = 0; k < i; k++)
                        {

                            temp = _arr[i + j + k] * p;
                            _arr[i + j + k] = _arr[j + k] - temp;
                            _arr[j + k] += temp;
                            p *= w;
                        }
                    }
                }

                if (_inv)
                {

                    for (int i = 0; i < n; i++)
                    {

                        _arr[i] /= n;
                    }
                }
            }

            void MyMultiply()
            {

                FFT(x, false);
                FFT(y, false);

                for (int i = 0; i < x.Length; i++)
                {

                    x[i] = x[i] * y[i];
                }

                FFT(x, true);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
