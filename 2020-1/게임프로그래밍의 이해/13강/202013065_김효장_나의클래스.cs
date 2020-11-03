using System;

class Fruit // 과일 클래스
{
    string name; // 과일 이름
    int count; // 과일 갯수

    public Fruit(string n, int c) // 과일 생성자
    {
        name = n;
        count = c;
    }

    public string Name // 이름
    {
        get
        {
            return name;
        }
    }

    public int milliliter // 과일 갯수에 따라 밀리리터를 계산해서 보여줌
    {
        get
        {
            return count * 100;
        }
    }
}

class Juice // 주스 클래스
{
    Fruit fruit; // 과일 클래스를 가지고 있다.
    public Juice(string n, int s) // 주스 생성자
    {
        fruit = new Fruit(n, s); // 과일 생성
    }

    public void PrintInfo() // 출력
    {
        Console.WriteLine(fruit.Name + "Juice " + fruit.milliliter + "ml"); // 과일에 따른 주스 량을 출력
    }
}


class Program
{
    static void Main()
    {
        Juice applejuice = new Juice("apple", 1); // 사과 주스
        Juice pineapplejuice = new Juice("pineapple", 2); // 파인애플 주스
        Juice bananajuice = new Juice("banana", 3); // 바나나 주스


        // 출력 부문
        applejuice.PrintInfo();
        pineapplejuice.PrintInfo();
        bananajuice.PrintInfo();
    }
}
