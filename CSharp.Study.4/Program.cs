using System;

namespace CSharp.Study._4
{
    //第4章 C#的高级特性           



    //4.1 委托
    //委托（delegate）是一种知道如何调用方法的对象。
    //具体来说，委托定义了方法的返回类型（return type）和参数类型（parameter type）。

    //定义委托类型
    delegate int Transformer(int x);
    class Program
    {
        //定义一个与委托类型Transformer兼容的方法，即返回类型为int并有一个int类型的参数
        static int Square(int x) => x * x;

        static void Main(string[] args)
        {
            //创建委托实例
            //将一个方法赋值给一个委托变量就能创建一个委托实例
            Transformer t = Square;  //相当于 Transformer t=new Transtormer(Square);

            //调用
            int result = t(3);  //相当于 int result=t.Invoke(3);

            Console.WriteLine(result);  //answer is 9            
        }
    }





    //4.1.1 用委托书写插件方法
    //public delegate int Transformer(int x);
    //class Util
    //{
    //    //Transform方法是一个高阶函数(high-order function)，因为它是一个以函数作为参数的函数。（返回委托的方法也称为高阶函数）
    //    public static void Transform(int[] values, Transformer t)  //委托参数
    //    {
    //        for (int i = 0; i < values.Length; i++)
    //        {
    //            values[i] = t(values[i]);
    //        }
    //    }
    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        int[] values = { 1, 2, 3 };
    //        Util.Transform(values, Square);  //Hook in the Square method
    //        foreach (int i in values)
    //            Console.WriteLine(i + " ");  //1 4 9
    //    }

    //    static int Square(int x) => x * x;
    //}



    ////4.1.2 多播委托
    ////委托可以使用+和+=运算符联结多个委托实例。
    ////如果一个多播委托拥有非void的返回类型，则调用者将从最后一个出发的方法接收返回值。前面的方法仍然调用，但是
    ////返回值都会被丢弃。大部分调用多播委托的情况都会返回void类型，因此这个细小的差异就没有了。

    //public delegate void ProgressReporter(int percentComplete);
    //public class Util
    //{
    //    public static void HardWork(ProgressReporter p)
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            p(i * 10);  //Invoke delegate
    //            System.Threading.Thread.Sleep(100);  //Simulate hard work
    //        }
    //    }
    //}
    //class Program
    //{
    //    static void WriteProgressToConsole(int percentComplete)
    //        => Console.WriteLine(percentComplete);
    //    static void WritePregressToFile(int percentComplete)
    //        => System.IO.File.WriteAllText("progress.txt", percentComplete.ToString());
    //    static void Main(string[] args)
    //    {
    //        ProgressReporter p = WriteProgressToConsole;
    //        p += WritePregressToFile;
    //        Util.HardWork(p);
    //    }
    //}




    ////4.1.3 实例目标方法和静态目标方法
    ////将一个实例方法赋值给委托对象时，后者不但要维护方法的引用，还需要维护方法所属的实例的引用。
    ////System.Delegate类的Target属性代表这个实例（如果委托引用的是一个静态方法，则该属性值为null）。

    //public delegate void ProgressReporter(int percentComplete);
    //class X
    //{
    //    public void InstanceProgress(int percentComplete)
    //        => Console.WriteLine(percentComplete);
    //    //static public void InstanceProgress(int percentComplete)
    //    //    => Console.WriteLine(percentComplete);
    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        X x = new X();
    //        ProgressReporter p = x.InstanceProgress;
    //        p(99);  //99            
    //        Console.WriteLine(p.Target == x);  //True
    //        Console.WriteLine(p.Method);  //Void InstanceProgress(Int32)

    //        //ProgressReporter p = X.InstanceProgress;
    //        //p(99);
    //        //Console.WriteLine(p.Target==null);
    //        //Console.WriteLine(p.Method);
    //    }
    //}



    ////4.1.4 泛型委托类型
    //public delegate T Transformer<T>(T arg);
    //public class Util
    //{
    //    public static void Transform<T>(T[] values, Transformer<T> t)
    //    {
    //        for(int i = 0; i < values.Length; i++)
    //        {
    //            values[i] = t(values[i]);
    //        }
    //    }
    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        int[] values = { 1, 2, 3 };
    //        Util.Transform(values, Square);  //Hook in Square
    //        foreach (int i in values)
    //            Console.WriteLine(i + " ");
    //    }
    //    static int Square(int x) => x * x;
    //}




