using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 30
이름 : 배성훈
내용 : 두 수의 합
    문제번호 : 9024번

    정렬, 이분탐색, 두 포인터 문제다
    서로 합하는 수 들이 다른 수이기에 두 포인터로 찾을 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0779
    {

        static void Main779(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, k;
            int[] arr;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while (test-- > 0)
                {

                    Input();

                    sw.Write($"{GetRet()}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                n = ReadInt();
                k = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }
            }

            int GetRet()
            {

                Array.Sort(arr, 0, n);

                int l = 0;
                int r = n - 1;
                int ret = 0;
                int min = 200_000_000;

                while (l < r)
                {

                    int cur = arr[l] + arr[r] - k;
                    int chk = cur < 0 ? -cur : cur;

                    if (chk < min)
                    {

                        ret = 1;
                        min = chk;
                    }
                    else if (chk == min) ret++;

                    if (cur <= 0) l++;
                    else r--;
                }

                return ret;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                arr = new int[1_000_000];
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
#if other
// #include <iostream>
// #include <algorithm>
// #include <cmath>
// #define MAX 200000001
using namespace std;

int main() {
	cin.tie(NULL);
	cout.tie(NULL);
	ios_base::sync_with_stdio(false);
	int Tc;
	cin >> Tc;
	for (int i = 0; i < Tc; i++){

		int Nums, Target, Count = 0;
		cin >> Nums >> Target;
		int* NumArray = new int[Nums];
		//수 입력받기
		for (int j = 0; j < Nums; j++){
			cin >> NumArray[j];
		}

		//오름차순 정렬
		sort(NumArray, NumArray + Nums);

		//찾기
		int Gap = MAX, Front = 0, Back = Nums - 1;
		while (Front < Back){
			//두 수의 합 계산
			int TempGap = NumArray[Front] + NumArray[Back];
			//해당 숫자에 가까운지 확인
			if (TempGap > Target){
				Back--;
			}
			else if (TempGap < Target) {
				Front++;
			}
			//같을 경우
			else{
				Front++;
				Back--;
			}
			//가까운 값
			if (abs(Target - (TempGap)) < Gap) {
				//다르면 가까운 값 갱신 
				Gap = abs(Target - (TempGap));
				Count = 1;
			}
			//같으면 가까운 값에 대한 조합 추가
			else if (abs(Target - (TempGap)) == Gap){
				Count++;
			}
		}
		cout << Count <<"\n";
	}
	return 0;
}
#endif
}
