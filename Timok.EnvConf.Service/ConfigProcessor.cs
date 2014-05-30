using System;
using System.IO;
using System.Xml;
using Timok.Common;
using Timok.EnvConf.Model;
using Timok.EnvConf.Persistance;

namespace Timok.EnvConf.Service {
  public class ConfigProcessor {
    const string PREFIX = "$";
    const string FILE_MASK = "*.config";
    static EnvConfigurationRepository envRepo = new EnvConfigurationRepository();
    readonly string[] configFilePaths;
    readonly EnvConfiguration envConfig;

    public ConfigProcessor(string pEnvName, string pBuildPath) {
      envRepo = new EnvConfigurationRepository();
      envConfig = envRepo.Get(pEnvName);
      var rootPath = pBuildPath;
      const string appRootPath = ".";
      configFilePaths = Directory.GetFiles(Path.Combine(rootPath, appRootPath), FILE_MASK, SearchOption.AllDirectories);
    }

    public void Scan() {
      process(scanDynamicKeyValues);
    }

    public void Update() {
      process(setDynamicKeyValues);
    }

    void process(Action<string, string, string> pAction) {
      foreach (var filePath in configFilePaths) {
        var conf = new XmlDocument();
        conf.Load(filePath);
        var elements = conf.GetElementsByTagName("add");
        foreach (XmlNode element in elements) {
          foreach (XmlAttribute attribute in element.Attributes) {
            if (attribute.InnerText.StartsWith(PREFIX)) {
              var value = envConfig.GetConfigValue(attribute.InnerText);
              pAction(filePath, attribute.InnerText, value);
            }
          }
        }
      }
    }

    static void scanDynamicKeyValues(string pFilePath, string pKey, string pValue) {
      if (string.IsNullOrEmpty(pValue)) {
        throw new Exception("Unknown Config Element: " + pKey);
      }
    }

    static void setDynamicKeyValues(string pFilePath, string pKey, string pValue) {
      AppConfig.SetValue(pFilePath, pKey, pValue);
    }
  }
}