## Implementação Análise Semântica, Tabela de Símbolos e AAS

Aceita uma gramática para os elementos definidos abaixo e implementa o analisador sintático ascendente SLR, e a análise semântica juntamente com a tabela de símbolos e a árvore de análise sintática para esta gramática:
```
Função
Lista de parâmetros
Declaração de variável
Comandos de atribuição, if, while, print e chamada de função
```
Também foi implementado a geração de código e executável, sendo a saída na linguagem C# e posteriormente compilado utilizado o compilador C# ([Microsoft Learn - Compilar código usando o compilador C#](https://learn.microsoft.com/pt-br/troubleshoot/developer/visualstudio/csharp/language-compilers/compile-code-using-compiler))

Exemplo de código fonte de entrada:
```
function x (float a, int b){
   float x;
   float y;
   float z;
   x = a + b;
   y = 10 * x + 22.8 / b;
   if (x > 10){
      print(x);
      z = y - x;
   }

   float t;
   int i;
   i = 0;
   t = 654.2 * a - b;
   while(i < 16) {
      t = a / b + 1.99;
      i = i + 1;
   }

   int n;
   float f;
   f = 9876.225;
   if (x == y){
      print(y);
      int j;
      j = 32 - b;
      n = 1234;

      while(n < y) {
         f = a / b + y;
         t = f *3;

         if(f != t) {
            print(f);
         }

         n = n + 1;
      }

      x = y * f - 4321;
   }
   print(t);
}
```
