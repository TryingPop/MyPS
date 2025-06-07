using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 동전
    문제번호 : 3363번

    구현, 브루트포스 문제다

    문제 입력이 아주 불친절하다
    scanf나 cin 같은 함수는 잘 구분해주는데,
    직접 입력받은 문자열로 구분지을러면 특수 처리가 필요하다;
*/

namespace BaekJoon.etc
{
    internal class etc_0299
    {

        static void Main299(string[] args)
        {

#if first
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            bool[] chk = new bool[3];

            int[][] calc = new int[3][];
            for (int i = 0; i < 3; i++)
            {

                calc[i] = new int[13];
            }

            int[] left = new int[13];
            int[] right = new int[13];
            for (int i = 0; i < 3; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);


                int l = 0;
                int r = 0;

                int op = 0;
                int opIdx = 0;
                for (int j = 0; j < temp.Length; j++)
                {

                    if (temp[j] == '>' || temp[j] == '<' || temp[j] == '=')
                    {

                        l = j - 1;
                        r = j + 1;
                        op = temp[j] - '0';
                        opIdx = j;
                        break;
                    }
                }

                int lidx = 0;
                for (int j = 0; j < opIdx; j++)
                {

                    left[j] = temp[j];
                    lidx++;
                }

                int ridx = 0;
                for (int j = r; j < temp.Length; j++)
                {

                    right[j - r] = temp[j];
                    ridx++;
                }

                bool isTrue = true;
                bool isL = true;
                if (op == '<' - '0') isTrue = false;
                else if (op == '>' - '0') 
                { 
                    
                    isTrue = false; 
                    isL = false; 
                }

                for (int j = 0; j < 4; j++)
                {

                    calc[i][left[j]] = isTrue ? 1 : isL ? 2 : 3;
                    calc[i][right[j]] = isTrue ? 1 : isL ? 3 : 2;
                }

                for (int j = 1; j < 13; j++)
                {

                    if (calc[i][j] != 0) continue;
                    calc[i][j] = isTrue ? 4 : 1;
                }
            }

            int ret = 0;
            int cnt = 0;
            bool isUp = true;
            for (int i = 1; i < 13; i++)
            {

                int b0 = calc[0][i];
                int b1 = calc[1][i];
                int b2 = calc[2][i];

                if (b0 == 1 || b1 == 1 || b2 == 1)
                {

                    calc[0][i] = 1;
                    calc[1][i] = 1;
                    calc[2][i] = 1;
                    cnt++;
                }
                else if ((b0 == 2 && b1 == 3) || (b1 == 2 && b2 == 3) || (b2 == 2 && b0 == 3)
                    || (b0 == 3 && b1 == 2) || (b1 == 3 && b2 == 2) || (b2 == 3 && b0 == 2))
                {

                    calc[0][i] = 1;
                    calc[1][i] = 1;
                    calc[2][i] = 1;
                    cnt++;
                } 

                if (b0 == 4 && b1 == 4 && b2 != 4)
                {

                    b0 = b2;
                    b1 = b2;
                }
                else if (b1 == 4 && b2 == 4 && b0 != 4)
                {

                    b1 = b0;
                    b2 = b0;
                }
                else if (b2 == 4 && b0 == 4 && b1 != 4)
                {

                    b2 = b1;
                    b0 = b1;
                }
                else if (b0 == 4 && b1 == b2 && b1 != 4)
                {

                    b0 = b1;
                }
                else if (b1 == 4 && b2 == b0 && b2 != 4)
                {

                    b1 = b2;
                }
                else if (b2 == 4 && b0 == b1 && b0 != 4)
                {

                    b2 = b0;
                }

                if (b0 == b1 && b1 == b2 && b1 != 4)
                {

                    if (ret != 0) ret = -1;
                    else ret = i;

                    if (b1 == 3) isUp = true;
                    else isUp = false;
                }
            }

            if (cnt == 12) Console.WriteLine("impossible");
            else if (ret == -1) Console.WriteLine("indefinite");
            else 
            { 
                
                Console.Write($"{ret}");
                if (isUp) Console.Write('+');
                else Console.Write('-');
            }

#endif

            string ZERO = "impossible";
            string TWO = "indefinite";

            int EQUAL = 1;
            int RIGHT = 2;
            int LEFT = 3;

            List<int>[] left, right;
            int[] op;

            Solve();
            void Solve()
            {

                Input();

                int ret1 = 0;
                bool ret2 = false;
                int cnt = 0;
                for (int i = 1; i <= 12; i++)
                {

                    if (IsCorrect(i, true)) 
                    { 
                        
                        if (cnt == 0)
                        {

                            ret1 = i;
                            ret2 = true;
                        }

                        cnt++; 
                    }

                    if (IsCorrect(i, false)) 
                    { 
                        
                        if (cnt == 0) ret1 = i;
                        cnt++;
                    }
                }

                if (cnt == 0) Console.Write(ZERO);
                else if (1 < cnt) Console.Write(TWO);
                else
                {

                    Console.Write(ret1);
                    Console.Write(ret2 ? '+' : '-');
                }
            }

