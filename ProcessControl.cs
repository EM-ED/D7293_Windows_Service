using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace D7293_Windows_Service
{
    internal class ProcessControl
    {
        List<string> Whitelisted_Processes_MD5;

        public ProcessControl() 
        {

        }

        /// <summary>
        /// Verifies All Running Processes, Kills the ones that are netither have a valid X.509 Certificate or are part of the whitelist
        /// </summary>
        public void VerifyAllProcesses()
        {
            Process[] AllProcesses = GetAllRunningProcesses();
            foreach (Process Process_ in AllProcesses)
            {
                if(ProcessHasValidCertificate(Process_) || ProcessIsWhitelisted(Process_)) { continue; }
                else
                {
                    EndProcess(Process_);
                }
            }
        }

        #region Private Methods
        private Process[] GetAllRunningProcesses()
        {
            return Process.GetProcesses();
        }

        private static bool ProcessHasValidCertificate(Process Process_)
        {
            try
            {
                string pathToExe = Process_.MainModule.FileName;

                // Loads the X.509 certificate from the main EXE
                X509Certificate2 certificate = new X509Certificate2(pathToExe);

                return certificate.Verify();
            }
            catch
            {
                return false;
            }
        }

        private static bool ProcessIsWhitelisted(Process Process_)
        {
            ///REPLACE WITH CODE COMMUNICATING WITH SERVER!
            return false;
        }
        private static void EndProcess(Process Process_)
        {
            Process_.Kill();
        }
        #endregion
    }
}
