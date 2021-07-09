using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> dataSource = new List<Person>()
            {
                new Person() { Age = 32, Name = "张" },
                new Person() { Age = 20, Name = "刘" },
                new Person() { Age = 43, Name = "马" },
                new Person() { Age = 12, Name = "刘" },
                new Person() { Age = 17, Name = "陈" }
            };
            for (int i = 0; i < dataSource.Count; i++)
                Console.Write("\t" + dataSource[i]);
            Console.WriteLine();

            // 使用接口，需要再定义一个类
            List<Person> list1 = dataSource.ToList();
            list1.Sort(new PersonComparer());
            for (int i = 0; i < list1.Count; i++)
                Console.Write("\t" + list1[i]);
            Console.WriteLine();

            // 使用预制的泛型类

            List<Person> list2 = dataSource.ToList();
            list2.Sort(new Comparison<Person>((x, y) => x.Age - y.Age));
            for (int i = 0; i < list2.Count; i++)
                Console.Write("\t" + list2[i]);
            Console.WriteLine();

            // 使用LINQ，最简单

            List<Person> list3 = dataSource.ToList();
            list3 = list3.OrderBy(x => x.Age).ToList();
            for (int i = 0; i < list3.Count; i++)
                Console.Write("\t" + list3[i]);
            Console.WriteLine();

            List<Person> list5 = dataSource.ToList();
            Person p = list5.OrderBy(x => x.Age).Where(x => x.Name=="刘").FirstOrDefault();
            Console.WriteLine();

            List<Person> list4 = dataSource.ToList();
            PersonPrint personPrint = new PersonPrint();
            for (int i = 0; i < list4.Count; i++)
                Console.Write(personPrint.Print(list4[i]));
            Console.ReadKey();
        }
    }
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return Name + "[" + Age + "]";
        }
    }


    class PersonComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return x.Age - y.Age;
        }
    }
    public interface IPrintPerson<T>
    {
        string Print(T x);
        string Print(T x, T y);
    }
    class PersonPrint: IPrintPerson<Person>
    {
        public string Print(Person x, Person y)
        {
            return x.ToString ()+ y.Age.ToString();
        }
        public string Print(Person x)
        {
            return x.ToString();
        }
    }
}