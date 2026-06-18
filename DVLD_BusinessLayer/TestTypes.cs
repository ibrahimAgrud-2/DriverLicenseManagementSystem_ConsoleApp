using System.Data;

public class clsTestType
{
    public int TestTypeID { get; set; }
    public string TestTypeTitle { get; set; }
    public string TestTypeDescription { get; set; }
    public double TestTypeFees { get; set; }

    public clsTestType()
    {
        TestTypeID = -1;
        TestTypeTitle = string.Empty;
        TestTypeDescription = string.Empty;
        TestTypeFees = 0.0;
    }

    private clsTestType(int testTypeID, string testTypeTitle, string testTypeDescription, double testTypeFees)
    {
        TestTypeID = testTypeID;
        TestTypeTitle = testTypeTitle;
        TestTypeDescription = testTypeDescription;
        TestTypeFees = testTypeFees;
    }

    public static clsTestType findTestType(int testTypeID)
    {
        string title = string.Empty;
        string description = string.Empty;
        double fees = 0.0;

        if (TestTypesDataAccess.findTestType(testTypeID, ref title, ref description, ref fees))
        {
            return new clsTestType(testTypeID, title, description, fees);
        }

        return null;
    }

    public static bool isTestTypeExist(int testTypeID)
    {
        return TestTypesDataAccess.isTestTypeExistByID(testTypeID);
    }

    public static DataTable getAllRecords()
    {
        return TestTypesDataAccess.getTestTypesRecords();
    }
}