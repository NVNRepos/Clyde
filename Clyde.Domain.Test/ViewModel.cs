using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clyde.Domain.Test
{
    [TestClass]
    public class ViewModel
    {
        private ClydeViewModelBase viewModel;

        [TestInitialize]
        public void Initialize()
        {
            viewModel = new ClydeViewModelBase(new StateHolderBehaviorDummy());
        }

        [TestMethod]
        public void Toggle()
        {
            //initial
            Assert.IsTrue(viewModel.CanStart);
            Assert.IsFalse(viewModel.CanStop);

            viewModel.ToggleState();

            Assert.IsFalse(viewModel.CanStart);
            Assert.IsTrue(viewModel.CanStop);

            viewModel.ToggleState();
            
            Assert.IsTrue(viewModel.CanStart);
            Assert.IsFalse(viewModel.CanStop);
        }

        [TestMethod]
        public void Start()
        {
            //initial
            Assert.IsTrue(viewModel.CanStart);
            Assert.IsFalse(viewModel.CanStop);

            viewModel.StartHolder();

            Assert.IsFalse(viewModel.CanStart);
            Assert.IsTrue(viewModel.CanStop);

            viewModel.StartHolder();

            Assert.IsFalse(viewModel.CanStart);
            Assert.IsTrue(viewModel.CanStop);
        }

        [TestMethod]
        public void Stop()
        {
            Start();
            viewModel.StopHolder();

            Assert.IsTrue(viewModel.CanStart);
            Assert.IsFalse(viewModel.CanStop);

            viewModel.StopHolder();

            Assert.IsTrue(viewModel.CanStart);
            Assert.IsFalse(viewModel.CanStop);
        }

        [TestMethod]
        public void Dispose()
        {
            viewModel.Dispose();

            Assert.IsFalse(viewModel.CanStart);
            Assert.IsFalse(viewModel.CanStop);

            viewModel.StartHolder();

            Assert.IsFalse(viewModel.CanStart);
            Assert.IsFalse(viewModel.CanStop);

            viewModel.StopHolder();

            Assert.IsFalse(viewModel.CanStart);
            Assert.IsFalse(viewModel.CanStop);
        }
    }
}
