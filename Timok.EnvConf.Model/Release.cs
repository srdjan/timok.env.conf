namespace Timok.EnvConf.Model {
  public class Release {
    public Release(string pVersion, string pRevision) {
      Version = pVersion;
      Revision = pRevision;
    }

    public string Version { get; private set; }
    public string Revision { get; private set; }
  }
}