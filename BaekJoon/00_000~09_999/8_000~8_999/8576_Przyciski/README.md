# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 구현
  - 자료 구조
  - 해시를 사용한 집합과 맵

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 512 MB

## 문제
#### Bajtek znalazł ciekawą zabawkę. Zabawka ta ma n+1 przycisków. Nad każdym z n pierwszych przycisków znajduje się mały licznik, początkowo wskazujący zero. Naciśnięcie przycisku pod licznikiem zwiększa wskazywaną przezeń liczbę o 1.
#### Zabawka szybko by się Bajtkowi znudziła, gdyby nie kuriozalne działanie przycisku o numerze n+1. Po jego użyciu wszystkie n liczników zaczyna wskazywać największą z widocznych dotąd na zabawce wartości. Na przykład, jeżeli n = 5 i kolejne liczniki wskazują liczby 0, 0, 1, 2, 0, to po naciśnięciu przycisku o numerze 6 wszystkie liczniki będą wskazywać 2.
#### Wiedząc, które przyciski wybierał kolejno Bajtek, chcemy poznać wartości wszystkich liczników po zakończeniu zabawy.

## 입력
#### Pierwszy wiersz standardowego wejścia zawiera dwie liczby całkowite n, m (1 ≤ n, m ≤ 10^6), oznaczające kolejno liczbę liczników na zabawce i liczbę operacji wykonanych przez Bajtka. Drugi wiersz wejścia zawiera m liczb całkowitych p_1, p_2, ... , p_m (1 ≤ p_i ≤ n+1), oznaczających numery kolejnych przycisków wciskanych przez Bajtka.

## 출력
#### Pierwszy i jedyny wiersz standardowego wyjścia powinien zawierać n liczb całkowitych, oddzielonych pojedynczymi odstępami, oznaczających wartości znajdujące się na kolejnych licznikach po zakończeniu zabawy.

## 예제 입력
5 7<br/>
3 4 4 6 1 4 4<br/>

## 예제 출력
3 2 2 4 2<br/>

## 문제 링크
https://www.acmicpc.net/problem/8576