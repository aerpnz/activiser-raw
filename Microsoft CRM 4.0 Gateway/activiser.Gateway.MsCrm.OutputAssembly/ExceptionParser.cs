using System.Reflection;
using System;
using System.Collections;

namespace activiser.WebService.CrmOutputGateway
{
    public class ExceptionParser
    {
        private Exception _targetEx;

        private MemoryStatus memoryStatus = new MemoryStatus();
        private string _timeStampText = DateTime.Now.ToString("s");
        private string _ToString;

        public ExceptionParser(Exception targetException)
        {
            _targetEx = targetException;
            _ToString = this.GetExceptionString();
        }

        private Hashtable GetCustomExceptionInfo(Exception Ex)
        {
            Hashtable customInfo = new Hashtable();
            //PropertyInfo pi;
            foreach (PropertyInfo pi in Ex.GetType().GetProperties())
            {
                Type baseEx = typeof(System.Exception);
                if (baseEx.GetProperty(pi.Name) == null)
                {
                    customInfo.Add(pi.Name, pi.GetValue(Ex, null));
                }
            }
            return customInfo;
        }

        private string OtherInfomationToString()
        {
            try
            {
                Hashtable ht = GetCustomExceptionInfo(_targetEx);
                IDictionaryEnumerator ide = ht.GetEnumerator();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                while (ide.MoveNext())
                {
                    if (sb.ToString() != string.Empty)
                    {
                        sb.Append('\n');
                    }
                    sb.AppendFormat("{0}: ", ide.Key.ToString());
                    if (!(ide.Value == null))
                    {
                        sb.AppendFormat("{0}", ide.Value.ToString());
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message + "\n\n" + ex.GetType().FullName + "\n\nLocation:" + ex.StackTrace + "\nSource:" + ex.Source;
            }
            finally
            {
            }
        }

        public new string ToString()
        {
            return this._ToString;
        }

        private string GetTargetMethodFormat(Exception Ex)
        {
            return "[" + Ex.TargetSite.DeclaringType.Assembly.GetName().Name + "]" + Ex.TargetSite.DeclaringType.ToString() + "::" + Ex.TargetSite.Name + "()";
        }

        private string GetExceptionString()
        {
            System.Text.StringBuilder sbCopyInformation = new System.Text.StringBuilder(1000);
            try
            {
                Exception innerEx = _targetEx;
                sbCopyInformation.Append("Exception Information:\n");
                sbCopyInformation.AppendFormat("{0}: {1}\n", "Time", _timeStampText);
                sbCopyInformation.AppendFormat("{0}: {1}\n", "Module", _targetEx.GetType().FullName);
                sbCopyInformation.AppendFormat("{0}: {1}\n", "Message", _targetEx.Message);
                sbCopyInformation.AppendFormat("{0}: {1}\n", "Source", _targetEx.Source);
                sbCopyInformation.AppendFormat("{0}: {1}\n", "Target Method", GetTargetMethodFormat(_targetEx));
                sbCopyInformation.AppendFormat("{0}: {1}\n", "Help Link", _targetEx.HelpLink);
                sbCopyInformation.AppendFormat("{0}: \n{1}\n", "Stack Trace", _targetEx.StackTrace);

                if (!(innerEx == null))
                {
                    sbCopyInformation.Append("\nInner Exception Trace:\n");
                    int level = 0;
                    while (!(innerEx == null))
                    {
                        string indent = new string(' ', level * 4);
                        sbCopyInformation.AppendFormat("{0}{1}\n", indent, innerEx.GetType().FullName);
                        sbCopyInformation.AppendFormat("{0}{1}\n", indent, innerEx.Message);
                        sbCopyInformation.AppendFormat("{0}{1}\n", indent, GetTargetMethodFormat(innerEx));
                        innerEx = innerEx.InnerException;
                        level += 1;
                    }
                }

                string OtherInformationString = OtherInfomationToString();
                if (OtherInformationString != string.Empty)
                {
                    sbCopyInformation.AppendFormat("\n{0}:{1}\n", "Other Information", OtherInformationString);
                }

                sbCopyInformation.AppendLine();
                sbCopyInformation.AppendFormat("{0}:{1}", "System Information", '\n');
                sbCopyInformation.AppendFormat("{0}: {1}\n", "Windows User Name", Environment.UserName);
                sbCopyInformation.AppendFormat("{0}: {1}\n", "Machine Name", Environment.MachineName);
                sbCopyInformation.AppendFormat("{0}: {1}\n", "Domain Name", Environment.UserDomainName);
                sbCopyInformation.AppendFormat("{0}: {1} bytes ({2} GB)\n", "Total Physical Memory", memoryStatus.TotalPhysicalMemory, memoryStatus.TotalPhysicalMemory / 1000000000);
                sbCopyInformation.AppendFormat("{0}: {1} bytes ({2} GB)\n", "Available Physical Memory", memoryStatus.AvailablePhysicalMemory, memoryStatus.AvailablePhysicalMemory / 1000000000);
                sbCopyInformation.AppendFormat("{0}: {1}\n", "Operating System Version", Environment.OSVersion.ToString());
                //sbCopyInformation.AppendFormat("{0}: {1}\n", "Processor Type", m_ProcessorType);
                return sbCopyInformation.ToString();
            }
            catch (Exception ex)
            {
                return sbCopyInformation.ToString() +
                    string.Format("\nand got this exception whilst parsing the exception:\n{0}\nLocation:{1}\nSource:{2}\nStack Trace:{3}", ex.Message, ex.GetType().FullName, ex.Source, ex.StackTrace);
            }
            finally
            {
            }
        }

        class MemoryStatus
        {
            private struct MEMORYSTATUSEX
            {
                public int DWLength;
                public int DWLoad;
                public long DWTotalPhysicalMemory;
                public long DWAvailablePhysicalMemory;
                public long DWTotalPageFileMemory;
                public long DWAvailablePageFileMemory;
                public long DWTotalVirtualMemory;
                public long DWAvailableVirtualMemory;
                public long DWAvailableExtendedMemory;
            }
            private MEMORYSTATUSEX _MemoryStatusExtended;
            [System.Runtime.InteropServices.DllImport("Kernel32", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            static extern void GlobalMemoryStatusEx(ref MEMORYSTATUSEX _MemoryStatus);

            public MemoryStatus()
            {
                _MemoryStatusExtended.DWLength = System.Runtime.InteropServices.Marshal.SizeOf(_MemoryStatusExtended);
                GlobalMemoryStatusEx(ref _MemoryStatusExtended);
            }

            public long MemoryLoad { get { return _MemoryStatusExtended.DWLoad; } }
            public long TotalPhysicalMemory { get { return _MemoryStatusExtended.DWTotalPhysicalMemory; } }
            public long AvailablePhysicalMemory { get { return _MemoryStatusExtended.DWAvailablePhysicalMemory; } }
            public long TotalPageFileMemory { get { return _MemoryStatusExtended.DWTotalPageFileMemory; } }
            public long AvailablePageFileMemory { get { return _MemoryStatusExtended.DWAvailablePageFileMemory; } }
            public long TotalVirtualMemory { get { return _MemoryStatusExtended.DWTotalVirtualMemory; } }
            public long AvailableVirtualMemory { get { return _MemoryStatusExtended.DWAvailableVirtualMemory; } }
            public long AvailableExtendedVirtualMemory { get { return _MemoryStatusExtended.DWAvailableExtendedMemory; } }
        }
    }
}