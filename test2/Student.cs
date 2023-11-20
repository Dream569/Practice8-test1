using System;

namespace IPractice
{
    public class Student : IHuman, IComparable, ICloneable
    {
        public string Surname { get; private set; }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public Student(string surname, string name, int age)
        {
            if (age < 0)
                throw new ArgumentException(nameof(age));

            Surname = surname;
            Name = name;
            Age = age;
        }
        public object Clone()
        {
            return new Student(Surname, Name, Age);
        }

        public int CompareTo(object obj)
        {
            if (!(obj is IHuman))
                return 1;

            return Surname.CompareTo(((IHuman)obj).Surname);
        }
        public override string ToString()
        {
            return $"[Студент] {Surname} {Name}, ему {Age}";
        }
    }
}
