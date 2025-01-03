# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 브루트포스 알고리즘
  - 애드 혹

## 제한조건
  - 시간 제한 : 5초
  - 메모리 제한 : 512 MB

## 문제
Alice and Bob have a lawn in front of their house, shaped like an N metre by M metre rectangle. Each year, they try to cut the lawn in some interesting pattern. They used to do their cutting with shears, which was very time-consuming; but now they have a new automatic lawnmower with multiple settings, and they want to try it out.<br/>
The new lawnmower has a height setting - you can set it to any height h between 1 and 100 millimetres, and it will cut all the grass higher than h it encounters to height h. You run it by entering the lawn at any part of the edge of the lawn; then the lawnmower goes in a straight line, perpendicular to the edge of the lawn it entered, cutting grass in a swath 1m wide, until it exits the lawn on the other side. The lawnmower's height can be set only when it is not on the lawn.<br/>
Alice and Bob have a number of various patterns of grass that they could have on their lawn. For each of those, they want to know whether it's possible to cut the grass into this pattern with their new lawnmower. Each pattern is described by specifying the height of the grass on each 1m x 1m square of the lawn.<br/>
The grass is initially 100mm high on the whole lawn.<br/>


## 입력
The first line of the input gives the number of test cases, T. T test cases follow. Each test case begins with a line containing two integers: N and M. Next follow N lines, with the ith line containing M integers ai,j each, the number ai,j describing the desired height of the grass in the jth square of the ith row.<br/>


## 제한
  - 모든 입력은 정수로 주어진다.
  - 1 ≤ T ≤ 100.
  - 1 ≤ N, M ≤ 100.
  - 1 ≤ ai,j ≤ 100.


## 출력
For each test case, output one line containing "Case #x: y", where x is the case number (starting from 1) and y is either the word "YES" if it's possible to get the x-th pattern using the lawnmower, or "NO", if it's impossible (quotes for clarity only).<br/>


## 예제 입력
3<br/>
3 3<br/>
2 1 2<br/>
1 1 1<br/>
2 1 2<br/>
5 5<br/>
2 2 2 2 2<br/>
2 1 1 1 2<br/>
2 1 2 1 2<br/>
2 1 1 1 2<br/>
2 2 2 2 2<br/>
1 3<br/>
1 2 1<br/>

## 예제 출력
Case #1: YES<br/>
Case #2: NO<br/>
Case #3: YES<br/>


## 풀이
잔디를 깎을 때, 열과 행의 순서는 뒤바껴도 상관없다.<br/>
열부터 먼저 깎던, 행부터 먼저깎던 결과는 항상 일정하다.<br/>
그리고 같은 열(행)을 2번깎는데, 이는 가장 짧은 길이로 1번 깎는것과 같은 결과를 낸다.<br/>
3행 4열인 맵에 대해 다음과 같이 자른다고 보자.

||1행|2행|3행|1열|2열|3열|4열|
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|깎을 길이|5|6|6|6|5|2|4|

먼저 1행, 2행, 3행을 깎을 길이에 맞춰 깎은 경우는 다음과 같다.<br/>

||1열|2열|3열|4열|
|:---:|:---:|:---:|:---:|:---:|
|1행|5|5|5|5|
|2행|6|6|6|6|
|3행|6|6|6|6|

이후 1열, 2열, 3열, 4열을 깎을 길이에 맞춰 깎은 경우는 다음과 같다.<br/>

||1열|2열|3열|4열|
|:---:|:---:|:---:|:---:|:---:|
|1행|5|5|2|4|
|2행|6|5|2|4|
|3행|6|5|2|4|

이후 각 행과 열에 대해 각각의 최대값을 찾으면 다음과 같다.<br/>

||1행|2행|3행|1열|2열|3열|4열|
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|최댓값|5|6|6|6|5|2|4|
|깎을 길이|5|6|6|6|5|2|4|

이는 깎을 길이와 일치한다.<br/>
즉, 깎을 수 있는 경우 최댓값은 깎을 길이와 일치한다.<br/>
그리고 각 r행 c열의 길이에 대해 다음이 성립한다.<br/>
r, c의 잔디의 길이를 board[r][c], r행의 최댓값을 row[r], c열의 최댓값을 col[c]라 하자.<br/>
그러면 row[r] <= board[r][c] 이거나 col[c] <= board[r][c]이다.<br/>
깎을 수 있는 경우는 위 식이 성립함을 알 수 있다.<br/>


역 역시 귀류법으로 보일 수 있다.<br/>
만약 적당한 r, c가 존재해 board[r][c] < row[r]이고 board[r][c] < col[c]라 하자.<br/>
그러면 해당 좌표를 깎기 위해서는 열 또는 행을 board[r][c]가 되게 해야한다.<br/>
그런데 board[r][c] < row[r]의 경우로 보면 r행에서의 최대값이 row[r]이라 했으므로 r행은 될 수 없다.<br/>
그래서 c열을 밀 수 밖에 없는데, 이 역시 board[r][c] < col[c] 같은 논리로 될 수 없다.<br/>
이는 가정에 모순이고 귀류법으로 row[r] <= board[r][c] 또는 col[c] <= board[r][c] 일 수 밖에 없다.<br/>


그래서 각 행과 열의 최댓값을 찾고, board의 모든 좌표를 board[r][c] < row[r] 이고 board[r][c] < col[c]가 있는지로 판별했다.<br/>

12337번 역시 해당 코드로 풀 수 있다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/12338