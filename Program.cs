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

        public static void Main()
        {
            // Using a delegate with an instance of a class
            Dog myPet = new Dog();
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
        }
    }
    class Dog
    {
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