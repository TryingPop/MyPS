# C#

## 난이도 : 브론즈 3

## 알고리즘 분류
  - 구현

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
Autubuso maršrutą sudaro N stotelių. Į pirmą stotelę autobusas atvažiuoja tiesiai iš parko, todėl jis visuomet būna tuščias. Toliau kiekvienoje stotelėje į autobusą įlipa ir išlipa kažkiek keleivių.<br/>
Keleiviai mėgsta sėdėti, tad jeigu autobuse yra laisvų sėdimų vietų – keleiviai atsisės, o stovės tik tada, kai nebus nei vienos laisvos sėdimos vietos. Iš viso autobuse yra K sėdimų vietų.<br/>
Jums žinoma, kiek kiekvienoje stotelėje įlips ir išlips keleivių. Raskite, kiek daugiausiai keleivių vienu metu turės stovėti autobuse.<br/>


## 입력
Pirmoje eilutėje pateikti du sveikieji skaičiai N ir K. N yra stotelių skaičius autobuso maršrute, o K – sėdimų vietų skaičius autobuse.<br/>
Tolesnėse N eilučių pateikta po du skaičius ai ir bi. ai – tai stotelėje įlipsiančių keleivių skaičius, o bi – išlipsiančių keleivių skaičius.<br/>


## 출력
Išveskite vieną sveikąjį skaičių – kiek daugiausiai keleivių turės stovėti autobuse vienu metu.<br/>


## 제한
  - 2 ≤ N ≤ 1 000
  - 0 ≤ ai, bi, K ≤ 1 000


## 예제 입력
4 10<br/>
5 0<br/>
5 2<br/>
10 5<br/>
0 13<br/>


## 예제 출력
3<br/>


## 힌트
Tarp trečios ir ketvirtos stotelės 10 keleivių sėdės, o 3 stovės.<br/>


## 풀이
승객이 타고 내리는 경우는 O(1)에 해결가능하고 N의 범위가 1000이하이므로 시뮬레이션 방법이 유효하다.<br/>
그래서 버스를 이동시키면서 승객을 태우면서 시뮬레이션 돌려 최댓값을 찾았다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/7279