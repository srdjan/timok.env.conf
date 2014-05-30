using System;
using System.Collections.Generic;

namespace Timok.EnvConf.Model {
  public class EnvConfiguration {
    public List<ConfigurationElement> ConfigurationElements;

    public EnvConfiguration(string pName) {
      Name = pName;
      RootPath = ".";
      ConfigurationElements = new List<ConfigurationElement>();

      var env = new EnvConfiguration {
                                       Name = "PROD",
                                       Tiers = new[] {
                                                       new Tier {
                                                                  Name = "APP",
                                                                  Configuration = new[] {
                                                                                          new ConfigurationElement {
                                                                                                                     Key =
                                                                                                                       "$ConnectionStringDb1",
                                                                                                                     Value =
                                                                                                                       "prod-connection-string-db1"
                                                                                                                   },
                                                                                          new ConfigurationElement {
                                                                                                                     Key =
                                                                                                                       "$ConnectionStringDb2",
                                                                                                                     Value =
                                                                                                                       "prod-connection-string-db2"
                                                                                                                   },
                                                                                          new ConfigurationElement {
                                                                                                                     Key =
                                                                                                                       "$ConnectionStringDb3",
                                                                                                                     Value =
                                                                                                                       "prod-connection-string-db3"
                                                                                                                   }
                                                                                        }
                                                                },
                                                       new Tier {
                                                                  Name = "WEB",
                                                                  Configuration = new[] {
                                                                                          new ConfigurationElement {
                                                                                                                     Key =
                                                                                                                       "$ExtEndPoint1",
                                                                                                                     Value =
                                                                                                                       "1.1.1.1"
                                                                                                                   },
                                                                                          new ConfigurationElement {
                                                                                                                     Key =
                                                                                                                       "$ExtEndPoint2",
                                                                                                                     Value =
                                                                                                                       "1.1.1.2"
                                                                                                                   },
                                                                                          new ConfigurationElement {
                                                                                                                     Key =
                                                                                                                       "$ExtEndPoint3",
                                                                                                                     Value =
                                                                                                                       "1.1.1.3"
                                                                                                                   }
                                                                                        }
                                                                }
                                                     }
                                     };
    }

    EnvConfiguration() {
      throw new NotImplementedException();
    }

    public string Name { get; private set; }
    public Tier[] Tiers { get; set; }
    public Release CurrentRelease { get; private set; }
    public string RootPath { get; private set; }

    public void Add(ConfigurationElement pConfigElement) {
      ConfigurationElements.Add(pConfigElement);
    }

    public string GetConfigValue(string pKey) {
      throw new NotImplementedException();
    }
  }

  public class ConfigurationElement {
    public string Key { get; set; }
    public string Value { get; set; }
  }

  public class Tier {
    public string Name { get; set; }
    public ConfigurationElement[] Configuration { get; set; }
  }
}