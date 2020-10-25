namespace Data.BLL
{
    public enum Enum_UserGroups
    {
        Administrator = 1
    }

    public enum Enum_Keyvalues
    {
        CivilStatus = 1,
        Gender = 4,
        DrivingLicenceType = 44,
        YesNo = 71,
        TrueFalse = 95,
        Blood = 146,
        Brand = 156,
        DrivingType = 160,
        MaritalStatus = 164,
        PersonType = 167,
        ProjectType = 173,
        EducationStatus = 175,
        ParentialStatus = 180,
        Driver = 171,
        DriverType = 160,
        Hostess = 186,
        Personal = 170,
        Company = 187,
        Group = 191,
        Sector = 195,
        Region = 200,
        DiscountType = 208

    }

    public enum Enum_Combobox
    {
        Users,
        UserGroups,
        Customer,
        ProjectVehicle,
        Route,
        Vehicle,
        Hostess,
        Project,
        Driver,
        Person,
        Discount,
        CivilStatus,
        Genders,
        DrivingLicenceTypes,
        YesNo,
        TrueFalse,
        Blood,
        Brand,
        DrivingType,
        MaritalStatus,
        PersonType,
        ProjectType,
        EducationStatus,
        ParentialType,
        Supplier,
        Company,
        Group,
        Sector,
        Region,
        VehicleDrivers,
        SchoolClass,
        School,
        City,
        County,
        ContactUser,
    }

    public enum Enum_PersonType
    {
        Student = 169,
        Personal = 170,
        Driver = 171,
        Hostess = 186,
    }
    public enum Enum_Driver
    {
        Driver = 171,
    }
    public enum Enum_DriverType
    {
        Supplier = 161,
        Company = 162,
    }
    public enum Enum_Hostess
    {
        Hostess = 186,
    }
    public enum Enum_ParentType
    {
        Father = 182,
        Mother = 181,
        Other = 183,
    }
    public enum Enum_DiscountType
    {
        Percent = 209,
        Money = 210
    }

}
