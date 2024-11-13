using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 13
이름 : 배성훈
내용 : Deletive Editing
    문제번호 : 25358번

    구현, 문자열 문제다.
    두 포인터를 써서 비교했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1109
    {

        static void Main1109(string[] args)
        {

            string YES = "YES\n";
            string NO = "NO\n";
            bool[] impo;
            Solve();
            void Solve()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int n = int.Parse(sr.ReadLine());
                impo = new bool[26];
                for (int i = 0; i < n; i++)
                {

                    string[] input = sr.ReadLine().Split();
                    sw.Write(GetRet(input[0], input[1]) ? YES : NO);
                }

                sr.Close();
                sw.Close();
            }

            bool GetRet(string _str, string _find)
            {

                Array.Fill(impo, false);

                int idx2 = _find.Length - 1;
                for (int idx1 = _str.Length - 1; idx1 >= 0; idx1--)
                {

                    if (_str[idx1] != _find[idx2]) impo[_str[idx1] - 'A'] = true;
                    else
                    {

                        if (impo[_str[idx1] - 'A']) return false;
                        idx2--;
                        if (idx2 == -1) break;
                    }
                }

                return idx2 == -1;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

int main(){
	ios_base::sync_with_stdio(false), cin.tie(nullptr);
	int N;
	cin >> N;
	for(int i = 0; i < N; i++){
		string S, T;
		cin >> S >> T;
		vector<int> freq(26, 0);
		for(char c : T) freq[c - 'A']++;
		reverse(T.begin(), T.end());
		reverse(S.begin(), S.end());
		string g;
		for(char c : S){
			if(freq[c - 'A'] > 0){
				g += c;
				freq[c - 'A']--;
			}
		}
		if(g == T){
			cout << "YES" << '\n';
		} else {
			cout << "NO" << '\n';
		}
	}
}

#endif
}
