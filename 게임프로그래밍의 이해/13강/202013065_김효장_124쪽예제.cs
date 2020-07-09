// 4장 124쪽 내용 입니다.
// 클래스에 대한 예제 프로그램 입니다.
using System;
class Engine
{
    string name;
    int size;

    public Engine(string n, int s)
    {    // 생성자
        name = n;
        size = s;
    }
    public int Size
    {      // 배기량을 취득하는 속성
        get
        {
            return size;
        }
    }
}

class Car
{
    string name;
    Engine engine;

    public Car(string n, Engine e)
    {
        name = n;
        engine = e;
    }

    public void printInfo()
    {
        Console.WriteLine(name + "(" + engine.Size + "cc)");
    }
}

class Program
{
    static void Main()
    {
        Engine unity = new Engine("유니티엔진", 1234);
        Engine unreal = new Engine("언리얼엔진", 1235);
        Engine daram = new Engine("다람쥐엔진", 1236);

        Car[] cars = new Car[4];
        cars[0] = new Car("포르쉐", unity);
        cars[1] = new Car("람보르기니", unreal);
        cars[2] = new Car("부가티", daram);
        cars[3] = new Car("지게차", daram);

        foreach (Car car in cars)
            car.printInfo();
    }
}
