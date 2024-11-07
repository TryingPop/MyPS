# C#

## 난이도 : 실버 2

## 알고리즘 분류
  - 자료 구조
  - 문자열
  - 해시를 사용한 집합과 맵
  - 트리를 사용한 집합과 맵

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
#### 생태학에서 나무의 분포도를 측정하는 것은 중요하다. 그러므로 당신은 미국 전역의 나무들이 주어졌을 때, 각 종이 전체에서 몇 %를 차지하는지 구하는 프로그램을 만들어야 한다.

## 입력
#### 프로그램은 여러 줄로 이루어져 있으며, 한 줄에 하나의 나무 종 이름이 주어진다. 어떤 종 이름도 30글자를 넘지 않으며, 입력에는 최대 10,000개의 종이 주어지고 최대 1,000,000그루의 나무가 주어진다.

## 출력
#### 주어진 각 종의 이름을 사전순으로 출력하고, 그 종이 차지하는 비율을 백분율로 소수점 4째자리까지 반올림해 함께 출력한다.

## 예제 입력
Red Alder<br/>
Ash<br/>
Aspen<br/>
Basswood<br/>
Ash<br/>
Beech<br/>
Yellow Birch<br/>
Ash<br/>
Cherry<br/>
Cottonwood<br/>
Ash<br/>
Cypress<br/>
Red Elm<br/>
Gum<br/>
Hackberry<br/>
White Oak<br/>
Hickory<br/>
Pecan<br/>
Hard Maple<br/>
White Oak<br/>
Soft Maple<br/>
Red Oak<br/>
Red Oak<br/>
White Oak<br/>
Poplan<br/>
Sassafras<br/>
Sycamore<br/>
Black Walnut<br/>
Willow<br/>

## 예제 출력
Ash 13.7931<br/>
Aspen 3.4483<br/>
Basswood 3.4483<br/>
Beech 3.4483<br/>
Black Walnut 3.4483<br/>
Cherry 3.4483<br/>
Cottonwood 3.4483<br/>
Cypress 3.4483<br/>
Gum 3.4483<br/>
Hackberry 3.4483<br/>
Hard Maple 3.4483<br/>
Hickory 3.4483<br/>
Pecan 3.4483<br/>
Poplan 3.4483<br/>
Red Alder 3.4483<br/>
Red Elm 3.4483<br/>
Red Oak 6.8966<br/>
Sassafras 3.4483<br/>
Soft Maple 3.4483<br/>
Sycamore 3.4483<br/>
White Oak 10.3448<br/>
Willow 3.4483<br/>
Yellow Birch 3.4483<br/>

## 풀이
나무 이름과 개수를 확인해야 하므로 딕셔너리 자료구조로 나무 종과 갯수를 저장했다.<br/>
그리고 정렬은 foreach로 읽어올 때 정렬해서 읽어 왔다. 정렬을 고려한다면 SortedDictionary도 좋다.<br/>
소수점 4째자리의 정밀도를 요구하기에 double 형으로 % 연산을 했다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/4358