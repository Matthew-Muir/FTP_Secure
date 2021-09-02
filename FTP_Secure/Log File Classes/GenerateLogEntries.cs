using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace FTP_Secure
{
    public static class GenerateLogEntries
    {

        public static List<LogEntry> PopulateLogEntryList(XPathNavigator nav, XmlDocument doc)
        {
            


            var dts = nav.SelectSingleNode("/session/@start").Value.Substring(0, 19);
            dts = DateTime.Parse(dts).ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss");
            var protocol = DetermineProtocol(nav);
            var transferType = DetermineTransferType(doc);

            var transferredFilesList = PopulateListOfTransfers(doc);

            List<LogEntry> list = PopulateListOfLogEntries(PopulateListOfTransfers(doc), dts, protocol);

            return list;

        }

        private static Protocol DetermineProtocol(XPathNavigator nav)
        {
            var sessionNameValue = nav.SelectSingleNode("/session/group/@name").Value;

            if(sessionNameValue.Contains("sftp"))
            {
                return Protocol.Sftp;
            }
            else if (sessionNameValue.Contains("ftp") && (sessionNameValue.Contains("implicit") || sessionNameValue.Contains("explicit")))
            {
                return Protocol.Ftps;
            }
            else
            {
                return Protocol.Ftp;
            }
        }

        private static TransferType DetermineTransferType(XmlDocument doc)
        {
            if(doc.SelectNodes("//upload").Count > 0){
                return TransferType.Send;
            }
            else if(doc.SelectNodes("//download").Count > 0)
            {
                return TransferType.Receive;
            }
            else
            {
                return TransferType.Failure;
            }
        }

        private static List<ReturnedNodeList> PopulateListOfTransfers(XmlDocument doc)
        {
            List<ReturnedNodeList> list = new List<ReturnedNodeList>();
            
            if(doc.SelectNodes("//upload").Count > 0)
            {
                list.Add(new ReturnedNodeList(doc.SelectNodes("//upload"), TransferType.Send));

            }
            if(doc.SelectNodes("//download").Count > 0)
            {
                list.Add(new ReturnedNodeList(doc.SelectNodes("//download"), TransferType.Receive));

            }
            if(doc.SelectNodes("//failure").Count > 0)
            {
                list.Add(new ReturnedNodeList(doc.SelectNodes("//failure"), TransferType.Failure));

            }

            return list;





        }

        private static List<LogEntry> PopulateListOfLogEntries(List<ReturnedNodeList> listofLists, string dts, Protocol protocol)
        {
            var logList = new List<LogEntry>();

            //Loop through the lists of lists
            for (int i = 0; i < listofLists.Count; i++)
            {
                //temp val = first list of LoL
                ReturnedNodeList returnedNodeList = listofLists[i];

                //For lists that aren't of failure type do...
                if(returnedNodeList.transferType != TransferType.Failure)
                {
                    //loop through nodeList
                    for (int k = 0; k < returnedNodeList.nodeList.Count; k++)
                    {
                        logList.Add(new LogEntry()
                        {
                            Dts = dts,
                            Protocol = protocol,
                            TransferType = returnedNodeList.transferType,
                            TransferResult =  TransferResult.Success,
                            FileName = returnedNodeList.nodeList[k].ChildNodes[0].Attributes[0].Value,
                            Destination = returnedNodeList.nodeList[k].ChildNodes[1].Attributes[0].Value
                        });
                    }
                }
                //For lists that have failed transfers
                else if(returnedNodeList.transferType == TransferType.Failure)
                {
                    //Loop through nodeList
                    for (int j = 0; j < returnedNodeList.nodeList.Count; j++)
                    {
                        try
                        {

                            logList.Add(new LogEntry()
                            {
                                Dts = dts,
                                Protocol = protocol,
                                TransferType = TransferType.Failure,
                                TransferResult = TransferResult.Failure,
                                FileName = returnedNodeList.nodeList[j].ChildNodes[0].InnerXml.Replace("\n", " ").Trim(),
                                Destination = returnedNodeList.nodeList[j].ChildNodes[1].InnerXml.Replace("\n", " ").Trim()
                            });
                        }
                        catch (Exception e)
                        {

                            logList.Add(new LogEntry()
                            {
                                Dts = dts,
                                Protocol = protocol,
                                TransferType = TransferType.Failure,
                                TransferResult = TransferResult.Failure,
                                FileName = e.Message,
                                Destination = "Incorrect Task Editor Configuration"
                            });
                        }
                    }
                }
            }
            return logList;
        }
    }
}