    ////4.1.5 Func和Action委托
    ////delegate TResult Func<out TResult>();
    ////delegate TResult Func<in T, out TResult>(T arg);
    ////delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
    ////... and so on, up to T16
    ////delegate void Action();
    ////delegate void Action<in T>(T arg);
    ////delegate void Action<in T1, in T2>(T1 arg1, T2 arg2);
    ////... and so on, up to T16

    ////前面例子中的Transform委托可以用一个带有T类型参数并返回T类型的Func委托代替
    ////public delegate T Transformer<T>(T arg);
    //public class Util
    //{
    //    public static void Transform<T>(T[] values, Func<T, T> t)  //Func<T,T> 替换Transformer<T>
    //    {
    //        for (int i = 0; i < values.Length; i++)
    //        {
    //            values[i] = t(values[i]);
    //        }
    //    }
    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        int[] values = { 1, 2, 3 };
    //        Util.Transform(values, Square);  //Hook in Square
    //        foreach (int i in values)
    //            Console.WriteLine(i + " ");
    //    }
    //    static int Square(int x) => x * x;
    //}



    ////4.1.6 委托和接口
    ////能用委托解决的问题，都可以用接口解决，例如下。
    ////如果以下一个或多个条件成立，委托可能是比接口更好的选择：
    ////-接口内仅定义了一个方法
    ////-需要多播能力
    ////-订阅者需要多次实现接口
    //public interface ITransformer
    //{
    //    int Transform(int x);
    //}
    //public class Util
    //{
    //    public static void TransformAll(int[] values, ITransformer t)
    //    {
    //        for(int i = 0; i < values.Length; i++)
    //        {
    //            values[i] = t.Transform(values[i]);
    //        }
    //    }
    //}
    //class Squarer : ITransformer
    //{
    //    public int Transform(int x)
    //        => x * x;
    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        int[] values = { 1, 2, 3 };
    //        Util.TransformAll(values, new Squarer());
    //        foreach(int i in values)
    //        {
    //            Console.WriteLine(i);
    //        }
    //    }
    //}





    //4.1.7 委托的兼容性

    //4.1.7.1 类型的兼容性
    //即使签名相似，委托类型也互不兼容：
    //delegate void D1();
    //delegate void D2();
    //...
    //D1 d1=Method1;
    //D2 d2=d1;   //Compile-time error
    //但是允许下面的写法：
    //D2 d2=new D2(d1);

    //如果委托实例指向相同的目标方法，则认为它们是等价的：
    //delegate void D();
    //...
    //D d1=Method1;
    //D d2=Method1;
    //Console.WriteLine(d1==d2);   //True
    //如果多播委托按照相同的顺序引用相同的方法，则认为它们是等价的。


    ////4.1.7.2 参数的兼容性
    ////当调用方法时，可以给方法的参数提供更加特定的变量类型，这是正常的多态行为。
    ////基于同样的原因，委托也可以有比它的目标方法参数类型更具体的参数类型，这称为逆变。
    //delegate void StringAction(string s);
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        StringAction sa = new StringAction(ActOnOnject);
    //        sa("hello");  //hello
    //    }
    //    static void ActOnOnject(object o) => Console.WriteLine(o);
    //}
    ////委托仅仅替其他人调用方法。
    ////在本例中，在调用StringAction时，参数类型是string。当这个参数传递给目标方法时，参数隐式向上转换为object。

    ////标准事件模式的设计宗旨是通过使用公共的EventArgs基类来利用逆变特性。
    ////例如，可以用两个不同的委托调用同一个方法，一个传递MouseEventArgs而另一个则传递KeyEventArgs。



    ////4.1.7.3 返回类型的兼容性
    ////调用一个方法时可能得到比请求类型更特定的返回值类型，这也是正常的多态行为。
    ////基于同样的原因，委托的目标方法可能返回比委托声明的返回值类型更加特定的返回值类型，这称为协变。
    //delegate object ObjectRetriever();
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        ObjectRetriever o = new ObjectRetriever(RetrieveString);
    //        object result = o();
    //        Console.WriteLine(result);  //hello
    //    }
    //    static string RetrieveString() => "hello";
    //}
    ////ObjectRetriever期望返回一个object。但若返回object子类也是可以的，这是因为委托的返回类型是协变的。




    //4.1.7.4 泛型委托类型的参数协变
    //如果我们要定义一个泛型委托类型，那么最好参考如下的准则：
    //-将只用于返回值类型的类型参数标记为协变（out）。
    //-将只用于参数的任意类型参数标记为逆变（in）。
    //这样可以依照类型的继承关系自然地进行类型转换。

    //以下（在System命名空间中定义的）委托拥有协变类型参数TResult：
    //delegate TResult Func<out TResult>();
    //它允许如下的操作：
    //Func<string> x=...;
    //Func<object> y=x;

