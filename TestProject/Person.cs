using MiCS;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ScriptLibrary1
{
    public class Person
    {
        private string _Name;
        public int MyProperty
        {
            get
            {
                return 2;
            }
        }

        public Person(string name)
        {
            _Name = name;
        }

        [MixedSide]
        public void MethodWithoutMixedSideAttribute()
        {
            //Console.WriteLine("LOL! I am not a mixed side function!");
        }

        [MixedSide]
        public void TestFunction(string name, string name2, string name3)
        {
            string[] lol = { "tomsa", "lolol" };

            _Name = "TestFunctionName";
            Person p = new Person("Tomas");
            p.MethodWithoutMixedSideAttribute();
        }
    }
}