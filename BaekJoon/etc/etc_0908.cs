using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 25
이름 : 배성훈
내용 : 전구와 스위치
    문제번호 : 2138번

    그리디 알고리즘 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0908
    {

        static void Main908(string[] args)
        {

            StreamReader sr;

            int n;
            bool[] bulb;

            Solve();
            void Solve()
            {

                Input();

                Find();
            }

            void Find()
            {

                bool[] calc = new bool[n];

                Copy(bulb, calc);

                int ret = GetRet(calc);

                Copy(bulb, calc);
                Switch(calc, 0);

                ret = Math.Min(ret, GetRet(calc) + 1);

                if (ret == 123_456) ret = -1;

                Console.Write(ret);
            }

            void Switch(bool[] _arr, int _i)
            {

                if (0 < _i) _arr[_i - 1] = !_arr[_i - 1];
                _arr[_i] = !_arr[_i];

                if (_i < n - 1) _arr[_i + 1] = !_arr[_i + 1];
            }

            int GetRet(bool[] _calc)
            {

                int ret = 0;
                for (int i = 1; i < n; i++)
                {

                    if (_calc[i - 1]) continue;
                    Switch(_calc, i);
                    ret++;
                }

                if (!_calc[n - 1]) ret = 123_456;
                return ret;
            }

            void Copy(bool[] _src, bool[] _dst)
            {

                for (int i = 0; i < n; i++)
                {

                    _dst[i] = _src[i];
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                bulb = new bool[n];
                for (int i = 0; i < n; i++)
                {

                    bool chk = sr.Read() == '1';
                    bulb[i] = chk;
                }

                if (sr.Read() == '\r') sr.Read();

                for (int i = 0; i < n; i++)
                {

                    bool chk = sr.Read() == '1';
                    bulb[i] = chk == bulb[i];
                }

                sr.Close();
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

#if other
// #include<iostream>
using namespace std;
// #define MAX 100001
int main(){
	ios::sync_with_stdio(false);
	cin.tie(0);
	cout.tie(0);
	int N;
	char A[100001], B[100001], C[100001];
	cin>>N>>A>>B;
	int ans=MAX;
	for(int k=0;k<2;++k){
		int cnt=0;
		for(int i=0;i<N;++i)	C[i]=A[i];
		C[0]=k+'0';
		if(C[0]!=A[0]){
			C[1]=C[1]=='1'?'0':'1';
			++cnt;
		}
		for(int i=1;i<N;++i){
			if(B[i-1]!=C[i-1]){
				C[i-1]=C[i-1]=='1'?'0':'1';
				C[i]=C[i]=='1'?'0':'1';
				C[i+1]=C[i+1]=='1'?'0':'1';
				++cnt;
			}
		}
		if(B[N-1]==C[N-1]){
			ans=cnt<ans?cnt:ans;
		}
	}
	if(ans==MAX)	cout<<"-1\n";
	else	cout<<ans<<"\n";
	return 0;
}
#endif
}