    //而下面（在System命名空间中定义）的委托拥有逆变类型参数T：
    //delegate void Action<in T>(T arg);
    //因而可以执行如下的操作：
    //Action<object> x=...;
    //Action<string> y=x;




    //4.2 事件

    //声明事件最简单的方法是在委托成员的前面加上event关键字：
    //public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);  //Delegate definition
    //public class Broadcaster
    //{
    //      public event PriceChangedHandler PriceChanged;  //Event declaration
    //}



    ////4.2.1 标准事件模式    

    ////标准事件模式的核心是System.EventArgs类，一个预定义的没有成员（但是有一个静态的Empty属性）的类。
    ////EventArgs是为事件传递信息的基类。
    ////在该示例中，我们继承EventArgs以便在PriceChanged事件触发时传递新的和旧的Price值。
    ////考虑到复用性，EventArgs子类应当根据它包含的信息来命名（而非根据使用它的事件命名）。
    ////它一般将数据以属性或只读字段的方式暴露给外界。
    //public class PriceChangedEventArgs : EventArgs
    //{
    //    public readonly decimal LastPrice;
    //    public readonly decimal NewPrice;

    //    public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
    //    {
    //        LastPrice = lastPrice;
    //        NewPrice = newPrice;
    //    }

    //}
    //public class Stock
    //{
    //    string symbol;
    //    decimal price;
    //    public Stock(string symbol)
    //    {
    //        this.symbol = symbol;
    //    }
    //    //EventArgs子类就位后，下一步就是选择或者定义事件的委托了。这一步需要遵循三条规则：
    //    //-委托必须以void作为返回值。
    //    //-委托必须接受两个参数，第一个参数是object类型，第二个参数则是EventArgs的子类。第一个参数表明了事件的广播者，第二个参数包含了需要传递的额外信息。
    //    //-委托的名称必须以EventHandler结尾。
    //    //框架定义了一个名为System.EventHandler<>的泛型委托，该委托满足以上提到的三个条件：
    //    //public delegate void EventHandler<TEventArgs>
    //    //  (object source, TEventArgs e) where TEventArgs: EventArgs;
    //    //接下来就是定义选定委托类型的事件了。这里使用泛型的EventHandler委托
    //    public event EventHandler<PriceChangedEventArgs> PriceChanged;

    //    //最后，该模式需要编写一个protected的虚方法来触发事件。
    //    //方法名必须和事件名称一致，以On作为前缀，并接收唯一的EventArgs参数
    //    protected virtual void OnPriceChanged(PriceChangedEventArgs e)
    //    {
    //        //在多线程情形下，为了保证线程安全，在测试和调用委托之前，需要将它保存在一个临时变量中：
    //        //var temp=PriceChanged;
    //        //if(temp != null) temp(this,e);
    //        //我们可以使用C# 6的null条件运算符来避免临时变量的声明：
    //        PriceChanged?.Invoke(this, e);
    //    }
    //    public decimal Price 
    //    {
    //        get { return price; }
    //        set {
    //            if (price == value) return;
    //            decimal oldPrice = price;
    //            price = value;
    //            OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
    //        } 
    //    }
    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Stock stock = new Stock("THPW");
    //        stock.Price = 27.10M;
    //        //Register with the PriceChanged event
    //        stock.PriceChanged += stock_PriceChanged;
    //        stock.Price = 31.59M;
    //    }
    //    static void stock_PriceChanged(object sender, PriceChangedEventArgs e)
    //    {
    //        if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
    //            Console.WriteLine("Alert, 10% stock price increase! ");
    //    }
    //}








    //4.3 Lambda表达式
    //Lambda表达式是一种可以替代委托实例的匿名方法。

    //委托类型：
    //delegate int Transformer(int i);
    //可使用Lambda表达式 x=> x * x 赋值，也可以调用该表达式：
    //Transformer sqr= x=>x*x;  //这里x相当于委托的参数，x*x对应委托的返回类型。
    //Console.WriteLine(sqr(3));  //9
    //Lambda表达式的每一个参数对应委托的一个参数，而表达式的类型（可以是void）对应着委托的返回类型。
    //本例中，x对应参数i，而表达式x*x的类型对应着返回值类型int。
    //Lambda表达式的代码除了表达式之外还可以是语句块，因此我们可以把上例改写成：
    //x => {return x*x;};

    //Lambda表达式通常和Func和Action委托一起使用，因此前面的表达式通常写成如下形式：
    //Func<int,int> sqr= x=>x*x;

