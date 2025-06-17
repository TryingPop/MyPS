# C#

## 난이도 : 브론즈 4

## 알고리즘 분류
  - 수학
  - 사칙연산

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
Testing for Corona can be done individually, e.g., 100 people require 100 test kits. Alternatively, the test can be done in groups (pools), e.g., 100 people can be divided into five group of 20 people each and then using only one test kid per group. If one or more groups test positive, then individual tests are needed for each person in those group. So, for our example, five groups will need 5 test kits and let’s say two groups test positive, so we would need additional 40 (2*20) test kits for a total of 45 (5+40) test kits.<br/>
Given the data for the two possible testing approaches, determine which approach will use fewer test kits.<br/>


## 입력
There is only one input line; it provides three integers: g (2 ≤ g ≤ 50), indicating the number of groups, p (2 ≤ p ≤ 50), indicating the number of people in each group, and t (0 ≤ t ≤ g), indicating how many groups tested positive (i.e., people in these groups need to be tested individually).<br/>


## 출력
Print 1 (one) if testing everyone individually will use fewer kits, 2 (two) if testing in groups will use fewer kits, and 0 (zero) if the two approaches use the same number of kits.<br/>


## 예제 입력
20 10 18<br/>


## 예제 출력
0<br/>


## 풀이
개별 검사를 하는 경우 g * p가 된다.<br/>
반면 그룹별 검사를 하는 경우 g + p * t가 된다.<br/>
이 둘을 비교해 정답에 맞춰 출력하면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/25828