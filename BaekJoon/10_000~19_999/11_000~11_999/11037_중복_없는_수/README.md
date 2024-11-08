# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 브루트포스 알고리즘
  - 정렬
  - 이분 탐색
  - 백트래킹

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
중복 없는 수는 각 숫자(1, 2, 3, ..., 9)가 최대 한 번씩 등장하고, 숫자 0은 포함하지 않는 수이다. 따라서 중복 없는 수는 최대 9자리로 이루어질 수 있다. 중복 없는 수의 예시로는 9, 32, 489, 98761, 983245 등이 있다.<br/>
정수 N이 주어질 때, N보다 크면서 가장 작은 중복 없는 수를 출력하는 프로그램을 작성하라.<br/>

## 입력
입력은 여러 개의 테스트 케이스로 이루어져 있다. 각 테스트 케이스는 한 줄로 이루어져 있으며, 각 줄에 정수 N(0 ≤ N ≤ 999,999,999)이 주어진다.<br/>

## 출력
각 테스트 케이스마다 답을 출력한다. 만약 답에 해당하는 수가 없는 경우는 0을 출력한다.<br/>

## 예제 입력
99<br/>
881<br/>
133<br/>
999999999<br/>

## 예제 출력
123<br/>
891<br/>
134<br/>
0<br/>

## 풀이
중복 없는 수를 직접 만들어 풀었다.<br/>
입력 받은 숫자에 1을 더하고 문자열로 만든다.<br/>
각 자리수를 조사하는데 해당 자리에 숫자를 기록하고 썼음을 기록한다.<br/>
만약 중복되는게 없으면 바로 종료한다.<br/>

중복되는게 있으면 해당 수에서 기록하는걸 멈추고 큰수가 되게 중복없는 수를 만든다.<br/>
해당 자리는 기존 값보다 큰 수가 와야한다. 그래서 값을 1씩 늘리면서 채워간다.<br/>
여기서 중복안된 수를 발견하면 나머지 뒤쪽 부분은 안쓴수를 작은수부터 채워가면 된다.<br/>
만약 1씩 늘려가면서 9까지 확인해도 없다면 앞자리로 이동해 반복한다.<br/>
맨 왼쪽 단위에서 없다면 단위를 올린다.<br/>
이렇게 진행하면서 9자리인 경우에서 못찾으면 0을 출력했다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/11037