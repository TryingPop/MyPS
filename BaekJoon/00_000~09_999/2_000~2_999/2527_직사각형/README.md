# C#

## 난이도 : 실버 1

## 알고리즘 분류
  - 기하학
  - 많은 조건 분기

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
2차원 격자공간에 두 개의 꼭짓점 좌표로 표현되는 직사각형이 있다. 직사각형은 아래와 같이 왼쪽 아래 꼭짓점 좌표 (x, y)와 오른쪽 위 꼭짓점 좌표 (p, q)로 주어진다.<br/>
이 문제에서 모든 직사각형은 두 꼭짓점의 좌표를 나타내는 4개의 정수 x y p q 로 표현된다. 단 항상 x<p, y<q 이다. 예를 들어 위 그림에 제시된 직사각형이라면 아래와 같이 표현된다.<br/>

	3 2 9 8

두 개의 직사각형은 그 겹치는 부분의 특성에 따라 다음 4가지 경우로 분류될 수 있다. <br/>
먼저 두 직사각형의 겹치는 부분이 직사각형인 경우이다. 아래 그림(a)는 공통부분이 직사각형인 경우의 3가지 예를 보여준다,<br/>
또는 겹치는 부분이 아래 그림 (b)와 같이 선분이 될 수도 있고, 그림 (c)와 같이 점도 될 수 있다.<br/>
마지막으로 아래 그림 (d)와 같이 공통부분 없이 두 직사각형이 완전히 분리된 경우도 있다.<br/>
여러분은 두 직사각형의 겹치는 부분이 직사각형인지, 선분인지, 점인지, 아니면 전혀 없는 지를 판별해서 해당되는 코드 문자를 출력해야 한다. <br/>


|공통부분의 특성|코드 문자|
|:---:|:---:|
|직사각형|a|
|선분|b|
|점|c|
|공통부분이 없음|d|

## 입력
4개의 줄로 이루어져 있다. 각 줄에는 8개의 정수가 하나의 공백을 두고 나타나는데, 첫 4개의 정수는 첫 번째 직사각형을, 나머지 4개의 정수는 두 번째 직사각형을 각각 나타낸다. 단 입력 직사각형의 좌표 값은 1이상 50,000 이하의 정수로 제한된다.<br/>

## 출력
4개의 각 줄에 주어진 두 직사각형의 공통부분을 조사해서 해당하는 코드 문자를 출력파일의 첫 4개의 줄에 각각 차례대로 출력해야 한다.<br/>

## 예제 입력
3 10 50 60 100 100 200 300<br/>
45 50 600 600 400 450 500 543<br/>
11 120 120 230 50 40 60 440<br/>
35 56 67 90 67 80 500 600<br/>

## 예제 출력
d<br/>
a<br/>
a<br/>
b<br/>

## 풀이
x의 범위와 y의 범위가 일치하는 구간으로 어떤 유형에 속하는지 찾아냈다.<br/>
만약 x, y가 한 점에서 만나면 점이된다.<br/>
x 또는 y가 점에서 만나고 나머지 하나는 구간이면 직선이된다.<br/>
x와 y 모두 구간이면 사각형이되고<br/>
둘 중 하나라도 만나지 않는다면 만나지 않는다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/2527