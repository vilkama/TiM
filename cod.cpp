#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <locale.h>

int Ind(int *a,int x,int n)
{
    int i;
    for(i=0;i<n;i++)
        if(a[i]==x)
          return n-i-1;
    return -1;
}

void Table(char *str,int *arr,int TheInputView)
{
    int i,j;
   for(i=0;i<TheInputView;i++)
        printf("%c ",arr[i]+'A');
    printf("Функция\n");
    for(i=0;i<pow(2,TheInputView);i++)
    {
        for(j=TheInputView-1;j>=0;j--)
          printf("%i ",i>>j&1);
        printf(" %i\n",DNF(str,arr,i,TheInputView));
    }
}

int contrary(char *str,int *arr,int TheInputView)
{
    int i,j;
    for(i=0;i<pow(2,TheInputView);i++)
    {
        if(DNF(str,arr,i,TheInputView))
          return 0;
    }
    return 1;
}

int TheInputView(char *s,int *arr){
  int i,k=0,j;
  int a[26]={0};
  while(*s)
  {
      if(*s>='A' && *s<='Z')
      {
          if(a[*s-'A']++==0)
            k++;
      }
      s++;

  }
  realloc((int*)arr,k*sizeof(int));
  j=0;
  for(i=0;i<26;i++)
  {
      if(a[i]){
        arr[j++]=i;
      }
  }
  return k;
}
int DNF(char *s,int *arr,int b,int n)
{
    int t1=0,t2=0,sum,i;
    int *bit;
    bit=(int*)calloc(n,sizeof(int));
    for(i=n-1;i>=0;i--)
        bit[i]=b>>i & 1;
    while(*s)
    {
        if(*s=='(')
        {
            s++;
            t1=t2=0;
            while(*s!=')')
            {
                if(*s=='!')
                {
                   s++;
                   t1=t1||!bit[Ind(arr,*s-'A',n)];
                   s++;
                }
                else if(*s>='A' && *s<='Z')
                {
                    t1=t1||bit[Ind(arr,*s-'A',n)];
                    s++;
                }
                else if(*s=='v')
                {
                  s++;
                  if(*s=='!')
                  {
                     s++;
                     t1=t1 || !bit[Ind(arr,*s-'A',n)];
                     s++;
                  }
                  else
                  {
                    t1=t1 || bit[Ind(arr,*s-'A',n)];
                    s++;
                  }
                }
                else
                   s++;
            }
            s++;
            if(t1 == 0)
                return 0;
        }
        else if(*s=='&')
        {
          s++;
        }
        else if(*s=='!')
        {
          t1=0;
          s++;
          t1=t1||!bit[Ind(arr,*s-'A',n)];
          s++;
          if((*s=='\0'||*s=='&') && t1==0)
            return 0;
        }
        else if(*s>='A' && *s<='Z')
        {
          t1=0;
          t1=t1||bit[Ind(arr,*s-'A',n)];
          s++;
          if((*s=='\0'||*s=='&') && t1==0)
            return 0;
        }
        else if(*s=='v')
                {
                  s++;
                  if(*s=='!')
                  {
                     s++;
                     t1=t1 || !bit[Ind(arr,*s-'A',n)];
                     s++;
                  }
                  else
                  {
                    t1=t1 || bit[Ind(arr,*s-'A',n)];
                    s++;
                  }
                }
    }
    return t1;
}


int main()
{
    setlocale (LC_ALL,".1251");
    char s[100];
    int i,n,j;
    int *arr;
    printf("Пожалуйста вводите отрицания через !, например: !A \n");
    scanf("%s",s);
    arr=(int*)calloc(26,sizeof(int));
    n=TheInputView(s,arr);
    Table(s,arr,n);
    contrary(s,arr,n)? printf("Формула противоричива") : printf("Формула не противоричива");
    return 0;
}
