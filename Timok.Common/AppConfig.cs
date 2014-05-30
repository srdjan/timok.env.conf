using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace Timok.Common {
  public static class AppConfig {
    const string APP_SETTINGS_SECTION = "appSettings";

    public static string GetValue(string pConfigFile, string pKey, string pSection = APP_SETTINGS_SECTION) {
      string _settingValue = null;

      var _section = getConfigSection(pConfigFile, pSection);
      if (_section != null) {
        _settingValue = _section[pKey] as string;
      }
      return (_settingValue ?? string.Empty);
    }

    public static bool SetValue(string pConfigFile, string pKey, object pValue, string pSection = APP_SETTINGS_SECTION) {
      var _cfgDoc = new XmlDocument();
      _cfgDoc.Load(pConfigFile);
      var _node = _cfgDoc.SelectSingleNode("//" + pSection);

      if (_node == null) {
        throw new InvalidOperationException(string.Format("{0} section not found", pSection));
      }

      try {
        // XPath select setting "add" element that contains this key 	  
        var _addElem = (XmlElement)_node.SelectSingleNode("//add[@key='" + pKey + "']");
        if (_addElem != null) {
          _addElem.SetAttribute("value", pValue.ToString());
        }
          // not found, so we need to add the element, key and value
        else {
          var _entry = _cfgDoc.CreateElement("add");
          _entry.SetAttribute("key", pKey);
          _entry.SetAttribute("value", pValue.ToString());
          _node.AppendChild(_entry);
        }
        //save it
        saveConfigDoc(_cfgDoc, pConfigFile);
        return true;
      }
      catch {
        return false;
      }
    }

    //------------------------------------------ private 
    static IDictionary getConfigSection(string pConfigFile, string pSection) {
      try {
        var _cfgFile = new XmlDocument();
        var _reader = new XmlTextReader(pConfigFile);
        _cfgFile.Load(_reader);
        _reader.Close();

        var _nodes = _cfgFile.GetElementsByTagName(pSection);
        foreach (XmlNode _node in _nodes) {
          if (_node.LocalName == pSection) {
            var _handler = new DictionarySectionHandler();
            return (IDictionary)_handler.Create(null, null, _node);
          }
        }
      }
      catch {}
      return null;
    }

    static void saveConfigDoc(XmlNode pCfgDoc, string pCfgDocPath) {
      var _writer = new XmlTextWriter(pCfgDocPath, null) {
                                                           Formatting = Formatting.Indented
                                                         };
      pCfgDoc.WriteTo(_writer);
      _writer.Flush();
      _writer.Close();
    }
  }
}