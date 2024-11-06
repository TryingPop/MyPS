using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 30
이름 : 배성훈
내용 : 문자열 잘라내기
    문제번호 : 2866번

    문자열, 이분 탐색, 해시 문제다
    처음엔 그냥 크기별로 문자열을 짤라서 넣으면 되지 않을까 생각하고
    시도했다가 엄청난 string 생산의 메모리 초과로 터졌다

    이후 문자열 해싱을 직접 구현하면 되지 않을까 생각하고 구현하니
    해시 충돌로 여러 번 틀렸다;

    이후 고민하니 모두 넣는거보다 
    필요한 길이만 넣으면 되지 않을까 생각하고,
    필요한 길이만큼만 넣어 해싱을 하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0927
    {

        static void Main927(string[] args)
        {

            StreamReader sr;

            int row, col;
            char[][] strs;
            HashSet<int> chk;
            char[] str;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            int BinarySearch()
            {

                int l = 0;
                int r = row;
                int ret = row;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (ChkContains(mid)) l = mid + 1;
                    else 
                    {

                        ret = mid;
                        r = mid - 1; 
                    }
                }

                return ret;
            }

            bool ChkContains(int _len)
            {

                chk.Clear();

                for (int c = 0; c < col; c++)
                {

                    int len = 0;

                    for (int i = 0; i < _len; i++)
                    {

                        int r = row - 1 - i;
                        str[len++] = strs[r][c];
                    }

                    int temp = GetMyHashCode(len);

                    if (chk.Contains(temp)) return true;
                    chk.Add(temp);
                }

                return false;
            }

            void GetRet()
            {

                chk = new(col);
                str = new char[row];

                int ret = BinarySearch();

                Console.Write(row - ret);
            }

            int GetMyHashCode(int _len)
            {

                int num = 5381;
                int num2 = 5381;
                int num3;

                int i = 0;

                while(i < _len)
                {

                    num3 = str[i];
                    num = ((num << 5) + num) ^ num3;

                    if (i + 1 == _len) break;

                    i++;
                    num3 = str[i];
                    num2 = ((num2 << 5) + num2) ^ num3;
                }

                return num + num2 * 1566083941;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                strs = new char[row][];
                for (int r = 0; r < row; r++)
                {

                    strs[r] = new char[col];

                    for (int c = 0; c < col; c++)
                    {

                        strs[r][c] = (char)sr.Read();
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System.Text;
StreamReader sr = new StreamReader(Console.OpenStandardInput());
int[] inputs = sr.ReadLine().Split().Select(int.Parse).ToArray();
int wordCount = inputs[0];
string input;
StringBuilder[] sb = new StringBuilder[inputs[1]];
for (int i = 0; i < inputs[1]; i++) sb[i] = new StringBuilder();
while (inputs[0]-- > 0)
{
    input = sr.ReadLine();
    for (int j = 0; j < input.Length; j++) sb[j].Append(input[j]);
}
sr.Close();
var words = sb.Select(n => n.ToString()).ToArray();
int count = 0;
for (int i = 0; i < wordCount; i++)
{
    if (words.Select(n => n.Substring(i)).Distinct().Count() != words.Length) break;
    count++;
}
Console.WriteLine(count > 0 ? count -1 : 0 );
#elif other2
// #pragma GCC optimize("O3")
// #pragma GCC target("arch=haswell")
// #include <unistd.h>
// #include <sys/mman.h>
// #include <sys/stat.h>
// #include <stdbool.h>
// #include <string.h>
// #define WSIZE 4

char strings[1000][1001]; // Global variables and static variables are automatically initialized to zero.
int hash[1000][1001];
int length;
int size;
char * rbuf;
int rpos = 0;

int binary_search(void);
bool duplicate(int mid);
int read_int(void);
void write_int(int n);

int main(void) {
    struct stat sb;
    fstat(0, &sb);
    rbuf = (char *)mmap(NULL, sb.st_size, PROT_READ, MAP_PRIVATE, 0, 0);
    int R = read_int();
    int C = read_int();
    char (* table)[C + 1] = (char (*)[C + 1])(rbuf + rpos);
    int i, j;
    for (i = 0; i < C; i++) {
        for (j = R - 1; j >= 0; j--) {
            char ch = table[j][i];
            strings[i][j] = ch;
            hash[i][j] = hash[i][j + 1] * 31 + ch;
        }
    }
    length = R;
    size = C;
    int count = binary_search();
    write_int(count);
    munmap(rbuf, sb.st_size);
    return 0;
}

int binary_search(void) {
    int left = 0;
    int right = length;
    while (right - left >= 2) {
        int mid = left + (right - left) / 2;
        if (duplicate(mid))
            right = mid;
        else
            left = mid;
    }
    return right - 1;
}

bool duplicate(int mid) {
    for (int i = 0; i < size - 1; i++) {
        for (int j = i + 1; j < size; j++) {
            if (hash[i][mid] == hash[j][mid] && strcmp(&strings[i][mid], &strings[j][mid]) == 0) {
                return true;
            }
        }
    }
    return false;
}

int read_int(void) {
    int n = 0;
    char c;
    while ((c = rbuf[rpos++]) != ' ' && c != '\n')
        n = (n << 3) + (n << 1) + c - '0';
    return n;
}

void write_int(int n) {
    char wbuf[WSIZE] = {[WSIZE - 1] = '\n'};
    int wpos = WSIZE - 2;
    while (true) {
        wbuf[wpos] = (char)(n % 10 + '0');
        n /= 10;
        if (n == 0) break;
        wpos--;
    }
    write(STDOUT_FILENO, wbuf + wpos, WSIZE - wpos);
}
#elif other3
// #include <algorithm>
// #include <string>
// #include <iostream>
// #include <unordered_set>

using namespace std;

int R, C;
string arr[1000];
unsigned long long hashval[1000][1000];

void fillHash()
{
	for(int col = 0; col < C; col++)
	{
		unsigned long long hash = 5381;
		for(int row = R - 1; row >= 0; row--)
		{
			hashval[row][col] = hash = ((hash << 5) + hash) + arr[row][col];
		}
	}
}

int solve()
{
	fillHash();
	int ans = 0;
	for(int row = 1; row < R; row++)
	{
		unordered_set<unsigned long long> hashTable;
		for(int col = 0; col < C; col++)
		{
			if(hashTable.find(hashval[row][col]) != hashTable.end())
			{
				return ans;
			}
				
			else
				hashTable.insert(hashval[row][col]);
		}
		ans++;
	}
	return ans;
}

int main() {
	ios_base::sync_with_stdio(0);
	cin.tie(0); cout.tie(0);

	cin >> R >> C;
	for(int i = 0; i < R; i++)
		cin >> arr[i];

	cout << solve();
	return 0;
}
#elif other4
// #include<iostream>
// #include<vector>
// #include<deque>
// #include<algorithm>
// #include<string>
// #include<cstring>
// #include<set>
// #include<cmath>
// #include<queue>
// #include<stack>
// #include<tuple>
// #include<sstream>
// #include<unordered_set>
// #include<unordered_map>
using namespace std;
const int intmax = 2147483647;
int dx[] = { 1,0,-1,0 ,1,1,-1,-1 };
int dy[] = { 0,1,0,-1 ,1,-1,1,-1 };
// #define ll long long


int main() {

	ios_base::sync_with_stdio(false);
	cin.tie(NULL);
	cout.tie(NULL);

	int R, C;
	cin >> R >> C;

	vector<string> str(R);
	for (int i = 0; i < R; i++) {
		cin >> str[i];
	}

	vector<string> s(C);
	for (int i = 0; i < C; i++) {
		for (int j = R - 1; j >= 0; j--) {
			s[i].push_back(str[j][i]);
		}
	}
	sort(s.begin(), s.end());

	int count = R;
	for (int i = 1; i < C; i++) {
		for (int j = 0; j < R; j++) {
			if (s[i][j] != s[i - 1][j]) {
				count = min(count, R - j - 1);
				break;
			}
		}
	}
	cout << count;

}

#endif

}
