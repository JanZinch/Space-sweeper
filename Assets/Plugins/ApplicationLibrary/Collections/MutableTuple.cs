namespace CodeBase.ApplicationLibrary.Collections
{
    public class MutableTuple<T1, T2>
    {
        public MutableTuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
    }
}