namespace Program
{

    class App
    {
        // Delegate is a type that represents a reference to a method.
        // They are useful for creating reusable method signatures (they dont define behavior, the methods assigned to it does)

        // This delegate type returns void and takes a string
        // In this example we are using it to delegate methods of the Dog class
        public delegate void MyDelegate(string s);


        // This delegate type takes a string and returns a string
        // In this example we are using it to declare an anonymous function
        public delegate string MyGreeting(string s);

        // This delegate takes two values of type T and returns a value of type T
        public delegate T Calculator<T>(T num1, T num2);

        // This is a generic methodn that takes a generic delegate
        // This method returns type T and takes two parameters of type T as well as a generic delegate
        public static T Operate<T>(T a, T b, Calculator<T> operation)
        {
            return operation(a, b);
        }

        // We can do the same as above without using a custom delegate, but a built in one
        public static T OperateV2<T>(T a, T b, Func<T, T, T> operation)
        {
            return operation(a, b);
        }

        // Same as above, but OperateV3 now returns a different type, but takes type T
        // Its written with lambda operator, but pretty much the same thing as above
        // Essentially operation is a callback function
        public static TResult OperateV3<T, TResult>(T a, T b, Func<T, T, TResult> operation) => operation(a, b);
        public static void Main()
        {
            // Using a delegate with an instance of a class
            Dog myPet = new Dog("Ella", 4);
            myPet.makeSound("barks"); // Calling makeSound directly from the class

            MyDelegate del = myPet.makeSound;
            del("Woof woof"); // Calling directly from the delegate

            // Using a delegate with a static method
            MyDelegate del2 = Dog.sleep;
            del2("the dog sleeps without making a sound"); // Calling the delegate directly from without making an instance

            // We can cast multiple delegates by creating a new delegate
            MyDelegate multiCast = del + del2;
            multiCast("standard sound"); // with one single string, equivalent to = sleep("standard sound"), makesound("standardsound")

            // Anonymous methods - A method without a name defined in line with the delegate keyword and assign it directly to the delegate type
            // Instead of writing a named method, and assigning it to the Delegate method, we can just declare it inline with no name  using the delegate keyword.
            MyGreeting norwegian = delegate (string message)
            {
                return message;
            };

            Console.WriteLine(norwegian("Heisann!"));

            // Lamba expressions
            // When using a lambda expression, we can ommit the delegate keyword.
            // We can also ommit the parameter type as its infered through the delegate method declared earlier
            MyGreeting spanish = message => message;
            Console.WriteLine(spanish("Hola!"));

            // Built-in deleagetes are delegates than are already available to use, so we dont have to create custom ones.
            Func<string, string> mandarin = message => message; // A Func delegate takes a type and returns a type, like our MyGreeting delegate
            Action<int, int> sum = (a, b) => Console.WriteLine(a + b); // An Action delegate performs a task, but returns no value
            Predicate<int> isEven = a => a % 2 == 0; // A Predicate delegate takes a type and returns a boolean after performing a conditional check

            Console.WriteLine(mandarin("Ni hao!"));
            Console.WriteLine(isEven(5)); // False
            Console.WriteLine(isEven(4)); // True
            sum(5, 10); // 15

            // Generic Delegates simply specifies that it can return any type, and take any type of paramters
            // The logic of the method passed to a generic delegate must support the types
            // addInt works as long as the arguments passed are ints.
            // addDouble works as long as the arguments passed are doubles.
            // Essensitally, the Calculator delegate can be replaced by Func<T, T, T>, because they achieve the same thing in this example
            // Meaning we can just replace it with Func<int, int, int> addInt = (a, b) => a + b; instead
            Calculator<int> addInt = (a, b) => a + b; // takes two ints and returns int
            Calculator<double> addDouble = (a, b) => a + b; // same as above, but returns a double type
            Calculator<string> concat = (a, b) => a + b; // takes a string and returns a string
            Calculator<Dog> compareDogs = (dog1, dog2) => dog1.Age > dog2.Age ? dog1 : dog2; // takes two dog instances and returns a dog instance

            Dog oldest = compareDogs(new Dog("Rex", 5), new Dog("Buddy", 3));

            Console.WriteLine(addInt(4, 4)); // 8
            Console.WriteLine(addDouble(2.3, 3.2)); // 5.5
            Console.WriteLine(concat("2", "2")); // 22
            Console.WriteLine(oldest.Name + " is " + oldest.Age + " years old and is oldest.");

            // Generic Method that takes a Generic Delegate
            // Here the types are explict
            // A more powerful way of using generic
            int result1 = Operate<int>(50, 50, addInt);
            string result2 = Operate<string>("Hello ", "World", concat);

            Console.WriteLine(result1); // 100
            Console.WriteLine(result2);

            // Using a built in delegate instead
            // Here the types are inferred
            // OperateV2 returns int, takes int a, int b and func
            // func infers int x, int y and return int from the expression
            Console.WriteLine(OperateV2(100, 100, (x, y) => x + y)); // 200

            // Lets take it one step further, say we want to return a string instead
            // We can adjust the method to return string only, but still take ints
            string result3 = OperateV3<int, string>(5, 5, (a, b) => (a + b).ToString());
            Console.WriteLine(result3); // 10
            Console.WriteLine(result3.GetType().Name); // String
        }
    }
    class Dog
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Dog(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public void makeSound(string sound)
        {
            Console.WriteLine(sound);
        }

        public static void sleep(string sound)
        {
            Console.WriteLine(sound);
        }
    }


}