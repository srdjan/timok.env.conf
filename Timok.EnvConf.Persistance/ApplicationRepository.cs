using System.Collections.Generic;
using Timok.EnvConf.Model;

namespace Timok.EnvConf.Persistance {
  public class ApplicationRepository {
    static readonly Dictionary<string, Application> applications;

    static ApplicationRepository() {
      applications = new Dictionary<string, Application> {
                                                           { "App1", new Application("App1") },
                                                           { "App2", new Application("App2") }
                                                         };
    }

    public Application Get(string pAppName) {
      return applications[pAppName];
    }
  }
}