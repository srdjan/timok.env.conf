namespace Timok.EnvConf.Model {
  public class Application {
    public Application(string pName) {
      Name = pName;
      RootPath = ".";
    }

    public string Name { get; private set; }
    public string RootPath { get; private set; }
  }
}