namespace Talent.Core
{
    public enum EntityMode
    {
        Create,
        Edit
    }

    public enum GenderId
    {
        Male = 1,
        Female = 2
    }

    public enum DocumentType
    {
        BankStatement = 1,
        IssuedID = 2,
        AddressProof = 3,
        SignedSelfie = 4
    }

    public enum DocumentStatus
    {
        Processing = 1,
        Approved = 2,
        Rejected = 3
    }


    public enum VerificationLevel
    {
        NotVerify = 0,
        Newbie = 10,
        Verified = 20,
        Trusted = 30,
        Institution = 40,
    }
   
}