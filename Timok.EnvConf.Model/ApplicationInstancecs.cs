namespace Timok.EnvConf.Model {
  public class ApplicationInstance : Application {
    public ApplicationInstance(string pName, EnvConfiguration pEnvironment) : base(pName) {
      Environment = pEnvironment;
    }

    public EnvConfiguration Environment { get; private set; }
  }
}