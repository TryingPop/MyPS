# C#

## 난이도 : 실버 2

## 알고리즘 분류
  - 다이나믹 프로그래밍

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
#### The game of hopscotch involves chalk, sidewalks, jumping, and picking things up. Our variant of hopscotch involves money as well. The game is played on a square grid of dimension n: each grid location is labelled (p,q) where 0 <= p < n and 0 <= q < n. Each grid location has on it a stack of between 0 and 100 pennies.
#### A contestant begins by standing at location (0,0). The contestant collects the money where he or she stands and then jumps either horizontally or vertically to another square. That square must be within the jumping capability of the contestant (say, k locations) and must have more pennies than those that were on the current square.
#### Given n, k, and the number of pennies at each grid location, compute the maximum number of pennies that the contestant can pick up before being unable to move.

## 입력
  - a line containing two integers between 1 and 100: n and k
  - n lines, each with n numbers: the first line contains the number of pennies at locations (0,0) (0,1) ... (0,n-1); the next line contains the number of pennies at locations (1,0), (1,1), ... (1,n-1), and so on.

## 출력
  - a single integer giving the number of pennies collected

## 예제 입력
3 1<br/>
1 2 5<br/>
10 11 6<br/>
12 12 7<br/>

## 예제 출력
37<br/>

## 문제 링크
https://www.acmicpc.net/problem/4421