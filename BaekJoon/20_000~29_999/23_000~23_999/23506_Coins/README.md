# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 그리디 알고리즘
  - 정렬

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
You are given an N x N matrix and in exactly N of its cells there is a single coin.  You are going to play a game on this matrix where in one turn you can take a coin and move it to any adjacent cell. Two cells are adjacent if they share a side. However, during these moves, no two coins may occupy the same cell at the same time. Your goal is to make every row and column contain exactly one coin in as few moves as possible. Determine the minimum number of moves required.<br/>


## 입력
In the first line of input is the number N (N ≤ 200000): the number of rows, columns and coins.<br/>
In the (i+1)-th line are two integers r_i and c_i, denoting the initial row and column of the i-th coin.  It is guaranteed that all pairs (r_i,c_i) are different.<br/>


## 출력
Print a single integer: the minimum number of moves required to win at the game.<br/>


## 예제 입력
3<br/>
2 1<br/>
2 2<br/>
2 3<br/>


## 예제 출력
2<br/>


## 풀이
x축 이동과 y축 이동은 서로 영향을 주지 않는다.<br/>
그래서 x축 이동인 경우와 y축 이동인 경우의 순서를 바꿔도 된다.<br/>
그래서 x축 이동을 먼저하고 y축 이동을 뒤에하게 하면 x축 이동과 y축 이동을 나눠서 생각해도 된다.<br/>


이제 x축 이동의 최소횟수를 찾아본다.<br/>
x축 값을 기준으로 오름차순 정렬한다.<br/>
그리고 가장 낮은거부터 1번에 이동하게 하고, 다음으로 낮은 것을 2번, ..., n번까지 최소한의 이동으로 이동하며 배치한다.<br/>
그러면 그리디로 해당 이동 방법이 최소 횟수임을 알 수 있다.<br/>
이렇게 찾은값을 xSum이라 하자.<br/>


y축도 똑같이 y축 값을 기준으로 오름차순 정렬한다.<br/>
그리고 가장 낮은거부터 1번으로 이동하게 하고, 다음으로 낮은 것을 2번, ..., n번까지 배치하며 ySum값을 찾는다.<br/>


x축 이동과 y축 이동이 서로 영향을 주지 않기에 xSum + ySum이 정답이 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/23506