using DVLD_DataAccessLayer;
using System;
using System.Data;

namespace DVLD_BusinessLayer
{
    public class Tests
    {
        public int testID { get; set; }
        public int testAppointmentID { get; set; }
        public int testResult { get; set; }
        public string notes { get; set; }
        public int createdByUserID { get; private set; }

        public Tests()
        {
            this.testID = -1;
            this.testAppointmentID = -1;
            this.testResult = 0;
            this.notes = null;
            this.createdByUserID = -1;
        }

        private Tests(int testID, int testAppointmentID, int testResult, string notes, int createdByUserID)
        {
            this.testID = testID;
            this.testAppointmentID = testAppointmentID;
            this.testResult = testResult;
            this.notes = notes;
            this.createdByUserID = createdByUserID;
        }

        public static DataTable getTestRecords()
        {
            DataTable dt = new DataTable();
            dt = clsTestDataAccess.getTestRecords();
            return dt;
        }

        public static Tests findTest(int testID)
        {
            int testAppointmentID = -1;
            int testResult = 0;
            string notes = null;
            int createdByUserID = -1;

            if (clsTestDataAccess.findTest(testID, ref testAppointmentID, ref testResult, ref notes, ref createdByUserID))
            {
                return new Tests(testID, testAppointmentID, testResult, notes, createdByUserID);
            }
            return null;
        }

        public static bool isTestExist(int testID)
        {
            return clsTestDataAccess.isTestExist(testID);
        }

        public bool save()
        {
            if (this.testID == -1)
            {
                return addNewTest();
            }
            else
            {
                return updateTest();
            }
        }

        private bool addNewTest()
        {
            this.createdByUserID = 1;

            this.testID = clsTestDataAccess.addTest(
                this.testAppointmentID,
                this.testResult,
                this.notes,
                this.createdByUserID
            );

            return (this.testID != -1);
        }

        private bool updateTest()
        {
            return clsTestDataAccess.updateTest(
                this.testID,
                this.testAppointmentID,
                this.testResult,
                this.notes
            );
        }

        public static bool deleteTest(int testID)
        {
            return clsTestDataAccess.deleteTest(testID);
        }
    }
}