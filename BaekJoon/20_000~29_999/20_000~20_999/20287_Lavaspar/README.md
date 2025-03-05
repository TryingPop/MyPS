# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 브루트포스 알고리즘

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
Caça Palavras é um passatempo bastante conhecido, embora esteja perdendo um pouco do seu prestígio nos últimos anos. O objetivo deste jogo é encontrar palavras em uma matriz, onde cada célula dessa matriz contém uma letra.<br/>
Bibika e seu irmão estavam jogando Caça Palavras, porém em pouco tempo perderam o interesse, visto que encontrar todas as palavras estava ficando relativamente fácil. Como Bibika queria que seu irmão saísse um pouco do computador, ela pesquisou na internet jogos do mesmo estilo e acabou encontrando o Caça Lavaspar.<br/>
Caça Lavaspar é um jogo que segue a mesma ideia do famoso Caça Palavras. Porém, ao invés de simplesmente ter que encontrar uma palavra na matriz, o objetivo é encontrar um anagrama qualquer da palavra, fazendo assim com que o jogo fique mais difícil e interessante. O anagrama pode ser encontrado em uma linha, coluna ou diagonal.<br/>
Um anagrama de uma palavra é formado pelo rearranjo das letras da palavra. Às vezes, o anagrama não tem sentido, mas isto não importa. BALO, LOBA e AOLB são exemplos de anagramas da palavra BOLA.<br/>
Bibika percebeu ser possível que uma mesma célula da matriz fizesse parte de anagramas de diferentes palavras e então ela passou a chamar essas células de células especiais.<br/>
Agora ela gostaria de saber, dada uma configuração de uma matriz e uma coleção de palavras, quantas células especiais existem?<br/>
A imagem acima ilustra o primeiro exemplo, onde a coleção de palavras consiste de três palavras: BOLA, CASA e BOI. Os retângulos de cada cor representam anagramas de palavras diferentes da entrada. As 3 células especiais estão pintadas de amarelo.<br/>


## 입력
A primeira linha possui dois inteiros L e C, que correspondem ao número de linhas e de colunas da matriz, respectivamente.<br/>
Seguem então L linhas, cada uma contendo uma palavra com C letras.<br/>
Após isso, a próxima linha contém um inteiro, N, que representa a quantidade de palavras na coleção de palavras a seguir.<br/>
E então, por fim, temos mais N linhas, onde cada uma delas contém uma palavra da coleção.<br/>
Todos os caracteres utilizados, tanto na matriz quanto na coleção de palavras, são letras maiúsculas do alfabeto inglês.<br/>
É garantido que nenhum par de palavras da coleção é um anagrama uma da outra.<br/>


## 출력
A saída deve consistir de uma única linha que contém o número de células especiais.<br/>


## 제한
  - 2 ≤ L, C ≤ 40.
  - 2 ≤ N ≤ 20.
  - O número P de letras de cada uma das N palavras está no intervalo 2 ≤ P ≤ min(15, max(L, C)).


## 예제 입력
4 5<br/>
XBOIC<br/>
DKIRA<br/>
ALBOA<br/>
BHGES<br/>
3<br/>
BOLA<br/>
CASA<br/>
BOI<br/>


## 예제 출력
3<br/>


## 풀이
서로 다른 두 글자의 애너그램을 만들 수 있는 문자에 해당하는 블록의 갯수를 찾아야 한다.<br/>
맵의 블록의 갯수가 최대 40 x 40 = 1_600개 이다.<br/>
그리고 문자의 길이는 최대 15이고, 갯수는 20개이고 방향은 4방향이다.<br/>
그래서 각 셀을 조사해도 전체 경우는 1_600 x 300 x 4 = 1_920_000 = 이다.<br/>
그래서 브루트포스로 각 셀을 조사했다.<br/>

그리고 같은 애너그램 문자에 대해서는 카운팅 되지 않으므로 상태 저장을 비트 마스킹으로 저장했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/20287