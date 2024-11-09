# C#

## 난이도 : 골드 1

## 알고리즘 분류
  - 구현
  - 자료 구조
  - 시뮬레이션
  - 분리 집합

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 512 MB

## 문제
Alice and Bob are playing a generalized version of Connect Four. In their game, the board consists of w columns of height h and the goal is to be the first player to complete a row of k tiles of equal colour, either vertically, horizontally or diagonally. The two players alternate dropping their tiles into one of the columns, with Alice using red tiles and going first and Bob using yellow tiles and going second. Once a tile is dropped, it falls down to the bottommost available position, making that position no longer available. Once a column has h tiles in it, it becomes full and the players can no longer drop their tiles there.<br/>
Figure G.1: Visualisation of the sample cases, showing the state of the game after 0, 3, 8 and 12 moves, respectively. Alice's tiles are shown in red, Bob's tiles in yellow.<br/>
As Alice and Bob found it quite challenging to keep track of the winning condition, they just kept playing until the board was completely filled with tiles. They recorded a log of the moves they made and asked you to tell them who won the game, and on what turn they did so. If neither player managed to complete a row, the game ends in a draw, so report that instead.<br/>

## 입력
The input consists of:<br/>

  - One line with three integers h, w and k (h, w ≥ 1, h x w ≤ 250,000, 1 ≤ k ≤ max(h,w)). The columns are numbered from 1 to w.
  - One line with h x w integers a_1,...,a_{h x w} (1 ≤ a_i ≤ w for each i), where a_i is the index of the column that the ith tile was dropped in. Odd indices correspond to Alice's moves and even indices correspond to Bob's moves. Each column appears exactly h times in this list.

## 출력
Output the winner of the game (A for Alice or B for Bob), followed by the number of moves needed to decide the winner. If the game ends in a draw, output D instead.<br/>

## 예제 입력
4 3 2<br/>
1 1 2 3 3 2 2 1 1 2 3 3<br/>

## 예제 출력
A 3<br/>

## 풀이
w x h 가 25만까지 가고, k가 max(w, h)까지 올 수 있으므로 매번 돌을 놓을 때 모든 방향에 길이를 확인하는건 시간초과날거 같았다.<br/>
그래서 각 방향에 그룹을 설정하고, 해당 돌을 놓을 때 인접한걸 확인해 유니온 파인드 알고리즘으로 그룹화 시켜 확인했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/20907