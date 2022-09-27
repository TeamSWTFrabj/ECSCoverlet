using System;
using NSubstitute;
using NUnit.Framework;

namespace ECS.Test.NSubstituteFakes
{
    public class ECSNSubstituteFakes
    {
        private IHeater _fakeHeater;
        private ITempSensor _fakeTempSensor;
        private IWindow _fakeWindow;
        private ECS _uut;

        [SetUp]
        public void SetUp()
        {
            _fakeHeater = Substitute.For<IHeater>();
            _fakeTempSensor = Substitute.For<ITempSensor>();
            _fakeWindow = Substitute.For<IWindow>();

            _uut = new ECS(_fakeTempSensor, _fakeHeater, _fakeWindow, 23, 27);
        }

        #region TempLow
        [Test]
        public void Regulate_TempIsLow_HeaterIsTurnedOn()
        {
            // Setup stub with desired response
            _fakeTempSensor.GetTemp().Returns(20);
            // Act
            _uut.Regulate();

            // Assert on the mock - was the heater called correctly
            _fakeHeater.Received(1).TurnOn();
        }


        [Test]
        public void Regulate_TempIsLow_WindowIsClosed()
        {
            // Setup stub with desired response
            _fakeTempSensor.GetTemp().Returns(20);
            // Act
            _uut.Regulate();

            // Assert on the mock - was the window called correctly
            _fakeWindow.Received(1).Close();
        }

        #endregion

        #region TempBetween
        [Test]
        public void Regulate_TempBetween_HeaterIsTurnedOff()
        {
            // Setup stub with desired response
            _fakeTempSensor.GetTemp().Returns(25);
            // Act
            _uut.Regulate();

            // Assert on the mock - was the heater called correctly
            _fakeHeater.Received(1).TurnOff();
        }


        [Test]
        public void Regulate_TempBetween_WindowIsClosed()
        {
            // Setup stub with desired response
            _fakeTempSensor.GetTemp().Returns(25);
            // Act
            _uut.Regulate();

            // Assert on the mock - was the window called correctly
            _fakeWindow.Received(1).Close();
        }

        #endregion


        #region TempHigh
        [Test]
        public void Regulate_TempIsAboveUpperThreshold_HeaterOff()
        {
            // Setup the stub with desired response
            _fakeTempSensor.GetTemp().Returns(28);
            _uut.Regulate();

            // Assert on the mock - was the heater called correctly
            _fakeHeater.Received(1).TurnOff();
        }

        [Test]
        public void Regulate_TempIsAboveUpperThreshold_WindowOpen()
        {
            // Setup the stub with desired response
            _fakeTempSensor.GetTemp().Returns(28);
            _uut.Regulate();

            // Assert on the mock - was the heater called correctly
            _fakeWindow.Received(1).Open();
        }

        #endregion

        #region SelfTest
        [TestCase(true, true, true, true)]
        [TestCase(true, true, false, false)]
        [TestCase(true, false, true, false)]
        [TestCase(true, false, false, false)]
        //[TestCase(false, true, true, false)]
        //[TestCase(false, true, false, false)]
        //[TestCase(false, false, true, false)]
        //[TestCase(false, false, false, false)]
        public void RunSelfTest_CombinationOfInput_CorrectOutput(
            bool tempResult, bool heaterResult, bool windowResult, bool expectedResult)
        {
            _fakeTempSensor.RunSelfTest().Returns(tempResult);
            _fakeHeater.RunSelfTest().Returns(heaterResult);
            _fakeWindow.RunSelfTest().Returns(windowResult);

            Assert.That(_uut.RunSelfTest(), Is.EqualTo(expectedResult));
        }
        #endregion

        #region ThresholdExceptions
        [Test]
        public void Thresholds_ValidUpperTemperatureThresholdSet_NoExceptionsThrown()
        {
            // Check that it doesn't throw
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.UpperTemperatureThreshold = 27; }, Throws.Nothing);
        }

        [Test]
        public void Thresholds_ValidLowerTemperatureThresholdSet_NoExceptionsThrown()
        {
            // Check that it doesn't throw 
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.LowerTemperatureThreshold = 26; }, Throws.Nothing);
        }

        [Test]
        public void Thresholds_UpperSetToLower_NoExceptionsThrown()
        {
            // Check that it doesn't throw when they are equal
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.UpperTemperatureThreshold = _uut.LowerTemperatureThreshold; }, Throws.Nothing);
        }

        [Test]
        public void Thresholds_LowerSetToUpper_NoExceptionsThrown()
        {
            // Check that it doesn't throw when they are equal
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.LowerTemperatureThreshold = _uut.UpperTemperatureThreshold; }, Throws.Nothing);
        }

        [Test]
        public void Thresholds_InvalidUpperTemperatureThresholdSet_ArgumentExceptionThrown()
        {
            // Check that it throws when upper is illegal
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.UpperTemperatureThreshold = 22; }, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Thresholds_InvalidLowerTemperatureThresholdSet_ArgumentExceptionThrown()
        {
            // Check that it throws when lower is illegal
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.LowerTemperatureThreshold = 29; }, Throws.TypeOf<ArgumentException>());
        }

        #endregion


    }

}