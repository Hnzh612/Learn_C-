namespace EventApplication
{
    /// <summary>
    /// 发布器类
    /// </summary>
    public class EventTest
    {
        private int value;
        public delegate void NumManipulationHandler();

        public event NumManipulationHandler ChangeNum;
        protected virtual void OnNumChanged()
        {
            if (ChangeNum != null)
            {
                ChangeNum();
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadLine();
            }
        }
        public void SetValue(int n)
        {
            if (value != n)
            {
                value = n;
                OnNumChanged();
            }
        }
        public EventTest()
        {
            int n = 5;
            SetValue(n);
        }
    }

    /// <summary>
    /// 订阅器类
    /// </summary>
    public class subscribEvent
    {
        public void printf()
        {
            Console.WriteLine("event fire");
            Console.ReadKey(); /* 回车继续 */
        }
    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        EventTest e = new EventTest();
    //        subscribEvent v = new subscribEvent();
    //        e.ChangeNum += new EventTest.NumManipulationHandler(v.printf);
    //        e.SetValue(7);
    //        e.SetValue(10);
    //    }
    //}
}