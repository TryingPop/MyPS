using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 28
이름 : 배성훈
내용 : 사냥꾼
    문제번호 : 8983번

    정렬, 이분탐색 문제다
    동물이 위험 범위 안인지 판별해야한다
    동물 좌표를 x, y라하고 사로 x 위치를 a, 사거리를 k라하면
    x + y - k <= a <= x - y + k
    인 a이면 동물은 위험범위에 있다

    그래서 사로를 정렬하고 x + y - k보다 이상인 가장 작은 사로를 찾아
    x + y - k 이하인지 확인하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0846
    {

        static void Main846(string[] args)
        {

            int INF = 2_000_000_001;
            StreamReader sr;

            int n, m, k;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                for (int i = 0; i < m; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    if (k - y < 0) continue;
                    int idx = BinarySearch(x + y - k);
                    if (arr[idx] <= x - y + k) ret++;
                }

                sr.Close();
                Console.Write(ret);
            }

            int BinarySearch(int _n)
            {

                int l = 0;
                int r = n + 1;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (arr[mid] < _n) l = mid + 1;
                    else r = mid - 1;
                }

                return r + 1;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                arr = new int[n + 2];
                arr[n + 1] = INF;
                arr[0] = -INF;
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                }

                Array.Sort(arr);
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
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '),int.Parse);
            int m = arr[0]; int n = arr[1]; int l = arr[2];
            int[] gun = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            Array.Sort(gun);
            List<(int, int)> anim = new();
            for(int i = 0; i < n; i++)
            {
                int[] temp = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
                anim.Add((temp[0], temp[1]));
            }
            int answer = 0;
            for(int i = 0; i < n; i++)
            {
                int left = 0;
                int right = m;
                while(left < right)
                {
                    int mid = (left + right) / 2;

                    long diff = anim[i].Item1 - gun[mid];
                    long length = (long)Math.Abs(anim[i].Item1 - gun[mid]) + anim[i].Item2;
                    if(length <= l)
                    {
                        answer++;
                        break;
                    }
                    if (diff <= 0)
                        right = mid;
                    else
                        left = mid + 1;
                }
            }

            output.Write(answer);

            input.Close();
            output.Close();
        }
    }
}
#elif other2
// #include <unistd.h>
// #include <sys/stat.h>
// #include <sys/mman.h>
// #include <algorithm>
using namespace std;

int main() {
    struct stat st; fstat(0, &st);
	char w[6], *p = (char*)mmap(0, st.st_size, PROT_READ, MAP_SHARED, 0, 0);
	auto ReadInt = [&]() {
		int ret = 0;
		for (char c = *p++; c & 16; ret = 10 * ret + (c & 15), c = *p++);
		return ret;
	};

    int n = ReadInt(), m = ReadInt(), k = ReadInt(), v[n], cnt = 0;
	for (int i = 0; i < n; i++) v[i] = ReadInt(); sort(v, v + n);
	while (m--) {
		int x = ReadInt(), y = ReadInt(), mn = 1e9;
		if (y > k) continue;
		auto it = lower_bound(v, v + n, x) - v;
        for (int i = it - 1; i <= it; i++) {
            if (i < 0 || i >= n) continue;
            mn = min(mn, abs(v[i] - x));
        }
		if (mn <= k - y) cnt++;
	}

    int sz = 1, t = cnt;
    while (t >= 10) sz++, t /= 10;
    for (int j = sz; j --> 0; cnt /= 10) w[j] = cnt % 10 | 48;
    write(1, w, sz);
}
#endif
}
