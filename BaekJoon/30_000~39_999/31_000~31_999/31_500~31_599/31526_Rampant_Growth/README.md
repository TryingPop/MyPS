# C#

## 난이도 : 브론즈 2

## 알고리즘 분류
  - 수학
  - 조합론

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 2048 MB

## 문제
Eddy is planning his garden, which can be represented as a grid. He wants his garden to have exactly one plant in each column. To make sure that plants do not compete for resources, if two plants are in adjacent columns, they must be in different rows.<br/>
Compute the number of different ways he can place plants in his garden to respect the above conditions. Two ways are different if one square has a plant in one arrangement but does not have a plant in the other. Because the number of ways may be large, output the number of ways modulo 998,244,353.<br/>


## 입력
The single line of input contains two space-separated integers r and c (1 ≤ r,c ≤ 50), where r is the number of rows in Eddy's garden grid, and c is the number of columns.<br/>


## 출력
Output a single integer, which is the number of ways Eddy can place plants in his garden, modulo 998,244,353.<br/>


## 예제 입력
42 25<br/>

## 예제 출력
722210361<br/>


## 풀이
각 열마다 하나의 식물을 심어야 하고 같은 행에서 인접한 식물이 없어야 한다.<br/>
이때 식물의 배치 순서 자체는 고정되어 있고, 각 열마다 어떤 행을 선택할지만 고려하면 된다.<br/>
문제는 총 row × col 격자에서 col개의 열에 각각 하나의 식물을 배치하되, 같은 행의 인접 열에 두 식물을 둘 수 없는 경우의 수를 구하는 것이다.<br/>


먼저 1열에 처음 놓을 수 있는 경우의 수는 행의 갯수 row만큼 있다. 그래서 경우의 수는 row 가지이다.<br/>
이후 i > 1부터는 i - 1열에 배치된 식물과 같은 행에 있으면 안되므로 가능한 경우는 row - 1개 있다.<br/>
그래서 전체 경우의 수는 row x (row - 1)^(col - 1)임을 알 수 있다.<br/>


row x (row - 1)^(col - 1)의 연산만 구하면 되므로 매번 row - 1을 곱한다면 col번 연산을 한다.<br/>
그래서 시간 복잡도는 O(col)이 된다.<br/>


반면 분할 정복을 이용한 거듭제곱을 한다면 O(log col)에 해결할 수 있다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/31526