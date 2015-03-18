using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusQueues.Sender
{
    internal class DataHelper
    {
        internal static List<BrokeredMessage> GenerateMessages(DataTable issues)
        {
            // Instantiate the brokered list object
            var result = new List<BrokeredMessage>();

            // Iterate through the table and create a brokered message for each row
            foreach (DataRow item in issues.Rows)
            {
                var message = new BrokeredMessage();
                foreach (DataColumn property in issues.Columns)
                {
                    message.Properties.Add(property.ColumnName, item[property]);
                }
                result.Add(message);
            }
            return result;
        }

        internal static DataTable ParseCsvFile()
        {
            var tableIssues = new DataTable("Issues");
            const string path = @"..\..\data.csv";
            try
            {
                using (var readFile = new StreamReader(path))
                {
                    // create the columns
                    string line = readFile.ReadLine();
                    foreach (string columnTitle in line.Split(','))
                    {
                        tableIssues.Columns.Add(columnTitle);
                    }

                    while ((line = readFile.ReadLine()) != null)
                    {
                        string[] row = line.Split(',');
                        tableIssues.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e);
            }

            return tableIssues;
        }
    }
}