            bool IsCorrect(int _num, bool _isUp)
            {

                for (int i = 0; i < 3; i++)
                {

                    int l = 0, r = 0;

                    for (int j = 0; j < left[i].Count; j++)
                    {

                        l += 2;
                        if (_num == left[i][j])
                        {

                            if (_isUp) l++;
                            else l--;
                        }
                    }
                    
                    for (int j = 0; j < right[i].Count; j++)
                    {


                        r += 2;
                        if (_num == right[i][j])
                        {

                            if (_isUp) r++;
                            else r--;
                        }
                    }

                    if (op[i] == EQUAL && l != r) return false;
                    else if (op[i] == LEFT && l <= r) return false;
                    else if (op[i] == RIGHT && l >= r) return false;
                }

                return true;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                op = new int[3];
                left = new List<int>[3];
                right = new List<int>[3];

                for (int i = 0; i < 3; i++)
                {

                    left[i] = new();
                    right[i] = new();

                    bool flag = false;
                    string chk = sr.ReadLine();
                    if (chk == null || chk == string.Empty) continue;
                    string[] temp = chk.Split();

                    for (int j = 0; j < temp.Length; j++)
                    {

                        if (temp[j] == null || temp[j] == string.Empty) continue;

                        if (temp[j][0] == '=')
                        {

                            op[i] = EQUAL;
                            flag = true;
                            continue;
                        }
                        else if (temp[j][0] == '<')
                        {

                            op[i] = RIGHT;
                            flag = true;
                            continue;
                        }
                        else if (temp[j][0] == '>')
                        {

                            op[i] = LEFT;
                            flag = true;
                            continue;
                        }

                        int num = Convert.ToInt32(temp[j]);
                        if (num <= 0) continue;

                        if (flag) right[i].Add(num);
                        else left[i].Add(num);
                    }
                }

                sr.Close();
            }
        }

#if first
        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool eof = false;
            while((c = _sr.Read()) != -1 && c != ' ')
            {

                if (c == '\r') continue;
                else if (c == '\n')
                {

                    eof = true;
                    break;
                }
                ret = ret * 10 + c - '0';
            }


            if (eof && ret == 0) return -1;
            return ret;
        }
#endif


#if other
// #include<stdio.h>
// #include<algorithm>
// #include<vector>
// #include<stack>
// #include<random>
using namespace std;
int a[3][4];
int c[3][4];
int b[3];
char xx[9];
int res[19];
int valid(int k, int isbig) {
	int i, j;
	for (i = 0; i < 3; i++) {
		int d = 0;
		for(j=0;j<4;j++){
			if (a[i][j] == k)d = 1;
			else if (c[i][j] == k)d = 2;
		}
		if (d == 0) {
			if (b[i] == 0)continue;
			else return false;
		}
		if (d == 1) {
			if (isbig) {
				if (b[i] == 1)continue;
				else return false;
			}
			else {
				if (b[i] == -1)continue;
				else return false;
			}
		}
		else {
			if (isbig) {
				if (b[i] == -1)continue;
				else return false;
			}
			else {
				if (b[i] == 1)continue;
				else return false;
			}
		}
	}
	return 1;
}
int main() {
	int i, j, k, l;
	for (i = 0; i < 3; i++) {
		for (j = 0; j < 4; j++) {
			scanf("%d", &a[i][j]);
		}
		scanf("%s", xx);
		if (xx[0] == '<')b[i] = -1;
		else if (xx[0] == '=')b[i] = 0;
		else b[i] = 1;
		for (j = 0; j < 4; j++) {
			scanf("%d", &c[i][j]);
		}
	}

	bool imp = true;
	for (i = 1; i <= 12; i++) {
		res[i] |= valid(i, 0) << 0;
		res[i] |= valid(i, 1) << 1;
		if (res[i])imp = false;
	}
	bool mult = false;
	int ri = -1;
	int rj = 0;
	for (i = 1; i <= 12; i++) {
		if (res[i]) {
			if (ri == -1) {
				ri = i;
				if (res[i] == 1)rj = 0;
				else if (res[i] == 2)rj = 1;
				else {
					mult = true;
				}
			}
			else {
				mult = true;
			}
		}
	}
	if (imp) { printf("impossible\n"); }
	else if (mult) { printf("indefinite\n"); }
	else {
		printf("%d%c\n", ri, "-+"[rj]);
	}
}
#endif
    }
}
