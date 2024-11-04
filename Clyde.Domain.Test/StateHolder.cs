using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clyde.Domain.Test
{
    [TestClass]
    public class StateHolder
    {
        private IStateHolder _stateHolder;

        [TestInitialize]
        public void TestInitialize()
            => _stateHolder = new StateHolderBehavoirDummy();

        [TestMethod]
        public void Start()
        {
            Assert.IsFalse(_stateHolder.Stop());
            Assert.AreEqual(ExecutionState.Idle, _stateHolder.State);
            Assert.IsTrue(_stateHolder.Start());
            Assert.AreEqual(ExecutionState.Running, _stateHolder.State);
            Assert.IsFalse(_stateHolder.Start());
            Assert.AreEqual(ExecutionState.Running, _stateHolder.State);
        }

        [TestMethod]
        public void Stop()
        {
            if (_stateHolder.State != ExecutionState.Running)
            {
                Assert.IsTrue(_stateHolder.Start());
            }
            Assert.AreEqual(ExecutionState.Running, _stateHolder.State);
            Assert.IsTrue(_stateHolder.Stop());
            Assert.AreEqual(ExecutionState.Idle, _stateHolder.State);
            Assert.IsFalse(_stateHolder.Stop());
            Assert.AreEqual(ExecutionState.Idle, _stateHolder.State);
        }

        [TestMethod]
        public void Dispose()
        {
            _stateHolder.Dispose();
            Assert.AreEqual(ExecutionState.Disposed, _stateHolder.State);

            Assert.IsFalse(_stateHolder.Start());
            Assert.AreEqual(ExecutionState.Disposed, _stateHolder.State);
            Assert.IsFalse(_stateHolder.Stop());
            Assert.AreEqual(ExecutionState.Disposed, _stateHolder.State);
        }
    }
}
