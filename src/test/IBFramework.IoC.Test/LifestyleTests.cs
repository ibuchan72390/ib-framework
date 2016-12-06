﻿using IBFramework.Core.Enum;
using IBFramework.Core.IoC;
using IBFramework.IoC;
using IBFramework.TestHelper;
using Xunit;

namespace IBFramework.IoC.Test
{
    public class LifestyleTests
    {
        private IContainerGenerator _sut;

        public LifestyleTests()
        {
            _sut = new ContainerGenerator();
        }

        [Fact]
        public void Singleton_Registration_Creates_One_And_Only_One_Instance()
        {
            _sut.Register<TestClass>().As<ITestInterface>().WithLifestyle(RegistrationLifestyleType.Singleton);

            var container = _sut.GenerateContainer();

            var v1 = container.Resolve<ITestInterface>();
            var v2 = container.Resolve<ITestInterface>();
            var v3 = container.Resolve<ITestInterface>();

            Assert.Same(v1, v2);
            Assert.Same(v2, v3);
            Assert.Same(v1, v3);
        }

        [Fact]
        public void Transient_Instance_Creates_New_Instance_Each_Time()
        {
            _sut.Register<TestClass>().As<ITestInterface>().WithLifestyle(RegistrationLifestyleType.Transient);

            var container = _sut.GenerateContainer();

            var v1 = container.Resolve<ITestInterface>();
            var v2 = container.Resolve<ITestInterface>();
            var v3 = container.Resolve<ITestInterface>();

            Assert.NotSame(v1, v2);
            Assert.NotSame(v2, v3);
            Assert.NotSame(v1, v3);
        }

    }
}
