using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 20
이름 : 배성훈
내용 : 침략자 진아
    문제번호 : 15812번

    브루트포스 문제다
    크기가 20 * 20인 보드이고, 최대 2개를 놓을 수 있으므로,
    400 * 399 * 400 = 160_000 * 399 = 64_000_000
*/

namespace BaekJoon.etc
{
    internal class etc_0147
    {

        static void Main147(string[] args)
        {

            StreamReader sr;

            int row, col;
            int[][] board;
            (int r, int c)[] poison;

            Solve();

            void Solve()
            {

                Input();

                int ret = DFS(0, 0, 0);

                Console.WriteLine(ret);
            }

            int DFS(int _r, int _c, int _depth)
            {

                int ret;
                if (_depth == 2) return Spread();

                ret = 10_000;
                for (int r = _r; r < row; r++)
                {

                    int s = _r == r ? _c : 0;
                    for (int c = s; c < col; c++)
                    {

                        if (board[r][c] == 1) continue;
                        poison[_depth] = (r, c);
                        int nextR = r;
                        int nextC = c + 1;
                        if (nextC == col)
                        {

                            nextC = 0;
                            nextR++;
                        }

                        int chk = DFS(nextR, nextC, _depth + 1);
                        ret = chk < ret ? chk : ret;
                    }
                }

                return ret;
            }

            int Spread()
            {

                int ret = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == 0) continue;
                        int dis1 = GetTaxiDis(r, c, poison[0].r, poison[0].c);
                        int dis2 = GetTaxiDis(r, c, poison[1].r, poison[1].c);

                        ret = Math.Max(ret, Math.Min(dis1, dis2));
                    }
                }

                return ret;
            }

            int GetTaxiDis(int _r1, int _c1, int _r2, int _c2)
            {

                int r = _r1 - _r2;
                r = r < 0 ? -r : r;

                int c = _c1 - _c2;
                c = c < 0 ? -c : c;

                return r + c;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = sr.Read() - '0';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                poison = new (int r, int c)[2];
                sr.Close();
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

#if other
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.StringTokenizer;

public class Main {
    public static void main(String[] arg) throws IOException {
        InputStreamReader isr = new InputStreamReader(System.in);
        BufferedReader br = new BufferedReader(isr);
        StringTokenizer st = new StringTokenizer(br.readLine());
        int N = Integer.parseInt(st.nextToken());
        int M = Integer.parseInt(st.nextToken());
        Invader invader = new Invader(N,M);
        int[][] map = new int[N][M];
        for(int i = 0; i < N; i++) {
            st = new StringTokenizer(br.readLine());
            char[] input = st.nextToken().toCharArray();
            for(int j = 0 ; j < M; j++) {
                map[i][j] = input[j]-'0';
            }
        }
        invader.createMap(map);
        System.out.print(invader.calcMinimum());
    }
}
class Invader {
    int[][] map;
    public Invader(int N,int M) {
        map = new int[N][M];
    }
    public void createMap(int[][] map) {
        this.map = map;
    }
    public int calcMinimum() {
        int minimumSecond = map[0].length + map.length - 2;
        for(int i = 0; i < map.length; i++) {
            for(int j = 0; j < map[i].length; j++) {
                if(map[i][j] == 0) {
                    for(int k = i; k < map.length; k++) {
                        for(int l = j+1; l <map[k].length; l++) {
                            if(map[k][l] == 0) {
                                int maxSecond = 0;
                                for(int m = 0; m < map.length; m++) {
                                    for (int n = 0; n < map[0].length; n++) {
                                        if (map[m][n] == 1) {
                                            if (Math.abs(m - i) + Math.abs(j - n) >= Math.abs(m - k) + Math.abs(l - n) && Math.abs(m - k) + Math.abs(l - n) > maxSecond) {
                                                maxSecond = Math.abs(m - k) + Math.abs(l - n);
                                            } else if (Math.abs(m - i) + Math.abs(j - n) <Math.abs(m - k) + Math.abs(l - n) && Math.abs(m - i) + Math.abs(j - n) > maxSecond) {
                                                maxSecond = Math.abs(m - i) + Math.abs(j - n);
                                            }
                                        }
                                    }
                                }
                                if(minimumSecond > maxSecond) {
                                    minimumSecond = maxSecond;
                                }
                            }
                        }
                    }
                }
            }
        }
        return minimumSecond;
    }
}

#elif other2
// #include <iostream>
// #include <vector>
// #include <queue>
using namespace std;

int N, M;
vector<pair<int, int>> houses;
vector<pair<int, int>> empty_spaces;

int get_l1_dist(const pair<int, int>& a, const pair<int, int>& b) {
    return abs(a.first - b.first) + abs(a.second - b.second);
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);

    cin >> N >> M;
    vector<string> map = vector<string>(N, "");

    for(int i=0; i < N; i++) {
        cin >> map[i];
        for(int j=0; j < M; j++) {
            if(map[i][j] == '1') {
                houses.push_back({i, j});
            } else {
                empty_spaces.push_back({i, j});
            }
        }
    }

    int min_dist = INT32_MAX;
    for(int i=0; i < static_cast<int>(empty_spaces.size()) - 1; i++) {
        for(int j=i + 1; j < static_cast<int>(empty_spaces.size()); j++) {
            int dist = 0;
            for(const auto& house: houses) {
                dist = max(dist, min(get_l1_dist(house, empty_spaces[i]), get_l1_dist(house, empty_spaces[j])));
                if(dist > min_dist) {
                    break;
                }
            }
            min_dist = min(min_dist, dist);
        }
    }
    cout << min_dist << '\n';
}
#endif
}