    //以下是带有两个参数的表达式示例：
    //Func<string,string,int> totalLength=(s1,s2)=>s1.Length+s2.Length;
    //int total=totalLength("hello","world");  //total is 10


    //4.3.1 显式指定Lambda参数的类型
    //编译器通常可以根据上下文推断出Lambda表达式的类型，但是当无法推断的时候则必须显式指定每一个参数的类型。
    //Bar((int x)=>Foo(x));
    //Bar<int>(x=>Foo(x));
    //Bar<int>(Foo);


    //4.3.2 捕获外部变量
    //Lambda表达式可以引用方法内定义的局部变量和方法的参数（外部变量，outer variables）。例如
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        int factor = 2;
    //        Func<int, int> multiplier = n => n * factor;
    //        Console.WriteLine(multiplier(3));  //6
    //    }
    //}
    //Lambda表达式所引用的外部变量称为捕获变量（captured variable）。
    //捕获变量的表达式称为闭包（closure）。

    //捕获的变量会在真正调用委托时赋值，而不是在捕获时赋值：
    //int factor=2;
    //Func<int,int> multiplier=n=>n*factor;
    //factor=10;
    //Console.WriteLine(multiplier(3));  //30

    //Lambda表达式也可以更新捕获的变量的值：
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        int seed = 0;
    //        Func<int> natural = () => seed++;  //c = a++: 先将a赋值给c，再对a进行自增运算。   c = ++a: 先将a进行自增运算，再将a赋值给c。
    //        Console.WriteLine(natural());  //0
    //        Console.WriteLine(natural());  //1
    //        Console.WriteLine(natural());  //2
    //    }
    //}

    //捕获变量的生命周期延伸到了和委托的生命周期一致。
    //在以下例子中，局部变量seed本应该在Natural执行完毕后从作用域中消失，但由于seed被捕获，因此其生命周期已经和捕获它的委托natural一致了。
    //class Program
    //{
    //    static Func<int> Natural()
    //    {
    //        int seed = 0;
    //        return () => seed++;  //Returns a closure
    //    }
    //    static void Main(string[] args)
    //    {
    //        Func<int> natural = Natural();
    //        Console.WriteLine(natural());  //0
    //        Console.WriteLine(natural());  //1
    //    }
    //}

    //在Lambda表达式内实例化的局部变量在每一次调用委托实例期间都是唯一的。
    //如果我们把上述例子改成在Lambda表达式内实例化seed，则程序的结果（当然这个结果不是我们期望的）将与之前不同：
    //class Program
    //{
    //    static Func<int> Natural()
    //    {
    //        return()=>{ int seed = 0; return seed++; };
    //    }
    //    static void Main(string[] args)
    //    {
    //        Func<int> natural = Natural();
    //        Console.WriteLine(natural());  //0
    //        Console.WriteLine(natural());  //0
    //    }
    //}

    //捕获迭代变量
    //当捕获for循环的迭代变量时，C#会认为该变量是在循环体外定义的。
    //而这意味着同一次变量在每一次迭代都被捕获了，因此程序输出333而非012
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Action[] actions = new Action[3];
    //        for(int i = 0; i < 3; i++)
    //        {
    //            actions[i] = () => Console.WriteLine(i);
    //        }
    //        foreach (Action a in actions)
    //            a();  //333
    //    }
    //}
    //如果我们真的希望输出012，那么需要将循环变量指定到循环内部的局部变量中：
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Action[] actions = new Action[3];
    //        for(int i = 0; i < 3; i++)
    //        {
    //            int loopScopedi = i;
    //            actions[i] = () => Console.WriteLine(loopScopedi);
    //        }
    //        foreach(Action a in actions)
    //        {
    //            a();
    //        }
    //    }
    //}
    //由于loopScopedi对于每一次迭代都是新创建的，因此每一个闭包都将捕获不同的变量。




    //4.4 匿名方法
    //匿名方法的写法是在delegate关键字后面跟上参数的声明（可选），然后是方法体。
    //例如，以下面的委托为例：
    //delegate int Transformer(int i);    
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        //如下是实现和调用匿名方法的示例：
    //        Transformer sqr = delegate (int x) { return x * x; };
    //        //上一行代码语义上等同于下面的Lambda表达式：
    //        //Transformer sqr = (int x) => { return x * x; };
    //        //或者更简单的：
    //        //Transformer sqr = x => x * x;
    //        Console.WriteLine(sqr(3));  //9
    //    }
    //}
    //匿名方法和Lambda表达式捕获外部变量的方式是完全一样的。




    /*
    
    */
}
