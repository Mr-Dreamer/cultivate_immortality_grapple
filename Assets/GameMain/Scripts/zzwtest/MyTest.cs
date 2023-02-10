using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapple.Test
{
    class MyTest
    {
        MyClass<One> MyClass = new MyClass<One>();
        public static bool AreEqual<T>(T left, T right) where T : IComparer<T> => left.Equals(right);

    }

    class MyClass<T> where T : One, new()
    {

    }

    class One : All
    {
        private string name = null;
    }

    class Two
    {

    }
    class All
    {

    }

    interface InterfaceOne
    {

    }

    public class Person : OtherPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person(string name = "default", int age = 1)
        {
            Name = name;
            Age = age;
        }
    }

    public class OtherPerson
    {

    }
}
