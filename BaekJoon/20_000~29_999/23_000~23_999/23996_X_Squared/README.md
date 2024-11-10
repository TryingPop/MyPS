# C#

## 난이도 : 골드 1

## 알고리즘 분류
  - 자료 구조
  - 애드 혹

## 제한조건
  - 시간 제한 : 20초
  - 메모리 제한 : 1024 MB

## 문제
The hot new toy for this year is called "X Squared". It consists of a square N by N grid of tiles, where N is odd. Exactly 2 × N - 1 of the tiles are labeled with an X, and the rest are blank (which we will represent with the . character). In each move of the game, the player can either choose and exchange two rows of tiles, or choose and exchange two columns of tiles. The goal of the game is to get all of the X tiles to be on the two main diagonals of the grid, forming a larger X shape, as in the following example for N = 5:<br/>

	X...X
	.X.X.
	..X..
	.X.X.
	X...X

You are about to play with your X Squared toy, which is not yet in the goal state. You suspect that your devious younger sibling might have moved some of the tiles around in a way that has broken the game. Given the current configuration of the grid, can you determine whether it is possible to win or not?<br/>

## 입력
The first line of the input gives the number of test cases, T. T test cases follow. Each one begins with one line with an integer N, the size of the grid. N more lines with N characters each follow; the j-th character on the i-th of these lines is X if the tile in the i-th row and j-th column of the grid has an X, or . if that tile is blank.<br/>

## 출력
For each test case, output one line containing Case #x: y, where x is the test case number (starting from 1) and y is POSSIBLE if it is possible to win, and IMPOSSIBLE otherwise.<br/>

## 제한
  - 1 ≤ T ≤ 100.
  - N mod 2 = 1. (N is odd.)
  - The grid has exactly 2 × N - 1 X tiles and exactly N2 - 2 × N + 1 . tiles.
  - The grid is not already in the goal state, as described in the problem statement.
  - 3 ≤ N ≤ 55.

## 예제 입력
2<br/>
3<br/>
..X<br/>
XX.<br/>
XX.<br/>
3<br/>
...<br/>
XXX<br/>
XX.<br/>

## 예제 출력
Case #1: POSSIBLE<br/>
Case #2: IMPOSSIBLE<br/>

## 힌트
In Sample Case #1, one winning strategy is:<br/>

  1. Swap the top row with the middle row.
  2. Swap the rightmost column with the middle column.

	..X    XX.    X.X
	XX. -> ..X -> .X.
	XX.    XX.    X.X

In Sample Case #2, no sequence of moves can turn the grid into the desired final configuration.<br/>

## 풀이
행을 바꿀 시 열에 있는 X의 개수는 불변이다.<br/>
마찬가지로 열을 바꿀 시 행에 있는 X의 개수는 불변이다.<br/>
그래서 X가 되는 경우를 보면 각 열로 보면 X가 1개인 경우는 유일하고 이외는 X가 2개씩 이었야 한다.<br/>
행의 경우 단순히 열처럼 배치하면 다음과 같은 반례가 존재한다.<br/>

	X.X..
	X....
	.X.X.
	.X..X
	...XX

행의 경우 X가 한 개있는 곳에 1개가 있어야 하고 이외는 2개씩 있어야 한다.<br/>
이렇게 검증하니 이상없이 통과한다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/23996