using System.Collections.Generic;
using Timok.EnvConf.Model;

namespace Timok.EnvConf.Persistance {
  public class EnvConfigurationRepository {
    static readonly Dictionary<string, EnvConfiguration> environments;

    static EnvConfigurationRepository() {
      environments = new Dictionary<string, EnvConfiguration>();

      var qaDynamicConfigValues = new ConfigurationElement {
                                                             Key = "$ConnectionString",
                                                             Value = "qa-connection-string"
                                                           };
      var qa = new EnvConfiguration("QA");
      qa.Add(qaDynamicConfigValues);
      environments.Add(qa.Name, qa);

      var prodDynamicConfigValues = new ConfigurationElement {
                                                               Key = "$ConnectionString",
                                                               Value = "prod-connection-string"
                                                             };
      var prod = new EnvConfiguration("PROD");
      environments.Add(prod.Name, prod);
    }

    public EnvConfiguration Get(string pEnvName) {
      return environments[pEnvName];
    }
  }
}