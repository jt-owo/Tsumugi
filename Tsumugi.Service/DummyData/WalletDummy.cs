using System;

namespace Tsumugi.Service.DummyData
{
    public class WalletDummy
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public double Sum { get; set; }

        public WalletDummy() { }

        public WalletDummy(string name, double sum)
        {
            ID = Guid.NewGuid();
            UserID = Guid.NewGuid();
            Name = name;
            Sum = sum;
        }
    }
}
