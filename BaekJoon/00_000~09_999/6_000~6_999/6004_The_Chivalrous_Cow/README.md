# C#

## 난이도 : 실버 2

## 알고리즘 분류
  - 그래프 이론
  - 그래프 탐색
  - 너비 우선 탐색

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Farmer John traded one of his cows for a cow that Farmer Don called 'The Knight'. This cow has the unique ability to jump around the pasture in moves that looked like a knight on a chessboard (two squares over, one up... or maybe two squares down and one over, etc.). 'The Knight' can't jump on rocks or trees, but can really make her way around the pasture, which is partitioned for our purposes into an X by Y set of squares (1 <= X <= 150; 1 <= Y <= 150).<br/>
'The Knight' likes hay just like any other cow. Given a map of 'The Knight's starting place, locations of the tree, shrub, rock, and other obstacles, and the location of a bale of hay, determine how many 'hops' the Knight must make in order to get to the hay. The Knight cow will be marked by a 'K' on the map; obstacles by '*', and the haybale by 'H'. Here's a typical map:<br/>

	                    11 | . . . . . . . . . .
	                    10 | . . . . * . . . . . 
	                     9 | . . . . . . . . . . 
	                     8 | . . . * . * . . . . 
	                     7 | . . . . . . . * . . 
	                     6 | . . * . . * . . . H 
	                     5 | * . . . . . . . . . 
	                     4 | . . . * . . . * . . 
	                     3 | . K . . . . . . . . 
	                     2 | . . . * . . . . . * 
	                     1 | . . * . . . . * . . 
	                     0 ----------------------
	                                           1 
	                       0 1 2 3 4 5 6 7 8 9 0 


The knight could travel the path indicated by A, B, C, ... to get to the hay bale in just 5 moves (other routes of length 5 might be possible):<br/>

	                    11 | . . . . . . . . . .
	                    10 | . . . . * . . . . .
	                     9 | . . . . . . . . . .
	                     8 | . . . * . * . . . .
	                     7 | . . . . . . . * . .
	                     6 | . . * . . * . . . F<
	                     5 | * . B . . . . . . .
	                     4 | . . . * C . . * E .
	                     3 | .>A . . . . D . . .
	                     2 | . . . * . . . . . *
	                     1 | . . * . . . . * . .
	                     0 ----------------------
	                                           1
	                       0 1 2 3 4 5 6 7 8 9 0


Hint: This problem is probably most easily solved with a simple first-in/first-out 'queue' data structure implemented as a few parallel arrays.<br/>


## 입력
  - Line 1: Two space-separated integers: X and Y
  - Lines 2..Y+1: Line Y-i+2 contains X characters with no spaces (i.e., the map is read in as shown in the text above): map row i


## 출력
  - Line 1: A single integer that is the minimum number of hops to get to the hay bale. It is always possible to get to the haybale.


## 예제 입력
10 11<br/>
..........<br/>
....*.....<br/>
..........<br/>
...*.*....<br/>
.......*..<br/>
..*..*...H<br/>
*.........<br/>
...*...*..<br/>
.K........<br/>
...*.....*<br/>
..*....*..<br/>


## 예제 출력
5<br/>


## 풀이
K에서 체스의 나이트 이동 방법으로 H로 이동할 수 있는지 확인하는 문제다.<br/>
지도의 크기가 150 x 150이므로 BFS로 최단 경로를 확인하며 풀었다.<br/>
힌트로도 큐를 이용하면 쉽게 구할 수 있다고 한다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/6004