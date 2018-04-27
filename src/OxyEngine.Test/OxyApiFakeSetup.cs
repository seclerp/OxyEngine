using FakeItEasy;
using NUnit.Framework;
using OxyEngine.Dependency;
using OxyEngine.Interfaces;

namespace OxyEngine.Test
{
  [SetUpFixture]
  public class OxyApiFakeSetup
  {
    [OneTimeSetUp]
    public void SetupOxyApi()
    {
      Container.Instance.RegisterByName(InstanceName.Api, A.Fake<IOxyApi>());
    }
  }
}