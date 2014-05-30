using System;
using Timok.EnvConf.Service;

namespace Timok.EnvConf.DeploymentFake {
  internal class Program {
    static void Main(string[] args) {
      var ENV = args[0];
      var app = args[1];

      var configTransformer = new ConfigProcessor(ENV, app);
      configTransformer.Update();

      Console.WriteLine("Success! Press ENTER to exit.");
      Console.ReadLine();
    }
  }
}