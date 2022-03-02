namespace ECS.Test.ManualFakes
{
    internal class FakeTempSensor : ITempSensor
    {
        public int Temp { get; set; }
        public bool SelfTestResult { get; set; }

        public FakeTempSensor()
        {
            Temp = 0;
            SelfTestResult = true;
        }

        public int GetTemp()
        {
            return Temp;
        }
        public bool RunSelfTest()
        {
            return SelfTestResult;
        }
    }

    internal class FakeHeater : IHeater
    {
        public int TurnOffCalledTimes { get; set; }
        public int TurnOnCalledTimes { get; set; }
        public bool SelfTestResult { get; set; }

        public FakeHeater()
        {
            TurnOffCalledTimes = 0;
            TurnOnCalledTimes = 0;
            SelfTestResult = true;
        }

        public void TurnOn()
        {
            ++TurnOnCalledTimes;
        }

        public void TurnOff()
        {
            ++TurnOffCalledTimes;
        }

        public bool RunSelfTest()
        {
            return SelfTestResult;
        }
    }

    internal class FakeWindow : IWindow
    {
        public int CloseCalledTimes { get; set; }
        public int OpenCalledTimes { get; set; }
        public bool SelfTestResult { get; set; }

        void IWindow.Close()
        {
            CloseCalledTimes++;
        }

        void IWindow.Open()
        {
            OpenCalledTimes++;
        }

        bool IWindow.RunSelfTest()
        {
            return SelfTestResult;
        }
    }
}
