using System;

/*
Источник: https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/operator-overloading

Fraction - упрощенная структура, представляющая рациональное число.
Необходимо перегрузить операции:
+ (бинарный)
- (бинарный)
*
/ (в случае деления на 0, выбрасывать DivideByZeroException)

Тестирование приложения выполняется путем запуска разных наборов тестов, например,
на вход поступает две строки, содержацие числители и знаменатели двух дробей, разделенные /, соответственно.
1/3
1/6
Программа должна вывести на экран сумму, разность, произведение и частное двух дробей, соответственно,
с использованием перегруженных операторов (при необходимости, сокращать дроби):
1/2
1/6
1/18
2

Обратите внимание, если дробь имеет знаменатель 1, то он уничтожается (2/1 => 2). Если дробь в числителе имеет 0, то 
знаменатель также уничтожается (0/3 => 0).
Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но вывод меняться не должен.
*/

public readonly struct Fraction
{
    private readonly int num;
    private readonly int den;

    public Fraction(int numerator, int denominator)
    {
        if (denominator != 0)
        {
            if (denominator > 0)
            {
                num = numerator;
                den = denominator;
            }
            else
            {
                num = -numerator;
                den = -denominator;
            }

            var gcd = FindGCD(Math.Abs(num), Math.Abs(den));
            num /= gcd;
            den /= gcd;
        }
        else
        {
            throw new ArgumentException();
        }
    }

    public static int FindGCD(int a, int b)
    {
        return b == 0 ? a : FindGCD(b, a % b);
    }
    public static Fraction operator +(Fraction a, Fraction b)
    {
        return new Fraction(a.num * b.den + b.num * a.den,
            a.den * b.den);
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
        return new Fraction(a.num * b.den - b.num * a.den,
            a.den * b.den);
    }
    public static Fraction operator *(Fraction a, Fraction b)
    {
        return new Fraction(a.num * b.num, a.den * b.den);
    }
    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b.num != 0)
        {
            return new Fraction(a.num * b.den, a.den * b.num);
        }
        else
        {
            throw new DivideByZeroException();
        }
    }

    public override string ToString()
    {
        if (num == 0)
        {
            return "0";
        }
        if (den == 1)
        {
            return $"{num}";
        }
        return $"{num}/{den}";
    }
}

public static class OperatorOverloading
{
    public static void Main()
    {
        try
        {
            var input1 = Console.ReadLine().Split('/');
            var input2 = Console.ReadLine().Split('/');
            var a = new Fraction(int.Parse(input1[0]), int.Parse(input1[1]));
            var b = new Fraction(int.Parse(input2[0]), int.Parse(input2[1]));
            Console.WriteLine(a + b);
            Console.WriteLine(a - b);
            Console.WriteLine(a * b);
            Console.WriteLine(a / b);

        }
        catch (ArgumentException)
        {
            Console.WriteLine("error");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("zero");
        }
    }
}
