# C#

## 난이도 : 실버 4

## 알고리즘 분류
  - 구현
  - 시뮬레이션

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
Musical chairs is a game frequently played at children's parties. Players are seated in a circle facing outwards. When the music starts, the players have to stand up and move clockwise round the chairs. One chair is removed, and when the music stops the players all have to try to sit down on one of the chairs. The player who does not manage to sit down is out, and the game continues until there is just one player left, who is the winner.<br/>


## 입력
The first line contains a single integer, N which is the number of players (1 < N <= 15). The next N lines have the names of the players. The length of player's name does not exceed 10.<br/>
The next line contains R, an integer which tells how many rounds are to be processed (0 < R < N). The next R lines each contain a pair of integers S and M, separated by a space. S is the number of the seat to be removed. Seats are numbered from 1 to the number of seats remaining in a clockwise direction.<br/>
M is the number of moves made before the music stops (0 < M <= 30). A move takes a player from one seat to the next in a clockwise direction. A move from the highest seat number takes a player to seat 1.<br/>


## 출력
After each round has taken place, output a line<br/>

	<name> has been eliminated.

where \<name\> is the name of the person who does not find a seat. This will be the person who would have ended up at the seat which was removed.<br/>
At the end of the specified number of moves, output a line which either says<br/>

	<name> has won.

where a single player remains, or<br/>

	Players left are <name list>.

where the game is not yet finished.<br/>
\<name list\> contains the name of each player not yet eliminated in the same order as in the input. The names are separated by spaces.<br/>


## 예제 입력
5<br/>
Anne<br/>
Bill<br/>
Chen<br/>
Di<br/>
Everet<br/>
4<br/>
3 6<br/>
2 8<br/>
1 5<br/>
2 6<br/>


## 예제 출력
Bill has been eliminated.<br/>
Anne has been eliminated.<br/>
Chen has been eliminated.<br/>
Everet has been eliminated.<br/>
Di has won.<br/>


## 풀이
N이 15이고, M이 30이므로 매번 의자 제거 게임을 배열을 회전하며 시뮬레이션 돌려도 N^2 x M = 225 x 30 < 300 x 30 = 9000이다.<br/>
시뮬레이션 방법이 유효하다.<br/>


이에 시뮬레이션 돌리면서 의자를 제거하고,빠지는 사람을 추가했다.<br/>
주의할 것은 의자가 제거되면 번호 갱신이 기존의 가장 낮은것부터 1번부터 1씩 증가한 인덱스로 갱신된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/15183