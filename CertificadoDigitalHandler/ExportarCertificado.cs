using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;

namespace CertificadoDigitalHandler
{
    internal class ExportarCertificado
    {
        private readonly string path;
        private readonly string senha;
        private readonly string pathToSave;

        public ExportarCertificado(string nome, string path, string senha)
        {
            this.path = Path.Combine(path, String.Concat(nome,".pfx"));
            this.senha = senha;
            this.pathToSave = Path.Combine(path, "Save", String.Concat(nome, ".pfx"));
        }

        public X509Certificate2 ConverterCertificadoWinServer2019()
        {
            //try
            //{
            //    X509Certificate2 x509 = new X509Certificate2(path, senha, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
            //    return x509;
            //}
            //catch (Exception e)
            //{
                Runspace rs = RunspaceFactory.CreateRunspace();
                rs.Open();

                PowerShell ps = PowerShell.Create();
                ps.Runspace = rs;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Concat("$pwd = ConvertTo-SecureString -String ", senha, " -Force -AsPlainText;"));
                sb.AppendLine(string.Concat("$mypfx = Get-PfxData -FilePath ", path, " -Password $pwd;"));
                sb.AppendLine(string.Concat("Export-PfxCertificate -PFXData $mypfx -FilePath ", path, " -Password $pwd -CryptoAlgorithmOption TripleDES_SHA1;"));
                ps.AddScript(sb.ToString());
                ps.Invoke();

                //X509Certificate2 certTripleDES = new X509Certificate2(path, json.password);
                //return Convert.ToBase64String(certTripleDES.RawData);
                byte[] rawCert = File.ReadAllBytes(path);
                var string64cert = Convert.ToBase64String(rawCert);
                //Removendo caracteres indesejados
                string64cert = string64cert.Remove(0, 1);
                string64cert = string64cert.Remove(string64cert.Length - 1, 1);
                X509Certificate2 x509 = new X509Certificate2();
                x509.Import(Convert.FromBase64String(string64cert), senha,
                X509KeyStorageFlags.Exportable);

                File.WriteAllBytes(path, x509.RawData);
                return x509;

            //}
        }
    
        public void ExportarCertificadoAleMode()
        {
            ImportarCertificado();
            var string64Cert = File.ReadAllBytes(path);

            X509Certificate2 certToImport = new X509Certificate2(string64Cert, senha, X509KeyStorageFlags.Exportable);

            X509Store store2 = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store2.Open(OpenFlags.MaxAllowed);
            store2.Add(certToImport);
            store2.Close();

            store2.Open(OpenFlags.ReadOnly);
            foreach (var certBytes in from X509Certificate2 cert in store2.Certificates
                                      where cert.Subject.Contains(certToImport.Subject)
                                      let certBytes = cert.Export(X509ContentType.Pfx, senha)
                                      select certBytes)
            {
                File.WriteAllBytes(pathToSave, certBytes);
            }
        }

        private void ImportarCertificado()
        {
            var string64Cert = File.ReadAllBytes(path);

            X509Certificate2 certToImport = new X509Certificate2(string64Cert, senha, X509KeyStorageFlags.Exportable);

            X509Store store2 = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store2.Open(OpenFlags.MaxAllowed);
            store2.Add(certToImport);
            store2.Close();
        }
    }
}
